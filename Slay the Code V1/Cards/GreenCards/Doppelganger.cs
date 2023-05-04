
namespace STV
{
    public class Doppelganger : Card
    {
        public Doppelganger(bool Upgraded = false)
        {
            Name = "Doppelganger";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = -1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 22;
            MagicNumber = 0;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, hero.Energy + MagicNumber);
            hero.AddBuff(45, hero.Energy + MagicNumber);
            hero.Energy = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Next turn, draw X{(Upgraded ? $"+1" : "")} cards and gain X{(Upgraded ? $"+1" : "")} Energy.";
        }

        public override Card AddCard()
        {
            return new Doppelganger();
        }
    }
}