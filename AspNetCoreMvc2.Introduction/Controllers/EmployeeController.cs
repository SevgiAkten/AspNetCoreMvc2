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
        ICalculator _calculator2;
		public EmployeeController(ICalculator calculator, ICalculator calculator2 )
		{
			_calculator = calculator;
            _calculator2 = calculator2;
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
            _calculator2.Calculate(1000);
			return _calculator.Calculate(100).ToString();
		}        
    }
}