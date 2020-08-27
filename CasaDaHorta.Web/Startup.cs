using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CasaDaHora.Domain.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using CasaDaHora.Domain.Account.Repository;
using CasaDaHorta.Repository.AccountRepository;
using CasaDaHorta.Services.Account;
using CasaDaHorta.Repository.Context;

namespace CasaDaHorta.Web
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
            services.AddTransient<IAccountRepository, AccountRepository>();
            
            //Controla a parte de conta e perfil
            services.AddTransient<IUserStore<Accounty>, AccountRepository>();
            services.AddTransient<IRoleStore<Role>, RoleRepository>();

            //Gestor de login com sucesso ou não
            services.AddTransient<IAccountIdentityManager, AccountIdentityManager>();
            //Gerenciador de conta (cria edita ou deleta)
            services.AddTransient<IAccountService, AccountService>();

            services.AddDbContext<CasaDaHortaContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("CasaDaHortaConnection"));
            });

            //controlador de conta e perfil
            services.AddIdentity<Accounty, Role>()
                    .AddDefaultTokenProviders();

            //para conseguir criar um login e acesso
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddControllersWithViews();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
