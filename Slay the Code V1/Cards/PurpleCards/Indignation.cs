
namespace STV
{
    public class Indignation : Card
    {
        public Indignation(bool Upgraded = false)
        {
            Name = "Indignation";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 1;
            BuffAmount = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (hero.Stance != "Wrath")
                hero.SwitchStance("Wrath");
            else foreach (Enemy e in encounter)
                    e.AddBuff(BuffID, BuffAmount, hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"If you are in Wrath, apply {BuffAmount} Vulnerable to ALL enemies, otherwise enter Wrath.";
        }

        public override Card AddCard()
        {
            return new Indignation();
        }
    }
}