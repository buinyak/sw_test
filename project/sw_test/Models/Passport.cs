using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sw_test.Models
{
	public class Passport
	{
		public int Id { get; set; }
		public int EmployeeId { get; set; }
		public string Type { get; set; }
		public string Number { get; set; }
	}
}
