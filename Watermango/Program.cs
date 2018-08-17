using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Watermango
{
    class Program
	{
		// declare so it gets evaluated at compile time
		private const string WaterCommand = "WATER";
		private const string StatusCommand = "STATUS";
		private const string ShutdownCommand = "SHUTDOWN";

		static void Main(string[] args)
        {
			bool isExit = false;
			//var builder = new ConfigurationBuilder()
			//	.SetBasePath(Directory.GetCurrentDirectory())
			//	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			//IConfigurationRoot configuration = builder.Build();
			//IConfiguration _config = builder.Build();

			//Console.WriteLine(configuration.GetConnectionString("Storage"));

			// prep the program and stage the plants list for operation
			IPrepProgram _prep = new PrepProgram();
			var allPlants = _prep.LoadedPlants(5);
			PlantOperations _plantOperations = new PlantOperations(allPlants);

			while (!isExit)
			{
				var date = DateTime.Now;
				Console.WriteLine($"\nHello, welcome to   ~~~~~~watermango~~~~~   where you can make some of our office plants very happy \nLoggin time: {date:d} at {date:t}!\n");
				
				// list all the plants IDs
				Console.WriteLine("\nHere are the list of Plants we have in the system!\n");
				foreach (var plant in allPlants)
				{
					Console.WriteLine($"Id: {plant.Id},  watered date: {plant.WaterDate},  # times watered: {plant.TimesWatered}");
				}

				// command palette
				Console.WriteLine("\nHere are the list of commands you can use. Please type a command here!");
				Console.WriteLine("\n1. Command: WATER ID   |  Desc: water a particular plant (i.e. WATER 1)\n2. Command: STATUS ID  |  Desc: returns the status plant (i.e. STATUS 1)\n3. Command: SHUTDOWN   |  Desc: Exit the app!");
				var command = Console.ReadLine();

				// if it contains no spaces then enter message and retry
				if (!command.Contains(" "))
				{
					// check if they want to shutdown ? set exit to true : invalid command
					isExit = (command.Equals(ShutdownCommand)) ? true : false;
					var outputMessage = (isExit) ? "\nShutting down, have a nice day \nPress Enter to Close console!": "\nCommand contains no spaces!";
					
					Console.WriteLine(outputMessage);
				}
				else
				{
					// count the number of spaces in the input
					var count = 0;
					for (int i = 0; i < command.Length; i++)
					{
						if (command[i].Equals(" "))
						{
							count++;
						}
					}
					if (count > 1)
					{
						// invalid command
						Console.WriteLine($"\nCommand contains too many spaces! {count} spaces found");
					}
					else
					{
						// evaluate & launch command
						Console.WriteLine("\nEvaluating Command");
						// split the input evaluate the type of command
						string[] commandSet = command.Split(" ");

						// validate ID if a numeric & command is valid
						var idIsNumeric = int.TryParse(commandSet[1], out int plantId);
						var isCommand = (commandSet[0].Equals(WaterCommand) || commandSet[0].Equals(StatusCommand))
							? true : false;

						if (!idIsNumeric || !isCommand)
						{
							Console.WriteLine("\nId Value is not a numeric value or Command is not valid, please retry with a proper command and a number for plant Id from above Plant List!");
						} else
						{
							Console.WriteLine("\nOk Cool working on it..!");

							var validation = (commandSet[0].Equals(WaterCommand))
								? _plantOperations.WaterPlant(plantId)
								: (commandSet[0].Equals(StatusCommand))
								? _plantOperations.StatusOfPlant(plantId)
								: "\n\nInvalid Command, please try again.";

							Console.WriteLine(validation);
						}						
					}
				}

				Console.ReadLine();
				Console.Clear();
			}
		}
	}
}
