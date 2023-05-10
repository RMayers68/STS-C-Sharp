namespace STV
{
    public class Augmenter : Event
    {
        public Augmenter()
        {
            Name = "Augmenter";
            StartOfEncounter = $"A man with an eyepatch and a devilish grin strides up to you.\nShady Man: \"Hey there, stranger. Interested in advancing science? I can make you stronger than any training or blessing.\"\n\"You're gonna need it if you're one of those heroes with a death wish.\"\nShady Man: \"Whad'ya say?\"";
            Options = new() { "[T]est J.A.X - Obtain a J.A.X. Card", "[B]ecome Test Subject - Transform 2 cards in your deck", "[I]ngest Mutagens - Obtain a Mutagenic Strength Relic" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "T", "B", "I" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "T")
            {
                Console.WriteLine(Result(0));
                hero.AddToDeck(new JAX());
            }
            else if (playerChoice == "B")
            {
                for (int i = 0; i < 2; i++)
                    Card.PickCard(hero.Deck, "transform").TransformCard(hero);
                Console.WriteLine(Result(1));
            }
            else
            {
                Console.WriteLine(Result(2));
                hero.AddToRelics(Dict.relicL[167]);
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Shady Man: \"Excellent.\"\nThe man hands over a dangerous looking syringe filled with a glowing liquid before skulking off into a shadowy alleyway.",
                1 => "hady Man: \"Superb.\"\nThe man injects you with three unknown substances and pulls out a notepad. As you begin to feel light-headed, he starts to frantically write down notes.\nLosing track of time completely, by the time you regain your senses, the shady character has disappeared.",
                _ => "Shady Man: \"Marvelous.\"\r\nYou quaff the mysterious substance. Immediately, you are invigorated and feel your muscle fibers twitch.",
            };
        }
    }
}
