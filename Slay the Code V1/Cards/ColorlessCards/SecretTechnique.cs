
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
            EnergyCost = ;
            if (EnergyCost >= 0)
                SetTmpEnergyCost(EnergyCost);
            GoldCost = CardRNG.Next(45, 56);
            MagicNumber = 1;
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
            return DescriptionModifier + $"Choose a Skill from your draw pile and place it into your hand. {(Upgraded ? ";
                }

                public override Card AddCard()
                {
                        return new SecretTechnique();
                }
        }
}