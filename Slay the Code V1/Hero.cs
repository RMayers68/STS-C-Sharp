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
        public List<Card> DrawPile { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Card> ExhaustPile { get; set; }
        public List<Relic> Relics { get; set; }
        public List<Orb> Orbs { get; set; }
        public List<Potion> Potions { get; set; }
        
        public Hero(string name, int maxHP)
        {
            Name = name;
            MaxHP = maxHP;
            Hp = maxHP;
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
                this.Deck = new();
                this.DrawPile = new();
                this.Hand = new();
                this.DiscardPile = new();
                this.ExhaustPile = new();
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

        // Initial Deck
        public void StartingDeck()
        {
            for (int i = 0; i < 8; i++)
            {
                while (i < 4)
                {
                    Deck.Add(new Card(Dict.cardL[220]));
                    i++;
                }
                while (i < 8)
                {
                    Deck.Add(new Card(Dict.cardL[219]));
                    i++;
                }
            }
            switch (Name)
            {
                case "Ironclad":
                    Deck.Add(new Card(Dict.cardL[3]));
                    Deck.Add(new Card(Dict.cardL[220]));
                    break;
                case "Silent":
                    Deck.Add(new Card(Dict.cardL[220]));
                    Deck.Add(new Card(Dict.cardL[219]));
                    Deck.Add(new Card(Dict.cardL[121]));
                    Deck.Add(new Card(Dict.cardL[139]));
                    break;
                case "Defect":
                    Deck.Add(new Card(Dict.cardL[170]));
                    Deck.Add(new Card(Dict.cardL[214]));
                    break;
                case "Watcher":
                    Deck.Add(new Card(Dict.cardL[241]));
                    Deck.Add(new Card(Dict.cardL[285]));
                    break;
            }
            Deck.Sort();
        }

        // Hero exclusive methods

        public void SetMaxHP(int change)
        {
            MaxHP += change;
            if (Hp > MaxHP)
                Hp = MaxHP;
            else if (change > 0)
                Hp += change;
        }

        public void CombatHeal(int heal)
        {
            if (Relics.Find(x => x.Name == "Magic Flower") != null)
                heal = Convert.ToInt32(heal * 1.5);
            HealHP(heal);
        }

        public void HealHP(int heal)
        {
            if (Relics.Find(x => x.Name == "Mark of the Bloom") != null)
                Console.WriteLine("Your attempt at healing failed due to the Mark of the Bloom.");
            else
            {
                Hp += heal;
                if (Hp > MaxHP)
                {
                    heal = Hp + heal - MaxHP;
                    Hp = MaxHP;
                }                   
                Console.WriteLine($"You have healed {heal} HP and are now at {Hp}/{MaxHP}!");
            }
        }

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
                    if (FindBuff("Strength") != null)
                        damage += FindBuff("Strength").Intensity;
                    if (Stance == "Wrath")
                        damage *= 2;
                    if (FindBuff("Weak") != null)
                        damage = Convert.ToInt32(damage * 0.75);
                    if (target.FindBuff("Vulnerable") != null)
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
                    if (target.FindBuff("Curl Up") is Buff curlUp && curlUp != null)      // Louse Curl Up Effect
                    {
                        Console.WriteLine($"The Louse has curled up and gained {curlUp.Intensity} Block!");
                        target.Block += curlUp.Intensity;
                        target.Buffs.Remove(curlUp);
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

        public void SwitchStance(string newStance)
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
            if (FindBuff("Mental Fortress") != null)                              
                GainBlock(FindBuff("Mental Fortress").Intensity);
            if (oldStance != Stance && oldStance == "Calm")
                Energy += 2;
            else if (oldStance != Stance && Stance == "Divinity")
                Energy += 3;
            // Flurry of Blows Check
            if (Card.FindCard("Flurry of Blows", DiscardPile) is Card flurryOfBlows && flurryOfBlows != null && Hand.Count < 10) 
                flurryOfBlows.MoveCard(DiscardPile, Hand);               
        }

        public void ChannelOrb(List<Enemy> encounter, int orbID)
        {
            Orb channeledOrb = new(Dict.orbL[orbID]);
            if (Orbs.Count == OrbSlots)
            {
                Evoke(encounter);
                Orbs.RemoveAt(0);
            }               
            Orbs.Add(channeledOrb);
            Actions.Add($"Channel {channeledOrb.Name} Orb");
        }

        public void Evoke(List<Enemy> encounter, int position = 0)
        {
            if (Hp <= 0) return;
            Random random = new();
            if (Orbs[position] == null) return;
            switch (Orbs[position].Name)
            {
                default:
                    GainTurnEnergy(2);
                    Console.WriteLine($"The Evoked Plasma Orb gave the {Name} 2 Energy!");
                    break;
                case "Lightning":
                    int target = random.Next(0, encounter.Count);
                    NonAttackDamage(encounter[target], 8);
                    Console.WriteLine($"The {encounter[target].Name} took 8 damage from the Evoked Lightning Orb!");
                    break;
                case "Frost":
                    GainBlock(5);
                    Console.WriteLine($"The {Name} gained 5 Block from the Evoked Frost Orb!");
                    break;
                case "Dark":
                    Actor lowestHP = encounter[0];
                    foreach (var enemy in encounter)
                        if (enemy.Hp < lowestHP.Hp) lowestHP = enemy;
                    NonAttackDamage(lowestHP, Orbs[position].Effect);
                    Console.WriteLine($"The Evoked Dark Orb exploded on the {lowestHP.Name} for {Orbs[0].Effect} damage!");
                    break;
            }
        }

        // Method to look if hand is full and sending card to discard pile if hand is full
        public void AddToHand(Card card)
        {
            if (card == null) return;
            if (Hand.Count < 10)
                Hand.Add(card);
            else DiscardPile.Add(card);
        }

        // Method for adding to current energy amount for the turn
        public void GainTurnEnergy(int energy)
        {
            this.Energy += energy;
            Console.WriteLine($"The {Name} gained {energy} Energy!");
        }

        public void GoldChange(int amount) //For when rewards are set
        {
            Gold += amount;
            if (amount > 0)
                Console.WriteLine($"The {Name} gained {amount} Gold!");
            else Console.WriteLine($"The {Name} lost {amount * -1} Gold!");
        }

        public void CombatRewards(List<Card> deck, Random rng, string roomLocation)
        {
            deck.Add(Card.ChooseCard(Card.RandomCards(Name, 3, rng), "add to your Deck."));
            GoldChange(roomLocation == "Boss" ? rng.Next(95,106) : roomLocation == "Elite" ? rng.Next(35,36) : rng.Next(10,21));
            if (roomLocation == "Boss")

            if (rng.Next(10) < PotionChance || FindRelic("White Beast Statue") != null)
            {
                Potions.Add(Dict.potionL[rng.Next(10)]);
                PotionChance--;
            }
            else PotionChance++;
        }

        public void ShuffleDrawPile(Random rng)
        {
            int n = DrawPile.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (DrawPile[n], DrawPile[k]) = (DrawPile[k], DrawPile[n]);
            }
        }

        public Relic FindRelic(string name)
        {
            return Relics.Find(x => x.Name == name);
        }
    }
}