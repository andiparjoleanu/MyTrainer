using Microsoft.Extensions.DependencyInjection;
using PoseTrainerClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoseTrainerClient
{
    public static class ServicesExtentionMethods
    {
        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<HomeClient>();
            services.AddHttpClient<AccountClient>();
        }
    }
}
