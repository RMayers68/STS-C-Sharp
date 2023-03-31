namespace STV
{
    public class Potion
    {
        //attributes
        private int PotionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rarity { get; set; }

        //constructors
        public Potion(int potionID, string name, string description, string rarity)
        {
            this.PotionID = potionID;
            this.Name = name;
            this.Description = description;
            this.Rarity = rarity;
        }

        public Potion(Potion potion)
        {
            this.PotionID = potion.PotionID;
            this.Name = potion.Name;
            this.Description = potion.Description;
            this.Rarity = potion.Rarity;
        }
        //string method
        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
        public void UsePotion(Hero hero,List<Enemy> encounter,List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)                              // potion methods (correlating to PotionID)
        {
            int target = 0;
            switch (Name)
            {
                case "Fire Potion":
                    target = hero.DetermineTarget(encounter);
                    hero.NonAttackDamage(encounter[target], 20);
                    break;
                case "Energy Potion":
                    hero.GainTurnEnergy(2);
                    break;
                case "Strength Potion":
                    hero.AddBuff(4,2);
                    break;
                case "Block Potion":
                    hero.GainBlock(12);
                    break;
                case "Fear Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(1, 3);
                    break;
                case "Swift Potion":
                    STS.DrawCards(drawPile, hand, discardPile, rng, 3);
                    break;
                case "Weak Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(2, 3);
                    break;
                case "Focus Potion":
                    hero.AddBuff(7, 2);
                    break;
                case "Cultist Potion":
                    hero.AddBuff(3, 1);
                    break;
                case "Liquid Bronze":
                    hero.AddBuff(41,3); 
                    break;  
            }
        }
    }
}
