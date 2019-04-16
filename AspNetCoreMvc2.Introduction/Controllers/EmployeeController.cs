using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Entities;
using AspNetCoreMvc2.Introduction.Models;
using AspNetCoreMvc2.Introduction.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreMvc2.Introduction.Controllers
{
	public class EmployeeController : Controller
	{
		private ICalculator _calculator;
		public EmployeeController(ICalculator calculator)
		{
			_calculator = calculator;
		}
		public IActionResult Add()
		{
			var employeeAddViewModel = new EmployeeAddViewModel
			{
				Employee = new Employee(),
				Cities = new List<SelectListItem>
				{
					new SelectListItem{Text="Ankara", Value="6"},
					new SelectListItem{Text="İstanbul", Value="34"},
				}
			};
			return View(employeeAddViewModel);
		}

		[HttpPost]
		public IActionResult Add(Employee employee)
		{

			return View();
		}

		public string Calculate()
		{
			return _calculator.Calculate(100).ToString();
		}

        public string Calculate2()
        {
            //for test bilgeadam branch
            return _calculator.Calculate(100).ToString();
        }

        public string Calculate3()
        {
            //for test bilgeadam branch
            return _calculator.Calculate(100).ToString();
        }
    }
}