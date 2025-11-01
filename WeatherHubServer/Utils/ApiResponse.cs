using System.Text.Json.Serialization;

namespace Utils
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Errors { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data) =>
            new ApiResponse<T> { Success = true, Data = data };

        public static ApiResponse<T> Fail(string message, object? errors = null) =>
            new ApiResponse<T> { Success = false, Message = message, Errors = errors };


    }
}
