using PoseTrainerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoseTrainerClient.Services
{
    public class HomeClient
    {
        public HttpClient Client { get; set; }

        public HomeClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44399/api/home");

            Client = client;
        }

        public async Task<List<ExerciseVM>> GetAllExercises()
        {
            try
            {
                List<ExerciseVM> exercises = await HttpMethods<List<ExerciseVM>>.GetAsync(Client, "/getAllExercises");
                return exercises;
            }
            catch(Exception)
            {
                return new List<ExerciseVM>();
            }
        }

        public async Task<ResultVM> SaveRepsUnilateral(UnilateralExercisesHistoryVM reps)
        {
            try
            {
                ResultVM resultVM = await HttpMethods<UnilateralExercisesHistoryVM>.PostAsync(Client, reps, "/SaveRepsUnilateral");
                return resultVM;
            }
            catch (Exception ex)
            {
                return new ResultVM { 
                    Message = ex.Message,
                    Status = Status.Error
                };
            }
        }

        public async Task<ResultVM> SaveRepsBilateral(BilateralExercisesHistoryVM reps)
        {
            try
            {
                ResultVM resultVM = await HttpMethods<BilateralExercisesHistoryVM>.PostAsync(Client, reps, "/SaveRepsBilateral");
                return resultVM;
            }
            catch (Exception ex)
            {
                return new ResultVM
                {
                    Message = ex.Message,
                    Status = Status.Error
                };
            }
        }
    }

}
