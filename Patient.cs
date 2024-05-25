namespace SQL_CSharp_Practice
{
  class Patient : Person // Inherit from the abstract class Person
  {
    public List<MedicalHistory> History { get; }
    // public List<Appointment> Appointments { get; set; }

    public Patient(string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber)
        : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber)
    {
      History = new List<MedicalHistory>(); // empty list
      // Appointments = new List<Appointment>();
    }

    public override void DisplayInformation()
    {
      base.DisplayInformation();
      try
      {
        if (History != null && History.Count > 0)
        {
          Console.WriteLine($"Medical History:");
          int counter = 1;
          foreach (MedicalHistory record in History)
          {
            Console.WriteLine($"Record #{counter}.");
            record.DisplayMedicalHistoryInfo();
            counter++;
          }
        }

        // if (Appointments != null && Appointments.Count > 0)
        // {
        //   Console.WriteLine("***********************");
        //   Console.WriteLine($"Appointments:");
        //   int counter = 1;
        //   foreach (Appointment appointment in Appointments)
        //   {
        //     Console.WriteLine($"Appointment #{counter}.");
        //     appointment.DisplayAppointmentInfo();
        //     counter++;
        //   }
        // }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error displaying medical history: {ex.Message}");
      }
    }

    public void AddMedicalRecord(string description)
    {
      MedicalHistory record = new MedicalHistory(description);
      History.Add(record);
    }


  }
}
