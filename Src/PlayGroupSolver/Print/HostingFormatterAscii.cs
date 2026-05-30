using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class HostingFormatterAscii : PlanFormatterAscii
	{
		private SolutionHosting _solHost;

		public HostingFormatterAscii(SolutionHosting solHost)
			: base(solHost.SolutionPlan)
		{
			_solHost = solHost;
		}

		public HostingFormatterAscii()
			: base()
		{

		}

		public override void WriteName(StringBuilder sb, int c, int colWidth, int groupIdx)
		{
			string host = "";

			if (c == _solHost.Variables[groupIdx])
			{
				host = " (*)";
			}

			sb.Append(InputData.Names[c] + host);
			sb.Append(Padding(InputData.Names[c] + host, colWidth));
		}


		public override void PrintPlan(string filename, Solution sol)
		{
			_solHost = (SolutionHosting)sol;
			base.PrintPlan(filename, sol);
		}
	}
}
