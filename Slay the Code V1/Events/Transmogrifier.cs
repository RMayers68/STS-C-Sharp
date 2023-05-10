namespace STV
{
    public class Transmogrifier : Event
    {
        public Transmogrifier()
        {
            Name = "Transmogrifier";
            StartOfEncounter = $"Before you lies an elaborate shrine to a forgotten spirit.";
            Options = new() { "[P]ray", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]} - Transform a Card\n{Options[1]}");
            string playerChoice = "";
            while (playerChoice != "L" && playerChoice != "P")
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                Card transformCard = Card.PickCard(hero.Deck, "transform");
                transformCard.TransformCard(hero);
                Console.WriteLine(Result(0));
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As the power of the shrine flows through you, your mind feels altered.",
                _ => "You ignore the shrine.",
            };
        }
    }
}
