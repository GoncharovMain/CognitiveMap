using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace CognitiveMap
{
	public class Program
	{
		public static void Main()
		{
			Graph graph = new Graph("example/graph.csv");
			Factor factor = new Factor("example/factors_changes.csv");

			factor.Print();
			factor = factor + graph;
			
			Consonans consonans = graph * factor;

			graph.Print();
			factor.Print();
			consonans.Print();

			factor.Save("example/factor_result.csv");
			consonans.Save("example/consonans.csv");
		}
	}
}