using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnAsp.Domain
{
    [Table("posts")]
    public class Post
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name"), MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; }
    }
}
