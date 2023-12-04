namespace WebApiRestful.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
