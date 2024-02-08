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

        public EventController(IEventRepository eventRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor,IClubRepository clubRepository)
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
           var currentUser = _httpContextAccessor.HttpContext.User;
            // return Ok(currentUser.GetUserId());
           
           var Clubs = await _clubRepository.GetClubsByUserIdAsync(currentUser.GetUserId());
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
        [HttpGet]
        [Route("event/{runningEvent}/{id}")]
        public async Task<IActionResult> DetailEvent(int id, string runningEvent)
        {
            var selectedEvent = await _eventRepository.GetByIdAsync(id);
            if (selectedEvent == null) return NotFound();
            var events = new EventDetailsViewModel
            {
                Id = selectedEvent.Id,
                Title = selectedEvent.Title,
                Description = selectedEvent.Description,
                Image = selectedEvent.Image,
                StartTime = selectedEvent.StartTime,
                EntryFee = selectedEvent.EntryFee,
                Website = selectedEvent.Website,
                Facebook = selectedEvent.Facebook,
                Contact = selectedEvent.Contact,
                EventCategory = selectedEvent.EventCategory,
                ClubId = selectedEvent.ClubId,
                Club = selectedEvent.Club,
                isAdmin = selectedEvent.Club.UserId == _httpContextAccessor.HttpContext.User.GetUserId()
            };
            return View(events);
            // return selectedEvent == null ? NotFound() : View(events);
        }

        [HttpGet]
        [Route("event/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            var userId = _httpContextAccessor.HttpContext.User.GetUserId();
            if (@event == null) return View("NotFound");
            if (@event.Club.UserId != userId) return View("Forbidden");
            
            var eventVM = new EditEventViewModel
            {
                Title = @event.Title,
                Description = @event.Description,
                URL = @event.Image,
                EventCategory = @event.EventCategory
            };
            return View(eventVM);
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
          
            };

            _eventRepository.Update(@event);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("event/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var clubDetails = await _eventRepository.GetByIdAsync(id);
            if (clubDetails == null) return View("Error");
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
    }
}