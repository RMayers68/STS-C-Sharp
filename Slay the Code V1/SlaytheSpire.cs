// Richard Mayers, Sept 11th, 2022

namespace STV
{
    class STS
    {
        public static void Main()
        {
            int menuChoice = 1;
            while (menuChoice != 3)                                                     //MAIN MENU
            {
                Console.Clear();
                Console.WriteLine($"{Tab(6)}Slay The Spire!\n\n{Tab(6)}****************\n\n{Tab(6)}1: Play\n\n{Tab(6)}2: Card Library\n\n{Tab(6)}3: Exit");
                while (!Int32.TryParse(Console.ReadLine(), out menuChoice) || menuChoice < 1 || menuChoice > 4)
                    Console.WriteLine("Invalid input, enter again:");
                switch (menuChoice)
                {
                    case 1:                                                             // PLAY
                        Console.Clear();
                        int heroChoice = 0;
                        Console.WriteLine("What hero would you like to choose? Each comes with their own set of cards and playstyles:");
                        Console.WriteLine("1: Ironclad\n2: Silent\n3: Defect\n4: Watcher");
                        while (!Int32.TryParse(Console.ReadLine(), out heroChoice) || heroChoice < 1 || heroChoice > 4)
                            Console.WriteLine("Invalid input, enter again:");
                        Actor hero = new Actor(Dict.heroL[heroChoice]);
                        switch (hero.Name)
                        {
                            case "Ironclad":
                                hero.Relics.Add(new Relic(Dict.relicL[0]));
                                break;
                            case "Silent":
                                hero.Relics.Add(new Relic(Dict.relicL[1]));
                                break;
                            case "Defect":
                                hero.Relics.Add(new Relic(Dict.relicL[2]));
                                hero.OrbSlots = 3;
                                break;
                            case "Watcher":
                                hero.Relics.Add(new Relic(Dict.relicL[3]));
                                hero.Stance = "None";
                                break;
                        }
                        List<Card> Deck = CreateDeck(hero);
                        Console.WriteLine($"You have chosen the {hero.Name}! Here is the {hero.Name} Deck.\n");
                        int x = 1;
                        foreach (var (item, index) in Deck.Select((value, i) => (value, i)))
                        {
                            Console.Write(x + ":" + Deck[index].Name + "      ");
                            x++;
                            if (x % 6 == 0)
                                Console.Write("\n\n");
                        }
                        Pause();
                        List<Actor> encounter = EncounterSet();
                        int debugCheck = Debug();
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
                        }
                        for (int i = 0;i < Dict.potionL.Count; i++)
                            hero.Potions.Add(new Potion(Dict.potionL[i]));
                        Combat(hero, encounter, Deck);
                        break;
                    case 2:                                                             // LIBRARY
                        Console.Clear();
                        Console.WriteLine("What card would you like to look at? Enter the number or enter 0 to leave.\n");
                        int y = 1;
                        foreach (var (item, index) in Dict.cardL.Select((value, i) => (value, i)))
                        {
                            Console.Write(y + ":" + Dict.cardL[index].Name + "      ");
                            y++;
                            if (y % 6 == 0)
                                Console.Write("\n\n");
                        }

                        int viewCard = 1;
                        while (viewCard != 0)
                        {
                            while (!Int32.TryParse(Console.ReadLine(), out viewCard) || viewCard < 0 || viewCard > 349)
                                Console.WriteLine("Invalid input, enter again:");
                            if (viewCard == 0)
                                break;
                            Console.WriteLine("\n" + Dict.cardL[viewCard - 1].String());
                        }
                        Console.Clear();
                        break;
                    /*case 4:                                                       // Programming Test Case Area
                        for (int i = 0; i < 10; i++)
                            Console.WriteLine($"{(i-7) % 7}");
                        Pause();
                        break;*/
                }
            }
        }

        //COMBAT METHODS
        public static void Combat(Actor hero, List<Actor> encounter, List<Card> Deck)
        {
            Random cardRNG = new();
            Console.Clear();
            switch (encounter.Count)
            {
                default: break;
                case 1:
                    Console.WriteLine($"You have encountered a {encounter[0].Name}!");
                    break;
                case 2:
                    if (encounter[0].Name == encounter[1].Name)
                        Console.WriteLine($"You have encountered 2 {encounter[0].Name}s!");
                    else Console.WriteLine($"You have encountered a {encounter[0].Name} and a {encounter[1].Name}!");
                    break;
            }
            List<Card> drawPile = Shuffle(Deck, cardRNG);
            List<Card> hand = new();
            List<Card> discardPile = new();
            List<Card> exhaustPile = new();
            int turnNumber = 0;
            while ((!encounter.All(x => x.Hp == 0)) && (hero.Hp != 0) && encounter.Count > 0)
            {
                for (int i = 0; i < hero.Buffs.Count; i++)                                          //Start Turn Setup
                {
                    hero.Buffs[i].DurationDecrease();
                    if (hero.Buffs[i].DurationEnded())
                    {
                        Console.WriteLine($"\nYour {hero.Buffs[i].Name} {hero.Buffs[i].BuffDebuff} has ran out.");
                        hero.Buffs.RemoveAt(i);
                        Pause();
                    }
                }
                foreach(Orb orb in hero.Orbs)
                {
                    if (orb.Name == "Plasma")
                        hero.GainEnergy(1);
                }
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
                        if (encounter[i].Buffs[j].Name == "Ritual" && turnNumber != 1)
                            encounter[i].AddBuff(4,encounter[i].Buffs.Find(x => x.Name.Equals("Ritual")).Intensity.GetValueOrDefault(3)); // Adds Ritual Intensity to Strength
                        if (removeBuff)
                            encounter[i].Buffs.RemoveAt(j);
                    }
                }
                turnNumber++;                                                                       // End Turn Setup
                for (int i = 0; i < encounter.Count; i++)
                    encounter[i].EnemyIntent(turnNumber,encounter);
                Pause();
                PlayerTurn(hero, encounter, drawPile, hand, discardPile,cardRNG,exhaustPile,turnNumber);
                if (hero.Orbs.Count != null || hero.Orbs.Count != 0)                                //Start Orb Time
                {
                    Random random = new Random();
                    int focus = 0;
                    if (hero.Buffs.Exists(x => x.Name == "Focus"))
                        focus = hero.Buffs.Find(x => x.Name == "Focus").Intensity.GetValueOrDefault();
                    foreach (var orb in hero.Orbs)
                    {
                        if (hero.Buffs.Exists(x => x.Name == "Focus"))
                            focus = hero.Buffs.Find(x => x.Name == "Focus").Intensity.GetValueOrDefault();
                        switch (orb.OrbID)
                        {
                            case 0:
                                int target = random.Next(0, encounter.Count);
                                focus += 3;
                                hero.NonAttackDamage(encounter[target], focus);
                                Console.WriteLine($"The {encounter[target].Name} took {focus} damage from the Lightning Orb!");
                                break;
                            case 1:
                                focus += 2;
                                hero.GainBlock(focus);
                                Console.WriteLine($"The {hero.Name} gained {focus} Block from the Frost Orb!");
                                break;
                            case 2:
                                orb.Effect += focus+6;
                                Console.WriteLine($"The {orb.Name} Orb stored {focus+6} more Power!");
                                break;
                        }
                    }
                }                                                                                     // End Orb Time
                Console.WriteLine("Enemy's Turn!\n");
                EnemyTurn(hero, encounter, drawPile, discardPile, hand);
            }
            Console.Clear();
            if (hero.Hp == 0)
                Console.WriteLine("\nYou were Defeated!\n");
            else
            {
                Console.WriteLine("\nVictorious, the creature is slain!\n");
                if (hero.Relics[0].Name == "Burning Blood")
                    hero.HealHP(6);
                if (encounter.Exists(x => x.Name == "Looter"))                                        // Taking Gold back from Looters
                {
                    foreach(var looter in encounter.FindAll(x => x.Name == "Looter"))
                        hero.Gold += looter.Gold;
                }
            }                
            Pause();
        }
        public static void PlayerTurn(Actor hero, List<Actor> encounter, List<Card> drawPile, List<Card> hand, List<Card> discardPile, Random rng, List<Card> exhaustPile, int turnNumber)
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
            while (playerChoice != 5 && (!encounter.All(x => x.Hp == 0)) && hero.Hp != 0)
            {
                Console.Clear();

                CombatMenu(hero, encounter, drawPile, hand, discardPile,exhaustPile,turnNumber);
                while (!Int32.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 1 || playerChoice > 5)
                    Console.WriteLine("Invalid input, enter again:");
                switch (playerChoice)
                {
                    case 1:
                        Console.WriteLine("\nWhat card would you like to read? Enter the number or enter 0 to choose another option.\n");
                        int viewCard = 1;
                        while (viewCard != 0)
                        {
                            while (!Int32.TryParse(Console.ReadLine(), out viewCard) || viewCard < 0 || viewCard > hand.Count)
                                Console.WriteLine("Invalid input, enter again:");
                            if (viewCard == 0)
                                break;
                            Console.WriteLine("\n" + hand[viewCard - 1].String());
                        }
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
                            if (Int32.Parse(card.EnergyCost) > hero.Energy)
                                Console.WriteLine($"You failed to play the card. You need {card.EnergyCost} to play {card.Name}.");
                            else if (card.Type == "Attack" && hero.Buffs.Exists(x => x.Name == "Entangled"))
                                Console.WriteLine($"You are Entangled and can't play an Attack!");
                            else
                            {
                                hand.Remove(card);
                                hero.Energy -= (Int32.Parse(card.EnergyCost));
                                card.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                                if (card.Type == "Skill" && encounter[0].Buffs.Exists(x => x.Name == "Enrage"))             //Gremlin Nob Enrage Check
                                    encounter[0].AddBuff(4, 2);
                                HealthUnderZero(hero, encounter);
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
                        hero.Potions[usePotion-1].UsePotion(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                        HealthUnderZero(hero,encounter);
                        break;
                    case 4:                                                                             // Enemy Info Menu
                        Console.Clear();
                        for (int i = 0; i < encounter.Count; i++)
                        {
                            Console.WriteLine("********************\n");
                            Console.WriteLine($"Enemy {i + 1}: {encounter[i].Name}{Tab(3)}HP:{encounter[i].Hp}/{encounter[i].MaxHP}{Tab(3)}Block:{encounter[i].Block}\n");
                            Console.WriteLine($"Intent:{encounter[i].Intent}\n");
                            Console.Write($"Buffs/Debuffs: ");
                            if (encounter[i].Buffs.Count == 0)
                                Console.Write("None\n");
                            else for (int j = 0; j < encounter[i].Buffs.Count; j++)
                                    Console.WriteLine($"{encounter[i].Buffs[j].Name} - {encounter[i].Buffs[j].Description()}\n");
                            Console.WriteLine("\n********************");
                        }
                        Pause();
                        break;
                    case 5:
                        
                        if (hero.Buffs.Exists(x => x.Name == "Regeneration"))
                            hero.HealHP(hero.Buffs.Find(x => x.Name == "Regeneration").Duration.GetValueOrDefault());
                        if (hero.Buffs.Exists(x => x.Name == "Metallicize"))
                            hero.GainBlock(hero.Buffs.Find(x => x.Name == "Metallicize").Intensity.GetValueOrDefault());
                        if (hero.Buffs.Exists(x => x.Name == "Plated Armor"))
                            hero.GainBlock(hero.Buffs.Find(x => x.Name == "Plated Armor").Intensity.GetValueOrDefault());
                        if (hero.Buffs.Exists(x => x.Name == "Strength Down"))
                        {
                            hero.AddBuff(4, -hero.Buffs.Find(x => x.Name == "Strength Down").Intensity.Value);
                            hero.Buffs.RemoveAll(x => x.Name == "Strength Down");
                        }
                        if (hero.Buffs.Exists(x => x.Name == "Dexterity Down"))
                        {
                            hero.AddBuff(9, -hero.Buffs.Find(x => x.Name == "Dexterity Down").Intensity.Value);
                            hero.Buffs.RemoveAll(x => x.Name == "Dexterity Down");
                        }
                        for (int i = 0; i < encounter.Count; i++)
                        {
                            encounter[i].Block = 0;
                        }
                        foreach (var enemy in encounter.FindAll(x => x.Buffs.Exists(y => y.Name == "Poison")))
                            enemy.PoisonDamage();
                        HealthUnderZero(hero, encounter);
                        Console.Clear();
                        break;
                }

            }
            for (int i = hand.Count; i > 0; i--)                    //Discard at end of turn (Comment to find easy for disabling)
            {
                if (hand[i-1].Description.Contains("Retain."))
                    break;
                else
                {
                    if (hand[i-1].Description.Contains("Ethereal"))
                        hand[i-1].Exhaust(exhaustPile);
                    else discardPile.Add(hand[i-1]);
                    hand.RemoveAt(i-1);
                }
            }
        }
        public static void EnemyTurn(Actor hero, List<Actor> encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand)
        {
            for (int i = 0; i < encounter.Count; i++)
            {
                if (encounter[i].Hp == 0)
                    Console.WriteLine($"The {encounter[i].Name} is dead and therefore... does nothing.");
                else
                {
                    encounter[i].Actions.Add(encounter[i].Intent);
                    encounter[i].EnemyAction(hero,drawPile, discardPile,encounter);
                    HealthUnderZero(hero, encounter);
                    if (encounter[i].Buffs.Exists(x => x.Name == "Metallicize"))
                        encounter[i].GainBlock(hero.Buffs.Find(x => x.Name == "Metallicize").Intensity.GetValueOrDefault());
                }
            }

            hero.Block = 0;
        }

        // HEALTH CHECK
        public static void HealthUnderZero(Actor hero, List<Actor> encounter)                                      
        {
            for (int i = 0; i < encounter.Count; i++)
            {
                if (encounter[i].Hp <= 0) 
                    encounter[i].Hp = 0;
                if (encounter[i].Buffs.Exists(x => x.Name == "Spore Cloud") && encounter[i].Hp == 0)
                {
                    hero.AddBuff(1, 2);
                    encounter[i].Buffs.RemoveAll(x => x.Name == "Spore Cloud");
                }                  
            }
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
            int cardsDrew = 0;
            while (hand.Count < 10)
            {
                if (drawPile.Count == 0)
                    Discard2Draw(drawPile, discardPile, rng);
                if (drawPile.Count == 0)
                    break;
                int n = drawPile.Count;
                hand.Add(drawPile[n - 1]);
                drawPile.RemoveAt(n - 1);
                cardsDrew++;
                if (cardsDrew == cards)
                    break;
            }
        }

        public static void Discard(List<Card> hand, List<Card> discardPile, Card card)
        {
            if (!hand.Any())
                return;
            discardPile.Add(card);
            hand.Remove(card);
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
        //BUFF METHODS
        public static void DivinityCheck(Actor hero, List<Card>discardPile,List<Card> hand)
        {
            if (!hero.Buffs.Exists(x => x.Name == "Mantra"))
                return;
            if (hero.Buffs.Find(x => x.Name == "Mantra").Counter > 10)
            {
                hero.Buffs.Find(x => x.Name == "Mantra").CounterSet(-10);
                hero.Buffs.Find(x => x.Name == "Mantra").IntensitySet(10);
                hero.SwitchStance("Divinity", discardPile, hand);
            }
        }

        //MENU AND UI METHODS
        public static void CombatMenu(Actor hero, List<Actor> Encounter, List<Card> drawPile, List<Card> hand, List<Card> discardPile, List<Card> exhaustPile, int turnNumber)
        {
            Console.WriteLine($"\tActions:{Tab(5)}TURN {turnNumber}{Tab(4)}Hand:\n********************{Tab(9)}********************\n");
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
            string tab = "";
            for (int j = 0; j < i; j++)
                tab += "\t";
            return tab;
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
        public static List<Actor> EncounterSet()
        {
            Random enemyRNG = new Random();
            List<Actor> list = new();
            int encounterChoice = enemyRNG.Next(0, 4);
            switch (encounterChoice)
            {
                case 0:                                                     // Jaw Worm
                    list.Add(new Actor(Dict.enemyL[0]));
                    break;
                case 1:                                                     // Cultist
                    list.Add(new Actor(Dict.enemyL[1]));
                    break;
                case 2:                                                     // Two Louses
                    for (int i = 0; i < 2; i++)
                    { 
                        if (enemyRNG.Next(0,2) == 0)
                            list.Add(new Actor(Dict.enemyL[2]));
                        else list.Add(new Actor(Dict.enemyL[7]));
                    }                   
                    break;
                case 3:                                                     // 1 Small Slime and 1 Med Slime
                    int slimeSwitch = enemyRNG.Next(0, 2);
                    switch (slimeSwitch)
                    {
                        case 0:
                            list.Add(new Actor(Dict.enemyL[6]));
                            list.Add(new Actor(Dict.enemyL[3]));
                            break;
                        case 1:
                            list.Add(new Actor(Dict.enemyL[5]));
                            list.Add(new Actor(Dict.enemyL[4]));
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:                                                     // Looter
                    list.Add(new Actor(Dict.enemyL[11]));
                    break;
                case 5:                                                     //Lots of Slimes
                    for (int i = 0;i < 5;i++)
                    {
                        if (i < 3)
                            list.Add(new Actor(Dict.enemyL[6]));
                        else list.Add(new Actor(Dict.enemyL[5]));
                    }
                    break;
                case 6:                                                     // Large Slime
                    if (enemyRNG.Next(0, 2) == 0)
                        list.Add(new Actor(Dict.enemyL[20]));
                    else list.Add(new Actor(Dict.enemyL[21]));
                    break;
                case 7:                                                     // 2 Fungi Beasts
                    list.Add(new Actor(Dict.enemyL[10]));
                    list.Add(new Actor(Dict.enemyL[10]));
                    break;
                case 8:                                                     // 3 Louses
                    for (int i = 0; i < 3; i++)
                    {
                        if (enemyRNG.Next(0, 2) == 0)
                            list.Add(new Actor(Dict.enemyL[2]));
                        else list.Add(new Actor(Dict.enemyL[7]));
                    }
                    break;
                case 9:                                                     // Blue Slaver
                    list.Add(new Actor(Dict.enemyL[8]));
                    break;
                case 10:                                                    // Red Slaver
                    list.Add(new Actor(Dict.enemyL[9]));
                    break;
                case 11:                                                    // Gremlin Gang
                    List<int> gangBlock = new();
                    gangBlock.Add(8);
                    for (int i = 0; i < 4; i++)
                    {
                        int gremlin = 8;
                        while(gangBlock.Contains(gremlin))
                        {
                            gremlin = enemyRNG.Next(0, 8) switch
                            {
                                int j when j >= 0 && j <= 1 => 12,
                                int j when j >= 2 && j <= 3 => 13,
                                int j when j >= 4 && j <= 4 => 14,
                                int j when j >= 5 && j <= 6 => 15,
                                int j when j >= 7 && j <= 7 => 16,
                            };
                        }                                               
                        list.Add(new Actor(Dict.enemyL[gremlin]));
                        gangBlock.Add(gremlin);
                    }
                    break;
                case 12:                                                    // Exordium Thugs
                    int thugs = enemyRNG.Next(0, 4) switch
                    {
                        int j when j == 0 => 2,
                        int j when j == 1 => 7,
                        int j when j == 2 => 3,
                        int j when j == 3 => 4,
                    };
                    list.Add(new Actor(Dict.enemyL[thugs]));
                    thugs = enemyRNG.Next(0, 4) switch
                    {
                        int j when j == 0 => 1,
                        int j when j == 1 => 8,
                        int j when j == 2 => 9,
                        int j when j == 3 => 11,
                    };
                    list.Add(new Actor(Dict.enemyL[thugs]));
                    break;
                case 13:                                                    // Exordium Wildlife
                    int wildlife = enemyRNG.Next(0, 4) switch
                    {
                        int j when j == 0 => 2,
                        int j when j == 1 => 7,
                        int j when j == 2 => 3,
                        int j when j == 3 => 4,
                    };
                    list.Add(new Actor(Dict.enemyL[wildlife]));
                    wildlife = enemyRNG.Next(0, 4) switch
                    {
                        int j when j == 0 => 1,
                        int j when j == 1 => 8,
                        int j when j == 2 => 9,
                        int j when j == 3 => 11,
                    };
                    list.Add(new Actor(Dict.enemyL[wildlife]));
                    break;
                case 14:                                                    // Elite
                    int elite = enemyRNG.Next(17, 20);
                    if (elite == 19)
                        for (int i = 0; i < 3; i++)
                            list.Add(new Actor(Dict.enemyL[elite]));
                    else list.Add(new Actor(Dict.enemyL[elite]));
                    break;
                case 15:                                                    // Boss
                    int boss = enemyRNG.Next(22, 25);
                    list.Add(new Actor(Dict.enemyL[boss]));
                    break;
                default: break;
            }
            for (int i = 0; i < list.Count; i++)                            //Enemy Init
            {
                list[i].MaxHP = list[i].EnemyHealthSet(list[i].BottomHP, list[i].TopHP);
                list[i].Hp = list[i].MaxHP;
                list[i].Hp = list[i].MaxHP;
                switch (list[i].EnemyID)
                {
                    default:
                        break;
                    case 2:
                        Random random = new Random();
                        list[i].AddBuff(5,random.Next(3, 8));
                        break;
                    case 10:
                        list[i].AddBuff(28, 2);
                        break;
                    case 11:
                        list[i].AddBuff(18, 15);
                        break;
                    case 18:
                        list[i].AddBuff(15, 3);
                        list[i].AddBuff(23, 8);
                        break;
                    case 19:
                        list[i].AddBuff(8, 1);
                        break;
                    case 20:
                        list[i].AddBuff(28, 0);
                        break;
                    case 21:
                        list[i].AddBuff(28, 0);
                        break;
                    case 22:
                        list[i].AddBuff(28, 0);
                        break;
                    case 23:
                        list[i].AddBuff(16, 30);
                        break;
                }
            }
            return list;
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
    }
}

