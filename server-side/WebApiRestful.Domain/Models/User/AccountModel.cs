using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Models
{
    public class AccountModel
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
