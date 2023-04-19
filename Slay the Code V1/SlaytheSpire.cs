﻿// Richard Mayers, Sept 11th, 2022

using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

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
                            List<Room> map = MapGeneration();
                            List<Room> choices = new();                            
                            Room activeRoom = null;
                            for (int floor = 1; floor <= 16; floor++)
                            {
                                int roomNumber = 0;
                                DrawMap(map);
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
                    if (hero.FindRelic("Ssserpent") != null)
                        hero.GoldChange(50);
                    EventDecider(hero, actModifier);
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
                        }
                        Pause();
                    }
                    if (hero.FindRelic("Tea Set") != null)
                        hero.GainTurnEnergy(2);
                    break;
                case "Merchant":
                    Shop(hero);
                    break;
                case "Treasure":
                    hero.GoldChange(100);
                    break;
                case "Elite":
                    encounter = CreateEncounter(3 + actModifier);
                    if (hero.FindRelic("Sling") != null)
                        hero.AddBuff(4,2);
                    Combat(hero, encounter,activeRoom);
                    break;
                case "Boss":
                    encounter = CreateEncounter(4 + actModifier);
                    if (hero.FindRelic("Pantograph") != null)
                        hero.CombatHeal(25);
                    Combat(hero,encounter,activeRoom);
                    break;
            }
        }

        private static void Shop(Hero hero)
        {
            if (hero.FindRelic("Ticket") != null)
                hero.HealHP(15);
            if (hero.FindRelic("Smiling") != null)
                hero.RemoveCost = 50;
            int shopChoice = -1;        
            Random shopRNG = new();
            List<Card> shopCards = new();
            List<Potion> shopPotions = new();
            for (int i = 0; i < 7; i++)
            {
                Card shopCard = new();
                while (shopCard.Type != "Attack" && i < 2)
                    shopCard = Card.RandomCards(hero.Name, 1, shopRNG)[0];
                while (shopCard.Type != "Skill" && i >= 2 && i < 4)
                    shopCard = Card.RandomCards(hero.Name, 1, shopRNG)[0];
                while (shopCard.Type != "Power" && i == 4)
                    shopCard = Card.RandomCards(hero.Name, 1, shopRNG)[0];
                while (shopCard.Rarity != "Uncommon" && i == 5)
                    shopCard = Card.RandomCards("Colorless", 1, shopRNG)[0];
                while (shopCard.Rarity != "Rare" && i == 6)
                    shopCard = Card.RandomCards("Colorless", 1, shopRNG)[0];
                shopCards.Add(shopCard); 
            }
            for (int i = 0; i < 3; i++)
            {
                Potion shopPotion = new(Dict.potionL[shopRNG.Next(Dict.potionL.Count)]);
                shopPotions.Add(shopPotion);
            }
            string removeCard = "Remove Card";            
            while (shopChoice != 0)
            {
                ScreenWipe();
                Console.WriteLine($"Hello {hero.Name}! You have {hero.Gold} Gold. What would you like to purchase? Enter your option or press 0 to leave.\n");
                Console.WriteLine("\nCards:\n*************************************");
                for (int i = 1; i <= 7; i++)
                    Console.WriteLine($"{i}: {shopCards[i-1].Name} {(shopCards[i-1].Name == "Purchased" ? "" : "- " + shopCards[i-1].GetGoldCost())}");
                Console.WriteLine("\nPotions:\n*************************************");
                for (int i = 8; i <= 10; i++)
                    Console.WriteLine($"{i}: {shopPotions[i - 8].Name} {(shopPotions[i - 8].Name == "Purchased" ? "" : "- " + shopPotions[i - 8].GoldCost)}");
                Console.WriteLine($"*************************************\n11: {removeCard} {(removeCard == "Remove Card" ? $"- {hero.RemoveCost}" : "")}");                  
                while (!Int32.TryParse(Console.ReadLine(), out shopChoice) || shopChoice < 0 || shopChoice > 11)
                    Console.WriteLine("Invalid input, enter again:");
                int newHeroGold;
                if (shopChoice > 0 && shopChoice < 8)
                {
                    Card shopCard = shopCards[shopChoice - 1];
                    if (shopCard.Name != "Purchased")
                    {
                        newHeroGold = hero.Gold - shopCard.GetGoldCost();
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.Deck.Add(shopCard);
                            Console.WriteLine($"You have purchased {shopCard.Name}");
                            shopCards[shopChoice - 1] = new Card();
                        }
                        else Console.WriteLine("You don't have enough Gold to buy this card.");
                    }
                    else Console.WriteLine("Card has already been purchased.");
                }
                else if (shopChoice > 7 && shopChoice < 11)
                {
                    Potion shopPotion = shopPotions[shopChoice - 8];
                    if (shopPotion.Name != "Purchased")
                    {
                        if (hero.Potions.Count <= hero.PotionSlots)
                        {
                            newHeroGold = hero.Gold - shopPotion.GoldCost;
                            if (newHeroGold >= 0)
                            {
                                hero.Gold = newHeroGold;
                                hero.Potions.Add(shopPotion);
                                Console.WriteLine($"You have purchased {shopPotion.Name}");
                                shopPotions[shopChoice - 8] = new Potion();
                            }
                            else Console.WriteLine("You don't have enough Gold to buy this potion.");
                        }
                        else Console.WriteLine("You don't have any empty potions slots.");
                    }
                    else Console.WriteLine("Potion has already been purchased.");
                }
                else if (shopChoice == 11)
                {
                    if (removeCard == "Remove Card")
                    {
                        newHeroGold = hero.Gold - hero.RemoveCost;
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.Deck.Remove(Card.ChooseCard(hero.Deck, "remove from your Deck"));
                            removeCard = "Removed";
                            if (hero.FindRelic("Smiling") == null)
                                hero.RemoveCost += 25;
                        }
                        else Console.WriteLine("You don't have enough Gold to remove a card.");
                    }
                    else Console.WriteLine("You have already removed a card.");
                }
                else break;
                Pause();
            }                       
        }

        public static void EventDecider(Hero hero, int actModifier) 
        {
            Random eventRNG;
            return;
        }

        //COMBAT METHODS
        public static void Combat(Hero hero, List<Enemy> encounter,Room activeRoom)
        {
            Random rng = new();
            ScreenWipe();
            Console.WriteLine("Next encounter:");
            foreach (Actor actor in encounter) 
                Console.WriteLine(actor.Name);
            foreach (Card c in hero.Deck)
                hero.DrawPile.Add(new Card(c));
            int turnNumber = 0;
            StartOfCombat(hero, encounter, rng);
            //Check HP values to end encounter when one group is reduced to 0
            while ((!encounter.All(x => x.Hp == 0)) && (hero.Hp != 0) && encounter.Count > 0)
            {
                // Modify buffs for hero
                for (int i = 0; i < hero.Buffs.Count; i++)                                          
                {
                    hero.Buffs[i].DurationDecrease();
                    if (hero.Buffs[i].DurationEnded())
                    {
                        Console.WriteLine($"\nYour {hero.Buffs[i].Name} {(hero.Buffs[i].BuffDebuff == true ? "Buff" : "Debuff")} has ran out.");
                        hero.Buffs.RemoveAt(i);
                        Pause();
                    }
                }
                // Same for enemy
                for (int i = 0; i < encounter.Count; i++)
                {
                    if (encounter[i].FindBuff("Ritual") is Buff ritual && ritual != null)
                        encounter[i].AddBuff(4, ritual.Intensity); 
                    for (int j = encounter[i].Buffs.Count - 1; j >= 0; j--)
                    {
                        encounter[i].Buffs[j].DurationDecrease();
                        if (encounter[i].Buffs[j].DurationEnded())
                        {
                            Console.WriteLine($"\nThe {encounter[i].Name}'s {encounter[i].Buffs[j].Name} {(encounter[i].Buffs[j].BuffDebuff == true ? "Buff" : "Debuff")} has ran out.");
                            encounter[i].Buffs.RemoveAt(j);
                        }                                                
                    }
                }
                turnNumber++;                                                        
                for (int i = 0; i < encounter.Count; i++)
                    encounter[i].SetEnemyIntent(turnNumber,encounter);
                Pause();


                // PLAYER TURN - MENU PLUS ALL ACTIONS LISTED
                if (turnNumber != 1)
                    hero.DrawCards(rng, 5);
                hero.Energy += hero.MaxEnergy;
                if (hero.Orbs.FindAll(x => x.Name == "Plasma").Count is int plasmaCount && plasmaCount > 0)
                    hero.Energy += plasmaCount;
                string playerChoice = "";
                while (playerChoice != "E" && (!encounter.All(x => x.Hp == 0)) && hero.Hp != 0)
                {
                    ScreenWipe();

                    //Menu creation
                    Console.WriteLine($"\tActions:{Tab(5)}TURN {turnNumber}{Tab(5)}Hand:\n********************{Tab(9)}********************\n");
                    for (int i = 1; i < 11; i++)
                    {
                        switch (i)
                        {
                            case 1:
                                Console.WriteLine($"(R)ead Card's Effects {(hero.Hand.Count >= i ? Tab(9) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 2:
                                Console.WriteLine($"(V)iew Your Buffs/Debuffs {(hero.Hand.Count >= i ? Tab(8) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 3:
                                Console.WriteLine($"Use (P)otion {(hero.Hand.Count >= i ? Tab(10) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 4:
                                Console.WriteLine($"View Enemy (I)nformation {(hero.Hand.Count >= i ? Tab(8) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 5:
                                Console.WriteLine($"(E)nd Turn {(hero.Hand.Count >= i ? Tab(10) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 6:
                                Console.WriteLine($"******************** {(hero.Hand.Count >= i ? Tab(9) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 7:
                                Console.WriteLine($"{hero.Name} Information: {(hero.Hand.Count >= i ? Tab(9) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 8:
                                Console.WriteLine($"Draw Pile:{hero.DrawPile.Count}\tDiscard Pile:{hero.DiscardPile.Count}{Tab(2)}Exhausted:{hero.ExhaustPile.Count} {(hero.Hand.Count >= i ? Tab(5) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 9:
                                Console.WriteLine($"HP:{hero.Hp}/{hero.MaxHP} + {hero.Block} Block\tEnergy:{hero.Energy}/{hero.MaxEnergy} {(hero.Hand.Count >= i ? Tab(7) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                            case 10:
                                Console.WriteLine($"{(hero.Hand.Count >= i ? Tab(11) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
                                break;
                        }
                    }
                    if (hero.Stance != null)
                        Console.WriteLine($"{hero.Name}'s Stance: {hero.Stance}");
                    if (hero.Orbs.Count > 0)
                    {
                        int orbNumber = 0;
                        Console.Write($"{hero.Name}'s Orbs:\n");
                        foreach (var orb in hero.Orbs) 
                            Console.Write($"{orbNumber++}: {orb.Name} - {orb.Effect}{Tab(2)}");
                    }

                    // Waiting for correct player choice
                    playerChoice = Console.ReadLine().ToUpper();

                    // Play Card function
                    if (Int32.TryParse(playerChoice, out int cardChoice) && cardChoice > 0 && cardChoice <= hero.Hand.Count)
                    {
                        hero.Hand[cardChoice - 1].CardAction(hero, encounter, rng);
                        HealthUnderZero(hero, encounter);
                        Pause();
                    }

                    // Other menu functions
                    else switch (playerChoice)
                        {
                            default:
                                Console.WriteLine("Invalid input, enter again:");
                                break;
                            case "R":
                                //ConsoleTableBuilder.From(hand).ExportAndWriteLine(TableAligntment.Center);
                                foreach (Card c in hero.Hand)
                                    Console.WriteLine(c.ToString());
                                Pause();
                                break;
                            case "B":
                                foreach (var buff in hero.Buffs)
                                {
                                    Console.WriteLine($"{buff.Name} - {buff.Description()}\n");
                                }
                                Pause();
                                break;
                            case "P":
                                int usePotion = 0;
                                for (int i = 0; i < hero.Potions.Count; i++)
                                    Console.WriteLine($"{i + 1}: {hero.Potions[i]}");
                                Console.WriteLine($"\nWhat potion would you like to use? Enter the number or enter 0 to choose another option.");
                                while (!Int32.TryParse(Console.ReadLine(), out usePotion) || usePotion < 0 || usePotion > hero.Potions.Count)
                                    Console.WriteLine("Invalid input, enter again:");
                                if (usePotion == 0)
                                    break;
                                hero.Potions[usePotion - 1].UsePotion(hero, encounter, rng);
                                HealthUnderZero(hero, encounter);
                                break;
                            case "I":                                                                             // Enemy Info Menu
                                ScreenWipe();
                                for (int i = 0; i < encounter.Count; i++)
                                {
                                    if (encounter[i].Hp == 0)
                                        continue;
                                    Console.WriteLine("************************************************************************\n");
                                    Console.WriteLine($"Enemy {i + 1}: {encounter[i].Name}{Tab(2)}HP:{encounter[i].Hp}/{encounter[i].MaxHP}{Tab(2)}Block:{encounter[i].Block}\n");
                                    Console.WriteLine($"Intent: {encounter[i].Intent}\n");
                                    Console.Write($"Buffs/Debuffs: ");
                                    if (encounter[i].Buffs.Count == 0)
                                        Console.Write("None\n");
                                    else for (int j = 0; j < encounter[i].Buffs.Count; j++)
                                            Console.WriteLine($"{encounter[i].Buffs[j].Name} - {encounter[i].Buffs[j].Description()}\n");
                                    Console.WriteLine("************************************************************************\n");
                                }
                                Pause();
                                break;
                            case "E":
                                HealthUnderZero(hero, encounter);
                                for (int i = 0; i < encounter.Count; i++)
                                {
                                    encounter[i].Block = 0;
                                }
                                if (hero.FindRelic("Ice") == null)
                                    hero.Energy = 0;
                                ScreenWipe();
                                break;
                        }
                }
                for (int i = hero.Hand.Count - 1; i >= 0; i--)                  
                {
                    if (hero.Hand[i].GetDescription().Contains("Retain."))
                    {
                        if (hero.Hand[i].Name == "Sands of Time" && hero.Hand[i].EnergyCost != 0)
                            hero.Hand[i].EnergyCost--;
                        if (hero.Hand[i].Name == "Windmill Strike")
                            hero.Hand[i].SetAttackDamage(hero.Hand[i].GetMagicNumber());
                        if (hero.Hand[i].Name == "Perserverance")
                            hero.Hand[i].SetBlockAmount(hero.Hand[i].GetMagicNumber());
                        continue;
                    }
                    else if (hero.Hand[i].GetDescription().Contains("Ethereal"))
                        hero.Hand[i].Exhaust(hero, hero.Hand);                   
                    else if (hero.FindRelic("Pyramid") != null)
                        continue;
                    else hero.Hand[i].MoveCard(hero.Hand, hero.DiscardPile);  //Discard at end of turn (Comment to find easy for disabling)
                }

                // END OF PLAYER AND START OF ENEMY TURN
                foreach (var orb in hero.Orbs)
                {
                    if(orb is null) continue;
                    switch (orb.Name)
                    {
                        case "Lightning":
                            int target = rng.Next(0, encounter.Count);
                            hero.NonAttackDamage(encounter[target], 3);
                            Console.WriteLine($"The {encounter[target].Name} took 3 damage from the Lightning Orb!");
                            break;
                        case "Frost":
                            hero.GainBlock(2);
                            Console.WriteLine($"The {hero.Name} gained 2 Block from the Frost Orb!");
                            break;
                        case "Dark":
                            orb.Effect += 6;
                            Console.WriteLine($"The {orb.Name} Orb stored 6 more Energy!");
                            break;
                    }
                }
                Console.WriteLine("Enemy's Turn!\n");
                for (int i = 0; i < encounter.Count; i++)
                {
                    if ((encounter[i].Hp != 0))
                    {
                        encounter[i].Actions.Add(encounter[i].Intent);
                        encounter[i].EnemyAction(hero, encounter);
                        HealthUnderZero(hero, encounter);
                    }
                }
                if (hero.FindBuff("Barricade") == null || hero.FindBuff("Blur") == null)
                {
                    if (hero.FindRelic("Calipers") != null)
                        hero.Block -= 15;
                    else hero.Block = 0;
                }
                    
            }

            // END OF COMBAT
            ScreenWipe();
            if (hero.Hp == 0)
                Console.WriteLine("\nYou were Defeated!\n");
            else
            {
                Console.WriteLine("\nVictorious, the creature is slain!\n");
                if (hero.Relics[0].Name == "Burning Blood")
                    hero.HealHP(6);
                hero.CombatRewards(hero.Deck,rng, activeRoom.Location);
            }                
            Pause();
        }

        private static void StartOfCombat(Hero hero, List<Enemy> encounter, Random rng)
        {
            // Relic check for Hero
            if (hero.FindRelic("Water") is Relic water && water != null)
                for (int i = 0; i < water.EffectAmount; i++)
                    hero.AddToHand(new Card(Dict.cardL[336]));
            if (hero.FindRelic("Mark of Pain") != null)
                for (int i = 0; i < 2; i++)
                    hero.DrawPile.Add(new(Dict.cardL[357]));
            if (hero.FindRelic("Ench") != null)
            {
                Card enchiridion = new(Card.RandomCards(hero.Name, 1, rng, "Power")[0]);
                enchiridion.SetTmpEnergyCost(0);
                hero.AddToHand(enchiridion);
            }
            if (hero.FindRelic("Runic Capacitor") != null)
                hero.OrbSlots += 2;
            if (hero.FindRelic("Cracked") != null)
                hero.ChannelOrb(encounter, 0);
            if (hero.FindRelic("Nuclear") != null)
                hero.ChannelOrb(encounter, 3);
            if (hero.FindRelic("Symbiotic") != null)
                hero.ChannelOrb(encounter, 2);
            if (hero.FindRelic("Vial") != null)
                hero.CombatHeal(2);
            if (hero.FindRelic("Clockwork") != null)
                hero.AddBuff(8, 1);
            if (hero.FindRelic("Akebeko") != null)
                hero.AddBuff(85, 8);
            if (hero.FindRelic("Anchor") != null)
                hero.GainBlock(10);
            if (hero.FindRelic("Marbles") != null)
                foreach (Enemy e in encounter)
                    e.AddBuff(1, 1, hero);
            if (hero.FindRelic("Red Mask") != null)
                foreach (Enemy e in encounter)
                    e.AddBuff(2, 1, hero);
            if (hero.FindRelic("Scales") != null)
                hero.AddBuff(41, 3);
            if (hero.FindRelic("Data") != null)
                hero.AddBuff(7, 1);
            if (hero.FindRelic("Lantern") != null)
                hero.GainTurnEnergy(1);
            if (hero.FindRelic("Fossil") != null)
                hero.AddBuff(56, 1);
            if (hero.FindRelic("Oddly") != null)
                hero.AddBuff(9, 1);
            if (hero.FindRelic("Vajra") != null)
                hero.AddBuff(4, 1);
            if (hero.FindRelic("Du-Vu") is Relic duvu && duvu != null)
                hero.AddBuff(4, duvu.PersistentCounter);
            if (hero.FindRelic("Girya") is Relic girya && girya != null)
                hero.AddBuff(4, girya.EffectAmount);
            if (hero.FindRelic("Thread") != null)
                hero.AddBuff(95, 4);
            if (hero.FindRelic("Gremlin Visage") != null)
                hero.AddBuff(2, 1);
            if (hero.FindRelic("Mutagenic") != null)
            {
                hero.AddBuff(4, 3);
                hero.AddBuff(30, 3);
            }
            if (hero.FindRelic("Preserved") != null)
                foreach (Enemy e in encounter)
                    e.Hp = Convert.ToInt32(e.Hp * 0.75);
            if (hero.FindRelic("Twisted") != null)
                foreach (Enemy e in encounter)
                    e.AddBuff(39, 4, hero);
            if (hero.FindRelic("Ninja") != null)
                Card.AddShivs(hero, 3);          
            if (hero.FindRelic("Tear") != null)
                hero.SwitchStance("Calm");
            if (hero.FindRelic("Neow") is Relic neow && neow.IsActive)
            {
                foreach (Enemy e in encounter)
                    e.Hp = 1;
                neow.PersistentCounter--;
                if (neow.PersistentCounter == neow.EffectAmount)
                    neow.IsActive = false;
            }
            // Enemy check

            hero.ShuffleDrawPile(rng);
            // Draw cards
            if (hero.FindRelic("Snecko Eye") != null)
            {
                hero.AddBuff(96, 1);
                hero.DrawCards(rng, 2);
            }              
            if (hero.FindRelic("Snake") != null)
                hero.DrawCards(rng, 2);
            else if (hero.FindRelic("Serpent") != null)
                hero.DrawCards(rng, 1);
            if (hero.FindRelic("Preparation") != null)
                hero.DrawCards(rng, 2);
            if (hero.FindRelic("Toolbox") != null)
                hero.AddToHand(new(Card.ChooseCard(Card.RandomCards("Colorless",3,rng), "add to your hand")));
            // Gambling Chip
            if (hero.FindRelic("Gambling") != null)
                GamblingIsGood(hero, rng);               
        }


        // HEALTH CHECK
        public static void HealthUnderZero(Hero hero, List<Enemy> encounter)                                      
        {
            Random rng = new();
            for (int i = encounter.Count - 1; i >= 0; i--)
                if (encounter[i].Hp <= 0)
                {
                    encounter[i].Hp = 0;
                    if (encounter.Count > 1 && hero.FindRelic("Specimen") != null && encounter[i].FindBuff("Poison") is Buff poison && poison != null)
                    {
                        int specimen = rng.Next(0, encounter.Count);
                        while (specimen == i)
                            specimen = rng.Next(0,encounter.Count);
                        encounter[specimen].AddBuff(39, poison.Intensity, hero);
                    }
                    if (hero.FindRelic("Gremlin Horn") != null)
                    {
                        hero.GainTurnEnergy(1);
                        hero.DrawCards(rng, 1);
                    }
                    encounter.RemoveAt(i);
                }
            if (hero.Hp <= 0)
            {
                if (hero.Potions.Find(x => x.Name.Contains("Fairy")) is Potion fairy && fairy != null)
                {
                    fairy.UsePotion(hero, encounter, rng);
                }
                else if (hero.FindRelic("Lizard") is Relic lizard && lizard.IsActive)
                {
                    hero.CombatHeal(hero.MaxHP / 2);
                    lizard.IsActive = false;
                }
                else hero.Hp = 0;
            }
        }

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

        // INITILIAZE METHODS
 
        public static List<Enemy> CreateEncounter(int list)
        {
            Random enemyRNG = new();
            List<Enemy> encounter = new();
            int encounterChoice;
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
                encounter[i].MaxHP = encounter[i].EnemyHealthSet();
                encounter[i].Hp = encounter[i].MaxHP;
                switch (encounter[i].EnemyID)
                {
                    default:
                        break;
                    case 2 or 7:
                        Random random = new();
                        encounter[i].AddBuff(5,random.Next(3, 8));
                        break;
                    case 11:
                        encounter[i].AddBuff(18, 15);
                        break;

                }
            }
            return encounter;
        }

        public static bool Debug()
        {
            int debugChoice;
            while (!Int32.TryParse(Console.ReadLine(), out debugChoice))
                break;
            if (debugChoice == 2968)
                return true;
            else return false;
        }

        public static List<Room> MapGeneration()
        {
            // Variable Init
            Random mapRng = new();
            Dictionary<int, List<Room>> MapTemplate = new();
            List<Room> paths = new();

            // Fills Map Template with 7*15 grid of Rooms
            for (int i = 1; i < 16; i++)
                MapTemplate[i] = new List<Room> { new(0, i), new(1, i), new(2, i), new(3, i), new(4, i), new(5, i), new(6, i) };

            // Runs 6 attempts at creating valid paths through Map       
            for (int i = 0; i < 6; i++)
            {

                // Grab starting node at random and move floor by floor to a valid room
                paths.Add(MapTemplate[1][mapRng.Next(0, 7)]);
                for (int j = 1; j < 15; j++)
                {
                    while (true)
                    {
                        try     // Prevents -1 or 7 index which would be out of range
                        {
                            Room tmp = new(MapTemplate[j + 1][mapRng.Next(paths.Last().RoomNumber - 1, paths.Last().RoomNumber + 2)]);
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
            List<Room> distinct = new();
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
                    Room bossRoom = new(3, 16, "Boss");
                    foreach (Room r in distinct.Where(x => x.Floor == i))
                    {
                        r.ChangeName("Rest Site");
                        r.Connections.Add(bossRoom);
                    }                       
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
                        choice = mapRng.Next(merchant);
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
            Console.WriteLine("\tBoss\t\n");
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
                    else break; //Right check on last room is impossible path, skip
                }                   
                Console.WriteLine();
            }
        }

        public static Room FindRoom(int floor, int roomNumber, List<Room> list)
        {  return list.Find(x => x.RoomNumber == roomNumber && x.Floor == floor); }

        public static void GamblingIsGood(Hero hero, Random rng)
        {
            int gambleChoice = -1;
            int gambleAmount = hero.Hand.Count;
            int gamble = 0;
            while (gambleChoice != 0 && gambleAmount > 0)
            {
                Console.WriteLine($"\nEnter the number of the card you would like to discard or hit 0 to move on.");
                for (int i = 1; i <= gambleAmount; i++)
                    Console.WriteLine($"{i}:{hero.DrawPile[hero.Hand.Count - i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out gambleChoice) || gambleChoice < 0 || gambleChoice > gambleAmount)
                    Console.WriteLine("Invalid input, enter again:");
                if (gambleChoice > 0)
                {
                    Card gambledCard = hero.Hand[^gambleChoice];
                    gambledCard.MoveCard(hero.Hand, hero.DiscardPile);
                    gambleAmount--;
                    gamble++;
                }
            }
            hero.DrawCards(rng, gamble);
        }
    }
}