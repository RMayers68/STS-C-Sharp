
namespace STV
{
    public class TrueGrit : Card
    {
        public TrueGrit(bool Upgraded = false)
        {
            Name = "True Grit";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 7;
            BlockLoops = 1;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Exhaust a{(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new TrueGrit();
                }
        }
}
