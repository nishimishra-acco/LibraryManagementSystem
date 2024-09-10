
using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Models
{
    public class Response<T>
    {
        public T data { get; set; }
        public string Message { get; set; } = string.Empty;
        
    }
}
