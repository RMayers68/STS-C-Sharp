
namespace STV
{
    public class Equilibrium : Card
    {
        public Equilibrium(bool Upgraded = false)
        {
            Name = "Equilibrium";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 13;
            BlockLoops = 1;
            BuffID = 69;
            BuffAmount = 1;
            HeroBuff = true;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. Retain your hand this turn.";
        }

        public override Card AddCard()
        {
            return new Equilibrium();
        }
    }
}