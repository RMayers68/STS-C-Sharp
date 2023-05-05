namespace STV
{
    public class WindingHalls : Event
    {
        public WindingHalls()
        {
            Name = "Winding Halls";
            StartOfEncounter = $"As you slowly make your way up the twisting pathways, you constantly find yourself losing your way as the walls and ground seem to inexplicably shift before your eyes.\nThe constant whispering voices in the back of your head aren't helping things either.\nPassing by a structure you are certain you have previously seen you start to question if you are going insane, or if the impossible geography of this place is starting to get to you.\nYou need to change something, and soon.\nThat's what the voices say anyway, and why would they lie?";
            Options = new() { "[E]mbrace Madness - Receive 2 Madness, Lose HP equal to 12.5% Max HP.", "[P]ress On - Become Cursed with Writhe, Heal 1/4 of Max HP.", "[R]etrace Your Steps - Lose 5% Max HP." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "E", "P", "R" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "E")
            {
                for (int i = 0; i < 2; i++)
                    hero.AddToDeck(new Madness());
                Console.WriteLine(Result(0));
                hero.SelfDamage(Convert.ToInt32(hero.MaxHP * 0.125) * -1);
            }
            else if (playerChoice == "P")
            {
                hero.AddToDeck(new Writhe());
                Console.WriteLine(Result(1));
                hero.HealHP(hero.MaxHP / 4);
            }
            else
            {
                Console.WriteLine(Result(2));
                hero.SetMaxHP(hero.MaxHP / 20 * -1);
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Something in you cracks.\nOnly the truly mad can understand a place like this, so you give into the chattering voices and continue on with a \"new\" perspective.\nThings do seem to make so much more sense now.",
                1 => "As you take a moment to stop and carefully observe the undulating landscape around you, the hint of a pattern starts to emerge from within the randomness.\nWhenever the demented noises begin to interrupt your thoughts, you struggle through the mental pain and ignore it.\nEventually you successfully map out a path forward, and continue on, now resistant to the nefarious nature of this alien place.",
                _ => "You spend what seems like an eternity lost in the maze.\nSlowly but surely, you are able to retrace your steps, reorient yourself, and make it out of the twisting passages.\nYou feel drained from the experience.",
            };
        }
    }
}