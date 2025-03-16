namespace ASP.NET_API_Template.Core.Interfaces;

public interface IStudentRepository : IBaseRepository<Student>
{
    public Task<GetStudentDto> GetStudentById(int id);
    public Task<PagedResult<GetStudentDto>> GetAllStudents(
        int pageNumber,
        int pageSize,
        string? searchTerm = null);
    public Task<BaseResponse<string>> AddStudentAsync(AddStudentDto clientDto);
    public Task<BaseResponse<string>> UpdateStudentAsync(UpdateStudentDto clientDto);
    public Task<BaseResponse<string>> DeleteStudentAsync(int id);
}
