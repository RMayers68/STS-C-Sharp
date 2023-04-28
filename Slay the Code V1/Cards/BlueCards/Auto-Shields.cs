
using System.Xml.Linq;

namespace STV
{
    public class Auto-Shields : Card
        {
                public Auto-Shields(bool Upgraded = false)
    {
        Name = "Auto-Shields";
        Type = "Skill";
        Rarity = "Uncommon";
        DescriptionModifier = "";
        EnergyCost = ;
        if (EnergyCost >= 0)
            SetTmpEnergyCost(EnergyCost);
        GoldCost = CardRNG.Next(45, 56);
        BlockAmount = 11;
        BlockLoops = 1;
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
        return DescriptionModifier + $"If you have 0 Block, gain {BlockAmount} Block.";
    }

    public override Card AddCard()
    {
        return new Auto- Shields();
    }
}
}

