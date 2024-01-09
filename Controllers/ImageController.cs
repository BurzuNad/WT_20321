using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VT_20321.Models;

namespace VT_20321.Controllers {
    public class ImageController : Controller {
        private readonly UserManager<ApplicationUser> _userManager;
        IWebHostEnvironment _env;
        public ImageController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env) {
            _userManager=userManager;
            _env = env;
        }
        public async Task <IActionResult> GetImage() {
            /*var id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);*/
            var user = await _userManager.GetUserAsync(User);
            if (user.Avatar != null) {
                return File(user.Avatar, "image/*");
            }
            else {
                var avatarPath = "/images/Avatar.jpg";
                return File(_env.WebRootFileProvider
                .GetFileInfo(avatarPath)
               .CreateReadStream(), "image/*");
                /*var defaultAvatar = System.IO.File.ReadAllBytes("D:/ПОИС/Веб - технология/VT_20321/VT_20321/wwwroot/images/Avatar.jpg");
                return File(defaultAvatar, "image/*");*/
            }           
        }
    }
}
