using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDaHora.Domain.Post.IPostRepository;
using CasaDaHorta.CrossCutting.Storage;
using CasaDaHorta.Repository.AccountRepository;
using CasaDaHorta.Services.PostsServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CasaDaHorta.API
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
            services.AddDbContext<CasaDaHorta.Repository.Context.CasaDaHortaContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("CasaDaHortaDb")));
            services.AddTransient<AzureStorage>();
            //este aqui busca a conection string do blob store do Azure
            services.Configure<AzureStorageOptions>(Configuration.GetSection("Microsift.Storage"));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostServices, PostServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
