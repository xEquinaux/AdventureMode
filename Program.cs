namespace AdventureMode
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Utility.Initialize();
			StartHere:
			Console.WriteLine("Command: enter 'bounties' to generate a set of them. Or enter 'exit' to close.");
			string readLine = string.Empty;
			while ((readLine = Console.ReadLine()) != "bounties") 
			{
				if (readLine == "exit")
				{
					Environment.Exit(1);
				}
				Console.Clear();
			}
			int num = 0;
			string readLine2 = string.Empty;
			byte count = 0;
			COUNT:
			Console.Write("Input how many waypoints you'd like to query (1-8): ");
			while (!byte.TryParse(Console.ReadLine(), out count));
			if (count < 1 || count > 8)
			{
				Console.WriteLine("Try again.");
				goto COUNT;
			}
			Console.Clear();
			IList<string> area = new List<string>();
			for (int n = 0; n < 5; n++)
			{ 
				if (n == 3) 
				{
					if (count == 2)
					{ 
						for (int m = 0; m < 2; m++)
						{
							AddWaypoint(ref area, n);
						}
					}
					else if (count == 1)
					{
						AddWaypoint(ref area, n);
					}
					else continue;
					continue;
				}
				for (int i = 0; i < count; i++)
				{
					AddWaypoint(ref area, n);
				}
			}
			Console.Clear();
			Console.WriteLine("Output of bounties:");
			int num2 = 0;
			int num3 = 0;
			foreach (string a in area)
			{
				if (num2++ % count == 0)
				{
					if (count > 2 && num2 / count == 3)
					{
						num3++;
					}
					Console.ResetColor();
					Console.WriteLine($"\nAct {++num3}");
				}
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(a);
			}
			Console.ResetColor();
			Console.WriteLine("\nPress any key to restart...");
			Console.ReadKey();
			Console.Clear();
			goto StartHere;
		}
		public static void AddWaypoint(ref IList<string> area, int actNum)
		{
			ROLL:
			string location = Utility.Location((Act)actNum + 1);
			if (!area.Contains(location))
			{
				area.Add(location);
				return;
			}
			goto ROLL;
		}
	}
	public struct Utility
	{
		public static System.Random rand;
		public static void Initialize()
		{
			rand = new System.Random(DateTime.Now.Millisecond);
		}
		public static string Location(Act act)
		{
			int num = rand.Next(8);
			if (act == Act.Four)
			{
				num = rand.Next(2);
			}
			return GetName(num + 1, (int)act);
		}
		public static string[] Location(Act act, short count = 3)
		{
			if (count > 8) count = 8;
			string[] location = new string[count];
			int num = 0;
			do
			{
				START:
				string area = GetName(rand.Next(8) + 1, (int)act);
				if (!location.Contains(area))
				{
					location[num] = area;
				}
				else goto START;
			} while (num++ < count);
			return location;
		}
		public static string GetName(int num, int act)
		{
			return Waypoint.Area(act)[num].Replace('_', ' ');
		}
	}
	public struct Waypoint
	{
		public static IList<string> Area(int act)
		{
			IList<string> result = new List<string>();
			switch (act)
			{
				case 1:
					result.Add("Rogue_Encampment");
					result.Add("Cold_Plains");
					result.Add("Stony_Field");
					result.Add("Dark_Wood");
					result.Add("Black_Marsh");
					result.Add("Outer_Cloister");
					result.Add("Jail_level_1");
					result.Add("Inner_Cloister");
					result.Add("Catacombs_level_2");
					break;
				case 2:
					result.Add("Lut_Gholein");
					result.Add("Sewers_level_2");
					result.Add("Dry_Hills");
					result.Add("Halls_of_the_Dead_level_2");
					result.Add("Far_Oasis");
					result.Add("Lost_City");
					result.Add("Palace_Cellar_level_1");
					result.Add("Arcane_Sanctuary");
					result.Add("Canyon_of_the_Magi");
					break;
				case 3:
					result.Add("Kurast_Docks");
					result.Add("Spider_Forest");
					result.Add("Great_Marsh");
					result.Add("Flayer_Jungle");
					result.Add("Lower_Kurast");
					result.Add("Kurast_Bazaar");
					result.Add("Upper_Kurast");
					result.Add("Travincal");
					result.Add("Durance_of_Hate_level_2");
					break;
				case 4:
					result.Add("Pandemonium_Fortress");
					result.Add("City_of_the_Damned");
					result.Add("River_of_Flames");
					break;
				case 5:
					result.Add("Harrogath");
					result.Add("Frigid_Highlands");
					result.Add("Arreat_Plateau");
					result.Add("Crystalline_Passage");
					result.Add("Halls_of_Pain");
					result.Add("Glacial_Trail");
					result.Add("Frozen_Tundra");
					result.Add("The_Ancients_Way");
					result.Add("Worldstone_Keep_level_2");
					break;
			}
			return result;
		}
	}
	public enum Act : int
	{
		One = 1,
		Two = 2,
		Three = 3,
		Four = 4,
		Five = 5
	}
}
