using System.ComponentModel.DataAnnotations;
using SampleAPI.Component.DomainLayer.Models.Entities;
using SampleAPI.Core.Common.Helpers;

namespace SampleAPI.Component.DomainLayer.Models
{
    public class UpdateUser
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

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