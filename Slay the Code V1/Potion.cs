namespace STV
{
    public class Potion
    {
        //attributes
        public string Name { get; set; }
        public string Rarity { get; set; }
        public int EffectAmount { get; set; }
        public int GoldCost { get; set; }

        private static readonly Random PotionRNG = new();

        //constructors
        public Potion()
        {
            this.Name = "Purchased";
            this.GoldCost = 0;
        }

        public Potion(string Name, string Rarity, int EffectAmount)
        {
            this.Name = Name;
            this.EffectAmount = EffectAmount;
            this.Rarity = Rarity;
        }

        public Potion(Potion potion)
        {
            this.Name = potion.Name;
            this.Rarity = potion.Rarity;
            this.EffectAmount = potion.EffectAmount;
            this.GoldCost = Rarity == "Rare" ? PotionRNG.Next(95, 106) : Rarity == "Uncommon" ? PotionRNG.Next(72, 79) : PotionRNG.Next(48, 53);
        }
        //string method
        public override string ToString()
        {
            return $"{Name} - {GetDescription()}";
        }

        public void UsePotion(Hero hero,List<Enemy> encounter, int turnNumber)                              // potion methods (correlating to PotionID)
        {
            int target = 0;
            if (hero.Relics.Find(x => x.Name == "Sacred Bark") != null)
                EffectAmount *= 2;
            switch (Name)
            {
                case "Fire Potion":
                    target = hero.DetermineTarget(encounter);
                    hero.NonAttackDamage(encounter[target], EffectAmount,Name);
                    break;
                case "Energy Potion":
                    hero.GainTurnEnergy(EffectAmount);
                    break;
                case "Strength Potion":
                    hero.AddBuff(4,EffectAmount);
                    break;
                case "Block Potion":
                    hero.GainBlock(EffectAmount, encounter);
                    break;
                case "Fear Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(1, EffectAmount, hero);
                    break;
                case "Swift Potion":
                    hero.DrawCards(EffectAmount);
                    break;
                case "Weak Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(2, EffectAmount, hero);
                    break;
                case "Focus Potion":
                    hero.AddBuff(7, EffectAmount);
                    break;
                case "Cultist Potion":
                    hero.AddBuff(3, EffectAmount);
                    break;
                case "Attack Potion" or "Skill Potion" or "Power Potion" or "Colorless Potion":
                    Card cardPotion;
                    if (Name == "Colorless Potion")
                        //cardPotion = new(Card.PickCard(Card.RandomCards("Colorless", 3), "add to your hand"));
                        //else cardPotion = new(Card.PickCard(Card.RandomCards(hero.Name, 3, Name.Split(" ")[0]),"add to your hand"));
                        //cardPotion.SetTmpEnergyCost(0);
                        for (int i = 0; i < EffectAmount; i++) ;
                        //hero.AddToHand(cardPotion);                   
                    break;
                case "Blessing of the Forge":
                    foreach (Card c in hero.Hand)
                        c.UpgradeCard();
                    break;
                case "Dexterity Potion":
                    hero.AddBuff(9, EffectAmount);
                    break;
                case "Explosive Potion":
                    foreach (Enemy e in encounter)
                        hero.NonAttackDamage(e, EffectAmount, Name);
                    break;
                case "Flex Potion":
                    hero.AddBuff(4, EffectAmount);
                    hero.AddBuff(30, EffectAmount);
                    break;
                case "Speed Potion":
                    hero.AddBuff(4, EffectAmount);
                    hero.AddBuff(97, EffectAmount);
                    break;
                case "Blood Potion":
                    hero.HealHP(Convert.ToInt32(hero.MaxHP * (EffectAmount / 100.0)));
                    break;
                case "Poison Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(39, EffectAmount, hero);
                    break;
                case "Bottled Miracle":
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        if (hero.Hand.Count < 10) ;
                            //hero.Hand.Add(new Card(Dict.cardL[336]));
                        //else hero.DiscardPile.Add(new Card(Dict.cardL[336]));
                    }
                    break;
                case "Liquid Bronze":
                    hero.AddBuff(41,EffectAmount); 
                    break;
                case "Ancient Potion":
                    hero.AddBuff(8, EffectAmount);
                    break;
                case "Distilled Chaos":
                    for (int i = 0; i < EffectAmount; i++ )
                    {
                        Card distilledChaos = hero.DrawPile.Last();
                        distilledChaos.CardAction(hero,encounter,turnNumber);
                        if (distilledChaos.GetDescription().Contains("Exhaust") || distilledChaos.Type == "Status" || distilledChaos.Type == "Curse")
                            distilledChaos.Exhaust(hero, encounter, hero.DrawPile);
                        else if (distilledChaos.Type == "Power")
                            hero.DrawPile.Remove(distilledChaos);
                        else if (Name == "Tantrum")
                            hero.ShuffleDrawPile();
                        else
                            distilledChaos.MoveCard(hero.DrawPile, hero.DiscardPile);
                    }
                    break;
                case "Duplication Potion":
                    hero.AddBuff(99,EffectAmount);
                    break;
                case "Essence of Steel":
                    hero.AddBuff(95,EffectAmount);
                    break;
                case "Gambler's Brew":
                    Battle.GamblingIsGood(hero);
                    break;
                case "Liquid Memories":
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        Card liquidMemories = Card.PickCard(hero.DiscardPile, "add to your hand");
                        if (hero.Hand.Count < 10)
                        {
                            liquidMemories.MoveCard(hero.DiscardPile, hero.Hand);
                            liquidMemories.SetTmpEnergyCost(0);
                        }                      
                    }
                    break;
                case "Regen Potion":
                    hero.AddBuff(98, EffectAmount);
                    break;
                case "Potion of Capacity":
                    hero.OrbSlots += EffectAmount;
                    break;
                case "Elixir":
                    int exhaustChoice = -1;
                    int exhaustAmount = hero.Hand.Count;
                    while (exhaustChoice != 0 && exhaustAmount > 0)
                    {
                        Console.WriteLine($"\nEnter the number of the card you would like to discard or hit 0 to move on.");
                        for (int i = 1; i <= exhaustAmount; i++)
                            Console.WriteLine($"{i}:{hero.DrawPile[hero.Hand.Count - i].Name}");
                        while (!Int32.TryParse(Console.ReadLine(), out exhaustChoice) || exhaustChoice < 0 || exhaustChoice > exhaustAmount)
                            Console.WriteLine("Invalid input, enter again:");
                        if (exhaustChoice > 0)
                        {
                            Card exhaustedCard = hero.Hand[^exhaustChoice];
                            exhaustedCard.MoveCard(hero.Hand, hero.DiscardPile);
                            exhaustAmount--;
                        }
                    }
                    break;
                case "Cunning Potion":
                    Card.AddShivs(hero, EffectAmount);
                    break;
                case "Stance Potion":
                    int stanceChoice = -1;
                    Console.WriteLine($"\nWhat stance would you like to chnge to?\n1: Wrath \n2: Calm");
                    while (!Int32.TryParse(Console.ReadLine(), out stanceChoice) || stanceChoice < 1 || stanceChoice > 2)
                        Console.WriteLine("Invalid input, enter again:");
                    if (stanceChoice == 1)
                        hero.SwitchStance("Wrath");
                    else hero.SwitchStance("Calm");
                    break;
                case "Entropic Brew":
                    for (int i = 0; i < hero.PotionSlots - hero.Potions.Count; i++)
                        hero.Potions.Add(new Potion(Dict.potionL[PotionRNG.Next(42)]));
                    break;
                case "Fairy in a Bottle":
                    hero.CombatHeal(Convert.ToInt32(hero.MaxHP * (EffectAmount / 100.0)));
                    break;
                case "Fruit Juice":
                    hero.SetMaxHP(EffectAmount);
                    break;
                case "Smoke Bomb":
                    // Code combat to end
                    break;
                case "Snecko Oil":
                    hero.DrawCards(EffectAmount);
                    foreach (Card c in hero.Hand)
                        c.SetEnergyCost(PotionRNG.Next(4));
                    break;
                case "Essence of Darkness":
                    for (int i = 0; i < hero.OrbSlots * EffectAmount; i++)
                        Orb.ChannelOrb(hero,encounter, 2);
                    break;
                case "Heart of Iron":
                    hero.AddBuff(32, EffectAmount);
                    break;
                case "Ghost in a Jar":
                    hero.AddBuff(52,EffectAmount);
                    break;
                case "Ambrosia":
                    hero.SwitchStance("Divinity");
                    break;
            }
            // Toy Ornithopter Relic Check && Effect
            if (hero.Relics.Find(x => x.Name == "Toy Ornithopter") != null)
                hero.CombatHeal(5);
            hero.Potions.Remove(this);
        }

        public static Potion RandomPotion(Hero hero)
        {
            if (hero.HasRelic("Sozu"))
            {
                Console.WriteLine("You cant take the potion due to Sozu. (Hope you didn't spend gold!)");
                return null;
            }
            return new(Dict.potionL[PotionRNG.Next(42)]);
        }
        public string GetDescription(Hero? hero = null)
        {
            int sacredBark = 1;
            if (hero != null && hero.Relics.Find(x => x.Name == "Sacred Bark") != null)
                sacredBark++;
            return Name switch
            {
                "Fire Potion" => $"Deal {EffectAmount * sacredBark} damage.",
                "Energy Potion" => $"Gain {EffectAmount * sacredBark} Energy.",
                "Strength Potion" => $"Gain {EffectAmount * sacredBark} Strength.",
                "Block Potion" => $"Gain {EffectAmount * sacredBark} Block.",
                "Fear Potion" => $"Apply {EffectAmount * sacredBark} Vulnerable.",
                "Swift Potion" => $"Draw {EffectAmount * sacredBark} cards.",
                "Weak Potion" => $"Apply {EffectAmount * sacredBark} Weak.",
                "Focus Potion" => $"Gain {EffectAmount * sacredBark} Focus.",
                "Liquid Bronze" => $"Gain {EffectAmount * sacredBark} Thorns.",
                "Attack Potion" => $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Attack cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.",
                "Blessing of the Forge" => $"Upgrade all cards in your hand for the rest of combat.",
                "Colorless Potion" => $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Colorless cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.",
                "Dexterity Potion" => $"Gain {EffectAmount * sacredBark} Dexterity.",
                "Explosive Potion" => $"Deal {EffectAmount * sacredBark} damage to ALL enemies.",
                "Flex Potion" => $"Gain {EffectAmount * sacredBark} Strength. At the end of your turn, lose {EffectAmount * sacredBark} Strength.",
                "Power Potion" => $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Power cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.",
                "Skill Potion" => $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Skill cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.",
                "Speed Potion" => $"Gain {EffectAmount * sacredBark} Dexterity. At the end of your turn, lose {EffectAmount * sacredBark} Dexterity.",
                "Blood Potion" => $"Heal for {EffectAmount * sacredBark}% of your Max HP.",
                "Poison Potion" => $"Apply {EffectAmount * sacredBark} Poison.",
                "Bottled Miracle" => $"Add {EffectAmount * sacredBark} Miracles to your hand.",
                "Ancient Potion" => $"Gain {EffectAmount * sacredBark} Artifact.",
                "Distilled Chaos" => $"Play the top {EffectAmount * sacredBark} cards of your draw pile.",
                "Duplication Potion" => $"This turn, your {(sacredBark == 2 ? "next 2 cards are" : "next card is")} played twice.",
                "Essence of Steel" => $"Gain {EffectAmount * sacredBark} Plated Armor.",
                "Gambler's Brew" => $"Discard any number of cards, then draw that many.",
                "Liquid Memories" => $"Choose {(sacredBark == 2 ? "2 cards" : "a card")} in your discard pile and return {(sacredBark == 2 ? "them" : "it")} to your hand. {(sacredBark == 2 ? "They cost" : "It costs")} 0 this turn.",
                "Regen Potion" => $"Gain {EffectAmount * sacredBark} Regeneration.",
                "Potion of Capcity" => $"Gain {EffectAmount * sacredBark} Orb slots.",
                "Elixir" => $"Exhaust any number of cards in your hand.",
                "Cunning Potion" => $"Add {EffectAmount * sacredBark} Upgraded Shivs to your hand.",
                "Stance Potion" => $"Enter Calm or Wrath.",
                "Cultist Potion" => $"Gain {EffectAmount * sacredBark} Ritual.",
                "Entropic Brew" => $"Fill all your empty potion slots with random potions.",
                "Fairy in a Bottle" => $"When you would die, heal to {EffectAmount * sacredBark} of your Max HP instead and discard this potion.",
                "Fruit Juice" => $"Gain {EffectAmount * sacredBark} Max HP.",
                "Smoke Bomb" => $"Escape from a non-boss combat. Receive no rewards.",
                "Snecko Oil" => $"Draw {EffectAmount * sacredBark} cards. Randomize the costs of all cards in your hand for the rest of the combat.",
                "Essence of Darkness" => $"Channel {EffectAmount * sacredBark} Dark for each orb slot.",
                "Heart of Iron" => $"Gain {EffectAmount * sacredBark} Metallicize.",
                "Ghost in a Jar" => $"Gain {EffectAmount * sacredBark} Intangible.",
                "Ambrosia" => $"Enter Divinity Stance.",
                _ => "",
            };
        }
    }
}