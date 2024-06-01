namespace sql_csharp_practice.Models
{
  public class MedicalHistory
  {
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int PatientId { get; set; }

    public MedicalHistory(int patientId, string description, DateTime date)
    {
      Date = date;
      Description = description;
      PatientId = patientId;
    }

    public MedicalHistory()
    { }

    public void DisplayMedicalHistoryInfo()
    {
      Console.WriteLine($"Date/Time: {Date:yyyy-MM-dd HH:mm:ss}H");
      Console.WriteLine($"Description: {Description}");
    }
  }
}