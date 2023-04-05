﻿// Richard Mayers, Sept 11th, 2022

using ConsoleTableExt;
using System.Collections.Generic;

namespace STV
{
    class STS
    {
        public static void Main()
        {
            Console.Title = "Slay The Spire";  

            // Menu init
            int menuChoice = 1;
            List<String> mainMenu = new List<string> { "1: Enter The Spire", "2: Card Library", "3: Exit" };

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
                        Hero hero = new Hero(Dict.heroL[heroChoice]);
                        switch (hero.Name)
                        {
                            case "Ironclad":
                                hero.Relics.Add(new Relic(Dict.relicL[0]));
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                break;
                            case "Silent":
                                hero.Relics.Add(new Relic(Dict.relicL[1]));
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                break;
                            case "Defect":
                                hero.Relics.Add(new Relic(Dict.relicL[2]));
                                Console.BackgroundColor = ConsoleColor.DarkCyan;
                                hero.OrbSlots = 3;
                                break;
                            case "Watcher":
                                hero.Relics.Add(new Relic(Dict.relicL[3]));
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                hero.Stance = "None";
                                break;
                        }
                        ScreenWipe();
                        hero.Deck = CreateDeck(hero);
                        Console.WriteLine($"You have chosen the {hero.Name}! Here is the {hero.Name} Deck.\n");                    
                        ConsoleTableExt.ConsoleTableBuilder.From(hero.Deck).ExportAndWriteLine(TableAligntment.Center);
                        Pause();
                        
                        for (int act = 1; act <= 3; act++)
                        {
                            Console.WriteLine($"You have entered Act {act}!");
                            List<Room> map = MapGeneration();
                            List<Room> choices = new List<Room>();                            
                            Room activeRoom = null;
                            for (int floor = 1; floor <= 16; floor++)
                            {
                                int roomNumber = 0;
                                DrawMap(map);
                                if (floor == 1)
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
                            hero.Energy = 9999;
                            hero.MaxEnergy = 9999;
                            for (int i = 0; i < encounter.Count; i++)
                            {
                                encounter[i].Hp = 9999;
                                encounter[i].MaxHP = 9999;
                            }
                            for (int i = 0; i < Dict.potionL.Count; i++)
                                hero.Potions.Add(new Potion(Dict.potionL[i]));
                        }*/                 
                        
                        break;
                    case 2:                                                             // LIBRARY
                        ScreenWipe();
                        ConsoleTableBuilder.From(Dict.cardL.Values.ToList()).ExportAndWriteLine();
                        Pause();
                        break;
                    /*case 4:                                                       // Programming Test Case Area
                        for (int i = 0; i < 10; i++)
                            Console.WriteLine($"{(i-7) % 7}");
                        Pause();
                        break;*/
                }
            }
        }
        // Method to determine next event based on room
        public static void RoomDecider(Hero hero,Room activeRoom,int actModifier)
        { 
            List<Enemy> encounter = new();
            switch (activeRoom.Location)
            {
                default: break;
                case "Monster":
                    if (hero.EasyFights < 3)
                    {
                        encounter = CreateEncounter(1+actModifier);
                        hero.EasyFights++;
                    }
                    else encounter = CreateEncounter(2+actModifier);
                    Combat(hero, encounter,activeRoom);
                    break;
                case "Event":
                    EventDecider(hero);
                    break;
                case "Rest Site":
                    //Eventually add in Upgrade and Recall
                    hero.HealHP(hero.MaxHP / 3);
                    break;
                case "Merchant":
                    Shop(hero,rng);
                    break;
                case "Treasure":
                    hero.GoldChange(100);
                    break;
                case "Elite":
                    encounter = CreateEncounter(3 + actModifier);
                    Combat(hero, encounter,activeRoom);
                    break;
                case "Boss":
                    encounter = CreateEncounter(4 + actModifier);
                    Combat(hero, encounter,activeRoom);
                    break;
            }
        }

        private static void Shop(Hero hero)
        {
            int shopChoice = -1;        
            Random shopRNG = new Random();
            Dictionary<Card,int> shopList = new();
            Dictionary<Potion,int> shopList2 = new();
            for (int i = 0; i < 7; i++)
            {
                Card shopCard = new();
                while (shopCard.Type != "Attack" && i < 2)
                    shopCard = hero.RandomCards(hero.Name, 1, shopRNG)[0];
                while (shopCard.Type != "Skill" && i >= 2 && i < 4)
                    shopCard = hero.RandomCards(hero.Name, 1, shopRNG)[0];
                while (shopCard.Type != "Power" && i == 4)
                    shopCard = hero.RandomCards(hero.Name, 1, shopRNG)[0];
                while (shopCard.Rarity != "Uncommon" && i == 5)
                    shopCard = hero.RandomCards("Colorless", 1, shopRNG)[0];
                while (shopCard.Rarity != "Rare" && i == 6)
                    shopCard = hero.RandomCards("Colorless", 1, shopRNG)[0];
                shopList.Add(shopCard,shopCard.Rarity == "Rare" ? shopRNG.Next(135, 166) : shopCard.Rarity == "Uncommon" ? shopRNG.Next(68, 83) : shopRNG.Next(45, 56)); 
            }
            for (int i = 0; i < 3; i++)
            {
                Potion shopPotion = new Potion(Dict.potionL[shopRNG.Next(Dict.potionL.Count)]);
                shopList2.Add(shopPotion, shopPotion.Rarity == "Rare" ? shopRNG.Next(95, 106) : shopPotion.Rarity == "Uncommon" ? shopRNG.Next(72, 79) : shopRNG.Next(48, 53));
            }
            string removeCard = "Remove Card";            
            while (shopChoice != 0)
            {
                int choices = 1;
                List<Object> shoppingOptions = new();
                ScreenWipe();
                Console.WriteLine($"Hello {hero.Name}! You have {hero.Gold}. What would you like to purchase? Enter your option or press 0 to leave.");
                foreach(Card shopCard in shopList.Keys)
                {
                    Console.WriteLine($"{choices}: {shopCard.Name} - {shopList[shopCard]}");
                    choices++;
                    shoppingOptions.Add(shopCard);
                }
                foreach(Potion shopPotion in shopList2.Keys)
                {
                    Console.WriteLine($"{choices}: {shopPotion.Name} - {shopList2[shopPotion]}");
                    choices++;
                    shoppingOptions.Add(shopPotion);
                }
                if (removeCard == "Remove Card")
                {
                    Console.WriteLine($"{choices}: {removeCard} - 75");
                    shoppingOptions.Add(removeCard);
                }                    
                else choices--;
                while (!Int32.TryParse(Console.ReadLine(), out shopChoice) || shopChoice < 0 || shopChoice > choices)
                    Console.WriteLine("Invalid input, enter again:");
                //Add lines dealing with choice because I'm tired and need break
            }                       
        }

        public static void EventDecider(Hero hero) 
        {
            return;
        }

        //COMBAT METHODS
        public static void Combat(Hero hero, List<Enemy> encounter,Room activeRoom)
        {
            Random cardRNG = new();
            ScreenWipe();
            Console.WriteLine("Next encounter:");
            foreach (Actor actor in encounter) 
                Console.WriteLine(actor.Name);
            List<Card> drawPile = new(Shuffle(hero.Deck, cardRNG));
            List<Card> hand = new();
            List<Card> discardPile = new();
            List<Card> exhaustPile = new();
            int turnNumber = 0;

            //Check HP values to end encounter when one group is reduced to 0
            while ((!encounter.All(x => x.Hp == 0)) && (hero.Hp != 0) && encounter.Count > 0)
            {
                // Modify buffs for hero
                for (int i = 0; i < hero.Buffs.Count; i++)                                          
                {
                    hero.Buffs[i].DurationDecrease();
                    if (hero.Buffs[i].DurationEnded())
                    {
                        Console.WriteLine($"\nYour {hero.Buffs[i].Name} {hero.Buffs[i].BuffDebuff} has ran out.");
                        hero.Buffs.RemoveAt(i);
                        Pause();
                    }
                }
                // Same for enemy
                for (int i = 0; i < encounter.Count; i++)
                {
                    for (int j = encounter[i].Buffs.Count - 1; j >= 0; j--)
                    {
                        bool removeBuff = false;
                        encounter[i].Buffs[j].DurationDecrease();
                        if (encounter[i].Buffs[j].DurationEnded())
                        {
                            string buffDebuff = "";
                            if (encounter[i].Buffs[j].BuffDebuff)
                                buffDebuff = "Buff";
                            else buffDebuff = "Debuff";
                            Console.WriteLine($"\nThe {encounter[i].Name}'s {encounter[i].Buffs[j].Name} {buffDebuff} has ran out.");
                            removeBuff = true;
                        }
                        if (encounter[i].Buffs[j].Name == "Ritual")
                            encounter[i].AddBuff(4, Actor.FindBuff("Ritual", encounter[i].Buffs).Intensity.GetValueOrDefault(3)); // Adds Ritual Intensity to Strength
                        if (removeBuff)
                            encounter[i].Buffs.RemoveAt(j);
                    }
                }
                turnNumber++;                                                        
                for (int i = 0; i < encounter.Count; i++)
                    encounter[i].SetEnemyIntent(turnNumber,encounter);
                Pause();
                PlayerTurn(hero, encounter, drawPile, hand, discardPile,cardRNG,exhaustPile,turnNumber);

                // In between Player and Enemy Turn
                Random random = new Random();
                foreach (var orb in hero.Orbs)
                {
                    if(orb is null) continue;
                    switch (orb.OrbID)
                    {
                        case 0:
                            int target = random.Next(0, encounter.Count);
                            hero.NonAttackDamage(encounter[target], 3);
                            Console.WriteLine($"The {encounter[target].Name} took 3 damage from the Lightning Orb!");
                            break;
                        case 1:
                            hero.GainBlock(2);
                            Console.WriteLine($"The {hero.Name} gained 2 Block from the Frost Orb!");
                            break;
                        case 2:
                            orb.Effect += 6;
                            Console.WriteLine($"The {orb.Name} Orb stored 6 more Energy!");
                            break;
                    }
                }

                // Start of Enemy Turn
                Console.WriteLine("Enemy's Turn!\n");
                EnemyTurn(hero, encounter, drawPile, discardPile, hand);
            }
            ScreenWipe();
            if (hero.Hp == 0)
                Console.WriteLine("\nYou were Defeated!\n");
            else
            {
                Console.WriteLine("\nVictorious, the creature is slain!\n");
                if (hero.Relics[0].Name == "Burning Blood")
                    hero.HealHP(6);
                hero.CombatRewards(hero.Deck,cardRNG);
            }                
            Pause();
        }


        public static void PlayerTurn(Hero hero, List<Enemy> Encounter, List<Card> drawPile, List<Card> hand, List<Card> discardPile, Random rng, List<Card> exhaustPile, int turnNumber)
        {
            if (turnNumber == 1 && hero.Relics[0].Name == "Pure Water")
                hand.Add(new Card(Dict.cardL[336]));
            if (turnNumber == 1 && hero.Relics[0].Name == "Cracked Core")
                hero.Orbs.Add(new Orb(Dict.orbL[0]));
            if (turnNumber == 1 && hero.Relics[0].Name == "Ring of the Snake")
                DrawCards(drawPile, hand, discardPile, rng, 7);
            else DrawCards(drawPile, hand, discardPile, rng, 5);
            hero.Energy = hero.MaxEnergy;
            int playerChoice = 0;
            while (playerChoice != 5 && (!Encounter.All(x => x.Hp == 0)) && hero.Hp != 0)
            {
                ScreenWipe();

                CombatMenu(hero, Encounter, drawPile, hand, discardPile,exhaustPile,turnNumber);
                while (!Int32.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 1 || playerChoice > 5)
                    Console.WriteLine("Invalid input, enter again:");
                switch (playerChoice)
                {
                    case 1:
                        ConsoleTableBuilder.From(hand).ExportAndWriteLine(TableAligntment.Center);
                        /*Console.WriteLine("\nWhat card would you like to read? Enter the number or enter 0 to choose another option.\n");
                        int viewCard = 1;
                        while (viewCard != 0)
                        {
                            while (!Int32.TryParse(Console.ReadLine(), out viewCard) || viewCard < 0 || viewCard > hand.Count)
                                Console.WriteLine("Invalid input, enter again:");
                            if (viewCard == 0)
                                break;
                            Console.WriteLine("\n" + hand[viewCard - 1].String());
                        }*/
                        Pause();
                        break;
                    case 2:
                        int playCard = 1;
                        while (playCard != 0)
                        {
                            Console.WriteLine($"\nWhat card would you like to play? Enter the number or enter 0 to choose another option.");
                            while (!Int32.TryParse(Console.ReadLine(), out playCard) || playCard < 0 || playCard > hand.Count)
                                Console.WriteLine("Invalid input, enter again:");
                            if (playCard == 0)
                                break;
                            Card card = hand[playCard - 1];
                            if (card.EnergyCost == "X")
                                card.EnergyCost = $"{hero.Energy}";
                            else
                            {
                                card.UseCard(hero, Encounter, drawPile, discardPile, hand, exhaustPile, rng);
                                HealthUnderZero(hero, Encounter);
                            }
                            Pause();
                            break;
                        }
                        break;
                    case 3:
                        int usePotion = 0;
                        for (int i = 0; i < hero.Potions.Count; i++)
                            Console.WriteLine($"{i+1}: {hero.Potions[i].ToString()}");
                        Console.WriteLine($"\nWhat potion would you like to use? Enter the number or enter 0 to choose another option.");
                        while (!Int32.TryParse(Console.ReadLine(), out usePotion) || usePotion < 0 || usePotion > hero.Potions.Count)
                            Console.WriteLine("Invalid input, enter again:");
                        if (usePotion == 0)
                            break;
                        hero.Potions[usePotion-1].UsePotion(hero, Encounter, drawPile, discardPile, hand, exhaustPile, rng);
                        HealthUnderZero(hero,Encounter);
                        break;
                    case 4:                                                                             // Enemy Info Menu
                        ScreenWipe();
                        for (int i = 0; i < Encounter.Count; i++)
                        {
                            Console.WriteLine("************************************************************************\n");
                            Console.WriteLine($"Enemy {i + 1}: {Encounter[i].Name}{Tab(2)}HP:{Encounter[i].Hp}/{Encounter[i].MaxHP}{Tab(2)}Block:{Encounter[i].Block}\n");
                            Console.WriteLine($"Intent: {Encounter[i].Intent}\n");
                            Console.Write($"Buffs/Debuffs: ");
                            if (Encounter[i].Buffs.Count == 0)
                                Console.Write("None\n");
                            else for (int j = 0; j < Encounter[i].Buffs.Count; j++)
                                    Console.WriteLine($"{Encounter[i].Buffs[j].Name} - {Encounter[i].Buffs[j].Description()}\n");
                            Console.WriteLine("************************************************************************\n");
                        }
                        Pause();
                        break;
                    case 5:
                        HealthUnderZero(hero, Encounter);
                        for (int i = 0; i < Encounter.Count; i++)
                        {
                            Encounter[i].Block = 0;
                        }
                        ScreenWipe();
                        break;
                }

            }
            for (int i = hand.Count; i > 0; i--)                    //Discard at end of turn (Comment to find easy for disabling)
            {
                if (hand[i-1].Description.Contains("Retain."))
                {
                    hero.Actions.Add($"{hand[i-1].Name} Retained");
                    continue;
                }                   
                else
                {
                    if (hand[i-1].Description.Contains("Ethereal"))
                        hand[i-1].Exhaust(exhaustPile);
                    else discardPile.Add(hand[i-1]);
                    hand.RemoveAt(i-1);
                }
            }
        }
        public static void EnemyTurn(Hero hero, List<Enemy> Encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand)
        {
            for (int i = 0; i < Encounter.Count; i++)
            {
                if (!(Encounter[i].Hp == 0))
                {
                    Encounter[i].Actions.Add(Encounter[i].Intent);
                    Encounter[i].EnemyAction(hero, drawPile, discardPile, Encounter);
                    HealthUnderZero(hero, Encounter);
                }  
                else Console.WriteLine($"The {Encounter[i].Name} is dead and therefore... does nothing.");
            }
            hero.Block = 0;
        }

        // HEALTH CHECK
        public static void HealthUnderZero(Hero hero, List<Enemy> Encounter)                                      
        {
            for (int i = 0; i < Encounter.Count; i++)
                if (Encounter[i].Hp <= 0) Encounter[i].Hp = 0;
            if (hero.Hp <= 0) hero.Hp = 0;
        }

        // DECK METHODS

        public static List<Card> Shuffle(List<Card> Deck, Random rng)
        {
            int n = Deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = Deck[k];
                Deck[k] = Deck[n];
                Deck[n] = value;
            }
            return Deck;
        }

        public static void Discard2Draw(List<Card> drawPile, List<Card> discardPile, Random rng)
        {
            for (int i = discardPile.Count; i > 0; i--)
            {
                drawPile.Add(discardPile[i - 1]);
                discardPile.RemoveAt(i - 1);
            }
            Shuffle(drawPile, rng);
        }

        public static void DrawCards(List<Card> drawPile, List<Card> hand, List<Card> discardPile, Random rng, int cards)
        {
            while (hand.Count < 10)
            {
                if (drawPile.Count == 0)
                    Discard2Draw(drawPile, discardPile, rng);
                if (drawPile.Count == 0)
                    break;
                hand.Add(drawPile[drawPile.Count - 1]);
                drawPile.RemoveAt(drawPile.Count - 1);
                cards--;
                if (cards == 0)
                    return;
            }
        }

        public static Card ChooseCard(List<Card> list)
        {
            int cardChoice = 0;
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}:{list[i].Name}");
            Console.WriteLine("Which card would you like to choose?");
            while (!Int32.TryParse(Console.ReadLine(), out cardChoice) || cardChoice < 1 || cardChoice > list.Count)
                Console.WriteLine("Invalid input, enter again:");
            return list[cardChoice-1];
        }

        

        //MENU AND UI METHODS
        public static void CombatMenu(Hero hero, List<Enemy> Encounter, List<Card> drawPile, List<Card> hand, List<Card> discardPile, List<Card> exhaustPile, int turnNumber)
        {
            Console.WriteLine($"\tActions:{Tab(5)}TURN {turnNumber}{Tab(5)}Hand:\n********************{Tab(9)}********************\n");
            for (int i = 1; i < 11; i++)
            {
                switch (i)
                {
                    case 1:
                        if (hand.Count >= 1)
                            Console.WriteLine($"1: Read card's effects{Tab(9)}{i}:{hand[0].Name}\n");
                        else Console.WriteLine("1: Read card's effects\n");
                        break;
                    case 2:
                        if (hand.Count >= 2)
                            Console.WriteLine($"2: Play a card       {Tab(9)}{i}:{hand[1].Name}\n");
                        else Console.WriteLine("2: Play a card\n");
                        break;
                    case 3:
                        if (hand.Count >= 3)
                            Console.WriteLine($"3: Use a potion      {Tab(9)}{i}:{hand[2].Name}\n");
                        else Console.WriteLine("3: Use a potion\n");
                        break;
                    case 4:
                        if (hand.Count >= 4)
                            Console.WriteLine($"4: View enemy information{Tab(8)}{i}:{hand[3].Name}\n");
                        else Console.WriteLine("4: View enemy information\n");
                        break;
                    case 5:
                        if (hand.Count >= 5)
                            Console.WriteLine($"5: End your turn     {Tab(9)}{i}:{hand[4].Name}\n");
                        else Console.WriteLine("5: End your turn\n");
                        break;
                    case 6:
                        if (hand.Count >= 6)
                            Console.WriteLine($"********************{Tab(9)}{i}:{hand[5].Name}\n");
                        else Console.WriteLine("********************\n");
                        break;
                    case 7:
                        if (hand.Count >= 7)
                            Console.WriteLine($"{hero.Name} Information:{Tab(9)}{i}:{hand[6].Name}\n");
                        else Console.WriteLine($"{hero.Name} Information:\n");
                        break;
                    case 8:
                        if (hand.Count >= 8)
                            Console.WriteLine($"Draw Pile:{drawPile.Count}\tDiscard Pile:{discardPile.Count}{Tab(2)}Exhausted:{exhaustPile.Count}{Tab(5)}{i}:{hand[7].Name}\n");
                        else Console.WriteLine($"Draw Pile:{drawPile.Count}\tDiscard Pile:{discardPile.Count}\tExhausted:{exhaustPile.Count}\n");
                        break;
                    case 9:
                        if (hand.Count >= 9)
                            Console.WriteLine($"HP:{hero.Hp}/{hero.MaxHP} + {hero.Block} Block\tEnergy:{hero.Energy}/{hero.MaxEnergy} {Tab(7)}{i}:{hand[8].Name}\n");
                        else Console.WriteLine($"HP:{hero.Hp}/{hero.MaxHP} + {hero.Block} Block\tEnergy:{hero.Energy}/{hero.MaxEnergy}\n");
                        break;
                    case 10:
                        if (hand.Count >= 10)
                        {
                            if (hero.Buffs.Count == 0)
                                Console.WriteLine($"{hero.Name}'s Buffs/Debuffs: None{Tab(8)}{i}:{hand[9].Name}\n");
                            else Console.WriteLine($"{hero.Name}'s Buffs/Debuffs:{Tab(8)}{i}:{hand[9].Name}\n");
                        }
                        else if (hero.Buffs.Count == 0)
                            Console.WriteLine($"{hero.Name}'s Buffs/Debuffs: None\n");
                        else Console.WriteLine($"{hero.Name}'s Buffs/Debuffs:\n");
                        break;
                }
            }
            foreach (var buff in hero.Buffs)
            {
                Console.WriteLine($"{buff.BuffDebuff} - {buff.Name} - {buff.Description()}\n");
            }
            if (hero.Stance != null)
                Console.WriteLine($"{hero.Name}'s Stance: {hero.Stance}");
            if (hero.Orbs.Count > 0)
            {
                int orbNumber = 0;
                Console.Write($"{hero.Name}'s Orbs:\n");
                foreach (var orb in hero.Orbs)
                {
                    orbNumber++;
                    Console.Write($"{orbNumber}: {orb.Name} - {orb.Effect}{Tab(2)}");
                }
                    
            }

        }
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
        // INITILIAZE METHODS

        public static List<Card> CreateDeck(Actor hero)
        {
            List<Card> Deck = new();
            for (int i = 0; i < 8; i++)
            {
                while (i < 4)
                {
                    Deck.Add(Dict.cardL[220]);
                    i++;
                }
                while (i < 8)
                {
                    Deck.Add(Dict.cardL[219]);
                    i++;
                }
            }
            switch (hero.Name)
            {
                case "Ironclad":
                    Deck.Add(new Card(Dict.cardL[3]));
                    Deck.Add(new Card(Dict.cardL[220]));
                    break;
                case "Silent":
                    Deck.Add(new Card(Dict.cardL[220]));
                    Deck.Add(new Card(Dict.cardL[219]));
                    Deck.Add(new Card(Dict.cardL[121]));
                    Deck.Add(new Card(Dict.cardL[139]));
                    break;
                case "Defect":
                    Deck.Add(new Card(Dict.cardL[170]));
                    Deck.Add(new Card(Dict.cardL[214]));
                    break;
                case "Watcher":
                    Deck.Add(new Card(Dict.cardL[241]));
                    Deck.Add(new Card(Dict.cardL[285]));
                    break;
            }
            Deck.Sort(delegate (Card x, Card y)
            {
                if (x.Name == null && y.Name == null) return 0;
                else if (x.Name == null) return -1;
                else if (y.Name == null) return 1;
                else return x.Name.CompareTo(y.Name);
            });
            return Deck;

        }

        public static List<Enemy> CreateEncounter(int list)
        {
            Random enemyRNG = new Random();
            List<Enemy> encounter = new();
            int encounterChoice = 0;
            /* Looks at specific list (Encounter Type plus Act Modifier )
             * Encounter Types:
             * 1. Debut (first 3 normal combats each floor)
             * 2. Normal
             * 3. Elite
             * 4. Boss
             * 5. Event Combats
             * 
             * Level Modifier:
             * Act 1: 0
             * Act 2: 5
             * Act 3: 10
             * 
             * Example(Act 3 Elites): 10+3 = Case 13
             */
            switch (list)
            {
                default:       
                    encounterChoice = enemyRNG.Next(4);
                    switch (encounterChoice)
                    {
                        default:     //Cultist
                            encounter.Add(new Enemy(Dict.enemyL[0]));
                            break;
                        case 1:     //Jaw Worm
                            encounter.Add(new Enemy(Dict.enemyL[1]));
                            break;
                        case 2:     // 2 Louses (Red or Green)
                            for (int i = 0; i < 2; i++)
                                encounter.Add(new Enemy(Dict.enemyL[2+enemyRNG.Next(2)]));
                            break;
                        case 3:     // Small Slimes
                            if (enemyRNG.Next(2) == 0)
                            {
                                encounter.Add(new Enemy(Dict.enemyL[7]));
                                encounter.Add(new Enemy(Dict.enemyL[4]));
                            }
                            else
                            {
                                encounter.Add(new Enemy(Dict.enemyL[6]));
                                encounter.Add(new Enemy(Dict.enemyL[5]));
                            }
                            break;
                    }
                    break;
                case 2:
                    encounterChoice = enemyRNG.Next(0, 10);
                    switch (encounterChoice)
                    {
                        default:        // Gremlin Gang
                            for (int i = 0; i < 4; i++)
                            {
                                encounter.Add(new Enemy(Dict.enemyL[12+enemyRNG.Next(5)]));
                                break;
                            }
                            break;
                        case 1:         // Large Slime
                            if (enemyRNG.Next(2) == 0)
                                encounter.Add(new Enemy(Dict.enemyL[21]));
                            else
                                encounter.Add(new Enemy(Dict.enemyL[22]));
                            break;
                        case 2:         // Lots of Slimes
                            for (int i = 0; i < 3; i++)
                                encounter.Add(new Enemy(Dict.enemyL[6]));
                            for (int i = 0; i < 2; i++)
                                encounter.Add(new Enemy(Dict.enemyL[5]));
                            break;
                        case 3:         // Blue Slaver
                            encounter.Add(new Enemy(Dict.enemyL[8]));
                            break;
                        case 4:         // Red Slaver
                            encounter.Add(new Enemy(Dict.enemyL[9]));
                            break;
                        case 5:         // 3 Louses
                            for (int i = 0; i < 3; i++)
                            {
                                encounter.Add(new Enemy(Dict.enemyL[2 + enemyRNG.Next(2)]));
                            }
                            break;
                        case 6:         // Fungi Beasts
                            for (int i = 0; i < 2; i++)
                                encounter.Add(new Enemy(Dict.enemyL[10]));
                            break;
                        case 7:         // Exordium Thugs
                            encounter.Add(new Enemy(Dict.enemyL[2+enemyRNG.Next(4)]));
                            switch (enemyRNG.Next(4))
                            {
                                default:
                                    encounter.Add(new Enemy(Dict.enemyL[11]));
                                    break;
                                case 1:
                                    encounter.Add(new Enemy(Dict.enemyL[1]));
                                    break;
                                case 2:
                                    encounter.Add(new Enemy(Dict.enemyL[8]));
                                    break;
                                case 3:
                                    encounter.Add(new Enemy(Dict.enemyL[9]));
                                    break;
                            }
                            break;
                        case 8:         // Exordium Wildlife
                            if (enemyRNG.Next(2) == 0)
                                encounter.Add(new Enemy(Dict.enemyL[0]));
                            else encounter.Add(new Enemy(Dict.enemyL[10]));
                            encounter.Add(new Enemy(Dict.enemyL[2 + enemyRNG.Next(4)]));
                            break;
                        case 9:         //Looter
                            encounter.Add(new Enemy(Dict.enemyL[11]));
                            break;
                    }
                    break;
                case 3:
                    encounterChoice = enemyRNG.Next(3);
                    switch (encounterChoice)
                    {
                        default:     // Lagavulin
                            encounter.Add(new Enemy(Dict.enemyL[18]));
                            break;
                        case 1:     // Gremlin Nob
                            encounter.Add(new Enemy(Dict.enemyL[17]));
                            break;
                        case 2:     // 3 Sentries
                            for (int i = 0; i < 3; i++)
                                encounter.Add(new Enemy(Dict.enemyL[19]));
                            break;
                    }
                    break;
                case 4:
                    encounterChoice = enemyRNG.Next(3);
                    switch (encounterChoice)
                    {
                        default:     // Slime Boss
                            encounter.Add(new Enemy(Dict.enemyL[20]));
                            break;
                        case 1:     // The Guardian
                            encounter.Add(new Enemy(Dict.enemyL[23]));
                            break;
                        case 2:     // Hexaghost
                            for (int i = 0; i < 3; i++)
                                encounter.Add(new Enemy(Dict.enemyL[24]));
                            break;
                    }
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;

            }
            
            for (int i = 0; i < encounter.Count; i++)
            {
                encounter[i].MaxHP = encounter[i].EnemyHealthSet(encounter[i].BottomHP, encounter[i].TopHP);
                encounter[i].Hp = encounter[i].MaxHP;
                encounter[i].Hp = encounter[i].MaxHP;
                switch (encounter[i].EnemyID)
                {
                    default:
                        break;
                    case 2 or 7:
                        Random random = new Random();
                        encounter[i].AddBuff(5,random.Next(3, 8));
                        break;
                    case 11:
                        encounter[i].AddBuff(18, 15);
                        break;

                }
            }
            return encounter;
        }

        public static int Debug()
        {
            int debugChoice = 0;
            while (!Int32.TryParse(Console.ReadLine(), out debugChoice))
                break;
            if (debugChoice == 2968)
                return 1;
            else return 0;
        }

        public static List<Room> MapGeneration()
        {
            // Variable Init
            Random rng = new Random();
            Dictionary<int, List<Room>> MapTemplate = new();
            List<Room> paths = new List<Room>();

            // Fills Map Template with 7*15 grid of Rooms
            for (int i = 1; i < 16; i++)
                MapTemplate[i] = new List<Room> { new(0, i), new(1, i), new(2, i), new(3, i), new(4, i), new(5, i), new(6, i) };

            // Runs 6 attempts at creating valid paths through Map       
            for (int i = 0; i < 6; i++)
            {

                // Grab starting node at random and move floor by floor to a valid room
                paths.Add(MapTemplate[1][rng.Next(0, 7)]);
                for (int j = 1; j < 15; j++)
                {
                    while (true)
                    {
                        try     // Prevents -1 or 7 index which would be out of range
                        {
                            Room tmp = new Room(MapTemplate[j + 1][rng.Next(paths.Last().RoomNumber - 1, paths.Last().RoomNumber + 2)]);
                            tmp.Connections.Add(paths.Last());
                            paths.Last().Connections.Add(tmp);
                            paths.Add(tmp);
                            break;
                        }
                        catch (ArgumentOutOfRangeException) { continue; }  // This runs loop until valid room is found
                    }
                }
            }

            // Remove duplicate Rooms selected during the process and merge connections so no path is lost.
            List<Room> distinct = new List<Room>();
            foreach (Room room in paths)
            {
                if (!distinct.Contains(room))
                    distinct.Add(room);
                else
                {
                    distinct.Find(x => x.Equals(room)).Connections.AddRange(room.Connections);
                }
            }

            // Sort List by floors for assigning locations and visualization
            distinct = distinct.OrderBy(x => x.Floor).ThenBy(x => x.RoomNumber).ToList();
            // Assign locations to each room, starting on bottom floor
            for (int i = 1; i < 16; i++)
            {

                //Initial RNG chances
                int monster = 40, eVent = 65, elite = 83, restSite = 95, merchant = 101, choice = 0;

                // Next 3 conditionals are to set 1 floor of rooms to constant locations
                if (i == 1)
                {
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                        r.ChangeName("Monster");
                    continue;
                }
                else if (i == 9)
                {
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                        r.ChangeName("Treasure");
                    continue;
                }
                else if (i == 15)
                {
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                        r.ChangeName("Rest Site");
                    continue;
                }

                // Next 2 are to change RNG chances for certain floors that can not have some events (-1 odds set on those rooms to prevent them being selected)
                else if (i > 1 && i < 6)
                {
                    monster = 63;
                    eVent = 93;
                    elite = -1;
                    restSite = -1;
                }
                else if (i == 14)
                {
                    monster = 50;
                    eVent = 70;
                    elite = 94;
                    restSite = -1;
                }

                // For Floors not 1,9,15 : Roll random number until a valid location is selected
                foreach (Room r in distinct.Where(x => x.Floor == i))
                {
                    while (true)
                    {
                        choice = rng.Next(merchant);
                        if (choice < monster)
                            r.ChangeName("Monster");
                        else if (choice < eVent)
                            r.ChangeName("Event");
                        else if (choice < elite)
                        {
                            if (r.Connections.Find(x => x.Location == "Elite") == null)
                                r.ChangeName("Elite");
                        }
                        else if (choice < restSite)
                        {
                            if (r.Connections.Find(x => x.Location == "Rest Site") == null)
                                r.ChangeName("Rest Site");
                        }
                        else
                        {
                            if (r.Connections.Find(x => x.Location == "Merchant") == null)
                                r.ChangeName("Merchant");
                        }
                        if (r.Location != "Undecided")
                            break;
                    }
                }
            }
            return distinct;
        }

        public static void DrawMap(List<Room> map)
        {
            ScreenWipe();
            // Drawing the Map   
            Console.WriteLine("\t   Boss\t\n");
            for (int floor = 15; floor >= 1; floor--)
            {
                // Rules for drawing lines with rooms
                for (int roomNumber = 0; roomNumber < 7; roomNumber++)
                {
                    if (FindRoom(floor,roomNumber,map) == null)
                        Console.Write("    ");
                    else Console.Write(FindRoom(floor, roomNumber, map).ShortHand + "   ");
                }
                Console.WriteLine();
                // Rules for drawing paths inbetween Rooms
                for (int roomNumber = 0; roomNumber < 7; roomNumber++)
                {
                    Room currentRoom = FindRoom(floor, roomNumber, map), nextRoom = FindRoom(floor, roomNumber + 1, map);
                    bool currentExists = currentRoom != null, nextExists = nextRoom != null;

                    //Null move position to next to check
                    if (!currentExists)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        // Middle Check
                        if (FindRoom(floor - 1, roomNumber, currentRoom.Connections) == null)
                            Console.Write("  ");
                        else
                            Console.Write("| ");
                    }
                            
                    // Diagonal Checks
                    if (roomNumber != 6)
                    {
                        bool leftPathExists = false, rightPathExists = false;
                        if (nextExists)
                            leftPathExists = FindRoom(floor - 1, roomNumber, nextRoom.Connections) != null;
                        if (currentExists)
                            rightPathExists = FindRoom(floor - 1, roomNumber + 1, currentRoom.Connections) != null;
                        switch (rightPathExists, leftPathExists)
                        {
                            default:
                                Console.Write("  ");
                                break;
                            case (true, true):
                                Console.Write("X ");
                                break;
                            case (true, false):
                                Console.Write("\\ ");
                                break;
                            case (false, true):
                                Console.Write("/ ");
                                break;
                        }

                    }
                    else; //Right check on last room is impossible path, skip
                }                   
                Console.WriteLine();
            }
        }

        public static Room FindRoom(int floor, int roomNumber, List<Room> list)
        {
            Room room = list.Find(x => x.RoomNumber == roomNumber && x.Floor == floor);
            return room;
        }
    }
}


