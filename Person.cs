using System.Data.Common;

namespace sql_csharp_practice
{
  public enum Gender
  {
    Male,
    Female
  }
  public abstract class Person
  {
    public static int nextId = 1; public string FirstName { get; set; }
    public int ID { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    public Person(string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber)
    {
      ID = GetNextId();
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

    // aditional methods
    public static int GetNextId()
    {
      return nextId++;
    }
  }
}
