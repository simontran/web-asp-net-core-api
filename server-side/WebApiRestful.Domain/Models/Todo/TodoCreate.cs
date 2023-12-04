using System.ComponentModel.DataAnnotations;

namespace WebApiRestful.Domain.Models
{
    public class TodoCreate
    {
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int? IsComplete { get; set; }
    }
}
