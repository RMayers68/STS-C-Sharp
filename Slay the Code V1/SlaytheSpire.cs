// Richard Mayers, April 20th, 2023
using static Global.Functions;
using ConsoleTableExt;
using STV;
using System.Diagnostics.Metrics;

namespace STV
{
    class STS
    {
        public static void Main()
        {
            Console.Title = "Slay The Spire";
            // Menu init
            int menuChoice = 1;
            List<string> mainMenu = new() { "1: Enter The Spire", "2: Card Library", "3: Exit" };

            //MAIN MENU
            while (menuChoice != 3)                                                     
            {
                Console.BackgroundColor = ConsoleColor.Black;
                ScreenWipe();
                Console.BackgroundColor = ConsoleColor.Black;
                ConsoleTableExt.ConsoleTableBuilder.From(mainMenu).ExportAndWriteLine(TableAligntment.Center);
                while (!Int32.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 4)
                    Console.WriteLine("Invalid input, enter again:");
                switch (menuChoice)
                {
                    case 1:      // PLAY                                                       
                        ScreenWipe();
                        int heroChoice = 0;
                        Console.WriteLine("What hero would you like to choose? Each comes with their own set of cards and playstyles:");
                        string[] characters = { "1: Ironclad", "2: Silent", "3: Defect", "4: Watcher" };
                        List<string> characterList = characters.ToList();
                        ConsoleTableExt.ConsoleTableBuilder.From(characterList).ExportAndWriteLine(TableAligntment.Center);
                        while (!Int32.TryParse(Console.ReadLine(), out heroChoice) || heroChoice < 1 || heroChoice > 4)
                            Console.WriteLine("Invalid input, enter again:");
                        Hero hero = new(Dict.heroL[heroChoice]);
                        switch (hero.Name)
                        {
                            case "Ironclad":
                                Console.BackgroundColor = ConsoleColor.DarkRed;                                
                                break;
                            case "Silent":
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                break;
                            case "Defect":
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                                hero.OrbSlots = 3;
                                break;
                            case "Watcher":
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                hero.Stance = "None";
                                break;
                        }
                        hero.Relics.Add(Dict.relicL[heroChoice - 1]);
                        ScreenWipe();
                        hero.StartingDeck();                 
                        Console.WriteLine($"You have chosen the {hero.Name}! Here is the {hero.Name} Deck.\n");                    
                        ConsoleTableExt.ConsoleTableBuilder.From(hero.Deck).ExportAndWriteLine(TableAligntment.Center);
                        Pause();                       
                        for (int act = 1; act <= 3; act++)
                        {
                            Console.WriteLine($"You have entered Act {act}!");
                            Pause();
                            List<Room> map = Room.MapGeneration();
                            List<Room> choices = new();                            
                            Room activeRoom = null;
                            for (int floor = 1; floor <= 16; floor++)
                            {
                                if (hero.FindRelic("Maw") is Relic mawBank && mawBank.IsActive)
                                    hero.GoldChange(12);
                                int roomNumber = 0;
                                Room.DrawMap(map);
                                if (floor == 1) // Choosing starting room
                                {
                                    choices = map.FindAll(x => x.Floor == 1);
                                    Console.WriteLine("\nWhat room would you like to enter?\n");
                                    foreach (Room r in choices)
                                    {
                                        Console.Write(r.ToString() + "\t");
                                    }                                   
                                    while (!Int32.TryParse(Console.ReadLine(), out roomNumber) || FindRoom(floor,roomNumber,choices) == null)
                                        Console.WriteLine("Invalid input, enter again:");
                                    activeRoom = new Room(FindRoom(floor, roomNumber, choices));
                                }
                                else if (floor == 16) // Entering the Boss Room
                                {
                                    Console.WriteLine("I hope you're ready to face the boss...");
                                    Pause();
                                    activeRoom = FindRoom(floor, 3, activeRoom.Connections);
                                }
                                else
                                {
                                    roomNumber = activeRoom.RoomNumber;
                                    Dictionary<string,int> directions = new();
                                    Console.WriteLine("\nWhat path would you like to go down?\n");
                                    for (int i = -1; i < 2; i++)
                                    {
                                        if (FindRoom(floor, activeRoom.RoomNumber + i, activeRoom.Connections) != null)
                                            switch (i) 
                                            {
                                                case -1:
                                                    directions.Add("L", roomNumber - 1);
                                                    Console.Write("(L)eft\t");
                                                    break;
                                                case 0:
                                                    directions.Add("M", roomNumber);
                                                    Console.Write("(M)iddle\t");
                                                    break;
                                                default:
                                                    directions.Add("R", roomNumber + 1);
                                                    Console.Write("(R)ight\t");
                                                    break;
                                            }
                                    }
                                    string directionChoice = Console.ReadLine().ToUpper();
                                    while (!directions.ContainsKey(directionChoice))
                                    {
                                        Console.WriteLine("Invalid input, enter again:");
                                        directionChoice = Console.ReadLine().ToUpper();
                                    }
                                    activeRoom = new Room(FindRoom(floor, directions[directionChoice],map));
                                }                               
                                
                                RoomDecider(hero,activeRoom,act*5-5);
                                if (hero.Hp <= 0)
                                    break;
                            }
                            if (hero.Hp <= 0)
                                break;
                        }                       
                        /*int debugCheck = Debug();                                // Debug Switch
                        if (debugCheck != 0)
                        {
                            hero.Hp = 9999;
                            hero.MaxHP = 9999;
                            hero.MaxEnergy = 9999;
                            for (int i = 0; i < Dict.potionL.Count; i++)
                                hero.Potions.Add(new Potion(Dict.potionL[i]));
                        }*/                                       
                        break;
                    case 2:                                                             // LIBRARY
                        ScreenWipe();
                        ConsoleTableBuilder.From(Dict.cardL.Values.ToList()).ExportAndWriteLine();
                        Pause();
                        break;
                }
            }
        }
        // Method to determine next event based on room
        public static void RoomDecider(Hero hero,Room activeRoom,int actModifier)
        { 
            switch (activeRoom.Location)
            {
                default: break;
                case "Monster":
                    if (hero.EasyFights < 3)
                    {
                        Battle.Combat(hero, Enemy.CreateEncounter(1 + actModifier), activeRoom);
                        hero.EasyFights++;
                    }
                    else Battle.Combat(hero, Enemy.CreateEncounter(2 + actModifier), activeRoom);
                    break;
                case "Event":
                    if (hero.FindRelic("Ssserpent") != null)
                        hero.GoldChange(50);
                    Event.EventDecider(hero, actModifier);
                    break;
                case "Rest Site":
                    if (hero.FindRelic("Eternal") != null)
                        hero.HealHP(3 * (hero.Deck.Count / 5));
                    int restChoice = -1;
                    while (restChoice != 0)
                    {
                        ScreenWipe();
                        Console.WriteLine($"Hello {hero.Name}! You have arrived at a campfire. What would you like to do? Enter your option.\n");
                        while (!Int32.TryParse(Console.ReadLine(), out restChoice) || restChoice < 0 || restChoice > 11)
                            Console.WriteLine("Invalid input, enter again:");
                        switch (restChoice)
                        {
                            default: // Rest
                                int regalPillow = 0;
                                if (hero.FindRelic("Regal") != null)
                                    regalPillow = 15;
                                hero.HealHP(Convert.ToInt32(hero.MaxHP * 0.3) + regalPillow);
                                if (hero.FindRelic("Dream") != null)
                                    hero.AddToDeck(Card.ChooseCard(Card.RandomCards(hero.Name, 3), "add to your Deck"));
                                break;
                            case 2: // Upgrade
                                if (hero.Deck.Any(x => !x.IsUpgraded()))
                                    Card.ChooseCard(hero.Deck.FindAll(x => !x.IsUpgraded()), "upgrade").UpgradeCard();
                                break;
                            case 3: // Lift
                                if (hero.FindRelic("Girya") is Relic girya && girya != null)
                                {
                                    girya.EffectAmount++;
                                    girya.PersistentCounter--;
                                }
                                break;
                            case 4: // Toke
                                if (hero.FindRelic("Peace") != null)
                                    hero.Deck.Remove(Card.ChooseCard(hero.Deck, "remove from your Deck"));
                                break;
                        }
                        Pause();
                    }
                    if (hero.FindRelic("Ancient") != null)
                        hero.GainTurnEnergy(2);
                    break;
                case "Merchant":
                    Shop.VisitShop(hero);
                    break;
                case "Treasure":
                    hero.GoldChange(100);
                    break;
                case "Elite":
                    if (hero.FindRelic("Sling") != null)
                        hero.AddBuff(4,2);
                    Battle.Combat(hero, Enemy.CreateEncounter(3 + actModifier), activeRoom);
                    break;
                case "Boss":
                    if (hero.FindRelic("Pantograph") != null)
                        hero.CombatHeal(25);
                    Battle.Combat(hero, Enemy.CreateEncounter(4 + actModifier), activeRoom);
                    break;
            }
        }      
    }
}

namespace Global
{
    class Functions
    {
        //MENU AND UI METHODS
        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...\n");
            Console.ReadKey();
        }

        public static string Tab(int i)
        {
            string tab = " ";
            for (int j = 0; j < i; j++)
                tab += "\t";
            return tab;
        }

        public static void ScreenWipe()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
        }

        public static Room FindRoom(int floor, int roomNumber, List<Room> list)
        { return list.Find(x => x.RoomNumber == roomNumber && x.Floor == floor); }

        public static bool Debug()
        {
            int debugChoice;
            while (!Int32.TryParse(Console.ReadLine(), out debugChoice))
                break;
            if (debugChoice == 2968)
                return true;
            else return false;
        }
    }
}