namespace STV
{
    public class WheelOfChange : Event
    {
        public WheelOfChange()
        {
            Name = "Wheel Of Change";
            StartOfEncounter = $"You come upon a dapper looking, cheery gremlin.\r\nGremlin: \"It's time to spin the wheel! Are you R E A D Y ? Of course you are!\"";
            Options = new() { "[S]pin the WHEEL OF CHANGE!!" };
        }

        public override void EventAction(Hero hero)
        {
            string choice = "S";
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}");
            string playerChoice = "";
            while (choice == playerChoice)
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            int wheelChoice = EventRNG.Next(6);
            Console.WriteLine(Result(wheelChoice));
            if (wheelChoice == 0)
                hero.GoldChange(200);
            else if (wheelChoice == 1)
                hero.AddToRelics(Relic.RandomRelic(hero.Name));
            else if (wheelChoice == 2)
                hero.HealHP(hero.MaxHP);
            else if (wheelChoice == 3)
                hero.AddToDeck(new Decay());
            else if (wheelChoice == 4)
                hero.Deck.Remove(Card.PickCard(hero.Deck, "remove"));
            else hero.NonAttackDamage(hero, hero.MaxHP / 10, "the Gremlin's Shiv");
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Gremlin: \"You win some GOLD!\"\r\nGremlin: \"YAY!!!!\"",
                1 => "Gremlin: \"Ah, a gift!\"\r\nGremlin: \"Enjoy!\"",
                2 => "Gremlin: \"Oooh, a free Heal for you!\"",
                3 => "Gremlin: \"Looks like you won a Curse!\"\r\nGremlin: \"That's not good.\"\r\nGremlin: \"Oh well! Better luck next time!\"",
                4 => "Gremlin: \"Ohh, the power of darkness...\"\r\nGremlin: \"Choose a card to remove from your deck!\"",
                _ => "Gremlin: \"Uh oh!\"\r\nGremlin: \"You lose!\"\r\nYou spot him readying a shiv...\r\nYou slash at the crazy gremlin but he's simply too quick!\r\nHe gets you a few times with a crude shiv.\r\nGremlin: \"The price has been paid!!\r\nand with that, both the gremlin and its wheel disappear in a puff of smoke.",
            };
        }
    }
}
