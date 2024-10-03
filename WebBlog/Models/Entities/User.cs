using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebBlog.Models.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Only 50 Characters are allowed!")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(100,ErrorMessage = "Only 100 Characters are allowed!")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be in the format any@any.com!")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8,ErrorMessage ="Password must be at least 8 digits")]
        public string Password { get; set; }
        public byte[]? ProfileImage { get; set; }
        public string? Role { get; set; }
        [Required]
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        [StringLength(500,ErrorMessage ="Address can not containe more than 500 characters!")]
        public string Address { get; set; }
        [Range(10000,100000,ErrorMessage ="Salary Must Be Between 10,000 : 100,000")]
        public double? Salary { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
