namespace sql_csharp_practice.Models
{
  public enum Gender
  {
    Male,
    Female
  }
  public abstract class Person
  {
    public static int nextId = 1;
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }

    public Person() { }
    protected Person(string firstName, string lastName, DateTime dateOfBirth, Gender gender, string address, string phoneNumber)
    {
      Id = GetNextId();
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
