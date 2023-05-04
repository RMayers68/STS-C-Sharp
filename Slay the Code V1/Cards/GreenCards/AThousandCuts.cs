
namespace STV
{
    public class AThousandCuts : Card
    {
        public AThousandCuts(bool Upgraded = false)
        {
            Name = "A Thousand Cuts";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 35;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Whenever you play a card, deal {BuffAmount} damage to ALL enemies.";
        }

        public override Card AddCard()
        {
            return new AThousandCuts();
        }
    }
}