// Richard Mayers, April 21th, 2023
using static Global.Functions;
using ConsoleTableExt;
using STV;

namespace STV
{
    class STS
    {
        private static readonly Random RNG = new();

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
                        Play();
                        break;
                    case 2:                                                             // LIBRARY
                        ScreenWipe();
                        ConsoleTableBuilder.From(CardLibrary.ViewLibrary()).ExportAndWriteLine();
                        Pause();
                        break;
                }
            }
        }

        private static void Play()
        {
            ScreenWipe();
            int heroChoice = 0;
            Console.WriteLine("What hero would you like to choose? Each comes with their own set of cards and playstyles:");
            List<string> characters = new() { "1: Ironclad", "2: Silent", "3: Defect", "4: Watcher" };
            ConsoleTableExt.ConsoleTableBuilder.From(characters).ExportAndWriteLine(TableAligntment.Center);
            while (!Int32.TryParse(Console.ReadLine(), out heroChoice) || heroChoice < 1 || heroChoice > 4)
                Console.WriteLine("Invalid input, enter again:");
            Hero hero = new(Dict.heroL[heroChoice]);
            hero.AddToRelics(Dict.relicL[heroChoice - 1]);
            hero.StartingDeck();
            if (hero.Name == "Ironclad")
                Console.BackgroundColor = ConsoleColor.DarkRed;
            else if (hero.Name == "Silent")
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            else if (hero.Name == "Defect")
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                hero.OrbSlots = 3;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                hero.Stance = "None";
            }
            ScreenWipe();
            Console.WriteLine($"You have chosen the {hero.Name}! Here is the {hero.Name} Deck.\n");
            // Manual tests go here
            hero.GoldChange(999);
            ConsoleTableExt.ConsoleTableBuilder.From(hero.Deck).ExportAndWriteLine(TableAligntment.Center);
            Pause();
            for (int act = 1; act <= 3; act++)
            {
                //Initial RNG chances
                List<double> eventChances = new() { 0.02, 0.05, 0.9 };
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
                        while (!Int32.TryParse(Console.ReadLine(), out roomNumber) || FindRoom(floor, roomNumber, choices) == null)
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
                        Dictionary<string, int> directions = new();
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
                        activeRoom = new Room(FindRoom(floor, directions[directionChoice], map));
                    }
                    RoomDecider(hero, activeRoom, act * 5 - 5,eventChances);
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
        }

        // Method to determine next event based on room
        public static void RoomDecider(Hero hero,Room activeRoom,int actModifier, List<double> eventChances)
        { 
            switch (activeRoom.Location)
            {
                default: break;
                case "Monster":
                    if (hero.EasyFights < 3)
                    {
                        hero.Encounter = Enemy.CreateEncounter(1 + actModifier);
                        Battle.Combat(hero, hero.Encounter);
                        hero.EasyFights++;
                    }
                    else
                    {
                        hero.Encounter = Enemy.CreateEncounter(2 + actModifier);
                        Battle.Combat(hero, hero.Encounter);
                    }
                    hero.CombatRewards(activeRoom.Location);
                    break;
                case "Event":
                    if (hero.HasRelic("Ssserpent"))
                        hero.GoldChange(50);
                    double rng = RNG.NextDouble();
                    if (hero.FindRelic("Tiny Chest") is Relic chest && chest != null)
                    {
                        chest.PersistentCounter--;
                        if (chest.PersistentCounter == 0)
                        {
                            rng = 0.01;
                            chest.PersistentCounter = chest.EffectAmount;
                        }
                    }
                    if (rng < eventChances[0])
                    {
                        OpenChest(hero);
                        eventChances[0] = 0.00;
                    }
                    else if (rng < eventChances[1])
                    {
                        Shop.VisitShop(hero);
                        eventChances[1] = 0.00;
                    }
                    else if (!hero.HasRelic("Juzu") && rng > eventChances[2])
                    {
                        if (hero.EasyFights < 3)
                        {
                            Battle.Combat(hero, Enemy.CreateEncounter(1 + actModifier));
                            hero.EasyFights++;
                        }
                        else Battle.Combat(hero, Enemy.CreateEncounter(2 + actModifier));
                        hero.CombatRewards(activeRoom.Location);
                        eventChances[2] = 1.00;
                    }
                    else Event.EventDecider(hero, actModifier);
                    eventChances[0] += 0.02;
                    eventChances[1] += 0.05;
                    eventChances[2] -= 0.1;
                    break;
                case "Rest Site":
                    if (hero.HasRelic("Eternal"))
                        hero.HealHP(3 * (hero.Deck.Count / 5));
                    int restChoice = -1;
                    while (restChoice != 0)
                    {
                        ScreenWipe();
                        Console.WriteLine($"Hello {hero.Name}! You have arrived at a campfire. What would you like to do? Enter your option.\n");
                        Console.WriteLine("0: Leave (I would really only do this if I couldn't do anything else.");
                        Console.WriteLine("1: Rest (Unless you have too much coffee)\n");
                        Console.WriteLine("2: Upgrade (Unless you found a sweet hammer)\n");
                        Console.WriteLine("3: Lift (Only if you found some dumbells to train with)\n");
                        Console.WriteLine("4: Toke (Only if you found some good shiiiiit on the way up)\n");
                        Console.WriteLine("5: Dig (Only if you have something to dig with. Obviously.)\n");
                        while (!Int32.TryParse(Console.ReadLine(), out restChoice) || restChoice < 0 || restChoice > 5)
                            Console.WriteLine("Invalid input, enter again:");
                        switch (restChoice)
                        {
                            default:
                                Console.WriteLine("Okay, you're really just leaving? See ya bud.");
                                break;
                            case 1: // Rest
                                int regalPillow = 0;
                                if (hero.HasRelic("Regal"))
                                    regalPillow = 15;
                                if (hero.HasRelic("Coffee Dripper"))
                                    Console.WriteLine("You can't heal due to Coffee Dripper's effect.");
                                else
                                {
                                    hero.HealHP(Convert.ToInt32(hero.MaxHP * 0.3) + regalPillow);
                                    if (hero.HasRelic("Dream"))
                                        hero.AddToDeck(Card.PickCard(Card.RandomCards(hero.Name, 3), "add to your Deck"));
                                }
                                break;
                            case 2: // Upgrade
                                if (hero.HasRelic("Fusion"))
                                    Console.WriteLine("You can't upgrade a card due to Fusion Hammer's effect.");
                                if (hero.Deck.Any(x => !x.IsUpgraded()))
                                    Card.PickCard(hero.Deck.FindAll(x => !x.IsUpgraded()), "upgrade").UpgradeCard();
                                break;
                            case 3: // Lift
                                if (hero.FindRelic("Girya") is Relic girya && girya != null)
                                {
                                    girya.EffectAmount++;
                                    girya.PersistentCounter--;
                                }
                                break;
                            case 4: // Toke
                                if (hero.HasRelic("Peace"))
                                    hero.Deck.Remove(Card.PickCard(hero.Deck, "remove from your Deck"));
                                break;
                            case 5: //Dig
                                hero.AddToRelics(Relic.RandomRelic(hero.Name));
                                break;
                        }
                        restChoice = 0;
                        Pause();
                    }
                    if (hero.HasRelic("Ancient"))
                        hero.GainTurnEnergy(2);
                    break;
                case "Merchant":
                    Shop.VisitShop(hero);
                    break;
                case "Treasure":
                    OpenChest(hero);                   
                    break;
                case "Elite":
                    hero.Encounter = Enemy.CreateEncounter(3 + actModifier);
                    if (hero.HasRelic("Sling"))
                        hero.AddBuff(4,2);
                    if (hero.HasRelic("Slaver's"))
                    {
                        hero.MaxEnergy++;
                        Battle.Combat(hero, hero.Encounter);
                        hero.MaxEnergy--;
                    }
                    else Battle.Combat(hero, hero.Encounter);
                    hero.CombatRewards(activeRoom.Location);
                    break;
                case "Boss":
                    hero.Encounter = Enemy.CreateEncounter(4 + actModifier);
                    if (hero.HasRelic("Pantograph"))
                        hero.CombatHeal(25);
                    if (hero.HasRelic("Slaver's"))
                    {
                        hero.MaxEnergy++;
                        Battle.Combat(hero, hero.Encounter);
                        hero.MaxEnergy--;
                    }
                    else Battle.Combat(hero, hero.Encounter);
                    hero.CombatRewards(activeRoom.Location);
                    break;
            }
        }

        private static void OpenChest(Hero hero)
        {
            if (hero.FindRelic("Nloth's Hungry") is Relic nloth && nloth.IsActive)
            {
                Console.WriteLine("Nloth has consumed the chest's contents. Unfortunate.");
                nloth.IsActive = false;
            }
            else
            {
                hero.AddToRelics(Relic.RandomRelic(hero.Name));
                if (hero.FindRelic("Matryoshka") is Relic matroy && matroy.EffectAmount != 0)
                {
                    matroy.EffectAmount--;
                    hero.AddToRelics(Relic.RandomRelic(hero.Name));
                }
                if (RNG.Next(2) == 0)
                {
                    hero.GoldChange(RNG.Next(27, 69));
                }
            }
            Pause();
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