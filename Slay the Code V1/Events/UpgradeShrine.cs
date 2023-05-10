using System.Linq;

namespace STV
{
    public class UpgradeShrine : Event
    {
        public UpgradeShrine()
        {
            Name = "Upgrade Shrine";
            StartOfEncounter = $"Before you lies an elaborate shrine to a forgotten spirit.";
            Options = new() { "[P]ray - Upgrade a Card", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "P", "L" };
            if (hero.Deck.FindAll(x => x.Type != "Curse").All(x => x.Upgraded))
            {
                Options[0] = "Locked - No Upgradeable Cards";
                choices.Remove("P");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                Card upgradeCard = Card.PickCard(hero.Deck, "upgrade");
                upgradeCard.UpgradeCard();
                Console.WriteLine(Result(0));
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As the power of the shrine flows through you, making you stronger.",
                _ => "You ignore the shrine.",
            };
        }
    }
}
