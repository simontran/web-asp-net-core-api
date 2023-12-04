namespace WebApiRestful.Domain.Models
{
    public class TodoUpdate
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? IsComplete { get; set; }
    }
}
