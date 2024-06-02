using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnAsp.Domain
{
    [Table("visits")]
    public class Visit
    {
        [Key, Column("id")]
        public long Id { get; set; }

        [Column("user_id", TypeName = "uniqueidentifier")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Column("problem_id")]
        public int ProblemId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("target")]
        [MaxLength(100)]
        public string Target { get; set; } = string.Empty;

        [Column("status")]
        public Status Status { get; set; }

        public User? User { get; set; }
        public Problem? Problem { get; set; }
    }
}
