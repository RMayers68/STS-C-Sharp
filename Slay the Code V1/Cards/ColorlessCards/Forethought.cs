
namespace STV
{
    public class Forethought : Card
    {
        public Forethought(bool Upgraded = false)
        {
            Name = "Forethought";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card forethought;
            if (Upgraded)
            {
                int forethoughtChoice = -1;
                int forethoughtAmount = hero.Hand.Count;
                while (forethoughtChoice != 0 && forethoughtAmount > 0)
                {
                    Console.WriteLine($"\nEnter the number of the card you would like to move to your drawpile or hit 0 to move on.");
                    for (int i = 1; i <= forethoughtAmount; i++)
                        Console.WriteLine($"{i}:{hero.DrawPile[hero.Hand.Count - i].Name}");
                    while (!Int32.TryParse(Console.ReadLine(), out forethoughtChoice) || forethoughtChoice < 0 || forethoughtChoice > forethoughtAmount)
                        Console.WriteLine("Invalid input, enter again:");
                    if (forethoughtChoice > 0)
                    {
                        forethought = hero.Hand[^forethoughtChoice];
                        forethought.TmpEnergyCost = 0;
                        hero.Hand.Remove(forethought);
                        hero.DrawPile = hero.DrawPile.Prepend(forethought).ToList();
                        forethoughtAmount--;
                    }
                }
            }
            else
            {
                forethought = PickCard(hero.Hand, "move to your drawpile");
                forethought.TmpEnergyCost = 0;
                hero.Hand.Remove(forethought);
                hero.DrawPile = hero.DrawPile.Prepend(forethought).ToList();
            }
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Place {(Upgraded ? $"any number of cards" : "a card")} from your hand on the bottom of your draw pile. {(Upgraded ? $"They" : "It")} costs 0 until played.";
        }

        public override Card AddCard()
        {
            return new Forethought();
        }
    }
}