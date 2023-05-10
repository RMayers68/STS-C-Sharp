
namespace STV
{
    public class Blasphemy : Card
    {
        public Blasphemy(bool Upgraded = false)
        {
            Name = "Blasphemy";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 71;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.SwitchStance("Divinity");
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Retain. " : "")}Enter Divinity. Die next turn. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Blasphemy();
        }
    }
}