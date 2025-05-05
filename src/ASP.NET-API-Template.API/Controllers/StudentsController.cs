namespace ASP.NET_API_Template.API.Controllers;

[ApiController]
public class StudentsController(IUnitOfWork _unitOfWork) : ControllerBase
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
        int pageNumber = 1,
        [Range(1, 50)] int pageSize = 10,
        string? searchTerm = null)
    {
        var result = await _unitOfWork.Students.GetAllStudents(pageNumber, pageSize, searchTerm);
        if (result.Items == null || result.Items.Count == 0)
            return NotFound(BaseResponse<string>.ErrorResponse("لم يتم العثور على الطلاب"));


        return Ok(BaseResponse<PagedResult<StudentDto>>
            .SuccessResponse("تم جلب الطلاب بنجاح", result));
    }


    /// <summary>
    /// الحصول على طالب بواسطة المعرف
    /// </summary>
    /// <param name="Id">معرف الطالب</param>
    /// <returns>تفاصيل الطالب</returns>
    [HttpGet(SystemApiRouts.Students.GetById)]
    [ProducesResponseType(typeof(BaseResponse<StudentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<StudentDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetStudentById(int Id)
    {
        var result = await _unitOfWork.Students.GetStudentById(Id);
        if (result is null)
            return NotFound(BaseResponse<string>.ErrorResponse("لم يتم العثور على الطلاب"));


        return Ok(BaseResponse<StudentDto>.SuccessResponse("تم جلب الطالب بنجاح", result));
    }

    /// <summary>
    /// إنشاء طالب جديد
    /// </summary>
    /// </remarks>
    [HttpPost(SystemApiRouts.Students.Add)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStudent(AddStudentDto client)
    {
        var result = await _unitOfWork.Students.AddStudentAsync(client);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);

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
    public async Task<IActionResult> UpdateStudent(UpdateStudentDto client)
    {
        var result = await _unitOfWork.Students.UpdateStudentAsync(client);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    #endregion


    #region Delete Methods

    /// <summary>
    /// حذف طالب
    /// </summary>
    /// <param name="Id">معرف الطالب</param>
    /// <returns>لا يوجد محتوى</returns>
    [HttpDelete(SystemApiRouts.Students.Delete)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStudent(int Id)
    {
        var result = await _unitOfWork.Students.DeleteStudentAsync(Id);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    #endregion
}
