using Microsoft.AspNetCore.Mvc;
using InsaClub.Data.Enum;
using InsaClub.Interfaces;
using InsaClub.Models;
using InsaClub.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsaClub.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClubRepository _clubRepository;

        public EventController(IEventRepository eventRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor, IClubRepository clubRepository)
        {
            _eventRepository = eventRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
            _clubRepository = clubRepository;
        }


        [HttpGet("events/{category?}/{page?}/{pageSize?}")]
        public async Task<IActionResult> Index(int category = -1, int page = 1, int pageSize = 6)
        {
            if (page < 1 || pageSize < 1)
            {
                return NotFound();
            }

            // if category is -1 (All) dont filter else filter by selected category
            var events = category switch
            {
                -1 => await _eventRepository.GetSliceAsync((page - 1) * pageSize, pageSize),
                _ => await _eventRepository.GetEventsByCategoryAndSliceAsync((EventCategory)category, (page - 1) * pageSize, pageSize),
            };

            var count = category switch
            {
                -1 => await _eventRepository.GetCountAsync(),
                _ => await _eventRepository.GetCountByCategoryAsync((EventCategory)category),
            };

            var viewModel = new IndexEventViewModel
            {
                Events = events,
                Page = page,
                PageSize = pageSize,
                TotalEvents = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                Category = category,
            };

            return View(viewModel);
        }




        [HttpGet]
        [Route("event/create")]
        public async Task<IActionResult> Create()
        {
            //    var currentUser = _httpContextAccessor.HttpContext.User;
            // return Ok(currentUser.GetUserId());

            var Clubs = await _clubRepository.GetClubsByUserIdAsync(_httpContextAccessor.HttpContext.User.GetUserId());
            var ClubList = Clubs.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            });
            var Club = new CreateEventViewModel
            {
                Clubs = ClubList
            };
            return View(Club);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel eventVM)
        {
            // return Ok(eventVM);
            // return Ok(ModelState.IsValid);
            var result = await _photoService.AddPhotoAsync(eventVM.Image);

            var @event = new Event
            {
                Title = eventVM.Title,
                Description = eventVM.Description,
                Image = result.Url.ToString(),
                ClubId = eventVM.ClubId,
                EventCategory = eventVM.EventCategory,
            };
            _eventRepository.Add(@event);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel eventVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View(eventVM);
            }

            var userEvent = await _eventRepository.GetByIdAsyncNoTracking(id);
            // return Ok(userEvent);
            if (userEvent == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(eventVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(eventVM);
            }

            if (!string.IsNullOrEmpty(userEvent.Image))
            {
                _ = _photoService.DeletePhotoAsync(userEvent.Image);
            }

            var @event = new Event
            {
                Id = id,
                Title = eventVM.Title,
                Description = eventVM.Description,
                Image = photoResult.Url.ToString(),
                EventCategory = eventVM.EventCategory,
                ClubId = userEvent.ClubId

            };
            // return Ok(@event);

            _eventRepository.Update(@event);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("event/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _eventRepository.GetByIdAsync(id);
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (clubDetails == null) return View("NotFound");
            if (clubDetails.Club.UserId != userId) return View("Forbidden");
            return View(clubDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var eventDetails = await _eventRepository.GetByIdAsync(id);

            if (eventDetails == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(eventDetails.Image))
            {
                _ = _photoService.DeletePhotoAsync(eventDetails.Image);
            }

            _eventRepository.Delete(eventDetails);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("event/join/{id}")]
        public async Task<IActionResult> JoinEvent(int id)
        {
            // return Ok("mrgl");
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null) return NotFound();
            _eventRepository.JoinEvent(id, userId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("event/leave/{id}")]
        public async Task<IActionResult> LeaveEvent(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var @event = await _eventRepository.GetByIdAsync(id);
            if (@event == null) return NotFound();
            _eventRepository.LeaveEvent(id, userId);
            return RedirectToAction("Index");
        }
    }
}