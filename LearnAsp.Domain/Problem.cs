using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnAsp.Domain
{
    [Table("problems")]
    public class Problem
    {
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("name"), MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
    }
}
