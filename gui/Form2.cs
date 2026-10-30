using AdventureMode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace gui
{
    public partial class Form2 : Form
    {
        public static DateTime Now = DateTime.Now;
        public static Random rand = 
            new Random(
                Now.Microsecond + 
                Now.Second + 
                Now.Day + 
                Now.Month + 
                Now.Year
            );
        public static Button[] board = default;
        public Form2()
        {
            InitializeComponent();
            board = new Button[]
            {
                button1,    button2,    button3,    button4,    button5,
                button6,    button7,    button8,    button9,    button10,
                button11,   button12,   button13,   button14,   button15,
                button16,   button17,   button18,   button19,   button20,
                button21,   button22,   button23,   button24,   button25
            };
            foreach (var item in board)
            {
                item.Click += (e, o) => 
                {
                    if (item.BackColor == Color.ForestGreen)
                    { 
                        item.BackColor = Color.SteelBlue;
                    }
                    else
                    {   
                        item.BackColor = Color.ForestGreen; 
                    }
                    item.ForeColor = Color.GhostWhite;
                };
                item.ForeColor = Color.GhostWhite;
                item.BackColor = Color.SteelBlue;
            }
            Card.Generate();
        }
    }
    public struct Card
    {
        public bool active = false;
        public Region result = default;
        public Button button = default;
        public Card(Region result)
        {
            this.result = result;
        }
        public static Card[] Generate()
        {
            Card[] card = new Card[25];
            IList<int> include = new List<int>();
            int tries = 200;
            for (int i = 0; i < card.Length; i++)
            {
            RESTART:
                if (tries-- <= 0) return card;
                int index = Form2.rand.Next(RogueData.Count);
                Region region = RogueData.AllRegions[index];
                if (!include.Contains(index))
                {
                    include.Add(index);
                    card[i] = new Card(region);
                    card[i].button = Form2.board[i];
                    string activity = region.objective[Form2.rand.Next(region.objective.Length)];
                    card[i].button.Text = $"Act {region.act}\n{region.waypoint}\n{activity}";
                    continue;
                }
                else goto RESTART;
            }
            return card;
        }
    }

    public struct RogueData
    {
        public static Region[] ActOne = new Region[]
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
        public static Region BloodMoor => new Region(Act.One, Waypoint.Rogue_Encampment, ["Clear Den of Evil"]);
        public static Region ColdPlains => new Region(Act.One, Waypoint.Cold_Plains, ["Cave"]);
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
        public static Region DarkWood => new Region(Act.One, Waypoint.Dark_Wood, ["Clear the Tree of Inifuss"]);
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
        public static Region OuterCloister => new Region(Act.One, Waypoint.Outer_Cloister, ["Defeat the Smith"]);
        public static Region Jail => new Region(Act.One, Waypoint.Jail_level_1, ["Clear all champions in Level 3"]);
        public static Region InnerCloister => new Region(Act.One, Waypoint.Inner_Cloister, ["Clear the Cathedral"]);
        public static Region Catacombs => new Region(Act.One, Waypoint.Catacombs_level_2, ["Defeat Andariel"]);

        public static Region[] ActTwo = new Region[]
        {
                //LutGholein,
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
        public static Region LutGholein => new Region(Act.Two, Waypoint.Lut_Gholein, [string.Empty]);
        public static Region Sewers => new Region(Act.Two, Waypoint.Sewers_level_2, ["Defeat Radamant"]);
        public static Region RockyWaste => new Region(Act.Two, Waypoint.Lut_Gholein, ["Clear Stony Tomb Level 2"]);
        public static Region DryHills => new Region(Act.Two, Waypoint.Dry_Hills, ["Clear Halls of the Dead Level 3"]);
        public static Region HallsoftheDead => new Region(Act.Two, Waypoint.Halls_of_the_Dead_level_2, ["Loot Sparkly Chest"]);
        public static Region FarOasis => new Region(Act.Two, Waypoint.Far_Oasis, ["Defeat Beetleburst"]);
        public static Region MaggotLair => new Region(Act.Two, Waypoint.Far_Oasis, ["Loot Sparkly Chest"]);
        public static Region LostCity => new Region(Act.Two, Waypoint.Lost_City, ["Clear Ancient Tunnels"]);
        public static Region ValleyofSnakes => new Region(Act.Two, Waypoint.Lost_City, ["Clear Claw Viper Temple level 1"]);
        public static Region ClawViperTemple => new Region(Act.Two, Waypoint.Lost_City, ["Clear Level 2"]);
        public static Region Harem => new Region(Act.Two, Waypoint.Palace_Cellar_level_1, ["Clear all champions in level 1"]);
        public static Region ThePalaceCellar => new Region(Act.Two, Waypoint.Palace_Cellar_level_1, ["Clear all champions in level 3"]);
        public static Region ArcaneSanctuary => new Region(Act.Two, Waypoint.Arcane_Sanctuary, ["Loot the Non-Summoner Chests"]);
        public static Region CanyonoftheMagi => new Region(Act.Two, Waypoint.Canyon_of_the_Magi, ["Clear the Champions"]);
        public static Region TalRashasTomb => new Region(Act.Two, Waypoint.Canyon_of_the_Magi, ["Loot the Sparkly Chests"]);
        public static Region TalRashasChamber => new Region(Act.Two, Waypoint.Canyon_of_the_Magi, ["Defeat Duriel"]);

        public static Region[] ActThree = new Region[]
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
        public static Region KurastDocks => new Region(Act.Three, Waypoint.Kurast_Docks, [string.Empty]);
        public static Region SpiderForest => new Region(Act.Three, Waypoint.Spider_Forest,
            [
                "Loot Arachnid Lair sparkly chest",
                    "Clear the Spider Cavern"
            ]);
        public static Region GreatMarsh => new Region(Act.Three, Waypoint.Great_Marsh, ["Loot all Sparkly Chests"]);
        public static Region FlayerJungle => new Region(Act.Three, Waypoint.Flayer_Jungle,
            [
                "Clear Swampy Pit",
                    "Loot Sparkly Chest in Flayer Dungeon"
            ]);
        public static Region LowerKurast => new Region(Act.Three, Waypoint.Lower_Kurast, ["Loot Super Chests"]);
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
        public static Region Travincal => new Region(Act.Three, Waypoint.Travincal, ["Defeat Council"]);
        public static Region DuranceofHate => new Region(Act.Three, Waypoint.Durance_of_Hate_level_2,
            [
                "Defeat Council in Durance level 3",
                    "Defeat Mephisto"
            ]);

        public static Region[] ActFour = new Region[]
        {
                //PandemoniumFortress,
                OuterSteppes,
                PlainsofDespair,
                CityoftheDamned,
                RiverofFlame,
                ChaosSanctuary
        };
        public static Region PandemoniumFortress => new Region(Act.Four, Waypoint.Pandemonium_Fortress, [string.Empty]);
        public static Region OuterSteppes => new Region(Act.Four, Waypoint.Pandemonium_Fortress, ["Clear at least one champion pack"]);
        public static Region PlainsofDespair => new Region(Act.Four, Waypoint.City_of_the_Damned, ["Defeat Izual"]);
        public static Region CityoftheDamned => new Region(Act.Four, Waypoint.City_of_the_Damned, ["Clear at least one champion pack"]);
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

        public static Region[] ActFive = new Region[]
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
        public static Region Harrogath => new Region(Act.Five, Waypoint.Harrogath, [string.Empty]);
        public static Region BloodyFoothills => new Region(Act.Five, Waypoint.Frigid_Highlands, ["Defeat Shenk"]);
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
        public static Region CrystallinePassage => new Region(Act.Five, Waypoint.Crystalline_Passage, ["Clear Frozen River"]);
        public static Region NihlathaksTemple => new Region(Act.Five, Waypoint.Halls_of_Pain,
            [
                "Defeat Pindleskin",
                   "Clear Halls of Anguish",
                    "Loot Sparkly Chest in Halls of Pain",
                    "Clear Halls of Vaught",
                    "Defeat Nihlathak"
            ]);
        public static Region GlacialTrail => new Region(Act.Five, Waypoint.Glacial_Trail, ["Loot Drifter Cavern sparkly chest"]);
        public static Region FrozenTundra => new Region(Act.Five, Waypoint.Frozen_Tundra, ["Clear the Infernal Pit"]);
        public static Region TheAncientsWay => new Region(Act.Five, Waypoint.The_Ancients_Way, ["Loot Icy Cellar sparkly chest"]);
        public static Region ArreatSummit => new Region(Act.Five, Waypoint.The_Ancients_Way, ["Visit Arreat Summit"]);
        public static Region WorldstoneKeep => new Region(Act.Five, Waypoint.Worldstone_Keep_level_2, ["Clear Worldstone Keep Level 3"]);
        public static Region ThroneofDestruction => new Region(Act.Five, Waypoint.Worldstone_Keep_level_2, ["Defeat Baal Waves"]);
        public static Region TheWorldstoneChamber => new Region(Act.Five, Waypoint.Worldstone_Keep_level_2, ["Defeat Baal"]);
        public static List<Region> AllRegions =
                RogueData.ActOne
                    .Concat(RogueData.ActTwo)
                    .Concat(RogueData.ActThree)
                    .Concat(RogueData.ActFour)
                    .Concat(RogueData.ActFive)
                    .ToList();
        public static int Count = AllRegions.Count;
    }
    public struct Region
    {
        public Region(Act act, Waypoint wp, string[] objective)
        {
            this.act = act;
            this.waypoint = wp;
            this.objective = objective;
        }
        public bool active = false;
        public Act act;
        public Waypoint waypoint;
        public string[] objective;
        public override string ToString()
        {
            return $"Act:{act} Waypoint:{waypoint} Length:{objective.Length}";
        }
    }
    public struct Waypoint
    {
        public string name = string.Empty;
        public Act act = Act.None;
        public Waypoint(Act act, string name)
        {
            this.name = name;
            this.act = act;
        }
        public static Waypoint
            Rogue_Encampment = new Waypoint(Act.One, "Rogue Encampment"),
            Cold_Plains = new Waypoint(Act.One, "Cold Plains"),
            Stony_Field = new Waypoint(Act.One, "Stony Field"),
            Dark_Wood = new Waypoint(Act.One, "Dark Wood"),
            Black_Marsh = new Waypoint(Act.One, "Black Marsh"),
            Outer_Cloister = new Waypoint(Act.One, "Outer Cloister"),
            Jail_level_1 = new Waypoint(Act.One, "Jail level 1"),
            Inner_Cloister = new Waypoint(Act.One, "Inner Cloister"),
            Catacombs_level_2 = new Waypoint(Act.One, "Catacoms level 2"),

            Lut_Gholein = new Waypoint(Act.Two, "Lut Gholein"),
            Sewers_level_2 = new Waypoint(Act.Two, "Sewers level 2"),
            Dry_Hills = new Waypoint(Act.Two, "Dry Hills"),
            Halls_of_the_Dead_level_2 = new Waypoint(Act.Two, "Halls of the Dead level 2"),
            Far_Oasis = new Waypoint(Act.Two, "Far Oasis"),
            Lost_City = new Waypoint(Act.Two, "Lost City"),
            Palace_Cellar_level_1 = new Waypoint(Act.Two, "Palace Cellar level 1"),
            Arcane_Sanctuary = new Waypoint(Act.Two, "Arcane Sanctuary"),
            Canyon_of_the_Magi = new Waypoint(Act.Two, "Canyon of the Magi"),

            Kurast_Docks = new Waypoint(Act.Three, "Kurast Docks"),
            Spider_Forest = new Waypoint(Act.Three, "Spider Forest"),
            Great_Marsh = new Waypoint(Act.Three, "Great Marsh"),
            Flayer_Jungle = new Waypoint(Act.Three, "Flayer Jungle"),
            Lower_Kurast = new Waypoint(Act.Three, "Lower Kurast"),
            Kurast_Bazaar = new Waypoint(Act.Three, "Kurast Bazaar"),
            Upper_Kurast = new Waypoint(Act.Three, "Upper Kurast"),
            Travincal = new Waypoint(Act.Three, "Tranvincal"),
            Durance_of_Hate_level_2 = new Waypoint(Act.Three, "Durance of Hate level 2"),

            Pandemonium_Fortress = new Waypoint(Act.Four, "Pandemonium Fortress"),
            City_of_the_Damned = new Waypoint(Act.Four, "City of the Damned"),
            River_of_Flames = new Waypoint(Act.Four, "River of Flame"),

            Harrogath = new Waypoint(Act.Five, "Harrogath"),
            Frigid_Highlands = new Waypoint(Act.Five, "Frigid Highlands"),
            Arreat_Plateau = new Waypoint(Act.Five, "Arreat Plateau"),
            Crystalline_Passage = new Waypoint(Act.Five, "Crystalline Passage"),
            Halls_of_Pain = new Waypoint(Act.Five, "Halls of Pain"),
            Glacial_Trail = new Waypoint(Act.Five, "Glacial Trail"),
            Frozen_Tundra = new Waypoint(Act.Five, "Frozen Tundra"),
            The_Ancients_Way = new Waypoint(Act.Five, "The Ancients Way"),
            Worldstone_Keep_level_2 = new Waypoint(Act.Five, "Worldstone Keep level 2");
        public override string ToString()
        {
            return name;
        }
    }
    public enum Act : int
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }
}
