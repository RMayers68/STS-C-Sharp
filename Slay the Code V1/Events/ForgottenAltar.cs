namespace STV
{
    public class ForgottenAltar : Event
    {
        public ForgottenAltar()
        {
            Name = "Forgotten Altar";
            StartOfEncounter = $"In front of you sits an altar to a forgotten god.\nAtop the altar sits an ornate female statue with arms outstretched.\nShe calls out to you, demanding sacrifice.";
            Options = new() { "[O]ffer - Obtain a special Relic. Lose Golden Idol", "[S]acrifice - Gain 5 Max HP. Take damage equal to 25% of Max HP.", "[D]esecrate - Become Cursed with Decay." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "O", "S", "D" };
            if (!hero.HasRelic("Golden Idol"))
            {
                Options[1] = "Locked - Requires Golden Idol";
                choices.Remove("O");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "O")
            {
                Console.WriteLine(Result(0));
                hero.Relics.Remove(hero.FindRelic("Golden Idol"));
                hero.AddToRelics(Dict.relicL[160]);
            }
            else if (playerChoice == "S")
            {
                Console.WriteLine(Result(1));
                hero.SetMaxHP(5);
                hero.SelfDamage(hero.MaxHP / 4);
            }
            else
            {
                Console.WriteLine(Result(2));
                hero.AddToDeck(new Decay());
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As you gently set the idol onto the altar a cold wind swirls throughout the room.\nThe arms of the statue begin to discolor and crumble.\nYour golden idol begins to dull in color and begins bleeding from its eyes.\nThe bleeding never ceases.",
                1 => "You stand on the altar and cut your wrists.\nAs the blood spills out in sacrifice, the arms of the statue reach out and close around your eyes.\nEverything goes dark.\nYou wake up a short time later feeling a new potential surging through you.",
                _ => "You lash out and smash the statue in front of you, breaking the magical hold the room had placed upon you.\nA dark wail echoes all around you, and you can feel the cursed magic seep into your bones.",
            };
        }
    }
}
