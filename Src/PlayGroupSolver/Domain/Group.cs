using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class Group
	{	
		public List<Child> Children { get; set; }

		public Group()
		{
			Children = new List<Child>();
		}

		public void AddChild(Child child)
		{
			Children.Add(child);
			Children.Sort();
		}
	}
}
