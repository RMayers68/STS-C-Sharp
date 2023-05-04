namespace STV
{
    public abstract class Card : IEquatable<Card>, IComparable<Card>
    {
        // attributes
        public string Name { get; set; }
        public string Type { get; set; } // Attack, Skill, Power, Status or Curse
        public string Rarity { get; set; } //Common,Uncommon,Rare 
        public string DescriptionModifier { get; set; } // Added when effects add function to a card (e.g Meditate giving a card Retain, Bottles giving Innate) 
        public int EnergyCost { get; set; }
        public int AttackDamage { get; set; }
        public int BlockAmount { get; set; }
        public int MagicNumber { get; set; } // Ironclad self-damage, Silent discards, Defect Orb Channels, and Watcher Scrys, among other misc uses
        public int BuffID { get; set; }
        public int BuffAmount { get; set; }
        public int CardsDrawn { get; set; }
        public int EnergyGained { get; set; }
        public bool Upgraded { get; set; }
        public int GoldCost { get; set; }
        public int TmpEnergyCost { get; set; }

        public static readonly Random CardRNG = new();
        
        // Energy Cost Mutator

        public void SetEnergyCost(int energyCost)
        {
            EnergyCost = energyCost;
            TmpEnergyCost = energyCost;
        }

        //comparators and equals
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is not Card objAsPart) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Card other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }
        public override int GetHashCode()
        {
            return GoldCost + BlockAmount + MagicNumber;
        }

        public int CompareTo(Card other)
        {
            // A null value means that this object is greater.
            if (Name == null && other.Name == null) return 0;
            else if (Name == null) return -1;
            else if (other.Name == null) return 1;
            else return Name.CompareTo(other.Name);
        }

        // methods
        public override string ToString()
        {
            if (EnergyCost == -2)
                return $"Name: {Name}\nType: {Type}\nEffect: {GetDescription()}";
            return $"Name: {Name}\nEnergy Cost: {(EnergyCost > TmpEnergyCost ? $"{TmpEnergyCost}" : $"{EnergyCost}" )} \tType: {Type}\nEffect: {GetDescription()}\n";
        }

        
        public static Card FindCard(string cardName, List<Card> list)
        {
            return list.Find(x => x.Name == cardName);
        }

        public static List<Card> FindAllCardsInCombat(Hero hero, string name = "")
        {
            List<Card> allLists = new();
            if (name == "")
            {
                allLists.AddRange(hero.Hand);
                allLists.AddRange(hero.DiscardPile);
                allLists.AddRange(hero.DrawPile);
            }
            else
            {
                allLists.AddRange(hero.Hand.FindAll(x => x.Name.Contains(name)));
                allLists.AddRange(hero.DiscardPile.FindAll(x => x.Name.Contains(name)));
                allLists.AddRange(hero.DrawPile.FindAll(x => x.Name.Contains(name)));
            }            
            return allLists;
        }

        public static Card PickCard(List<Card> list, string action)
        {
            int cardChoice;
            if (list.Count < 1) { return new Purchased(); }
            Console.WriteLine($"Which card would you like to {action}?");
            for (int i = 1; i <= list.Count; i++)
                Console.WriteLine($"{i}:{list[i-1]}");
            while (!Int32.TryParse(Console.ReadLine(), out cardChoice) || cardChoice < 1 || cardChoice > list.Count)
                Console.WriteLine("Invalid input, enter again:");
            return list[cardChoice - 1];
        }

        public static Card ChooseCard(int amount,string action,string name, string typeExclusion = "")
        {
            int cardChoice = -1;
            List<Card> list = new(RandomCards(name, amount, typeExclusion));
            while (cardChoice != 0)
            {
                Console.WriteLine($"\nEnter the number of the card you would like to {action} or hit 0 to move on.");
                for (int i = 1; i <= amount; i++)
                    Console.WriteLine($"{i}:{list[^i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out cardChoice) || cardChoice < 0 || cardChoice > amount)
                    Console.WriteLine("Invalid input, enter again:");
                if (cardChoice > 0)
                {
                    return list[^cardChoice];
                }
            }
            return null;
        }
        //Moving Cards to different List methods
        public void MoveCard(List<Card> from, List<Card> to)
        {
            if (Name == "Purchased")
                return;
            from.Remove(this);
            to.Add(this);
        }
       
        public void Exhaust(Hero hero,List<Enemy> encounter, List<Card> leaveThisList)
        {
            MoveCard(leaveThisList, hero.ExhaustPile);
            if (Name == "Sentinel")
                hero.GainTurnEnergy(EnergyGained);
            if (hero.HasRelic("Charon's"))
            {
                foreach (Enemy e in encounter)
                    hero.NonAttackDamage(e, 3, "Charon's Ashes");
            }
            if (Name == "Necronomicurse")
                //hero.AddToHand(new(this));
            if (hero.HasRelic("Dead Branch"))
                //hero.AddToHand(new(Card.RandomCard(hero.Name)));
            if (hero.FindBuff("Dark Embrace") is Buff embrace && embrace != null)
                hero.DrawCards(embrace.Intensity);
            if (hero.FindBuff("Feel No Pain") is Buff feel && feel != null)
                hero.GainBlock(feel.Intensity, encounter);
            Console.WriteLine($"{Name} has been exhausted.");
        }

        public void Discard(Hero hero, List<Enemy> encounter, int turnNumber)
        {
            if (hero.Hand.Count < 1)
                return;
            if (Name == "Tactician")
                hero.GainTurnEnergy(EnergyGained);
            else if (Name == "Reflex")
                hero.DrawCards(MagicNumber);
            MoveCard(hero.Hand, hero.DiscardPile);
            if (hero.HasRelic("Tingsha"))
                hero.NonAttackDamage(encounter[CardRNG.Next(encounter.Count)],3,"Tingsha");
            if (hero.HasRelic("Tough Bandages"))
                hero.GainBlock(3, encounter);
            hero.AddAction("Discard",turnNumber);
            if (hero.HasRelic("Hovering Kite") && hero.Actions.FindAll(x => x == $"{turnNumber}: Discard").Count == 1)
                hero.Energy++;

        }

        public static void Scry(Hero hero, int amount)
        {
            int scryChoice = -1;
            if (hero.HasRelic("Golden Eye"))
                amount += 2;
            while (scryChoice != 0 && amount > 0)
            {
                Console.WriteLine($"\nEnter the number of the card you would like to scry into your discard pile or hit 0 to move on.");
                for (int i = 1; i <= amount; i++)
                    Console.WriteLine($"{i}:{hero.DrawPile[^i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out scryChoice) || scryChoice < 0 || scryChoice > amount)
                    Console.WriteLine("Invalid input, enter again:");
                if (scryChoice > 0)
                {
                    Card scryedCard = hero.DrawPile[^scryChoice];
                    scryedCard.MoveCard(hero.DrawPile, hero.DiscardPile);
                    amount--;
                }
            }
            if (hero.FindBuff("Nirvana") is Buff cobain && cobain != null /*Yikes*/)
                hero.GainBlock(cobain.Intensity);
            // Weave Check
            if (FindCard("Weave", hero.DiscardPile) is Card weave && weave != null && hero.Hand.Count < 10)
                weave.MoveCard(hero.DiscardPile, hero.Hand);
        }

        public static void AddShivs(Hero hero, int amount)
        {
            for (int i = 0; i < amount; i++) ;
                hero.AddToHand(new Shiv());
        }

        public static List<Card> RandomCards(string name, int count, string typeExclusion = "")
        {
            List<Card> cards = new();
            for (int i = 0; i < count; i++)
            {
                Card referenceCard;
                if (typeExclusion != "")
                    referenceCard = SpecificTypeRandomCard(name,typeExclusion);
                else referenceCard = RandomCard(name);
                cards.Add(referenceCard.AddCard());
            }
            return cards;
        }

        public static Card SpecificTypeRandomCard(string name = "All Heroes", string typeExclusion = "")
        {
            Card referenceCard;
            if (typeExclusion != null)
            {
                do
                    referenceCard = RandomCard(name);
                while (referenceCard.Type != typeExclusion);
                return referenceCard;
            }
            else return RandomCard(name);
        }

        public static Card SpecificRarityRandomCard(string name = "All Heroes", string typeExclusion = "")
        {
            Card referenceCard;
            if (typeExclusion != null)
            {
                do
                    referenceCard = RandomCard(name);
                while (referenceCard.Type != typeExclusion);
                return referenceCard;
            }
            else return RandomCard(name);
        }

        public static Card RandomCard(string type = "All Heroes")
        {
            return type switch
            {
                "Ironclad" => Dict.cardL[CardRNG.Next(73)],
                "Silent" => Dict.cardL[CardRNG.Next(73, 146)],
                "Defect" => Dict.cardL[CardRNG.Next(146, 219)],
                "Watcher" => Dict.cardL[CardRNG.Next(221, 294)],
                "Colorless" => Dict.cardL[CardRNG.Next(297, 332)],
                "Curse" => Dict.cardL[CardRNG.Next(346, 355)],
                _ => Dict.cardL[CardRNG.Next(294)],
            };
        }

        public void TransformCard(Hero hero)
        {
            Card transformedCard;
            string type;
            if (Type == "Curse" || Type == "Colorless")
                type = Type;
            else type = hero.Name;
            do
                transformedCard = RandomCard(type);
            while (transformedCard.Name == Name);
            hero.Deck.Remove(this);
            hero.AddToDeck(transformedCard);
            Console.WriteLine($"Your {Name} card transformed into {transformedCard.Name}!");
        }

        public void CardAction(Hero hero, List<Enemy> encounter, int turnNumber)
        {
            if (Type == "Attack" && hero.FindBuff("Free Attack") is Buff free && free != null)
            {
                TmpEnergyCost = 0;
                hero.RemoveCounterCheck(free);
            }
            // Check to see if the Card is Playable, if not leave function early
            if (TmpEnergyCost > hero.Energy)
            {
                Console.WriteLine($"You failed to play {Name}. You need {EnergyCost} Energy to play {Name}.");
                return;
            }
            if (Type == "Attack" && hero.FindBuff("Entangled") != null)
            {
                Console.WriteLine("You can't play an Attack as you are Entangled this turn.");
                return;
            }
            if (Name == "Clash" && !hero.Hand.All(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Clash as you have something that isn't an Attack in your hand.");
                return;
            }
            if (Name == "Signature Move" && hero.Hand.Any(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Signature Move as you have a different Attack in your hand.");
                return;
            }
            if (Name == "Grand Finale" && hero.DrawPile.Count != 0)
            {
                Console.WriteLine("You can't play Grand Finale because you have cards in your draw pile.");
                return;
            }
            if (GetDescription().Contains("Unplay"))
            {
                if (Type == "Curse" && hero.HasRelic("Blue Candle"))
                    hero.HPLoss(1);
                else if (!(Type == "Status" && hero.HasRelic("Medical Kit")))
                {
                    Console.WriteLine("This card is unplayable, read it's effects to learn more.");
                    return;
                }
            }

            // Moves the Card Played from Hand to Designated Location and check certain relic effects 
            if (FindCard(Name, hero.Hand) != null)
            {
                if (Type == "Skill")
                {
                    if (hero.HasBuff("Corruption"))
                    {
                        TmpEnergyCost = 0;
                        Exhaust(hero, encounter, hero.Hand);
                    }
                    foreach (Enemy e in encounter)
                    {
                        if (e.FindBuff("Enrage") is Buff enrage && enrage != null)
                            e.AddBuff(4, enrage.Intensity);
                    }
                }
                if (GetDescription().Contains("Exhaust") || Type == "Status" || Type == "Curse")
                {
                    if (hero.HasRelic("Strange") && CardRNG.Next(2) == 0)
                        MoveCard(hero.Hand, hero.DiscardPile);
                    Exhaust(hero, encounter, hero.Hand);
                }
                else if (Type == "Power")
                {
                    foreach (Card forceField in Card.FindAllCardsInCombat(hero, "Force Field"))
                        forceField.EnergyCost--;
                    if (hero.HasRelic("Bird"))
                        hero.CombatHeal(2);
                    hero.Hand.Remove(this);
                    if (hero.HasRelic("Mummified") && hero.Hand.Count != 0)
                        hero.Hand[CardRNG.Next(hero.Hand.Count)].TmpEnergyCost = 0;
                    if (hero.FindBuff("Heatsinks") is Buff heat && heat != null)
                        hero.DrawCards(heat.Intensity);
                    if (hero.FindBuff("Storm") is Buff storm && storm != null)
                        for (int i = 0; i < storm.Intensity; i++)
                            Orb.ChannelOrb(hero, encounter, 0);
                    if (encounter[0] is AwakenedOne aW && aW.HasBuff("Curiosity"))
                        aW.AddBuff(4, 1);
                }
                else if (Name == "Tantrum")
                {
                    MoveCard(hero.Hand, hero.DrawPile);
                    hero.ShuffleDrawPile();
                }
                else if (hero.FindBuff("Rebound") is Buff rebound && rebound != null)
                {
                    MoveCard(hero.Hand, hero.DrawPile);
                    hero.RemoveCounterCheck(rebound);
                }
                else
                    MoveCard(hero.Hand, hero.DiscardPile);
            }

            // Card Effects begin here
            Console.WriteLine($"You played {this.Name}.");
            int target = 0, extraDamage = 0, wallop = encounter[target].Hp, xEnergy = hero.Energy + (hero.HasRelic("Chemical X") ? 2 : 0);
            string lastCardPlayed = "";
            if (EnergyCost == -1)
                hero.Energy = xEnergy;
            else if (EnergyCost != -2)
                hero.Energy -= TmpEnergyCost;
            this.TmpEnergyCost = EnergyCost;

            foreach (Enemy e in encounter)
            {
                if (e.FindBuff("Slow") is Buff slow && slow != null)
                    slow.Intensity++;
                if (e.FindBuff("Time Warp") is Buff warp && warp != null)
                    warp.Counter--;
            }



            // Effects relating to playing an Attack card
            if (Type == "Attack")
            {
                if (hero.HasRelic("Duality"))
                {
                    hero.AddBuff(9, 1);
                    hero.AddBuff(97, 1);
                }
                if (hero.FindBuff("Vigor") is Buff vigor && vigor != null)
                {
                    extraDamage += vigor.Intensity;
                    hero.Buffs.Remove(vigor);
                }
                if (TmpEnergyCost == 0 && hero.HasRelic("Wrist"))
                    extraDamage += 4;
                if (Name.Contains("Strike") && hero.HasRelic("Strike"))
                    extraDamage += 3;
                if (Name.Contains("Shiv") && hero.FindBuff("Accuracy") is Buff accuracy && accuracy != null)
                    extraDamage += accuracy.Intensity;
                if (hero.FindBuff("Rage") is Buff rage && rage != null)
                    hero.GainBlock(rage.Intensity, encounter);
                if (hero.FindRelic("Nunchaku") is Relic nunchaku && nunchaku != null)
                {
                    nunchaku.PersistentCounter--;
                    if (nunchaku.PersistentCounter == 0)
                    {
                        hero.GainTurnEnergy(1);
                        nunchaku.PersistentCounter = nunchaku.EffectAmount;
                    }
                }
                if (hero.FindRelic("Pen Nib") is Relic penNib && penNib != null)
                {
                    penNib.PersistentCounter--;
                    if (penNib.PersistentCounter == 0)
                    {
                        extraDamage += extraDamage + AttackDamage;
                        penNib.PersistentCounter = penNib.EffectAmount;
                    }
                }
                if (hero.HasBuff("Double Damage"))
                    extraDamage += extraDamage + AttackDamage;
            }
            else if (hero.HasBuff("Hex"))
            hero.AddToDrawPile(new Dazed(),true);

            CardEffect(hero, encounter, turnNumber, extraDamage);

            // End of card action, card action being documented in turn list, check to play card twice
            if (hero.FindRelic("Ink") is Relic ink && ink != null)
            {
                ink.PersistentCounter--;
                if (ink.PersistentCounter == 0)
                {
                    hero.DrawCards(1);
                    ink.PersistentCounter = ink.EffectAmount;
                }
            }
            hero.AddAction($"{Name}-{Type} Played", turnNumber);
            if (hero.FindBuff("Panache") is Buff panache && panache != null)
            {
                panache.Counter++;
                if (panache.Counter == 5)
                {
                    panache.Counter = 0;
                    foreach (Enemy e in encounter)
                        hero.NonAttackDamage(e, panache.Intensity, panache.Name);
                }
            }
            if (hero.FindRelic("Orange") is Relic orange && orange.IsActive && hero.FindTurnActions(turnNumber, "Attack").Count > 0 && hero.FindTurnActions(1, "Skill").Count > 0 && hero.FindTurnActions(1, "Power").Count > 0)
            {
                hero.Buffs.RemoveAll(x => !x.BuffDebuff);
                orange.IsActive = false;
            }
            if (hero.FindBuff("Thousand Cuts") is Buff cuts && cuts != null)
                foreach (Enemy e in encounter)
                    hero.NonAttackDamage(e, cuts.Intensity, "Thousand Cuts");
            if (hero.FindBuff("After Image") is Buff image && image != null)
                hero.GainBlock(image.Intensity, encounter);
            foreach (Enemy e in encounter)
                if (e.FindBuff("Choked") is Buff choke && choke != null)
                    e.HPLoss(choke.Intensity);
            if (hero.FindBuff("Duplication") is Buff dupe && dupe != null)
            {
                TmpEnergyCost = 0;
                CardAction(hero, encounter, turnNumber);
                hero.RemoveCounterCheck(dupe);
            }
            if (Type == "Attack" && hero.FindRelic("Necro") is Relic necro && necro.IsActive && EnergyCost > 2)
            {
                TmpEnergyCost = 0;
                CardAction(hero, encounter, turnNumber);
                necro.IsActive = false;
            }
            if (hero.FindBuff("Echo Form") is Buff echo && echo != null && echo.Intensity < hero.FindTurnActions(turnNumber).Count)
            {
                TmpEnergyCost = 0;
                CardAction(hero, encounter, turnNumber);
            }
            if (Type == "Attack" && hero.FindBuff("Double Tap") is Buff dtap && dtap != null)
            {
                TmpEnergyCost = 0;
                CardAction(hero, encounter, turnNumber);
                hero.RemoveCounterCheck(dtap);
            }
            if (Type == "Skill" && hero.FindBuff("Burst") is Buff burst && burst != null)
            {
                TmpEnergyCost = 0;
                CardAction(hero, encounter, turnNumber);
                hero.RemoveCounterCheck(burst);
            }
            if (Type == "Power" && hero.FindBuff("Amplify") is Buff amplify && amplify != null)
            {
                TmpEnergyCost = 0;
                CardAction(hero, encounter, turnNumber);
                hero.RemoveCounterCheck(amplify);
            }
        }
        public abstract void CardEffect(Hero hero, List<Enemy> encounter, int turnNumber, int extraDamage);

        public abstract Card AddCard();

        // Description String
        public abstract string GetDescription();

        public virtual void UpgradeCard()
        {
            if ((Upgraded && Name != "Searing Blow") || Type == "Curse" || (Type == "Status" && Name != "Burn"))
                return;           
            Upgraded = true;
            Name += "+";
            Console.WriteLine($"{Name} has been Upgraded!");
        }
    }
}