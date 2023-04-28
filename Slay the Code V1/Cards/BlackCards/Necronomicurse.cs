namespace STV
{
    public class Necronomicurse : Card
    {
        public Necronomicurse()
        {
            Name = "Necronomicurse";
            Type = "Curse";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = -2;
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.Hand.Add(AddCard());
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Unplayable. There is no escape from this Curse.";
        }

        public override Card AddCard()
        {
            return new Necronomicurse();
        }
    }
}