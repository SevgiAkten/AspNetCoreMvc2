using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMvc2.Introduction.Entities
{
	public class Employee
	{
		public int Id { get; set; }
		[Display(Name="First Name")]
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int CityId { get; set; }
	}
}
