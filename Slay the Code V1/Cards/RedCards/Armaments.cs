﻿namespace STV
{
    public class Armaments : Card
    {
        public Armaments(bool Upgraded = false)
        {
            Name = "Armaments";
            Type = "Skill";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            BlockAmount = 5;
            if (Upgraded)
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
            else if (hero.Hand.Find(x => !x.Upgraded) != null)
                PickCard(hero.Hand,"upgrade").UpgradeCard();
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