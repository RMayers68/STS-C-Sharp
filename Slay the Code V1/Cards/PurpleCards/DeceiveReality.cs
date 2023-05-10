
namespace STV
{
    public class DeceiveReality : Card
    {
        public DeceiveReality(bool Upgraded = false)
        {
            Name = "Deceive Reality";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BlockAmount = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            hero.AddToHand(new Safety());
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Add a Safety to your hand.";
        }

        public override Card AddCard()
        {
            return new DeceiveReality();
        }
    }
}