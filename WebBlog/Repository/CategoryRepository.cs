using Microsoft.EntityFrameworkCore;
using WebBlog.Models.Context;
using WebBlog.Models.Entities;

namespace WebBlog.Repository
{
    public class CategoryRepository : IMainRepository<Category>
    {
        private BlogDBContext _dbContext;
        public CategoryRepository(BlogDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(Category entity)
        {
            _dbContext.Categories.Add(entity);
        }

        public void Delete(int id)
        {
            _dbContext.Remove(GetById(id));
        }

        public void DeleteAll()
        {
            _dbContext.Remove(GetAll());
        }

        public ICollection<Category> GetAll()
        {
           var categories = _dbContext.Categories.Include(c=>c.Posts).ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c=>c.Id == id);
            return category;
        }

        public bool Save()
        {
           return _dbContext.SaveChanges() > 0;
        }

        public void Update(Category entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
