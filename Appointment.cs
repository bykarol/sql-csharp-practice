namespace SQL_CSharp_Practice
{
  public enum AppointmentStatus
  {
    Pending,
    Confirmed,
    Canceled,
    Completed
  }

  class Appointment
  {
    private static int nextId = 1;
    public int ID { get; }
    public Patient Patient { get; }
    public DateTime Date { get; set; }
    public AppointmentStatus Status { get; set; }
    public string Notes { get; set; }

    public Appointment(Patient patient, DateTime date, string notes = "")
    {
      ID = GetNextId();
      Patient = patient;
      Date = date;
      Status = AppointmentStatus.Pending;
      Notes = notes;
    }

    public void DisplayAppointmentInfo()
    {
      Console.WriteLine($"Appointment ID: {ID}");
      Console.WriteLine($"{Patient.FirstName} {Patient.LastName}");
      Console.WriteLine($"Date/Time: {Date:yyyy-MM-dd HH:mm:ss}");
      Console.WriteLine($"Status: {Status}");
      if (!string.IsNullOrEmpty(Notes))
      {
        Console.WriteLine($"Notes: {Notes}");
      }
    }

    // aditional methods
    private static int GetNextId()
    {
      return nextId++;
    }
  }
}