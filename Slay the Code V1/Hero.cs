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
        public int RemoveCost { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> DrawPile { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> DiscardPile { get; set; }
        public List<Card> ExhaustPile { get; set; }
        public List<Orb> Orbs { get; set; }
        public List<Potion> Potions { get; set; }
        public List<Enemy> Encounter { get; set; }

        private static readonly Random HeroRNG = new();

        public Hero(int choice)
        {
            {
                Name = choice switch
                {
                    1 => "Ironclad",
                    2 => "Silent",
                    3 => "Defect",
                    _ => "Watcher",
                };
                MaxHP = 80;
                OrbSlots = 1;
                if (choice == 2)
                    MaxHP = 70;
                else if (choice == 3)
                {
                    MaxHP = 75;
                    OrbSlots = 3;
                }                 
                else if (choice == 4)
                    MaxHP = 72;
                Hp = MaxHP;
                MaxEnergy = 3;
                Energy = 0;
                Block = 0;
                Deck = new();
                DrawPile = new();
                Hand = new();
                DiscardPile = new();
                ExhaustPile = new();
                Buffs = new();
                Relics = new();
                Potions = new();
                Actions = new();
                Orbs = new();
                Stance = "None";               
                Gold = 99;
                EasyFights = 0;
                PotionSlots = 3;
                PotionChance = 4;
                RemoveCost = 75;
            }
        }

        // Initial Deck
        public void StartingDeck()
        {
            for (int i = 0; i < 8; i++)
            {
                while (i < 4)
                {                   
                    AddToDeck(new Defend());
                    i++;
                }
                while (i < 8)
                {
                    AddToDeck(new Strike());
                    i++;
                }
            }
            switch (Name)
            {
                case "Ironclad":
                    AddToDeck(new Strike());
                    AddToDeck(new Bash());
                    break;
                case "Silent":
                    AddToDeck(new Strike());
                    AddToDeck(new Defend());
                    AddToDeck(new Survivor());
                    AddToDeck(new Neutralize());
                    break;
                case "Defect":
                    AddToDeck(new Zap());
                    AddToDeck(new Dualcast());
                    break;
                case "Watcher":
                    AddToDeck(new Eruption());
                    AddToDeck(new Vigilance());
                    break;
            }
            Deck.Sort();
        }

        // Hero exclusive methods

        public void CardBlock(int block, List<Enemy> encounter = null)
        {
            if (FindBuff("No Block") != null || block == 0)
                return;
            GainBlock(block, encounter);
        }

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

        public void SelfDamage(int damage)
        {
            if (Hp <= 0) return;
            damage = HPLoss(damage);
            Console.WriteLine($"{Name} hurt themselves for {damage} damage!");
            if (damage > 0 && FindBuff("Rupture") is Buff rupture && rupture != null)
                AddBuff(4, rupture.Intensity);
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
            if (FindBuff("Mental Fortress") is Buff mental && mental != null)                              
                GainBlock(mental.Intensity);
            if (oldStance != Stance)
            {
                if (oldStance == "Calm")
                {
                    if (HasRelic("Violet"))
                        Energy += 3;
                    else Energy += 2;
                }
                else if (oldStance == "Divinity")
                    Energy += 3;
                else if (oldStance == "Wrath" && FindBuff("Rushdown") is Buff rush && rush != null)
                    DrawCards(rush.Intensity);
            }          
            if (Card.FindCard("Flurry of Blows", DiscardPile) is Card flurryOfBlows && flurryOfBlows != null && Hand.Count < 10) 
                flurryOfBlows.MoveCard(DiscardPile, Hand);               
        }

        public void DrawCards(int cards)
        {
            if (HasBuff("No Draw") || Hand.Count >= 10)
            {
                Console.WriteLine("You cannot draw any more cards this turn.");
            }
            else
            {
                for (int i = 0; i < cards; i++)
                {
                    if (Hand.Count == 10)
                        return;
                    if (DrawPile.Count == 0)
                        Discard2Draw();
                    if (DrawPile.Count == 0)
                        break;
                    Card drawCard = DrawPile.Last();
                    drawCard.MoveCard(DrawPile, Hand);
                    if (drawCard.Name == "Endless Agony")
                        AddToHand(drawCard.AddCard());
                    if (drawCard.Type == "Status" && FindBuff("Evolve") is Buff evolve && evolve != null)
                        DrawCards(evolve.Intensity);
                    if (HasBuff("Confused") && drawCard.EnergyCost < 0)
                        drawCard.SetEnergyCost(HeroRNG.Next(4));
                    if (FindBuff("Fire Breathing") is Buff fire && fire != null && (drawCard.Type == "Status" || drawCard.Type == "Curse"))
                        foreach (Enemy e in Encounter)
                            NonAttackDamage(e, fire.Intensity, fire.Name);
                    else if (drawCard.Name == "Deus Ex Machina")
                    {
                        drawCard.MoveCard(Hand, ExhaustPile);
                        for (int j = 0; j < drawCard.MagicNumber; i++) ;
                            AddToHand(new Miracle());
                    }
                }
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
            if (HasRelic("Ectoplasm") && amount > 0)
            {
                Console.WriteLine("You gained no Gold due to Ectoplasm's Effect!");
                return;
            }
            Gold += amount;
            if (amount > 0)
            {
                if (HasRelic("Bloody"))
                    HealHP(5);
                Console.WriteLine($"The {Name} gained {amount} Gold!");
            }               
            else Console.WriteLine($"The {Name} lost {amount * -1} Gold!");
        }


        public void ResetHeroAfterCombat()
        {
            Buffs.Clear();
            Energy = 0;
            if (Stance != "None")
                SwitchStance("None");
            DrawPile.Clear();
            Hand.Clear();
            DiscardPile.Clear();
            ExhaustPile.Clear();
            Encounter.Clear();  
            Orbs.Clear();
            if (Name == "Defect")
                OrbSlots = 3;
            else OrbSlots = 1;
            if (HasRelic("Red Skull"))
                FindRelic("Red Skull").IsActive = false;
            if (HasRelic("Emotion"))
                FindRelic("Emotion").IsActive = false;
            if (HasRelic("Centennial"))
                FindRelic("Centennial").IsActive = true;         
        }

        public void CombatRewards(string roomLocation)
        {
            int cardRewardAmount = 3;
            if (HasRelic("Question"))
                cardRewardAmount++;
            if (HasRelic("Busted Crown"))
                cardRewardAmount -= 2;
            AddToDeck(Card.ChooseCard(cardRewardAmount,"add to your Deck", HasRelic("Prismatic") ? "All Heroes" : Name));
            if (HasRelic("Prayer Wheel") && roomLocation == "Monster")
                AddToDeck(Card.ChooseCard(cardRewardAmount, "add to your Deck", HasRelic("Prismatic") ? "All Heroes" : Name));
            GoldChange(Convert.ToInt32((roomLocation == "Boss" ? HeroRNG.Next(95,106) : roomLocation == "Elite" ? HeroRNG.Next(35,36) : HeroRNG.Next(10,21)) * (HasRelic("Golden Idol") ? 1.25 : 1.00)));
            if (roomLocation == "Boss")
                ;// Nothing yet 
            else if (roomLocation == "Elite")
            {
                if (HasRelic("Black Star"))
                    AddToRelics(Relic.RandomRelic(this));
                AddToRelics(Relic.RandomRelic(this));
            }
            if (HeroRNG.Next(10) < PotionChance || HasRelic("White Beast Statue"))
            {
                AddToPotions(Potion.RandomPotion(this));
                PotionChance--;
            }
            else PotionChance++;
        }

        // DrawPile is shuffled whenever a card is added to the drawpile
        public void ShuffleDrawPile()
        {
            if (HasRelic("Melange"))
                Card.Scry(this, 3);
            if (HasRelic("The Abacus"))
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
            Shuffle();
        }

        public void Shuffle()
        {
            List<Card> newDrawPile = new();
            for(int i = DrawPile.Count-1; i >= 0; i--)
            {
                int j = HeroRNG.Next(i + 1);
                newDrawPile.Add(DrawPile[j]);
                DrawPile.RemoveAt(j);
            }
            DrawPile = new(newDrawPile);
        }

        // Methods to add to Hero lists
        public void AddToHand(Card card)
        {
            if (card == null) return;
            if (HasBuff("Master Reality"))
                card.UpgradeCard();
            if (Hand.Count < 10)
                Hand.Add(card);
            else DiscardPile.Add(card);
        }

        public void AddToDiscard(Card card)
        {
            if (card == null) return;
            if (HasBuff("Master Reality"))
                card.UpgradeCard();
            DiscardPile.Add(card);
        }

        public void AddToPotions(Potion potion)
        {
            if (potion == null) return;
            if (Potions.Count < PotionSlots)
            {
                Potions.Add(new(potion));
                Console.WriteLine($"You have added {potion.Name} to your potion bag.");
            }
                
            else Console.WriteLine("Your potions are full and you can not add more of them.");
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
                else if (HasRelic("Darkstone"))
                    SetMaxHP(6);
            }
            if ((card.Type == "Attack" && HasRelic("Molten Egg")) || (card.Type == "Skill" && HasRelic("Toxic Egg")) || (card.Type == "Power" && HasRelic("Frozen Egg")))
                card.UpgradeCard();
            if (HasRelic("Ceramic"))
                GoldChange(9);
            Deck.Add(card.AddCard());
            Console.WriteLine(card != null ? $"You have added {card.Name} to your Deck!" : "This was a check, remove now.\n");
        }

        public void RemoveFromDeck(Card card)
        {
            if (card == null || card.Name == "Curse of the Bell" || card.Name == "Necronomicurse") return;
            if (card.Name == "Parasite")
                SetMaxHP(-3);
            Deck.Remove(card);
            Console.WriteLine($"You have removed {card.Name} to your Deck!");
        }

        public void AddToDrawPile(Card card, bool shuffle)
        {
            DrawPile.Add(card);
            if (HasBuff("Master Reality"))
                card.UpgradeCard();
            if (shuffle)
                ShuffleDrawPile();
        }
        public void AddToRelics(Relic relic)
        {
            relic.RelicPickupEffect(this);
            Relics.Add(new(relic));
        }

        public void RemoveCounterCheck(Buff buff)
        {
            buff.Counter--;
            if (buff.Counter == 0)
                Buffs.Remove(buff);
        }
    }
}