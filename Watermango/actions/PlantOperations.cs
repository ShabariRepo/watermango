using System;
using System.Collections.Generic;
using System.Text;
using Watermango.model;
using System.Linq;
using System.Threading;

namespace Watermango
{
    class PlantOperations : IPlantOperations
	{
		private IList<Plant> _PlantList;
		private string returnMessage;
		//private OperationReturn _opRet;

		public PlantOperations()
		{
		}

		public PlantOperations(List<Plant> PlantListIn)
		{
			_PlantList = PlantListIn;
			//_opRet = new OperationReturn();
		}

		// Water plant & update the list
		// PlantId = Id of plant from user input
		public string WaterPlant(int PlantId)
		{
			var plant = _PlantList.Where(p => p.Id == PlantId).SingleOrDefault();

			if(plant == null)
			{
				returnMessage = $"\nNo Plant with the given id: {PlantId}!";
			} else
			{
				var seconds = (DateTime.Now - plant.WaterDate).TotalSeconds;
								
				// water plant if > 30 seconds & update time and # watered
				if (seconds < 30)
				{
					returnMessage = "\nSorry the plant is not thirsty yet, please wait 30 seconds or more before watering the same plant.";
				} else
				{
					plant.TimesWatered++;
					plant.WaterDate = DateTime.Now;

					returnMessage = $"\nSuccessfully watered plant {PlantId}.";					
				}
			}

			Thread.Sleep(10000);
			return returnMessage;
		}
		
		// Return status of the plant
		// PlantId = Id of plant from user input
		public string StatusOfPlant(int PlantId)
		{
			var plant = _PlantList.Where(p => p.Id == PlantId).SingleOrDefault();

			if (plant == null)
			{
				returnMessage = $"\nNo Plant with the given id: {PlantId}!";
			}
			else
			{
				// get the hours and minutes between watering
				var mins = Math.Ceiling((DateTime.Now - plant.WaterDate).TotalMinutes);
				var hours = Math.Floor((DateTime.Now - plant.WaterDate).TotalHours);

				var minsMessage = mins < 1 ? "within the past" : mins.ToString();
				var hoursMessage = hours < 1 ? "Within the past" : hours.ToString();

				returnMessage = $"\nPlant {PlantId} has not been watered for {hoursMessage} hour(s) and {minsMessage} minute(s).  Its been watered {plant.TimesWatered} time(s) so far.";
			}

			return returnMessage;
		}
    }
}
