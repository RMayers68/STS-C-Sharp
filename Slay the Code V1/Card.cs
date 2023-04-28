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
        public int AttackLoops { get; set; }
        public int BlockAmount { get; set; }
        public int BlockLoops { get; set; }
        public int MagicNumber { get; set; } // Ironclad self-damage, Silent discards, Defect Orb Channels, and Watcher Scrys, among other misc uses
        public int BuffID { get; set; }
        public int BuffAmount { get; set; }
        public int CardsDrawn { get; set; }
        public int EnergyGained { get; set; }
        public bool Targetable { get; set; }
        public bool HeroBuff { get; set; }
        public bool EnemyBuff { get; set; }
        public bool SingleAttack { get; set; }
        public bool AttackAll { get; set; }
        public bool SelfDamage { get; set; }
        public bool Discards { get; set; }
        public bool OrbChannels { get; set; }
        public bool Scrys { get; set; }
        public bool Upgraded { get; set; }
        public int GoldCost { get; set; }
        public int TmpEnergyCost { get; set; }

        public static readonly Random CardRNG = new();

        // constructors
        public Card()
        {
            this.Name = "Purchased";
            this.GoldCost = 0;
        }
        
        //accessors and mutators
        public void SetAttackDamage(int addedDamage)
        { this.AttackDamage += addedDamage; }

        public void SetBlockAmount(int addedDamage)
        { this.BlockAmount += addedDamage; }

        public void SetEnergyCost(int energyCost)
        {
            EnergyCost = energyCost;
            TmpEnergyCost = energyCost;
        }
        public void SetTmpEnergyCost(int tmpEnergyCost)
        { TmpEnergyCost = tmpEnergyCost; }

        public int GetGoldCost()
        { return GoldCost; }

        public int GetMagicNumber()
        { return MagicNumber; }

        public string GetName()
        { return $"{Name}{(Upgraded ? "+" : "")}"; }

        public bool IsUpgraded()
        { return Upgraded; }

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
                return $"Name: {Name}{(Upgraded ? "+" : "")}\nType: {Type}\nEffect: {GetDescription()}";
            return $"Name: {Name}{(Upgraded ? "+" : "")}\nEnergy Cost: {(EnergyCost > TmpEnergyCost ? $"{TmpEnergyCost}" : $"{EnergyCost}" )} \tType: {Type}\nEffect: {GetDescription()}\n";
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
            if (list.Count < 0) { return null; }
            Console.WriteLine($"Which card would you like to {action}?");
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}:{list[i].Name}");
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
                //hero.AddToHand(new Card(Dict.cardL[296]));
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
                //cards.Add(new Card(referenceCard));
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

            // Moves the Card Played from Hand to Designated Location and check certain relic effects (that can be said in a lot of sections oops)
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
                        hero.Hand[CardRNG.Next(hero.Hand.Count)].SetTmpEnergyCost(0);
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
                hero.Energy = 0;
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
            //else if (hero.HasBuff("Hex"))
            //hero.AddToDrawPile(new(Dict.cardL[356]),true);

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
        public virtual string GetDescription()
        {
            return (DescriptionModifier + Name switch
            {
                _ => "",
            });
        }

        //"Rare" CardRNG.Next(135, 166); "Uncommon" CardRNG.Next(68, 83); CardRNG.Next(45, 56);

        public virtual void UpgradeCard()
        {
            if ((Upgraded && Name != "Searing Blow") || Type == "Curse" || (Type == "Status" && Name != "Burn"))
                return;
            switch (Name)
            {
                case "Bash":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Battle Trance":
                    CardsDrawn++;
                    break;
                case "Berserk":
                    BuffAmount--;
                    break;
                case "Blood for Blood":
                    EnergyCost--;
                    AttackDamage += 4;
                    break;
                case "Bloodletting":
                    EnergyGained++;
                    break;
                case "Bludgeon":
                    AttackDamage += 10;
                    break;
                case "Body Slam":
                    EnergyCost--;
                    break;
                case "Burning Pact":
                    CardsDrawn++;
                    break;
                case "Carnage":
                    AttackDamage += 8;
                    break;
                case "Clash":
                    AttackDamage += 4;
                    break;
                case "Cleave":
                    AttackDamage += 3;
                    break;
                case "Clothesline":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Combust":
                    BuffAmount += 2;
                    break;
                case "Corruption":
                    EnergyCost--;
                    break;
                case "Dark Embrace":
                    EnergyCost--;
                    break;
                case "Demon Form":
                    BuffAmount++;
                    break;
                case "Disarm":
                    BuffAmount--;
                    break;
                case "Double Tap":
                    BuffAmount++;
                    break;
                case "Dropkick":
                    AttackDamage += 3;
                    break;
                case "Dual Wield":
                    MagicNumber++;
                    break;
                case "Entrench":
                    EnergyCost--;
                    break;
                case "Evolve":
                    BuffAmount++;
                    break;
                case "Exhume":
                    EnergyCost--;
                    break;
                case "Feed":
                    AttackDamage += 2;
                    MagicNumber++;
                    break;
                case "Feel No Pain":
                    BuffAmount++;
                    break;
                case "Fiend Fire":
                    AttackDamage += 3;
                    break;
                case "Fire Breathing":
                    BuffAmount += 4;
                    break;
                case "Flame Barrier":
                    BlockAmount += 4;
                    BuffAmount += 2;
                    break;
                case "Flex":
                    BuffAmount += 2;
                    break;
                case "Ghostly Armor":
                    BlockAmount += 3;
                    break;
                case "Havoc":
                    EnergyCost--;
                    break;
                case "Headbutt":
                    AttackDamage += 3;
                    break;
                case "Heavy Blade":
                    MagicNumber += 2;
                    break;
                case "Hemokinesis":
                    AttackDamage += 5;
                    break;
                case "Immolate":
                    AttackDamage += 7;
                    break;
                case "Impervious":
                    BlockAmount += 10;
                    break;
                case "Infernal Blade":
                    EnergyCost--;
                    break;
                case "Inflame":
                    BuffAmount++;
                    break;
                case "Intimidate":
                    BuffAmount++;
                    break;
                case "Iron Wave":
                    BlockAmount += 2;
                    AttackDamage += 2;
                    break;
                case "Juggernaut":
                    BuffAmount += 2;
                    break;
                case "Metallicize":
                    BuffAmount++;
                    break;
                case "Offering":
                    CardsDrawn += 2;
                    break;
                case "Perfected Strike":
                    MagicNumber++;
                    break;
                case "Pommel Strike":
                    AttackDamage++;
                    CardsDrawn++;
                    break;
                case "Power Through":
                    BlockAmount += 5;
                    break;
                case "Pummel":
                    AttackLoops++;
                    break;
                case "Rage":
                    BuffAmount += 2;
                    break;
                case "Rampage":
                    MagicNumber += 3;
                    break;
                case "Reaper":
                    AttackDamage++;
                    break;
                case "Reckless Charge":
                    AttackDamage += 3;
                    break;
                case "Rupture":
                    BuffAmount++;
                    break;
                case "Searing Blow":
                    MagicNumber++;
                    AttackDamage += MagicNumber + 3;
                    break;
                case "Second Wind":
                    BlockAmount += 2;
                    break;
                case "Seeing Red":
                    EnergyCost--;
                    break;
                case "Sentinel":
                    BlockAmount += 3;
                    EnergyGained++;
                    break;
                case "Sever Soul":
                    AttackDamage += 6;
                    break;
                case "Shockwave":
                    BuffAmount += 2;
                    break;
                case "Shrug It Off":
                    BlockAmount += 3;
                    break;
                case "Spot Weakness":
                    BuffAmount++;
                    break;
                case "Sword Boomerang":
                    AttackLoops++;
                    break;
                case "Thunderclap":
                    AttackDamage += 3;
                    break;
                case "True Grit":
                    BlockAmount += 2;
                    break;
                case "Twin Strike":
                    AttackDamage += 2;
                    break;
                case "Uppercut":
                    BuffAmount++;
                    break;
                case "Warcry":
                    CardsDrawn++;
                    break;
                case "Whirlwind":
                    AttackDamage += 3;
                    break;
                case "Wild Strike":
                    AttackDamage += 5;
                    break;
                case "A Thousand Cuts":
                    BuffAmount++;
                    break;
                case "Accuracy":
                    BuffAmount += 2;
                    break;
                case "Acrobatics":
                    CardsDrawn++;
                    break;
                case "Adrenaline":
                    EnergyGained++;
                    break;
                case "Alchemize":
                    EnergyCost--;
                    break;
                case "All-Out Attack":
                    AttackDamage += 4;
                    break;
                case "Backflip":
                    BlockAmount += 3;
                    break;
                case "Backstab":
                    AttackDamage += 4;
                    break;
                case "Bane":
                    AttackDamage += 3;
                    break;
                case "Blade Dance":
                    MagicNumber++;
                    break;
                case "Blur":
                    BlockAmount += 3;
                    break;
                case "Bouncing Flask":
                    MagicNumber++;
                    break;
                case "Bullet Time":
                    EnergyCost--;
                    break;
                case "Burst":
                    BuffAmount++;
                    break;
                case "Caltrops":
                    BuffAmount += 2;
                    break;
                case "Catalyst":
                    MagicNumber++;
                    break;
                case "Choke":
                    BuffAmount += 2;
                    break;
                case "Cloak And Dagger":
                    MagicNumber++;
                    break;
                case "Concentrate":
                    MagicNumber--;
                    break;
                case "Corpse Explosion":
                    BuffAmount += 3;
                    break;
                case "Crippling Cloud":
                    BuffAmount += 3;
                    break;
                case "Dagger Spray":
                    AttackDamage += 2;
                    break;
                case "Dagger Throw":
                    AttackDamage += 3;
                    break;
                case "Dash":
                    AttackDamage += 3;
                    BlockAmount += 3;
                    break;
                case "Deadly Poison":
                    BuffAmount += 2;
                    break;
                case "Deflect":
                    BlockAmount += 3;
                    break;
                case "Die Die Die":
                    AttackDamage += 4;
                    break;
                case "Distraction":
                    EnergyCost--;
                    break;
                case "Dodge and Roll":
                    BlockAmount += 2;
                    BuffAmount += 2;
                    break;
                case "Doppelganger":
                    BuffAmount++;
                    break;
                case "Endless Agony":
                    AttackDamage += 2;
                    break;
                case "Envenom":
                    EnergyCost--;
                    break;
                case "Escape Plan":
                    BlockAmount += 2;
                    break;
                case "Eviscerate":
                    AttackDamage += 2;
                    break;
                case "Expertise":
                    CardsDrawn++;
                    break;
                case "Finisher":
                    AttackDamage += 2;
                    break;
                case "Flechettes":
                    AttackDamage += 2;
                    break;
                case "Flying Knee":
                    AttackDamage += 3;
                    break;
                case "Footwork":
                    BuffAmount++;
                    break;
                case "Glass Knife":
                    AttackDamage += 4;
                    break;
                case "Grand Finale":
                    AttackDamage += 10;
                    break;
                case "Heel Hook":
                    AttackDamage += 3;
                    break;
                case "Leg Sweep":
                    BuffAmount++;
                    BlockAmount += 3;
                    break;
                case "Malaise":
                    BuffAmount++;
                    break;
                case "Masterful Stab":
                    AttackDamage += 4;
                    break;
                case "Neutralize":
                    AttackDamage++;
                    BuffAmount++;
                    break;
                case "Nightmare":
                    EnergyCost--;
                    break;
                case "Noxious Fumes":
                    BuffAmount++;
                    break;
                case "Outmaneuver":
                    EnergyGained++;
                    break;
                case "Phantasmal Killer":
                    EnergyCost--;
                    break;
                case "Piercing Wail":
                    BuffAmount -= 2;
                    break;
                case "Poisoned Stab":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Predator":
                    AttackDamage += 5;
                    break;
                case "Prepared":
                    CardsDrawn++;
                    MagicNumber++;
                    break;
                case "Quick Slash":
                    AttackDamage += 4;
                    break;
                case "Reflex":
                    CardsDrawn++;
                    break;
                case "Riddle with Holes":
                    AttackDamage++;
                    break;
                case "Setup":
                    EnergyCost--;
                    break;
                case "Skewer":
                    AttackDamage += 3;
                    break;
                case "Slice":
                    AttackDamage += 3;
                    break;
                case "Sneaky Strike":
                    AttackDamage += 4;
                    break;
                case "Sucker Punch":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Survivor":
                    BlockAmount += 3;
                    break;
                case "Tactician":EnergyGained++;
                    break;
                case "Terror":
                    EnergyCost--;
                    break;
                case "Tools of the Trade":
                    EnergyCost--;
                    break;
                case "Unload":
                    AttackDamage += 4;
                    break;
                case "Well-Laid Plans":
                    BuffAmount++;
                    break;
                case "Wraith Form":
                    BuffAmount++;
                    break;
                case "Aggregate":
                    MagicNumber--;
                    break;
                case "All For One":
                    AttackDamage += 4;
                    break;
                case "Amplify":
                    BuffAmount++;
                    break;
                case "Auto-Shields":
                    BlockAmount += 4;
                    break;
                case "Ball Lightning":
                    AttackDamage += 3;
                    break;
                case "Barrage":
                    AttackDamage += 2;
                    break;
                case "Beam Cell":
                    AttackDamage++;
                    BuffAmount++;
                    break;
                case "Biased Cognition":
                    BuffAmount++;
                    break;
                case "Blizzard":
                    MagicNumber++;
                    break;
                case "Boot Sequence":
                    BlockAmount += 3;
                    break;
                case "Buffer":
                    BuffAmount++;
                    break;
                case "Bullseye":
                    AttackDamage += 3;
                    BuffAmount++;
                    break;
                case "Capacitor":
                    MagicNumber++;
                    break;
                case "Chaos":
                    BlockLoops++;
                    break;
                case "Charge Battery":
                    BlockAmount += 3;
                    break;
                case "Claw":
                    AttackDamage += 2;
                    break;
                case "Cold Snap":
                    AttackDamage += 3;
                    break;
                case "Compile Driver":
                    AttackDamage += 3;
                    break;              
                case "Consume":
                    BuffAmount++;
                    break;
                case "Coolheaded":
                    CardsDrawn++;
                    break;
                case "Core Surge":
                    AttackDamage += 4;
                    break;
                case "Creative AI":
                    EnergyCost--;
                    break;
                case "Defragment":
                    BuffAmount++;
                    break;
                case "Doom and Gloom":
                    AttackDamage += 4;
                    break;
                case "Double Energy":
                    EnergyCost--;
                    break;
                case "Dualcast":
                    EnergyCost--;
                    break;
                case "Electrodynamics":
                    BlockLoops++;
                    break;
                case "Equilibrium":
                    BlockAmount += 3;
                    break;
                case "Force Field":
                    BlockAmount += 4;
                    break;
                case "FTL":
                    AttackDamage++;
                    MagicNumber++;
                    break;
                case "Fusion":
                    EnergyCost--;
                    break;
                case "Genetic Algorithm":
                    MagicNumber++;
                    break;
                case "Glacier":
                    BlockAmount += 3;
                    break;
                case "Go for the Eyes":
                    AttackDamage++;
                    BuffAmount++;
                    break;
                case "Heatsinks":
                    BuffAmount++;
                    break;
                case "Hologram":
                    BlockAmount += 2;
                    break;
                case "Hyperbeam":
                    AttackDamage += 8;
                    break;
                case "Leap":
                    BlockAmount += 3;
                    break;
                case "Loop":
                    BuffAmount++;
                    break;
                case "Melter":
                    AttackDamage += 4;
                    break;
                case "Meteor Strike":
                    AttackDamage += 6;
                    break;
                case "Multi-Cast":
                    MagicNumber++;
                    break;
                case "Overclock":
                    CardsDrawn++;
                    break;
                case "Reboot":
                    CardsDrawn += 2;
                    break;
                case "Rebound":
                    AttackDamage += 3;
                    break;
                case "Recursion":
                    EnergyCost--;
                    break;
                case "Recycle":
                    EnergyCost--;
                    break;
                case "Reinforced Body":
                    BlockAmount += 2;
                    break;
                case "Reprogram":
                    BuffAmount++;
                    break;
                case "Rip and Tear":
                    AttackDamage += 2;
                    break;
                case "Scrape":
                    AttackDamage += 3;
                    CardsDrawn++;
                    break;
                case "Seek":
                    MagicNumber++;
                    break;
                case "Self Repair":
                    BuffAmount += 3;
                    break;
                case "Skim":
                    CardsDrawn++;
                    break;
                case "Stack":
                    MagicNumber += 3;
                    break;
                case "Static Discharge":
                    BuffAmount++;
                    break;
                case "Steam Barrier":
                    BlockAmount += 2;
                    break;
                case "Streamline":
                    AttackDamage += 5;
                    break;
                case "Sunder":
                    AttackDamage += 8;
                    break;
                case "Sweeping Beam":
                    AttackDamage += 3;
                    break;
                case "Tempest":
                    BlockLoops++;
                    break;
                case "Thunder Strike":
                    AttackDamage += 2;
                    break;
                case "TURBO":
                    EnergyGained++;
                    break;
                case "White Noise":
                    EnergyCost--;
                    break;
                case "Zap":
                    EnergyCost--;
                    break;
                case "Defend":
                    BlockAmount += 3;
                    break;
                case "Strike":
                    AttackDamage += 3;
                    break;
                case "Bowling Bash":
                    AttackDamage += 3;
                    break;
                case "Brilliance":
                    AttackDamage += 4;
                    break;
                case "Carve Reality":
                    AttackDamage += 4;
                    break;
                case "Collect":
                    BuffAmount++;
                    break;
                case "Conclude":
                    AttackDamage += 4;
                    break;
                case "Conjure Blade":
                    MagicNumber++;
                    break;
                case "Consecrate":
                    AttackDamage += 3;
                    break;
                case "Crescendo":
                    EnergyCost--;
                    break;
                case "Crush Joints":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Cut Through Fate":
                    AttackDamage += 2;
                    MagicNumber++;
                    break;
                case "Deceive Reality":
                    BlockAmount += 3;
                    break;
                case "Deus Ex Machina":
                    MagicNumber++;
                    break;
                case "Devotion":
                    BuffAmount++;
                    break;
                case "Empty Body":
                    BlockAmount += 3;
                    break;
                case "Empty Fist":
                    AttackDamage += 5;
                    break;
                case "Empty Mind":
                    CardsDrawn++;
                    break;
                case "Eruption":
                    EnergyCost--;
                    break;
                case "Evaluate":
                    BlockAmount += 4;
                    break;
                case "Fasting":
                    BuffAmount++;
                    break;
                case "Fear No Evil":
                    AttackDamage += 3;
                    break;
                case "Flurry Of Blows":
                    AttackDamage += 2;
                    break;
                case "Flying Sleeves":
                    AttackDamage += 2;
                    break;
                case "Follow-Up":
                    AttackDamage += 4;
                    break;
                case "Foresight":
                    BuffAmount++;
                    break;
                case "Halt":
                    BlockAmount++;
                    MagicNumber += 5;
                    break;
                case "Indignation":
                    BuffAmount += 2;
                    break;
                case "Inner Peace":
                    CardsDrawn++;
                    break;
                case "Judgment":
                    MagicNumber += 10;
                    break;
                case "Just Lucky":
                    MagicNumber++;
                    AttackDamage++;
                    BlockAmount++;
                    break;
                case "Lesson Learned":
                    AttackDamage += 3;
                    break;
                case "Like Water":
                    BuffAmount += 2;
                    break;
                case "Master Reality":
                    EnergyCost--;
                    break;
                case "Meditate":
                    MagicNumber++;
                    break;
                case "Mental Fortress":
                    BuffAmount += 2;
                    break;
                case "Nirvana":
                    BuffAmount++;
                    break;
                case "Omniscience":
                    EnergyCost--;
                    break;
                case "Perseverance":
                    BlockAmount += 2;
                    MagicNumber++;
                    break;
                case "Pray":
                    BuffAmount++;
                    break;
                case "Pressure Points":
                    BuffAmount += 3;
                    break;
                case "Prostrate":
                    BuffAmount++;
                    break;
                case "Protect":
                    BlockAmount += 4;
                    break;
                case "Ragnarok":
                    AttackDamage++;
                    AttackLoops++;
                    break;
                case "Reach Heaven":
                    AttackDamage += 5;
                    break;
                case "Rushdown":
                    EnergyCost--;
                    break;
                case "Sanctity":
                    BlockAmount += 3;
                    break;
                case "Sands of Time":
                    AttackDamage += 6;
                    break;
                case "Sash Whip":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Scrawl":
                    EnergyCost--;
                    break;
                case "Signature Move":
                    AttackDamage += 10;
                    break;
                case "Simmering Fury":
                    BuffAmount++;
                    break;
                case "Spirit Shield":
                    MagicNumber++;
                    break;
                case "Study":
                    EnergyCost--;
                    break;
                case "Swivel":
                    BlockAmount += 3;
                    break;
                case "Talk to the Hand":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Tantrum":
                    AttackLoops++;
                    break;
                case "Third Eye":
                    BlockAmount += 2;
                    MagicNumber += 2;
                    break;
                case "Tranquility":
                    EnergyCost--;
                    break;
                case "Vault":
                    EnergyCost--;
                    break;
                case "Vigilance":
                    BlockAmount += 4;
                    break;
                case "Wallop":
                    AttackDamage += 3;
                    break;
                case "Wave of the Hand":
                    BuffAmount++;
                    break;
                case "Weave":
                    AttackDamage += 2;
                    break;
                case "Wheel Kick":
                    AttackDamage += 5;
                    break;
                case "Windmill Strike":
                    AttackDamage += 3;
                    MagicNumber++;
                    break;
                case "Wreath of Flame":
                    BuffAmount += 3;
                    break;
                case "Bite":
                    AttackDamage++;
                    MagicNumber++;
                    break;
                case "J.A.X.":
                    BuffAmount++;
                    break;
                case "Shiv":
                    AttackDamage += 2;
                    break;
                case "Apotheosis":
                    EnergyCost--;
                    break;
                case "Bandage Up":
                    MagicNumber += 2;
                    break;
                case "Blind":
                    Targetable = false;
                    break;
                case "Dark Shackles":
                    BuffAmount -= 6;
                    break;
                case "Deep Breath":
                    CardsDrawn++;
                    break;
                case "Dramatic Entrance":
                    AttackDamage += 4;
                    break;
                case "Finesse":
                    BlockAmount += 2;
                    break;
                case "Flash of Steel":
                    AttackDamage += 3;
                    break;
                case "Good Instincts":
                    BlockAmount += 3;
                    break;
                case "Jack Of All Trades":
                    MagicNumber++;
                    break;
                case "Madness":
                    EnergyCost--;
                    break;
                case "Magnetism":
                    EnergyCost--;
                    break;
                case "Master of Strategy":
                    CardsDrawn++;
                    break;
                case "Panacea":
                    BuffAmount++;
                    break;
                case "Panache":
                    BuffAmount += 4;
                    break;
                case "Purity":
                    MagicNumber += 2;
                    break;
                case "Sadistic Nature":
                    BuffAmount += 2;
                    break;
                case "Swift Strike":
                    AttackDamage += 3;
                    break;
                case "Transmutation":
                    MagicNumber++;
                    break;
                case "Trip":
                    Targetable = false;
                    break;
                case "Impatience":
                    CardsDrawn++;
                    break;
                case "Panic Button":
                    BlockAmount += 10;
                    break;
                case "Chrysalis":
                    MagicNumber += 2;
                    break;
                case "Hand of Greed":
                    AttackDamage += 5;
                    MagicNumber += 5;
                    break;
                case "Mayhem":
                    EnergyCost--;
                    break;
                case "Metamorphosis":
                    MagicNumber += 2;
                    break;
                case "The Bomb":
                    BuffAmount += 10;
                    break;
                case "Violence":
                    MagicNumber++;
                    break;
                case "Ritual Dagger":
                    MagicNumber += 2;
                    break;
                case "Beta":
                    EnergyCost--;
                    break;
                case "Insight":
                    CardsDrawn++;
                    break;
                case "Miracle":
                    EnergyGained++;
                    break;
                case "Omega":
                    BuffAmount += 10;
                    break;
                case "Safety":
                    BlockAmount += 4;
                    break;
                case "Smite":
                    AttackDamage += 4;
                    break;
                case "Through Violence":
                    AttackDamage += 10;
                    break;
                case "Burn":
                    AttackDamage += 2;
                    break;
                case "Expunger":
                    AttackDamage += 6;
                    break;
                case "Become Almighty":
                    BuffAmount++;
                    break;
                case "Fame and Fortune":
                    MagicNumber += 5;
                    break;
                case "Live Forever":
                    BuffAmount += 2;
                    break;
            }
            Upgraded = true;
            Console.WriteLine($"{Name} has been upgraded!");
        }
    }
}