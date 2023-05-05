namespace STV
{
    public class OldBeggar : Event
    {
        public OldBeggar()
        {
            Name = "OldBeggar";
            StartOfEncounter = $"An old beggar cloaked in fur reaches his hands out towards you as you pass. \"Spare some coin, child?\"";
            Options = new() { "[O]ffer Gold - Lose 75 Gold, Remove a Card", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "O", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "O")
            {
                hero.Gold -= 75;
                Console.WriteLine(Result(0));
                hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
                Console.WriteLine(Result(1));   
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "The beggar takes off its cloak to reveal that he is Cleric!\nCleric: \"You are a kind soul. Receive my purification!\" he screams.\nYou are unsure if he is grateful or mad.",
                1 => "Cleric: \"I hope you do better this time, friend!\" he shouts.\nWondering what was implied by this, you push forward.",
                _ => "The beggar looks to the floor as you pass.\n\"You will never make a difference... You never do.\"",
            };
        }
    }
}