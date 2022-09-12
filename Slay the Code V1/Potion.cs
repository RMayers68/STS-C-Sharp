namespace STV
{
    public class Potion
    {
        //attributes
        public int PotionID { get; set; }
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
        public void UsePotion(Actor hero,List<Actor> encounter,List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)                              // potion methods (correlating to PotionID
        {
            int target = 0;
            switch (Name)
            {                                
                case "Ambrosia":
                    hero.SwitchStance("Divinity",discardPile,hand);
                    break;
                case "Ancient Potion":
                    hero.AddBuff(8, 1);
                    break;
                case "Block Potion":
                    hero.GainBlock(12);
                    break;
                case "Blood Potion":
                    hero.HealHP(hero.MaxHP * 0.2);
                    break;
                case "Bottled Miracle":
                    for (int i = 0; i < 2; i++ )
                    {
                        if (hand.Count < 10)
                            hand.Add(new Card(Dict.cardL[336]));
                        else discardPile.Add(new Card(Dict.cardL[336]));
                    }
                    Console.WriteLine(" You have gained 2 Miracles!");
                    break;
                case "Cultist Potion":
                    hero.AddBuff(3, 1);
                    break;
                case "Cunning Potion":
                    for (int i = 0; i < 3; i++)
                    {
                        if (hand.Count < 10)
                            hand.Add(new Card(Dict.cardL[317]));
                        else discardPile.Add(new Card(Dict.cardL[317]));
                    }
                    Console.WriteLine(" You have gained 3 Shivs!");
                    break;
                case "Dexterity Potion":
                    hero.AddBuff(9, 2);
                    break;
                case "Distilled Chaos":
                    for (int i = 0; i < 3; i++)
                        drawPile[drawPile.Count - 1].CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                    break;
                case "Fear Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(1, 3);
                    break;
                case "Fire Potion":
                    target = hero.DetermineTarget(encounter);
                    hero.NonAttackDamage(encounter[target], 20);
                    break;
                case "Focus Potion":
                    hero.AddBuff(7, 2);
                    break;
                case "Energy Potion":
                    hero.GainEnergy(2);
                    break;
                case "Essence of Darkness":
                    for (int i = 0; i < hero.OrbSlots; i++)
                        hero.ChannelOrb(encounter, 2);
                    break;
                case "Essence of Steel":
                    hero.AddBuff(20, 4);
                    break;
                case "Explosive Potion":
                    for (int i = 0; i < encounter.Count; i++)
                        hero.NonAttackDamage(encounter[i], 10);
                    break;
                case "Fairy in a Bottle":
                    hero.HealHP(hero.MaxHP * 0.3);
                    break;
                case "Flex Potion":
                    hero.AddBuff(4, 5);
                    hero.AddBuff(21, 5);
                    break;
                case "Fruit Juice":
                    hero.MaxHP += 5;
                    hero.Hp += 5;
                    break;
                case "Ghost in a Jar":
                    hero.AddBuff(22, 1);
                    break;
                case "Heart of Iron":
                    hero.AddBuff(23, 6);
                    break;
                case "Liquid Bronze":
                    hero.AddBuff(24, 3);
                    break;
                case "Posion Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(25, 6);
                    break;
                case "Potion of Capacity":
                    hero.OrbSlots += 2;
                    break;
                case "Regen Potion":
                    hero.AddBuff(26, 5);
                    break;
                case "Speed Potion":
                    hero.AddBuff(9, 5);
                    hero.AddBuff(27, 5);
                    break;
                case "Stance Potion":
                    List<Card> stances = (Dict.cardL[364], Dict.cardL[365]);
                    string stancePick = STS.ChooseCard(stances).Name;
                    hero.SwitchStance(stancePick, discardPile, hand);
                    break;
                case "Strength Potion":
                    hero.AddBuff(4, 2);
                    break;
                case "Swift Potion":
                    STS.DrawCards(drawPile, hand, discardPile, rng, 3);
                    break;
                case "Weak Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(2, 3);
                    break;
            }
        }
    }
}
