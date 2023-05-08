namespace STV
{
    public class TheLibrary : Event
    {
        public TheLibrary()
        {
            Name = "The Library";
            StartOfEncounter = $"You come across an ornate building which appears abandoned.\nA plaque that has been torn free from a wall is on the floor. It reads, \"THE LIBRARY\".\nInside, you find countless rows of scrolls, manuscripts, and books.\nYou pick one and cozy yourself into a chair for some quiet time.";
            Options = new() { "[R]ead - Choose 1 of 20 cards to add to your deck.", "[S]leep - Heal 1/3 of your max HP." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "R", "S" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "R")
            {
                hero.AddToDeck(Card.PickCard(Card.RandomCards(hero.Name, 20), "add"));
                Console.WriteLine(Result(EventRNG.Next(3)));
            }
            else
            {
                Console.WriteLine(Result(3));
                hero.HealHP(hero.MaxHP / 3);
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "The story is about an insect-controlling teenage girl who aspires to become a hero.\nThe book is filled with creative uses of powers, combat strategies, and varying perspectives.\nSatisfying.",
                1 => "The story is about a man who journeyed beyond the stars and found himself stuck on a desolate foreign planet.\nIngenuity, luck, perseverance, and humor to retain his sanity were his tools to return home.\nFascinating.",
                2 => "The story takes place in a giant isolated building underground as the outside conditions have become unbearable.\nThe novel is mired with conspiracies, propaganda, and injustice. You ponder if similar dynamics are at play within the Spire.\r\nUnsettling.",
                _ => "Reading is for chumps.\r\nYou doze off in a comfy chair instead.\r\nZzz... zzz... ..Zz....\r\nYou wake up feeling refreshed.",
            };
        }
    }
}