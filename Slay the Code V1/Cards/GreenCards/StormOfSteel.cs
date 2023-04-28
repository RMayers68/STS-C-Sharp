
namespace STV
{
    public class StormofSteel : Card
    {
        public StormofSteel(bool Upgraded = false)
        {
            Name = "Storm of Steel";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            SelfDamage = true;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) ;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Discard your hand. Add 1 Shiv{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new StormofSteel();
                }
        }
}