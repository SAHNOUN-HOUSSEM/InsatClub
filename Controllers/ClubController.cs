using Microsoft.AspNetCore.Mvc;
using InsaClub.Data.Enum;
using InsaClub.Helpers;
using InsaClub.Interfaces;
using InsaClub.Models;
using InsaClub.ViewModels;

namespace InsaClub.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IUserRepository userRepository)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var clubs = category switch
            {
                -1 => await _clubRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _clubRepository.GetClubsByCategoryAndSliceAsync((ClubCategory)category, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _clubRepository.GetCountAsync(),
                _ => await _clubRepository.GetCountByCategoryAsync((ClubCategory)category),
            };

            var clubViewModel = new IndexClubViewModel
            {
                Clubs = clubs,
                Page = page,
                PageSize = pageSize,
                TotalClubs = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
            };

            return View(clubViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DetailClub(int id, string runningClub)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            string curUserId;
            try
            {
                curUserId = HttpContext.User.GetUserId();
            }
            catch (Exception)
            {
                curUserId = "";
            }
            ViewBag.isMember = false;
            ViewBag.isOwner = false;

            if (curUserId != "")
            {
                var isMember = await _userRepository.IsMemberOf(curUserId, id);
                ViewBag.isMember = isMember;
                ViewBag.isOwner = club.UserId == curUserId;
            }

            var members = await _userRepository.GetMembersOfClub(id);
            var detailClubViewModel = new DetailClubViewModel
            {
                Club = club,
                Members = members,
            };
            return club == null ? NotFound() : View(detailClubViewModel);
        }



        [HttpGet]
        public IActionResult Create()
        {
            var curUserId = HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { UserId = curUserId };
            return View(createClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);


                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    UserId = clubVM.UserId,
                    CreatedAt = DateTime.Now

                };
                _clubRepository.Add(club);
                var curUser = await _userRepository.GetUserById(clubVM.UserId);
                var memberClub = new MemberClub
                {
                    ClubId = club.Id,
                    UserId = clubVM.UserId,
                    Club = club,
                    User = curUser
                };
                await _clubRepository.AddMemberToClub(club.Id, curUser.Id);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub == null)
            {
                return View("Error");
            }

            if (clubVM.Image != null)
            {
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Photo upload failed");
                    return View(clubVM);
                }

                if (!string.IsNullOrEmpty(userClub.Image))
                {
                    _ = _photoService.DeletePhotoAsync(userClub.Image);
                }

                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = photoResult.Url.ToString(),
                    UserId = userClub.UserId,
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");

            }
            else
            {
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = userClub.Image,
                    UserId = userClub.UserId,
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");


        }





    }
}