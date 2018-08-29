using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using MlNetCore.Models.Views;

namespace MlNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public SignInManager<User> SignInManager
        {
            get { return this._signInManager; }
        }

        public UserManager<User> UserManager
        {
            get { return this._userManager; }
        }

        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost("Register")]
       public async Task<IActionResult> Register(UserViewModel userViewModel)
       {
           if(ModelState.IsValid)
           {
               var user = new User { UserName = userViewModel.UserName,
               Email = userViewModel.Email};
               var result = await UserManager.CreateAsync(user, userViewModel.Password);
               if(result.Succeeded)
               {
                   await SignInManager.SignInAsync(user, true);
                    return RedirectToAction("Users", "Account");
                }
                else foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(userViewModel);
       }

        [AllowAnonymous]
        [HttpGet("Login")]
        public async Task<IActionResult> Login()
        {
            //WHY?
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            var viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, true, false);
                if (result.Succeeded) return RedirectToAction("Users", "UserManagement");
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(loginViewModel);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            //WHY?
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "HelloWorld");
        }

        
    }
}
