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
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string action)
        {
            Console.WriteLine("************Register************");
            if (action == "UpdatePhoto")
            {
                return RedirectToAction("UpdatePhotoRegister", registerViewModel);
            }
            if (!ModelState.IsValid) return View(registerViewModel);
            Console.WriteLine("************Register Valids************");
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                Console.WriteLine("User is not null");
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            Console.WriteLine("Image is null");
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

        public async Task<IActionResult> UpdatePhotoRegister(RegisterViewModel registerViewModel)
        {

            Console.WriteLine("************Register 22************");
            Console.WriteLine("********EMAIL*************");
            Console.WriteLine(registerViewModel.EmailAddress);
            Console.WriteLine(registerViewModel.Image);
            Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("ModelState Error: " + error.ErrorMessage);
            }
            if (registerViewModel.Image != null) // only update profile image
            {
                Console.WriteLine("Image is not null");
                var photoResult = await _photoService.AddPhotoAsync(registerViewModel.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View(registerViewModel);
                }


                registerViewModel.ProfileImageUrl = photoResult.Url.ToString();
            }
            return View("Register", registerViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Event");
        }






    }
}