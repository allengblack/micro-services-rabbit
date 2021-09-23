using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MassTransit;
using Subscriber.NET.Services;

namespace Subscriber.NET
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddMassTransit(x =>
            // {
            //     x.AddConsumer<MessageConsumer>();
            //     x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            //     {
            //         cfg.Host(new Uri("rabbitmq://localhost"),h =>
            //         {
            //             h.Username("guest");
            //             h.Password("guest");
            //         });
            //         cfg.ReceiveEndpoint("messageQueue", ep =>
            //         {
            //             ep.PrefetchCount = 16;
            //             ep.UseMessageRetry(r => r.Interval(2, 100));
            //             ep.ConfigureConsumer<MessageConsumer>(provider);
            //         });
            //     }));
            // });

            // services.AddMassTransitHostedService();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Subscriber.NET", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Subscriber.NET v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}