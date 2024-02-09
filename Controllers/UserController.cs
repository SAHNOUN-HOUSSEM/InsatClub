using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InsaClub.ViewModels;
using InsaClub.Interfaces;
using InsaClub.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InsaClub.Controllers
{
    public class EditProfileVM
    {
        public EditProfileViewModel EditProfileViewModel { get; set; }
    }
    public class PasswordUpdateVM
    {
        public PasswordUpdateViewModel PasswordUpdateViewModel { get; set; }
    }
    public class UpdatePhotoUpdateVM
    {
        public UpdatePhotoUpdateModel UpdatePhotoUpdateModel { get; set; }
    }
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoService _photoService;

        public UserController(IUserRepository userRepository, UserManager<User> userManager, IPhotoService photoService, IEventRepository eventRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _photoService = photoService;
            _eventRepository = eventRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar.jpg",
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }
            // return Ok(user);
            var clubs = user.Clubs;
            var clubsIn = await _userRepository.GetClubsOfUser(user.Id);
            var eventsIds = user.EventsIn;
            var events = new List<Event>();
            foreach(var eventIn in eventsIds)
            {
                var evt = await _eventRepository.GetEventById(eventIn.EventId);
                events.Add(evt);
            }
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                StudyLevel = user.StudyLevel,
                Bio = user.Bio,
                ProfileImageUrl = user.ProfileImageUrl ?? "/img/avatar.jpg",
                Clubs = clubs,
                Events = events,
                ClubsIn = clubsIn,
            };


            return View(userDetailViewModel);
        }

        private ProfileViewModel createProfileViewModel(User user, string activeTab = "profile")
        {
            return new ProfileViewModel()
            {
                EditProfileViewModel = new EditProfileViewModel()
                {
                    EmailAddress = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    StudyLevel = user.StudyLevel,
                    Bio = user.Bio,
                },
                UpdatePhotoUpdateModel = new UpdatePhotoUpdateModel()
                {
                    ProfileImageUrl = user.ProfileImageUrl,
                },
                PasswordUpdateViewModel = new PasswordUpdateViewModel()
                {
                    OldPassword = "",
                    NewPassword = "",
                    ConfirmNewPassword = ""
                },
                ActiveTab = activeTab
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }
            var userPopulated = await _userRepository.GetUserById(user.Id);



            var editVM = createProfileViewModel(userPopulated);
            return View(editVM);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel input)
        {
            var editVM = input.EditProfileViewModel;

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }
            var userPopulated = await _userRepository.GetUserById(user.Id);

            //test if there are errors in the model named "EditProfileViewModel"
            var cond = ModelState.Keys.FirstOrDefault(k => k.Contains("EditProfileViewModel") && ModelState[k].Errors.Count > 0);

            if (cond != null)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                //console all errors

                return View("EditProfile", createProfileViewModel(userPopulated));
            }




            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.StudyLevel = editVM.StudyLevel;
            user.Bio = editVM.Bio;


            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(PasswordUpdateVM input)
        {
            Console.WriteLine("***************UpdatePassword******************");
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }
            var userPopulated = await _userRepository.GetUserById(user.Id);
            var passwordUpdateViewModel = input.PasswordUpdateViewModel;
            var cond = ModelState.Keys.FirstOrDefault(k => k.Contains("PasswordUpdateViewModel") && ModelState[k].Errors.Count > 0);

            if (cond != null)
            {
                ModelState.AddModelError("", "Failed to update password");
                //log all errors
                foreach (var key in ModelState.Keys)
                {
                    Console.WriteLine(key);
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return View("EditProfile", createProfileViewModel(userPopulated, "security"));
            }



            var result = await _userManager.ChangePasswordAsync(user, passwordUpdateViewModel.OldPassword, passwordUpdateViewModel.NewPassword);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("PasswordUpdateViewModel.OldPassword", "Old password is incorrect");
                return View("EditProfile", createProfileViewModel(userPopulated, "security"));
            }

            return RedirectToAction("Detail", "User", new { user.Id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdatePhoto(UpdatePhotoUpdateVM input)
        {
            Console.WriteLine("***************UpdatePhoto******************");
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("Error");
            }
            var userPopulated = await _userRepository.GetUserById(user.Id);


            var updatePhotoUpdateModel = input.UpdatePhotoUpdateModel;
            var cond = ModelState.Keys.FirstOrDefault(k => k.Contains("UpdatePhotoUpdateModel") && ModelState[k].Errors.Count > 0);

            if (cond != null)
            {

                ModelState.AddModelError("", "Failed to update photo");
                return View("EditProfile", createProfileViewModel(userPopulated, "image"));
            }


            if (updatePhotoUpdateModel.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(updatePhotoUpdateModel.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", updatePhotoUpdateModel);
                }

                if (!string.IsNullOrEmpty(user.ProfileImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }

                user.ProfileImageUrl = photoResult.Url.ToString();
                updatePhotoUpdateModel.ProfileImageUrl = user.ProfileImageUrl;

                await _userManager.UpdateAsync(user);

            }
            return View("EditProfile", createProfileViewModel(userPopulated, "image"));



        }
    }

}