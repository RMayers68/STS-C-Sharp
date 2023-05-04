
namespace STV
{
    public class Omniscience : Card
    {
        public Omniscience(bool Upgraded = false)
        {
            Name = "Omniscience";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 4;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(135, 166);
            MagicNumber = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card omni;
            do
                omni = PickCard(hero.DrawPile, "play twice");
            while (omni.GetDescription().Contains("Unplayable"));
            omni.TmpEnergyCost = 0;
            for (int i = 0; i < MagicNumber; i++)
                omni.CardAction(hero, encounter, turnNumber);
            omni.Exhaust(hero, encounter, hero.DrawPile);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                EnergyCost--;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose a card in your draw pile. Play the chosen card twice and Exhaust it. Exhaust.";
        }

        public override Card AddCard()
        {
            return new Omniscience();
        }
    }
}