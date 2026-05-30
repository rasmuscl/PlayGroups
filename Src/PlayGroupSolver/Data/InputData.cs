using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public static class InputData
	{
		public static int NumberOfChildren { get { return NumberOfBoys + NumberOfGirls; }}
		public static int NumberOfDays { get; set; }
		public static int NumberOfBoys { get; set; }
		public static int NumberOfGirls { get; set; }

		private static int[] _children;
		private static List<List<int>> _groupLayout;
		
		public static List<List<int>> GroupLayout
		{
			get
			{
				return _groupLayout;
			}
			set
			{
				int count = 0, idx = 0;
				for (int i = 0; i < value.Count; i++)
				{
					count += value[i].Count;
				}
				_children = new int[count];

				for (int i = 0; i < value.Count; i++)
				{
					for (int j = 0; j < value[i].Count; j++)
					{
						_children[idx++] = value[i][j];
					}
				}
				_groupLayout = value;
			}
		}

		public static List<string> Names { get; set; }
		
		public static List<string> Dates { get; set; }

		public static bool IsBoy(int idx)
		{
			return _children[idx] == 0;
		}

		public static bool IsGirl(int idx)
		{
			return _children[idx] == 1;
		}
	}
}
