namespace ASP.NET_API_Template.Core.Interfaces;

public interface IStudentRepository
{
    Task<BaseResponse<PagedResult<StudentDto>>> GetPagedStudents(
        int pageSize,
        int pageNumber,
        string searchTerm);

    Task<BaseResponse<StudentDto>> GetStudentById(int id);


    Task<BaseResponse<string>> AddStudentAsync(AddStudentDto studentDto);


    Task<BaseResponse<string>> UpdateStudentAsync(int id, UpdateStudentDto studentDto);



    Task<BaseResponse<string>> DeleteStudentAsync(int id);

}
