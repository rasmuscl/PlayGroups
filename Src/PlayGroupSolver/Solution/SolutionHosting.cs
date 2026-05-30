using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PlayGroupSolver {
  public class SolutionHosting: Solution {
    /// <summary>
    /// The one and only solution we work on
    /// </summary>
    private static SolutionPlan _solPlan;

    // Variables
    private int[] _vars;
    private int[] _stats;
    private int[] _lastHosted;
    private int[] _distPenalty;
    private int _nassigned; // Used in IsComplete check
    private int _nextunass; // Used for calculating Successors
    private List<int>[] _domains;
    private int _varcount;
    private int _cost;
    private int _bound;

    private List<int> preferredHosts = new List<int> { 22, 4, 5, 9, 10, 14, 17 };

    public SolutionPlan SolutionPlan { get { return _solPlan; } }
    public int[] Variables { get { return _vars; } }

    public SolutionHosting( SolutionPlan solPlan ) {
      _solPlan = solPlan;

      // Init variables, domains, etc
      Initialize();
    }

    /// <summary>
    /// Init all internal variables
    /// </summary>
    private void Initialize() {
      _varcount = InputData.NumberOfDays * InputData.GroupLayout.Count;
      _vars = new int[_varcount];
      _stats = new int[InputData.NumberOfChildren];
      _lastHosted = new int[InputData.NumberOfChildren];
      _distPenalty = new int[InputData.NumberOfChildren];
      _domains = new List<int>[_varcount];

      for ( int i = 0; i < InputData.NumberOfChildren; i++ ) {
        _stats[i] = 0;
        _lastHosted[i] = -1;
        _distPenalty[i] = 0;
      }

      for ( int i = 0; i < _varcount; i++ ) {
        _vars[i] = -1;
        _domains[i] = new List<int>();
      }

      // Init var domains
      for ( int i = 0; i < InputData.NumberOfDays; i++ ) // i = 0..10
			{
        for ( int j = 0, l = 0; j < InputData.GroupLayout.Count; l += InputData.GroupLayout[j].Count, j++ ) // j=0..4
				{
          int var = i * InputData.GroupLayout.Count + j;

          for ( int k = 0; k < InputData.GroupLayout[j].Count; k++ ) {
            int val = _solPlan.Variables[i * InputData.NumberOfChildren + l + k];
            _domains[var].Add( val ); // 0, 1, 2, ... , 49
          }
        }
      }

      _nextunass = 0;
      _nassigned = 0;
      _cost = 0;
      _bound = 0;
    }

    private void Assign( int var, int val, bool updateBound ) {
      _vars[var] = val;
      _nextunass++;
      _nassigned++;

      UpdateStats( var, val, var / InputData.GroupLayout.Count );
      UpdateCost();

      if ( updateBound ) {
        UpdateBound();
      }
    }

    private void UpdateStats( int var, int val, int day ) {
      _stats[val]++;

      if ( _stats[val] > 1 ) {
        _distPenalty[val] += DistPenalty( day - _lastHosted[val] );
      }
      _lastHosted[val] = day;
    }

    private void UpdateCost() {
      int cost = 0;

      for ( int i = 0; i < _varcount; i++ ) {
        if ( _vars[i] != -1 ) {
          int stat = _stats[_vars[i]];

          cost += StatPenalty( stat );

          // Special rules					
          if ( !preferredHosts.Contains( _vars[i] ) && stat > 1 ) {
            cost += 2000;
          }

          if ( stat >= 1 ) {
            cost += _distPenalty[_vars[i]];
          }
        }
      }
      _cost = cost;
    }

    private int DistPenalty( int dist ) {
      switch ( dist ) {
        case 1: return 2000;
        case 2: return 1500;
        case 3: return 1000;
        default: return 0;
      }
    }

    private int StatPenalty( int stat ) {
      switch ( stat ) {
        case 0: return 0;
        case 1: return 0;
        case 2: return 50;
        case 3: return 600;
        default: return 2000;// We accept at most 3 times
      }
    }

    public override int Objective() {
      return _cost;
    }


    private void UpdateBound() {
      SolutionHosting s = (SolutionHosting) Clone();
      s.MakeComplete();
      _bound = (int) ( (double) s.Objective() );
    }

    public void MakeComplete() {
      int cost = _cost;

      // Assign all un-assigned vars
      for ( int i = _nextunass; i < _vars.Length; i++ ) {
        //Assign( _nextunass, _domains[_nextunass][new Random().Next() % _domains[_nextunass].Count], false );
        Assign( _nextunass, _domains[_nextunass][0], false );
      }
      UpdateCost();

      // Multiply with weight
      _cost = cost + (int) ( (double) ( _cost - cost ) * 0.10 );
    }

    public override int Bound() {
      if ( IsComplete() ) {
        return Objective();
      }
      else {
        return _bound;
      }
    }

    public override bool IsFeasible() {
      return true;
    }

    public override bool IsComplete() {
      return _nassigned == _vars.Length;
    }

    public override Solution Clone() {
      SolutionHosting clone = new SolutionHosting( _solPlan );

      for ( int i = 0; i < InputData.NumberOfChildren; i++ ) {
        clone._stats[i] = _stats[i];
        clone._distPenalty[i] = _distPenalty[i];
        clone._lastHosted[i] = _lastHosted[i];
      }

      // Just copy assigned vars
      for ( int i = 0; i < _nassigned; i++ ) {
        clone._vars[i] = _vars[i];
      }

      clone._nassigned = _nassigned;
      clone._nextunass = _nextunass;
      clone._cost = _cost;
      clone._bound = _bound;

      return clone;
    }

    public override List<Solution> Successors() {
      List<Solution> successors = new List<Solution>();

      // The "branching" based on next un-assigned var
      List<int> dom = _domains[_nextunass];

      for ( int i = 0; i < dom.Count; i++ ) {
        SolutionHosting p = (SolutionHosting) Clone();
        p.Assign( p._nextunass, dom[i], true );
        successors.Add( p );
      }
      return successors;
    }

    public override void PrintDebugInfo( string filename ) {
      int stat = 0;
      StringBuilder sb = new StringBuilder();
      for ( int i = 0; i < InputData.NumberOfChildren; i++ ) {
        sb.AppendLine( String.Format( "{0}: {1}", InputData.Names[i], _stats[i] ) );
        stat += _stats[i];
      }
      sb.AppendLine();
      sb.AppendLine( String.Format( "Total: {0}", stat ) );

      Console.WriteLine( sb.ToString() );
    }


    public override void SaveToFile( string filename ) {
      using ( StreamWriter s = new StreamWriter( filename ) ) {
        // s.Write(StatsToString());
      }
    }

    public override void LoadFromFile( string filename ) {
      // Initialize(filename, true);
    }
  }
}
