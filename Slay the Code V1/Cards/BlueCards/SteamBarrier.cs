
namespace STV
{
    public class SteamBarrier : Card
    {
        public SteamBarrier(bool Upgraded = false)
        {
            Name = "Steam Barrier";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.CardBlock(BlockAmount, encounter);
            BlockAmount--;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BlockAmount += 2;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. This card's Block is lowered by 1 this combat.";
        }

        public override Card AddCard()
        {
            return new SteamBarrier();
        }
    }
}
