using System;

namespace STV
{
    public class Vampires : Event
    {
        public Vampires()
        {
            Name = "Vampires(?)";
            StartOfEncounter = $"Navigating an unlit street, you come across several hooded figures in the midst of some dark ritual.\nAs you approach, they turn to you in eerie unison. The tallest among them bares fanged teeth and extends a long, pale hand towards you.";
            Options = new() { "[O]ffer Blood Vial - Remove all Strikes & Receive 5 Bites.", "[A]ccept Remove all Strikes. Receive 5 Bites. Lose 30% Max HP.", "[R]efuse" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "O", "A", "R" };
            if (!hero.HasRelic("Blood Vial"))
            {
                Options[0] = "Locked - Requires Blood Vial";
                choices.Remove("O");
            }
            Console.WriteLine($"{StartOfEncounter}");
            if (hero.Name == "Ironclad")
                Console.WriteLine("Fanged Stranger: \"Join us brother, and feel the warmth of the Spire.\"");
            else if (hero.Name == "Defect")
                Console.WriteLine("Fanged Stranger: \"Join us broken one, and feel the warmth of the Spire.\"");
            else Console.WriteLine("Fanged Stranger: \"Join us sister, and feel the warmth of the Spire.\"");
            Console.WriteLine($"{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "O")
            {
                Console.WriteLine(Result(0));
                hero.Deck.RemoveAll(x => x.Name == "Strike");
                for (int i = 0; i < 5; i++)
                    hero.AddToDeck(new Bite());
            }
            else if (playerChoice == "A")
            {
                Console.WriteLine(Result(1));
                hero.Deck.RemoveAll(x => x.Name == "Strike");
                for (int i = 0; i < 5; i++)
                    hero.AddToDeck(new Bite());
                hero.SetMaxHP(Convert.ToInt32(hero.MaxHP * 0.3) * -1);
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "The pale figures gasp as you take out the Blood Vial.\nPale Figures: \"The master's blood... the master's blood! THE MASTER'S BLOOD!\"\nThey all chant fervently as the tall one bows before you.\nFanged Stranger: \"Drink from His blood, and become one with Him.\"\nThe chant growing louder, you consume the contents of the vial. Your vision immediately warps and fades to darkness.\nYou wake up some time later, alone. An intense hunger passes through your belly. You must feed.",
                1 => "The tall figure grabs your arm, pulls you forward, and sinks his fangs into your neck. You feel a dark force pour into your neck and course through your body...\n\nYou wake up some time later, alone. An intense hunger passes through your belly. You must feed.",
                _ => "You step back and raise your weapon in defiance. The tall figure sighs.\nFanged Stranger: \"Very well.\"\nThe entire group of hooded figures morph into a thick black fog that flows away from you.\nYou are alone once more.",
            };
        }
    }
}
