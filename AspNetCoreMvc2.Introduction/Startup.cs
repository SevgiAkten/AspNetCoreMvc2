using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Models;
using AspNetCoreMvc2.Introduction.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreMvc2.Introduction
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
            var connection = @"Server=(localdb)\mssqllocaldb;Database=SchoolDb;Trusted_Connection=true";
            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connection));
			services.AddTransient <ICalculator, Calculator8>();
            //çok sıklıkla çağrılan sınıflar singleton olabilir add delete gibi. sadece 1 nesne örneği oluşturur bellekten silmez 
            //addscoped her kullanıcı için bir nesne oluşturu
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseStaticFiles();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
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
			routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index2}/{id?}");
			routeBuilder.MapRoute("MyRoute", "Sevgi/{controller=Home}/{action=Index3}/{id?}");
		}
	}
}
