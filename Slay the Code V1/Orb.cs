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
            int add = 0;
            if (hero.FindBuff("Focus") is Buff focus && focus != null)
                add += focus.Intensity;
            int effect = Effect + add;
            if (encounter.Count > 0)
            {
                if (Name == "Lightning")
                {
                    if (hero.HasBuff("Electrodynamics"))
                    {
                        foreach (Enemy e in encounter)
                        {
                            if (e.HasBuff("Lock-On"))
                                hero.NonAttackDamage(e, Convert.ToInt32(effect * 1.5), "Lightning Orb");
                            hero.NonAttackDamage(e, effect+5, "Lightning Orb");
                        }
                    }
                    else
                    {
                        int target = OrbRNG.Next(0, encounter.Count);
                        if (encounter[target].HasBuff("Lock-On"))
                            effect = Convert.ToInt32(effect * 1.5);
                        hero.NonAttackDamage(encounter[target], effect, "Lightning Orb");
                    }
                }                   
                else if (Name == "Frost")
                {
                    hero.GainBlock(effect, encounter);
                    Console.WriteLine($"The {hero.Name} gained {effect} Block from the {Name} Orb!");
                }
                else if (Name == "Dark")
                {
                    Effect += 6 + add;
                    Console.WriteLine($"The {Name} Orb stored {6+add} more Energy!");
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
            int add = 0;
            if (hero.FindBuff("Focus") is Buff focus && focus != null)
                add += focus.Intensity;
            int effect = Effect + add;
            if (Name == "Lightning")
            {
                if (hero.HasBuff("Electrodynamics"))
                {
                    foreach (Enemy e in encounter)
                    {
                        if (e.HasBuff("Lock-On"))
                            hero.NonAttackDamage(e, Convert.ToInt32((effect+5) * 1.5), "Evoked Lightning Orb");
                        hero.NonAttackDamage(e, effect, "Evoked Lightning Orb");
                    }
                }
                else
                {
                    int target = OrbRNG.Next(0, encounter.Count);
                    if (encounter[target].HasBuff("Lock-On"))
                        effect = Convert.ToInt32((effect + 5) * 1.5);
                    hero.NonAttackDamage(encounter[target], effect, "Evoked Lightning Orb");
                }
            }
            else if (Name == "Frost")
            {
                hero.GainBlock(effect, encounter);
                Console.WriteLine($"The {hero.Name} gained {effect+3} Block from the Evoked {Name} Orb!");
            }
            else if (Name == "Dark")
            {
                Actor lowestHP = encounter[0];
                foreach (var e in encounter)
                    if (e.Hp < lowestHP.Hp) lowestHP = e;
                if (lowestHP.HasBuff("Lock-On"))
                    effect = Convert.ToInt32(effect * 1.5);
                hero.NonAttackDamage(lowestHP, effect, "Evoked Dark Orb");
            }
            else
            {
                hero.GainTurnEnergy(2);
                Console.WriteLine($"The Evoked {Name} Orb gave the {hero.Name} 2 Energy!");
            }
        }
    }
}