
namespace STV
{
    public class SecretWeapon : Card
    {
        public SecretWeapon(bool Upgraded = false)
        {
            Name = "Secret Weapon";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
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
            return DescriptionModifier + $"Choose an Attack from your draw pile and place it into your hand. {(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new SecretWeapon();
                }
        }
}