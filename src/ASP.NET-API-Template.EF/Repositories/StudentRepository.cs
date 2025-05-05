using ASP.NET_API_Template.Core.DTOs;
using Mapster;
using Microsoft.Extensions.Logging;

namespace ASP.NET_API_Template.EF.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<StudentRepository> _logger;

    public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger) : base(context, logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BaseResponse<PagedResult<StudentDto>>> GetPagedStudents(
        int pageSize,
        int pageNumber,
        string? searchTerm = null)
    {

        Expression<Func<Student, bool>> filter = x => true;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            filter = x => x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm)
            || x.Email.Contains(searchTerm) || x.PhoneNumber.Contains(searchTerm);
        }

        var students = await GetPagedDataWithSelectionAsync<StudentDto>(
            orderBy: x => x.FirstName,
            criteria: filter,
            pageSize: pageSize,
            pageNumber: pageNumber);

        return BaseResponse<PagedResult<StudentDto>>.SuccessResponse("student retrived succfully", students);
    }

    public async Task<BaseResponse<StudentDto>> GetStudentById(int id)
    {

        var student = await FindWithSelectionAsync<StudentDto>(x => x.Id == id);
        if (student is null)
            return BaseResponse<StudentDto>.ErrorResponse("student not found");

        return BaseResponse<StudentDto>.SuccessResponse("student retrived succfully", student);
    }


    public async Task<BaseResponse<string>> AddStudentAsync(AddStudentDto studentDto)
    {

        var student = studentDto.Adapt<Student>();
        _context.Add(student);
        _context.SaveChanges();

        return BaseResponse<string>.SuccessResponse();
    }


    public async Task<BaseResponse<string>> UpdateStudentAsync(int id, UpdateStudentDto studentDto)
    {

        if (id != studentDto.Id)
            return BaseResponse<string>.ErrorResponse("student not found");

        var student = await GetByIdAsync(id);

        student.FirstName = studentDto.FirstName;
        student.LastName = studentDto.LastName;
        student.Email = studentDto.Email;
        student.PhoneNumber = studentDto.PhoneNumber;
        student.ModifiedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return BaseResponse<string>.SuccessResponse();
    }



    public async Task<BaseResponse<string>> DeleteStudentAsync(int id)
    {
        var student = await GetByIdAsync(id);

        if (student is null)
            return BaseResponse<string>.ErrorResponse("student not found");

        await DeleteAsync(student);
        return BaseResponse<string>.SuccessResponse();

    }
}
