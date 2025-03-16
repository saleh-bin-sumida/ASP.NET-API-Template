namespace ASP.NET_API_Template.Core.Models.Response;
public class BaseResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Errors { get; set; }


    //public BaseResponse(T? data, string message, List<string>? errors = null, bool success = true)
    //{
    //    Data = data;
    //    Message = message;
    //    Errors = errors;
    //    Success = success;
    //}
}
