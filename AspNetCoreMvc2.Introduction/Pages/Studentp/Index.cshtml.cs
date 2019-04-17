using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMvc2.Introduction.Entities;
using AspNetCoreMvc2.Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCoreMvc2.Introduction.Pages.Studentp
{
    public class IndexModel : PageModel
    {
		private readonly SchoolContext _context;

		public IndexModel(SchoolContext context)
		{
			_context = context;
		}

		public List<Student> Students{ get; set; }

		public void OnGet(string search)
        {
			Students = string.IsNullOrEmpty(search)
				? _context.Students.ToList()
				: _context.Students.Where(s => s.FirstName.ToLower().Contains(search)).ToList();
        }

		[BindProperty]
		public Student Student { get; set; }
		public IActionResult OnPost()
		{
			_context.Students.Add(Student);
			_context.SaveChanges();

			return RedirectToPage("/Studentp/Index");
		}
    }
}