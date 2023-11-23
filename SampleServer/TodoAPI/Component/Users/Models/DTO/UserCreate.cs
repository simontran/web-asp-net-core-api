using System.ComponentModel.DataAnnotations;
using TodoAPI.Component.DomainLayer.Models.Entities;

namespace TodoAPI.Component.DomainLayer.Models.DTO
{
    public class UserCreate
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [EnumDataType(typeof(Role))]
        public string? Role { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}