using Microsoft.AspNetCore.Mvc;
using WebBlog.Models.Entities;
using WebBlog.Repository;

namespace WebBlog.Controllers.EntitiesController
{
    public class AuthController : Controller
    {
        private UserRepository _user;
        private IMainRepository<User> _userRepo;
        public AuthController(UserRepository user , IMainRepository<User> userRepo)
        {
            _user = user;
            _userRepo = userRepo;
        }
        public IActionResult Login(string Email, string Password)
        {
            if (_user.CheckEmail(Email) && _user.CheckPassword(Password))
            {
                var userId = _user.GetUserIdByEmail(Email);
                if (_user.GetRole(userId).ToLower() == "admin")
                {
                    return RedirectToAction("AdminProfile", new {id = userId});
                }
                else
                {
                    return RedirectToAction("Profile", new {id = userId });
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Wrong Email or Password!");
                return LoginForm();
            }
        }
        public IActionResult Register(User user , IFormFile? profileImage)
        {
            if(_user.CheckEmail(user.Email))
            {
                ModelState.AddModelError("Email", "This Email already Exist!");
                return RegisterForm();
            }
            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName).ToLower();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    ModelState.AddModelError("ProfileImage", "Image must be .jpg or .png format!");
                    return RegisterForm();
                }
                using (var memoryStream = new MemoryStream())
                {
                    profileImage.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    if (Helper.IsJpeg(imageBytes) || Helper.IsPng(imageBytes))
                    {
                        user.ProfileImage = imageBytes;
                    }
                }
            }
            user.Password = Helper.HashPassword(user.Password);
            _userRepo.Create(user);
            _userRepo.Save();
            return RedirectToAction("Profile", new {id = user.Id});
        }
        public IActionResult LoginForm()
        {
            return View("Login");
        }
        public IActionResult RegisterForm() 
        {
            return View("SignIn");
        }
        public IActionResult Profile(int id)
        {
            var user = _user.GetById(id);
            return View(user);
        }
        public IActionResult AdminProfile(int id)
        {
            var user = _user.GetById(id);
            return View(user);
        }
    }
}
