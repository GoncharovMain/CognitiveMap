using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace CognitiveMap
{
	public class TableCsv
	{
		private string _src;
		private char _delimiter;
		protected double[,] _table;
		protected int _row;
		protected int _column;

		public int Row => _row;
		public int Column => _column;

		private TableCsv()
		{
			_delimiter = '\t';
		}
		protected TableCsv(int row, int column) : this()
		{
			_row = row;
			_column = column;

			_table = new double[_row, _column];
		}

		public TableCsv(string src) : this()
		{
			_src = src;
			InitFromCsv();
		}
		public TableCsv(double[,] table) : this()
		{
			_src = "empty.csv";

			_row = table.GetLength(0);
			_column = table.GetLength(1);

			_table = new double[_row, _column];

			for (int i = 0; i < _row; i++)
				for (int j = 0; j < _column; j++)
					_table[i, j] = table[i, j];
		}

		private void InitFromCsv()
		{
			List<List<double>> numbers = new List<List<double>>();
				
			using (StreamReader reader = new StreamReader(_src))
			{
				string line = "";
				while((line = reader.ReadLine()) != null)
				{
					numbers.Add(line.Split(_delimiter)
						.Select(number => Convert.ToDouble(number)).ToList());
				}
			}

			_row = numbers.Count();
			_column = numbers[0].Count();

			_table = new double[_row, _column];
			for (int i = 0; i < _row; i++)
				for (int j = 0; j < _column; j++)
					_table[i, j] = numbers[i][j];

		}

		public void Print()
		{
			_table.Print();
		}
		public void Save(string path)
		{
			using (StreamWriter writer = new StreamWriter(path))
			{
				for (int i = 0; i < _row; i++)
					writer.WriteLine(
						String.Join(_delimiter, this[i]));
			}
		}
		
		public double[] this[int i]
		{
			get { return _table.GetRow(i); }
		}

		public double this[int i, int j]
		{
			get { return _table[i, j]; }
			set { _table[i, j] = value; }
		}
	}

	public class Graph : TableCsv
	{
		public Graph(string src) : base(src){}

		public void Print()
		{
			Console.WriteLine("Graph: ");
			base.Print();
		}

		public static Factor operator +(Factor factor, Graph graph)
		{
			Factor result = new Factor(factor);

			double sum = 0;

			for (int k = 0; k < result.Column - 1; k++)
				for (int i = 0; i < graph.Column; i++){

					sum = 0;

					for (int j = 0; j < graph.Row; j++)
						sum += graph[j, i] * result[j, k];
					
					result[i, k + 1] = result[i, k] + sum;
				}

			return result;
		}
		public static Factor operator +(Graph graph, Factor factor)
		{
			return factor + graph;
		}

	}
	public class Factor : TableCsv
	{
		public Factor(int row, int column) : base(row, column){}

		public Factor(string src) : base(src){}

		public Factor(Factor factor) : base(factor._table){}

		public void Print()
		{
			Console.WriteLine("Factor: ");
			base.Print();
		}

		public static Consonans operator*(Factor factor, Graph graph)
		{
			// cons = Math.Abs(sum) / Math.Abs(a + b + c + ..)
			// cons = abs_sum / sum_abs;

			Consonans consonans = new Consonans(factor.Row, factor.Column);

			double abs_sum = 0;
			double sum_abs = 0;

			for (int k = 0; k < consonans.Column; k++)
				for (int i = 0; i < graph.Column; i++){

					abs_sum = 0; 
					sum_abs = 0;

					for (int j = 0; j < graph.Row; j++)
					{
						abs_sum += graph[j, i] * factor[j, k];
						sum_abs += Math.Abs(graph[j, i] * factor[j, k]);
					}

					consonans[i, k] = sum_abs == 0 ? 0 : Math.Abs(abs_sum) / sum_abs;
				}

			return consonans;
		}

		public static Consonans operator*(Graph graph, Factor factor)
		{
			return factor * graph;
		}
	}
	public class Consonans : TableCsv
	{
		public Consonans(int row, int column) : base(row, column){}

		public Consonans(string src) : base(src){}

		public void Print()
		{
			Console.WriteLine("Consonans: ");
			base.Print();
		}
	}
}