using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;

namespace PlayGroupSolver
{
	class PQueue
	{
		private readonly IPriorityQueue<Solution> _queue;

		public int Count { get { return _queue.Count; } }

		public PQueue()
		{
			_queue = new IntervalHeap<Solution>(); 			
		}

		internal bool Empty()
		{
			return _queue.Count == 0;
		}

		internal Solution Dequeue()
		{
			return _queue.DeleteMin();
		}

		internal void Enqueue(Solution sol)
		{
			_queue.Add(sol);
		}
	}
}
