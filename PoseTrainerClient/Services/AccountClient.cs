using PoseTrainerClient.Models;
using PoseTrainerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PoseTrainerClient.Services
{
    public class AccountClient
    {
        public HttpClient Client { get; set; }

        public AccountClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44399/api/account");

            Client = client;
        }

        public async Task<UserVM> GetCurrentClient()
        {
            try
            {
                UserVM user = await HttpMethods<UserVM>.GetAsync(Client, "/getUser");
                return user;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<ResultVM> Login(LoginVM loginVM)
        {
            try
            {
                ResultVM result = await HttpMethods<LoginVM>.PostAsync(Client, loginVM, "/login");
                return result;
            }
            catch (Exception e)
            {
                return new ResultVM
                {
                    Message = e.Message,
                    Status = Status.Error
                };
            }
        }

        public async Task<ResultVM> Register(RegisterVM registerVM)
        {
            try
            {
                ResultVM result = await HttpMethods<RegisterVM>.PostAsync(Client, registerVM, "/register");
                return result;
            }
            catch (Exception e)
            {
                return new ResultVM
                {
                    Message = e.Message,
                    Status = Status.Error
                };
            }
        }

        public async Task<ResultVM> Logout()
        {
            try
            {
                ResultVM result = await HttpMethods<ResultVM>.GetAsync(Client, "/logout");
                return result;
            }
            catch (Exception e)
            {
                return new ResultVM
                {
                    Message = e.Message,
                    Status = Status.Error
                };
            }
        }

    }
}
