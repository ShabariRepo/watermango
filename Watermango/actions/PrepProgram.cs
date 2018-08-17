using System;
using System.Collections.Generic;
using System.Text;
using Watermango.model;

namespace Watermango
{
    class PrepProgram : IPrepProgram
	{
		private IList<Plant> PlantList;

		public PrepProgram()
		{
			PlantList = new List<Plant>();
		}

		public List<Plant> LoadedPlants(int NumPlants)
		{
			for (int i=1; i<=NumPlants; i++)
			{
				CreatePlant(i);
			}

			return (List<Plant>)PlantList;
		}

		private void CreatePlant(int PlantId)
		{
			var localPlant = new Plant()
			{
				Id = PlantId,
				WaterDate = DateTime.Now.AddHours(-1)
			};
			
			PlantList.Add(localPlant);
		}
	}
}
