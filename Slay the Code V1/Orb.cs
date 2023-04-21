namespace STV
{
    public class Orb
    {
        //attributes
        public string Name { get; set; }
        public int Effect { get; set; }

        private static readonly Random OrbRNG = new();   

        //constructor
        public Orb(string name,int effect)
        {
            this.Name = name;
            this.Effect = effect;
        }

        public Orb(Orb orb)
        {
            this.Name = orb.Name;
            this.Effect = orb.Effect;
        }

        public void PassiveEffect(Hero hero, List<Enemy> encounter)
        {
            if (encounter.Count > 0)
            {
                if (Name == "Lightning")
                    hero.NonAttackDamage(encounter[OrbRNG.Next(0, encounter.Count)], 3, "Lightning Orb");
                else if (Name == "Frost")
                {
                    hero.GainBlock(2);
                    Console.WriteLine($"The {hero.Name} gained 2 Block from the {Name} Orb!");
                }
                else if (Name == "Dark")
                {
                    Effect += 6;
                    Console.WriteLine($"The {Name} Orb stored 6 more Energy!");
                }
            }          
        }

        public static void ChannelOrb(Hero hero, List<Enemy> encounter, int orbID)
        {
            Orb channeledOrb = new(Dict.orbL[orbID]);
            if (hero.Orbs.Count == hero.OrbSlots)
            {
                hero.Orbs[0].Evoke(hero,encounter);
                hero.Orbs.RemoveAt(0);
            }
            hero.Orbs.Add(channeledOrb);
            hero.AddAction($"Channel {channeledOrb.Name} Orb");
        }

        public void Evoke(Hero hero, List<Enemy> encounter)
        {
            if (hero.Hp <= 0) return;
            if (this == null) return;
            if (Name == "Lightning")
                hero.NonAttackDamage(encounter[OrbRNG.Next(0, encounter.Count)], 8, "Evoked Lightning Orb");
            else if (Name == "Frost")
            {
                hero.GainBlock(5);
                Console.WriteLine($"The {hero.Name} gained 5 Block from the Evoked {Name} Orb!");
            }
            else if (Name == "Dark")
            {
                Actor lowestHP = encounter[0];
                foreach (var e in encounter)
                    if (e.Hp < lowestHP.Hp) lowestHP = e;
                hero.NonAttackDamage(lowestHP, Effect, "Evoked Dark Orb");
            }
            else
            {
                hero.GainTurnEnergy(2);
                Console.WriteLine($"The Evoked {Name} Orb gave the {hero.Name} 2 Energy!");
            }
        }
    }
}