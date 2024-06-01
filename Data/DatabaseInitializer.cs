using System.Data.SQLite;
using Dapper;

namespace sql_csharp_practice.Services.Data
{
  public static class DatabaseInitializer
  {
    private static readonly string ConnectionString = "Data Source=appointmentManagementSys.db; Integrated Security=True";

    static DatabaseInitializer()
    {
      InitializeDatabase();
    }

    public static void InitializeDatabase()
    {
      using (var connection = new SQLiteConnection(ConnectionString))
      {
        var createPatientTable = @"CREATE TABLE IF NOT EXISTS patients (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            FirstName VARCHAR(100) NOT NULL,
                                            LastName VARCHAR(100) NOT NULL,
                                            DateOfBirth DATE NOT NULL,
                                            Gender INTEGER NOT NULL,
                                            Address VARCHAR(250) NOT NULL,
                                            PhoneNumber VARCHAR(15) NOT NULL)";

        var createAppointmentTable = @"CREATE TABLE IF NOT EXISTS appointments (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                PatientId INTEGER NOT NULL,
                                                Date Datetime NOT NULL,
                                                Status INTEGER NOT NULL,
                                                Notes TEXT,
                                                FOREIGN KEY (PatientId) REFERENCES patients(Id))";

        var createMedicalHistoryTable = @"CREATE TABLE IF NOT EXISTS medicalHistory (
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                  Date Datetime,
                                                  PatientId INTEGER NOT NULL,
                                                  Description TEXT NOT NULL,
                                                  FOREIGN KEY (PatientId) REFERENCES patients(Id))";

        connection.Execute(createPatientTable);
        connection.Execute(createAppointmentTable);
        connection.Execute(createMedicalHistoryTable);
      }
    }
  }
}
