using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Entities;
using AspNetCoreMvc2.Introduction.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc2.Introduction.Controllers
{
    public class SessionDemoController : Controller
    {
        public string Index()
        {
			HttpContext.Session.SetInt32("age", 26);
			HttpContext.Session.SetString("name", "sevgi");
			HttpContext.Session.SetObject("student",new Student { Email = "sd@gmail.com", FirstName = "Sevgi", LastName = "Akten", Id = 1 });
			return "Session is created";
        }

		public string GetSessions()
		{
			return String.Format("Hello {0}, you are {1}. Student is {2}", HttpContext.Session.GetString("name"), HttpContext.Session.GetInt32("age"),HttpContext.Session.GetObject<Student>("student").FirstName);
		}
    }
}