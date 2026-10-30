using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Linq;
using System.IO;

namespace AdventureMode
{
	internal class Program
	{
		public static StreamWriter WRITER = new StreamWriter("debug.csv");
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
					WRITER.Dispose();
					Environment.Exit(1);
				}
				Console.Clear();
			}
			//int num = 0;
			string readLine2 = string.Empty;
			byte count = 0;
		COUNT:
			Console.Write("Input how many bounties you'd like to query (1-8): ");
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
							AddObjective(ref area, n);
						}
					}
					else if (count == 1)
					{
						AddObjective(ref area, n);
					}
					else continue;
					continue;
				}
				for (int i = 0; i < count; i++)
				{
					AddObjective(ref area, n);
				}
			}
			area = area.OrderBy(t => "Act").ToList();
			Console.Clear();
			Console.WriteLine("Output of bounties:");
			//int num2 = 0;
			//int num3 = 0;
			foreach (string a in area)
			{
				/*if (num2++ % count == 0)
				{
					if (count > 2 && num2 / count == 3)
					{
						num3++;
					}
					Console.ResetColor();
					Console.WriteLine($"\nAct {++num3}");
				}*/
				//Console.ForegroundColor = ConsoleColor.White;
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
		public static void AddObjective(ref IList<string> area, int actNum)
		{
		ROLL:
			string location = Utility.GetRegionObjective((Act)actNum + 1);
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
			Program.WRITER.WriteLine("index,act,waypoint,index2");
		}
		public static string GetRegionObjective(Act act)
		{
			int index = 0;
			int index2 = 0;
			Region r = default;
			switch (act)
			{
				case Act.One:
					index = Utility.rand.Next(One.Regions.Length);
					r = One.Regions[index];
					index2 = Utility.rand.Next(r.objective.Length);
					Program.WRITER.WriteLine($"{index},{r.act},{r.waypoint},{index2}");
					return  
						$"Act {(int)r.act}:\n   {r.waypoint}\n   {r.objective[index2]}";
				case Act.Two:
					index = Utility.rand.Next(Two.Regions.Length);
					r = Two.Regions[index];
					index2 = Utility.rand.Next(r.objective.Length);
					Program.WRITER.WriteLine($"{index},{r.act},{r.waypoint},{index2}");
					return  
						$"Act {(int)r.act}:\n   {r.waypoint}\n   {r.objective[index2]}";
				case Act.Three:
					index = Utility.rand.Next(Three.Regions.Length);
					r = Three.Regions[index];
					index2 = Utility.rand.Next(r.objective.Length);
					Program.WRITER.WriteLine($"{index},{r.act},{r.waypoint},{index2}");
					return  
						$"Act {(int)r.act}:\n   {r.waypoint}\n   {r.objective[index2]}";
				case Act.Four:
					index = Utility.rand.Next(Four.Regions.Length);
					r = Four.Regions[index];
					index2 = Utility.rand.Next(r.objective.Length);
					Program.WRITER.WriteLine($"{index},{r.act},{r.waypoint},{index2}");
					return  
						$"Act {(int)r.act}:\n   {r.waypoint}\n   {r.objective[index2]}";
				case Act.Five:
					index = Utility.rand.Next(Five.Regions.Length);
					r = Five.Regions[index];
					index2 = Utility.rand.Next(r.objective.Length);
					Program.WRITER.WriteLine($"{index},{r.act},{r.waypoint},{index2}");
					return  
						$"Act {(int)r.act}:\n   {r.waypoint}\n   {r.objective[index2]}";
				default:
					r = default;
					return string.Empty;
			}
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
			return string.Empty;//Area(act)[num].ToString().Replace('_', ' ');
		}
	}
	public struct One
	{
		public static Region[] Regions = new Region[]
		{
			//RogueEncampment,
			BloodMoor,
			ColdPlains,
			BurialGrounds,
			StonyField,
			DarkWood,
			BlackMarsh,
			TamoeHighland,
			OuterCloister,
			Jail,
			InnerCloister,
			Catacombs
		};
		public static Region RogueEncampment => new Region(Act.One, Waypoint.Rogue_Encampment, []);
		public static Region BloodMoor => new Region(Act.One, Waypoint.Rogue_Encampment, [ "Clear Den of Evil" ]);
		public static Region ColdPlains => new Region(Act.One, Waypoint.Cold_Plains, [ "Cave" ]);
		public static Region BurialGrounds => new Region(Act.One, Waypoint.Cold_Plains, 
            [ 
                "Clear the Crypt", 
                "Clear the Mausoleum" 
            ]);
		public static Region StonyField => new Region(Act.One, Waypoint.Stony_Field, 
			[
				"Clear Underground Passage level 2",
				"Clear Tristram"
			]);
		public static Region DarkWood => new Region(Act.One, Waypoint.Dark_Wood, [ "Clear the Tree of Inifuss" ]);
		public static Region BlackMarsh => new Region(Act.One, Waypoint.Black_Marsh, 
			[
				"Clear Hole level 2",
				"Defeat the Countess"
			]);
		public static Region TamoeHighland => new Region(Act.One, Waypoint.Outer_Cloister, 
			[
				"Clear Pit level 2",
				"Visit the Monastery Gate"
			]);
		public static Region OuterCloister => new Region(Act.One, Waypoint.Outer_Cloister, [ "Defeat the Smith" ]);
		public static Region Jail => new Region(Act.One, Waypoint.Jail_level_1, [ "Clear all champions in Level 3" ]);
		public static Region InnerCloister => new Region(Act.One, Waypoint.Inner_Cloister, [ "Clear the Cathedral" ]);
		public static Region Catacombs => new Region(Act.One, Waypoint.Catacombs_level_2, [ "Defeat Andariel" ]);
	}
	public struct Two
	{
        public static Region[] Regions = new Region[]
        {
            //Lut_Gholein,
            Sewers,
            RockyWaste,
            DryHills,
            HallsoftheDead,
            FarOasis,
            MaggotLair,
            LostCity,
            ValleyofSnakes,
            Harem,
            ThePalaceCellar,
            ArcaneSanctuary,
            CanyonoftheMagi,
            TalRashasTomb,
            TalRashasChamber
        };
		public static Region LutGholein => new Region(Act.Two, Waypoint.Lut_Gholein, [ string.Empty ]);
		public static Region Sewers => new Region(Act.Two, Waypoint.Sewers_level_2, [ "Defeat Radamant" ]);
		public static Region RockyWaste => new Region(Act.Two, Waypoint.Lut_Gholein, [ "Clear Stony Tomb Level 2" ]);
		public static Region DryHills => new Region(Act.Two, Waypoint.Dry_Hills, [ "Clear Halls of the Dead Level 3" ]);
		public static Region HallsoftheDead => new Region(Act.Two, Waypoint.Halls_of_the_Dead_level_2, [ "Loot Sparkly Chest" ]);
		public static Region FarOasis => new Region(Act.Two, Waypoint.Far_Oasis, [ "Defeat Beetleburst" ]);
		public static Region MaggotLair => new Region(Act.Two, Waypoint.Far_Oasis, [ "Loot Sparkly Chest" ]);
		public static Region LostCity => new Region(Act.Two, Waypoint.Lost_City, [ "Clear Ancient Tunnels" ]);
		public static Region ValleyofSnakes => new Region(Act.Two, Waypoint.Lost_City, [ "Clear Claw Viper Temple level 1" ]);
		public static Region ClawViperTemple => new Region(Act.Two, Waypoint.Lost_City, [ "Clear Level 2" ]);
		public static Region Harem => new Region(Act.Two, Waypoint.Palace_Cellar_level_1, [ "Clear all champions in level 1" ]);
		public static Region ThePalaceCellar => new Region(Act.Two, Waypoint.Palace_Cellar_level_1, [ "Clear all champions in level 3"]);
		public static Region ArcaneSanctuary => new Region(Act.Two, Waypoint.Arcane_Sanctuary, [ "Loot the Non-Summoner Chests" ]);
		public static Region CanyonoftheMagi => new Region(Act.Two, Waypoint.Canyon_of_the_Magi, [ "Clear the Champions" ]);
		public static Region TalRashasTomb => new Region(Act.Two, Waypoint.Canyon_of_the_Magi, [ "Loot the Sparkly Chests" ]);
		public static Region TalRashasChamber => new Region(Act.Two, Waypoint.Canyon_of_the_Magi, [ "Defeat Duriel" ]);
	}
	public struct Three
	{
        public static Region[] Regions = new Region[]
        {
            //KurastDocks,
            SpiderForest,
            GreatMarsh,
            FlayerJungle,
            LowerKurast,
            KurastBazaar,
            UpperKurast,
            KurastCauseway,
            Travincal,
            DuranceofHate
        };
		public static Region KurastDocks => new Region(Act.Three, Waypoint.Kurast_Docks, [ string.Empty ]);
		public static Region SpiderForest => new Region(Act.Three, Waypoint.Spider_Forest, 
			[
				"Loot Arachnid Lair sparkly chest",
				"Clear the Spider Cavern"
			]);
		public static Region GreatMarsh => new Region(Act.Three, Waypoint.Great_Marsh, [ "Loot all Sparkly Chests" ]);
		public static Region FlayerJungle => new Region(Act.Three, Waypoint.Flayer_Jungle, 
			[
				"Clear Swampy Pit",
				"Loot Sparkly Chest in Flayer Dungeon"
			]);
		public static Region LowerKurast => new Region(Act.Three, Waypoint.Lower_Kurast, [ "Loot Super Chests" ]);
		public static Region KurastBazaar => new Region(Act.Three, Waypoint.Kurast_Bazaar, 
			[
				"Clear Ruined Temple",
				"Clear Disused Fane",
				"Loot Sparkly Chest in Sewers"
			]);
		public static Region UpperKurast => new Region(Act.Three, Waypoint.Upper_Kurast, 
			[
				"Clear Forgotten Temple",
				"Clear Disused Reliquary"
			]);
		public static Region KurastCauseway => new Region(Act.Three, Waypoint.Upper_Kurast, 
			[
				"Clear Disused Reliquary",
				"Clear Ruined Fane"
			]);
		public static Region Travincal => new Region(Act.Three, Waypoint.Travincal, [ "Defeat Council" ]);
		public static Region DuranceofHate => new Region(Act.Three, Waypoint.Durance_of_Hate_level_2, 
			[
				"Defeat Council in Durance level 3",
				"Defeat Mephisto"
			]);
	}
	public struct Four
	{
		public static Region[] Regions = new Region[]
		{
			//PandemoniumFortress,
			OuterSteppes,
			PlainsofDespair,
			CityoftheDamned,
			RiverofFlame,
			ChaosSanctuary
		};
		public static Region PandemoniumFortress => new Region(Act.Four, Waypoint.Pandemonium_Fortress, [ string.Empty ]);
		public static Region OuterSteppes => new Region(Act.Four, Waypoint.Pandemonium_Fortress, [ "Clear at least one champion pack" ]);
		public static Region PlainsofDespair => new Region(Act.Four, Waypoint.City_of_the_Damned, [ "Defeat Izual" ]);
		public static Region CityoftheDamned => new Region(Act.Four, Waypoint.City_of_the_Damned, [ "Clear at least one champion pack" ]);
		public static Region RiverofFlame => new Region(Act.Four, Waypoint.River_of_Flames, 
			[
				"Defeat Hephaestos",
				"Clear River of Flame proper"
			]);
		public static Region ChaosSanctuary => new Region(Act.Four, Waypoint.River_of_Flames, 
		[ 
			"Defeat Lord Deceise",
			"Defeat Diablo"
		]);
	}
	public struct Five
	{
		public static Region[] Regions = new Region[]
		{
			//Harrogath,
			BloodyFoothills,
			FrigidHighlands,
			ArreatPlateau,
			CrystallinePassage,
			NihlathaksTemple,
			GlacialTrail,
			FrozenTundra,
			TheAncientsWay,
			ArreatSummit,
			WorldstoneKeep,
			ThroneofDestruction,
			TheWorldstoneChamber
		};
		public static Region Harrogath => new Region(Act.Five, Waypoint.Harrogath, [ string.Empty ]);
		public static Region BloodyFoothills => new Region(Act.Five, Waypoint.Frigid_Highlands, [ "Defeat Shenk" ]);
		public static Region FrigidHighlands => new Region(Act.Five, Waypoint.Frigid_Highlands, 
			[
				"Defeat Eldritch",
				"Clear Abaddon"
			]);
		public static Region ArreatPlateau => new Region(Act.Five, Waypoint.Arreat_Plateau, 
			[
				"Defeat Threshsocket",
				"Clear Pit of Acheron"
			]);
		public static Region CrystallinePassage => new Region(Act.Five, Waypoint.Crystalline_Passage, [ "Clear Frozen River" ]);
		public static Region NihlathaksTemple => new Region(Act.Five, Waypoint.Halls_of_Pain, 
			[
				"Defeat Pindleskin",
				"Clear Halls of Anguish",
				"Loot Sparkly Chest in Halls of Pain",
				"Clear Halls of Vaught",
				"Defeat Nihlathak"
			]);
		public static Region GlacialTrail => new Region(Act.Five, Waypoint.Glacial_Trail, [ "Loot Drifter Cavern sparkly chest" ]);
		public static Region FrozenTundra => new Region(Act.Five, Waypoint.Frozen_Tundra, [ "Clear the Infernal Pit" ]);
		public static Region TheAncientsWay => new Region(Act.Five, Waypoint.The_Ancients_Way, [ "Loot Icy Cellar sparkly chest" ]);
		public static Region ArreatSummit => new Region(Act.Five, Waypoint.The_Ancients_Way, [ "Visit Arreat Summit" ]);
		public static Region WorldstoneKeep => new Region(Act.Five, Waypoint.Worldstone_Keep_level_2, [ "Clear Worldstone Keep Level 3" ]);
		public static Region ThroneofDestruction => new Region(Act.Five, Waypoint.Worldstone_Keep_level_2, [ "Defeat Baal Waves" ]);
		public static Region TheWorldstoneChamber => new Region(Act.Five, Waypoint.Worldstone_Keep_level_2, [ "Defeat Baal" ]);
	}
	public struct Region
	{
        public Region(Act act, string wp, string[] objective)
		{
			this.act = act;
			this.waypoint = wp;
            this.objective = objective;
		}
		public Act act;
		public string waypoint;
		public string[] objective;
        public override string ToString()
        {
			return $"Act:{act} Waypoint:{waypoint} Length:{objective.Length}";
        }
	}
	public struct Waypoint
	{
		public static string
			Rogue_Encampment = "Rogue Encampment",
			Cold_Plains = "Cold Plains",
            Stony_Field = "Stony Field",
            Dark_Wood = "Dark Wood",
            Black_Marsh = "Black Marsh",
			Outer_Cloister = "Outer Cloister",
			Jail_level_1 = "Jail level 1",
			Inner_Cloister = "Inner Cloister",
			Catacombs_level_2 = "Catacoms level 2",

			Lut_Gholein = "Lut Gholein",
			Sewers_level_2 = "Sewers level 2",
			Dry_Hills = "Dry Hills",
			Halls_of_the_Dead_level_2 = "Halls of the Dead level 2",
			Far_Oasis = "Far Oasis",
			Lost_City = "Lost City",
			Palace_Cellar_level_1 = "Palace Cellar level 1",
			Arcane_Sanctuary = "Arcane Sanctuary",
			Canyon_of_the_Magi = "Canyon of the Magi",

			Kurast_Docks = "Kurast Docks",
			Spider_Forest = "Spider Forest",
			Great_Marsh = "Great Marsh",
			Flayer_Jungle = "Flayer Jungle",
			Lower_Kurast = "Lower Kurast",
			Kurast_Bazaar = "Kurast Bazaar",
			Upper_Kurast = "Upper Kurast",
			Travincal = "Tranvincal",
			Durance_of_Hate_level_2 = "Durance of Hate level 2",

			Pandemonium_Fortress = "Pandemonium Fortress",
			City_of_the_Damned = "City of the Damned",
			River_of_Flames = "River of Flame",

			Harrogath = "Horrogath",
			Frigid_Highlands = "Frigid Highlands",
			Arreat_Plateau = "Arreat Plateau",
			Crystalline_Passage = "Crystalline Passage",
			Halls_of_Pain = "Halls of Pain",
			Glacial_Trail = "Glacial Trail",
			Frozen_Tundra = "Frozen Tundra",
			The_Ancients_Way = "The Ancients Way",
			Worldstone_Keep_level_2 = "Worldstone Keep level 2";
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