using Microsoft.AspNetCore.Mvc;
using WebBlog.Models.Entities;
using WebBlog.Repository;

namespace WebBlog.Controllers.EntitiesController
{
    public class PostController : Controller
    {
        private IMainRepository<Post> _post;
        private IMainRepository<Category> _category;
        private IMainRepository<User> _user;
        public PostController(IMainRepository<Post> post, IMainRepository<Category> category, IMainRepository<User> user)
        {
            _post = post;
            _category = category;
            _user = user;

        }
        public IActionResult GetAllPostsForAdmin()
        {
            var posts = _post.GetAll();
            return View(posts);
        }
        public IActionResult GetAllPosts()
        {
            var posts = _post.GetAll();
            return View(posts);
        }
        public IActionResult CreatePost(Post post , IFormFile? postImage) 
        {
            if (postImage != null && postImage.Length > 0)
            {
                var extension = Path.GetExtension(postImage.FileName).ToLower();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    ModelState.AddModelError("PostImage", "Image Must Be .jpg or .png!");
                    return RedirectToAction("EditPost");
                }
                using (var memoryStream = new MemoryStream())
                {
                    postImage.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    if (Helper.IsJpeg(imageBytes) || Helper.IsPng(imageBytes))
                    {
                        post.PostImage = imageBytes;
                    }
                }
            }
            _post.Create(post);
            _post.Save();
            return RedirectToAction("GetAllPosts");
        }
        public IActionResult GetPostById(int id)
        {
            var post = _post.GetById(id);
            return View(post);
        }
        public IActionResult DeletePost(int id)
        {
            _post.Delete(id);
            _post.Save();
            return RedirectToAction("GetAllPostsForAdmin");
        }
        public IActionResult NewPost()
        {
            var users = _user.GetAll();
            var categories = _category.GetAll();
            ViewBag.Users = users;
            ViewBag.Categories = categories;
            return View();
        }
        public IActionResult EditPost(int id)
        {
            var oldpost = _post.GetById(id);
            var users = _user.GetAll();
            var categories = _category.GetAll();
            ViewBag.Users = users;
            ViewBag.Categories = categories;
            return View(oldpost);
        }
        public IActionResult UpdatePost(Post post, IFormFile? postImage)
        {
            if (postImage != null && postImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    postImage.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    if (Helper.IsJpeg(imageBytes))
                    {
                        post.PostImage = imageBytes;
                    }
                    else if (Helper.IsPng(imageBytes))
                    {
                        post.PostImage = imageBytes;
                    }
                    else
                    {
                        ModelState.AddModelError("ProfileImage", "Image Must Be .jpg or .png!");
                        return RedirectToAction("EditPost");
                    }
                }
            }
            _post.Update(post);
            _post.Save();
            return RedirectToAction("GetAllPostsForAdmin", new { id = post.Id });
        }
        
    }
}