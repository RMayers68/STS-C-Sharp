
namespace STV.Events
{
    public class DesignerInspire : Event
    {
        public DesignerInspire()
        {
            Name = "Designer In-Spire";
            StartOfEncounter = $"You discover a colorful shop with the banner \"IN-SPIRE\" and walk in to see what's inside.\r\n\"No, no way. Nope. Can't let you in!\" A man with ridiculous clothing appears at the entrance to bar you.\r\nDesigner: \"This will not do, no no. What is this style? Disgusting! Are you bleeeeding? Groooss. Business?? You a customer? Fine. Whaaatever.\"\r\nHe lets out an exaggerated sigh and points at a list of services.\r\nThe services seem fine, but you would rather punch this smug man in his smug face.";
            Options = new() { "[A]djustments - Upgrade a card (40 Gold)", "[C]lean Up - Remove a card (60 Gold) ", "[F]ull Service - Remove a card, then upgrade a random card (90 Gold)", "[P]unch" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "A", "C", "F", "P" };
            if (hero.Gold < 40)
            {
                Options[0] = "Locked - Not Enough Gold";
                choices.Remove("A");
            }
            if (hero.Gold < 60)
            {
                Options[1] = "Locked - Not Enough Gold";
                choices.Remove("C");
            }
            if (hero.Gold < 90)
            {
                Options[2] = "Locked - Not Enough Gold";
                choices.Remove("F");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}\n{Options[3]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "A")
                Card.PickCard(hero.Deck, "upgrade").UpgradeCard();
            else if (playerChoice == "C")
                hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
            else if (playerChoice == "F")
            {
                hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
                List<Card> list = hero.Deck.FindAll(x => x.Type != "Curse").FindAll(x => !x.Upgraded);
                list[EventRNG.Next(list.Count)].UpgradeCard();
            }
            else
            {
                hero.SelfDamage(3);
                Console.WriteLine(Result(1));
            }
            if (playerChoice == "A" || playerChoice == "F" || playerChoice == "C")
                Console.WriteLine(Result(0));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Designer: \"Okay, bye bye now.\"\r\n...should've punched him.",
                _ => "You punch him so hard your fist hurts.\r\nDesigner: \"My FACE!! Now I'll have to-\"\r\nHe fainted. Who's groooss and bleeeeding now?",
            };
        }
    }
}
