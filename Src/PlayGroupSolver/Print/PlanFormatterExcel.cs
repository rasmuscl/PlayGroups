using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlayGroupSolver
{
	public class PlanFormatterExcel : PlanFormatter
	{
		public PlanFormatterExcel(Solution sol) : base( sol )
		{

		}
		
		public PlanFormatterExcel() : base()
		{

		}

		public override string FormatSolution( Solution sol )
		{
			SolutionPlan solPlan = sol as SolutionPlan;

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < solPlan.NumberOfDays; i++)
			{
				DateTime dt = DateTime.Parse(InputData.Dates[i]);
				sb.Append(dt.ToString("dd-MM-yyyy"));
				sb.Append("	");
			}
			sb.AppendLine();

			for (int i = 0; i < solPlan.NumberOfChildren; i++)
			{
				for (int j = 0; j < solPlan.NumberOfDays; j++)
				{
					int c = solPlan.Variables[j * solPlan.NumberOfChildren + i];
					sb.Append( InputData.Names[c]);
					sb.Append("	");
				}
				sb.AppendLine("");
			}
			return sb.ToString();
		}
	}
}
