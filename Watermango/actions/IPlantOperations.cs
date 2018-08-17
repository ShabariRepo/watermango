using Watermango.model;

namespace Watermango
{
	interface IPlantOperations
	{
		string StatusOfPlant(int PlantId);
		string WaterPlant(int PlantId);
	}
}