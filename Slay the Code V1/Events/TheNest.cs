namespace STV
{
    public class TheNest : Event
    {
        public TheNest()
        {
            Name = "The Nest";
            StartOfEncounter = $"A long line of hooded figures can be seen entering an unassuming cathedral.\nNaturally, you join the line and are quickly surrounded by Cultists!\nThey ignore you as they gleefully chant and wave their weapons around.\nCultists: \"MURDER!! MURDER MURDER!!\"\nCultists: \"CAW CAW CAAAAAWWW!\"\nCultists: \"MURDER! MURDER MUURDER!!\"\nCultists: \"CAAW CAW CAAAAAAWW!!\"\nYou eye a Donation Box...";
            Options = new() { "[G]rab Box - Obtain 99 Gold.", "[S]tay in Line - Obtain Ritual Dagger. Lose 6 HP." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "G", "S" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "G")
            {
                Console.WriteLine(Result(0));
                hero.GoldChange(99);
            }
            else
            {
                Console.WriteLine(Result(1));
                hero.SelfDamage(6);
                hero.AddToDeck(new RitualDagger());
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "They didn't even notice.",
                _ => "You decide to stay in line (out of fear) to see what will happen.\nEventually, you are face-to-face with the leader. A well-dressed cultist hands you an Ornate Dagger. Like the others before you, you slash your forearm and let the blood drip into a misshapen bowl.\nThe cultists chant and holler for you!\nCultists: \"CAAW CAW CAAAAAAWW!!\"\nYou chant, too. Why not?",
            };
        }
    }
}