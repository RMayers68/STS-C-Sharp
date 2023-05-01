namespace STV
{
    public class DualWield : Card
    {
        public DualWield(bool Upgraded = false)
        {
            Name = "Dual Wield";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card dualWield = null;
            if (hero.Hand.Any(x => x.Type != "Skill"))
                do
                    dualWield = PickCard(hero.Hand, "copy");
                while (dualWield.Type == "Skill");
            if (dualWield != null)
                for (int i = 0; i < MagicNumber; i++)
                hero.AddToHand(dualWield.AddCard());
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                MagicNumber++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Create {(Upgraded ? $"2 copies" : "a copy")} of an Attack or Power card in your hand.";
        }

        public override Card AddCard()
        {
            return new DualWield();
        }
    }
}