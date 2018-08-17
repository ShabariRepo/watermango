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
		
		// Initial message output to user
		public void PrepProgramInit()
		{
			var date = DateTime.Now;
			Console.WriteLine($"\nHello, welcome to   ~~~~~~watermango~~~~~   where you can make some of our office plants very happy \nLoggin time: {date:d} at {date:t}!\n");
						
			// command palette
			Console.WriteLine("\nHere are the list of commands you can use. Please type a command here!");
			Console.WriteLine("\n1. Command: WATER ID   |  Desc: water a particular plant (i.e. WATER 1)\n2. Command: STATUS ID  |  Desc: returns the status of the particular plant (i.e. STATUS 1)\n3. Command: SHUTDOWN   |  Desc: Exit the app!");

		}

		// show full status of each plant
		public void ShowAllPlants(List<Plant> AllPlants)
		{
			// list all the plants IDs
			Console.WriteLine("\nHere are the list of Plants we have in the system!\n");
			foreach (var plant in AllPlants)
			{
				Console.WriteLine($"Id: {plant.Id},  watered date: {plant.WaterDate},  # times watered: {plant.TimesWatered}");
			}
		}
	}
}
