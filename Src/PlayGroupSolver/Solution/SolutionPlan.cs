using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlayGroupSolver
{
	public class SolutionPlan : Solution 
	{
		static int _planIdGlobal = -1;

		private int[,] _stats; // _stats[i,j]: Play count of i and j
		private int[,] _distPenalty;
		//private int[,] _specialPenalty;
		private int[,] _lastPlayed;		
		
		private int[] _vars;
		private int _nassigned; // Used in IsComplete check
		private int _nextunass; // Used for calculating Successors
		private List<int>[] _domains;
		private int _cost;
		private int _bound;
		
		private int _ndays;
		private int _nchildren;
		private int _planId;

		/// <summary>
		/// Mist getters
		/// </summary>
		public int NumberOfChildren { get { return _nchildren; } }
		public int NumberOfDays { get { return _ndays; } }
		public int[] Variables { get { return _vars; } }

		internal struct GroupInfo
		{
			public int GroupIndex { get; set; }
			public int GroupStart { get; set; }
			public int GroupEnd { get; set; }			
		}

		public SolutionPlan(string filename, bool updateStats)
		{
			_planId = _planIdGlobal++;
			
			_ndays = InputData.NumberOfDays;
			_nchildren = InputData.NumberOfChildren;

			_nassigned = 0;
			_nextunass = 0;
			_domains = new List<int>[_ndays*2];
			_cost = 0;
			_bound = 0;

			for (int i = 0; i < _ndays*2; i++)
			{
				_domains[i] = new List<int>();

				if (i % 2 == 0) // boys
				{
					for (int j = 0; j < InputData.NumberOfBoys; j++)
					{
						_domains[i].Add(j);
					}
				}
				else
				{
					for (int j = InputData.NumberOfBoys; j < _nchildren; j++)
					{
						_domains[i].Add(j);
					}
				}
			}

			Initialize(filename, updateStats);
		}
				
		public void Initialize(string filename, bool updateStats )
		{
			InitPlan();
			InitPlayStats();

			if (!string.IsNullOrEmpty(filename))
			{
				LoadFile(filename);
			}

			if (updateStats)
			{
				UpdatePlayStats();
			}
		}

		public string PlanToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(String.Format( "{0}", _planId));
			sb.AppendLine(String.Format( "Bound: {0}", Bound()));
			sb.AppendLine(String.Format( "Objective: {0}", Objective()));
			for (int i = 0; i < _nchildren; i++)
			{
				for (int j = 0; j < _ndays; j++)
				{
					sb.Append(_vars[j*_nchildren+i]);
					sb.Append("	");
				}
				sb.AppendLine("");				
			}
			return sb.ToString();
		}
		
		/// <summary>
		/// Print stats
		/// </summary>
		/// <param name="filename"></param>
		public override void PrintDebugInfo(string filename)
		{
			using (StreamWriter s = new StreamWriter(filename))
			{
				s.Write(StatsToString());
			}
		}

		public string StatsToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(_planId.ToString());
			for (int i = 0; i < _nchildren; i++)
			{
				for (int j = 0; j < _nchildren; j++)
				{
					sb.Append(_stats[i, j]);
					sb.Append("	");
				}
				sb.AppendLine("");
			}
			return sb.ToString();
		}
		
		private void InitPlayStats()
		{
			_stats = new int[_nchildren, _nchildren];
			_distPenalty = new int[_nchildren, _nchildren];
			_lastPlayed = new int[_nchildren, _nchildren];
			//_specialPenalty = new int[_nchildren, _nchildren];

			for (int i = 0; i < _nchildren; i++)
			{
				for (int j = 0; j < _nchildren; j++)
				{
					_stats[i, j] = 0;
					_distPenalty[i, j] = 0;
					_lastPlayed[i, j] = -1;
					//_specialPenalty[i, j] = 0;
				}
			}
		}

		private void InitPlan()
		{
			_vars = new int[_nchildren * _ndays];
			for (int i = 0; i < _vars.Length; i++)
			{
				_vars[i] = -1;
			}
		}

		private void LoadFile(string filename)
		{
			using (StreamReader s = new StreamReader(filename))
			{
				string line;
				string[] row;
				int lineCount = 0;

				while ((line = s.ReadLine()) != null)
				{
					row = line.Split(new char[] { ';', '	' });

					if (row.Length == 1)
					{
						continue;
					}

					if (_ndays == 0)
					{
						_ndays = row.Length;
					}

					for (int i = 0; i < row.Length; i++)
					{
						if (!String.IsNullOrEmpty(row[i]))
						{
							Assign(_nchildren * i + lineCount, Int32.Parse( row[i] ));
						}					
					}
					lineCount++;
				}
			}			
		}


		private void UpdatePairStats(int c1, int c2, int day)
		{	
			if (c1 != -1 && c2 != -1) // Both must be unassigned
			{
                try
                {
                    // Update symmetric stats
                    _stats[c1, c2]++;
                    _stats[c2, c1]++;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }				

				if (_stats[c1, c2] > 1) // they have played before
				{
					// Calcualate distance penalty - the closer, the more penalty
					_distPenalty[c1, c2] += DistPenalty(day - _lastPlayed[c1, c2]);
					_distPenalty[c2, c1] += DistPenalty(day - _lastPlayed[c2, c1]);
				}

				/*if (_stats[c1, c2] > 1 )
				{
					_specialPenalty[c1, c2] += SpecialPenalty(_stats[c1, c2]);
					_specialPenalty[c1, c2] += SpecialPenalty(_stats[c2, c1]);
				}*/

				// Update last played
				_lastPlayed[c1, c2] = day;
				_lastPlayed[c2, c1] = day;
			}
		}

		private void UpdateGroupStats( int from, int to, int day )
		{
			for (int k = from; k < to - 1; k++) // Stat all pairs within group
			{
				for (int l = k + 1; l < to; l++)
				{
					int c1 = _vars[day * _nchildren + k];
					int c2 = _vars[day * _nchildren + l];

					UpdatePairStats(c1, c2, day);
				}
			}
		}

		public void UpdatePlayStats()
		{
			InitPlayStats(); // Reset play stats

			for (int i = 0; i < _ndays; i++) // Run through all days
			{
				for (int j = 0, m = 0; j < _nchildren; j += InputData.GroupLayout[m].Count, m++) // Iterate over groups
				{
					UpdateGroupStats(j, j + InputData.GroupLayout[m].Count, i);
				}
			}
		}

		/// <summary>
		/// Updates the internal cost
		/// </summary>
		private void UpdateCost()
		{
			int cost = 0;

			for (int i = 0; i < _nchildren; i++)
			{
				for (int j = 0; j < _nchildren; j++)
				{
					if (i != j) // Only treat different children
					{
						int stat = _stats[i, j]; // How many times did i play with j

						cost += StatPenalty(stat);
												
						if (stat > 1) // If played before, add the distance penalty
						{
							cost += _distPenalty[i, j]/* + _specialPenalty[i, j]*/;
						}
					}
				}
			}
			_cost = cost;
		}

		/// <summary>
		/// Calculates cost (objective) for a complete plan
		/// </summary>
		/// <returns></returns>
		public override int Objective()
		{
			return _cost;			
		}

		private void UpdateBound()
		{
			SolutionPlan s = (SolutionPlan)Clone();
			s.MakeComplete();
			_bound = (int)((double)s.Objective());
		}

		public override int Bound()
		{
			// Return lower mininum based on current (partial) plan. Bound 
			// must be a "as good as it gets" value for specfic partial assignment
			
			if (IsComplete())
			{
				return Objective();
			}
			else
			{
				return _bound;

				//int nunassigned = _vars.Length - _nassigned;
				//double w1 = (double)_nassigned / (double)_vars.Length;
				//double w2 = (double)nunassigned / (double)_vars.Length;

				//int bound = (int)(w1 * cost) + (int)(w2 * 15000);

				/*int bound = 
					(int)((double)cost / ((double)((_nassigned == 0) ? 1 : _nassigned )) *_vars.Length)  + 
					(int)(w2 * 20000);*/

				//SolutionPlan s = (SolutionPlan)Clone();
				//s.MakeComplete();
				//return s.Objective();

				//int bound = cost + (int)(w2 * 8000); // current cost + fraction of remainder
				//return bound;
			}
		}

		/// <summary>
		/// Complete plan based on current state
		/// </summary>
		public void MakeComplete()
		{
			int cost = _cost;

			for (int i = _nextunass; i < _vars.Length; i++)
			{
				List<int> dom = GetDomain(_nextunass);

				if (dom.Count > 0)
				{
					//int j = new Random().Next() % dom.Count;
					int j = 0;
					_vars[_nextunass] = dom[j];
					dom.RemoveAt(j);
					_nextunass++;
					_nassigned++;
				}
			}
			UpdatePlayStats();
			UpdateCost();

			// Multiply with weight
			_cost = cost + (int)((double)(_cost-cost)*0.50);
		}
		
		private int StatPenalty(int stat)
		{
			switch (stat)
			{
				case 0: return 30;
				case 1: return 0;
				case 2: return 150;
				case 3: return 200;//100!
				default: return 1000;// We accept at most 3 times
			}
		}

		/*
		private int SpecialPenalty(int stat)
		{
			switch (stat)
			{
				case 1: return 0;
				case 2: return 50;
				case 3: return 500; 
				default: return 1000; // 2 times
			}
		}*/

		private int DistPenalty(int dist)
		{
			switch (dist)
			{
				case 1: return 50; // Adjacent days - bad!
				case 2: return 20;  // 
				case 3: return  0;  // 
				default: return 0;  //  
			}
		}
		
		private string FormatSet(HashSet<int> set)
		{
			List<int> resultList = new List<int>();
			foreach (int i in set)
			{
				resultList.Add(i);
			}
			return FormatList(resultList);
		}

		private string FormatList(List<int> list)
		{
			bool firstGirl = false;

			StringBuilder s = new StringBuilder();
			list.Sort();
			for (int i = 0; i < list.Count; i++)
			{
				s.Append(list[i] + 1);

				if (i < list.Count - 1)
				{
					if (list[i + 1] + 1 <= 12)
					{
						firstGirl = true;
					}
					if ( list[i+1] + 1 > 12 && firstGirl)
					{
						s.Append(" ---- ");
						firstGirl = false;
					}
					else
					{
						s.Append(", ");
					}
				}
			}
			s.Append(string.Format(" ({0})", list.Count));
			return s.ToString();
		}
				
		private List<int> GetPlayedWith(int child)
		{
			List<int> playedWith = new List<int>();

			for (int i = 0; i < _nchildren; i++)
			{
				if (_stats[child, i] > 0)
				{
					playedWith.Add(i);
				}
			}
			return playedWith;
		}
		
		private HashSet<int> GetNotPlayedWith(int child)
		{
			HashSet<int> playedWith = new HashSet<int>();

			for (int i = 0; i < _nchildren; i++)
			{
				if (_stats[child, i] == 0 && child != i)
				{
					playedWith.Add(i);
				}
			}
			return playedWith;
		}
		
		private string FormatNotUsed( int l, int u, int col)
		{
			HashSet<int> all = new HashSet<int>();
			for (int i = l; i < u; i++)
			{
				all.Add(i);
			}
			HashSet<int> inUse = new HashSet<int>();
			for (int i = 0; i < _nchildren; i++)
			{
				if (_vars[col * _nchildren + i] != -1 && _vars[col * _nchildren + i] >= l && _vars[col * _nchildren + i] < u)
				{
					inUse.Add(_vars[col * _nchildren + i]);
				}
			}
			all.ExceptWith(inUse);
			return FormatSet(all);
		}

		public override bool IsFeasible()
		{
			// Return false is plan is infeasible (duplets and other rule violations)
			return true;
		}

		public override bool IsComplete()
		{
			return _nassigned == _vars.Length;
		}

		public override Solution Clone()
		{	
			// Copy entire instance
			SolutionPlan clone = new SolutionPlan(null, false);

			for (int i = 0; i < _nchildren; i++)
			{
				for (int j = 0; j < _nchildren; j++)
				{
					clone._stats[i,j] = _stats[i,j];
					clone._distPenalty[i,j] = _distPenalty[i,j];
					//clone._specialPenalty[i,j] = _specialPenalty[i,j];
					clone._lastPlayed[i,j] = _lastPlayed[i,j];
				}			 
			}

			// Just copy assigned vars
			for (int i = 0; i < _nassigned; i++)
			{
				clone._vars[i] = _vars[i];
			}
			
			clone._nassigned = _nassigned;
			clone._nextunass = _nextunass;
			clone._cost = _cost;
			clone._bound = _bound;
					
			// Copy (pruned) domains
			for (int i = 0; i < _ndays * 2; i++)
			{
				clone._domains[i].Clear(); // Empty domain
				for (int j = 0; j < _domains[i].Count; j++)
				{
					clone._domains[i].Add( _domains[i][j] );
				}
			}
			return clone;
		}
			
		private void Assign(int var, int value)
		{
			// Assign and prune domain
			_vars[var] = value;
			GetDomain(var).Remove(value);
			
			_nextunass++;
			_nassigned++;

			// Update after each assingment within group
			int day = var / _nchildren;
			GroupInfo groupInfo = GetGroupInfo(var % InputData.NumberOfChildren );

			if (groupInfo.GroupIndex != -1)
			{
				int c1 = _vars[var];

				for (int i = groupInfo.GroupStart; i < groupInfo.GroupEnd; i++)
				{
					int c2 = _vars[day * _nchildren + i];
					
					if (c1 != c2)
					{
						UpdatePairStats(c1, c2, day);
					}
				}
			}

			// Update the current cost
			UpdateCost();
			UpdateBound();
		}
		
		private GroupInfo GetGroupInfo(int var)
		{
			for (int i = 0, j = 0; i < _nchildren; i += InputData.GroupLayout[j].Count, j++) // i = 0, 5, 10, ...
			{	
				if (var >= i && var < i + InputData.GroupLayout[j].Count)
				{
					return new GroupInfo()
					{ 
						GroupStart = i, 
						GroupEnd = i + InputData.GroupLayout[j].Count, 
						GroupIndex = j
					};
				}				
			}
			return new GroupInfo() {  GroupStart = -1, GroupEnd = -1, GroupIndex = -1};
		}

		private List<int> GetDomain(int var)
		{
			// Boy or girl?
			int offset = InputData.IsBoy( var % _nchildren ) ? 0 : 1;				
			return _domains[(var / _nchildren) * 2 + offset];
		}

		public override List<Solution> Successors()
		{
			List<Solution> successors = new List<Solution>();

			// The "branching" based on next un-assigned var
			List<int> dom = GetDomain(_nextunass);

			for (int i = 0; i < dom.Count; i++)
			{
				SolutionPlan p = (SolutionPlan)Clone();
				p.Assign(p._nextunass, dom[i]);				
				successors.Add(p);
			}			
			return successors;
		}

		public override void SaveToFile(string filename)
		{
			using (StreamWriter s = new StreamWriter(filename))
			{
				s.Write(PlanToString());
			}			
		}

		public override void LoadFromFile(string filename)
		{
			Initialize(filename, true);
		}
	}
}
