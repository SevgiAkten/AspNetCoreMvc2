using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Identity;
using AspNetCoreMvc2.Introduction.Models;
using AspNetCoreMvc2.Introduction.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreMvc2.Introduction
{
	public class Startup
	{
		private IConfiguration _configuration;
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddDbContext<SchoolContext>(options => options.UseSqlServer(_configuration["dbConnection"]));
			services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_configuration["dbConnection"]));
			services.AddIdentity<AppIdentityUser, AppIdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;

				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.AllowedForNewUsers = true;

				options.User.RequireUniqueEmail = true;

				options.SignIn.RequireConfirmedEmail = true;
				options.SignIn.RequireConfirmedPhoneNumber = false;
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Security/Login";
				options.LogoutPath = "/Security/Logout";
				options.AccessDeniedPath = "/Security/AccessDenied";
				options.SlidingExpiration = true;
				options.Cookie = new CookieBuilder
				{
					HttpOnly = true,
					Name=".AspNetCoreDemo.Security.Cookie",
					Path="/",
					SameSite=SameSiteMode.Lax,
					SecurePolicy=CookieSecurePolicy.SameAsRequest
				};
			});

			services.AddTransient<ICalculator, Calculator8>();
			services.AddSession();
			services.AddDistributedMemoryCache();


			//çok sıklıkla çağrılan sınıflar singleton olabilir add delete gibi. sadece 1 nesne örneği oluşturur bellekten silmez 
			//addscoped her kullanıcı için bir nesne oluşturu
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseStaticFiles();
			env.EnvironmentName = EnvironmentName.Production;
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
			}

			app.UseSession();

			app.UseAuthentication();

			//app.UseMvc(routes =>
			//{
			//	routes.MapRoute(
			//		name: "default",
			//		template: "{controller=home}/{action=index}/{id?}"
			//		);
			//});

			//app.UseMvcWithDefaultRoute(); //yukardakiyle aynı

			app.UseMvc(ConfigureRoutes);
		}

		private void ConfigureRoutes(IRouteBuilder routeBuilder)
		{
			routeBuilder.MapRoute("Default", "{controller=Filter}/{action=Index}/{id?}");
			routeBuilder.MapRoute("MyRoute", "Sevgi/{controller=Home}/{action=Index3}/{id?}");

			routeBuilder.MapRoute(
		   name: "areas",
		   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		 );
		}
	}
}
