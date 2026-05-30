using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class HostingFormatterExcel : PlanFormatter
	{
		private SolutionHosting _solHost;
		private Plan _plan;
		private int[] _vars;

		public HostingFormatterExcel(SolutionHosting solHost)
			: base(solHost.SolutionPlan)
		{
			_solHost = solHost;

			InitPlan();
		}

		private void InitPlan()
		{
			SolutionPlan solPlan = _sol as SolutionPlan;

			List<Group> groups = new List<Group>();
			
			// Create list of all groups
			for (int i = 0; i < InputData.NumberOfDays; i++) // i = 0..10
			{
				for (int j = 0, l = 0; j < InputData.GroupLayout.Count; l += InputData.GroupLayout[j].Count, j++) // j=0..4
				{
					int var = i * InputData.GroupLayout.Count + j; 

					Group g = new Group();

					for (int k = 0; k < InputData.GroupLayout[j].Count; k++)
					{
						int val = solPlan.Variables[i * InputData.NumberOfChildren + l + k];
						g.AddChild(new Child(val, val == _solHost.Variables[i * InputData.GroupLayout.Count + j]));
					}
					groups.Add( g );
				}
			}
			
			// Build new sorted array						
			List<int> vars = new List<int>();
			for (int i = 0; i < groups.Count; i++)
			{
				for (int j = 0; j < groups[i].Children.Count; j++)
				{
					vars.Add( groups[i].Children[j].ChildNo );
				}
			}
			_vars = vars.ToArray();
		}

		public HostingFormatterExcel()
			: base()
		{

		}

		public override string FormatSolution(Solution sol)
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
					int c = _vars[j * solPlan.NumberOfChildren + i];
					sb.Append(InputData.Names[c]);
					sb.Append("	");
				}
				sb.AppendLine("");
			}
			return sb.ToString();
		}
	}
}
