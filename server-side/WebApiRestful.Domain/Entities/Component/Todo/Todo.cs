using WebApiRestful.Domain.Entities.Common;

namespace WebApiRestful.Domain.Entities.Component
{
    public class Todo : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? IsComplete { get; set; }
    }
}
