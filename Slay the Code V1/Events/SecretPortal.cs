namespace STV
{
    public class SecretPortal : Event
    {
        public SecretPortal()
        {
            Name = "Secret Portal";
            StartOfEncounter = $"Before you is a sight that seems out of place in the alien landscape around you.\nStrangely placed into one of the living walls of the Beyond is an enclosed stone entrance filled with a swirling magical portal.\nYou aren't sure where it leads, but maybe it could speed your journey through the Spire.";
            Options = new() { "[E]nter the Portal - Immediately travel to the Boss", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "E", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "E")
            {
                Console.WriteLine(Result(0));
                // Code to skip to End Boss Fight
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Jumping through the portal, your sense of time and space is completely torn apart.\nAs you reorient yourself to the new surroundings, you realize that right before you is a fearsome battle.",
                _ => "Careful and cautious seems the better approach for reaching the top of the Spire. Ignoring the portal you continue on.",
            };
        }
    }
}
