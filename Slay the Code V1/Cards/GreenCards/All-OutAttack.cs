
using System.Xml.Linq;

namespace STV
{
    public class AllOutAttack : Card
        {
                public AllOutAttack(bool Upgraded = false)
    {
        Name = "All-Out Attack";
        Type = "Attack";
        Rarity = "Uncommon";
        DescriptionModifier = "";
        EnergyCost = ;
        if (EnergyCost >= 0)
            SetTmpEnergyCost(EnergyCost);
        GoldCost = CardRNG.Next(45, 56);
        AttackDamage = 10;
        AttackLoops = 1;
        MagicNumber = 1;
        AttackAll = true;
        Discards = true;
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
        return DescriptionModifier + $"Deal {AttackDamage} damage to ALL enemies. Discard 1 card at random.";
    }

    public override Card AddCard()
    {
        return new All- OutAttack();
    }
}
}
