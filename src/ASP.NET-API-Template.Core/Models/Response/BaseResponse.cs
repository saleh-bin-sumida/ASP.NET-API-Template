namespace ASP.NET_API_Template.Core.Models.Response;
public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; set; }


    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Errors { get; set; }


    public BaseResponse(bool success, string message, T? data = default, List<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors;
    }

    public BaseResponse()
    {

    }

    public static BaseResponse<T> SuccessResponse(string message = "Operation successful", T? data = default)
    {
        return new BaseResponse<T>(true, message, data);
    }

    public static BaseResponse<T> ErrorResponse(string message, List<string>? errors = null)
    {
        return new BaseResponse<T>(false, message, default, errors);
    }
}
