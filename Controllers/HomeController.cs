using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InsaClub.Data;
using InsaClub.Interfaces;
using InsaClub.Models;
using InsaClub.ViewModels;


namespace InsaClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public HomeController( UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel();
            try
            {
               
                return View(homeViewModel);
            }
            catch (Exception)
            {
                homeViewModel.Clubs = null;
            }

            return View(homeViewModel);
        }

        public IActionResult Register() 
        {
            var response = new HomeUserCreateViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeVM)
        {
            var createVM = homeVM.Register;

            if (!ModelState.IsValid) return View(homeVM);

            var user = await _userManager.FindByEmailAsync(createVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("Register.Email", "This email address is already in use");
                return View(homeVM);
            }

            var newUser = new User
            {
                UserName = createVM.UserName,
                Email = createVM.Email,
             
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, createVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }
            return RedirectToAction("Index", "Club");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TeventIdentifier });
        // }
    }
}