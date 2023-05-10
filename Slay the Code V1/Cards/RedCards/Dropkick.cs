namespace STV
{
    public class Dropkick : Card
    {
        public Dropkick(bool Upgraded = false)
        {
            Name = "Dropkick";
            Type = "Attack";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            AttackDamage = 5;
            CardsDrawn = 1;
            EnergyGained = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            if (encounter[target].HasBuff("Vulnerable"))
            {
                hero.GainTurnEnergy(1);
                hero.DrawCards(CardsDrawn);
            }
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 3;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the target is Vulnerable, gain {EnergyGained} Energy and draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new Dropkick();
        }
    }
}