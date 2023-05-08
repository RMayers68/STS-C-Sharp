namespace STV
{
    public class FaceTrader : Event
    {
        public FaceTrader()
        {
            Name = "Face Trader";
            StartOfEncounter = $"You walk by an eerie statue holding several masks...\r\nSomething behind you softly whispers, \"Stop.\"\r\nYou swerve around to face the statue which is now facing you!\r\nOn closer inspection, it's not a statue but a statuesque, gaunt man. Is he even breathing?\r\nEerie Man: \"Face. Let me feel? Maybe trade?\"";
            Options = new() { "[F]eel Take damage equal to 10% of Max HP and gain 75 Gold", "[T]rade - 50% Good Face, 50% Bad Face", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "T", "L", "F", };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "F")
            {
                Console.WriteLine(Result(0));
                hero.GoldChange(75);
                hero.NonAttackDamage(hero, hero.MaxHP / 10,"the Eerie Man's creepy hands");
            }
            else if (playerChoice == "T")
            {
                Console.WriteLine(Result(1));
                int face = EventRNG.Next(5);
                if (face == 0)
                    hero.AddToRelics(Dict.relicL[176]);
                else if (face == 1)
                    hero.AddToRelics(Dict.relicL[169]);
                else if (face == 2)
                    hero.AddToRelics(Dict.relicL[165]);
                else if (face == 3)
                    hero.AddToRelics(Dict.relicL[163]);
                else hero.AddToRelics(Dict.relicL[161]);
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Eerie Man: \"Compensation. Compensation.\"\r\nMechanically, he cranes out a neat stack of gold and places it into your pouch.\r\nEerie Man: \"What a nice face. Nice face.\"\r\nWhile he touches your face, you begin to feel your life drain out of it!\r\nDuring this, his mask falls off and shatters. Screaming, he quickly covers his face with all six arms dropping even more masks! Amidst all the screaming and shattering, you escape.\r\nHis face was completely blank.",
                1 => "Eerie Man: \"For me? FOR ME? Oh yes.. Yes. Yes.. mmm...\"\r\nYou see one of his arms flicker, and your face is in its hand! Your face has been swapped.\r\nEerie Man: \"Nice face. Nice face.\"",
                _ => "Eerie Man: \"Stop. Stop. Stop. Stop. Stop.\"\r\nThis was probably the right call.",
            };
        }
    }
}
