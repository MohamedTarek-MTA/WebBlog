using WebBlog.Models.Context;
using WebBlog.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;
namespace WebBlog.Repository
{
    public class UserRepository : IMainRepository<User>
    {
        private BlogDBContext _dbContext;

        public UserRepository(BlogDBContext dBContext) 
        {
            _dbContext = dBContext;
        }
        public void Create(User entity)
        {
            _dbContext.Users.Add(entity);
        }

        public void Delete(int id)
        {
            _dbContext.Remove(GetById(id));
        }

        public void DeleteAll()
        {
            _dbContext.Remove(GetAll());
        }

        public ICollection<User> GetAll()
        {
            var users = _dbContext.Users.ToList();
            return users;
        }

        public User GetById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u=>u.Id == id);
            return user;
        }
        public User GetUserPostById(int id)
        {
            var userPost = _dbContext.Users.Include(p=>p.Posts).FirstOrDefault(u => u.Id == id);
            return userPost;
        }
        public User GetUserCommentById(int id)
        {
            var userComment = _dbContext.Users.Include(c => c.Comments).FirstOrDefault(u => u.Id == id);
            return userComment;
        }
        public bool Save()
        {
           return _dbContext.SaveChanges() > 0;
        }

        public void Update(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public bool CheckEmail(string email)
        {
            var userEmail = _dbContext.Users.FirstOrDefault(u => u.Email == email);
            if (userEmail != null)
            {
                return true;
            }
            else return false;
        }
        public bool CheckPassword(string password)
        {
            var hashedPassword = Helper.HashPassword(password);
           var existPassword =  _dbContext.Users.FirstOrDefault(u => u.Password == hashedPassword);
            if (existPassword != null)
            {
                return true;
            }
            return false;
        }
        public string GetRole(int id)
        {
            var user = GetById(id);
            return user.Role;
        }
        public int GetUserIdByEmail(string Email) 
        {
            var userId = _dbContext.Users.FirstOrDefault(u => u.Email == Email);
            return userId.Id;
        }
        
    }
}
