using Microsoft.EntityFrameworkCore;
using WebBlog.Models.Entities;

namespace WebBlog.Models.Context
{
    public class BlogDBContext: DbContext
    {
        public BlogDBContext() : base() { }
        public BlogDBContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Posts).WithOne(p => p.User).HasForeignKey(p=>p.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>().HasMany(p=>p.Comments).WithOne(c => c.Post).HasForeignKey(c=>c.PostId);
            modelBuilder.Entity<User>().HasMany(u => u.Categories).WithOne(c => c.User).HasForeignKey(u => u.UserId);
            modelBuilder.Entity<Category>().HasMany(c => c.Posts).WithOne(p => p.Category).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.CreationDate).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Post>().Property(p=>p.CreationTime).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Comment>().Property(c=>c.CreationTime).HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue("Publisher");
        }
    }
}
