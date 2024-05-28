namespace sql_csharp_practice
{
  class Doctor : Person // Inherit from the abstract class Person
  {
    public string Speciality = "";
    public Doctor(string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber, string speciality)
        : base(firstName, lastName, dateOfBirth, gender, address, phoneNumber)
    {
      Speciality = speciality;
    }

    public override void DisplayInformation()
    {
      base.DisplayInformation();
      Console.WriteLine($"Speciality: {Speciality}");
    }

  }
}
