using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChooseExercise()
        {
            List<ExerciseVM> exercises = await _homeClient.GetAllExercises();
            return PartialView(exercises);
        }

        [HttpGet]
        public IActionResult CounterUnilateralExercises()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult CounterBilateralExercises()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task SaveRepsUnilateral(UnilateralRepsVM convertedData)
        {
            var user = await _accountClient.GetCurrentClient();

            UnilateralExercisesHistoryVM unilateralExercisesHistory = new UnilateralExercisesHistoryVM
            {
                 Reps = convertedData.Reps,
                 ExerciseId = convertedData.ExerciseId,
                 UserId = user.UserId
            };

            await _homeClient.SaveRepsUnilateral(unilateralExercisesHistory);
        }

        [HttpPost]
        public async Task SaveRepsBilateral(BilateralRepsVM convertedData)
        {
            var user = await _accountClient.GetCurrentClient();

            BilateralExercisesHistoryVM bilateralExercisesHistory = new BilateralExercisesHistoryVM
            {
                LeftSideReps = convertedData.LeftSideReps,
                RightSideReps = convertedData.RightSideReps,
                ExerciseId = convertedData.ExerciseId,
                UserId = user.UserId
            };

            await _homeClient.SaveRepsBilateral(bilateralExercisesHistory);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
