using STV;
using System.ComponentModel.Design;

namespace STV
{
    public class Hero : Actor
    {
        public int MaxEnergy { get; set; }
        public int Energy { get; set; }
        public int OrbSlots { get; set; }
        public int EasyFights { get; set; }
        public int PotionSlots { get; set; }
        public int PotionChance { get; set; }
        public List<Card> Deck { get; set; }
        public List<Relic> Relics { get; set; }
        public List<Orb> Orbs { get; set; }
        public List<Potion> Potions { get; set; }
        
        public Hero(string name, int maxHP)
        {
            this.Name = name;
            this.MaxHP = maxHP;
            this.Hp = maxHP;
            this.MaxEnergy = 3;
            this.Energy = 3;
            this.Block = 0;
            this.Deck = new List<Card>();
            this.Buffs = new();
            this.Relics = new();
            this.Potions = new();
            this.Actions = new();
            this.Orbs = new();
            this.OrbSlots = 1;
            this.Gold = 99;
            this.EasyFights = 0;
            this.PotionSlots = 3;
            this.PotionChance = 1;
        }

        public Hero(Hero hero)
        {
            {
                this.Name = hero.Name;
                this.MaxHP = hero.MaxHP;
                this.Hp = hero.MaxHP;
                this.MaxEnergy = 3;
                this.Energy = 3;
                this.Block = 0;
                this.Deck = new List<Card>();
                this.Buffs = new();
                this.Relics = new();
                this.Potions = new();
                this.Actions = new();
                this.Orbs = new();
                this.OrbSlots = 1;
                this.Gold = 99;
                this.EasyFights = 0;
                this.PotionSlots = 3;
                this.PotionChance = 1;
            }
        }
        // Hero exclusive methods
        public int DetermineTarget(List<Enemy> encounter)
        {
            int x = 0;
            if (encounter.Count == 1 || Hp == 0)
                return x;
            Console.WriteLine("What enemy would you like to target?\n");
            for (int i = 0; i < encounter.Count; i++)
                Console.Write($"{i + 1}:{encounter[i].Name}\t");
            while (!Int32.TryParse(Console.ReadLine(), out x) || x < 1 || x > encounter.Count || encounter[x - 1].Hp == 0)
                Console.WriteLine("Invalid input, enter again:");
            return x - 1;
        }

        public void AttackAll(List<Enemy> encounter, int damage)
        {
            {
                if (Hp <= 0) return;
                foreach (var target in encounter)
                {
                    if (FindBuff("Strength", Buffs) != null)
                        damage += FindBuff("Strength", Buffs).Intensity.GetValueOrDefault(0);
                    if (Stance == "Wrath")
                        damage = damage * 2;
                    if (FindBuff("Weak", Buffs) != null)
                        damage = Convert.ToInt32(damage * 0.75);
                    if (FindBuff("Vulnerable",target.Buffs) != null)
                        damage = Convert.ToInt32(damage * 1.5);
                    if (target.Block > 0)
                    {
                        target.Block -= damage;
                        if (target.Block < 0)
                        {
                            target.Hp -= Math.Abs(target.Block);
                            target.Block = 0;
                        }
                    }
                    else
                        target.Hp -= damage;
                    Console.WriteLine($"{Name} attacked {target.Name} for {damage} damage!");
                    if (FindBuff("Curl Up", target.Buffs) != null)      // Louse Curl Up Effect
                    {
                        Console.WriteLine($"The Louse has curled up and gained {target.Buffs[0].Intensity} Block!");
                        target.Block += target.Buffs[0].Intensity.GetValueOrDefault();
                        target.Buffs.RemoveAt(0);
                    }
                }
                
            }
        }

        public void SelfDamage(int damage)
        {
            if (Hp <= 0) return;
            this.Hp -= damage;
            Console.WriteLine($"{Name} hurt themselves for {damage} damage!");
        }

        public void SwitchStance(string newStance, List<Card> discardPile, List<Card> hand)
        {
            string oldStance = Stance;
            Stance = newStance;
            if (oldStance == Stance)
                return;
            else
                switch (Stance)
                {
                    default:
                        Console.WriteLine($"{Name} has switched to {Stance} Stance.");
                        break;
                    case "None":
                        Console.WriteLine($"{Name} has left {oldStance} Stance.");
                        break;
                }
            // Mental Fortress Check
            if (FindBuff("Mental Fortress", Buffs) != null)                              
                GainBlock(FindBuff("Mental Fortress", Buffs).Intensity.GetValueOrDefault());
            if (oldStance != Stance && oldStance == "Calm")
                Energy += 2;
            else if (oldStance != Stance && Stance == "Divinity")
                Energy += 3;
            // Flurry of Blows Check
            if (Card.FindCard("Flurry of Blows", discardPile) is Card flurryOfBlows && flurryOfBlows != null && hand.Count < 10) 
                flurryOfBlows.MoveCard(discardPile, hand);               
        }

        public void ChannelOrb(List<Enemy> encounter, int orbID)
        {
            if (Hp <= 0) return;
            Orb channeledOrb = new Orb(Dict.orbL[orbID]);
            if (Orbs.Count == OrbSlots)
            {
                Evoke(encounter);
                Orbs.RemoveAt(0);
            }               
            Orbs.Add(channeledOrb);
            Actions.Add($"Channel {channeledOrb.Name} Orb");
        }

        public void Evoke(List<Enemy> encounter)
        {
            if (Hp <= 0) return;
            Random random = new();
            if (Orbs[0] == null) return;
            else if (Orbs[0].Name == "Lightning")
            {
                int target = random.Next(0, encounter.Count);
                NonAttackDamage(encounter[target], 8);
                Console.WriteLine($"The {encounter[target].Name} took 8 damage from the Evoked Lightning Orb!");
            }
            else if (Orbs[0].Name == "Frost")
            {
                GainBlock(5);
                Console.WriteLine($"The {Name} gained 2 Block from the Evoked Frost Orb!");
            }
            else if (Orbs[0].Name == "Dark")
            {
                Actor lowestHP = encounter[0];
                foreach (var enemy in encounter)
                    if (enemy.Hp < lowestHP.Hp) lowestHP = enemy;
                NonAttackDamage(lowestHP, Orbs[0].Effect);
                Console.WriteLine($"The Evoked Dark Orb exploded on the {lowestHP.Name} for {Orbs[0].Effect} damage!");
            }
            else
            {
                GainTurnEnergy(2);
            }
            Actions.Add($"Evoked {Orbs[0].Name} Orb");
        }

        // Method for adding to current energy amount for the turn
        public void GainTurnEnergy(int energy)
        {
            this.Energy += energy;
            Console.WriteLine($"The {Name} gained {energy} Energy!");
        }

        //  Method for adding to current max amount of energy that lasts for the battle.
        public void GainBattleEnergy(int energy)
        {
            this.MaxEnergy += energy;
        }
        public void GoldChange(int amount) //For when rewards are set
        {
            Gold += amount;
            if (amount > 0)
                Console.WriteLine($"The {Name} gained {amount} Gold!");
            else Console.WriteLine($"The {Name} lost {amount * -1} Gold!");
        }


        public void CombatRewards(List<Card> deck, Random rng)
        {
            List<Card> cardReward = Card.RandomCards(Name,3, rng);
            deck.Add(STS.ChooseCard(cardReward, "add to your Deck."));
            GoldChange(35);
            if (rng.Next(10) < PotionChance)
            {
                Potions.Add(Dict.potionL[rng.Next(10)]);
                PotionChance--;
            }
            else PotionChance++;
        }
    }
}
