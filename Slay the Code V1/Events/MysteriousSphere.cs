namespace STV
{
    public class MysteriousSphere : Event
    {
        public MysteriousSphere()
        {
            Name = "Mysterious Sphere";
            StartOfEncounter = $"Jutting from the chaotic terrain around you, a bony sphere surrounds a mysterious glowing object within.\r\nWhile you are curious what lies inside, you notice some Orb Walkers keeping an eye on it.\"";
            Options = new() { "[O]pen Sphere - Fight and win a Rare Relic", "[L]eave" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "O", "L" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "O")
            {
                Console.WriteLine(Result(0));
                hero.Encounter = Enemy.CreateEncounter(15, 1);
                Battle.Combat(hero, hero.Encounter);
                if (hero.Hp < 0) return;
                else hero.CombatRewards("Elite");
            }
            else Console.WriteLine(Result(1));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "As soon as you strike the sphere, the Orb Walkers spring to life around you!",
                _ => "No need to be greedy.",
            };
        }
    }
}
