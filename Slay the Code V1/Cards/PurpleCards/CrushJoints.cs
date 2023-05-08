namespace STV
{
    public class CrushJoints : Card
    {
        public CrushJoints(bool Upgraded = false)
        {
            Name = "Crush Joints";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 8;
            BuffID = 1;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            string lastCardPlayed = hero.Actions.FindLast(x => x.Contains("Played"));
            if (lastCardPlayed != null && lastCardPlayed.Contains("Skill"))
                encounter[target].AddBuff(BuffID, BuffAmount,hero);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded)
            {
                AttackDamage += 2;
                BuffAmount++;
            }
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the last card played this combat was a Skill, apply {BuffAmount} Vulnerable.";
        }

        public override Card AddCard()
        {
            return new CrushJoints();
        }
    }
}