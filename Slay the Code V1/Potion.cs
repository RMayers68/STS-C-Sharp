namespace STV
{
    public class Potion
    {
        //attributes
        public string Name { get; set; }
        public string Rarity { get; set; }
        public int EffectAmount { get; set; }
        public int GoldCost { get; set; }

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
            Random rng = new Random();
            this.GoldCost = Rarity == "Rare" ? rng.Next(95, 106) : Rarity == "Uncommon" ? rng.Next(72, 79) : rng.Next(48, 53);
        }
        //string method
        public override string ToString()
        {
            return $"{Name} - {GetDescription()}";
        }

        public void UsePotion(Hero hero,List<Enemy> encounter, Random rng)                              // potion methods (correlating to PotionID)
        {
            int target = 0;
            if (hero.Relics.Find(x => x.Name == "Sacred Bark") != null)
                EffectAmount *= 2;
            switch (Name)
            {
                case "Fire Potion":
                    target = hero.DetermineTarget(encounter);
                    hero.NonAttackDamage(encounter[target], EffectAmount);
                    break;
                case "Energy Potion":
                    hero.GainTurnEnergy(EffectAmount);
                    break;
                case "Strength Potion":
                    hero.AddBuff(4,EffectAmount);
                    break;
                case "Block Potion":
                    hero.GainBlock(EffectAmount);
                    break;
                case "Fear Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(1, EffectAmount);
                    break;
                case "Swift Potion":
                    Card.DrawCards(rng,hero, EffectAmount);
                    break;
                case "Weak Potion":
                    target = hero.DetermineTarget(encounter);
                    encounter[target].AddBuff(2, EffectAmount);
                    break;
                case "Focus Potion":
                    hero.AddBuff(7, EffectAmount);
                    break;
                case "Cultist Potion":
                    hero.AddBuff(3, EffectAmount);
                    break;
                case "Attack Potion":
                    Card attackPotion = new Card(Card.ChooseCard(Card.RandomCards(hero.Name, 3, rng, "Attack"),"add to your hand"));
                    attackPotion.SetTmpEnergyCost(0);
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(attackPotion));
                        else hero.DiscardPile.Add(new Card(attackPotion));                       
                    }
                    break;
                case "Blessing of the Forge":
                    foreach (Card c in hero.Hand)
                        c.UpgradeCard();
                    break;
                case "Colorless Potion":
                    Card colorlessPotion = new Card(Card.ChooseCard(Card.RandomCards("Colorless", 3, rng), "add to your hand"));
                    colorlessPotion.SetTmpEnergyCost(0);
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(colorlessPotion));
                        else hero.DiscardPile.Add(new Card(colorlessPotion));
                    }
                    break;
                case "Dexterity Potion":
                    hero.AddBuff(9, EffectAmount);
                    break;
                case "Explosive Potion":
                    foreach (Enemy e in encounter)
                        hero.NonAttackDamage(e, EffectAmount);
                    break;
                case "Flex Potion":
                    hero.AddBuff(4, EffectAmount);
                    hero.AddBuff(30, EffectAmount);
                    break;
                case "Power Potion":
                    Card powerPotion = new Card(Card.ChooseCard(Card.RandomCards(hero.Name, 3, rng, "Power"), "add to your hand"));
                    powerPotion.SetTmpEnergyCost(0);
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(powerPotion));
                        else hero.DiscardPile.Add(new Card(powerPotion));
                    }
                    break;
                case "Skill Potion":
                    Card skillPotion = new Card(Card.ChooseCard(Card.RandomCards(hero.Name, 3, rng, "Skill"), "add to your hand"));
                    skillPotion.SetTmpEnergyCost(0);
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(skillPotion));
                        else hero.DiscardPile.Add(new Card(skillPotion));
                    }
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
                    encounter[target].AddBuff(39, EffectAmount);
                    break;
                case "Bottled Miracle":
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(Dict.cardL[336]));
                        else hero.DiscardPile.Add(new Card(Dict.cardL[336]));
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
                        distilledChaos.CardAction(hero,encounter,rng);
                        if (distilledChaos.GetDescription().Contains("Exhaust") || distilledChaos.Type == "Status" || distilledChaos.Type == "Curse")
                            distilledChaos.Exhaust(hero, hero.DrawPile);
                        else if (distilledChaos.Type == "Power")
                            hero.DrawPile.Remove(distilledChaos);
                        else if (Name == "Tantrum")
                            Card.Shuffle(hero.DrawPile, rng);
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
                    int gambleChoice = -1;
                    int gambleAmount = hero.Hand.Count;
                    int gamble = 0;
                    while (gambleChoice != 0 && gambleAmount > 0)
                    {
                        Console.WriteLine($"\nEnter the number of the card you would like to discard or hit 0 to move on.");
                        for (int i = 1; i <= gambleAmount; i++)
                            Console.WriteLine($"{i}:{hero.DrawPile[hero.Hand.Count - i].Name}");
                        while (!Int32.TryParse(Console.ReadLine(), out gambleChoice) || gambleChoice < 0 || gambleChoice > gambleAmount)
                            Console.WriteLine("Invalid input, enter again:");
                        if (gambleChoice > 0)
                        {
                            Card gambledCard = hero.Hand[hero.Hand.Count - gambleChoice];
                            gambledCard.MoveCard(hero.Hand, hero.DiscardPile);
                            gambleAmount--;
                            gamble++;
                        }
                    }
                    Card.DrawCards(rng, hero, gamble);
                    break;
                case "Liquid Memories":
                    for (int i = 0; i < EffectAmount; i++)
                    {
                        Card liquidMemories = Card.ChooseCard(hero.DiscardPile, "add to your hand");
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
                            Card exhaustedCard = hero.Hand[hero.Hand.Count - exhaustChoice];
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
                        hero.Potions.Add(new Potion(Dict.potionL[rng.Next(42)]));
                    break;
                case "Fairy in a Bottle":
                    hero.HealHP(Convert.ToInt32(hero.MaxHP * (EffectAmount / 100.0)));
                    break;
                case "Fruit Juice":
                    hero.SetMaxHP(EffectAmount);
                    break;
                case "Smoke Bomb":
                    // Code combat to end
                    break;
                case "Snecko Oil":
                    Card.DrawCards(rng, hero, EffectAmount);
                    foreach (Card c in hero.Hand)
                        c.SetEnergyCost(rng.Next(4));
                    break;
                case "Essence of Darkness":
                    for (int i = 0; i < hero.OrbSlots * EffectAmount; i++)
                        hero.ChannelOrb(encounter, 2);
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
                hero.HealHP(5);
        }


        public string GetDescription(Hero hero = null)
        {
            int sacredBark = 1;
            if (hero != null && hero.Relics.Find(x => x.Name == "Sacred Bark") != null)
                sacredBark++;
            switch(Name) 
            {
                default:  return "";
                case "Fire Potion": return $"Deal {EffectAmount * sacredBark} damage.";
                case "Energy Potion": return $"Gain {EffectAmount * sacredBark} Energy.";
                case "Strength Potion": return $"Gain {EffectAmount * sacredBark} Strength.";
                case "Block Potion": return $"Gain {EffectAmount * sacredBark} Block.";
                case "Fear Potion": return $"Apply {EffectAmount * sacredBark} Vulnerable.";
                case "Swift Potion": return $"Draw {EffectAmount * sacredBark} cards.";
                case "Weak Potion": return $"Apply {EffectAmount * sacredBark} Weak.";
                case "Focus Potion": return $"Gain {EffectAmount * sacredBark} Focus.";
                case "Liquid Bronze": return $"Gain {EffectAmount * sacredBark} Thorns.";
                case "Attack Potion": return $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Attack cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.";
                case "Blessing of the Forge": return $"Upgrade all cards in your hand for the rest of combat.";
                case "Colorless Potion": return $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Colorless cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.";
                case "Dexterity Potion": return $"Gain {EffectAmount * sacredBark} Dexterity.";
                case "Explosive Potion": return $"Deal {EffectAmount * sacredBark} damage to ALL enemies.";
                case "Flex Potion": return $"Gain {EffectAmount * sacredBark} Strength. At the end of your turn, lose {EffectAmount * sacredBark} Strength.";
                case "Power Potion": return $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Power cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.";
                case "Skill Potion": return $"Add {(sacredBark == 2 ? "2 copies" : "")} of 1 of 3 random Skill cards to your hand, {(sacredBark == 2 ? "they" : "it")} costs 0 this turn.";
                case "Speed Potion": return $"Gain {EffectAmount * sacredBark} Dexterity. At the end of your turn, lose {EffectAmount * sacredBark} Dexterity.";
                case "Blood Potion": return $"Heal for {EffectAmount * sacredBark}% of your Max HP.";
                case "Poison Potion": return $"Apply {EffectAmount * sacredBark} Poison.";
                case "Bottled Miracle": return $"Add {EffectAmount * sacredBark} Miracles to your hand.";
                case "Ancient Potion": return $"Gain {EffectAmount * sacredBark} Artifact.";
                case "Distilled Chaos": return $"Play the top {EffectAmount * sacredBark} cards of your draw pile.";
                case "Duplication Potion": return $"This turn, your {(sacredBark == 2 ? "next 2 cards are" : "next card is")} played twice.";
                case "Essence of Steel": return $"Gain {EffectAmount * sacredBark} Plated Armor.";
                case "Gambler's Brew": return $"Discard any number of cards, then draw that many.";
                case "Liquid Memories": return $"Choose {(sacredBark == 2 ? "2 cards" : "a card")} in your discard pile and return {(sacredBark == 2 ? "them" : "it")} to your hand. {(sacredBark == 2 ? "They cost" : "It costs")} 0 this turn.";
                case "Regen Potion": return $"Gain {EffectAmount * sacredBark} Regeneration.";
                case "Potion of Capcity": return $"Gain {EffectAmount * sacredBark} Orb slots.";
                case "Elixir": return $"Exhaust any number of cards in your hand.";
                case "Cunning Potion": return $"Add {EffectAmount * sacredBark} Upgraded Shivs to your hand.";
                case "Stance Potion": return $"Enter Calm or Wrath.";
                case "Cultist Potion": return $"Gain {EffectAmount * sacredBark} Ritual.";
                case "Entropic Brew": return $"Fill all your empty potion slots with random potions.";
                case "Fairy in a Bottle": return $"When you would die, heal to {EffectAmount * sacredBark} of your Max HP instead and discard this potion.";
                case "Fruit Juice": return $"Gain {EffectAmount * sacredBark} Max HP.";
                case "Smoke Bomb": return $"Escape from a non-boss combat. Receive no rewards.";
                case "Snecko Oil": return $"Draw {EffectAmount * sacredBark} cards. Randomize the costs of all cards in your hand for the rest of the combat.";
                case "Essence of Darkness": return $"Channel {EffectAmount * sacredBark} Dark for each orb slot.";
                case "Heart of Iron": return $"Gain {EffectAmount * sacredBark} Metallicize.";
                case "Ghost in a Jar": return $"Gain {EffectAmount * sacredBark} Intangible.";
                case "Ambrosia": return $"Enter Divinity Stance.";
            }
        }
    }
}