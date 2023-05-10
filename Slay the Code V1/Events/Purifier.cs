namespace STV
{
    public class Purifier : Event
    {
        public Purifier()
        {
            Name = "Purifier";
            StartOfEncounter = $"Before you lies an elaborate shrine to a forgotten spirit.";
            Options = new() { "[P]ray - Remove a Card", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "P", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                Card removeCard = Card.PickCard(hero.Deck, "remove");
                hero.RemoveFromDeck(removeCard);
                Console.WriteLine(Result(0));
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As you kneel in reverence, you feel a weight lifted off your shoulders.",
                _ => "You ignore the shrine.",
            };
        }
    }
}
