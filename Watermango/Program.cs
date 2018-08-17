using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Watermango
{
    class Program
	{
		// declare so it gets evaluated at compile time
		private const string WaterCommand = "WATER";
		private const string StatusCommand = "STATUS";
		private const string ShutdownCommand = "SHUTDOWN";

		private static bool isExit = false;
		private IPrepProgram _prep;
		private IPlantOperations _plantOperations;

		static void Main(string[] args)
        {
			//var builder = new ConfigurationBuilder()
			//	.SetBasePath(Directory.GetCurrentDirectory())
			//	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			//IConfigurationRoot configuration = builder.Build();
			//IConfiguration _config = builder.Build();

			//Console.WriteLine(configuration.GetConnectionString("Storage"));

			//Thread eventThread = new Thread(ThreadMethod);
			//eventThread.Start();

			// prep the program and stage the plants list for operation
			IPrepProgram _prep = new PrepProgram();
			var allPlants = _prep.LoadedPlants(5);
			IPlantOperations _plantOperations = new PlantOperations(allPlants);
			
			while (!isExit)
			{
				_prep.PrepProgramInit();
				_prep.ShowAllPlants(allPlants);
				var command = Console.ReadLine();

				// if it contains no spaces then enter message and retry
				if (!command.Contains(" "))
				{
					// check if they want to shutdown ? set exit to true : invalid command
					isExit = (command.Equals(ShutdownCommand)) ? true : false;
					var outputMessage = (isExit) ? "\nShutting down, have a nice day \nPress Enter to Close console!": "\nCommand is invalid, the phrase contains no spaces!";
					
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
						Console.WriteLine($"\nCommand is invalid, the phrase contains too many spaces! {count} spaces found");
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
							Console.WriteLine("Ok Cool working on it..!");


							new Thread(() =>
							{
								Thread.CurrentThread.IsBackground = true;
								/* run your code here */
								//Console.WriteLine("\n\nHello, world from second Thread");

								if(commandSet[0].Equals(WaterCommand))
									Console.WriteLine("***Watering plants is strenuous and takes the system about 10 SECONDS to finish, you will be notified once finished.  Press ENTER to continue issuing commands.***");

								var validation = (commandSet[0].Equals(WaterCommand))
									? _plantOperations.WaterPlant(plantId)
									: (commandSet[0].Equals(StatusCommand))
									? _plantOperations.StatusOfPlant(plantId)
									: "\n\nInvalid Command, please try again.";

								Console.WriteLine(validation);
							}).Start();
						}						
					}
				}

				Console.ReadLine();
				Console.Clear();
			}
		}
	}
}
