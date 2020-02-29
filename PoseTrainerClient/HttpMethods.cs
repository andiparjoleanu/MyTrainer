using Newtonsoft.Json;
using PoseTrainerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoseTrainerClient
{
    public static class HttpMethods<T>
    {
        public static async Task<T> GetAsync(HttpClient client, string path)
        {
            var response = await client.GetAsync(client.BaseAddress + path);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(stringContent);
            return result;
        }

        public static async Task<ResultVM> PostAsync(HttpClient client, T routeValue, string path)
        {
            StringContent userCredentials = new StringContent(JsonConvert.SerializeObject(routeValue), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress + path, userCredentials);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultVM>(stringContent);
            return result;
        }

        public static async Task<ResultVM> PutAsync(HttpClient client, T routeValue, string path)
        {
            StringContent userCredentials = new StringContent(JsonConvert.SerializeObject(routeValue), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(client.BaseAddress + path, userCredentials);
            var content = response.Content;
            var stringContent = await content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultVM>(stringContent);
            return result;
        }
    }
}
