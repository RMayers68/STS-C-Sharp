namespace STV
{
    public class CouncilOfGhosts : Event
    {
        public CouncilOfGhosts()
        {
            Name = "Council Of Ghosts";
            StartOfEncounter = $"As you continue your ascent, thick black smoke begins to billow out of the ground and walls around you, coalescing into three masked forms that start to speak.\nShape #1: \"Another puppet of Neow I think.\"\nShape #2: \"AGREED! SHE ALWAYS MAKES THE FUNNEST TOYS!\"\nYou notice an over-sized grin as the third addresses you.\nShape #3: \"Ignore the others... Would you like a taste of our power?\"";
            Options = new() { "[A]ccept - Receive 5 Apparition. Lose 50% Max HP.", "[P]urify - 50 Gold. Remove a card", "[R]efuse" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "A", "R" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "A")
            {
                Console.WriteLine(Result(0));
                hero.SetMaxHP(hero.MaxHP / 2 * -1);
                for (int i = 0; i < 5; i++)
                    hero.AddToDeck(new Apparition());
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Shape #3: \"Excellent!\"\nAs the ghostly shape speaks, you notice its large mouth opening wider and wider. Thick black smoke spews forth and envelops the room. You cannot see or breathe...\nJust before you lose consciousness, the sensation stops.\nWhatever those things were, they are gone now. You continue on, feeling rather hollow.",
                _ => "Shape #3: \"How disappointing...\"\nShape #1: \"You will join us sooner or later.\"\nShape #2: \"HA HA HA HAHAHA!\"\nThe shapes fade away, leaving only the unnerving laughter ringing in your ears.",
            };
        }
    }
}
