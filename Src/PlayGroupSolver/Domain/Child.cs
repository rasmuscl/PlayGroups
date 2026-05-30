using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class Child : IComparable<Child>
	{
		public bool IsHosting { get; set; }
		public int ChildNo { get; set; }

		public Child( int childNo, bool isHosting )
		{
			ChildNo = childNo;
			IsHosting = isHosting;
		}

		#region IComparable<Child> Members

		public int CompareTo(Child other)
		{
			if (other.IsHosting)
				return 1;
			else if (IsHosting)
				return -1;
			else
				return 0;
		}

		#endregion
	}
}
