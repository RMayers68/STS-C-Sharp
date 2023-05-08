namespace STV
{
    public class GoldenIdol : Event
    {
        public GoldenIdol()
        {
            Name = "Golden Idol";
            StartOfEncounter = $"You come across an inconspicuous pedestal with a shining gold idol sitting peacefully atop. It looks incredibly valuable.\nYou sure don't see any traps nearby.";
            Options = new() { "[T]ake - Obtain Golden Idol","[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "T", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "T")
            {
                Console.WriteLine(Result(0));
                Options = new() { "[O]utrun - Cursed with Injury", "[S]mash - Damage equal to 1/4 of Max HP", "[H]ide Lose 1/10 Max HP" };
                choices = new() { "O", "S","H" };
                Console.WriteLine($"{Options[0]}\n{Options[1]}\n{Options[2]}");
                playerChoice = "";
                while (!choices.Any(x => x == playerChoice))
                {
                    playerChoice = Console.ReadLine().ToUpper();
                }
                if (playerChoice == "O")
                {
                    Console.WriteLine(Result(1));
                    hero.AddToDeck(new Injury());
                }
                else if (playerChoice == "S")
                {
                    Console.WriteLine(Result(2));
                    hero.NonAttackDamage(hero, hero.MaxHP / 4, "Huge Boulder");
                }
                else
                {
                    Console.WriteLine(Result(3));
                    hero.SetMaxHP(-1 * hero.MaxHP / 10);
                }
            }
            else Console.WriteLine(Result(4));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As you grab the Idol and stow it away, a giant boulder smashes through the ceiling into the ground next to you.\r\nYou realize that the floor is slanted downwards as the boulder starts to roll towards you.",
                1 => "RUUUUUUUUUUN!\r\nYou barely leap into a side passageway as the boulder rushes by. Unfortunately it feels like you sprained something however.",
                2 => "You throw yourself at the boulder with everything you have. When the dust clears, you can make a safe way out.",
                3 => "SQUISH!\r\nThe boulder flattens you a little as it passes by, but otherwise you can get out of here.",
                _ => "If there was ever an obvious trap, this would be it.\r\nYou decide not to interfere with objects placed upon pedestals.",
            };
        }
    }
}
