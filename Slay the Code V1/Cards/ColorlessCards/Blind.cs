
namespace STV
{
    public class Blind : Card
    {
        public Blind(bool Upgraded = false)
        {
            Name = "Blind";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 2;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (Upgraded)
                foreach (Enemy e in encounter)
                    hero.Attack(e, AttackDamage + extraDamage, encounter);
            else
            {
                int target = DetermineTarget(encounter);
                encounter[target].AddBuff(BuffID, BuffAmount,hero);
            }
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Apply 2 Weak{(Upgraded ? $" to ALL enemies" : "")}.";
        }

        public override Card AddCard()
        {
            return new Blind();
        }
    }
}