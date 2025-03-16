namespace ASP.NET_API_Template.Core.Mapping;

public static class StudentProfile
{
    public static Expression<Func<Student, GetStudentDto>> ToGetStudentDto()
    {
        return client => new GetStudentDto
        {
            Id = client.Id,
            FullName = client.FullName,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
        };
    }

    public static Student ToStudent(this AddStudentDto addStudentDto)
    {
        return new Student
        {
            FullName = addStudentDto.FullName,
            Email = addStudentDto.Email,
            PhoneNumber = addStudentDto.PhoneNumber,
        };
    }

    public static void UpdateStudent(this Student client, UpdateStudentDto updateStudentDto)
    {
        client.FullName = updateStudentDto.FullName;
        client.Email = updateStudentDto.Email;
        client.PhoneNumber = updateStudentDto.PhoneNumber;
        client.ModifiedDate = DateTime.Now;
    }
}
