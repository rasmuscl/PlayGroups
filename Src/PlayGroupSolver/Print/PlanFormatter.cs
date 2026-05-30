using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlayGroupSolver
{
	public class PlanFormatter
	{
		protected Solution _sol;

		public PlanFormatter( Solution sol )
		{
			_sol = sol;
		}

		public PlanFormatter()
		{
			_sol = null;
		}

		/// <summary>
		/// Format the internal solution as a string
		/// </summary>
		/// <returns></returns>
		public virtual string FormatSolution()
		{
 			return FormatSolution( _sol );
		}

		/// <summary>
		/// Format the solution as a string
		/// </summary>
		/// <returns></returns>
		public virtual string FormatSolution( Solution sol )
		{
			return "";
		}

		/// <summary>
		/// Print plan to file
		/// </summary>
		/// <param name="filename"></param>
		public virtual void PrintPlan( string filename )
		{
			using (StreamWriter s = new StreamWriter(filename))
			{
				s.Write(FormatSolution());
			}
		}

		/// <summary>
		/// Print plan to file
		/// </summary>
		/// <param name="filename"></param>
		public virtual void PrintPlan(string filename, Solution sol)
		{
			using (StreamWriter s = new StreamWriter(filename))
			{
				s.Write(FormatSolution(sol));
			}
		}
	}
}
