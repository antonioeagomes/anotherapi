using Elmah.Io.AspNetCore;
using Elmah.Io.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Api.Configuration
{
    public static class LoggerConfig
    {
        public static IServiceCollection AddElmahConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ElmahIoOptions>(configuration.GetSection("ElmahIo"));
            services.AddElmahIo();

            services.AddLogging(builder =>
            {
                builder.AddElmahIo();
                builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            });
            
            return services;
        }

        public static IApplicationBuilder UseElmahConfig(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app; 
        }
    }
}
