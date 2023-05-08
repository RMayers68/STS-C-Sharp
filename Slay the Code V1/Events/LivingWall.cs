namespace STV
{
    public class LivingWall : Event
    {
        public LivingWall()
        {
            Name = "Living Wall";
            StartOfEncounter = $"As you come to a dead-end and begin to turn around, walls slam down from the ceiling, trapping you!\nThree faces materialize from the walls and speak.\n\"Forget what you know, and I'll let you go.\"\n\"I require change to see a new space.\"\n\"If you want to pass me, then you must grow.\"";
            Options = new() { "[F]orget - Remove a card", "[C]hange Transform a card", "[G]row Upgrade a card" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "F", "C", "G" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "F")
                hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
            else if (playerChoice == "C")
                Card.PickCard(hero.Deck, "transform").TransformCard(hero);                
            else Card.PickCard(hero.Deck.FindAll(x => x.Type != "Curse").FindAll(x => !x.Upgraded), "upgrade").UpgradeCard();
            Console.WriteLine("Satisfied, the walls in front of you merge back into the ceiling, leaving a path forward.");
        }
    }
}
