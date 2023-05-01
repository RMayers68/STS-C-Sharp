
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
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card secretAttackChoice = PickCard(hero.DrawPile.FindAll(x => x.Type == "Attack"), "add to your hand");
            hero.AddToHand(secretAttackChoice);
            hero.DrawPile.Remove(secretAttackChoice);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose an Attack from your draw pile and place it into your hand. {(Upgraded ? $"" : "Exhaust.")}";
        }

        public override Card AddCard()
        {
            return new SecretWeapon();
        }
    }
}