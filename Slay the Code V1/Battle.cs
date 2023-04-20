using static Global.Functions;
namespace STV
{
    public class Battle
    {
        private static readonly Random CombatRNG = new();
        public Battle() { }

        public static void Combat(Hero hero, List<Enemy> encounter, Room activeRoom)
        {
            ScreenWipe();
            Console.WriteLine("Next encounter:");
            foreach (Actor actor in encounter)
                Console.WriteLine(actor.Name);
            foreach (Card c in hero.Deck)
                hero.DrawPile.Add(new Card(c));
            int turnNumber = 0;
            StartOfCombat(hero, encounter);
            //Check HP values to end encounter when one group is reduced to 0
            while ((!encounter.All(x => x.Hp == 0)) && (hero.Hp != 0) && encounter.Count > 0)
            {
                // Start of Player Turn              
                StartOfPlayerTurn(hero, encounter, turnNumber);
                turnNumber++;
                Pause();

                // PLAYER TURN MENU PLUS ALL ACTIONS DETAILS                
                string playerChoice = "";
                while (playerChoice != "E" && (!encounter.All(x => x.Hp == 0)) && hero.Hp != 0)
                {
                    ScreenWipe();
                    if (hero.DrawPile.Count == 0 && hero.FindRelic("Unceasing") != null)
                        hero.DrawCards(1, encounter);
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
                        hero.Hand[cardChoice - 1].CardAction(hero, encounter);
                        HealthChecks(hero, encounter);
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
                                hero.Potions[usePotion - 1].UsePotion(hero, encounter);
                                HealthChecks(hero, encounter);
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
                                HealthChecks(hero, encounter);
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
                        hero.Hand[i].Exhaust(hero, encounter, hero.Hand);
                    else if (hero.FindRelic("Pyramid") != null)
                        continue;
                    else hero.Hand[i].MoveCard(hero.Hand, hero.DiscardPile);  //Discard at end of turn (Comment to find easy for disabling)
                }
                // Orbin Time
                foreach (var orb in hero.Orbs)
                {
                    if (orb is null) continue;
                    while (encounter.Count > 0)
                        orb.PassiveEffect(hero, encounter);
                }
                // END OF PLAYER AND START OF ENEMY TURN
                Console.WriteLine("Enemy's Turn!\n");
                for (int i = encounter.Count - 1; i >= 0; i--)
                {
                    encounter[i].Actions.Add(encounter[i].Intent);
                    encounter[i].EnemyAction(hero, encounter);
                    HealthChecks(hero, encounter);
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

                EndOfCombat(hero);
                hero.CombatRewards(activeRoom.Location);
            }
            Pause();
        }

        private static void StartOfCombat(Hero hero, List<Enemy> encounter)
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
                Card enchiridion = new(Card.RandomCards(hero.Name, 1, "Power")[0]);
                enchiridion.SetTmpEnergyCost(0);
                hero.AddToHand(enchiridion);
            }
            if (hero.FindRelic("Runic Capacitor") != null)
                hero.OrbSlots += 2;
            if (hero.FindRelic("Cracked") != null)
                Orb.ChannelOrb(hero, encounter, 0);
            if (hero.FindRelic("Nuclear") != null)
                Orb.ChannelOrb(hero, encounter, 3);
            if (hero.FindRelic("Symbiotic") != null)
                Orb.ChannelOrb(hero, encounter, 2);
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
            if (hero.FindRelic("Philo") != null)
                foreach (Enemy e in encounter)
                    e.AddBuff(4, 1, hero);
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

            hero.ShuffleDrawPile();
            // Draw cards
            if (hero.FindRelic("Snecko Eye") != null)
            {
                hero.AddBuff(96, 1);
                hero.DrawCards(2, encounter);
            }
            if (hero.FindRelic("Snake") != null)
                hero.DrawCards(2, encounter);
            else if (hero.FindRelic("Serpent") != null)
                hero.DrawCards(1, encounter);
            if (hero.FindRelic("Preparation") != null)
                hero.DrawCards(2, encounter);
            if (hero.FindRelic("Toolbox") != null)
                hero.AddToHand(new(Card.ChooseCard(Card.RandomCards("Colorless", 3), "add to your hand")));
            // Gambling Chip
            if (hero.FindRelic("Gambling") != null)
                GamblingIsGood(hero, encounter);
            HealthChecks(hero, encounter);
        }

        //COMBAT METHODS

        private static void StartOfPlayerTurn(Hero hero, List<Enemy> encounter, int turnNumber)
        {
            if (turnNumber != 0)
            {
                if (hero.FindRelic("Emotion").IsActive)
                {
                    foreach (Orb o in hero.Orbs)
                        o.PassiveEffect(hero, encounter);
                }
                // Duration buff decrease for hero
                foreach (Buff b in hero.Buffs)
                {
                    b.DurationDecrease();
                }
                hero.Buffs.RemoveAll(x => x.DurationEnded());
                // Same for enemy
                foreach (Enemy e in encounter)
                {
                    foreach (Buff b in e.Buffs)
                    {
                        b.DurationDecrease();
                    }
                    e.Buffs.RemoveAll(x => x.DurationEnded());
                    if (e.FindBuff("Ritual") is Buff ritual && ritual != null)
                        e.AddBuff(4, ritual.Intensity);
                }
            }
            // Start of Turn effects that happen on select turns
            switch (turnNumber)
            {
                default: break;
                case 2:
                    if (hero.FindRelic("Horn Cleat") != null)
                        hero.GainBlock(14);
                    break;
                case 3:
                    if (hero.FindRelic("Captain's Wheel") != null)
                        hero.GainBlock(1);
                    break;
            }
            // Start of turn that applies to all turns
            if (hero.FindRelic("Damaru") != null)
                hero.AddBuff(10, 1);
            if (hero.FindRelic("Mercury") != null)
                foreach (Enemy e in encounter)
                    hero.NonAttackDamage(e, 3);
            if (hero.FindRelic("Pocketwatch").IsActive)
                hero.DrawCards(3, encounter);
            hero.DrawCards(5, encounter);
            for (int i = 0; i < encounter.Count; i++)
                encounter[i].SetEnemyIntent(turnNumber, encounter);
            hero.Energy += hero.MaxEnergy;
            if (hero.Orbs.FindAll(x => x.Name == "Plasma").Count is int plasmaCount && plasmaCount > 0)
                hero.Energy += plasmaCount;
        }

        private static void EndOfCombat(Hero hero)
        {
            Console.WriteLine("\nVictorious, the creature is slain!\n");
            int endCombatHeal = 0;
            if (hero.FindRelic("Face of Cleric") != null)
                hero.SetMaxHP(1);
            if (hero.FindRelic("Meat") != null && hero.Hp <= hero.MaxHP / 2)
                endCombatHeal += 12;
            if (hero.FindRelic("Burning Blood") != null)
                endCombatHeal += 6;
            else if (hero.FindRelic("Burning Blood") != null)
                endCombatHeal += 12;
            if (hero.FindBuff("Self Repair") is Buff selfRepair && selfRepair != null)
                endCombatHeal += selfRepair.Intensity;
            if (endCombatHeal > 0)
                hero.CombatHeal(endCombatHeal);
            // Remove all buffs and reinit Hero
            hero.ResetHeroAfterCombat();
        }

        // HEALTH CHECK
        public static void HealthChecks(Hero hero, List<Enemy> encounter)
        {
            for (int i = encounter.Count - 1; i >= 0; i--)
                if (encounter[i].Hp <= 0)
                {
                    encounter[i].Hp = 0;
                    if (encounter.Count > 1 && hero.FindRelic("Specimen") != null && encounter[i].FindBuff("Poison") is Buff poison && poison != null)
                    {
                        int specimen = CombatRNG.Next(0, encounter.Count);
                        while (specimen == i)
                            specimen = CombatRNG.Next(0, encounter.Count);
                        encounter[specimen].AddBuff(39, poison.Intensity, hero);
                    }
                    if (hero.FindRelic("Gremlin Horn") != null)
                    {
                        hero.GainTurnEnergy(1);
                        hero.DrawCards(1, encounter);
                    }
                    encounter.RemoveAt(i);
                }
            if (hero.Hp <= 0)
            {
                if (hero.Potions.Find(x => x.Name.Contains("Fairy")) is Potion fairy && fairy != null)
                {
                    fairy.UsePotion(hero, encounter);
                }
                else if (hero.FindRelic("Lizard") is Relic lizard && lizard.IsActive)
                {
                    hero.CombatHeal(hero.MaxHP / 2);
                    lizard.IsActive = false;
                }
                else hero.Hp = 0;
            }
            else if (hero.Hp <= hero.MaxHP / 2 && hero.FindRelic("Red Skull") is Relic inactiveSkull && !inactiveSkull.IsActive)
            {
                hero.AddBuff(4, 3);
                inactiveSkull.IsActive = true;
            }
            else if (hero.Hp > hero.MaxHP / 2 && hero.FindRelic("Red Skull") is Relic activeSkull && activeSkull.IsActive)
            {
                hero.AddBuff(4, -3);
                activeSkull.IsActive = false;
            }
        }

        public static void GamblingIsGood(Hero hero, List<Enemy> encounter)
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
            hero.DrawCards(gamble, encounter);
        }
    }
}