namespace STV
{
    public class Duplicator : Event
    {
        public Duplicator()
        {
            Name = "Duplicator";
            StartOfEncounter = $"Before you lies a decorated altar to some ancient entity.";
            Options = new() { "[P]ray", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]} - Duplicate a Card\n{Options[1]}");
            string playerChoice = "";
            while (playerChoice != "L" && playerChoice != "P")
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                Card duplicatedCopy = Card.PickCard(hero.Deck,"duplicate");
                hero.AddToDeck(duplicatedCopy);
                Console.WriteLine(Result(0));
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "You kneel respectfully. A ghastly mirror image appears from the shrine and collides into you.",
                _ => "You ignore the shrine, confident in your choice.",
            };
        }
    }
}