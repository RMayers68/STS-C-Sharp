
namespace STV
{
    public class Reboot : Card
    {
        public Reboot(bool Upgraded = false)
        {
            Name = "Reboot";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            CardsDrawn = 4;
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
            return DescriptionModifier + $"Shuffle all of your cards into your draw pile, then draw {CardsDrawn} cards. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Reboot();
        }
    }
}