namespace printTable;

public class Employee
{
    public Employee(){}
    public Employee(int id, string? name, DateTime dateOfBirth)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
    }
    public Employee(int id, string? name, DateTime dateOfBirth, bool isDelete)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        IsDelete = isDelete;
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    public bool IsDelete;
 

}   