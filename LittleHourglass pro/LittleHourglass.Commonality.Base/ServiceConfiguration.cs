using LittleHourglass.Authorization.JWT;
using LittleHourglass.Commonality.Base.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Commonality.Base
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection Configuration(this IServiceCollection services)
        {         
           
            return services;
        }

        public static IApplicationBuilder ApplicationBuilder(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
