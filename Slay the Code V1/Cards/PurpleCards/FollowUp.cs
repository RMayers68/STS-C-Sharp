
using System.Xml.Linq;

namespace STV
{
    public class Follow_Up : Card
        {
                public Follow_Up(bool Upgraded = false)
    {
        Name = "Follow-Up";
        Type = "Attack";
        Rarity = "Common";
        DescriptionModifier = "";
        EnergyCost = ;
        if (EnergyCost >= 0)
            SetTmpEnergyCost(EnergyCost);
        GoldCost = CardRNG.Next(45, 56);
        AttackDamage = 7;
        AttackLoops = 1;
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
        return DescriptionModifier + $"Deal {AttackDamage} damage. If the last card played this combat was an Attack, gain 1 Energy.";
    }

    public override Card AddCard()
    {
        return new Follow_Up();
    }
}
}