
using STV;

namespace STV
{
    public class JAX : Card
        {
                public JAX(bool Upgraded = false)
                {
                        Name = "J.A.X.";
                        Type = "Skill";
                        Rarity = "Common";
                        DescriptionModifier = "";
                        EnergyCost = ;
                        if (EnergyCost >= 0)
                                SetTmpEnergyCost(EnergyCost);
    GoldCost = CardRNG.Next(45, 56);
                        MagicNumber = 3;
                        BuffID = 4;
                        BuffAmount = 2;
                        SelfDamage = true;
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
    return DescriptionModifier + $"Lose 3 HP. Gain {BuffAmount} Strength.";
}

public override Card AddCard()
{
    return new JAX();
}
        }
}
