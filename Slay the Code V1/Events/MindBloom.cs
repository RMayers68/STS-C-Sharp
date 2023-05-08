namespace STV
{
    public class MindBloom : Event
    {
        public MindBloom()
        {
            Name = "Mind Bloom";
            StartOfEncounter = $"While walking and traversing through the chaos of the Spire, your thoughts suddenly begin to feel very... real...\nImaginings of monsters and riches begin to manifest themselves into reality.\nThe sensation is quickly fleeting. What do you do?";
            Options = new() { "I am [W]ar - Fight a Boss from Act 1 & Obtain a Rare Relic", "I am [A]wake - Upgrade all Cards. Can no longer Heal", "I am [R]ich - Gain 999 gold. Become Cursed with Normalities." };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "W", "A", "R" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "W")
            {
                hero.Encounter = Enemy.CreateEncounter(15);
                if (hero.HasRelic("Pantograph"))
                    hero.CombatHeal(25);
                if (hero.HasRelic("Slaver's"))
                {
                    hero.MaxEnergy++;
                    Battle.Combat(hero, hero.Encounter);
                    hero.MaxEnergy--;
                }
                else Battle.Combat(hero, hero.Encounter);
                if (hero.Hp < 0) return;
                else hero.CombatRewards("Elite");
            }
            else if (playerChoice == "A")
            {
                foreach (Card c in hero.Deck)
                    c.UpgradeCard();
                hero.AddToRelics(Dict.relicL[166]);
                Console.WriteLine(Result(0));
            }
            else
            {
                hero.GoldChange(999);
                for (int i = 0; i < 2; i++)
                    hero.AddToDeck(new Normality());
                Console.WriteLine(Result(1));
            }
        }
        public override string Result(int result)
        {
            return result switch
            {
                0 => "Can it really be this easy?",
                _ => "Everything makes sense now.\r\nThe lack of memories, the ascent, the Ancient One.\r\nThis is the way it always was.\r\nThis is the way it always will be.\r\nAll will be forgotten again soon...",
            };
        }
    }
}
