using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PoseTrainerClient.Models;
using PoseTrainerClient.Services;
using PoseTrainerClient.ViewModels;

namespace PoseTrainerClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeClient _homeClient;
        private readonly AccountClient _accountClient;

        public HomeController(ILogger<HomeController> logger, 
            HomeClient homeClient,
            AccountClient accountClient)
        {
            _logger = logger;
            _homeClient = homeClient;
            _accountClient = accountClient;
        }

        public async Task<IActionResult> Index()
        {
            List<ExerciseVM> exercises = await _homeClient.GetAllExercises();
            return View(exercises);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
