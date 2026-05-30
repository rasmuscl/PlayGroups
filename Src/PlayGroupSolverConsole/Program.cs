using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayGroupSolver;

namespace PlayGroupSolverConsole {
  class Program {
    static void Main( string[] args ) {
      try {
        //ReadInputAnders();
        ReadInputLiva();


        // Create or load plan from file
        SolutionPlan s = CreatePlan();
        //SolutionPlan s = new SolutionPlan(@"plan_solution.txt", true);
        AssignHosts( s );
      }
      catch ( Exception exception ) {
        Console.WriteLine( "Error: {0}\n\n{1}", exception.Message, exception.StackTrace );
      }
    }

    /// <summary>
    /// Load setup
    /// </summary>
    private static void ReadInputAnders() {
      const int B = 0;
      const int G = 1;

      // Input data
      InputData.NumberOfGirls = 11;
      InputData.NumberOfBoys = 10;
      InputData.NumberOfDays = 10;
      InputData.GroupLayout = new List<List<int>>()
				{
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G, G }
				};

      InputData.Names = new List<string>()
				{
                    "Aslan", "Kian", "Louis", "Ludvig", "Luka", "Lukas", "Mads", "Marius", "Mattias", "Noman",
                    "Anna Luna", "Asta R", "Asta S", "Caroline", "Clara", "Emily", "Emma", "Fenja", "Iben", "Sofie", "Valentina"
				};

      InputData.Dates = new List<string>()
				{
					"18-09-2012",
					"10-10-2012",
					"19-11-2012",
           "12-12-2012",
					"08-01-2013",
					"06-02-2013",
					"04-03-2013",
					"09-04-2013",
					"06-05-2013",
					"05-06-2013"
				};
    }


    /// <summary>
    /// Load setup
    /// </summary>
    private static void ReadInputLiva() {
      const int B = 0;
      const int G = 1;

      // Input data
      InputData.NumberOfGirls = 12;
      InputData.NumberOfBoys = 10;
      InputData.GroupLayout = new List<List<int>>()
				{
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G },
					new List<int>() { B, B, G, G, G },
					new List<int>() { B, B, G, G, G }
				};
      InputData.Names = new List<string>()
				{
					"Mathias", "Malthe", "Oliver", "Frederik", "Eskil", "Christian", "Benjamin", "Anton", "Villads", "Albert",
					"Celine", "Dicte", "Elizabeth", "Elvira", "Frederikke", "Frida", "Liva", "Olivia Sofie", "Sarah-Sophie", "Sophie", "Susan", "Zara"
				};
      InputData.Dates = new List<string>()
				{
					"25-09-2013",
					"10-10-2013",
					"19-11-2013",
					"23-01-2014",
					"07-03-2014",
					"13-05-2014"
				};

      InputData.NumberOfDays = InputData.Dates.Count;
    }

    /// <summary>
    /// Assign host to plan
    /// </summary>
    /// <param name="s"></param>
    private static void AssignHosts( SolutionPlan s ) {
      Console.WriteLine( "Assigning hosts..." );

      Solution initial = new SolutionHosting( s );
      SolverBB assignHosts = new SolverBB( null, initial, new HostingFormatterAscii() );
      Solution solHost = assignHosts.SolveDFS();

      PlanFormatter f = new HostingFormatterAscii( (SolutionHosting) solHost );
      Console.WriteLine( f.FormatSolution() );
      solHost.PrintDebugInfo( null );

      PlanFormatter f2 = new HostingFormatterExcel( (SolutionHosting) solHost );
      f2.PrintPlan( "final.txt" );
    }

    /// <summary>
    /// Create plan
    /// </summary>
    /// <returns></returns>
    private static SolutionPlan CreatePlan() {
      Console.WriteLine( "Calculating plan..." );

      Console.WriteLine( DateTime.Now );

      SolverBB solver = new SolverBB( null, new SolutionPlan( null, true ), new PlanFormatterAscii() );
      Solution s = solver.SolveDFS();

      //Console.WriteLine("Objective: {0}", s.Objective());
      //Console.WriteLine("Bound: {0}", s.Bound());
      //Console.WriteLine("Nodes examined: {0}", solver.NodesExamined);
      Console.WriteLine( DateTime.Now );
      s.PrintDebugInfo( "stats_solution.txt" );
      s.SaveToFile( "plan_solution.txt" );

      PlanFormatterAscii f = new PlanFormatterAscii( s );
      // Console.WriteLine(f.FormatSolution());

      f.PrintPlan( "plan_best_solution.txt", s );
      return (SolutionPlan) s;
    }
  }
}
