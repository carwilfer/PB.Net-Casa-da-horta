using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CasaDaHora.Domain.Post.IPostRepository;
using CasaDaHorta.CrossCutting.Storage;
using CasaDaHorta.Repository.AccountRepository;
using CasaDaHorta.Repository.Amigo;
using CasaDaHorta.Services;
using CasaDaHorta.Services.AmigoServices;
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
using Microsoft.IdentityModel.Tokens;

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
            services.AddTransient<AmigoServices>();
            services.AddTransient<AuthenticateService>();
            services.AddTransient<AmigoRepository>();

            services.AddControllers();

            var key = Encoding.UTF8.GetBytes(this.Configuration["Token:Secret"]);
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = "Bearer";
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters.ValidIssuer = "AmigoDomain-API";
                o.TokenValidationParameters.ValidAudience = "AmigoDomain-API";
                o.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(key);
            });

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
