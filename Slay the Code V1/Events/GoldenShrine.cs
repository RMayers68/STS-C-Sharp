namespace STV
{
    public class GoldenShrine : Event
    {
        public GoldenShrine()
        {
            Name = "Golden Shrine";
            StartOfEncounter = $"Before you lies an elaborate shrine to a forgotten spirit.";
            Options = new() { "[P]ray - Gain 100 Gold", "[D]esecrate Gain 275 Gold. Become Cursed with Regret", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "P","D", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                hero.GoldChange(100);
                Console.WriteLine(Result(0));
            }
            else if (playerChoice == "D")
            {
                hero.GoldChange(275);
                Console.WriteLine(Result(1));
                hero.AddToDeck(new Regret());
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As your hand touches the shrine, gold rains from the ceiling showering you in riches.",
                1 => "Each time you strike the shrine, gold pours forth again and again!\r\nAs you pocket the riches, something weighs heavily on you.",
                _ => "You ignore the shrine.",
            };
        }
    }
}
