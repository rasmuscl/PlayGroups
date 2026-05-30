using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class Day
	{
		public List<Group> Groups { get; set; }
		public DateTime Date { get; set; }
		
		public Day(string date)
		{
			Groups = new List<Group>();
			Date = DateTime.Parse(date);
		}
	}
}
