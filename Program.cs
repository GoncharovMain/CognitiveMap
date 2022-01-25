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
			Graph graph = new Graph("data_link.csv");
			Factor factor = new Factor("factors_changes.csv");

			factor.Print();
			factor = factor + graph;
			
			Consonans consonans = graph * factor;

			graph.Print();
			factor.Print();
			consonans.Print();

			factor.Save("resources/factor_gon.csv");
			consonans.Save("resources/consonans_gon.csv");
		}
	}
}