using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc2.Introduction.Controllers
{
    public class FilterController : Controller
    {
		[CustomFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}