using System;
using System.Linq;
using System.Collections.Generic;

namespace CognitiveMap
{
	public static class ArrayExtension
	{
		public static double[] GetRow(this double[,] arr, int row)
		{
			double[] line = new double[arr.GetLength(1)];

			for (int i = 0; i < line.Length; i++)
				line[i] = arr[row, i];

			return line;
		}

		public static void Print(this double[,] arr)
		{
			Console.Write("\t|");

			for (int i = 1 ; i < arr.GetLength(1) + 1; i++)
				Console.Write("{0}\t", i);

			Console.WriteLine();
			Console.WriteLine(new String('-', 9 * arr.GetLength(1)));
			
			for (int i = 0; i < arr.GetLength(0); i++)
			{

				Console.Write("{0}\t|", i + 1);

				for (int j = 0; j < arr.GetLength(1); j++)
					Console.Write("{0:0.###}\t", arr[i, j]);

				Console.WriteLine();
			}
			Console.WriteLine();
		}
	}
}