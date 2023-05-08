using static Global.Functions;

namespace STV
{
    public class DeadAdventurer : Event
    {
        int eliteEncounterChance { get; set; }
        public DeadAdventurer()
        {
            Name = "Dead Adventurer";
            StartOfEncounter = $"You come across a dead adventurer on the floor.\r\nIt seems his demise came at the hands of a ferocious creature... Also his pants have been stolen!";
            Options = new() { $"[S]earch - Find Loot. {eliteEncounterChance}% chance of Elite fight", "[E]scape" };
            eliteEncounterChance = 25;
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "S", "E" };            
            Relic relic = new(Relic.RandomRelic(hero));
            int gold = 30, heroGoldBefore = hero.Gold;
            bool nothing = true;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
                string playerChoice = "";
                while (!choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                if (playerChoice == "S")
                {
                    nothing = RewardDetermine(hero, relic, gold, nothing);
                    if (heroGoldBefore < hero.Gold)
                        gold = 0;
                    if (EventRNG.Next(100) <= eliteEncounterChance)
                    {
                        Console.WriteLine(Result(4));
                        hero.Encounter = Enemy.CreateEncounter(5);
                        Battle.Combat(hero, hero.Encounter);
                        if (hero.Hp <= 0) return;
                        else
                        {
                            if (gold > 0)
                                hero.GoldChange(gold);
                            if (relic.Name != "Circlet")
                                hero.AddToRelics(relic);
                        }
                    }
                    else
                    {
                        if ( i < 2)
                        {
                            eliteEncounterChance += 25;
                            Console.WriteLine(Result(3));
                        }
                        else Console.WriteLine(Result(5));                       
                    }
                }
                else
                {
                    Console.WriteLine(Result(6));
                    break;
                }
            }          
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "You found some gold!",
                1 => "You found a relic!!",
                2 => "Hmm, couldn't find anything...",
                3 => "Continue searching?",
                4 => "While searching the adventurer you are caught off guard!",
                5 => "Looks like you searched all his belongings without a hitch!",
                _ => "You exit without a sound.",
            };
        }

        public bool RewardDetermine(Hero hero, Relic relic, int gold, bool nothing)
        {
            int goldReward = 33, relicReward = 67, nothingReward = 100, reward;
            if (gold == 0 && relic.Name == "Circlet")
            {
                goldReward = 0;
                relicReward = 0;
            }                
            else if (gold == 0 && !nothing)
            {
                goldReward = 0;
                nothingReward = 0;
                relicReward = 100;
            }
            else if (relic.Name == "Circlet" && !nothing)
            {
                relicReward = 0;
                nothingReward = 0;
                goldReward = 100;
            }
            else if (gold == 0)
            {
                goldReward = 0;
                relicReward = 50;
            }               
            else if (!nothing)
            {
                nothingReward = 0;
                goldReward = 50;
                relicReward = 100;
            }
            else
            {
                relicReward = 0;
                goldReward = 50;
            }
            reward = EventRNG.Next(0, 100);
            if (reward < goldReward)
            {
                Console.WriteLine(Result(0));
                hero.GoldChange(30);                
            }               
            else if (reward < relicReward)
            {
                Console.WriteLine(Result(1));
                hero.AddToRelics(relic);
                relic = new(Dict.relicL[178]);
            }
            else
            {
                Console.WriteLine(Result(2));
                nothing = false;
            }
            if (!nothing)
                return false;
            else return true;
        }
    }
}