using System.Data.SQLite;
using Dapper;
using sql_csharp_practice.Models;

namespace sql_csharp_practice.Services
{
  public static class DatabaseService
  {
    private static readonly string ConnectionString = "Data Source=appointmentManagementSys.db; Integrated Security=True";

    // CRUD OPERATIONS FOR PATIENTS
    public static void AddPatient(Patient patient)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        var sql = @"INSERT INTO patients (FirstName, LastName, DateOfBirth, Gender, Address, PhoneNumber)
                            VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Address, @PhoneNumber)";
        connection.Execute(sql, patient);
      }
    }

    public static Patient GetPatientById(int patientId)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        var patient = connection.QuerySingleOrDefault<Patient>(@"SELECT * FROM patients WHERE Id = @Id", new { Id = patientId });

        if (patient == null)
        {
          return null;
        }
        patient.Gender = (Gender)patient.Gender;
        patient.History = GetMedicalHistoryForPatient(patient.Id);
        return patient;
      }
    }
    public static List<Patient> GetPatients()
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        var queryPatients = "SELECT Id, FirstName, LastName, Gender FROM patients ORDER BY LastName ASC";
        var patients = connection.Query<Patient>(queryPatients).ToList();

        foreach (var patient in patients)
        {
          patient.Gender = (Gender)patient.Gender;
        }

        return patients;
      }
    }

    public static void AddAppointment(Appointment appointment)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var sql = @"INSERT INTO appointments (PatientId, Date, Status, Notes)
                            VALUES (@PatientId, @Date, @Status, @Notes)";
        connection.Execute(sql, new
        {
          appointment.Patient.Id,
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
        var appointments = connection.Query<Appointment>(@"SELECT * FROM appointments").ToList();

        foreach (var appointment in appointments)
        {
          appointment.Status = (AppointmentStatus)appointment.Status;
          appointment.Patient.Id = appointment.Patient.Id;
        }

        return appointments;
      }
    }

    public static void AddMedicalRecord(int patientId, MedicalHistory history)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        connection.Open();
        var sql = @"INSERT INTO medicalHistory (PatientId, Description)
                            VALUES (@PatientId, @Description)";
        connection.Execute(sql, new { PatientId = patientId, history.Description });
      }
    }

    public static void UpdatePatient(int patientId, string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        var sql = @"UPDATE patients
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

    // HELPERS
    private static List<MedicalHistory> GetMedicalHistoryForPatient(int patientId)
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        var medicalHistory = connection.Query<MedicalHistory>("SELECT Description FROM medicalHistory WHERE PatientId = @PatientId", new { PatientId = patientId }).ToList();

        if (medicalHistory.Count == 0)
        {
          return new List<MedicalHistory>(); // empty list if there is no medical records
        }

        return medicalHistory;
      }
    }

  }
}
