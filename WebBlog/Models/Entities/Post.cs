using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBlog.Models.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Only 50 Characters are allowed!")]
        public string Title { get; set; }
        public byte[]? PostImage { get; set; }
        [Required]
        [StringLength(int.MaxValue)]
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
