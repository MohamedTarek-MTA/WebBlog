using Microsoft.EntityFrameworkCore;
using WebBlog.Models.Context;
using WebBlog.Models.Entities;

namespace WebBlog.Repository
{
    public class CommentRepository : IMainRepository<Comment>
    {
        private BlogDBContext _dbContext;
        public CommentRepository(BlogDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Comment entity)
        {
            _dbContext.Comments.Add(entity);
        }

        public void Delete(int id)
        {
            _dbContext.Remove(GetById(id));
        }

        public void DeleteAll()
        {
            _dbContext.Remove(GetAll());
        }

        public ICollection<Comment> GetAll()
        {
            return _dbContext.Comments.ToList();
        }

        public Comment GetById(int id)
        {
            return _dbContext.Comments.FirstOrDefault(c => c.Id == id);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Update(Comment entity)
        {
           _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
