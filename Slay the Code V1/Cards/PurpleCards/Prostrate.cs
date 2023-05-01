
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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 4;
            BuffID = 10;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
            hero.CardBlock(BlockAmount, encounter);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
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