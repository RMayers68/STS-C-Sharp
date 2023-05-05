namespace STV
{
    public class TheColosseum : Event
    {
        public TheColosseum()
        {
            Name = "The Colosseum";
            StartOfEncounter = $"Thwack!!!\nYou were knocked unconscious.\nGroggy and with a throbbing head, you awaken to find yourself thrown in the center of a massive stadium with an overflowing audience of Slavers, Cultists, and other denizens of the City!\nAn armored giant with a golden crown bellows at you from atop,\nArmored Giant: \"WE NOW BEGIN THE COMBAT!!!!\"\nA gate on the opposite side opens...";
            Options = new() { "[F]ight" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() {"F"};
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            hero.Encounter = Enemy.CreateEncounter(10);
            Battle.Combat(hero, hero.Encounter);
            if (hero.Hp < 0) return;
            else
            {
                choices = new() { "C", "V" };
                Options = new() { "[C]owardice - Escape","[V]ictory - A powerful fight with many rewards" };
                playerChoice = "";
                Console.WriteLine(Result(0));
                Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
                while (choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                if (playerChoice == "V")
                {
                    hero.Encounter = Enemy.CreateEncounter(10, 1);
                    if (hero.HasRelic("Sling"))
                        hero.AddBuff(4, 2);
                    if (hero.HasRelic("Slaver's"))
                    {
                        hero.MaxEnergy++;
                        Battle.Combat(hero, hero.Encounter);
                        hero.MaxEnergy--;
                    }
                    if (hero.HasRelic("Insect"))
                    {
                        foreach (Enemy e in hero.Encounter)
                            e.Hp = Convert.ToInt32(e.Hp * 0.75);
                    }
                    Battle.Combat(hero, hero.Encounter);
                    if (hero.Hp < 0) return;
                    else
                    {
                        hero.GoldChange(100);
                        hero.AddToDeck(Card.ChooseCard(3, "add", hero.Name));
                        for (int i = 0; i < 2; i++)
                            hero.AddToRelics(Relic.RandomRelic(hero.Name));
                    }
                }
                else Console.WriteLine(Result(1));
            }           
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Armored Giant: \"WELL DONE, WEAKLING!\"\nThe giant mock claps whilst he riles up the crowd with exaggerated gestures.\nGold and confetti shower you!\nArmored Giant: \"TIME FOR THE REAL CHALLENGE!!\"\nThe last battle left a small opening in the Colosseum’s wall, you can easily escape through there while everyone is distracted.\nDo you stay and fight?",
                _ => "Armored Giant: \"COWARD!\"",
            };
        }
    }
}
