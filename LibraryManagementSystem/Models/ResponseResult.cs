
using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Models
{
    public class ResponseResult<T>
    {
        public T bookDto { get; set; }
        public string Message { get; set; } = string.Empty;
        
    }
}
