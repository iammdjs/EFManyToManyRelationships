
namespace EFManyToMany.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public ICollection<Tag>? Tags { get; set; }
    }
}
