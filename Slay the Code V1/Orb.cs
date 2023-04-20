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
            switch (Name)
            {
                default: break;
                case "Lightning":
                    int target = OrbRNG.Next(0, encounter.Count);
                    hero.NonAttackDamage(encounter[target], 3);
                    Console.WriteLine($"The {encounter[target].Name} took 3 damage from the {Name} Orb!");
                    break;
                case "Frost":
                    hero.GainBlock(2);
                    Console.WriteLine($"The {hero.Name} gained 2 Block from the {Name} Orb!");
                    break;
                case "Dark":
                    Effect += 6;
                    Console.WriteLine($"The {Name} Orb stored 6 more Energy!");
                    break;
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
            hero.Actions.Add($"Channel {channeledOrb.Name} Orb");
        }

        public void Evoke(Hero hero, List<Enemy> encounter)
        {
            if (hero.Hp <= 0) return;
            if (this == null) return;
            switch (Name)
            {
                default:
                    hero.GainTurnEnergy(2);
                    Console.WriteLine($"The Evoked {Name} Orb gave the {hero.Name} 2 Energy!");
                    break;
                case "Lightning":
                    int target = OrbRNG.Next(0, encounter.Count);
                    hero.NonAttackDamage(encounter[target], 8);
                    Console.WriteLine($"The {encounter[target].Name} took 8 damage from the Evoked {Name} Orb!");
                    break;
                case "Frost":
                    hero.GainBlock(5);
                    Console.WriteLine($"The {hero.Name} gained 5 Block from the Evoked {Name} Orb!");
                    break;
                case "Dark":
                    Actor lowestHP = encounter[0];
                    foreach (var e in encounter)
                        if (e.Hp < lowestHP.Hp) lowestHP = e;
                    hero.NonAttackDamage(lowestHP, Effect);
                    Console.WriteLine($"The Evoked {Name} Orb exploded on the {lowestHP.Name} for {Effect} damage!");
                    break;
            }
        }
    }
}