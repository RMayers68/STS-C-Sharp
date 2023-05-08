
namespace STV
{
    public class WingStatue : Event
    {
        public WingStatue()
        {
            Name = "Wing Statue";
            StartOfEncounter = $"Among the stone and boulders, you notice an intricate large blue statue resembling a wing.\r\nYou find gold spilling from its cracks. Maybe there is more inside...";
            Options = new() { "[P]ray Remove a card from your deck. Lose 7 HP.", "[D]estroy Gain 50-80 Gold.", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "D", "P", "L" };
            if (hero.Deck.FindAll(x => x.AttackDamage >= 10).Count < 1)
            {
                Options[1] = "Locked - Requires card with 10 or more damage per hit";
                choices.Remove("D");
            }
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "P")
            {
                Console.WriteLine(Result(0));
                hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
            }
            else if (playerChoice == "D")
            {                
                Console.WriteLine(Result(1));
                hero.GoldChange(EventRNG.Next(50, 81));
            }
            else Console.WriteLine(Result(2));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Someone once told you of a cult that worshipped a giant bird. As you kneel in prayer, you begin to feel ... lightheaded.\nYou wake up some time later, feeling strangely fleet of foot.",
                1 => "With all your might, you hack away at the statue.\nIt soon crumbles, revealing a pile of gold. You grab as much as you can and continue onwards.",
                _ => "The statue makes you feel uneasy. You walk past and continue onward.",
            };
        }
    }
}
