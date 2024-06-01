using sql_csharp_practice.Services;
using sql_csharp_practice.Models;
namespace sql_csharp_practice.Controllers
{
  class AppointmentsManager
  {
    public static List<Appointment> Appointments = new List<Appointment>();
    public static List<Patient> Patients;

    public AppointmentsManager(List<Patient> patients)
    {
      Patients = patients;
    }
    public static void AppointmentsManagerMenu()
    {
      Console.WriteLine("\nAPPOINTMENTS MENU:");
      Console.WriteLine("1. Schedule Appointment");
      Console.WriteLine("2. Update Appointment Status");
      Console.WriteLine("3. Cancel Appointment");
      Console.WriteLine("4. View Patient Appointments");
      Console.WriteLine("5. View Appointment List");
      Console.WriteLine("6. Return to Main Menu");
      Console.Write("Enter an option from the menu: ");

      string? userInput = Console.ReadLine();
      Console.Clear();
      switch (userInput)
      {
        case "1":
          ScheduleAppointment();
          break;
        case "2":
          UpdateAppointmentStatus();
          break;
        case "3":
          CancelAppointment();
          break;
        case "4":
          DisplayPatientAppointments();
          break;
        case "5":
          AppointmentScheduleList();
          break;
        case "6":
          Console.WriteLine("Returning to main menu...");
          break;
        default:
          Console.WriteLine("Invalid choice. Please try again.");
          break;
      }
    }


    // Appointments Management Methods
    public static void ScheduleAppointment()
    {
      try
      {
        Patient patient = PatientsManager.GetPatient();
        if (patient == null)
        {
          return;
        }
        // getting the appointment data
        Console.WriteLine($"Scheduling an appointment: {patient.FirstName} {patient.LastName}");
        Console.Write("Enter appointment date and time (yyyy-MM-dd HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime appointmentDate))
        {
          throw new FormatException("Invalid date format. Please use YYYY-MM-DD format.");
        }
        Console.Write("Appointment notes: ");
        string? notes = Console.ReadLine();

        // Data validation
        if (appointmentDate == DateTime.MinValue)
        {
          throw new ArgumentException("Date is required to book a new appointment");
        }

        // Scheduling a new appointment
        Appointment newAppointment;
        newAppointment = new Appointment(patient, appointmentDate, notes);

        // Adding the appointment to the list
        Appointments.Add(newAppointment);

        // Message to the user
        Console.WriteLine("Appointment successfully scheduled!");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
    public static void DisplayPatientAppointments()
    {
      Patient patient = PatientsManager.GetPatient();
      if (patient == null) { return; }
      List<Appointment> patientAppointments = Appointments.Where(x => x.Patient.FirstName == patient.FirstName && x.Patient.LastName == patient.LastName).ToList();
      if (patientAppointments == null || patientAppointments.Count == 0)
      {
        Console.WriteLine($"{patient.FirstName} {patient.LastName} has no appointments booked");
        return;
      }
      Console.WriteLine("************************");
      Console.WriteLine($"Scheduled Appointments");
      foreach (Appointment appointment in patientAppointments)
      {
        appointment.DisplayAppointmentInfo();
        Console.WriteLine("************************");
      }
    }
    public static void AppointmentScheduleList()
    {
      if (Appointments.Count == 0)
      {
        Console.WriteLine("There are not booked appointments.");
        return;
      }
      Console.WriteLine("************************");
      Console.WriteLine("Scheduled Appointment List");
      foreach (Appointment appointment in Appointments)
      {
        appointment.DisplayAppointmentInfo();
      }
      Console.WriteLine("************************");
    }
    public static void CancelAppointment()
    {
      try
      {
        Console.WriteLine("Canceling an appointment...");
        Patient patient = PatientsManager.GetPatient();
        if (patient == null) { return; }
        Console.Write($"Enter the ID of the appointment to Cancel: ");
        if (!int.TryParse(Console.ReadLine(), out int userInput))
        {
          Console.WriteLine("Invalid input. Please enter a valid ID.");
          return;
        }

        Appointment appointmentToCancel = Appointments.FirstOrDefault(x => x.Patient.FirstName == patient.FirstName && x.Patient.LastName == patient.LastName && x.ID == userInput);
        if (appointmentToCancel == null)
        {
          Console.WriteLine($"Appointment with ID #{userInput} does not exist.");
          return;
        }

        // Changing appointment status to canceled
        appointmentToCancel.Status = AppointmentStatus.Canceled;
        Console.WriteLine($"Appointment #{userInput} was canceled.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }

    public static void UpdateAppointmentStatus()
    {
      try
      {
        Console.WriteLine("Update Appointment Status...");
        Patient patient = PatientsManager.GetPatient();
        if (patient == null) { return; }
        Console.Write($"Enter the ID of the appointment to Cancel: ");
        if (!int.TryParse(Console.ReadLine(), out int userInput))
        {
          Console.WriteLine("Invalid input. Please enter a valid ID.");
          return;
        }
        Console.Write("Enter new Status (Pending/Confirmed/Canceled/Completed): ");
        if (!Enum.TryParse(Console.ReadLine(), out AppointmentStatus status) || !Enum.IsDefined(typeof(AppointmentStatus), status))
        {
          throw new ArgumentException("Invalid status. Please enter Pending, Confirmed, Canceled or Completed");
        }

        Appointment appointmentToUpdate = Appointments.FirstOrDefault(x => x.Patient.FirstName == patient.FirstName && x.Patient.LastName == patient.LastName && x.ID == userInput);
        if (appointmentToUpdate == null)
        {
          Console.WriteLine($"Appointment with ID #{userInput} does not exist.");
          return;
        }

        // Changing appointment status to canceled
        appointmentToUpdate.Status = status;
        Console.WriteLine($"Appointment #{userInput} was succesfully updated.");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
    }
  }
}