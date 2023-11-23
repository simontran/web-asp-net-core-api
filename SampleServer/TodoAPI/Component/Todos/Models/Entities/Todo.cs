using TodoAPI.Core.DomainLayer.Models.Entities;

namespace TodoAPI.Component.DomainLayer.Models.Entities
{
    public class Todo : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? IsComplete { get; set; }
    }
}