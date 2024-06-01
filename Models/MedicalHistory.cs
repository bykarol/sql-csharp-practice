namespace sql_csharp_practice.Models
{
  public class MedicalHistory
  {
    public DateTime Date { get; }
    public string Description { get; }
    // public Doctor Doctor { get; }

    public MedicalHistory(string description)
    {
      Date = DateTime.Now;
      Description = description;
    }

    public void DisplayMedicalHistoryInfo()
    {
      Console.WriteLine($"Date/Time: {Date:yyyy-MM-dd HH:mm:ss}H");
      Console.WriteLine($"Description: {Description}");
    }
  }
}