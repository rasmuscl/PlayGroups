using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class Plan
	{
		public List<Day> Days { get; set; }

		public Plan()
		{
			Days = new List<Day>();
		}
	}
}
