namespace STV
{
    public class Armaments : Card
    {
        public Armaments(bool upgraded = false)
        {
            Name = "Armaments";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
            Upgraded = false;
            if (upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage)
        {
            hero.CardBlock(BlockAmount, encounter);
            if (Upgraded)
            {
                foreach (Card c in hero.Hand)
                    c.UpgradeCard();
            }
            else if (hero.Hand.Find(x => x.IsUpgraded()) is Card armaments && armaments != null)
                armaments.UpgradeCard();
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BlockAmount} Block. Upgrade {(Upgraded ? "all cards" : "a card")} in your hand for the rest of combat.";
        }

        public override Card AddCard()
        {
            return new Armaments();
        }
    }
}