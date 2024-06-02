using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearnAsp.Domain
{
    [Table("users")]
    public class User
    {
        [Key, Column("id", TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("login"), MaxLength(100)]
        public string Login { get; set; } = string.Empty;

        [Column("password"), MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
