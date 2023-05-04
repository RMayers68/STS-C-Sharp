namespace STV
{
    public class OminousForge : Event
    {
        public OminousForge()
        {
            Name = "Ominous Forge";
            StartOfEncounter = $"You duck into a small hut. Inside, you find what appears to be a forge. The smithing tools are covered with dust, yet a fire roars inside the furnace. You feel on edge...";
            Options = new() { "[F]orge - Upgrade a card ", "[R]ummage Obtain Warped Tongs. Become Cursed with Pain.", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "F", "R", "L" };
            if (hero.Deck.FindAll(x => x.Type != "Curse").All(x => x.Upgraded))
            {
                Options[0] = "Locked - No Upgradeable Cards";
                choices.Remove("F");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "F")
            {
                List<Card> upgradables = hero.Deck.FindAll(x => x.Type != "Curse").FindAll(x => !x.Upgraded);
                upgradables[EventRNG.Next(upgradables.Count)].UpgradeCard();
                Console.WriteLine(Result(0));
            }
            else if (playerChoice == "R")
            {
                hero.AddToRelics(Dict.relicL[177]);
                Console.WriteLine(Result(1));
                hero.AddToDeck(new Pain());
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "You decide to put the forge to use and...\r\nCLANG CLAAANG CLANG!\r\n...improve your arsenal!",
                1 => "You decide to see if you can find anything of use. After uncovering tarps, looking through boxes, and checking nooks and crannies, you find a dust covered relic!\r\nTaking the relic, you can't shake a sudden feeling of sharp pain as you exit the hut. Maybe you disturbed some sort of spirit?",
                _ => "There doesn't seem to be anything of use. You exit the way you came, the flames of the furnace casting eerie shadows on the walls inside the hut..",
            };
        }
    }
}
