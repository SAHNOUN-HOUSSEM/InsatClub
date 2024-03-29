﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InsaClub.Interfaces;
using InsaClub.Models;
using InsaClub.ViewModels;

namespace InsaClub.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRespository;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRespository, IPhotoService photoService)
        {
            _dashboardRespository = dashboardRespository;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            // var userEvents = await _dashboardRespository.GetAllUserEvents();
            var userEvents = new List<Event>();
            var userClubs = await _dashboardRespository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Events = userEvents,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
    }
}