using Microsoft.AspNetCore.Mvc;
using OpenApiService;

namespace SchoolManagementClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManagementClient _userManagementClient;

        public AccountController(UserManagementClient userManagementClient)
        {
            this._userManagementClient = userManagementClient;
        }
        public IActionResult Login(string? returnUrl = null)
        {
            return View();
        }

        [HttpPost("LoginAsync")]
        public async Task<IActionResult> Login(LoginRequest model, string? returnUrl = null)
        {
            if (model.Email != null && model.Password != null)
            {
                var result = await _userManagementClient.LoginAsync(false, false, model);
                if (result != null && result.AccessToken != null)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        public IActionResult StudentRegister()
        {
            return View();
        }

        public IActionResult LecturerRegister()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
