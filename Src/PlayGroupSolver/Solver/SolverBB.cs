using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayGroupSolver
{
	public class SolverBB
	{
		private PQueue _pqueue;
		private Solution _bestSolution;
		private Solution _initial;
		private int _bestObjective;
		private int _nodesExamined;
		private PlanFormatter _formatter;

		public int NodesExamined { get { return _nodesExamined; } }

		public SolverBB(
			string filename, 
			Solution initialSolution, 
			PlanFormatter formatter )
		{
			_pqueue = new PQueue();
			_initial = initialSolution;
			_bestSolution = null;
			_bestObjective = int.MaxValue;
			_nodesExamined = 0;
			_formatter = formatter;
		}

		public Solution SolveDFS()
		{
			return SolveDFS(_initial);
		}

		/// <summary>
		/// DFS version of BB
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public Solution SolveDFS( Solution s )
		{
			_nodesExamined++;

			if (s.IsComplete())
			{
				// We know s is better based on check (A) above
				_bestSolution = s.Clone();
				_bestObjective = s.Objective();
				
				Console.WriteLine("Updating best solution ({0})", _bestObjective);
				
				_formatter.PrintPlan("plan_best.txt", _bestSolution);
				//_bestSolution.SaveToFile("plan_best_idx.txt");
				_bestSolution.PrintDebugInfo("plan_stats_best.txt");
			}
			else
			{
				List<Solution> succ = s.Successors(); // Dissolve s into sub-problems
				succ.Sort();
				foreach (Solution child in succ)
				{
					// Save only children with lower boudns for further handling
					int bound = child.Bound();
					if (child.IsFeasible() && bound < _bestObjective)
					{
						SolveDFS(child);
					}
					else
					{
						//Console.WriteLine("Node phatomed! (Bound: {0})", bound);
						//_formatter.PrintPlan("plan_dbg.txt", child);
						//child.PrintDebugInfo("stats_dbg.txt");
					}
				}
			}
			return _bestSolution;
		}		
		
		/// <summary>
		/// BFS version of BB
		/// </summary>
		/// <returns></returns>
		public Solution SolveBFS()
		{
			_pqueue.Enqueue(_initial.Clone());
			
			while (!_pqueue.Empty())
			{
				Solution s = _pqueue.Dequeue();

				// For debug
				//s.PrintPlan("plan_debug.txt");
				//s.PrintStats("stats_debug.txt");
				int bound = s.Bound();

				if (bound >= _bestObjective) // Check (A)
				{
					// Throw away
					Console.WriteLine("Throwing away node");
					break;
				}
				else
				{
					if (s.IsComplete())
					{						
						// We know s is better based on check (A) above
						_bestSolution = s.Clone();
						_bestObjective = s.Objective();

						Console.WriteLine("Updating best solution");
						_formatter.PrintPlan("plan_best.txt", s);
						// s.PrintStats("stats_best.txt");
					}
					else
					{
						List<Solution> succ = s.Successors(); // Dissolve s into sub-problems
						foreach (Solution child in succ)
						{
							int childBound = child.Bound();

							// Save only children with lower boudns for further handling
							if (child.IsFeasible() && childBound < _bestObjective)
							{
								_pqueue.Enqueue(child);
							}
							else
							{
								Console.WriteLine("Child phatomed");
							}
						}
					}
				}
			}
			return _bestSolution;
		}
	}
}
