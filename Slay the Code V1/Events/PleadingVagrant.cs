namespace STV
{
    public class PleadingVagrant : Event
    {
        public PleadingVagrant()
        {
            Name = "Pleading Vagrant";
            StartOfEncounter = $"While sneaking past a group of shrouded figures, one of them approaches you.\r\n\"Got anything for me friend? Please... maybe some Coin?\"\r\n\"I just need somewhere to stay, I have treasures I can trade...\"\r\nHe seems delusional, but harmless.";
            Options = new() { "[G]ive 85 Gold - Obtain a random Relic.", "[R]ob - Obtain a random Relic. Become Cursed with Shame.", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "G", "R", "L" };
            if (hero.Gold < 85)
            {
                Options[0] = "Locked - Not Enough Gold";
                choices.Remove("G");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "G")
            {
                hero.Gold -= 85;
                Console.WriteLine(Result(0));
                hero.AddToRelics(Relic.RandomRelic(hero));
            }
            else if (playerChoice == "R")
            {
                hero.AddToRelics(Relic.RandomRelic(hero));
                Console.WriteLine(Result(1));
                hero.AddToDeck(new Shame());
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Oh yes, yes! Here here, a fair trade!",
                1 => "You snatch the precious relic from his clutches and walk away.\nFrom behind you hear,\n\"Have you no shame? HAVE YOU NO SHAAAAAME?!\"\nYou have some shame.",
                _ => "You tell him that you only ever carry a credit card on you, no Coin. He stumbles away and you move on.",
            };
        }
    }
}
