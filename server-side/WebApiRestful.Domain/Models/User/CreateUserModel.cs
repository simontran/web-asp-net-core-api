using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Models
{
    public class CreateUserModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
