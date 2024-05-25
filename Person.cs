namespace SQL_CSharp_Practice
{
  enum Gender
  {
    Male,
    Female
  }
  abstract class Person
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    public Person(string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber)
    {
      FirstName = firstName;
      LastName = lastName;
      DateOfBirth = dateOfBirth;
      Gender = gender;
      Address = address;
      PhoneNumber = phoneNumber;
    }
    public virtual void DisplayInformation()
    {
      Console.WriteLine($"FullName: {FirstName} {LastName}");
      Console.WriteLine($"Date of Birth: {DateOfBirth.ToShortDateString()}");
      Console.WriteLine($"Gender: {Gender}");
      Console.WriteLine($"Address: {Address}");
      Console.WriteLine($"Phone Number: {PhoneNumber}");
    }
  }
}
