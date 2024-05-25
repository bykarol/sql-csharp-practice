namespace SQL_CSharp_Practice
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("#######################################################");
      Console.WriteLine("Welcome to the Appointment Management System (AMS)!");
      Console.WriteLine("Efficiently Streamlining Clinic Operations.");
      Console.WriteLine("########################################################");

      bool exit = false;
      while (!exit)
      {
        Console.WriteLine("\nMAIN MENU");
        Console.WriteLine("1. Manage Patients");
        Console.WriteLine("2. Manage Appointments");
        Console.WriteLine("3. Exit");
        Console.Write("Enter an option from the menu: ");

        string? userInput = Console.ReadLine();
        Console.Clear();
        switch (userInput)
        {
          case "1":
            PatientsManager.PatientsManagerMenu();
            break;
          case "2":
            Console.WriteLine("Manage Appointments Menu");
            AppointmentsManager.AppointmentsManagerMenu();
            break;
          case "3":
            Console.WriteLine("Thank you for using the Appointment Management System. Goodbye!");
            exit = true;
            break;
          default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
        }
      }
    }
  }
}
