
namespace STV
{
    public class BattleHymn : Card
    {
        public BattleHymn(bool Upgraded = false)
        {
            Name = "Battle Hymn";
            Type = "Power";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 70;
            BuffAmount = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"{(Upgraded ? $"Innate. " : "")} At the start of each turn add a Smite into your hand.";
        }

        public override Card AddCard()
        {
            return new BattleHymn();
        }
    }
}
