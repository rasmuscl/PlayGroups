using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlayGroupSolver
{
	public class PlanFormatterAscii : PlanFormatter
	{
		public PlanFormatterAscii(Solution sol ) : base( sol )
		{
			
		}

		public PlanFormatterAscii() : base()
		{

		}

		private string GetSeparator( int colWidth )
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < InputData.NumberOfDays; i++)
			{
				sb.Append("+");
				for (int j = 0; j <= colWidth; j++) sb.Append("-");				
			}
			sb.Append("+");
			return sb.ToString();
		}

		protected string Padding( string s, int colWidth )
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < colWidth - s.Length; i++)
			{
				sb.Append(" ");
			}
			return sb.ToString();
		}

		public virtual void WriteName( StringBuilder sb, int c, int colWidth, int groupIdx )
		{
			sb.Append(InputData.Names[c]);
			sb.Append(Padding(InputData.Names[c], colWidth));
		}

		public override string FormatSolution( Solution sol )
		{
			SolutionPlan solPlan = null;

			if (sol is SolutionPlan)
			{
				solPlan = sol as SolutionPlan;
			}
			else
			{
				solPlan = ((SolutionHosting)sol).SolutionPlan;
			}

			StringBuilder sb = new StringBuilder();

			int colWidth = GetColWidth();
			sb.AppendLine( GetSeparator( colWidth ) );

			for (int i = 0; i < solPlan.NumberOfDays; i++)
			{
				sb.Append("| ");
				sb.Append(FormatDate( InputData.Dates[i] ) );
				sb.Append(Padding(FormatDate(InputData.Dates[i]), colWidth));
			}
			sb.Append("|");
			sb.AppendLine();
			sb.AppendLine(GetSeparator(colWidth));


			int k = 0;
			for (int i = 0; i < InputData.GroupLayout.Count; i++)
			{
				for (int j = 0; j < InputData.GroupLayout[i].Count; j++)
				{
					for (int l = 0; l < solPlan.NumberOfDays; l++)
					{
						int c = solPlan.Variables[l * solPlan.NumberOfChildren + k];

						sb.Append("| ");

						if (c != -1)
						{
							WriteName(sb, c, colWidth, l * InputData.GroupLayout.Count + i);
						}
						else
						{
							sb.Append("");
							sb.Append(Padding("", colWidth));
						}
					}
					k++;
					sb.Append("|");
					sb.AppendLine();
				}
				sb.AppendLine(GetSeparator(colWidth));
			}
			
			return sb.ToString();
		}

		private string FormatDate(string date)
		{
			return DateTime.Parse(date).ToString("ddd dd. MMM yyyy");
		}

		private int GetColWidth()
		{
			int colWidth = 0;
			for (int i = 0; i < InputData.Names.Count; i++)
			{
				if (InputData.Names[i].Length > colWidth)
				{
					colWidth = InputData.Names[i].Length;
				}
			}

			for (int i = 0; i < InputData.Dates.Count; i++)
			{
				if (FormatDate( InputData.Dates[i] ).Length > colWidth)
				{
					colWidth = FormatDate(InputData.Dates[i]).Length;
				}
			}
			return colWidth+2;
		}
	}
}
