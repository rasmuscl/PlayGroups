using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VisualPlan
{
	public partial class PlanForm : Form
	{
		private Dictionary<string, int> _nameIdx;
		private int[,] _playStats;
		private int[,] _plan;
		private const int NUMBER_OF_DAYS = 10;
		private const int NUMBER_OF_CHILDREN = 24;
		private const int GROUP_SIZE = 4;
		private bool _controlDown = false;

		List<string> children = new List<string>()
				{
					"Albert", "Anton", "Benjamin", "Christian", "Eskil", "Frederik", "Hector", "Malthe", "Mathias", "Villads", "William", "",
					"Celine", "Dicte", "Elizabeth", "Elvira", "Frederikke", "Frida", "Liva", "Olivia Sofie", "Sarah-Sophie", "Sophie", "Susan", "Zara"
				};

		List<string> dates = new List<string>()
				{
					"23-08-2011", "21-09-2011", "13-10-2011", "18-11-2011", "10-01-2012", "08-02-2012", "01-03-2012", "30-03-2012", "24-04-2012", "10-05-2012"
				};

		public PlanForm()
		{
			InitializeComponent();
		}

		private void InitPlayStats()
		{
			_playStats = new int[NUMBER_OF_CHILDREN, NUMBER_OF_CHILDREN];

			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				for (int j = 0; j < NUMBER_OF_CHILDREN; j++)
				{
					_playStats[i, j] = 0;
				}
			}
		}

		private void InitPlan()
		{
			_plan = new int[NUMBER_OF_CHILDREN, NUMBER_OF_DAYS];
			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				for (int j = 0; j < NUMBER_OF_DAYS; j++)
				{
					_plan[i, j] = 0;
				}
			}
		}

		private void Initialize()
		{
			_nameIdx = new Dictionary<string, int>();
			/*_nameIdx[ "Albert" ] = 0;
			_nameIdx[ "Anton" ] = 1;
			_nameIdx[ "Benjamin" ] = 2;
			_nameIdx[ "Christian" ] = 3;
			_nameIdx[ "Eskil" ] = 4;
			_nameIdx[ "Frederik" ] = 5;
			_nameIdx[ "Hector" ] = 6;
			_nameIdx[ "Malthe" ] = 7;
			_nameIdx[ "Mathias" ] = 8;
			_nameIdx[ "Villads" ] = 9;
			_nameIdx["William"] = 10;
			_nameIdx["x"] = 11;*/

			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				_nameIdx[i.ToString()] = i;
			}


			InitPlayStats();
			InitPlan();
		}

		private void LoadFile(string filename)
		{
			using (StreamReader readFile = new StreamReader(filename))
			{
				string line;
				string[] row;
				int lineCount = 0;

				while ((line = readFile.ReadLine()) != null)
				{
					row = line.Split(new char[] { ';', '	' });

					if (row.Length == 1)
					{
						continue;
					}

					for (int i = 0; i < NUMBER_OF_DAYS; i++)
					{
						if (!String.IsNullOrEmpty(row[i]))
						{
							_plan[lineCount, i] = _nameIdx[row[i]];
						}
						else
						{
							_plan[lineCount, i] = -1;
						}
					}
					lineCount++;
				}
			}
		}

		private void PlanForm_Load(object sender, EventArgs e)
		{
			LoadPlan();
			
		}

		
		private void LoadPlan()
		{
			try
			{
				Initialize();
				LoadFile(@"plan_solution.txt");
				UpdatePlan();

				UpatePlayStats();

				this.Text = String.Format("Play Planner - Cost: {0}", Cost());
			}
			catch (Exception exception)
			{
				MessageBox.Show(String.Format( "{0}\n\n{1}", exception.Message, exception.StackTrace));
				throw;
			}			
		}


		private void UpdatePlan()
		{
			dataGridViewPlan.Rows.Clear();
			dataGridViewPlan.Rows.Add(NUMBER_OF_CHILDREN-1);

			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{				
				for (int j = 0; j < NUMBER_OF_DAYS; j++)
				{
					if (_plan[i, j] != -1)
					{
						dataGridViewPlan.Rows[i].Cells[j].Value = children[_plan[i, j]];
					}
				}
			}

			ResetColors();
		}

		private void ResetColors()
		{
			for (int i = 0; i < dataGridViewPlan.Rows.Count; i++)
			{
				for (int j = 0; j < dataGridViewPlan.Columns.Count; j++)
				{
					if ((i / 4) % 2 == 0) // even
						dataGridViewPlan.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
					else
						dataGridViewPlan.Rows[i].Cells[j].Style.BackColor = Color.White;

				}
			}			
		}


		
		public int Cost()
		{
			int cost = 0;

			for (int i = 0; i < NUMBER_OF_DAYS; i++)
			{
				for (int j = 0; j < NUMBER_OF_CHILDREN; j += GROUP_SIZE)
				{
					if (_plan[j, i] != -1 && _plan[j + 1, i] != -1)	
					{
						cost += _playStats[_plan[j, i], _plan[j + 1, i]];
					}
				}
			}
			return cost;
		}




		public void UpatePlayStats()
		{
			InitPlayStats();

			for (int i = 0; i < NUMBER_OF_DAYS; i++)
			{
				for (int j = 0; j < NUMBER_OF_CHILDREN; j += GROUP_SIZE)
				{
					for (int k = j; k < j + GROUP_SIZE - 1; k++)
					{
						for (int l = k + 1; l < j + GROUP_SIZE; l++)
						{
							if (_plan[k, i] != -1 && _plan[l, i] != -1)
							{
								_playStats[_plan[k, i], _plan[l, i]]++;
								_playStats[_plan[l, i], _plan[k, i]]++;
							}
						}
					}
				}
			}
		}

		private void ShowPlayInfo( int child, bool playedWith, bool onlyGroups, bool resetColors )
		{
			if (resetColors)
			{
				ResetColors();
			}
			UpatePlayStats();

			List<int> notPlayedWith = new List<int>();
									
			for (int i = 0; i < dataGridViewPlan.Rows.Count; i++)
			{
				for (int j = 0; j < dataGridViewPlan.Columns.Count; j++)
				{
					if (dataGridViewPlan.Rows[i].Cells[j].Value != null)
					{
						int v = (int)_plan[dataGridViewPlan.Rows[i].Cells[j].RowIndex, dataGridViewPlan.Rows[i].Cells[j].ColumnIndex];

						if (child != v)
						{
							if ((_playStats[child, v] > 0 && playedWith) || (_playStats[child, v] == 0 && !playedWith))
							{
								if (!onlyGroups || CheckGroup(child, i / 4, j))
								{
									dataGridViewPlan.Rows[i].Cells[j].Style.BackColor = (v > 11) ? Color.LightPink : Color.LightGreen;
								}
							}

							if (_playStats[child, v] == 0 && !notPlayedWith.Contains(v))
							{
								notPlayedWith.Add(v);
							}
						}
						else
						{
							dataGridViewPlan.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
						}
					}
				}
			}
						
			labelNotPlayedWith.Text = FormatList( notPlayedWith);

			// Intersect not played with
			if (dataGridViewPlan.SelectedCells.Count > 1)
			{
				HashSet<int> result = new HashSet<int>();

				for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
				{
					result.Add(i);
				}

				foreach (DataGridViewCell cell in dataGridViewPlan.SelectedCells)
				{
					HashSet<int> set = GetNotPlayedWith(((int)cell.Value) - 1);
					result.IntersectWith(set);
				}

				List<int> resultList = new List<int>();

				foreach (int c in result)
				{
					resultList.Add(c);
				}

				labelNotPlayedWithSelected.Text = FormatList(resultList);
			}
			else
			{
				labelNotPlayedWithSelected.Text = "";
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

		private bool CheckGroup(int child, int group, int day)
		{
			for (int i = group * GROUP_SIZE; i < group * GROUP_SIZE + GROUP_SIZE; i++)
			{
				if (_plan[i, day] == child)
				{
					return true;
				}
			}
			return false;
		}
		
		private List<int> GetPlayedWith(int child)
		{
			List<int> playedWith = new List<int>();

			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				if (_playStats[child, i] > 0)
				{
					playedWith.Add(i);
				}
			}
			return playedWith;
		}


		private HashSet<int> GetNotPlayedWith(int child)
		{
			HashSet<int> playedWith = new HashSet<int>();

			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				if (_playStats[child, i] == 0 && child != i)
				{
					playedWith.Add(i);
				}
			}
			return playedWith;
		}
		
		private void dataGridViewPlan_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if ( e.RowIndex != -1 && e.ColumnIndex != -1 )
			{
				if (!_controlDown)
				{
					dataGridViewPlan.ClearSelection();
				}
				dataGridViewPlan.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

				if (dataGridViewPlan.SelectedCells[0].Value != null)
				{
					int val = _plan[dataGridViewPlan.SelectedCells[0].RowIndex, dataGridViewPlan.SelectedCells[0].ColumnIndex];

					if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						ShowPlayInfo(val, true, true, !_controlDown);

						UpdateUsedInfo(e.ColumnIndex);

					}
					else
					{
						ShowPlayInfo(val, false, false, true);
					}
				}
			}
		}


		private string FormatNotUsed( int l, int u, int col)
		{
			HashSet<int> all = new HashSet<int>();
			for (int i = l; i < u; i++)
			{
				all.Add(i);
			}
			HashSet<int> inUse = new HashSet<int>();
			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				if (_plan[i, col] != -1 && _plan[i, col] >= l &&_plan[i, col] < u)
				{
					inUse.Add(_plan[i, col]);
				}
			}
			all.ExceptWith(inUse);
			return FormatSet(all);
		}


		private void UpdateUsedInfo(int col)
		{			
			labelNotUsedBoys.Text = FormatNotUsed(0, 12, col);
			labelNotUsedGirls.Text = FormatNotUsed(12, 24, col);
		}

		private void buttonReload_Click(object sender, EventArgs e)
		{
			LoadPlan();
		}
			

		private void dataGridViewPlan_KeyDown(object sender, KeyEventArgs e)
		{
			_controlDown = e.Control;
		}

		private void dataGridViewPlan_KeyUp(object sender, KeyEventArgs e)
		{
			_controlDown = e.Control;
		}

		private void PrintStats()
		{
			for (int i = 0; i < NUMBER_OF_CHILDREN; i++)
			{
				for (int j = 0; j < NUMBER_OF_CHILDREN; j++)
				{
					Console.Write(_playStats[i, j]);
					Console.Write("	");
				}
				Console.WriteLine("");
			}
		}

		private void buttonPrintStats_Click(object sender, EventArgs e)
		{
			PrintStats();
		}
	}
}
