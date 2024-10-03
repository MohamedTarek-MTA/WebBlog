using Microsoft.AspNetCore.Mvc;
using WebBlog.Models.Entities;
using WebBlog.Repository;

namespace WebBlog.Controllers.EntitiesController
{
    public class CategoryController : Controller
    {
        private IMainRepository<Category> _category;
        private IMainRepository<User> _user;
        private IMainRepository<Post> _post;
        public CategoryController(IMainRepository<Category> category, IMainRepository<User> user, IMainRepository<Post> post)
        {
            _category = category;
            _user = user;
            _post = post;
        }
        public IActionResult CreateCategory(Category category)
        {
            _category.Create(category);
            _category.Save();
            return RedirectToAction("GetAllCategoriesForAdmin", new {id = category.Id});
        }
        public IActionResult EditCategory(int id)
        {
            var oldCategory = _category.GetById(id);
            return View(oldCategory);
        }
        public IActionResult DeleteCategory(int id)
        {
            _category.Delete(id);
            _category.Save();
            return RedirectToAction("GetAllCategoriesForAdmin");
        }
        public IActionResult UpdateCategory(Category category)
        {
            
            _category.Update(category);
            _category.Save();
            return RedirectToAction("GetAllCategoriesForAdmin", new {id = category.Id});
        }
        public IActionResult GetAllCategories()
        {
            var categories = _category.GetAll();
            return View("GetAllCategories",categories);
        }
        public IActionResult GetAllCategoriesForAdmin()
        {
            var categories = _category.GetAll();
            return View("GetAllCategoriesForAdmin", categories);
        }
        public IActionResult NewCategory() 
        {
            var users = _user.GetAll();
            ViewBag.User = users;
            return View();
        }
        public IActionResult GetCategoryById(int id)
        {
            var category = _category.GetById(id);
            var posts = _post.GetAll();
            ViewBag.Posts = posts;
            return View(category);
        }
    }
}
