using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MiddlewareKavramı
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewareKavramı", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareKavramı v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.Use Kullanımı
            app.Use(async (context, next) =>
            {
                Console.WriteLine("Middleware 1 başladı.");

                await next.Invoke();

                Console.WriteLine("Middleware 1 sonlandırılıyor.");
            });
            app.Use(async (context, next) =>
               {
                   Console.WriteLine("Middleware 2 başladı.");

                   await next.Invoke();

                   Console.WriteLine("Middleware 2 sonlandırılıyor.");
               });
            app.Use(async (context, next) =>
              {
                  Console.WriteLine("Middleware 3 başladı.");

                  await next.Invoke();

                  Console.WriteLine("Middleware 3 sonlandırılıyor.");
              });

            app.UseHello();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Use middleware tetiklendi.");
                await context.Response.WriteAsync("Use middleware tetiklendi.");
                await next.Invoke();
            });

            //app.Map kullanımı 
            app.Map("/example", internalApp =>
                internalApp.Run(async context =>
                    {
                        Console.WriteLine("/example middleware tetiklendi.");
                        await context.Response.WriteAsync("/example middleware tetiklendi.");
                    }));

            //app.MapWhen kullanımı 
            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
              {
                  internalApp.Run(async context =>
                  {
                      Console.WriteLine("MapWhen Middleware Tetiklendi");
                      await context.Response.WriteAsync("MapWhen Middleware Tetiklendi");
                  });
              });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
