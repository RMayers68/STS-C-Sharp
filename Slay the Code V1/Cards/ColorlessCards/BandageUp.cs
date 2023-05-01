
namespace STV
{
    public class BandageUp : Card
    {
        public BandageUp(bool Upgraded = false)
        {
            Name = "Bandage Up";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 4;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CombatHeal(MagicNumber);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber +=2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Heal {MagicNumber} HP. Exhaust.";
        }

        public override Card AddCard()
        {
            return new BandageUp();
        }
    }
}