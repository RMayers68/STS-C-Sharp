using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace STV
{
	public class Card: IEquatable<Card> , IComparable<Card>
	{

		// attributes
		private int FuncID { get; set; } // ID correlates to method ran (Name without spaces)
		public string Name { get; set; }
		public string Type { get; set; } // Attack, Skill, Power, Status or Curse
		public string Rarity { get; set; } //Common,Uncommon,Rare 
		public string EnergyCost { get; set; } // currently string because of X and Unplayable Energy Cost cards
		public string Description { get; set; }



		//constructors
		public Card()
        {
			this.FuncID = -1;
			this.Name = "";
			this.Type = "";
			this.Rarity = "";
			this.EnergyCost = "";
			this.Description = "";
        }
		public Card(int funcID, string name, string type, string rarity, string energyCost, string description)
		{
			this.FuncID = funcID;
			this.Name = name;
			this.Type = type;
			this.Rarity = rarity;
			this.EnergyCost = energyCost;
			this.Description = description;
		}

		public Card(Card card)
        {
				this.FuncID = FuncID;
				this.Name = card.Name;
				this.Type = card.Type;
				this.Rarity = card.Rarity;
				this.EnergyCost = card.EnergyCost;
				this.Description = card.Description;
		}
		//comparators and equals
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			Card objAsPart = obj as Card;
			if (objAsPart == null) return false;
			else return Equals(objAsPart);
		}
		public int SortByNameAscending(string name1, string name2)
		{

			return name1.CompareTo(name2);
		}

		// Default comparer for Card type.
		public int CompareTo(Card compareCard)
		{
			// A null value means that this object is greater.
			if (compareCard == null)
				return 1;

			else
				return this.FuncID.CompareTo(compareCard.FuncID);
		}
		public override int GetHashCode()
		{
			return FuncID;
		}
		public bool Equals(Card other)
		{
			if (other == null) return false;
			return (this.FuncID.Equals(other.FuncID));
		}
		// Should also override == and != operators.

		public int SortbyNameAscending(Card card1, Card card2)
		{
			return card1.Name.CompareTo(card2.Name);
		}

		// methods
		public string String()
		{
			if (EnergyCost == "None")
				return $"Name: {Name}\nType: {Type}\nEffect: {Description}";
            return $"Name: {Name}\nEnergy Cost: {EnergyCost}\tType: {Type}\nEffect: {Description}";																	
		}
		
        public void CardAction(Hero hero, List<Enemy> encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)
		{
			int target = 0;
			int damage = 0;
			switch (Name)
			{
																// IRONCLAD CARDS (0 - 72)																					
                case "Anger":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					discardPile.Add(new Card(this));
					break;
				case "Armaments":
					hero.GainBlock(5);
					//Upgrade card
					break;
				case "Barricade":
					hero.AddBuff(20, 1);
					break;				
				case "Bash":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 8);
					encounter[target].AddBuff(1, 2);
					break;
                case "Battle Trance":
                    STS.DrawCards(drawPile, discardPile, hand, rng, 3);
                    hero.AddBuff(21, 1);
                    break;
				case "Berserk":
					hero.AddBuff(1, 2);
					hero.GainBattleEnergy(1);
					break;
                case "Bloodletting":
					hero.GainTurnEnergy(2);
					hero.SelfDamage(3);
					break;
				case "Bludgeon":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 32);
					break;
				case "Body Slam":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], hero.Block);
					break;
				case "Brutality":
					hero.AddBuff(23,1);
					break;
				case "Burning Pact":
					Card burningPact = STS.ChooseCard(hand);
					burningPact.Exhaust(exhaustPile,hand);
                    STS.DrawCards(drawPile, discardPile, hand, rng, 3);
					break;
                case "Carnage":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					break;
                case "Clash":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 14);
                    break;
                case "Cleave":
					hero.AttackAll(encounter, 8);
					break;
				case "Clothesline":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 12);
					encounter[target].AddBuff(2,2);
					break;
				case "Combust":
					hero.AddBuff(24,1);
					break;
				case "Corruption":
					hero.AddBuff(25,1);
					break;
				case "Dark Embrace":
					hero.AddBuff(26,1);
					break;
				case "Demon Form":
					hero.AddBuff(3, 2);
					break;
				case "Disarm":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(4, -2);
					break;
				case "Double Tap":
					hero.AddBuff(27,1);
					break;
				case "Dropkick":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 5);
					if (encounter[target].Buffs.Find(x => x.BuffID.Equals(1)) != null)
						hero.GainTurnEnergy(1);
					break;
				case "Dual Wield":
					Card dualWield = new Card();
					do
						dualWield = STS.ChooseCard(hand);
					while (dualWield.Type == "Skill");
					hand.Add(dualWield);
					break;
				case "Entrench":
					hero.Block *= 2;
					break;
				case "Evolve":
					hero.AddBuff(28,1);
					break;
				case "Exhume":
					Card exhume = STS.ChooseCard(exhaustPile);
					exhaustPile.Remove(exhume);
					hand.Add(exhume);
					break;
				case "Feed":												//minion buff add
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					if (encounter[target].Hp <= 0)
                    {
						hero.HealHP(3);
						hero.MaxHP += 3;
                    }
					break;
				case "Feel No Pain":
					hero.AddBuff(29,3);
					break;
				case "Fiend Fire":											
					target = hero.DetermineTarget(encounter);
					damage = 0;
					for (int i = hand.Count; i > 1; i--)
                    {
						hand[i].Exhaust(exhaustPile,hand);
						damage += 7;
                    }
					hero.SingleAttack(encounter[target], damage);
					break;
				case "Flex":									
					hero.AddBuff(4, 2);
					hero.AddBuff(30, 2);
					break;
				case "Ghostly Armor":
					hero.Block += 10;
					break;
				case "Havoc":
					drawPile.Last();
                    drawPile.Last().CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                    drawPile.Last().Exhaust(exhaustPile,drawPile);
					break;
				case "Headbutt":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 9);
					Card headbutt = STS.ChooseCard(discardPile);
					discardPile.Remove(headbutt);
					drawPile.Add(headbutt);
                    break;
                case "Heavy Blade":
					target = hero.DetermineTarget(encounter);
					Buff heavyBlade = Actor.FindBuff("Strength", hero.Buffs);
					if (heavyBlade != null)
						damage += 14 + (heavyBlade.Intensity.GetValueOrDefault() * 3);
					hero.SingleAttack(encounter[target], damage);
					break;
				case "Hemokinesis":
					hero.SelfDamage(3);
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 14);
					break;
				case "Immolate":
					hero.AttackAll(encounter, 21);
					discardPile.Add(new Card(Dict.cardL[358]));
					break;
				case "Impervious":
					hero.CardBlock(30);
					break;
				case "Infernal Blade":
                    Card infernalBlade = new Card();
                    while (infernalBlade.Type != "Attack")
                        infernalBlade = Dict.cardL[rng.Next(0, 73)];
					hand.Add(infernalBlade);
					infernalBlade.EnergyCost = "0";
					break;
                case "Inflame":
					hero.AddBuff(4, 2);
					break;
				case "Intimidate":
					foreach (var enemy in encounter)
						enemy.AddBuff(2, 1);
					break;
				case "Iron Wave":
					target = hero.DetermineTarget(encounter);
					hero.CardBlock(5);
					hero.SingleAttack(encounter[target], 5);
					break;
				case "Juggernaut":
					hero.AddBuff(31, 5);
					break;
				case "Limit Break":
					Buff limitBreak = Actor.FindBuff("Strength", hero.Buffs);
					if (limitBreak != null)
						limitBreak.Intensity *= 2;
					break;
				case "Metallicize":
					hero.AddBuff(32, 3);
					break;
				case "Offering":
					hero.SelfDamage(6);
					hero.GainTurnEnergy(2);
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
					break;
				case "Perfected Strike":
					damage = 6;
					foreach (Card c in hand)
					{
						if (c.Name.Contains("Strike"))
							damage += 2;
					}
					foreach (Card c in drawPile)
					{
                        if (c.Name.Contains("Strike"))
                            damage += 2;
                    }
                    foreach (Card c in discardPile)
                    {
                        if (c.Name.Contains("Strike"))
                            damage += 2;
                    }
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], damage);
					break;
                case "Pommel Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 9);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Power Through":
					for (int i = 0; i < 2; i++)
                    {
						if (hand.Count < 10)
							hand.Add(new Card(Dict.cardL[357]));
						else discardPile.Add(new Card(Dict.cardL[357]));
					}
					hero.CardBlock(15);
					break;
				case "Pummel":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < 4; i++)
						hero.SingleAttack(encounter[target], 2);
					break;
				case "Rage":
					hero.AddBuff(33, 3);
					break;
				case "Rampage":                                    
					damage = 8;
					foreach (string s in hero.Actions)
						if (s.Contains("Rampage"))
							damage += 5;
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], damage);
					break;
				case "Reaper":
					int enemyHPBefore = 0;
					foreach(Enemy e in encounter)
					{
						enemyHPBefore += e.Hp;
					}
					hero.AttackAll(encounter, 4);
					int enemyHPAfter = 0;
                    foreach (Enemy e in encounter)
                    {
                        enemyHPAfter += e.Hp;
                    }
					hero.HealHP(enemyHPBefore - enemyHPAfter);
					break;
                case "Reckless Charge":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					discardPile.Add(new Card(Dict.cardL[356]));
					break;
				case "Rupture":
					hero.AddBuff(34, 1);
					break;
				case "Searing Blow":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 12);
					break;
				case "Second Wind":
					foreach( Card c in hand)
					{
						if (c.Type != "Attack")
						{
							c.Exhaust(exhaustPile);
							hand.Remove(c);
							hero.GainBlock(5);
						}
					}
					break;	
                case "Sentinel":
					hero.CardBlock(5);
					break;
				case "Seeing Red":
					hero.GainTurnEnergy(2);
					break;
				case "Sever Soul":
                    foreach (Card c in hand)
                        if (c.Type != "Attack")
                            c.Exhaust(exhaustPile,hand);                          
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 16);
                    break;
                case "Shockwave":
					foreach (var enemy in encounter)
                    {
						enemy.AddBuff(2, 3);
						enemy.AddBuff(1, 3);
					}
					break;
				case "Shrug It Off":
					hero.CardBlock(8);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Spot Weakness":
					target = hero.DetermineTarget(encounter);
					if (Enemy.AttackIntents().Contains(encounter[target].Intent))
						hero.AddBuff(4, 3);
					break;
                case "Sword Boomerang":
					for (int i = 0; i < 3; i++)
                    {
						target = rng.Next(0, encounter.Count);
						hero.SingleAttack(encounter[target], 3);
					}					
					break;
				case "Thunderclap":
					hero.AttackAll(encounter, 4);
					foreach (var enemy in encounter)
						enemy.AddBuff(1, 1);
					break;
				case "Twin Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 5);
					hero.SingleAttack(encounter[target], 5);
					break;
				case "Uppercut":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 13);
					encounter[target].AddBuff(1, 1);
					encounter[target].AddBuff(2, 1);
					break;
				case "Warcry":
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					Card warcry = STS.ChooseCard(hand);
					hand.Remove(warcry);
					drawPile.Add(warcry);
					break;
				case "Whirlwind":
					for(int i = 0;i < Int32.Parse(EnergyCost);i++)
						hero.AttackAll(encounter, 5);
					break;
				case "Wild Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 12);
					drawPile.Add(new Card(Dict.cardL[357]));
					STS.Shuffle(drawPile,rng);
					break;

																// SILENT CARDS (73 - 145)
                case "A Thousand Cuts":
					hero.AddBuff(35, 1);
					break;
				case "Accuracy":
					hero.AddBuff(36, 3);
					break;
				case "Acrobatics":
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
					if (hand.Count > 1)
						Discard(hero,hand, discardPile,STS.ChooseCard(hand));
					break;
				case "Adrenaline":
					hero.GainTurnEnergy(1);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "After Image":
					hero.AddBuff(37, 1);
					break;
				case "Alchemize":
					hero.Potions.Add(Dict.potionL[rng.Next(0,Dict.potionL.Count)]);
					break;
				case "All-Out Attack":                                                                          
					hero.AttackAll(encounter, 10);
					Discard(hero,hand, discardPile, hand[rng.Next(0, hand.Count)]);
					break;
				case "Backflip":
					hero.CardBlock(5);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "Backstab":								//Innate												
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 11);
					break;
                case "Blade Dance":
					for (int i = 0; i < 3; i++)
					{
						if (hand.Count < 10)
							hand.Add(new Card(Dict.cardL[317]));
						else discardPile.Add(new Card(Dict.cardL[317]));
					}
					break;
				case "Blur":
					hero.GainBlock(5);
					hero.AddBuff(38, 1);
					break;
				case "Bouncing Flask":
                    for (int i = 0; i < 3; i++)
                    {
                        target = rng.Next(0, encounter.Count);
						encounter[target].AddBuff(39, 3);
                    }
					break;
				case "Bullet Time":
					foreach (Card c in hand)
						c.EnergyCost = "0";
					hero.AddBuff(21, 1);
					break;
				case "Burst":
					hero.AddBuff(40, 1);
					break;
                case "Calculated Gamble":
					int gamble = 0;
					for (gamble = 0;gamble < hand.Count; gamble++)
                    {
						Discard(hero,hand, discardPile,hand[hand.Count-1]);
                    }
					STS.DrawCards(drawPile, hand, discardPile, rng, gamble);
					break;
				case "Caltrops":
					hero.AddBuff(41, 3);
					break;
				case "Catalyst":
                    target = hero.DetermineTarget(encounter);
					Buff catalyst = Actor.FindBuff("Poison", encounter[target].Buffs);
					if (catalyst != null)
						catalyst.Intensity *= 2;
					break;
				case "Choke":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 12);
					encounter[target].AddBuff(42, 3);
					break;               
				case "Cloak and Dagger":
					hero.CardBlock(6);
					if (hand.Count < 10)
						hand.Add(new Card(Dict.cardL[317]));
					else discardPile.Add(new Card(Dict.cardL[317]));
					break;
                case "Concentrate":
                    for (int i = 0; i < 3; i++)
                        Discard(hero,hand, discardPile, STS.ChooseCard(hand));
                    hero.GainTurnEnergy(2);
                    break;
				case "Corpse Explosion":
                    target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(38, 6);
					encounter[target].AddBuff(43, 1);
					break;
				case "Crippling Cloud":
					foreach(Enemy enemy in encounter)
					{
						enemy.AddBuff(38, 4);
						enemy.AddBuff(2, 2);
					}
					break;
                case "Dagger Spray":
					hero.AttackAll(encounter, 4);
					hero.AttackAll(encounter, 4);
					break;
				case "Dagger Throw":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 9);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					Discard(hero,hand, discardPile, STS.ChooseCard(hand));
					break;
				case "Dash":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					hero.CardBlock(10);
					break;
				case "Deadly Poison":
                    target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(38, 5);
					break;
                case "Deflect":
					hero.CardBlock(4);
					break;
				case "Die Die Die":
					hero.AttackAll(encounter, 13);
					break;
				case "Distraction":
                    Card distraction = new Card();
                    while (distraction.Type != "Skill")
                        distraction = Dict.cardL[rng.Next(73, 146)];
					distraction.EnergyCost = "0";
                    hand.Add(distraction);
                    break;
				case "Dodge and Roll":
					hero.AddBuff(44, 4);
					hero.CardBlock(4);
					break;
				case "Doppelganger":
					hero.AddBuff(22, hero.Energy);
					hero.AddBuff(45, hero.Energy);
					break;
                case "Endless Agony":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Envenom":
					hero.AddBuff(46, 1);
					break;
				case "Escape Plan":
					STS.DrawCards(drawPile, hand,discardPile, rng, 1);
					if (hand[hand.Count - 1].Type == "Skill")
						hero.CardBlock(3);
					break;
				case "Eviscerate":
                    target = hero.DetermineTarget(encounter);
					for (int i = 0; i < 3; i++)
						hero.SingleAttack(encounter[target], 6);
                    break;
                case "Expertise":
					int below6 = 6 - hand.Count;
					if (below6 > 0)
					STS.DrawCards(drawPile, hand, discardPile, rng, below6);
					break;
				case "Finisher":
					int finisher = 0;
					foreach (string s in hero.Actions)
						if (s.Contains("Attack"))
							finisher++;
                    target = hero.DetermineTarget(encounter);
                    for (int i = 0; i < finisher; i++)
                        hero.SingleAttack(encounter[target], 6);
					break;
                case "Flechettes":
					int flechettes = 0;
					for (int i = 0; i < hand.Count; i++)
					{
						if (hand[i].Type == "Skill")
							flechettes++;
					}
					target = hero.DetermineTarget(encounter);
					for (int i = flechettes; i > 0; i--)
						hero.SingleAttack(encounter[target], 4);
					break;
				case "Flying Knee":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 8);
					hero.AddBuff(22, 1);
					break;
				case "Footwork":
					hero.AddBuff(9, 2);
					break;
				case "Glass Knife":
					damage = 8;
					foreach (string s in hero.Actions)
						if (s.Contains(Name))
							damage -= 2;
                    target = hero.DetermineTarget(encounter);
					for (int i = 0; i <= 2; i++)
						hero.SingleAttack(encounter[target], damage);
					break;
				case "Grand Finale":
					hero.AttackAll(encounter, 40);
					break;
                case "Heel Hook":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 5);
					if (Actor.FindBuff("Weak",encounter[target].Buffs) != null)
						hero.GainTurnEnergy(1);
					break;
				case "Infinite Blades":
					hero.AddBuff(47,1);
					break;
				case "Leg Sweep":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(2, 1);
					hero.CardBlock(11);
					break;
				case "Malaise":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(4, hero.Energy);
					encounter[target].AddBuff(2, hero.Energy);
					break;
				case "Masterful Stab":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 12);
					break;
                case "Neutralize":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);
					encounter[target].AddBuff(2, 1);
					break;
				case "Nightmare":
					Card nightmare = STS.ChooseCard(hand);
					for(int i = 0;i < 3; i++)
						drawPile.Add(new Card(nightmare));
					break;
				case "Noxious Fumes":
					hero.AddBuff(48, 2);
					break;
				case "Outmaneuver":
					hero.AddBuff(22, 2);
					break;
				case "Phantasmal Killer":
					hero.AddBuff(49, 1);
					break;
				case "Piercing Wail":                                            
					for (int i = 0; i < encounter.Count; i++)
					{
						encounter[i].AddBuff(4, -6);
						encounter[i].AddBuff(30, -6);
					}
					break;
				case "Poisoned Stab":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 5);
					encounter[target].AddBuff(38, 3);
					break;
				case "Predator":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 15);
                    hero.AddBuff(45, 2);
					break;
                case "Prepared":
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					if (hand.Count > 1)
					Discard(hero,hand, discardPile, STS.ChooseCard(hand));
					break;
				case "Quick Slash":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 8);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Riddle with Holes":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < 5; i++)
						hero.SingleAttack(encounter[target], 3);
					break;
				case "Setup":
					Card setup = STS.ChooseCard(hand);
					drawPile.Add(setup);
					hand.Remove(setup);
					setup.EnergyCost = "0";
					break;
				case "Skewer":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < hero.Energy; i++)
						hero.SingleAttack(encounter[target], 7);
					break;
				case "Slice":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					break;
                case "Sneaky Strike":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 12);
                    if (hero.Actions.Contains("Discard"))
                        hero.Energy += 2;
                    break;
                case "Storm of Steel":
					int storm = 0;
					for (storm = 0; storm < hand.Count; storm++)
					{
						Discard(hero,hand, discardPile,hand[hand.Count-1]);
					}
					for (int i = storm; i > 0; i--)
						hand.Add(Dict.cardL[317]);
					break;				
                case "Sucker Punch":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					encounter[target].AddBuff(2, 1);
					break;
				case "Survivor":
					hero.CardBlock(8);
					Discard(hero,hand, discardPile, STS.ChooseCard(hand));
					break;
				case "Terror":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(1, 99);
					break;
				case "Tools of the Trade":
					hero.AddBuff(45,1);
					hero.AddBuff(50,1);
					break;
				case "Unload":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 14);
					for (int i = hand.Count; i > 0; i--)
					{
						if (hand[i-1].Type == "Attack")
							Discard(hero,hand,discardPile,hand[i-1]);
					}	
					break;
				case "Well-Laid Plans":
					hero.AddBuff(51, 1);
					break;
				case "Wraith Form":
					hero.AddBuff(52, 2);
					hero.AddBuff(53, 1);
					break;

															// DEFECT CARDS (146-218)
                case "Aggregate":                                                                   
                    hero.GainTurnEnergy(drawPile.Count / 4);
					break;
				case "All For One":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					foreach(Card zeroCost in discardPile)
                    {
						if (zeroCost.EnergyCost == "0" && hand.Count < 10)
                        {
							hand.Add(zeroCost);
							discardPile.Remove(zeroCost);
                        }
                    }
					break;
				case "Amplify":
					hero.AddBuff(54,1);
					break;
				case "Auto-Shields":
					if (hero.Block == 0)
						hero.CardBlock(11);
					break;
				case "Ball Lightning":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					hero.ChannelOrb(encounter, 0);
					break;
				case "Barrage":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < hero.Orbs.Count; i++)
						hero.SingleAttack(encounter[target], 4);
					break;
				case "Beam Cell":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);
					encounter[target].AddBuff(1, 1);
					break;
				case "Biased Cognition":
					hero.AddBuff(7,4);
					hero.AddBuff(55, 1);
					break;
				case "Blizzard":
					foreach (string s in hero.Actions)
						if (s.Contains("Channel Frost"))
							damage += 2;
                    hero.AttackAll(encounter,damage);
					break;
				case "Boot Sequence":
					hero.CardBlock(10);
					break;
				case "Buffer":
					hero.AddBuff(56, 1);
					break;
				case "Bullseye":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 8);
					encounter[target].AddBuff(57, 2);
					break;
                case "Capacitor":
					hero.OrbSlots += 2;
					break;
				case "Chaos":
					hero.ChannelOrb(encounter, rng.Next(0,4));
					break;
				case "Charge Battery":
					hero.CardBlock(7);
					hero.AddBuff(22,1);
					break;																											
				case "Chill":
					foreach(var e in encounter)
						hero.ChannelOrb(encounter, 1);
					break;
				case "Claw":
					foreach (string s in hero.Actions)
						if (s.Contains("Claw"))
							damage += 2;
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], damage + 3);																			
					break;
				case "Cold Snap":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					hero.ChannelOrb(encounter, 1);
					break;
				case "Compile Driver":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					STS.DrawCards(drawPile,hand,discardPile,rng,hero.Orbs.Distinct().Count());
					break;
				case "Consume":
					hero.AddBuff(7, 2);
					hero.OrbSlots -= 1;
					while (hero.OrbSlots > hero.Orbs.Count)
						hero.Orbs.RemoveAt(hero.Orbs.Count-1);
					break;
				case "Coolheaded":
					hero.ChannelOrb(encounter, 1);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Core Surge":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 11);
					hero.AddBuff(8, 1);
					break;
				case "Creative AI":
					hero.AddBuff(58,1);
					break;
				case "Darkness":
					hero.ChannelOrb(encounter,2);
					break;
				case "Defragment":
					hero.AddBuff(7,1);
					break;
				case "Doom and Gloom":
					hero.AttackAll(encounter, 10);
					hero.ChannelOrb(encounter, 2);
					break;
				case "Double Energy":
					hero.GainTurnEnergy(hero.Energy*2);
					break;
				case "Dualcast":
					hero.Evoke(encounter);
					hero.Evoke(encounter);
					hero.Orbs.RemoveAt(0);
					break;
				case "Echo Form":
					hero.AddBuff(59, 1);
					break;
				case "Electrodynamics":
					hero.AddBuff(68, 1);
					for (int i = 0; i < 2; i++)
						hero.ChannelOrb(encounter, 0);
					break;
				case "Equilibrium":
					hero.CardBlock(13);
					hero.AddBuff(69, 1);
					break;
				case "Fission":
					int fission = 0;
					foreach(var Orb in hero.Orbs)
                    {
						hero.Orbs.Remove(Orb);
						fission++;
					}
					hero.GainTurnEnergy(fission);
					STS.DrawCards(drawPile, hand, discardPile, rng, fission);
					break;
				case "Force Field":
					hero.CardBlock(12);
					break;
				case "FTL":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 5);
					if (hero.Actions.Count < 3)
					{
						STS.DrawCards(drawPile,hand, discardPile, rng, 1);
					}
					break;
                case "Fusion":
					hero.ChannelOrb(encounter, 3);
					break;
				case "Genetic Algorithm":                               // This card requires updates to card values to function properly
					hero.CardBlock(1);
					break;
				case "Glacier":
					hero.CardBlock(10);
					hero.ChannelOrb(encounter, 2);
					hero.ChannelOrb(encounter, 2);
					break;
				case "Go for the Eyes":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 3);
                    if (Enemy.AttackIntents().Contains(encounter[target].Intent))
						encounter[target].AddBuff(2, 1);
					break;
				case "Heatsinks":
					hero.AddBuff(60, 1);
					break;
				case "Hello World":
					hero.AddBuff(61,1);
					break;
                case "Hologram":
					hero.CardBlock(3);
					Card hologram = STS.ChooseCard(discardPile);
					hand.Add(hologram);
					discardPile.Remove(hologram);
					break;
				case "Hyperbeam":
					hero.AttackAll(encounter, 26);
					hero.AddBuff(7, -3);
					break;
				case "Leap":
					hero.CardBlock(9);
					break;
				case "Loop":
					hero.AddBuff(62,1);
					break;
				case "Machine Learning":
					hero.AddBuff(63,1);
					break;
				case "Melter":
					target = hero.DetermineTarget(encounter);
					encounter[target].Block = 0;
					hero.SingleAttack(encounter[target], 10);
					break;
				case "Meteor Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 24);
					for (int i = 0; i < 3; i++)
						hero.ChannelOrb(encounter,3);
					break;
				case "Multi-Cast":
					for (int i = 0; i < Int32.Parse(EnergyCost); i++)
						hero.Evoke(encounter);
					hero.Orbs.RemoveAt(0);
					break;
				case "Overclock":
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					discardPile.Add(new Card(Dict.cardL[355]));
					break;
				case "Rainbow":
					for (int i = 0;i<3;i++)
						hero.ChannelOrb(encounter,i);
					break;
				case "Reboot":
					foreach(Card c in discardPile)
                    {
						discardPile.Remove(c);
						drawPile.Add(c);
                    }
					foreach(Card c in hand)
                    {
						hand.Remove(c);
						drawPile.Add(c);
                    }
					STS.Shuffle(drawPile, rng);
					STS.DrawCards(drawPile, hand, discardPile, rng, 4);
					break;
				case "Rebound":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 8);
					hero.AddBuff(64, 1);
					break;
                case "Recursion":
					hero.Evoke(encounter);
					int tmp = hero.Orbs[0].OrbID;
					hero.Orbs.RemoveAt(0);
					hero.ChannelOrb(encounter,tmp);
					break;
				case "Recycle":
					Card recycle = STS.ChooseCard(hand);
					recycle.Exhaust(exhaustPile, hand);
					hero.Energy += Int32.Parse(recycle.EnergyCost);
					break;
				case "Reinforced Body":
					for(int i = 0;i<Int32.Parse(EnergyCost); i++)
						hero.CardBlock(7);
					break;
				case "Reprogram":
					hero.AddBuff(7, -1);
					hero.AddBuff(4, 1);
					hero.AddBuff(9, 1);
					break;
				case "Rip And Tear":
					for (int i = 0; i < 2; i++)
					{
						target = rng.Next(0, encounter.Count);
						hero.SingleAttack(encounter[target], 7);
					}
					break;
				case "Scrape":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					for (int i = 0;i< 4; i++)
                    {
						STS.DrawCards(drawPile, hand, discardPile, rng, 1);
						if (hand[hand.Count-1].EnergyCost != "0")
							Discard(hero, hand, discardPile, hand[hand.Count - 1]);
					}
					break;
				case "Seek":					
					hand.Add(STS.ChooseCard(drawPile));
					break;
				case "Self Repair":
					hero.AddBuff(65,7); 
					break;
				case "Skim":
					STS.DrawCards(drawPile,hand,discardPile, rng, 3);
					break;
				case "Stack":
					hero.CardBlock(discardPile.Count);
					break;
				case "Static Discharge":
					hero.AddBuff(66, 1);
					break;
                case "Steam Barrier":
					foreach (string s in hero.Actions)
						if (s.Contains("Steam"))
							damage += 2;
					if (damage >= 6)
						break;
					hero.CardBlock(6 - damage);
                    break;
				case "Storm":
					hero.AddBuff(67, 1);
					break;
                case "Streamline":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 15);
					if (EnergyCost != "0")
						EnergyCost = $"{Int32.Parse(EnergyCost)-1}";
					break;
				case "Sunder":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 24);
					if (encounter[target].Hp <= 0)
						hero.GainTurnEnergy(3);
					break;
				case "Sweeping Beam":
					hero.AttackAll(encounter, 6);
					STS.DrawCards(drawPile,hand,discardPile,rng, 1);
					break;
				case "Tempest":
					for (int i = 0; i < Int32.Parse(EnergyCost); i++)
						hero.ChannelOrb(encounter,0);
					break;
				case "Thunder Strike":
                    foreach (string s in hero.Actions)
                        if (s.Contains("Channel Lightning"))
                            damage++;
                    for (int i = 0; i < damage; i++)
                    {
                        target = rng.Next(0, encounter.Count);
                        hero.SingleAttack(encounter[target], 7);
                    }
                    break;
                case "TURBO":
					hero.GainTurnEnergy(2);
					discardPile.Add(Dict.cardL[359]);
					break;
				case "White Noise":
                    Card whiteNoise = new Card();
                    while (whiteNoise.Type != "Power")
                        whiteNoise = Dict.cardL[rng.Next(146, 219)];
                    whiteNoise.EnergyCost = "0";
                    hand.Add(whiteNoise);
                    break;
                case "Zap":
					hero.ChannelOrb(encounter, 0);
					break;

															// STRIKE AND DEFEND (219,220)
                case "Defend":
                    hero.CardBlock(5);
                    break;
                case "Strike":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 6);
                    break;

															// WATCHER CARDS (221 - 293, plus 219: Strike)					
                case "Alpha":                                                   
                    drawPile.Add(new Card(Dict.cardL[334]));
					STS.Shuffle(drawPile,rng);
					break;
				case "Battle Hymn":
					hero.AddBuff(70, 1);
					break;
				case "Blasphemy":
					hero.SwitchStance("Divinity", discardPile, hand);
					hero.AddBuff(71, 1);
					break;
				case "Bowling Bash":
					target = hero.DetermineTarget(encounter);
					foreach(Actor a in encounter)
						hero.SingleAttack(encounter[target], 7);
					break;
                case "Brilliance":
					foreach (string s in hero.Actions)
						if (s.Contains("Mantra"))
							damage += Int32.Parse(s.Last().ToString());
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], damage+10);
                    break;
                case "Carve Reality":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					hand.Add(new Card(Dict.cardL[339]));
					break;
				case "Collect":
					hero.AddBuff(72, hero.Energy);
					break;
				case "Conclude":										//End turn needed
					hero.AttackAll(encounter,12);
					break;
				case "Conjure Blade":
					drawPile.Add(new Card(Dict.cardL[360]));
					drawPile[drawPile.Count - 1].Description = Regex.Replace(drawPile[drawPile.Count - 1].Description, "X", $"{EnergyCost}");
					break;
				case "Consecrate":
					hero.AttackAll(encounter, 5);
					break;
				case "Crescendo":
					hero.SwitchStance("Wrath",discardPile,hand);
					break;
				case "Crush Joints":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 8);
					string crushJoints = "";
					for (int i = hero.Actions.Count - 1; i >= 0; i--)
						if (hero.Actions[i].Contains("Played"))
						{
                            crushJoints = hero.Actions[i];
							break;
                        }						
					if (crushJoints.Contains("Skill"))
						encounter[target].AddBuff(1, 1);
					break;
				case "Cut Through Fate":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 8);
					Scry(drawPile, discardPile,hand, 2);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
                case "Deceive Reality":
					hero.CardBlock(4);
					hand.Add(new Card(Dict.cardL[338]));
					break;
				case "Deus Ex Machina":
					for (int i = 0; i < 2; i++)
                    {
						if (hand.Count < 10)
							hand.Add(new Card(Dict.cardL[336]));
						else discardPile.Add(new Card(Dict.cardL[336]));
					}
					Exhaust(exhaustPile,hand);
					break;
				case "Deva Form":
					hero.AddBuff(73, 1);
					break;
				case "Devotion":
					hero.AddBuff(74, 2);
					break;
				case "Empty Body":
					hero.CardBlock(7);
					hero.SwitchStance("None", discardPile, hand);
					break;
				case "Empty Fist":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 9);
					hero.SwitchStance("None", discardPile, hand);
					break;
				case "Empty Mind":
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					hero.SwitchStance("None", discardPile, hand);
					break;
				case "Eruption":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 9);
					hero.SwitchStance("Wrath", discardPile, hand);
					break;
				case "Establishment":
					hero.AddBuff(75, 1);
					break;
				case "Evaluate":
					hero.CardBlock(6);
					drawPile.Add(new Card(Dict.cardL[335]));
					STS.Shuffle(drawPile, rng);
					break;
				case "Fasting":													
					hero.AddBuff(4, 3);
					hero.AddBuff(9,3);
					hero.MaxEnergy = -1;
					break;
				case "Fear No Evil":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 8);
					List<string> list = new List<string>(Enemy.AttackIntents());
					if (list.Contains(encounter[target].Intent))
						hero.SwitchStance("Calm", discardPile, hand);
					break;
				case "Flurry Of Blows":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Flying Sleeves":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Follow-Up":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 7);
                    string followUp = "";
                    for (int i = hero.Actions.Count - 1; i >= 0; i--)
                        if (hero.Actions[i].Contains("Played"))
						{
                            followUp = hero.Actions[i];
							break;
                        }                          
					if (followUp.Contains("Attack"))
						hero.Energy += 1;
                    break;
                case "Foreign Influence":
					Card fore,ign,infl = new Card();
					List<Card> fi = new(4);
					for (int i = 0; i < 3; i++)
                    {
						Card uence = new Card();
						while (uence.Type != "Attack")
							uence = Dict.cardL[rng.Next(0, 291)];
						switch(i)
                        {
							case 0: fore = uence; fi.Add(fore); break;
							case 1: ign = uence; fi.Add(ign); break;
							case 2: infl = uence; fi.Add(infl); break;
                        }
                    }
					hand.Add(new(STS.ChooseCard(fi)));
					break;
				case "Foresight":
					hero.AddBuff(76, 3);
					break;
				case "Halt":
					hero.CardBlock(3);
					if (hero.Stance == "Wrath")
						hero.CardBlock(9);
					break;
				case "Indignation":
					if (hero.Stance == "Wrath")
						foreach (Actor a in encounter)
							a.AddBuff(1, 3);
					else hero.SwitchStance("Wrath", discardPile, hand);
					break;
				case "Inner Peace":
					if (hero.Stance == "Calm")
						STS.DrawCards(drawPile, hand, discardPile, rng, 3);
					else hero.SwitchStance("Calm", discardPile, hand);
                    break;
                case "Judgment":
					target = hero.DetermineTarget(encounter);
					if (encounter[target].Hp <= 30)
                    {
						encounter[target].Hp = 0;
						Console.WriteLine($"The {encounter[target].Name} has been judged!");
                    }
					break;
				case "Just Lucky":
					Scry(drawPile,discardPile,hand, 1);
					hero.CardBlock(2);
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);
					break;
				case "Lesson Learned":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 10);
                    if (encounter[target].Hp <= 0)
                    {
                        // Upgrade card permanently
                    }
					break;
				case "Like Water":
					hero.AddBuff(77, 5);
					break;
				case "Master Reality":
					hero.AddBuff(78, 1);
					break;
				case "Meditate":
					Card meditate = STS.ChooseCard(discardPile);
					hand.Add(meditate);
					discardPile.Remove(meditate);
					meditate.Description += " Retain.";
					hero.SwitchStance("Calm", discardPile, hand);
					break;
                case "Mental Fortress":
					hero.AddBuff(11, 4);
					break;
				case "Nirvana":
					hero.AddBuff(79, 3);
					break;
				case "Omniscience":
					Card omni = new();
					do
						omni = STS.ChooseCard(drawPile);
					while (omni.Description.Contains("Unplayable"));
					omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					omni.Exhaust(exhaustPile,drawPile);
					break;
				case "Perseverance":
					int perseverance = 0;
					foreach (string s in hero.Actions)
						if (s == "Perseverance Retained")
							perseverance += 2;
					hero.CardBlock(perseverance + 5);
					break;
                case "Pray":
					hero.AddBuff(10, 3);
                    hero.Actions.Add("Mantra Gained: 3");
                    drawPile.Add(new Card(Dict.cardL[335]));
					STS.Shuffle(drawPile, rng);
					break;
				case "Pressure Points":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(12, 8);
					for (int i = 0; i < encounter.Count; i++)
						if (Actor.FindBuff("Mark", encounter[i].Buffs) != null)
							hero.NonAttackDamage(encounter[i], Actor.FindBuff("Mark", encounter[i].Buffs).Intensity.GetValueOrDefault());
					break;
				case "Prostrate":
					hero.AddBuff(10, 2);
					hero.Actions.Add("Mantra Gained: 2");
					hero.CardBlock(4);
					break;
				case "Protect":
					hero.CardBlock(13);
					break;
				case "Ragnarok":
					for (int i = 0; i < 5; i++)
					{
						target = rng.Next(0, encounter.Count);
						hero.SingleAttack(encounter[target], 5);
					}
					break;
				case "Reach Heaven":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					drawPile.Add(new Card(Dict.cardL[340]));
					STS.Shuffle(drawPile, rng);
					break;
				case "Rushdown":
					hero.AddBuff(80, 2);
					break;
				case "Sanctity":
                    string sanctity = "";
                    for (int i = hero.Actions.Count - 1; i >= 0; i--)
                        if (hero.Actions[i].Contains("Played"))
						{
                            sanctity = hero.Actions[i];
							break;
                        }
					if (sanctity.Contains("Skill"))
						STS.DrawCards(drawPile, hand, discardPile, rng, 2);
                    break;
				case "Sands of Time":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 20);
					break;
				case "Sash Whip":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 8);
                    string sashWhip = "";
                    for (int i = hero.Actions.Count - 1; i >= 0; i--)
                        if (hero.Actions[i].Contains("Played"))
                        {
                            sashWhip = hero.Actions[i];
                            break;
                        }
					if (sashWhip.Contains("Attack"))
						encounter[target].AddBuff(2, 1);
					break;
                case "Scrawl":
					STS.DrawCards(drawPile, hand, discardPile, rng, 10 - hand.Count);
					break;
				case "Signature Move":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 30);
					break;
				case "Simmering Fury":
					hero.AddBuff(81,2);
					break;
				case "Spirit Shield":
					hero.CardBlock(hand.Count * 3);
					break;
				case "Study":
					hero.AddBuff(82, 1);
					break;
				case "Talk to the Hand":
                    target = hero.DetermineTarget(encounter);
                    hero.SingleAttack(encounter[target], 5);
					encounter[target].AddBuff(83, 2);
					break;
                case "Tantrum":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < 3; i++)
						hero.SingleAttack(encounter[target], 3);
					break;
				case "Third Eye":
					hero.CardBlock(7);
					Scry(drawPile, discardPile,hand, 3);
					break;
				case "Tranquility":
					hero.SwitchStance("Calm", discardPile, hand);
					break;
				case "Vault":
					// Have to code how to end turn early
					break;
				case "Vigilance":
					hero.CardBlock(8);
					hero.SwitchStance("Calm", discardPile, hand);
					break;
				case "Wallop":
					target = hero.DetermineTarget(encounter);
					int wallop = encounter[target].Hp;
					hero.SingleAttack(encounter[target], 9);
					wallop -= encounter[target].Hp;
					hero.CardBlock(wallop);
					break;
				case "Wave of the Hand":
					hero.AddBuff(84, 1);
					break;
				case "Weave":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Wheel Kick":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 15);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "Wish":
					List<Card> lastWish = new List<Card>();
					for (int i = 0; i < 3; i++)
						lastWish.Add(new Card(Dict.cardL[i + 361]));
					Card wish = STS.ChooseCard(lastWish);
					wish.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					break;
				case "Worship":
					hero.AddBuff(10, 5);
					hero.Actions.Add("Mantra Gained:5");
					break;
				case "Wreath of Flame":
					hero.AddBuff(85, 5);
					break;
				// COLORLESS CARDS (294 - 340)
				case "Apotheosis":
					break;
				case "Bandage Up":
					hero.HealHP(4);
					break;
				case "Beta":
					drawPile.Add(new Card(Dict.cardL[337]));
					STS.Shuffle(drawPile, rng);
					break;
				case "Bite":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					hero.HealHP(2);
					break;
				case "Blind":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(2, 2);
					break;
				case "Chrysalis":
					List<Card> chrysalis = new List<Card>(hero.RandomCards(hero.Name, 3, rng));
					foreach (Card card in chrysalis)
						card.EnergyCost = "0";
					drawPile.AddRange(chrysalis);
					STS.Shuffle(drawPile, rng);
					break;
				case "Dark Shackles":
                    hero.AddBuff(4, -9);
                    hero.AddBuff(30, -9);
                    break;
                case "Deep Breath":
					for (int i = discardPile.Count;i > 0;i--)
                    {
						drawPile.Add(discardPile[i - 1]);
						discardPile.RemoveAt(i-1);
					}
					STS.Shuffle(drawPile,rng);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Discovery":
					Card discovery = new(STS.ChooseCard(hero.RandomCards(hero.Name, 3, rng)));
					discovery.EnergyCost = "0";
					hand.Add(discovery);
					break;
				case "Dramatic Entrance":
					hero.AttackAll(encounter, 8);
					break;
				case "Enlightment":
					foreach (Card c in hand)
						if (Int32.Parse(c.EnergyCost) > 1)
							c.EnergyCost = "1";
					break;
				case "Finesse":
					hero.CardBlock(2);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Flash of Steel":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Forethought":
					Card forethought = STS.ChooseCard(hand);
					forethought.EnergyCost = "0";
					hand.Remove(forethought);
					drawPile.Prepend(forethought);
					break;
				case "Good Instincts":
					hero.CardBlock(6);
					break;
				case "Jack of All Trades":
					Card jackOfAllTrades = new Card(STS.ChooseCard(hero.RandomCards("Colorless",3,rng)));	
					hand.Add(jackOfAllTrades);
					break;
				case "Hand of Greed":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					if (encounter[target].Hp <= 0)
						hero.GoldChange(20);
					break;
				case "Impatience":
					if (!hand.Any(x => x.Type == "Attack"))
						STS.DrawCards(drawPile, hand,discardPile, rng, 2);
					break;
				case "Insight":
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "J.A.X.":
					hero.SelfDamage(3);
					hero.AddBuff(4, 2);
					break;
				case "Madness":
					if (hand.Count > 0)
						hand[rng.Next(hand.Count)].EnergyCost = "0";
					break;
				case "Magnetism":
					hero.AddBuff(86, 1);
					break;
				case "Master of Strategy":
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
					break;
				case "Mayhem":
					hero.AddBuff(88, 1);
					break;
				case "Metamorphosis":
                    List<Card> metamorphosis = new List<Card>(hero.RandomCards(hero.Name, 3, rng));
                    foreach (Card card in metamorphosis)
                        card.EnergyCost = "0";
                    drawPile.AddRange(metamorphosis);
                    STS.Shuffle(drawPile, rng);
                    break;
                case "Mind Blast":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], drawPile.Count);
					break;
				case "Miracle":
					hero.GainTurnEnergy(1);
					break;
				case "Panacea":
					hero.AddBuff(8, 1);
					break;
				case "Panic Button":
					hero.CardBlock(30);
					hero.AddBuff(13, 2);
					break;
				case "Purity":
					for (int i = 0; i < 3; i++)
						STS.ChooseCard(hand).Exhaust(exhaustPile, hand);
					break;
				case "Ritual Dagger":
					target = hero.DetermineTarget(encounter);
					string ritualDagger = Description;
					Description = Regex.Replace(Description, @"[a-zA-Z '.]", "");
					Description.Remove(Description.Length - 1);
					hero.SingleAttack(encounter[target], Int32.Parse(Description));
					Description = ritualDagger;
					if (encounter[target].Hp <= 0) ;
						//Add way to add to total damage.
					break;
				case "Sadistic Nature":
					hero.AddBuff(87, 3);
					break;
				case "Safety":
					hero.CardBlock(12);
					break;
				case "Secret Technique":
					List<Card> secretSkill = new();
					foreach(Card c in drawPile)
						if (c.Type == "Skill")
							secretSkill.Add(c);
					Card secretSkillChoice = STS.ChooseCard(secretSkill);
					hand.Add(secretSkillChoice);
					drawPile.Remove(secretSkillChoice);
					break;
				case "Secret Weapon":
					List<Card> secretAttack = new();
					foreach (Card c in drawPile)
						if (c.Type == "Attack")
							secretAttack.Add(c);
					Card secretAttackChoice = STS.ChooseCard(secretAttack);
					hand.Add(secretAttackChoice);
					drawPile.Remove(secretAttackChoice);
					break;
				case "Shiv":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Smite":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 12);
					break;
				case "Swift Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					break;
				case "The Bomb":
					hero.AddBuff(89, 3);
					break;
				case "Thinking Ahead":
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					Card thinkingAhead = new(STS.ChooseCard(hand));
					drawPile.Add(thinkingAhead);
					hand.Remove(thinkingAhead);
					break;
				case "Through Violence":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					break;
				case "Transmutation":
					for(int i = 0;i < hero.Energy; i++)
					{
						if (hand.Count < 10)
						{
							hand.Add(new Card(hero.RandomCards("Colorless", 1, rng)[0]));
						}
						else discardPile.Add(new Card(hero.RandomCards("Colorless", 1, rng)[0]));
                    }
					break;
				case "Trip":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(1, 2);
					break;
				case "Violence":
					for (int i = 0; i < 3; i++)
					{
						Card violence = new Card();
						while(violence.Type != "Attack" && drawPile.Any(x => x.Type == "Attack"))
							violence = drawPile[rng.Next(0, drawPile.Count)];
						if (violence == null)
							break;
						if (hand.Count < 10)
							hand.Add(violence);
						else discardPile.Add(violence);
						drawPile.Remove(violence);
					}					
					break;
				case "Necronomicurse":
					hand.Add(new Card(Dict.cardL[346]));
					break;
				case "Expunger":
					target = hero.DetermineTarget(encounter);
					string expunger = Description;
					Description = Regex.Replace(Description, @"[a-zA-Z .]", "");
					for (int i = 0; i < Int32.Parse(Description.Substring(1)); i++)
					hero.SingleAttack(encounter[target],9);
					Description = expunger;
					break;
				default:
					break;
			}
			hero.Actions.Add($"{Name}-{Type} Played");
			switch (Type)
            {
				default:
					break;
				case "Status":
					if (Name == "Slimed")
						break;
					Exhaust(exhaustPile);
					break;
				case "Curse":
					Exhaust(exhaustPile);
					break;
			}
		}
		//Moving Cards to different List methods
		public void Exhaust(List<Card> exhaustPile, List<Card> leaveThisList = null)
        {
			exhaustPile.Add(this);
			if (leaveThisList != null)
				leaveThisList.Remove(this);
			Console.WriteLine($"{Name} has been exhausted.");
		}

        public static void Discard(Hero hero, List<Card> hand, List<Card> discardPile, Card card)
        {
            if (!hand.Any())
                return;
            discardPile.Add(card);
            hand.Remove(card);
            hero.Actions.Add("Discard");
        }

		public static void Scry(List<Card> drawPile, List<Card> discardPile,List<Card> hand, int amount)
		{
			int scryChoice = -1;
			while (scryChoice != 0 && amount > 0)
			{
                Console.WriteLine($"\nEnter the number of the card you would like to scry into your discard pile or hit 0 to move on.");
                for (int i = 1; i <= amount; i++)
                    Console.WriteLine($"{i}:{drawPile[drawPile.Count - i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out scryChoice) || scryChoice < 0 || scryChoice > amount)
                    Console.WriteLine("Invalid input, enter again:");
				if (scryChoice > 0)
				{
					discardPile.Add(drawPile[drawPile.Count-scryChoice]);
					drawPile.RemoveAt(drawPile.Count-scryChoice);
					amount--;
				}
            }
            for (int i = discardPile.Count; i > 0; i--)											// Weave Check
            {
                if (discardPile[i - 1].Name == "Weave" && hand.Count < 10)
                {
                    hand.Add(discardPile[i - 1]);
                    discardPile.Remove(discardPile[i - 1]);
                }
            }
        }

		// Checks to see if a card can be played, then plays it
        public void UseCard(Hero hero, List<Enemy> Encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)
        {
            if (Name == "Eviscerate" || Name == "Force Field" || Name == "Sands of Time")
            {
				switch(Name)
				{
					default: 
						foreach(string s in hero.Actions)
							if (s.Contains("Discard") && EnergyCost != "0")
                                EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
						break;

                    case "Force Field":
                        foreach (string s in hero.Actions)
                            if (s.Contains("Power") && EnergyCost != "0")
                                EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
						break;
					case "Sands of Time":
						foreach(string s in hero.Actions )
							if (s == "Sands of Time Retained" && EnergyCost != "0")
                                EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
						break;
                }
			}
            if (Int32.Parse(EnergyCost) > hero.Energy)
			{
                Console.WriteLine($"You failed to play {Name}. You need {EnergyCost} Energy to play {Name}.");
				return;
            }                
            if (Name == "Clash" && !hand.All(x => x.Type == "Attack"))
			{
                Console.WriteLine("You can't play Clash as you have something that isn't an Attack in your hand.");
				return;
            }
			if (Name == "Signature Move" && hand.Any(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Signature Move as you have a different Attack in your hand.");
                return;
            }
            if (Name == "Grand Finale" && drawPile.Count != 0)
			{
                Console.WriteLine("You can't play Grand Finale because you have cards in your draw pile.");
                return;
            }
			if (Description.Contains("Unplay"))
			{
                Console.WriteLine("You can't play this card, read it's effects to learn more.");
                return;
            }
            hand.Remove(this);
            Console.WriteLine($"You played {Name}.");
            CardAction(hero, Encounter, drawPile, discardPile, hand, exhaustPile, rng);
            hero.Energy -= (Int32.Parse(EnergyCost));
            if (Description.Contains("Exhaust"))
                Exhaust(exhaustPile);
            else if (Name == "Tantrum")
            {
                drawPile.Add(this);
                STS.Shuffle(drawPile, rng);
            }
            else if (Type != "Power")
                discardPile.Add(this);
        }
    }
}