using Microsoft.AspNetCore.Mvc;
using PoseTrainerClient.Services;
using PoseTrainerClient.ViewModels;
using System.Threading.Tasks;

namespace PoseTrainerClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountClient _accountClient;
        public AccountController(AccountClient accountClient)
        {
            _accountClient = accountClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginVM loginVM)
        {
            var result = await _accountClient.Login(loginVM);
            if (result.Status == Status.Error)
            {
                TempData["TrySignInErrorMessage"] = result.Message;
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterVM registerVM)
        {
            var result = await _accountClient.Register(registerVM);

            if (result.Status == Status.Error)
            {
                TempData["RegisterErrorMessage"] = result.Message;
                return RedirectToAction("Register");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountClient.Logout();
            return RedirectToAction("Login");
        }
    }
}