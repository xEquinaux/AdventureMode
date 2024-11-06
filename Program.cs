﻿namespace AdventureMode
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
			byte count = 2;	// Change count per input in later version
			IList<string> area = new List<string>();
			while (num++ < 4)
			{
				for (int n = 0; n < 5; n++)
				{ 
					if (n == 3) continue;
					for (int i = 0; i < 3; i++)
					{
						area.Add(Utility.Location((Act)n, count)[i]);
					}
				}
			}
			Console.Clear();
			Console.WriteLine("Output of bounties:");
			int num2 = 0;
			foreach (string a in area)
			{
				if (num2 % count == 0)
				{
					Console.WriteLine($"\nAct {num2 + 1}");
				}
				Console.WriteLine(a);
				num2++;
			}
			Console.WriteLine("Press any key to restart...");
			Console.ReadKey();
			Console.Clear();
			goto StartHere;
		}
	}
	public struct Utility
	{
		public static System.Random rand;
		public static void Initialize()
		{
			rand = new System.Random(DateTime.Now.Millisecond);
		}
		public static string[] Location(Act act, byte count = 3)
		{
			if (count > 8) count = 8;
			string[] location = new string[count];
			int num = 0;
			var getAct = GetAct((int)act);
			do
			{
				START:
				string area = GetName(getAct[rand.Next(9)], 1);
				if (!location.Contains(area))
				{
					location[num] = area;
				}
				else goto START;
			} while (num++ < count);
			return location;
		}
		public static List<Waypoint> GetAct(int act)
		{
			return Enum.GetValues<Waypoint>().Where(t => (int)t == act).ToList();
		}
		public static string GetName(Waypoint w, int act)
		{
			if ((int)w == act && (int)w > 0)
			{
				return w.ToString().Replace('_', ' ');
			}
			else return string.Empty;
		}
	}
	public enum Waypoint : int
	{
		Rogue_Encampment = -1,
		Cold_Plains = 1,
		Stony_Field = 1,
		Dark_Wood = 1,
		Black_Marsh = 1,
		Outer_Cloister = 1,
		Jail, _level_1 = 1,
		Inner_Cloister = 1,
		Catacombs_level_2 = 1,

		Lut_Gholein = -2,
		Sewers_level_2 = 2,
		Dry_Hills = 2,
		Halls_of_the_Dead_level_2 = 2,
		Far_Oasis = 2,
		Lost_City = 2,
		Palace_Cellar_level_1 = 2,
		Arcane_Sanctuary = 2,
		Canyon_of_the_Magi = 2,

		Kurast_Docks = -3,
		Spider_Forest = 3,
		Great_Marsh = 3,
		Flayer_Jungle = 3,
		Lower_Kurast = 3,
		Kurast_Bazaar = 3,
		Upper_Kurast = 3,
		Travincal = 3,
		Durance_of_Hate_level_2 = 3,

		Pandemonium_Fortress = -4,
		City_of_the_Damned = 4,
		River_of_Flames = 4,

		Harrogath = -5,
		Frigid_Highlands = 5,
		Arreat_Plateau = 5,
		Crystalline_Passage = 5,
		Halls_of_Pain = 5,
		Glacial_Trail = 5,
		Frozen_Tundra = 5,
		The_Ancients_Way = 5,
		Worldstone_Keep_level_2 = 5
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