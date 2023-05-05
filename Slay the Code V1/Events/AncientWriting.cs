namespace STV
{
    public class AncientWriting : Event
    {
        public AncientWriting()
        {
            Name = "Ancient Writing";
            StartOfEncounter = $"Scaling the city, you notice a wall covered in the writing of Ancients. As you try to wrap your head around what the puzzling symbols and glyphs could mean, the writing begins to glow.\r\nSuddenly, the message becomes clear...";
            Options = new() { "[E]legance - Remove a card", "[S]implicity - Upgrade all Strikes and Defends." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "E", "S" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "E")
            {               
                Console.WriteLine(Result(0));
                hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
            }
            else
            {
                Console.WriteLine(Result(2));
                List<Card> upgrades = hero.Deck.FindAll(x => x.Name == "Strike");
                upgrades.AddRange(hero.Deck.FindAll(x => x.Name == "Defend"));
                foreach (Card card in upgrades)
                    card.UpgradeCard();
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "The answer was elegance.\nOf course.",
                _ => "The truth is always simple.",
            };
        }
    }
}
