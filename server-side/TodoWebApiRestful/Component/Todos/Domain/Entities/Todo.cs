using TodoWebApiRestful.Common.DomainLayer.Entities;

namespace TodoWebApiRestful.Component.DomainLayer.Entities
{
    public class Todo : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? IsComplete { get; set; }
    }
}
