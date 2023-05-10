namespace STV
{
    public class CursedTome : Event
    {
        public CursedTome()
        {
            Name = "CursedTome";
            StartOfEncounter = $"In an abandoned temple, you find a giant book, open, riddled with cryptic writings.\nAs you try to interpret the elaborate script, it begins shift and morph into writing you are familiar with.";
            Options = new() { "[R]ead", "[P]urify - 50 Gold. Remove a card", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "R", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "R")
            {
                Console.WriteLine(Result(0));
                choices = new() { "C" };
                Options = new() { "[C]ontinue - Lose 1 HP" };
                Console.WriteLine($"{StartOfEncounter}\n{Options[0]}");
                playerChoice = "";
                while (!choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                Console.WriteLine(Result(1));
                hero.SelfDamage(1);
                Options = new() { "[C]ontinue - Lose 2 HP" };
                playerChoice = "";
                while (!choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                Console.WriteLine(Result(2));
                hero.SelfDamage(2);
                Options = new() { "[C]ontinue - Lose 3 HP" };
                playerChoice = "";
                while (!choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                Console.WriteLine(Result(3));
                hero.SelfDamage(3);
                choices = new() { "T", "S" };
                Options = new() { "[T]ake - Obtain the Book, Lose 10 HP" , "[S]top - Lose 3 HP" };
                playerChoice = "";
                while (!choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                if (playerChoice == "T")
                {
                    Console.WriteLine(Result(4));
                    int bookRelic = EventRNG.Next(3);
                    if (bookRelic == 0)
                        hero.AddToRelics(Dict.relicL[162]);
                    else if (bookRelic == 1)
                        hero.AddToRelics(Dict.relicL[170]);
                    else hero.AddToRelics(Dict.relicL[172]);
                    hero.SelfDamage(10);
                }
                else
                {
                    Console.WriteLine(Result(5));
                    hero.SelfDamage(3);
                }
            }
            else Console.WriteLine(Result(6));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Odd. The book seems to be about an Ancient named Neow.\r\nThis piques your interest, but you have a general feeling of malaise.",
                1 => "The Ancient of resurrection, Neow, was exiled to the bottom of the Spire.\r\nYou feel compelled to read more, but your body begins to ache.",
                2 => "Seeking vengeance, Neow blesses outsiders, using them for her own purposes.\r\nYou are starting to feel very weak and tired.",
                3 => "Those resurrected by Neow remember only fragments of their past selves, cursed to fight for eternity.\r\nAs you near the final page, your old wounds begin to reopen!",
                4 => "Upon finishing the tome, you decide to take it with you. With proof in hand, will you retain your memories?",
                5 => "With incredible strain and willpower, you resist the trance of the tome and SLAM it shut.\r\nYou turn and exit the temple, feeling drained...",
                _ => "You exit, feeling a dark energy emanating from the book on the pedestal.",
            };
        }
    }
}
