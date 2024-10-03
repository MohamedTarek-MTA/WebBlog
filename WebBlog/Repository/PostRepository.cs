using Microsoft.EntityFrameworkCore;
using WebBlog.Models.Context;
using WebBlog.Models.Entities;

namespace WebBlog.Repository
{
    public class PostRepository : IMainRepository<Post>
    {
        private BlogDBContext _dbContext;
        public PostRepository(BlogDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Post entity)
        {
            _dbContext.Posts.Add(entity);
        }

        public void Delete(int id)
        {
           _dbContext.Remove(GetById(id));
        }

        public void DeleteAll()
        {
            _dbContext.Remove(GetAll());
        }

        public ICollection<Post> GetAll()
        {
            var posts = _dbContext.Posts.Include(c=>c.Category).Include(u => u.User).OrderByDescending(p=>p.CreationTime).ToList();
            return posts;
        }

        public Post GetById(int id)
        {
            var post = _dbContext.Posts.Include(c => c.Category).Include(u=>u.User).FirstOrDefault(p=>p.Id == id);
            return post;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Update(Post entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
