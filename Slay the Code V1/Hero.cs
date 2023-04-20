﻿namespace STV
{
    public class Hero : Actor
    {
        public int MaxEnergy { get; set; }
        public int Energy { get; set; }
        public int OrbSlots { get; set; }
        public int EasyFights { get; set; }
        public int PotionSlots { get; set; }
        public int PotionChance { get; set; }
        public int RemoveCost { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> DrawPile { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Card> ExhaustPile { get; set; }
        public List<Orb> Orbs { get; set; }
        public List<Potion> Potions { get; set; }

        private static readonly Random HeroRNG = new();

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
                this.Energy = 0;
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
                this.RemoveCost = 75;
            }
        }

        // Initial Deck
        public void StartingDeck()
        {
            for (int i = 0; i < 8; i++)
            {
                while (i < 4)
                {
                    AddToDeck(Dict.cardL[220]);
                    i++;
                }
                while (i < 8)
                {
                    AddToDeck(Dict.cardL[219]);
                    i++;
                }
            }
            switch (Name)
            {
                case "Ironclad":
                    AddToDeck(Dict.cardL[3]);
                    AddToDeck(Dict.cardL[220]);
                    break;
                case "Silent":
                    AddToDeck(Dict.cardL[220]);
                    AddToDeck(Dict.cardL[219]);
                    AddToDeck(Dict.cardL[121]);
                    AddToDeck(Dict.cardL[139]);
                    break;
                case "Defect":
                    AddToDeck(Dict.cardL[170]);
                    AddToDeck(Dict.cardL[214]);
                    break;
                case "Watcher":
                    AddToDeck(Dict.cardL[241]);
                    AddToDeck(Dict.cardL[285]);
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
                Console.WriteLine($"You have healed {heal} HP and are now at {Hp}/{MaxHP} HP!");
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

        public void SelfDamage(int damage,List<Enemy> encounter)
        {
            if (Hp <= 0) return;
            HPLoss(damage,encounter);
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
            if (FindBuff("Mental Fortress") != null)                              
                GainBlock(FindBuff("Mental Fortress").Intensity);
            if (oldStance != Stance && oldStance == "Calm")
            {
                if (FindRelic("Violet") != null)
                    Energy += 3;
                else Energy += 2;
            }               
            else if (oldStance != Stance && Stance == "Divinity")
                Energy += 3;
            if (Card.FindCard("Flurry of Blows", DiscardPile) is Card flurryOfBlows && flurryOfBlows != null && Hand.Count < 10) 
                flurryOfBlows.MoveCard(DiscardPile, Hand);               
        }

        // Method to look if hand is full and sending card to discard pile if hand is full
        public void AddToHand(Card card)
        {
            if (card == null) return;
            if (Hand.Count < 10)
                Hand.Add(card);
            else DiscardPile.Add(card);
        }

        public void AddToPotions(Potion potion)
        {
            if (potion == null) return;
            if (Potions.Count < PotionSlots)
                Potions.Add(potion);;
        }

        public void AddToDeck(Card card)
        {
            if (card == null) return;
            if (card.Type == "Curse")
            {
                if (FindRelic("Omamori") is Relic omamori && omamori.EffectAmount > 0)
                {
                    omamori.EffectAmount--;
                    return;
                }
                else if (FindRelic("Darkstone") != null)
                    SetMaxHP(6);
            }       
            if ((card.Type == "Attack" && FindRelic("Molten Egg") != null) || (card.Type == "Skill" && FindRelic("Toxic Egg") != null) || (card.Type == "Power" && FindRelic("Frozen Egg") != null))
                card.UpgradeCard();
            if (FindRelic("Ceramic") != null)
                GoldChange(9);
            Deck.Add(new(card));
            Console.WriteLine(card != null ? $"You have added {card.Name} to your Deck!" : "This was a check, remove now.\n");
        }

        public void DrawCards(int cards, List<Enemy> encounter)
        {
            while (Hand.Count < 10)
            {
                if (DrawPile.Count == 0)
                    Discard2Draw();
                if (DrawPile.Count == 0)
                    break;
                DrawPile.Last().MoveCard(DrawPile, Hand);
                if (Hand.Last().Name == "Deus Ex Machina")
                {
                    for (int i = 0; i < Hand.Last().GetMagicNumber(); i++)
                        AddToHand(new Card(Dict.cardL[336]));
                    Hand.Last().Exhaust(this, encounter, this.Hand);
                }
                cards--;
                if (cards == 0)
                    return;
            }
        }

        // Takes all cards in discard, moves them to drawpile, and then shuffles the drawpile
        public void Discard2Draw()
        {
            for (int i = DiscardPile.Count; i > 0; i--)
                DiscardPile.Last().MoveCard(DiscardPile, DrawPile);
            ShuffleDrawPile();
        }

        // Method for adding to current energy amount for the turn
        public void GainTurnEnergy(int energy)
        {
            this.Energy += energy;
            Console.WriteLine($"The {Name} gained {energy} Energy!");
        }

        public void GoldChange(int amount) //For when rewards are set
        {
            if (FindRelic("Ectoplasm") != null && amount > 0)
            {
                Console.WriteLine("You gained no Gold due to Ectoplasm's Effect!");
                return;
            }
            Gold += amount;
            if (amount > 0)
            {
                if (FindRelic("Bloody") != null)
                    HealHP(5);
                Console.WriteLine($"The {Name} gained {amount} Gold!");
            }               
            else Console.WriteLine($"The {Name} lost {amount * -1} Gold!");
        }


        public void ResetHeroAfterCombat()
        {
            Buffs.Clear();
            Energy = 0;
            DrawPile.Clear();
            Hand.Clear();
            DiscardPile.Clear();
            ExhaustPile.Clear();
            if (Name == "Defect")
                OrbSlots = 3;
            else OrbSlots = 1;
            if (FindRelic("Red Skull") is Relic redSkull && redSkull != null)
                redSkull.IsActive = false;
            if (FindRelic("Emotion") is Relic emotionChip && emotionChip != null)
                emotionChip.IsActive = false;
        }

        public void CombatRewards(string roomLocation)
        {
            AddToDeck(Card.ChooseCard(Card.RandomCards(Name, 3), "add to your Deck"));
            GoldChange(Convert.ToInt32((roomLocation == "Boss" ? HeroRNG.Next(95,106) : roomLocation == "Elite" ? HeroRNG.Next(35,36) : HeroRNG.Next(10,21)) * (FindRelic("Golden Idol") != null ? 1.25 : 1.00)));
            if (roomLocation == "Boss")

            if (HeroRNG.Next(10) < PotionChance || FindRelic("White Beast Statue") != null)
            {
                AddToPotions(Potion.RandomPotion());
                PotionChance--;
            }
            else PotionChance++;
        }

        public void ShuffleDrawPile()
        {
            if (FindRelic("Melange") != null)
                Card.Scry(this, 3);
            if (FindRelic("The Abacus") != null)
                GainBlock(6);
            if (FindRelic("Sundial") is Relic sundial && sundial != null)
            {
                sundial.PersistentCounter--;
                if (sundial.PersistentCounter == 0)
                {
                    GainTurnEnergy(2);
                    sundial.PersistentCounter = sundial.EffectAmount;
                }
            }
            int i = DrawPile.Count;
            while (i > 1)
            {
                i--;
                int j = HeroRNG.Next(i + 1);
                (DrawPile[i], DrawPile[j]) = (DrawPile[j], DrawPile[i]);
            }
        }
    }
}