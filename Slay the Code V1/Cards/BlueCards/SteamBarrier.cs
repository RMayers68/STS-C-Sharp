
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 6;
            BlockLoops = 1;
            if (Upgraded)
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
            return DescriptionModifier + $"Gain {BlockAmount} Block. This card's Block is lowered by 1 this combat.";
        }

        public override Card AddCard()
        {
            return new SteamBarrier();
        }
    }
}
