namespace STV
{
    public class Nloth : Event
    {
        string relicName = "";
        public Nloth()
        {
            Name = "N'loth";
            StartOfEncounter = $"An odd creature with a hunched back sprouting several tentacles is scrounging through a pile of trash and debris in front of you.\nAs you approach, he shuffles towards you in a non-threatening manner.\n\"N'loth hungry. Feed N'loth.\"";
            Options = new() { "[O]ffer Relic - Receive Gift", "[L]eave" };
            relicName = "Relic";
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "O", "L" };
            relicName = hero.Relics[EventRNG.Next(hero.Relics.Count)].Name;
            Options[0] = $"[O]ffer {relicName} - Receive Gift";
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "O")
            {
                hero.Relics.Remove(hero.FindRelic(relicName));
                Console.WriteLine(Result(0));
                hero.AddToRelics(Dict.relicL[168]);
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Holding the relic out towards him, N’loth snatches it out of your hand with his tentacles, dislocates his jaw, and slurps down your offer in one quick gulp.\nHe gives you a large, toothy grin as more tentacles appear from behind his cloak, these ones brandishing an impossibly neat looking box.\nHe pushes it towards you until you take it.",
                _ => "You shake your head. N'loth hunches even further and sighs, then scuttles away.",
            };
        }
    }
}
