using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using System.Text;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

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
			while (!byte.TryParse(Console.ReadLine(), out count)) ;
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
			return Area(act)[num].ToString().Replace('_', ' ');
		}
		public static IList<Waypoint> Area(int act)
		{
			IList<Waypoint> result = new List<Waypoint>();
			switch (act)
			{
				case 1:
					result.Add(Waypoint.Rogue_Encampment);
					result.Add(Waypoint.Cold_Plains);
					result.Add(Waypoint.Stony_Field);
					result.Add(Waypoint.Dark_Wood);
					result.Add(Waypoint.Black_Marsh);
					result.Add(Waypoint.Outer_Cloister);
					result.Add(Waypoint.Jail_level_1);
					result.Add(Waypoint.Inner_Cloister);
					result.Add(Waypoint.Catacombs_level_2);
					break;
				case 2:
					result.Add(Waypoint.Lut_Gholein);
					result.Add(Waypoint.Sewers_level_2);
					result.Add(Waypoint.Dry_Hills);
					result.Add(Waypoint.Halls_of_the_Dead_level_2);
					result.Add(Waypoint.Far_Oasis);
					result.Add(Waypoint.Lost_City);
					result.Add(Waypoint.Palace_Cellar_level_1);
					result.Add(Waypoint.Arcane_Sanctuary);
					result.Add(Waypoint.Canyon_of_the_Magi);
					break;
				case 3:
					result.Add(Waypoint.Kurast_Docks);
					result.Add(Waypoint.Spider_Forest);
					result.Add(Waypoint.Great_Marsh);
					result.Add(Waypoint.Flayer_Jungle);
					result.Add(Waypoint.Lower_Kurast);
					result.Add(Waypoint.Kurast_Bazaar);
					result.Add(Waypoint.Upper_Kurast);
					result.Add(Waypoint.Travincal);
					result.Add(Waypoint.Durance_of_Hate_level_2);
					break;
				case 4:
					result.Add(Waypoint.Pandemonium_Fortress);
					result.Add(Waypoint.City_of_the_Damned);
					result.Add(Waypoint.River_of_Flames);
					break;
				case 5:
					result.Add(Waypoint.Harrogath);
					result.Add(Waypoint.Frigid_Highlands);
					result.Add(Waypoint.Arreat_Plateau);
					result.Add(Waypoint.Crystalline_Passage);
					result.Add(Waypoint.Halls_of_Pain);
					result.Add(Waypoint.Glacial_Trail);
					result.Add(Waypoint.Frozen_Tundra);
					result.Add(Waypoint.The_Ancients_Way);
					result.Add(Waypoint.Worldstone_Keep_level_2);
					break;
			}
			return result;
		}
	}
	public struct One
	{
		public static Region RogueEncampment => new Region(Act.One, Waypoint.Rogue_Encampment, default, []);
		public static Region BloodMoor => new Region(Act.One, Waypoint.Rogue_Encampment, default, [ "Clear Den of Evil" ]);
		public static Region ColdPlains => new Region(Act.One, Waypoint.Cold_Plains, default, [ "Cave" ]);
		public static Region BurialGrounds =>new Region(Act.One, Waypoint.Cold_Plains, default, 
			[
				"Crypt",
				"Mausoleum"
			]);
		public static Region StonyField => new Region(Act.One, Waypoint.Stony_Field, default,
			[
				"Underground Passage",
				"Tristram"
			]);
		public static Region DarkWood => new Region(Act.One, Waypoint.Dark_Wood, default, [ "Clear the Tree of Inifuss" ]);
		public static Region BlackMarsh => new Region(Act.One, Waypoint.Black_Marsh, default,
			[
				"Hole",
				"Forgotten Tower"
			]);
		public static Region TamoeHighland => new Region(Act.One, Waypoint.Outer_Cloister, default, 
			[
				"Pit",
				"Monastery Gate"
			]);
		public static Region OuterCloister => new Region(Act.One, Waypoint.Outer_Cloister, default, [ "Barracks" ]);
		public static Region Jail => new Region(Act.One, Waypoint.Jail_level_1, default, [ "Clear all champions in Level 3" ]);
		public static Region InnerCloister => new Region(Act.One, Waypoint.Inner_Cloister, default, [ "Cathedral" ]);
		public static Region Catacombs => new Region(Act.One, Waypoint.Catacombs_level_2, default, [ "Defeat Andariel" ]);
	}
	public struct Two
	{
		public static Region LutGholein => string.Empty;
		public static Region Sewers => "Defeat Radamant";
		public static Region RockyWaste => "Stony Tomb";
		public static Region DryHills => string.Empty;
		public static Region HallsoftheDead => "Loot Sparkly Chest";
		public static Region FarOasis => "Defeat Beetleburst";
		public static Region MaggotLair => "Loot Sparkly Chest";
		public static Region LostCity => "Ancient Tunnels";
		public static Region ValleyofSnakes => string.Empty;
		public static Region ClawViperTemple => "Clear Level 2";
		public static Region Harem => string.Empty;
		public static Region ThePalaceCellar => string.Empty;
		public static Region ArcaneSanctuary => "Loot the Non-Summoner Chests";
		public static Region CanyonoftheMagi => "Clear the Champions";
		public static Region TalRashasTomb => "Loot the Sparkly Chests";
		public static Region TalRashasChamber => "Defeat Duriel";
	}
	public struct Three
	{
		public static Region KurastDocks => string.Empty;
		public static Region[] SpiderForest =>
			[
				"Arachnid Lair",
				"Spider Cavern"
			];
		public static Region GreatMarsh => "Loot all Sparkly Chests";
		public static Region[] FlayerJungle =>
			[
				"Swampy Pit",
				"Loot Sparky Chest in Flayer Dungeon"
			];
		public static Region LowerKurast => "Loot Super Chests";
		public static Region[] KurastBazaar =>
			[
				"Ruined Temple",
				"Disused Fane",
				"Loot Sparkly Chest in Sewers"
			];
		public static Region[] UpperKurast =>
			[
				"Forgotten Temple",
				"Reliquary"
			];
		public static Region[] KurastCauseway =>
			[
				"Disused Reliquary",
				"Ruined Fane"
			];
		public static Region Travincal => "Defeat Council";
		public static Region[] DuranceofHate =>
			[
				"Defeat Council",
				"Defeat Mephisto"
			];
	}
	public struct Four
	{
		public static Region PandemoniumFortress => string.Empty;
		public static Region OuterSteppes => string.Empty;
		public static Region PlainsofDespair => "Defeat Izual";
		public static Region CityoftheDamned => string.Empty;
		public static Region[] RiverofFlame =>
			[
				"Defeat Hephaestos",
				"Clear River of Flame proper"
			];
		public static Region ChaosSanctuary => "Defeat Diablo";
	}
	public struct Five
	{
		public static Region Harrogath => string.Empty;
		public static Region BloodyFoothills => "Defeat Shenk";
		public static Region[] FrigidHighlands =>
			[
				"Defeat Eldritch",
				"Clear Abaddon"
			];
		public static Region[] ArreatPlateau =>
			[
				"Defeat Threshsocket",
				"Clear Pit of Acheron"
			];
		public static Region CrystallinePassage => "Frozen River";
		public static Region[] NihlathaksTemple =>
			[
				"Defeat Pindleskin",
				"Halls of Anguish",
				"Loot Sparkly Chest in Halls of Pain",
				"Halls of Vaught"
			];
		public static Region GlacialTrail => "Drifter Cavern";
		public static Region FrozenTundra => "Infernal Pit";
		public static Region TheAncientsWay => "Icy Cellar";
		public static Region ArreatSummit => string.Empty;
		public static Region WorldstoneKeep => "Clear Level 3";
		public static Region ThroneofDestruction => "Defeat Baal Waves";
		public static Region TheWorldstoneChamber => "Defeat Baal";
	}
	public struct Region
	{
		public Region(Act act, Waypoint wp, Area area, string[] objective)
		{
			this.act = act;
			this.waypoint = wp;
			this.area = area;
			this.objective = objective;
		}
		public Act act;
		public Waypoint waypoint;
		public Area area;
		public string[] objective;
	}
	public struct Area
	{

	}
	public enum Waypoint : int
	{
		Rogue_Encampment = -1,
		Cold_Plains,
		Stony_Field,
		Dark_Wood,
		Black_Marsh,
		Outer_Cloister,
		Jail_level_1,
		Inner_Cloister,
		Catacombs_level_2,

		Lut_Gholein = -2,
		Sewers_level_2,
		Dry_Hills,
		Halls_of_the_Dead_level_2,
		Far_Oasis,
		Lost_City,
		Palace_Cellar_level_1,
		Arcane_Sanctuary,
		Canyon_of_the_Magi,

		Kurast_Docks = -3,
		Spider_Forest,
		Great_Marsh,
		Flayer_Jungle,
		Lower_Kurast,
		Kurast_Bazaar,
		Upper_Kurast,
		Travincal,
		Durance_of_Hate_level_2,

		Pandemonium_Fortress = -4,
		City_of_the_Damned,
		River_of_Flames,

		Harrogath = -5,
		Frigid_Highlands,
		Arreat_Plateau,
		Crystalline_Passage,
		Halls_of_Pain,
		Glacial_Trail,
		Frozen_Tundra,
		The_Ancients_Way,
		Worldstone_Keep_level_2
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
