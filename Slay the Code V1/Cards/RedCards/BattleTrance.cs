namespace STV
{
    public class BattleTrance : Card
    {
        public BattleTrance(bool Upgraded = false)
        {
            Name = "Battle Trance";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BuffID = 21;
            BuffAmount = 1;
            CardsDrawn = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.DrawCards(CardsDrawn);
            hero.AddBuff(21, 1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Draw {CardsDrawn} cards. You cannot draw additional cards this turn.";
        }

        public override Card AddCard()
        {
            return new BattleTrance();
        }
    }
}
