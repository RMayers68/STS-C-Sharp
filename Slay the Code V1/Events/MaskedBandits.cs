namespace STV
{
    public class MaskedBandits : Event
    {
        public MaskedBandits()
        {
            Name = "MaskedBandits";
            StartOfEncounter = $"You encounter a group of bandits wearing large red masks.\nRomeo: \"Hello, pay up to pass... a reasonable fee of ALL your gold will do! Heh heh!\"";
            Options = new() { "[P]ay - Lose ALL Gold ", "[F]ight!" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "F", "P" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "F")
            {
                Console.WriteLine(Result(1));
                hero.Encounter = Enemy.CreateEncounter(10, 2);
                Battle.Combat(hero, hero.Encounter);
                if (hero.Hp <= 0) return;
                else
                {
                    hero.CombatRewards("Monster");
                    hero.AddToRelics(Dict.relicL[174]);
                }
            }
            else
            {
                hero.GoldChange(-1 * hero.Gold);
                Console.WriteLine(Result(0));
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Romeo: \"Hehehe.. Thanks for the gold!\"\nRomeo: \"Oh, I love gold. It's so nice. shiny shiny chits they are!\"\nRomeo: \"Hey Bear, hey! This guy gave us all his gold! What a sucker, right?\nRomeo: Get this, I just had to ask nicely. Who knew?! I certainly didn't! What a chump!\"\nRomeo: \"Gang, let's all have a laugh for this wondrous occasion! Hahaah Ho HOH hoho! Hoh!\"\nRomeo: \"Oh? You're still here? Did you overhear something? Didn't think so.\nRomeo: *snerk* ...loser.... Hahaha haaah\"",
                _ => "Romeo: \"Grab 'em Bear!!\"",
            };
        }
    }
}
