
namespace STV
{
    public class Setup : Card
    {
        public Setup(bool Upgraded = false)
        {
            Name = "Setup";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(68, 83);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card setup = PickCard(hero.Hand, "add to the top of your drawpile");
            setup.MoveCard(hero.Hand, hero.DrawPile);
            setup.TmpEnergyCost = 0;
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Place a card in your hand on top of your draw pile. It cost 0 until it is played.";
        }

        public override Card AddCard()
        {
            return new Setup();
        }
    }
}