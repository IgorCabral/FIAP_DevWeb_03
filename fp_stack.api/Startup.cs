using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using fp_stack.core;
using fp_stack.core.Services;
using fp_stack.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace fp_stack.api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=StackDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));

            services.AddMvc(
                options =>
                    {
                        options.RespectBrowserAcceptHeader = true;
                        options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                    }
                );

            services.AddCors(o => o.AddPolicy("Default", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();            

            app.UseCors("Default");
            app.UseMvcWithDefaultRoute();                        
        }
    }
}
