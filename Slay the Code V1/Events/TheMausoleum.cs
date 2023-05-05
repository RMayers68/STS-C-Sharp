namespace STV
{
    public class TheMausoleum : Event
    {
        public TheMausoleum()
        {
            Name = "The Mausoleum";
            StartOfEncounter = $"Venturing through a series of tombs, you are faced with a large sarcophagus studded with gems in the center of a circular room.\nYou cannot make out the writing on the coffin, however, you do notice black fog seeping out from the sides.";
            Options = new() { "[O]pen Coffin - Obtain a relic. 50% Become Cursed with Writhe", "[L]eave" };
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
                hero.AddToRelics(Relic.RandomRelic());
                if (EventRNG.Next(2) == 0)
                {
                    Console.WriteLine(Result(0));
                    hero.AddToDeck(new Writhe());
                }
                else Console.WriteLine(Result(1));
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "You push open the coffin. As you do, black fog spews forth and covers the entire room!\nInside, you find no body, only a relic. You take it and move onwards, coughing violently.",
                1 => "You push open the coffin. The fog dissipates harmlessly.\nInside, you find the mortal remains of a decorated soldier grasping an old relic.. You pilfer it and move on.",
                _ => "You continue along your way, leaving the forgotten dead undisturbed.",
            };
        }
    }
}
