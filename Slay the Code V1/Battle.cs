﻿using System.Xml.Linq;
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
            foreach (Enemy e in encounter)
                Console.WriteLine(e.Name);
            foreach (Card c in hero.Deck)
                hero.DrawPile.Add(new Card(c));
            int turnNumber = 0;
            StartOfCombat(hero, encounter);
            //Check HP values to end encounter when one group is reduced to 0
            while ((!encounter.All(x => x.Hp == 0)) && (hero.Hp != 0) && encounter.Count > 0)
            {
                // Start of Player Turn              
                StartOfPlayerTurn(hero, encounter, turnNumber);
                if (activeRoom.Location == "Elite" || activeRoom.Location == "Boss")
                    hero.Energy++;
                turnNumber++;
                Pause();

                // PLAYER TURN MENU PLUS ALL ACTIONS DETAILS                
                string playerChoice = "";
                while (playerChoice != "E" && (!encounter.All(x => x.Hp == 0)) && hero.Hp != 0)
                {
                    ScreenWipe();
                    if (hero.DrawPile.Count == 0 && hero.HasRelic("Unceasing"))
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
                                Console.WriteLine($"View Your (B)uffs/Debuffs {(hero.Hand.Count >= i ? Tab(8) + i + ":" + hero.Hand[i - 1].GetName() : "")}\n");
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
                        int cardsPlayed = hero.Actions.FindAll(x => x.Contains($"{turnNumber}:")).Count;
                        if ((hero.HasRelic("Velvet") && cardsPlayed > 6) || (Card.FindCard("Normality", hero.Hand) != null && cardsPlayed > 3))
                            Console.WriteLine("You have already played too many cards this turn.");
                        else
                        {
                            hero.Hand[cardChoice - 1].CardAction(hero, encounter, turnNumber);
                            if (hero.FindTurnActions(turnNumber, "Attack").Count % 3 == 0)
                            {
                                if (hero.HasRelic("Kunai"))
                                    hero.AddBuff(9, 1);
                                if (hero.HasRelic("Shuriken"))
                                    hero.AddBuff(4, 1);
                                if (hero.HasRelic("Ornamental Fan"))
                                    hero.GainBlock(4);
                            }
                            if (hero.HasRelic("Letter Opener") && hero.FindTurnActions(turnNumber, "Skill").Count % 3 == 0)
                                foreach (Enemy e in encounter)
                                    hero.NonAttackDamage(e, 5);
                        }
                        HealthChecks(hero, encounter, turnNumber);
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
                                hero.Potions[usePotion - 1].UsePotion(hero, encounter, turnNumber);
                                HealthChecks(hero, encounter, turnNumber);
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
                            // END OF PLAYER AND START OF ENEMY TURN
                            case "E":
                                EndOfPlayerTurn(hero, encounter, turnNumber);
                                ScreenWipe();
                                break;
                        }

                }
                // Orbin Time
                foreach (var orb in hero.Orbs)
                {
                    if (orb is null) continue;
                    while (encounter.Count > 0)
                        orb.PassiveEffect(hero, encounter);
                }
                Console.WriteLine("Enemy's Turn!\n");
                for (int i = encounter.Count - 1; i >= 0; i--)
                {
                    encounter[i].AddAction(encounter[i].Intent,turnNumber);
                    encounter[i].EnemyAction(hero, encounter);
                    HealthChecks(hero, encounter, turnNumber);
                }
                if (hero.FindBuff("Barricade") == null || hero.FindBuff("Blur") == null)
                {
                    if (hero.HasRelic("Calipers"))
                        hero.Block -= 15;
                    else hero.Block = 0;
                }
            }
            // END OF COMBAT
            ScreenWipe();
            if (hero.Hp <= 0)
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
            if (hero.HasRelic("Mark of Pain"))
                for (int i = 0; i < 2; i++)
                    hero.DrawPile.Add(new(Dict.cardL[357]));
            if (hero.HasRelic("Ench"))
            {
                Card enchiridion = new(Card.RandomCards(hero.Name, 1, "Power")[0]);
                enchiridion.SetTmpEnergyCost(0);
                hero.AddToHand(enchiridion);
            }
            if (hero.HasRelic("Runic Capacitor"))
                hero.OrbSlots += 2;
            if (hero.HasRelic("Cracked"))
                Orb.ChannelOrb(hero, encounter, 0);
            if (hero.HasRelic("Nuclear"))
                Orb.ChannelOrb(hero, encounter, 3);
            if (hero.HasRelic("Symbiotic"))
                Orb.ChannelOrb(hero, encounter, 2);
            if (hero.HasRelic("Vial"))
                hero.CombatHeal(2);
            if (hero.HasRelic("Clockwork"))
                hero.AddBuff(8, 1);
            if (hero.HasRelic("Akebeko"))
                hero.AddBuff(85, 8);
            if (hero.HasRelic("Anchor"))
                hero.GainBlock(10);
            if (hero.HasRelic("Marbles"))
                foreach (Enemy e in encounter)
                    e.AddBuff(1, 1, hero);
            if (hero.HasRelic("Red Mask"))
                foreach (Enemy e in encounter)
                    e.AddBuff(2, 1, hero);
            if (hero.HasRelic("Philo"))
                foreach (Enemy e in encounter)
                    e.AddBuff(4, 1, hero);
            if (hero.HasRelic("Scales"))
                hero.AddBuff(41, 3);
            if (hero.HasRelic("Data"))
                hero.AddBuff(7, 1);
            if (hero.HasRelic("Lantern"))
                hero.GainTurnEnergy(1);
            if (hero.HasRelic("Fossil"))
                hero.AddBuff(56, 1);
            if (hero.HasRelic("Oddly"))
                hero.AddBuff(9, 1);
            if (hero.HasRelic("Vajra"))
                hero.AddBuff(4, 1);
            if (hero.FindRelic("Du-Vu") is Relic duvu && duvu != null)
                hero.AddBuff(4, duvu.PersistentCounter);
            if (hero.FindRelic("Girya") is Relic girya && girya != null)
                hero.AddBuff(4, girya.EffectAmount);
            if (hero.HasRelic("Thread"))
                hero.AddBuff(95, 4);
            if (hero.HasRelic("Gremlin Visage"))
                hero.AddBuff(2, 1);
            if (hero.HasRelic("Mutagenic"))
            {
                hero.AddBuff(4, 3);
                hero.AddBuff(30, 3);
            }
            if (hero.HasRelic("Preserved"))
                foreach (Enemy e in encounter)
                    e.Hp = Convert.ToInt32(e.Hp * 0.75);
            if (hero.HasRelic("Twisted"))
                foreach (Enemy e in encounter)
                    e.AddBuff(39, 4, hero);
            if (hero.HasRelic("Ninja"))
                Card.AddShivs(hero, 3);
            if (hero.HasRelic("Tear"))
                hero.SwitchStance("Calm");
            if (hero.FindRelic("Neow") is Relic neow && neow.IsActive)
            {
                foreach (Enemy e in encounter)
                    e.Hp = 1;
                neow.PersistentCounter--;
                if (neow.PersistentCounter == neow.EffectAmount)
                    neow.IsActive = false;
            }
            // Enemy Check

            // Init Shuffle
            hero.ShuffleDrawPile();
            // Draw cards
            if (hero.HasRelic("Snecko Eye"))
            {
                hero.AddBuff(96, 1);
                hero.DrawCards(2, encounter);
            }
            if (hero.HasRelic("Snake"))
                hero.DrawCards(2, encounter);
            if (hero.HasRelic("Preparation"))
                hero.DrawCards(2, encounter);
            if (hero.HasRelic("Toolbox"))
                hero.AddToHand(new(Card.ChooseCard(Card.RandomCards("Colorless", 3), "add to your hand")));
            // Gambling Chip
            if (hero.HasRelic("Gambling"))
                GamblingIsGood(hero, encounter);
            HealthChecks(hero, encounter,0);
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
                    if (hero.HasRelic("Horn Cleat"))
                        hero.GainBlock(14);
                    break;
                case 3:
                    if (hero.HasRelic("Captain's Wheel"))
                        hero.GainBlock(1);
                    break;
            }
            // Start of turn that applies to all turns
            if (hero.FindRelic("Happy") is Relic happyFlower &&  happyFlower != null)
            {
                happyFlower.PersistentCounter--;
                if (happyFlower.PersistentCounter == 0)
                {
                    hero.GainTurnEnergy(1);
                    happyFlower.PersistentCounter = happyFlower.EffectAmount;
                }
            }
            if (hero.FindRelic("Incense") is Relic incenseBurner && incenseBurner != null)
            {
                incenseBurner.PersistentCounter--;
                if (incenseBurner.PersistentCounter == 0)
                {
                    hero.AddBuff(52, 1);
                    incenseBurner.PersistentCounter = incenseBurner.EffectAmount;
                }
            }
            if (hero.FindRelic("Inserter") is Relic inserter && inserter != null)
            {
                inserter.PersistentCounter--;
                if (inserter.PersistentCounter == 0)
                {
                    hero.OrbSlots++;
                    inserter.PersistentCounter = inserter.EffectAmount;
                }
            }
            if (hero.HasRelic("Damaru"))
                hero.AddBuff(10, 1);
            if (hero.HasRelic("Brimstone"))
            {
                hero.AddBuff(4, 2);
                foreach (Enemy e in encounter)
                    e.AddBuff(4, 1);
            }
            if (hero.HasRelic("Mercury"))
                foreach (Enemy e in encounter)
                    hero.NonAttackDamage(e, 3);
            if (hero.HasRelic("Pocketwatch") && hero.Actions.FindAll(x => x.Contains($"{turnNumber}:")).FindAll(x => x.Contains("Played")).Count <= 3)
                hero.DrawCards(3, encounter);
            if (hero.HasRelic("Serpent"))
                hero.DrawCards(1, encounter);
            hero.DrawCards(5, encounter);
            if (hero.HasRelic("Warped") && hero.Hand.Find(x => x.IsUpgraded()) is Card armaments && armaments != null)
                armaments.UpgradeCard();
            for (int i = 0; i < encounter.Count; i++)
                encounter[i].SetEnemyIntent(turnNumber, encounter);
            hero.Energy += hero.MaxEnergy;
            if (hero.Orbs.FindAll(x => x.Name == "Plasma").Count is int plasmaCount && plasmaCount > 0)
                hero.Energy += plasmaCount;
        }
        private static void EndOfPlayerTurn(Hero hero, List<Enemy> encounter, int turnNumber)
        {
            for (int i = 0; i < encounter.Count; i++)
            {
                encounter[i].Block = 0;
            }
            if (!hero.HasRelic("Ice"))
                hero.Energy = 0;
            if (hero.HasRelic("Cloak"))
                hero.GainBlock(hero.Hand.Count);
            if (turnNumber == 7 && hero.HasRelic("Stone Calender"))
                foreach (Enemy e in encounter)
                    hero.NonAttackDamage(e, 52);
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
                else if (hero.HasRelic("Pyramid"))
                    continue;
                else hero.Hand[i].MoveCard(hero.Hand, hero.DiscardPile);  //Discard at end of turn (Comment to find easy for disabling)
            }
            if (hero.HasRelic("Nilry's"))
                hero.DrawPile.Add(new(Card.ChooseCard(Card.RandomCards(hero.Name, 3), "add to your drawpile")));
            HealthChecks(hero, encounter, turnNumber);
        }
        private static void EndOfCombat(Hero hero)
        {
            Console.WriteLine("\nVictorious, the creature is slain!\n");
            int endCombatHeal = 0;
            if (hero.HasRelic("Face of Cleric"))
                hero.SetMaxHP(1);
            if (hero.HasRelic("Meat") && hero.Hp <= hero.MaxHP / 2)
                endCombatHeal += 12;
            if (hero.HasRelic("Burning Blood"))
                endCombatHeal += 6;
            else if (hero.HasRelic("Burning Blood"))
                endCombatHeal += 12;
            if (hero.FindBuff("Self Repair") is Buff selfRepair && selfRepair != null)
                endCombatHeal += selfRepair.Intensity;
            if (endCombatHeal > 0)
                hero.CombatHeal(endCombatHeal);
            // Remove all buffs and reinit Hero
            hero.ResetHeroAfterCombat();
        }

        // HEALTH CHECK
        public static void HealthChecks(Hero hero, List<Enemy> encounter, int turnNumber)
        {
            for (int i = encounter.Count - 1; i >= 0; i--)
                if (encounter[i].Hp <= 0)
                {
                    encounter[i].Hp = 0;
                    if (encounter.Count > 1 && hero.HasRelic("Specimen") && encounter[i].FindBuff("Poison") is Buff poison && poison != null)
                    {
                        int specimen = CombatRNG.Next(0, encounter.Count);
                        while (specimen == i)
                            specimen = CombatRNG.Next(0, encounter.Count);
                        encounter[specimen].AddBuff(39, poison.Intensity, hero);
                    }
                    if (hero.HasRelic("Gremlin Horn"))
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
                    fairy.UsePotion(hero, encounter,turnNumber);
                }
                else if (hero.FindRelic("Lizard") is Relic lizard && lizard.IsActive)
                {
                    hero.CombatHeal(hero.MaxHP / 2);
                    lizard.IsActive = false;
                }
                else hero.Hp = 0;
            }
            if (hero.FindRelic("Red Skull") is Relic inactiveSkull && !inactiveSkull.IsActive && hero.Hp <= hero.MaxHP / 2 )
            {
                hero.AddBuff(4, 3);
                inactiveSkull.IsActive = true;
            }
            else if (hero.FindRelic("Red Skull") is Relic activeSkull && activeSkull.IsActive && hero.Hp <= hero.MaxHP / 2)
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