using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InsaClub.Data;
using InsaClub.Interfaces;
using InsaClub.Models;
using InsaClub.ViewModels;

namespace InsaClub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IPhotoService photoService
           )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _photoService = photoService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Event");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            var newUser = new User()
            {

                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                StudyLevel = registerViewModel.StudyLevel,
                ProfileImageUrl = registerViewModel.ProfileImageUrl,
                EmailConfirmed = true,

            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return RedirectToAction("Index", "Event");
        }

        // public async Task<IActionResult> UpdatePhotoRegister(RegisterViewModel registerViewModel)
        // {

        //     if (registerViewModel.Image != null) // only update profile image
        //     {
        //         var photoResult = await _photoService.AddPhotoAsync(registerViewModel.Image);

        //         if (photoResult.Error != null)
        //         {
        //             ModelState.AddModelError("Image", "Failed to upload image");
        //             return View(registerViewModel);
        //         }


        //         registerViewModel.ProfileImageUrl = photoResult.Url.ToString();
        //     }
        //     return View("Register", registerViewModel);

        // }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Event");
        }






    }
}