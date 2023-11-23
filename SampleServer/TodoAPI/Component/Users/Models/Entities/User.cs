using System.Text.Json.Serialization;
using TodoAPI.Core.DomainLayer.Models.Entities;

namespace TodoAPI.Component.DomainLayer.Models.Entities
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}