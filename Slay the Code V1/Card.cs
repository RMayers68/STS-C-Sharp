using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace STV
{
	public class Card : IEquatable<Card>, IComparable<Card>
	{

		// attributes
		public string Name { get; set; }
		public string Type { get; set; } // Attack, Skill, Power, Status or Curse
		public string Rarity { get; set; } //Common,Uncommon,Rare 
		public string EnergyCost { get; set; } // currently string because of X and Unplayable Energy Cost cards, will change in future
		public string Description { get; set; }
		private int GoldCost { get; set; }



		//constructors
		public Card()
		{
			this.Name = "Purchased";
			this.GoldCost = 0;
		}

		public Card(string name, string type, string rarity, string energyCost)
		{
			this.Name = name;
			this.Type = type;
			this.Rarity = rarity;
			this.EnergyCost = energyCost;
		}

		public Card(Card card)
		{
			Random rng = new Random();
			this.Name = card.Name;
			this.Type = card.Type;
			this.Rarity = card.Rarity;
			this.EnergyCost = card.EnergyCost;
			this.GoldCost = Rarity == "Rare" ? rng.Next(135, 166) : Rarity == "Uncommon" ? rng.Next(68, 83) : rng.Next(45, 56);
		}

		//comparators and equals
		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			Card objAsPart = obj as Card;
			if (objAsPart == null) return false;
			else return Equals(objAsPart);
		}

		public bool Equals(Card other)
		{
			if (other == null) return false;
			return (this.Name.Equals(other.Name));
		}

		// Default comparer for Card type.
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
			if (EnergyCost == "None")
				return $"Name: {Name}\nType: {Type}\nEffect: {setDescription()}";
			return $"Name: {Name}\nEnergy Cost: {EnergyCost}\tType: {Type}\nEffect: {setDescription()}";
		}

		public int getGoldCost()
		{
			return GoldCost;
		}

		public static Card FindCard(string cardName, List<Card> list)
		{
			return list.Find(x => x.Name == cardName);
		}

		public void MoveCard(List<Card> from, List<Card> to)
		{
			from.Remove(this);
			to.Add(this);
		}

		public void CardAction(Hero hero, List<Enemy> encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)
		{
			//Lines 117-163 check to see if the card is playable given the circumstances
			if (Name == "Eviscerate" || Name == "Force Field" || Name == "Sands of Time")
			{
				switch (Name)
				{
					default:
						foreach (string s in hero.Actions)
							if (s.Contains("Discard") && EnergyCost != "0")
								EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
						break;

					case "Force Field":
						foreach (string s in hero.Actions)
							if (s.Contains("Power") && EnergyCost != "0")
								EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
						break;
					case "Sands of Time":
						foreach (string s in hero.Actions)
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
			if (setDescription().Contains("Unplay"))
			{
				Console.WriteLine("You can't play this card, read it's effects to learn more.");
				return;
			}
			// Moving the card played from hand to designated location
			if (FindCard(Name, hand) != null)
			{
				if (setDescription().Contains("Exhaust") || Type == "Status" || Type == "Curse")
					Exhaust(exhaustPile, hand);
				else if (Type == "Power")
					hand.Remove(this);
				else if (Name == "Tantrum")
				{
					MoveCard(hand, drawPile);
					STS.Shuffle(drawPile, rng);
				}
				else
					MoveCard(hand, discardPile);
			}
			// Effects of cards begin here
			Console.WriteLine($"You played {Name}.");
			int xEnergy = hero.Energy;
			int target = 0;
			int damage = 0;
			if (EnergyCost == "X")
				EnergyCost = $"{hero.Energy}";
			hero.Energy -= Int32.Parse(EnergyCost);

			switch (Name)
			{
				// IRONCLAD CARDS (0 - 72)																					
				case "Anger":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					discardPile.Add(new Card(Dict.cardL[0]));
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
					hero.AddBuff(23, 1);
					break;
				case "Burning Pact":
					Card burningPact = STS.ChooseCard(hand, "exhaust");
					burningPact.Exhaust(exhaustPile, hand);
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
					encounter[target].AddBuff(2, 2);
					break;
				case "Combust":
					hero.AddBuff(24, 1);
					break;
				case "Corruption":
					hero.AddBuff(25, 1);
					break;
				case "Dark Embrace":
					hero.AddBuff(26, 1);
					break;
				case "Demon Form":
					hero.AddBuff(3, 2);
					break;
				case "Disarm":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(4, -2);
					break;
				case "Double Tap":
					hero.AddBuff(27, 1);
					break;
				case "Dropkick":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 5);
					if (Actor.FindBuff("Vulnerable", encounter[target].Buffs) != null)
						hero.GainTurnEnergy(1);
					break;
				case "Dual Wield":
					Card dualWield = new Card();
					do
						dualWield = STS.ChooseCard(hand, "copy");
					while (dualWield.Type == "Skill");
					hand.Add(dualWield);
					break;
				case "Entrench":
					hero.Block *= 2;
					break;
				case "Evolve":
					hero.AddBuff(28, 1);
					break;
				case "Exhume":
					Card exhume = STS.ChooseCard(exhaustPile, "bring back");
					exhaustPile.Remove(exhume);
					hand.Add(exhume);
					break;
				case "Feed":                                                //minion buff add
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					if (encounter[target].Hp <= 0)
					{
						hero.HealHP(3);
						hero.MaxHP += 3;
					}
					break;
				case "Feel No Pain":
					hero.AddBuff(29, 3);
					break;
				case "Fiend Fire":
					target = hero.DetermineTarget(encounter);
					damage = 0;
					for (int i = hand.Count; i > 1; i--)
					{
						hand[i].Exhaust(exhaustPile, hand);
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
					Card havoc = drawPile.Last();
					havoc.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					havoc.Exhaust(exhaustPile, drawPile);
					break;
				case "Headbutt":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 9);
					Card headbutt = STS.ChooseCard(discardPile, "send to the top of your drawpile");
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
					foreach (Enemy e in encounter)
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
					foreach (Card c in hand)
					{
						if (c.Type != "Attack")
						{
							c.Exhaust(exhaustPile, hand);
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
							c.Exhaust(exhaustPile, hand);
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
					Card warcry = STS.ChooseCard(hand, "add to the top of your drawpile");
					hand.Remove(warcry);
					drawPile.Add(warcry);
					break;
				case "Whirlwind":
					for (int i = 0; i < Int32.Parse(EnergyCost); i++)
						hero.AttackAll(encounter, 5);
					break;
				case "Wild Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 12);
					drawPile.Add(new Card(Dict.cardL[357]));
					STS.Shuffle(drawPile, rng);
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
						Discard(hero, hand, discardPile, STS.ChooseCard(hand, "discard"));
					break;
				case "Adrenaline":
					hero.GainTurnEnergy(1);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "After Image":
					hero.AddBuff(37, 1);
					break;
				case "Alchemize":
					hero.Potions.Add(Dict.potionL[rng.Next(0, Dict.potionL.Count)]);
					break;
				case "All-Out Attack":
					hero.AttackAll(encounter, 10);
					Discard(hero, hand, discardPile, hand[rng.Next(0, hand.Count)]);
					break;
				case "Backflip":
					hero.CardBlock(5);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "Backstab":                                //Innate												
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
					for (gamble = 0; gamble < hand.Count; gamble++)
					{
						Discard(hero, hand, discardPile, hand[hand.Count - 1]);
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
						Discard(hero, hand, discardPile, STS.ChooseCard(hand, "discard"));
					hero.GainTurnEnergy(2);
					break;
				case "Corpse Explosion":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(38, 6);
					encounter[target].AddBuff(43, 1);
					break;
				case "Crippling Cloud":
					foreach (Enemy enemy in encounter)
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
					Discard(hero, hand, discardPile, STS.ChooseCard(hand, "discard"));
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
					hero.AddBuff(22, xEnergy);
					hero.AddBuff(45, xEnergy);
					break;
				case "Endless Agony":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Envenom":
					hero.AddBuff(46, 1);
					break;
				case "Escape Plan":
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
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
					if (Actor.FindBuff("Weak", encounter[target].Buffs) != null)
						hero.GainTurnEnergy(1);
					break;
				case "Infinite Blades":
					hero.AddBuff(47, 1);
					break;
				case "Leg Sweep":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(2, 1);
					hero.CardBlock(11);
					break;
				case "Malaise":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(4, xEnergy);
					encounter[target].AddBuff(2, xEnergy);
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
					Card nightmare = STS.ChooseCard(hand, "copy");
					for (int i = 0; i < 3; i++)
						drawPile.Add(new Card(nightmare));
					// eventually have buff that copies card
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
						Discard(hero, hand, discardPile, STS.ChooseCard(hand, "discard"));
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
					Card setup = STS.ChooseCard(hand, "add to the top of your drawpile");
					drawPile.Add(setup);
					hand.Remove(setup);
					setup.EnergyCost = "0";
					break;
				case "Skewer":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < xEnergy; i++)
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
						xEnergy += 2;
					break;
				case "Storm of Steel":
					int storm = 0;
					for (storm = 0; storm < hand.Count; storm++)
					{
						Discard(hero, hand, discardPile, hand[hand.Count - 1]);
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
					Discard(hero, hand, discardPile, STS.ChooseCard(hand, "discard"));
					break;
				case "Terror":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(1, 99);
					break;
				case "Tools of the Trade":
					hero.AddBuff(45, 1);
					hero.AddBuff(50, 1);
					break;
				case "Unload":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 14);
					for (int i = hand.Count; i > 0; i--)
					{
						if (hand[i - 1].Type == "Attack")
							Discard(hero, hand, discardPile, hand[i - 1]);
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
					foreach (Card zeroCost in discardPile)
					{
						if (zeroCost.EnergyCost == "0" && hand.Count < 10)
						{
							hand.Add(zeroCost);
							discardPile.Remove(zeroCost);
						}
					}
					break;
				case "Amplify":
					hero.AddBuff(54, 1);
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
					hero.AddBuff(7, 4);
					hero.AddBuff(55, 1);
					break;
				case "Blizzard":
					foreach (string s in hero.Actions)
						if (s.Contains("Channel Frost"))
							damage += 2;
					hero.AttackAll(encounter, damage);
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
					hero.ChannelOrb(encounter, rng.Next(0, 4));
					break;
				case "Charge Battery":
					hero.CardBlock(7);
					hero.AddBuff(22, 1);
					break;
				case "Chill":
					foreach (var e in encounter)
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
					STS.DrawCards(drawPile, hand, discardPile, rng, hero.Orbs.Distinct().Count());
					break;
				case "Consume":
					hero.AddBuff(7, 2);
					hero.OrbSlots -= 1;
					while (hero.OrbSlots > hero.Orbs.Count)
						hero.Orbs.RemoveAt(hero.Orbs.Count - 1);
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
					hero.AddBuff(58, 1);
					break;
				case "Darkness":
					hero.ChannelOrb(encounter, 2);
					break;
				case "Defragment":
					hero.AddBuff(7, 1);
					break;
				case "Doom and Gloom":
					hero.AttackAll(encounter, 10);
					hero.ChannelOrb(encounter, 2);
					break;
				case "Double Energy":
					hero.GainTurnEnergy(hero.Energy * 2);
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
					foreach (var Orb in hero.Orbs)
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
						STS.DrawCards(drawPile, hand, discardPile, rng, 1);
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
					hero.AddBuff(61, 1);
					break;
				case "Hologram":
					hero.CardBlock(3);
					Card hologram = STS.ChooseCard(discardPile, "add into your hand");
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
					hero.AddBuff(62, 1);
					break;
				case "Machine Learning":
					hero.AddBuff(63, 1);
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
						hero.ChannelOrb(encounter, 3);
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
					for (int i = 0; i < 3; i++)
						hero.ChannelOrb(encounter, i);
					break;
				case "Reboot":
					foreach (Card c in discardPile)
					{
						discardPile.Remove(c);
						drawPile.Add(c);
					}
					foreach (Card c in hand)
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
					Orb recursion = hero.Orbs[0];
					hero.Evoke(encounter);
					hero.Orbs.RemoveAt(0);
					hero.Orbs.Add(recursion);
					break;
				case "Recycle":
					Card recycle = STS.ChooseCard(hand, "exhaust");
					recycle.Exhaust(exhaustPile, hand);
					hero.Energy += Int32.Parse(recycle.EnergyCost);
					break;
				case "Reinforced Body":
					for (int i = 0; i < Int32.Parse(EnergyCost); i++)
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
					for (int i = 0; i < 4; i++)
					{
						STS.DrawCards(drawPile, hand, discardPile, rng, 1);
						if (hand[hand.Count - 1].EnergyCost != "0")
							Discard(hero, hand, discardPile, hand[hand.Count - 1]);
					}
					break;
				case "Seek":
					hand.Add(STS.ChooseCard(drawPile, "add to your hand"));
					break;
				case "Self Repair":
					hero.AddBuff(65, 7);
					break;
				case "Skim":
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
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
						EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
					break;
				case "Sunder":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 24);
					if (encounter[target].Hp <= 0)
						hero.GainTurnEnergy(3);
					break;
				case "Sweeping Beam":
					hero.AttackAll(encounter, 6);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Tempest":
					for (int i = 0; i < Int32.Parse(EnergyCost); i++)
						hero.ChannelOrb(encounter, 0);
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
					STS.Shuffle(drawPile, rng);
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
					foreach (Actor a in encounter)
						hero.SingleAttack(encounter[target], 7);
					break;
				case "Brilliance":
					foreach (string s in hero.Actions)
						if (s.Contains("Mantra"))
							damage += Int32.Parse(s.Last().ToString());
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], damage + 10);
					break;
				case "Carve Reality":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					hand.Add(new Card(Dict.cardL[339]));
					break;
				case "Collect":
					hero.AddBuff(72, xEnergy);
					break;
				case "Conclude":                                        //End turn needed
					hero.AttackAll(encounter, 12);
					break;
				case "Conjure Blade"://fixing needed
					/*
					drawPile.Add(new Card(Dict.cardL[360]));
					drawPile[drawPile.Count - 1].Description = Regex.Replace(drawPile[drawPile.Count - 1].Description(), "X", $"{EnergyCost}");
					*/
					break;
				case "Consecrate":
					hero.AttackAll(encounter, 5);
					break;
				case "Crescendo":
					hero.SwitchStance("Wrath", discardPile, hand);
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
					Scry(drawPile, discardPile, hand, 2);
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
					Exhaust(exhaustPile, hand);
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
					hero.AddBuff(9, 3);
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
					Card fore, ign, infl = new Card();
					List<Card> fi = new(4);
					for (int i = 0; i < 3; i++)
					{
						Card uence = new Card();
						while (uence.Type != "Attack")
							uence = Dict.cardL[rng.Next(0, 291)];
						switch (i)
						{
							case 0: fore = uence; fi.Add(fore); break;
							case 1: ign = uence; fi.Add(ign); break;
							case 2: infl = uence; fi.Add(infl); break;
						}
					}
					hand.Add(new(STS.ChooseCard(fi, "add to your hand")));
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
					Scry(drawPile, discardPile, hand, 1);
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
					Card meditate = STS.ChooseCard(discardPile, "add to your hand");
					hand.Add(meditate);
					discardPile.Remove(meditate);
					//meditate.Description += "Retain.";			Have to fix how descriptions work
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
						omni = STS.ChooseCard(drawPile, "play twice");
					while (omni.setDescription().Contains("Unplayable"));
					omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					omni.Exhaust(exhaustPile, drawPile);
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
					hero.AddBuff(81, 2);
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
					Scry(drawPile, discardPile, hand, 3);
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
					Card wish = STS.ChooseCard(lastWish, "use");
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
					List<Card> chrysalis = new List<Card>(RandomCards(hero.Name, 3, rng));
					foreach (Card c in chrysalis)
						c.EnergyCost = "0";
					drawPile.AddRange(chrysalis);
					STS.Shuffle(drawPile, rng);
					break;
				case "Dark Shackles":
					hero.AddBuff(4, -9);
					hero.AddBuff(30, -9);
					break;
				case "Deep Breath":
					for (int i = discardPile.Count; i > 0; i--)
					{
						drawPile.Add(discardPile[i - 1]);
						discardPile.RemoveAt(i - 1);
					}
					STS.Shuffle(drawPile, rng);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Discovery":
					Card discovery = new(STS.ChooseCard(RandomCards(hero.Name, 3, rng), "add to your hand"));
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
					Card forethought = STS.ChooseCard(hand, "add to the bottom of your drawpile");
					forethought.EnergyCost = "0";
					hand.Remove(forethought);
					drawPile.Prepend(forethought);
					break;
				case "Good Instincts":
					hero.CardBlock(6);
					break;
				case "Jack of All Trades":
					Card jackOfAllTrades = new Card(STS.ChooseCard(RandomCards("Colorless", 3, rng), "add to your hand"));
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
						STS.DrawCards(drawPile, hand, discardPile, rng, 2);
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
					List<Card> metamorphosis = new List<Card>(RandomCards(hero.Name, 3, rng));
					foreach (Card c in metamorphosis)
						c.EnergyCost = "0";
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
						STS.ChooseCard(hand, "exhaust").Exhaust(exhaustPile, hand);
					break;
				case "Ritual Dagger": //We'll be fixing, comment this out
					/*target = hero.DetermineTarget(encounter);
					string ritualDagger = Description;
                    Description = Regex.Replace(Description, @"[a-zA-Z '.]", "");
					Description.Remove(Description.Length - 1);
					hero.SingleAttack(encounter[target], Int32.Parse(Description));
					Description = ritualDagger;
					if (encounter[target].Hp <= 0) ;
						//Add way to add to total damage.*/
					break;
				case "Sadistic Nature":
					hero.AddBuff(87, 3);
					break;
				case "Safety":
					hero.CardBlock(12);
					break;
				case "Secret Technique":
					List<Card> secretSkill = new();
					foreach (Card c in drawPile)
						if (c.Type == "Skill")
							secretSkill.Add(c);
					Card secretSkillChoice = STS.ChooseCard(secretSkill, "add to your hand");
					hand.Add(secretSkillChoice);
					drawPile.Remove(secretSkillChoice);
					break;
				case "Secret Weapon":
					List<Card> secretAttack = new();
					foreach (Card c in drawPile)
						if (c.Type == "Attack")
							secretAttack.Add(c);
					Card secretAttackChoice = STS.ChooseCard(secretAttack, "add to your hand");
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
					Card thinkingAhead = new(STS.ChooseCard(hand, "add to the top of your drawpile"));
					drawPile.Add(thinkingAhead);
					hand.Remove(thinkingAhead);
					break;
				case "Through Violence":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					break;
				case "Transmutation":
					for (int i = 0; i < xEnergy; i++)
					{
						if (hand.Count < 10)
						{
							hand.Add(new Card(RandomCards("Colorless", 1, rng)[0]));
						}
						else discardPile.Add(new Card(RandomCards("Colorless", 1, rng)[0]));
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
						while (violence.Type != "Attack" && drawPile.Any(x => x.Type == "Attack"))
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
				case "Expunger": // we will be fixing
					/*
					target = hero.DetermineTarget(encounter);
					string expunger = Description;
					Description = Regex.Replace(Description, @"[a-zA-Z .]", "");
					for (int i = 0; i < Int32.Parse(Description.Substring(1)); i++)
					hero.SingleAttack(encounter[target],9);
					Description = expunger;*/
					break;
				default:
					break;
			}
			hero.Actions.Add($"{Name}-{Type} Played");
		}
		//Moving Cards to different List methods
		public void Exhaust(List<Card> exhaustPile, List<Card> leaveThisList)
		{
			MoveCard(leaveThisList, exhaustPile);
			Console.WriteLine($"{Name} has been exhausted.");
		}

		public static void Discard(Hero hero, List<Card> hand, List<Card> discardPile, Card card)
		{
			if (!hand.Any())
				return;
			card.MoveCard(hand, discardPile);
			hero.Actions.Add("Discard");
		}

		public static void Scry(List<Card> drawPile, List<Card> discardPile, List<Card> hand, int amount)
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
					Card scryedCard = drawPile[drawPile.Count - scryChoice];
					scryedCard.MoveCard(drawPile, discardPile);
					amount--;
				}
			}
			// Weave Check
			if (Card.FindCard("Weave", discardPile) is Card weave && weave != null && hand.Count < 10)
				weave.MoveCard(discardPile, hand);
		}

		public static List<Card> RandomCards(string type, int count, Random rng)
		{
			List<Card> cards = new List<Card>();
			for (int i = 0; i < count; i++)
			{
				switch (type)
				{
					default:
						cards.Add(new Card(Dict.cardL[rng.Next(73)]));
						break;
					case "Silent":
						cards.Add(new Card(Dict.cardL[rng.Next(73, 146)]));
						break;
					case "Defect":
						cards.Add(new Card(Dict.cardL[rng.Next(146, 219)]));
						break;
					case "Watcher":
						cards.Add(new Card(Dict.cardL[rng.Next(221, 294)]));
						break;
					case "Colorless":
						cards.Add(new Card(Dict.cardL[rng.Next(297, 332)]));
						break;
				}
			}
			return cards;
		}

		public string setDescription()
		{
			switch (Name)
			{
				default: return "";
                case "Anger": return "Deal 6 damage. Place a copy of this card in your discard pile.";
                case "Armaments": return "Gain 5 Block. Upgrade a card in your hand for the rest of combat.";
                case "Barricade": return "Block no longer expires at the start of your turn.";
                case "Bash": return "Deal 8 damage. Apply 2 Vulnerable.";
                case "Battle Trance": return "Draw 3 cards. You cannot draw additional cards this turn.";
                case "Berserk": return "Gain 2 Vulnerable. At the start of your turn, gain 1 Energy.";
                case "Blood for Blood": return "Costs 1 less  for each time you lose HP in combat. Deal 18 damage.";
                case "Bloodletting": return "Lose 3 HP. Gain 2 Energy.";
                case "Bludgeon": return "Deal 32 damage.";
                case "Body Slam": return "Deal damage equal to your current Block.";
                case "Brutality": return "At the start of your turn, lose 1 HP and draw 1 card.";
                case "Burning Pact": return "Exhaust 1 card. Draw 2 cards.";
                case "Carnage": return "Ethereal. Deal 20 damage.";
                case "Clash": return "Can only be played if every card in your hand is an Attack. Deal 14 damage.";
                case "Cleave": return "Deal 8 damage to ALL enemies.";
                case "Clothesline": return "Deal 12 damage. Apply 2 Weak.";
                case "Combust": return "At the end of your turn, lose 1 HP and deal 5 damage to ALL enemies.";
                case "Corruption": return "Skills cost 0. Whenever you play a Skill, Exhaust it.";
                case "Dark Embrace": return "Whenever a card is Exhausted, draw 1 card.";
                case "Demon Form": return "At the start of each turn, gain 2 Strength.";
                case "Disarm": return "Enemy loses 2 Strength. Exhaust.";
                case "Double Tap": return "This turn, your next Attack is played twice.";
                case "Dropkick": return "Deal 5 damage. If the target is Vulnerable, gain  and draw 1 card.";
                case "Dual Wield": return "Create a copy of an Attack or Power card in your hand.";
                case "Entrench": return "Double your current Block.";
                case "Evolve": return "Whenever you draw a Status, draw 1 card.";
                case "Exhume": return "Choose an Exhausted card and put it in your hand. Exhaust.";
                case "Feed": return "Deal 10 damage. If this kills the enemy, gain 3 permanent Max HP. Exhaust.";
                case "Feel No Pain": return "Whenever a card is Exhausted, gain 3 Block.";
                case "Fiend Fire": return "Exhaust your hand. Deal 7 damage for each Exhausted card. Exhaust.";
                case "Fire Breathing": return "Whenever you draw a Status or Curse card, deal 6 damage to ALL enemies.";
                case "Flame Barrier": return "Gain 12 Block. Whenever you are attacked this turn, deal 4 damage to the attacker.";
                case "Flex": return "Gain 2 Strength. At the end of your turn, lose 2 Strength.";
                case "Ghostly Armor": return "Ethereal. Gain 10 Block.";
                case "Havoc": return "Play the top card of your draw pile and Exhaust it.";
                case "Headbutt": return "Deal 9 damage. Place a card from your discard pile on top of your draw pile.";
                case "Heavy Blade": return "Deal 14 damage. Strength affects Heavy Blade 3 times.";
                case "Hemokinesis": return "Lose 3 HP. Deal 14 damage.";
                case "Immolate": return "Deal 21 damage to ALL enemies. Shuffle a Burn into your discard pile.";
                case "Impervious": return "Gain 30 Block. Exhaust.";
                case "Infernal Blade": return "Add a random Attack to your hand. It costs 0 this turn. Exhaust.";
                case "Inflame": return "Gain 2 Strength.";
                case "Intimidate": return "Apply 1 Weak to ALL enemies. Exhaust.";
                case "Iron Wave": return "Gain 5 Block. Deal 5 damage.";
                case "Juggernaut": return "Whenever you gain Block, deal 5 damage to a random enemy.";
                case "Limit Break": return "Double your Strength. Exhaust.";
                case "Metallicize": return "At the end of your turn, gain 3 Block.";
                case "Offering": return "Lose 6 HP. Gain 2 Energy. Draw 3 cards. Exhaust.";
                case "Perfected Strike": return "Deal 6 damage. Deals an additional +2 damage for ALL of your cards containing Strike.";
                case "Pommel Strike": return "Deal 9 damage. Draw 1 card.";
                case "Power Through": return "Add 2 Wounds to your hand. Gain 15 Block.";
                case "Pummel": return "Deal 2 damage 4 times. Exhaust.";
                case "Rage": return "Whenever you play an Attack this turn, gain 3 Block.";
                case "Rampage": return "Deal 8 damage. Every time this card is played, increase its damage by 4 for this combat.";
                case "Reaper": return "Deal 4 damage to ALL enemies. Heal for unblocked damage. Exhaust.";
                case "Reckless Charge": return "Deal 7 damage. Shuffle a Dazed into your draw pile.";
                case "Rupture": return "Whenever you lose HP from a card, gain 1 Strength.";
                case "Searing Blow": return "Deal 12 damage. Can be upgraded any number of times.";
                case "Second Wind": return "Exhaust all non-Attack cards in your hand and gain 5 Block for each.";
                case "Seeing Red": return "Gain 2 Energy. Exhaust.";
                case "Sentinel": return "Gain 5 Block. If this card is Exhausted, gain 3 Energy.";
                case "Sever Soul": return "Exhaust all non-Attack cards in your hand. Deal 16 damage.";
                case "Shockwave": return "Apply 3 Weak and Vulnerable to ALL enemies. Exhaust.";
                case "Shrug It Off": return "Gain 8 Block. Draw 1 card.";
                case "Spot Weakness": return "If an enemy intends to attack, gain 3 Strength.";
                case "Sword Boomerang": return "Deal 3 damage to a random enemy 3 times.";
                case "Thunderclap": return "Deal 4 damage and apply 1 Vulnerable to ALL enemies.";
                case "True Grit": return "Gain 7 Block. Exhaust a random card from your hand.";
                case "Twin Strike": return "Deal 5 damage twice.";
                case "Uppercut": return "Deal 13 damage. Apply 1 Weak. Apply 1 Vulnerable.";
                case "Warcry": return "Draw a card. Place a card from your hand on top of your draw pile. Exhaust.";
                case "Whirlwind": return "Deal 5 damage to ALL enemies X times.";
                case "Wild Strike": return "Deal 12 damage. Shuffle a Wound into your draw pile.";
                case "A Thousand Cuts": return "Whenever you play a card, deal 1 damage to ALL enemies.";
                case "Accuracy": return "Shivs deal 3 additional damage.";
                case "Acrobatics": return "Draw 3 cards. Discard 1 card.";
                case "Adrenaline": return "Gain . Draw 2 cards. Exhaust.";
                case "After Image": return "Whenever you play a card, gain 1 Block.";
                case "Alchemize": return "Obtain a random potion. Exhaust.";
                case "All-Out Attack": return "Deal 10 damage to ALL enemies. Discard 1 card at random.";
                case "Backflip": return "Gain 5 Block. Draw 2 cards.";
                case "Backstab": return "Deal 11 damage. Innate. Exhaust.";
                case "Bane": return "Deal 7 damage. If the enemy is Poisoned, deal 7 damage again.";
                case "Blade Dance": return "Add 2 Shivs to your hand.";
                case "Blur": return "Gain 5 Block. Block is not removed at the start of your next turn.";
                case "Bouncing Flask": return "Apply 3 Poison to a random enemy 3 times.";
                case "Bullet Time": return "You cannot draw any cards this turn. Reduce the cost of cards in your hand to 0 this turn.";
                case "Burst": return "This turn your next Skill is played twice.";
                case "Calculated Gamble": return "Discard your hand, then draw that many cards. Exhaust.";
                case "Caltrops": return "Whenever you are attacked, deal 3 damage to the attacker.";
                case "Catalyst": return "Double an enemy's Poison. Exhaust.";
                case "Choke": return "Deal 12 damage. Whenever you play a card this turn, targeted enemy loses 3 HP.";
                case "Cloak And Dagger": return "Gain 6 Block. Add 1 Shiv to your hand.";
                case "Concentrate": return "Discard 3 cards. Gain  2 Energy.";
                case "Corpse Explosion": return "Apply 6 Poison. When an enemy dies, deal damage equal to its MAX HP to ALL enemies.";
                case "Crippling Cloud": return "Apply 4 Poison and 2 Weak to ALL enemies. Exhaust.";
                case "Dagger Spray": return "Deal 4 damage to ALL enemies twice.";
                case "Dagger Throw": return "Deal 9 damage. Draw 1 card. Discard 1 card.";
                case "Dash": return "Gain 10 Block. Deal 10 damage.";
                case "Deadly Poison": return "Apply 5 Poison.";
                case "Deflect": return "Gain 4 Block.";
                case "Die Die Die": return "Deal 13 damage to ALL enemies. Exhaust.";
                case "Distraction": return "Add a random Skill to your hand. It costs 0 this turn. Exhaust.";
                case "Dodge and Roll": return "Gain 4 Block. Next turn gain 4 Block.";
                case "Doppelganger": return "Next turn, draw X cards and gain X Energy.";
                case "Endless Agony": return "Whenever you draw this card, add a copy of it to your hand. Deal 4 damage. Exhaust.";
                case "Envenom": return "Whenever an attack deals unblocked damage, apply 1 Poison.";
                case "Escape Plan": return "Draw 1 card. If the card is a Skill, gain 3 Block.";
                case "Eviscerate": return "Costs 1 less Energy for each card discarded this turn. Deal 6 damage three times.";
                case "Expertise": return "Draw cards until you have 6 in hand.";
                case "Finisher": return "Deal 6 damage for each Attack played this turn.";
                case "Flechettes": return "Deal 4 damage for each Skill in your hand.";
                case "Flying Knee": return "Deal 8 damage. Next turn gain 1 Energy.";
                case "Footwork": return "Gain 2 Dexterity.";
                case "Glass Knife": return "Deal 8 damage twice. Glass Knife's damage is lowered by 2 this combat.";
                case "Grand Finale": return "Can only be played if there are no cards in your draw pile. Deal 40 damage to ALL enemies.";
                case "Heel Hook": return "Deal 5 damage. If the enemy is Weak, Gain  and draw 1 card.";
                case "Infinite Blades": return "At the start of your turn, add a Shiv to your hand.";
                case "Leg Sweep": return "Apply 2 Weak. Gain 12 Block.";
                case "Malaise": return "Enemy loses X Strength. Apply X Weak. Exhaust.";
                case "Masterful Stab": return "Cost 1 additional for each time you lose HP this combat. Deal 12 Damage.";
                case "Neutralize": return "Deal 3 damage. Apply 1 Weak.";
                case "Nightmare": return "Choose a card. Next turn, add 3 copies of that card into your hand. Exhaust.";
                case "Noxious Fumes": return "At the start of your turn, apply 2 Poison to ALL enemies.";
                case "Outmaneuver": return "Next turn gain 2 Energy.";
                case "Phantasmal Killer": return "On your next turn, your Attack damage is doubled.";
                case "Piercing Wail": return "ALL enemies lose 6 Strength for 1 turn. Exhaust.";
                case "Poisoned Stab": return "Deal 5 damage. Apply 3 Poison.";
                case "Predator": return "Deal 15 damage. Draw 2 more cards next turn.";
                case "Prepared": return "Draw 1 card. Discard 1 card.";
                case "Quick Slash": return "Deal 8 damage. Draw 1 card.";
                case "Reflex": return "Unplayable. If this card is discarded from your hand, draw 1 card.";
                case "Riddle with Holes": return "Deal 3 damage 5 times.";
                case "Setup": return "Place a card in your hand on top of your draw pile. It cost 0 until it is played.";
                case "Skewer": return "Deal 7 damage X times.";
                case "Slice": return "Deal 5 damage.";
                case "Sneaky Strike": return "Deal 12 damage. If you have discarded a card this turn, gain 2 Energy.";
                case "Storm of Steel": return "Discard your hand. Add 1  Shiv to your hand for each card discarded.";
                case "Sucker Punch": return "Deal 7 damage. Apply 1 Weak.";
                case "Survivor": return "Gain 8 Block. Discard a card.";
                case "Tactician": return "Unplayable. If this card is discarded from your hand, gain 1 Energy.";
                case "Terror": return "Apply 99 Vulnerable. Exhaust.";
                case "Tools of the Trade": return "At the start of your turn, draw 1 card and discard 1 card.";
                case "Unload": return "Deal 12 damage. Discard ALL non-Attack cards.";
                case "Well-Laid Plans": return "At the end of your turn, Retain up to 1 card.";
                case "Wraith Form": return "Gain 2 Intangible. At the end of your turn, lose 1 Dexterity.";
                case "Aggregate": return "Gain  for every 6 cards in your draw pile.";
                case "All For One": return "Deal 10 damage. Put all Cost 0 cards from your discard pile into your hand.";
                case "Amplify": return "This turn, your next Power is played twice.";
                case "Auto-Shields": return "If you have 0 Block, gain 11 Block.";
                case "Ball Lightning": return "Deal 7 damage. Channel 1 Lightning.";
                case "Barrage": return "Deal 4 damage for each Channeled Orb.";
                case "Beam Cell": return "Deal 3 damage and apply 1 Vulnerable.";
                case "Biased Cognition": return "Gain 4 Focus. At the start of each turn, lose 1 Focus.";
                case "Blizzard": return "Deal damage equal to 2 times the Frost Channeled this combat to ALL enemies.";
                case "Boot Sequence": return "Gain 10 Block. Innate. Exhaust.";
                case "Buffer": return "Prevent the next time you would lose HP.";
                case "Capacitor": return "Gain 2 Orb slots.";
                case "Chaos": return "Channel 1 random Orb.";
                case "Chill": return "Channel 1 Frost for each enemy in combat. Exhaust.";
                case "Claw": return "Deal 3 damage. All Claw cards deal 2 additional damage this combat.";
                case "Cold Snap": return "Deal 6 damage. Channel 1 Frost.";
                case "Charge Battery": return "Gain 7 Block. Next turn gain 1 Energy.";
                case "Consume": return "Gain 2 Focus. Lose 1 Orb Slot.";
                case "Coolheaded": return "Channel 1 Frost. Draw 1 card.";
                case "Creative AI": return "At the start of each turn, add a random Power card to your hand.";
                case "Darkness": return "Channel 1 Dark.";
                case "Defragment": return "Gain 1 Focus.";
                case "Doom and Gloom": return "Deal 10 damage to ALL enemies. Channel 1 Dark.";
                case "Double Energy": return "Double your Energy. Exhaust.";
                case "Dualcast": return "Evoke your next Orb 2 times.";
                case "Echo Form": return "The first card you play each turn is played twice. Ethereal.";
                case "Fission": return "Remove all of your Orbs. Gain an Orb slot for each Orb removed.";
                case "Force Field": return "Costs 1 less  for each Power card played this combat. Gain 12 Block.";
                case "FTL": return "Deal 5 damage. If you have played less than 3 cards this turn, draw 1 card.";
                case "Fusion": return "Channel 1 Plasma.";
                case "Genetic Algorithm": return "Gain 1 Block. When played, permanently increase this card's Block by 2. Exhaust.";
                case "Glacier": return "Gain 9 Block. Channel 2 Frost.";
                case "Go for the Eyes": return "Deal 3 damage. If the enemy intends to attack, apply 1 Weak.";
                case "Heatsinks": return "Whenever you play a Power card, draw 1 card.";
                case "Hello World": return "At the start of your turn, add a random Common card into your hand.";
                case "Hologram": return "Gain 3 Block. Return a card from your discard pile to your hand. Exhaust.";
                case "Hyperbeam": return "Deal 25 damage to ALL enemies. Lose 3 Focus.";
                case "Leap": return "Gain 9 Block.";
                case "Bullseye": return "Deal 8 damage. Apply 2 Lock-On.";
                case "Loop": return "At the start of your turn, use the passive ability of your first Orb.";
                case "Machine Learning": return "Draw 1 additional card at the start of each turn.";
                case "Melter": return "Remove all Block from an enemy. Deal 10 damage.";
                case "Meteor Strike": return "Deal 24 damage. Channel 3 Plasma.";
                case "Multi-Cast": return "Evoke your next Orb X times.";
                case "Overclock": return "Draw 2 cards. Add a Burn into your discard pile.";
                case "Rainbow": return "Channel 1 Lightning, 1 Frost, and 1 Dark. Exhaust.";
                case "Reboot": return "Shuffle all of your cards into your draw pile, then draw 4 cards. Exhaust.";
                case "Rebound": return "Deal 8 damage. Place the next card you play this turn on top of your draw pile.";
                case "Recursion": return "Evoke your next Orb. Channel the Orb that was just Evoked.";
                case "Recycle": return "Exhaust a card. Gain Energy equal to its cost.";
                case "Reinforced Body": return "Gain 7 Block X times.";
                case "Reprogram": return "Lose 2 Focus. Gain 1 Strength. Gain 1 Dexterity.";
                case "Rip and Tear": return "Deal 7 damage to a random enemy 2 times.";
                case "Scrape": return "Deal 7 damage. Draw 3 cards. Discard all cards drawn this way that do not cost 0.";
                case "Seek": return "Choose 1 card from your draw pile and place them into your hand. Exhaust.";
                case "Self Repair": return "At the end of combat, heal 7 HP.";
                case "Skim": return "Draw 3 cards.";
                case "Stack": return "Gain Block equal to the number of cards in your discard pile.";
                case "Static Discharge": return "Whenever you take attack damage, Channel 1 Lightning.";
                case "Steam Barrier": return "Gain 6 Block. This card's Block is lowered by 2 this combat.";
                case "Storm": return "Whenever you play a Power, Channel 1 Lightning.";
                case "Streamline": return "Deal 15 damage. Whenever you play Streamline, reduce its cost by 1 for this combat.";
                case "Sunder": return "Deal 24 damage. If this kills the enemy, gain 3 Energy.";
                case "Sweeping Beam": return "Deal 6 damage to ALL enemies. Draw 1 card.";
                case "Tempest": return "Channel X Lightning. Exhaust.";
                case "Thunder Strike": return "Deal 7 damage to a random enemy for each Lightning Channeled this combat.";
                case "TURBO": return "Gain 2 Energy. Add a Void into your discard pile.";
                case "White Noise": return "Add a random Power to your hand. It costs 0 this turn. Exhaust.";
                case "Zap": return "Channel 1 Lightning.";
                case "Equilibrium": return "Gain 13 Block. Retain your hand this turn.";
                case "Compile Driver": return "Deal 7 damage. Draw 1 card for each unique Orb you have.";
                case "Electrodynamics": return "Lightning now hits ALL enemies. Channel 2 Lightning.";
                case "Core Surge": return "Deal 11 damage. Gain 1 Artifact. Exhaust.";
                case "Defend": return "Gain 5 Block.";
                case "Strike": return "Deal 6 damage.";
                case "Alpha": return "Shuffle a Beta into your draw pile. Exhaust.";
                case "Battle Hymn": return "At the start of each turn, add a Smite into your hand.";
                case "Blasphemy": return "Enter Divinity. Die next turn. Exhaust";
                case "Bowling Bash": return "Deal 7 damage for each enemy in combat.";
                case "Brilliance": return "Deal 10 damage. Deals additional damage equal to Mantra gained this combat.";
                case "Carve Reality": return "Deal 6 damage. Add a Smite to your hand.";
                case "Collect": return "Put Miracle+ into your hand at the start of your next X turns. Exhaust.";
                case "Conclude": return "Deal 12 damage to ALL enemies. End your turn.";
                case "Conjure Blade": return "Shuffle an Expunger with X into your draw pile. Exhaust. (Expunger costs 1, deals 9 damage X times.)";
                case "Consecrate": return "Deal 5 damage to ALL enemies.";
                case "Crescendo": return "Retain. Enter Wrath. Exhaust.";
                case "Crush Joints": return "Deal 8 damage. If the last card played this combat was a Skill, apply 1 Vulnerable.";
                case "Cut Through Fate": return "Deal 7 damage. Scry 2. Draw 1 card.";
                case "Deceive Reality": return "Gain 4 Block. Add a Safety to your hand.";
                case "Deus Ex Machina": return "Unplayable. When you draw this card, add 2 Miracles to your hand and Exhaust.";
                case "Deva Form": return "Ethereal. At the start of your turn, gain Energy and increase this gain by 1.";
                case "Devotion": return "At the start of your turn, gain 2 Mantra.";
                case "Empty Body": return "Gain 7 Block. Exit your Stance.";
                case "Empty Fist": return "Deal 9 damage. Exit your Stance.";
                case "Empty Mind": return "Draw 2 cards. Exit your Stance.";
                case "Eruption": return "Deal 9 damage. Enter Wrath.";
                case "Establishment": return "Whenever a card is Retained, reduce its cost by 1 this combat.";
                case "Evaluate": return "Gain 6 Block. Shuffle an Insight into your draw pile.";
                case "Fasting": return "Gain 3 Strength. Gain 3 Dexterity. Gain 1 less  at the start of each turn.";
                case "Fear No Evil": return "Deal 8 damage. If the enemy intends to Attack, enter Calm.";
                case "Flurry Of Blows": return "Deal 4 damage. Whenever you change stances, return this from the discard pile to your hand.";
                case "Flying Sleeves": return "Retain. Deal 4 damage twice.";
                case "Follow-Up": return "Deal 7 damage. If the last card played this combat was an Attack, gain .";
                case "Foreign Influence": return "Choose 1 of 3 Attacks of any color to add into your hand. Exhaust.";
                case "Foresight": return "At the start of your turn, Scry 3.";
                case "Halt": return "Gain 3 Block. If you are in Wrath, gain 9 additional Block.";
                case "Indignation": return "If you are in Wrath, apply 3 Vulnerable to ALL enemies, otherwise enter Wrath.";
                case "Inner Peace": return "If you are in Calm, draw 3 cards. Otherwise, enter Calm.";
                case "Judgment": return "If the enemy has 30 or less HP, set their HP to 0.";
                case "Just Lucky": return "Scry 1. Gain 2 Block. Deal 3 damage.";
                case "Lesson Learned": return "Deal 10 damage. If Fatal, Upgrade a random card in your deck. Exhaust.";
                case "Like Water": return "At the end of your turn, if you are in Calm, gain 5 Block.";
                case "Master Reality": return "Whenever a card is created during combat, Upgrade it.";
                case "Meditate": return "Put a card from your discard pile into your hand and Retain it. Enter Calm. End your turn.";
                case "Mental Fortress": return "Whenever you change Stances, gain 4 Block.";
                case "Nirvana": return "Whenever you Scry, gain 3 Block.";
                case "Omniscience": return "Choose a card in your draw pile. Play the chosen card twice and Exhaust it. Exhaust.";
                case "Perseverance": return "Retain. Gain 5 Block. When Retained, increase its Block by 2 this combat.";
                case "Pray": return "Gain 3 Mantra. Shuffle an Insight into your draw pile.";
                case "Pressure Points": return "Apply 8 Mark. ALL enemies lose HP equal to their Mark.";
                case "Prostrate": return "Gain 2 Mantra. Gain 4 Block.";
                case "Protect": return "Retain. Gain 13 Block.";
                case "Ragnarok": return "Deal 5 damage to a random enemy 5 times.";
                case "Reach Heaven": return "Deal 10 damage. Shuffle a Through Violence into your draw pile.";
                case "Rushdown": return "Whenever you enter Wrath, draw 2 cards.";
                case "Sanctity": return "Gain 6 Block. If the last card played was a Skill, draw 2 cards.";
                case "Sands of Time": return "Retain. Deal 20 damage. When Retained, lower its cost by 1 this combat.";
                case "Sash Whip": return "Deal 8 damage. If the last card played this combat was an Attack, apply 1 Weak.";
                case "Scrawl": return "Draw cards until your hand is full. Exhaust.";
                case "Signature Move": return "Can only be played if this is the only Attack in your hand. Deal 30 damage.";
                case "Simmering Fury": return "At the start of your next turn, enter Wrath and draw 2 cards.";
                case "Spirit Shield": return "Gain 3 Block for each card in your hand.";
                case "Study": return "At the end of your turn, shuffle an Insight into your draw pile.";
                case "Swivel": return "Gain 8 Block. The next Attack you play costs 0.";
                case "Talk to the Hand": return "Deal 5 damage. Whenever you attack this enemy, gain 2 Block. Exhaust.";
                case "Tantrum": return "Deal 3 damage 3 times. Enter Wrath. Shuffle this card into your draw pile.";
                case "Third Eye": return "Gain 7 Block. Scry 3.";
                case "Tranquility": return "Retain. Enter Calm. Exhaust.";
                case "Vault": return "Take an extra turn after this one. End your turn. Exhaust.";
                case "Vigilance": return "Gain 8 Block. Enter Calm.";
                case "Wallop": return "Deal 9 damage. Gain Block equal to unblocked damage dealt.";
                case "Wave of the Hand": return "Whenever you gain Block this turn, apply 1 Weak to ALL enemies.";
                case "Weave": return "Deal 4 damage. Whenever you Scry, return this from the discard pile to your Hand.";
                case "Wheel Kick": return "Deal 15 damage. Draw 2 cards.";
                case "Windmill Strike": return "Retain. Deal 7 damage. When Retained, increase its damage by 4 this combat.";
                case "Wish": return "Choose one: Gain 6 Plated Armor, 3 Strength, or 25 Gold. Exhaust.";
                case "Worship": return "Gain 5 Mantra.";
                case "Wreath of Flame": return "Your next Attack deals 5 additional damage.";
                case "Bite": return "Deal 7 damage. Heal 2 HP.";
                case "J.A.X.": return "Lose 3 HP. Gain 2 Strength.";
                case "Shiv": return "Deal 4 damage. Exhaust.";
                case "Apotheosis": return "Upgrade ALL of your cards for the rest of combat. Exhaust.";
                case "Bandage Up": return "Heal 4 HP. Exhaust.";
                case "Blind": return "Apply 2 Weak.";
                case "Dark Shackles": return "Enemy loses 9 Strength for the rest of this turn. Exhaust.";
                case "Deep Breath": return "Shuffle your discard pile into your draw pile. Draw 1 card.";
                case "Dramatic Entrance": return "Innate. Deal 6 damage to ALL enemies. Exhaust.";
                case "Enlightenment": return "Reduce the cost of cards in your hand to 1 this turn.";
                case "Finesse": return "Gain 2 Block. Draw 1 card.";
                case "Flash of Steel": return "Deal 3 damage. Draw 1 card.";
                case "Good Instincts": return "Gain 4 Block.";
                case "Jack Of All Trades": return "Add 1 random Colorless card to your hand. Exhaust.";
                case "Madness": return "A random card in your hand costs 0 for the rest of combat. Exhaust.";
                case "Magnetism": return "At the start of each turn, add a random colorless card to your hand.";
                case "Master of Strategy": return "Draw 3 cards. Exhaust.";
                case "Mind Blast": return "Innate. Deal damage equal to the number of cards in your draw pile.";
                case "Panacea": return "Gain 1 Artifact. Exhaust.";
                case "Panache": return "Every time you play 5 cards in a single turn, deal 10 damage to ALL enemies.";
                case "Purity": return "Choose and exhaust up to 3 cards in your hand. Exhaust.";
                case "Sadistic Nature": return "Whenever you apply a Debuff to an enemy, they take 3 damage.";
                case "Secret Technique": return "Choose a Skill from your draw pile and place it into your hand. Exhaust.";
                case "Secret Weapon": return "Choose an Attack from your draw pile and place it into your hand. Exhaust.";
                case "Swift Strike": return "Deal 5 damage.";
                case "Thinking Ahead": return "Draw 2 cards. Place a card from your hand on top of your draw pile. Exhaust.";
                case "Transmutation": return "Add X random colorless cards into your hand. Exhaust.";
                case "Trip": return "Apply 2 Vulnerable.";
                case "Discovery": return "Choose 1 of 3 random cards to add to your hand. It costs 0 this turn. Exhaust.";
                case "Forethought": return "Place a card from your hand on the bottom of your draw pile. It costs 0 until it is played.";
                case "Impatience": return "If you have no Attack cards in your hand, draw 2 cards.";
                case "Panic Button": return "Gain 30 Block. You cannot gain Block from cards for the next 2 turns. Exhaust.";
                case "Chrysalis": return "Add 3 random Skills into your Draw Pile. They cost 0 this combat. Exhaust.";
                case "Hand of Greed": return "Deal 20 damage. If Fatal, gain 20 Gold.";
                case "Mayhem": return "At the start of your turn, play the top card of your draw pile.";
                case "Metamorphosis": return "Add 3 random Attacks into your Draw Pile. They cost 0 this combat. Exhaust.";
                case "The Bomb": return "At the end of 3 turns, deal 40 damage to ALL enemies.";
                case "Violence": return "Place 3 random Attack cards from your draw pile into your hand. Exhaust.";
                case "Apparition": return "Gain 1 Intangible. Exhaust. Ethereal.";
                case "Ritual Dagger": return "Deal 15 Damage. If this kills an enemy, permanently increase this card's damage by 3. Exhaust.";
                case "Beta": return "Shuffle an Omega into your draw pile. Exhaust.";
                case "Insight": return "Retain. Draw 2 cards. Exhaust.";
                case "Miracle": return "Retain. Gain 1 Energy. Exhaust.";
                case "Omega": return "At the start of your turn deal 50 damage to ALL enemies.";
                case "Safety": return "Retain. Gain 12 Block. Exhaust.";
                case "Smite": return "Retain. Deal 12 damage. Exhaust.";
                case "Through Violence": return "Retain. Deal 20 damage. Exhaust.";
                case "Ascender's Bane": return "Unplayable. Ethereal. Cannot be removed from your deck.";
                case "Clumsy": return "Unplayable. Ethereal.";
                case "Decay": return "Unplayable. At the end of your turn, take 2 damage.";
                case "Doubt": return "Unplayable. At the end of your turn, gain 1 Weak.";
                case "Injury": return "Unplayable.";
                case "Necronomicurse": return "Unplayable. There is no escape from this Curse.";
                case "Normality": return "Unplayable. You cannot play more than 3 cards this turn.";
                case "Pain": return "Unplayable. While in hand, lose 1 HP when other cards are played.";
                case "Parasite": return "Unplayable. If transformed or removed from your deck, lose 3 Max HP.";
                case "Regret": return "Unplayable. At the end of your turn, lose 1 HP for each card in your hand.";
                case "Writhe": return "Unplayable. Innate.";
                case "Shame": return "Unplayable. At the end of your turn, gain 1 Frail.";
                case "Pride": return "At the end of your turn, place a copy of this card on top of your draw pile.";
                case "Curse of the Bell": return "Unplayable. Cannot be removed from your deck.";
                case "Burn": return "Unplayable. At the end of your turn, take 2 damage.";
                case "Dazed": return "Unplayable. Ethereal.";
                case "Wound": return "Unplayable.";
                case "Slimed": return "Exhaust.";
                case "Void": return "Unplayable. Whenever this card is drawn, lose 1 Energy.";
                case "Expunger": return "Deal 9 damage X times.";
                case "Become Almighty": return "Gain 3 Strength.";
                case "Fame and Fortune": return "Gain 25 Gold.";
                case "Live Forever": return "Gain 6 Plated Armor.";
            }
		}
	}
}