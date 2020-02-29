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
    }
}
