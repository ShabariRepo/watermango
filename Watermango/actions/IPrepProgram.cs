using System.Collections.Generic;
using Watermango.model;

namespace Watermango
{
	interface IPrepProgram
	{
		List<Plant> LoadedPlants(int NumPlants);
		void PrepProgramInit();
		void ShowAllPlants(List<Plant> AllPlants);
	}
}