namespace STV
{
    public class HypnotizingColoredMushrooms : Event
    {
        public HypnotizingColoredMushrooms()
        {
            Name = "Hypnotizing Colored Mushrooms";
            StartOfEncounter = $"You enter a corridor full of hypnotizing colored mushrooms.\nDue to your lack of specialization in mycology you are unable to identify the specimens.\nYou want to escape, but feel oddly compelled to eat a mushroom.";
            Options = new() { "[S]tomp - Anger the Mushrooms.", "[E]at - Heal 1/4 HP. Become Cursed with a Parasite." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "S", "E" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "S")
            {
                Console.WriteLine(Result(0));
                hero.Encounter = Enemy.CreateEncounter(5, 1);
                Battle.Combat(hero, hero.Encounter);
                if (hero.Hp <= 0) return;
                else
                {
                    hero.CombatRewards("Monster");
                    hero.AddToRelics(Dict.relicL[173]);
                }
            }
            else
            {
                Console.WriteLine(Result(1));
                hero.HealHP(hero.MaxHP / 4);
                hero.AddToDeck(new Parasite());
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "Ambushed!!\r\nRodents infested by the mushrooms appear out of nowhere!",
                _ => "You give in to the unnatural desire to eat.\nAs you consume mushroom after mushroom, you feel yourself entering into a daze and pass out. As you awake, you feel very odd.\nYou Heal 25% of your HP, but you also get infected.",
            };
        }
    }
}
