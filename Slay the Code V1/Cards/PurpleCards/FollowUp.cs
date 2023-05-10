
using System.Xml.Linq;

namespace STV
{
    public class FollowUp : Card
    {
        public FollowUp(bool Upgraded = false)
        {
            Name = "Follow-Up";
            Type = "Attack";
            Rarity = "Common";
            DescriptionModifier = "";
            EnergyCost = 1;
            if (EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            GoldCost = CardRNG.Next(45, 56);
            AttackDamage = 7;
            EnergyGained = 1;
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            int target = DetermineTarget(encounter);
            hero.Attack(encounter[target], AttackDamage + extraDamage);
            string lastCardPlayed = hero.Actions.FindLast(x => x.Contains("Played"));
            if (lastCardPlayed != null && lastCardPlayed.Contains("Attack"))
                hero.GainTurnEnergy(EnergyGained);
        }

        public override void UpgradeCard()
        {
            if (!Upgraded) 
                AttackDamage += 4;
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Deal {AttackDamage} damage. If the last card played this combat was an Attack, gain 1 Energy.";
        }

        public override Card AddCard()
        {
            return new FollowUp();
        }
    }
}