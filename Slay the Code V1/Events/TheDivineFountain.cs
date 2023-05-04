namespace STV
{
    public class TheDivineFountain : Event
    {
        public TheDivineFountain()
        {
            Name = "The Divine Fountain";
            StartOfEncounter = $"You come across shimmering water flowing endlessly from a fountain on a nearby wall.";
            Options = new() { "[D]rink","[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]} - Remove All Curses\n{Options[1]}");
            string playerChoice = "";
            while (playerChoice != "L" && playerChoice != "D")
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "D")
            {
                hero.Deck.RemoveAll(x => x.Type == "Curse");
                Console.WriteLine(Result(0));
            }
            else Console.WriteLine(Result(1));
        }           

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As you drink the water, you feel a dark grasp loosen.",
                _ => "Unsure of the nature of this water, you continue on your way, parched.\r\n",
            };
        }
    }
}
