
namespace STV
{
    public class SecretTechnique : Card
    {
        public SecretTechnique(bool Upgraded = false)
        {
            Name = "Secret Technique";
            Type = "Skill";
            Rarity = "Rare";
            DescriptionModifier = "";
            EnergyCost = 0;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(135, 166);
            if (Upgraded)
                UpgradeCard();
        }

        public override void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage = 0)
        {
            Card secretSkillChoice = PickCard(hero.DrawPile.FindAll( x => x.Type == "Skill"), "add to your hand");
            hero.AddToHand(secretSkillChoice);
            hero.DrawPile.Remove(secretSkillChoice);
        }

        public override void UpgradeCard()
        {
            base.UpgradeCard();
        }

        public override string GetDescription()
        {
            return DescriptionModifier + $"Choose a Skill from your draw pile and place it into your hand. {(Upgraded ? $"" : "Exhaust.")}";
        }

        public override Card AddCard()
        {
            return new SecretTechnique();
        }
    }
}