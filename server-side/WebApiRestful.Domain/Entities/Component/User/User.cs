using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiRestful.Domain.Entities.Common;

namespace WebApiRestful.Domain.Entities.Component
{
    public class User : BaseEntity
    {
        [Required]
        public string? UserName { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
