using System.Text.Json.Serialization;
using SampleAPI.Core.DomainLayer.Models.Entities;

namespace SampleAPI.Component.Users.Models.Entities
{
    public class User : BaseEntity
    {
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}