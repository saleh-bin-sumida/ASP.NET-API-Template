namespace ASP.NET_API_Template.API.Controllers;

[ApiController]
public class StudentsController(IUnitOfWork _unitOfWork, ILogger<StudentsController> _logger) : ControllerBase
{


    #region Get Methods

    /// <summary>
    /// الحصول على جميع الطلاب
    /// </summary>
    /// <param name="pageNumber">رقم الصفحة</param>
    /// <param name="pageSize">حجم الصفحة</param>
    /// <param name="searchTerm">نص البحث, اختياري</param>
    /// <remarks>
    /// سيتم جلب الطلاب الذين تحتوي اسماءهم او اي حقل من حقولهم عل النص البحثي في حالة ارفاقه ومن النوع المحدد
    /// <br/>
    /// في حالة لم يتم تحديد نص بحثي او نوع الطالب سيم الجلب حسب الصفحات    
    /// </remarks>
    /// <returns>قائمة الطلاب</returns>
    [HttpGet(SystemApiRouts.Students.GetAll)]
    [ProducesResponseType(typeof(BaseResponse<PagedResult<StudentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllStudents(
        int pageSize = 10,
        int pageNumber = 1,
        string? searchTerm = null)
    {
        var response = await _unitOfWork.Students.GetPagedStudents(pageSize, pageNumber, searchTerm);
        return Ok(response);
    }


    /// <summary>
    /// الحصول على طالب بواسطة المعرف
    /// </summary>
    /// <param name="id">معرف الطالب</param>
    /// <returns>تفاصيل الطالب</returns>
    [HttpGet(SystemApiRouts.Students.GetById)]
    [ProducesResponseType(typeof(BaseResponse<StudentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<StudentDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var response = await _unitOfWork.Students.GetStudentById(id);
        if (!response.Success)
        {
            _logger.LogWarning("Student with ID: {Id} not found", id);
            return BadRequest(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// إنشاء طالب جديد
    /// </summary>
    /// </remarks>
    [HttpPost(SystemApiRouts.Students.Add)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStudent(AddStudentDto studentDto)
    {
        var response = await _unitOfWork.Students.AddStudentAsync(studentDto);
        if (!response.Success)
        {
            _logger.LogError("Failed to create student: {Errors}", response.Errors);
            return BadRequest(response);
        }
        return Ok(response);

    }

    #endregion


    #region Put Methods

    /// <summary>
    /// تحديث طالب موجود
    /// </summary>
    /// <returns>لا يوجد محتوى</returns>
    [HttpPut(SystemApiRouts.Students.Update)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto studentDto)
    {
        var response = await _unitOfWork.Students.UpdateStudentAsync(id, studentDto);
        if (!response.Success)
        {
            _logger.LogError("Failed to update student with ID: {Id}", id);
            return BadRequest(response);
        }
        return Ok(response);
    }

    #endregion


    #region Delete Methods

    /// <summary>
    /// حذف طالب
    /// </summary>
    /// <param name="id">معرف الطالب</param>
    /// <returns>لا يوجد محتوى</returns>
    [HttpDelete(SystemApiRouts.Students.Delete)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var response = await _unitOfWork.Students.DeleteStudentAsync(id);
        if (!response.Success)
        {
            _logger.LogError("Failed to delete student with ID: {Id}", id);
            return BadRequest(response);
        }
        return Ok(response);
    }

    #endregion
}
