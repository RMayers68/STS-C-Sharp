namespace STV
{
    public class WorldOfGoop : Event
    {
        int gold;
        public WorldOfGoop()
        {
            Name = "World Of Goop";
            StartOfEncounter = $"You fall into a puddle.\nIT'S MADE OF SLIME GOOP!!\nFrantically, you claw yourself out over several minutes as you feel the goop starting to burn.\nYou can feel goop in your ears, goop in your nose, goop everywhere.\nClimbing out, you notice that some of your gold is missing. \nLooking back to the puddle you see your missing coins combined with gold from unfortunate adventurers mixed together in the puddle.";
            Options = new() { "[G]ather Gold - Gain 75 Gold, Lose 11 HP.", $"[L]eave It - Lose {gold} gold." };
            gold = 20;
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "G", "L" };
            if (hero.Gold < 50)
                gold = EventRNG.Next(20, hero.Gold);
            else gold = EventRNG.Next(20, 50);
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "G")
            {
                hero.Gold += 75;
                hero.NonAttackDamage(hero, 11, "Goop Burn");
                Console.WriteLine(Result(0));
            }
            else
            {
                hero.GoldChange(-1 * gold);
                Console.WriteLine(Result(1));
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Feeling the sting of the goop as the prolonged exposure starts to melt away at your skin, you manage to fish out the gold.",
                _ => "You decide that mess is not worth it.",
            };
        }
    }
}
