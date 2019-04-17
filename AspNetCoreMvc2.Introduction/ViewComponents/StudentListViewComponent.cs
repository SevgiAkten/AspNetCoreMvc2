using AspNetCoreMvc2.Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvc2.Introduction.ViewComponents
{

	public class StudentListViewComponent : ViewComponent
	{
		private SchoolContext _context;
		public StudentListViewComponent(SchoolContext context)
		{
			_context = context;
		}

		public ViewViewComponentResult Invoke()
		{
			return View(new StudentListViewModel { Students = _context.Students.ToList() });
		}
	}
}
