namespace STV
{
    public class TheWomanInBlue : Event
    {
        public TheWomanInBlue()
        {
            Name = "The Woman In Blue";
            StartOfEncounter = $"From the darkness, an arm pulls you into a small shop. As your eyes adjust, you see a pale woman in sharp clothes gesturing towards a wall of potions.\r\nPale Woman: \"Buy a potion. Now!\" she states.";
            Options = new() { "Buy [1] Potion - 20 Gold.\r\n", "Buy [2] Potions - 30 Gold.", "Buy [3] Potions - 40 Gold.", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "1", "2", "3", "L" };
            List<Potion> potions = new() { new Potion(Potion.RandomPotion(hero)), new Potion(Potion.RandomPotion(hero)), new Potion(Potion.RandomPotion(hero)) };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}\n{Options[3]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (Int32.TryParse(playerChoice, out int amount))
            {
                int potionChoice = -1;
                while (potionChoice != 0 && amount > 0)
                {
                    Console.WriteLine($"\nEnter the number of the potion you would like to take or hit 0 to move on.");
                    for (int i = 1; i <= amount; i++)
                        Console.WriteLine($"{i}:{potions[^i].Name}");
                    while (!Int32.TryParse(Console.ReadLine(), out potionChoice) || potionChoice < 0 || potionChoice > amount)
                        Console.WriteLine("Invalid input, enter again:");
                    if (potionChoice > 0)
                    {
                        hero.AddToPotions(potions[^potionChoice]);
                        amount--;
                    }
                }
                Console.WriteLine(Result(0));
            }
            else
            {
                Console.WriteLine(Result(1));
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Pale Woman: \"Good. Now leave.\"\r\nYou exit the shop cautiously.",
                _ => "WHAM\r\nHer gloved fist collides with your face, nearly knocking you off your feet.\r\nPale Woman: \"Get out before I litter the floor with your guts.\"\r\nYou take her word and exit with your guts still safely in your body.",
            };
        }
    }
}
