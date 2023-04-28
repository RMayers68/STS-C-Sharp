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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 5;
            AttackLoops = 1;
            CardsDrawn = 1;
            EnergyGained = 1;
            Targetable = true;
            SingleAttack = true;
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
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the target is Vulnerable, gain {EnergyGained} Energy and draw {CardsDrawn} card.";
        }

        public override Card AddCard()
        {
            return new Dropkick();
        }
    }
}