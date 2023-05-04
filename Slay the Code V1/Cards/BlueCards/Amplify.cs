
namespace STV
{
    public class Amplify : Card
    {
        public Amplify(bool Upgraded = false)
        {
            Name = "Amplify";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            BuffID = 54;
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
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"This turn, your next {(Upgraded ? $"next two Powers are" : "Power is")} played twice";
        }

        public override Card AddCard()
        {
            return new Amplify();
        }
    }
}
