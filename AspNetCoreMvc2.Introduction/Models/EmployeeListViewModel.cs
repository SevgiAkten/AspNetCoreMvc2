using System.Collections.Generic;
using AspNetCoreMvc2.Introduction.Entities;

namespace AspNetCoreMvc2.Introduction.Models
{
	public class EmployeeListViewModel
	{
		public List<Employee> Employees { get; set; }
		public List<string> Cities { get; set; }
	}
}