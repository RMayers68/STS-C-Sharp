
namespace STV
{
    public class Prostrate : Card
    {
        public Prostrate(bool Upgraded = false)
        {
            Name = "Prostrate";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 4;
            BlockLoops = 1;
            BuffID = 10;
            BuffAmount = 2;
            HeroBuff = true;
            if (Upgraded)
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
            return DescriptionModifier + $"Gain {MagicNumber} Mantra. Gain {BlockAmount} Block.";
        }

        public override Card AddCard()
        {
            return new Prostrate();
        }
    }
}