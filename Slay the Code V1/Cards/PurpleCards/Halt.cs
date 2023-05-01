
namespace STV
{
    public class Halt : Card
    {
        public Halt(bool Upgraded = false)
        {
            Name = "Halt";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 3;
            BlockLoops = 1;
            MagicNumber = 9;
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. If you are in Wrath, gain {MagicNumber} additional Block.";
        }

        public override Card AddCard()
        {
            return new Halt();
        }
    }
}