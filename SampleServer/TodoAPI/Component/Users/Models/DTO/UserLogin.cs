using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Component.DomainLayer.Models.DTO
{
    public class UserLogin
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}