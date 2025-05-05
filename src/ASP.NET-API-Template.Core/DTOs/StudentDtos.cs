namespace ASP.NET_API_Template.Core.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class AddStudentDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public required string PhoneNumber { get; set; }
}

public class UpdateStudentDto
{
    public int Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public required string PhoneNumber { get; set; }

}
