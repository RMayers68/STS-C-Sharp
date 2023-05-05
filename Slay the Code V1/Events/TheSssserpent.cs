namespace STV
{
    public class TheSssserpent : Event
    {
        public TheSssserpent()
        {
            Name = "The Sssserpent";
            StartOfEncounter = $"You walk into a room to find a large hole in the ground. As you approach the hole, an enormous serpent creature appears from within.\nSerpent: \"Ho hooo! Hello hello! what have we got here? Hello adventurer, I ask a simple question.\"\nSerpent: \"The most fulfilling of lives is that in which you can buy anything!\"\nSerpent: \"Do you agree?\"";
            Options = new() { "[A]gree Receive 175 Gold. Cursed with Doubt.", "[D]isagree" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "A", "D" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "A")
            {
                Console.WriteLine(Result(0));
                hero.GoldChange(175);
                hero.AddToDeck(new Doubt());
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Serpent: \"Yeeeeeeessssssssssessss\"\nSerpent: \"Thisss will all be worthhh it.\"\nSerpent: \"..ssSSs..... ss... sssss....!\"\nThe serpent rears its head and blasts a stream of gold upwards!\nIt is amazing and terrifying simultaneously.\nYou gather all the gold, thank the snake, and get going.",
                _ => "The serpent stares at you with a look of extreme disappointment.",
            };
        }
    }
}
