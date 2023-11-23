using System.ComponentModel.DataAnnotations;
using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Core.Common.Helpers;

namespace TodoAPI.Component.DomainLayer.Models.DTO
{
    public class UserUpdate
    {
        public string? UserName { get; set; }

        [EnumDataType(typeof(Role))]
        public string? Role { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // treat empty string as null for password fields to 
        // make them optional in front end apps
        private string? password;
        [MinLength(6)]
        public string? Password
        {
            get => password;
            set => password = StringConversions.ReplaceEmptyWithNull(value);
        }

        private string? confirmPassword;
        [Compare("Password")]
        public string? ConfirmPassword
        {
            get => confirmPassword;
            set => confirmPassword = StringConversions.ReplaceEmptyWithNull(value);
        }
    }
}