namespace STV
{
    public class TheCleric : Event
    {
        public TheCleric()
        {
            Name = "The Cleric";
            StartOfEncounter = $"A strange blue humanoid with a golden helm(?) approaches you with a huge smile.\n\"Hello friend! I am Cleric! Are you interested in my services?!\" the creature shouts, loudly.";
            Options = new() { "[H]eal - 35 Gold. Heal 1/4 of Max HP", "[P]urify - 50 Gold. Remove a card", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "H", "P", "L" };
            if (hero.Gold < 50)
            {
                Options[1] = "Locked - Not Enough Gold";
                choices.Remove("P");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "H")
            {
                hero.Gold -= 35;
                hero.HealHP(hero.MaxHP / 4);
                Console.WriteLine(Result(0));
            }
            else if (playerChoice == "P")
            {
                hero.Gold -= 50;
                hero.SetMaxHP(5);
                Console.WriteLine(Result(1));
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "A warm golden light envelops your body and dissipates.\nThe creature grins.\nCleric: \"Cleric best healer. Have a good day!\"",
                1 => "A cold blue flame envelops your body and dissipates.\nThe creature grins.\nCleric: \"Cleric talented. Have a good day!\"",
                _ => "You don't trust this \"Cleric\", so you leave.",
            };
        }
    }
}
