using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBlog.Models.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(int.MaxValue,ErrorMessage ="Your Comment is to long!")]
        public string Content { get; set; }
        public byte[]? CommentImage { get; set; }
        public DateTime CreationTime { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
