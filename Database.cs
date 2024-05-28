using System.Data.SQLite;
using Dapper;

namespace sql_csharp_practice
{
  public static class Database
  {
    private const string ConnectionString = "Data Source=appointments.db;";

    static Database()
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var createPatientTable = @"CREATE TABLE IF NOT EXISTS Patients (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            FirstName TEXT NOT NULL,
                                            LastName TEXT NOT NULL,
                                            DateOfBirth TEXT NOT NULL,
                                            Gender INTEGER NOT NULL,
                                            Address TEXT NOT NULL,
                                            PhoneNumber TEXT NOT NULL)";

        var createAppointmentTable = @"CREATE TABLE IF NOT EXISTS Appointments (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                PatientId INTEGER NOT NULL,
                                                Date TEXT NOT NULL,
                                                Status INTEGER NOT NULL,
                                                Notes TEXT,
                                                FOREIGN KEY (PatientId) REFERENCES Patients(Id))";

        var createMedicalHistoryTable = @"CREATE TABLE IF NOT EXISTS MedicalHistory (
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                  PatientId INTEGER NOT NULL,
                                                  Description TEXT NOT NULL,
                                                  FOREIGN KEY (PatientId) REFERENCES Patients(Id))";

        connection.Execute(createPatientTable);
        connection.Execute(createAppointmentTable);
        connection.Execute(createMedicalHistoryTable);
      }
    }

    public static void AddPatient(Patient patient)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var sql = @"INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, Address, PhoneNumber)
                            VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Address, @PhoneNumber)";
        connection.Execute(sql, new
        {
          patient.FirstName,
          patient.LastName,
          DateOfBirth = patient.DateOfBirth.ToString("yyyy-MM-dd"),
          Gender = (int)patient.Gender,
          patient.Address,
          patient.PhoneNumber
        });
      }
    }

    public static List<Patient> GetPatients()
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var patients = connection.Query<Patient>(@"SELECT Id, FirstName, LastName, DateOfBirth, Gender, Address, PhoneNumber FROM Patients").ToList();

        foreach (var patient in patients)
        {
          patient.Gender = (Gender)patient.Gender;
          patient.History = GetMedicalHistoryForPatient(patient.ID);
        }

        return patients;
      }
    }

    private static List<MedicalHistory> GetMedicalHistoryForPatient(int patientId)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        return connection.Query<MedicalHistory>("SELECT Description FROM MedicalHistory WHERE PatientId = @PatientId", new { PatientId = patientId }).ToList();
      }
    }

    public static void AddAppointment(Appointment appointment)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var sql = @"INSERT INTO Appointments (PatientId, Date, Status, Notes)
                            VALUES (@PatientId, @Date, @Status, @Notes)";
        connection.Execute(sql, new
        {
          appointment.Patient.ID,
          Date = appointment.Date.ToString("yyyy-MM-dd HH:mm:ss"),
          Status = (int)appointment.Status,
          appointment.Notes
        });
      }
    }

    public static List<Appointment> GetAppointments()
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var appointments = connection.Query<Appointment>(@"SELECT Id, PatientId, Date, Status, Notes FROM Appointments").ToList();

        foreach (var appointment in appointments)
        {
          appointment.Status = (AppointmentStatus)appointment.Status;
          appointment.Patient.ID = appointment.Patient.ID;
        }

        return appointments;
      }
    }

    public static Patient GetPatientById(int patientId)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var patient = connection.QuerySingleOrDefault<Patient>(@"SELECT Id, FirstName, LastName, DateOfBirth, Gender, Address, PhoneNumber FROM Patients WHERE Id = @Id", new { Id = patientId });

        if (patient != null)
        {
          patient.Gender = (Gender)patient.Gender;
          patient.History = GetMedicalHistoryForPatient(patient.ID);
        }

        return patient;
      }
    }

    public static void AddMedicalRecord(int patientId, MedicalHistory history)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var sql = @"INSERT INTO MedicalHistory (PatientId, Description)
                            VALUES (@PatientId, @Description)";
        connection.Execute(sql, new { PatientId = patientId, history.Description });
      }
    }

    public static void UpdatePatient(int patientId, string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var sql = @"UPDATE Patients
                    SET FirstName = @FirstName,
                        LastName = @LastName,
                        DateOfBirth = @DateOfBirth,
                        Gender = @Gender,
                        Address = @Address,
                        PhoneNumber = @PhoneNumber
                    WHERE Id = @Id";

        var parameters = new
        {
          patientId,
          firstName,
          lastName,
          dateOfBirth,
          gender,
          address,
          phoneNumber
        };

        connection.Execute(sql, parameters);
      }
    }

  }
}
