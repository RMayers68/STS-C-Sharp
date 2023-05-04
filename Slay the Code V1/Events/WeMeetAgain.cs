namespace STV
{
    public class WeMeetAgain : Event
    {
        public WeMeetAgain()
        {
            Name = "We Meet Again!";
            StartOfEncounter = $"\"We meet again!\"\r\nA cheery disheveled fellow approaches you gleefully. You do not know this man.\r\n\"It's me, Ranwid! Have any goods for me today? The usual? A fella like me can't make it alone, you know?\r\nYou eye him suspiciously and consider your options...";
            Options = new() { "Give [P]otion", "Give [G]old", "Give [C]ard", "[A]ttack" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "P", "G","C", "A" };
            Potion pot = new(); int goldCost = 0; Card card = new Purchased();
            if (hero.Potions.Count < 1)
            {
                Options[0] = "Locked - No Potions";
                choices.Remove("P");
            }
            else
            {
                pot = hero.Potions[EventRNG.Next(hero.Potions.Count)];
                Options[0] += $" - Lose {pot.Name}. Receive a Relic.";
            }
            if (hero.Gold < 50)
            {
                Options[1] = "Locked - Not Enough Gold";
                choices.Remove("G");
            }
            else
            {
                goldCost = EventRNG.Next(50, hero.Gold > 150 ? 150 : hero.Gold);
                Options[1] += $" - Lose {goldCost}. Receive a Relic.";
            }
            List<Card> list = hero.Deck.FindAll(x => x.Type != "Curse").FindAll(x => x.Rarity != "Basic");
            if (list.Count < 1)
            {
                Options[2] = "Locked - No Proper Card";
                choices.Remove("C");
            }
            else
            {
                card = list[EventRNG.Next(list.Count)];
                Options[2] += $" - Lose {card.Name}. Receive a Relic.";
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}\n{Options[3]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                hero.Potions.Remove(pot);
                Console.WriteLine(Result(0));
            }
            else if (playerChoice == "G")
            {
                hero.Gold -= goldCost;
                Console.WriteLine(Result(1));
            }
            else if (playerChoice == "C")
            {
                hero.Deck.Remove(card);
                Console.WriteLine(Result(2));
            }
            else Console.WriteLine(Result(4));
            if (playerChoice == "P" || playerChoice == "G" || playerChoice == "C")
            {
                Console.WriteLine(Result(3));
                hero.AddToRelics(Relic.RandomRelic(hero.Name));
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Ranwid: \"Exquisite! Was feeling parched.\"\r\nGlup glup glup\r\nHe downs the potion in one go and lets out a satisfied burp.",
                1 => "Ranwid: \"Magnificent! This will be quite handy if I run into those mask wearing hoodlums again.\"",
                2 => "Ranwid: \"Exemplary! I shall study this further in my chambers.\"",
                3 => "He rummages around his various pockets...\r\nRanwid: \"Here, look what I've got for you today! Take it take it!\"",
                _ => "Ranwid: \"Aaaaagghh!! What a jerk you are sometimes!\"\r\nHe runs away.",
            };
        }
    }
}