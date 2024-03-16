namespace Apis.Models
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public object Errors { get; set; }
        public object StatusCode { get; set; }
    }
}
