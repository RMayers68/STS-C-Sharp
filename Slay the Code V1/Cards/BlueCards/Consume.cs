
namespace STV
{
    public class Consume : Card
    {
        public Consume(bool Upgraded = false)
        {
            Name = "Consume";
            Type = "Skill";
            Rarity = "Uncommon";
            DescriptionModifier = "";
            EnergyCost = 2;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(68, 83);
            BuffID = 7;
            BuffAmount = 2;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            hero.AddBuff(BuffID, BuffAmount);
            hero.OrbSlots -= 1;
            while (hero.OrbSlots > hero.Orbs.Count)
                hero.Orbs.RemoveAt(hero.Orbs.Count - 1);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                BuffAmount++;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Gain {BuffAmount} Focus. Lose 1 Orb Slot.";
        }

        public override Card AddCard()
        {
            return new Consume();
        }
    }
}