
namespace STV
{
    public class InnerPeace : Card
    {
        public InnerPeace(bool Upgraded = false)
        {
            Name = "Inner Peace";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            CardsDrawn = 3;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            if (hero.Stance != "Calm")
                hero.SwitchStance("Calm");
            else hero.DrawCards(CardsDrawn);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                CardsDrawn++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"If you are in Calm, draw {CardsDrawn} cards. Otherwise, enter Calm.";
        }

        public override Card AddCard()
        {
            return new InnerPeace();
        }
    }
}