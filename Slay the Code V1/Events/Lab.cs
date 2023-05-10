namespace STV
{
    public class Lab : Event
    {
        public Lab()
        {
            Name = "The Labaratory";
            StartOfEncounter = $"You find yourself in a room filled with racks of test tubes, beakers, flasks, forceps, pinch clamps, stirring rods, tongs, goggles, funnels, pipets, cylinders, condensers, and even a rare spiral tube of glass.\r\nWhy do you know the name of all these tools? It doesn't matter, you take a look around.";
            Options = new() { "[S]earch" };
        }

        public override void EventAction(Hero hero)
        {
            Console.WriteLine($"{StartOfEncounter}\n{Result(0)}");
            int playerChoice = -1;
            int amount = 3;
            List<Potion> potList = new() { Potion.RandomPotion(hero), Potion.RandomPotion(hero), Potion.RandomPotion(hero) };
            while (playerChoice != 0 && amount > 0)
            {
                Console.WriteLine($"\nEnter the number of the potion you would like to take or hit 0 to move on.");
                for (int i = 1; i <= amount; i++)
                    Console.WriteLine($"{i}:{potList[^i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 0 || playerChoice > amount)
                    Console.WriteLine("Invalid input, enter again:");
                if (playerChoice > 0)
                {
                    hero.AddToPotions(potList[^playerChoice]);
                    amount--;
                }
            }
        }

        public override string Result(int result)
        {
            return "You found a few potions lying about, do you dare to swipe any?";
        }
    }
}
