using Microsoft.AspNetCore.Mvc;
using WebBlog.Models.Context;
using WebBlog.Models.Entities;
using WebBlog.Repository;

namespace WebBlog.Controllers.EntitiesController
{
    public class UserController : Controller
    {
        private readonly IMainRepository<User> _userRepo;

        public UserController(IMainRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult GetUserById(int id)
        {
            var user = _userRepo.GetById(id);
            return View("GetUserById", user);
        }

        public IActionResult GetAllUsers()
        {
            var users = _userRepo.GetAll();
            return View("GetAllUsers", users);
        }
        public IActionResult EditUser(int id)
        {
            var oldUser = _userRepo.GetById(id);
            return View(oldUser);
        }
        public IActionResult UpdateUser(User user , IFormFile? profileImage)
        {

            if (profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName).ToLower();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    ModelState.AddModelError("ProfileImage", "Image must be .jpg or .png format!");
                    return RedirectToAction("EditUser");
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
            _userRepo.Update(user);
            _userRepo.Save();
            return RedirectToAction("GetUserById", new {id = user.Id});

        }
        public IActionResult DeleteUser(int id)
        {
            _userRepo.Delete(id);
            _userRepo.Save();
            return RedirectToAction("Index", "Home");
        }
    }
}
