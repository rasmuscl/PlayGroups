using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text;

namespace PlayGroupSolver
{
	/// <summary>
	/// Generic Solution representation
	/// </summary>
	public abstract class Solution : IComparable<Solution>
	{
		/// <summary>
		/// Return objective of complete solution
		/// </summary>
		/// <returns></returns>
		public abstract int Objective();

		/// <summary>
		/// Returns lower bound of current (partial) solution (Complete -> Objective)
		/// </summary>
		/// <returns></returns>
		public abstract int Bound();

		/// <summary>
		/// Returns whether solution is feasible (potential valid)
		/// </summary>
		/// <returns></returns>
		public abstract bool IsFeasible();

		/// <summary>
		/// Returns true if all variables are assigned
		/// </summary>
		/// <returns></returns>
		public abstract bool IsComplete();

		/// <summary>
		/// Copies solution
		/// </summary>
		/// <returns></returns>
		public abstract Solution Clone();

		/// <summary>
		/// Branching - return next level of successors
		/// </summary>
		/// <returns></returns>
		public abstract List<Solution> Successors();

		/// <summary>
		/// Method for printing debug info
		/// </summary>
		public abstract void PrintDebugInfo( string fileanem );
				
		/// <summary>
		/// Save solution to file
		/// </summary>
		/// <param name="fileanem"></param>
		public abstract void SaveToFile(string filename);

		/// <summary>
		/// Load solution from file
		/// </summary>
		/// <param name="fileanem"></param>
		public abstract void LoadFromFile(string filename);

		#region IComparable<Solution> Members

		/// <summary>
		/// Compares two solutions - used for sorting solutions
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(Solution other)
		{
			return Bound() - other.Bound();
		}
		#endregion
	}
}
