using System.Text.RegularExpressions;

namespace STV
{
	public class Card: IEquatable<Card> , IComparable<Card>
	{

		// attributes
		public int FuncID { get; set; } // ID correlates to method ran (Name without spaces)
		public string Name { get; set; }
		public string Type { get; set; } // Attack, Skill, Power, Status or Curse (A,S,P,T,C)
		public string Rarity { get; set; } //Common,Uncommon,Rare (C,U,R)
		public string EnergyCost { get; set; }
		public string Description { get; set; }


		//constructors
		public Card()
        {
			this.FuncID = 0;
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
				this.FuncID = card.FuncID;
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
			return $"Name: {Name}\nEnergy Cost: {EnergyCost}\tType: {Type}\nEffect: {Description}";																	// need if conditions for X cost, type, and rarity
		}
		
        public void CardAction(Actor hero, List<Actor> encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)
		{
			Console.WriteLine($"You played {Name}.");
			int target = 0;
			int damage = 0;
			switch (Name)
			{
																									// Start of Ironclad cards
				case "Anger":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					discardPile.Add(new Card(this));
					break;
				case "Bash":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 8);
					encounter[target].AddBuff(1, 2);
					break;
				case "Bloodletting":
					hero.GainEnergy(1);
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
				case "Carnage":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					break;
				case "Cleave":
					hero.AttackAll(encounter, 8);
					break;
				case "Clothesline":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 12);
					encounter[target].AddBuff(2,2);
					break;
				case "Disarm":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(4, -2);
					break;
				case "Dropkick":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 5);
					if (encounter[target].Buffs.Contains(encounter[target].Buffs.Find(x => x.BuffID.Equals(1))))
						hero.GainEnergy(1);
					break;
				case "Entrench":
					hero.Block *= 2;
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
				case "Fiend Fire":											// Make Exhaust go from hand to exhaust Pile. Haven't finished damage calc
					target = hero.DetermineTarget(encounter);
					for (int i = hand.Count; i > 1; i--)
                    {
						if (hand[i].Equals(this))
							break;
						hand[i].Exhaust(exhaustPile);					
                    }
					hero.SingleAttack(encounter[target], 10);
					if (encounter[target].Hp <= 0)
					{
						hero.Hp += 3;
						hero.MaxHP += 3;
					}
					break;
				case "Flex":												// Debuff to lose strength at end of turn
					hero.AddBuff(4, 2);
					break;
				case "Ghostly Armor":
					hero.Block += 10;
					break;
				case "Heavy Blade":
					target = hero.DetermineTarget(encounter);
					if (hero.Buffs.Contains(hero.Buffs.Find(x => x.BuffID.Equals(4))))
						damage += (hero.Buffs.Find(x => x.BuffID.Equals(4)).Intensity.GetValueOrDefault() * 2);
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
				case "Limit Break":
					if (hero.Buffs.Contains(hero.Buffs.Find(x => x.BuffID.Equals(4))))
						hero.Buffs.Find(x => x.BuffID.Equals(4)).IntensitySet(hero.Buffs.Find(x => x.BuffID.Equals(4)).Intensity.GetValueOrDefault()*2);
					break;
				case "Offering":
					hero.SelfDamage(6);
					hero.GainEnergy(2);
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
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
				case "Reckless Charge":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					discardPile.Add(new Card(Dict.cardL[356]));
					break;
				case "Sentinel":
					hero.CardBlock(5);
					break;
				case "Seeing Red":
					hero.GainEnergy(2);
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
				case "Whirlwind":
					for(int i = 0;i < Int32.Parse(EnergyCost);i++)
						hero.AttackAll(encounter, 5);
					break;
				case "Wild Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 12);
					drawPile.Add(new Card(Dict.cardL[357]));
					STS.Shuffle(drawPile,rng);
					break;																								// Start of Silent cards
				case "Acrobatics":
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
					if (hand.Count > 1)
					STS.Discard(hand, discardPile,STS.ChooseCard(hand));
					break;
				case "Adrenaline":
					hero.GainEnergy(1);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "Alchemize":
					hero.Potions.Add(Dict.potionL[rng.Next(0,Dict.potionL.Count)]);
					break;
				case "All-Out Attack":                                                                                  //Need way for Random Discard to happen
					hero.AttackAll(encounter, 10);
					STS.Discard(hand, discardPile, hand[rng.Next(0, hand.Count)]);
					break;
				case "Backflip":
					hero.CardBlock(5);
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "Blade Dance":
					for (int i = 0; i < 3; i++)
					{
						if (hand.Count < 10)
							hand.Add(new Card(Dict.cardL[317]));
						else discardPile.Add(new Card(Dict.cardL[317]));
					}
					break;
				case "Calculated Gamble":
					int gamble = 0;
					for (gamble = 0;gamble < hand.Count; gamble++)
                    {
						STS.Discard(hand, discardPile,hand[hand.Count-1]);
                    }
					STS.DrawCards(drawPile, hand, discardPile, rng, gamble);
					break;
				case "Concentrate":
					for (int i= 0; i < 3; i++)
						STS.Discard(hand, discardPile,STS.ChooseCard(hand));
					hero.GainEnergy(2);
					break;
				case "Cloak and Dagger":
					hero.CardBlock(6);
					if (hand.Count < 10)
						hand.Add(new Card(Dict.cardL[317]));
					else discardPile.Add(new Card(Dict.cardL[317]));
					break;
				case "Dagger Spray":
					hero.AttackAll(encounter, 4);
					hero.AttackAll(encounter, 4);
					break;
				case "Dagger Throw":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 9);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					STS.Discard(hand, discardPile, STS.ChooseCard(hand));
					break;
				case "Dash":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					hero.CardBlock(10);
					break;
				case "Deflect":
					hero.CardBlock(4);
					break;
				case "Die Die Die":
					hero.AttackAll(encounter, 13);
					break;
				case "Endless Agony":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 4);
					break;
				case "Escape Plan":
					STS.DrawCards(drawPile, hand,discardPile, rng, 1);
					if (hand[hand.Count - 1].Type == "Skill")
						hero.CardBlock(3);
					break;
				case "Expertise":
					int below6 = 6 - hand.Count;
					if (below6 > 0)
					STS.DrawCards(drawPile, hand, discardPile, rng, below6);
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
				case "Heel Hook":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 5);
					if (encounter[target].Buffs.Contains(encounter[target].Buffs.Find(x => x.BuffID.Equals(2))))
						hero.GainEnergy(1);
					break;
				case "Leg Sweep":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(2, 1);
					hero.CardBlock(11);
					break;
				case "Malaise":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(4, (Int32.Parse(EnergyCost) * -1));
					encounter[target].AddBuff(2, Int32.Parse(EnergyCost));
					break;
				case "Neutralize":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);
					encounter[target].AddBuff(2, 1);
					break;
				case "Piercing Wail":                                               // Buff to gain strength at end of turn
					for (int i = 0; i < encounter.Count; i++)
					{
						encounter[i].AddBuff(4, -6);
					}
					break;
				case "Prepared":
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					if (hand.Count > 1)
					STS.Discard(hand, discardPile, STS.ChooseCard(hand));
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
				case "Skewer":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < Int32.Parse(EnergyCost); i++)
						hero.SingleAttack(encounter[target], 7);
					break;
				case "Slice":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					break;
				case "Storm of Steel":
					int storm = 0;
					for (storm = 0; storm < hand.Count; storm++)
					{
						STS.Discard(hand, discardPile,hand[hand.Count-1]);
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
					STS.Discard(hand, discardPile, STS.ChooseCard(hand));
					break;
				case "Terror":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(1, 99);
					break;
				case "Unload":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 14);
					for (int i = hand.Count; i > 0; i--)
					{
						if (hand[i-1].Type == "Attack")
							STS.Discard(hand,discardPile,hand[i-1]);
					}	
					break;
																																//DEFECT CARDS
				case "Aggregate":
					hero.GainEnergy(drawPile.Count / 4);
					break;
				case "All For One":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 10);
					foreach(Card zeroCost in discardPile)
                    {
						if (zeroCost.EnergyCost == "0" && hand.Count <10)
                        {
							hand.Add(zeroCost);
							discardPile.Remove(zeroCost);
                        }
                    }
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
				case "Capacitor":
					hero.OrbSlots += 2;
					break;
				case "Chaos":
					hero.ChannelOrb(encounter, rng.Next(0,4));
					break;
				case "Charge Battery":
					hero.CardBlock(7);
					break;																													//NEXT TURN BUFFS NEED DOING
				case "Chill":
					foreach(var e in encounter)
						hero.ChannelOrb(encounter, 1);
					break;
				case "Claw":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);																				// Add damage scaling
					break;
				case "Cold Snap":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					hero.ChannelOrb(encounter, 1);
					break;
				case "Compile Driver":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 7);
					List<Orb> distinctOrbs = new(hero.Orbs.Distinct());
					STS.DrawCards(drawPile,hand,discardPile,rng,distinctOrbs.Count);
					distinctOrbs.Clear();
					break;
				case "Consume":
					hero.AddBuff(7, 2);
					hero.OrbSlots -=1;
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
					hero.GainEnergy(hero.Energy*2);
					break;
				case "Dualcast":
					hero.Evoke(encounter);
					hero.Evoke(encounter);
					hero.Orbs.RemoveAt(0);
					break;
				case "Fission":
					int fission = 0;
					foreach(var Orb in hero.Orbs)
                    {
						hero.Orbs.Remove(Orb);
						fission++;
					}
					hero.GainEnergy(fission);
					STS.DrawCards(drawPile, hand, discardPile, rng, fission);
					break;
				case "Fusion":
					hero.ChannelOrb(encounter, 3);
					break;
				case "Glacier":
					hero.CardBlock(10);
					hero.ChannelOrb(encounter, 2);
					hero.ChannelOrb(encounter, 2);
					break;
				case "Hologram":
					hero.CardBlock(3);
					Card hologram = STS.ChooseCard(discardPile);
					hand.Add(hologram);                                                         //Add choose card function
					discardPile.Remove(hologram);
					break;
				case "Hyperbeam":
					hero.AttackAll(encounter, 26);
					hero.AddBuff(7, -3);
					break;
				case "Leap":
					hero.CardBlock(9);
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
				case "Recursion":
					hero.Evoke(encounter);
					int tmp = hero.Orbs[0].OrbID;
					hero.Orbs.RemoveAt(0);
					hero.ChannelOrb(encounter,tmp);
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
							STS.Discard(hand,discardPile,hand[hand.Count-1]);
					}
					break;
				case "Seek":					
					hand.Add(STS.ChooseCard(drawPile));
					break;
				case "Skim":
					STS.DrawCards(drawPile,hand,discardPile, rng, 3);
					break;
				case "Stack":
					hero.CardBlock(discardPile.Count);
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
						hero.GainEnergy(3);
					break;
				case "Sweeping Beam":
					hero.AttackAll(encounter, 6);
					STS.DrawCards(drawPile,hand,discardPile,rng, 1);
					break;
				case "Tempest":
					for (int i = 0; i < Int32.Parse(card.EnergyCost); i++)
						hero.ChannelOrb(encounter,0);
					break;
				case "TURBO":
					hero.GainEnergy(2);
					discardPile.Add(Dict.cardL[359]);
					break;
				case "Zap":
					hero.ChannelOrb(encounter, 0);
					break;
																														//WATCHER CARDS
				case "Alpha":
					drawPile.Add(new Card(Dict.cardL[334]));
					STS.Shuffle(drawPile,rng);
					break;
				case "Bowling Bash":
					target = hero.DetermineTarget(encounter);
					foreach(Actor a in encounter)
						hero.SingleAttack(encounter[target], 7);
					break;
				case "Carve Reality":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					hand.Add(new Card(Dict.cardL[339]));
					break;
				case "Conclude":
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
					Exhaust(exhaustPile);
					break;
				case "Defend":
					hero.CardBlock(5);
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
				case "Evaluate":
					hero.CardBlock(6);
					drawPile.Add(new Card(Dict.cardL[335]));
					STS.Shuffle(drawPile, rng);
					break;
				case "Fasting":																	//Energy calc every turn
					hero.AddBuff(4, 3);
					hero.AddBuff(9,3);
					break;
				case "Fear No Evil":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 8);
					List<string> list = new List<string>(Actor.AttackIntents());
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
				case "Judgment":
					target = hero.DetermineTarget(encounter);
					if (encounter[target].Hp <= 30)
                    {
						encounter[target].Hp = 0;
						Console.WriteLine($"The {encounter[target].Name} has been judged!");
                    }
					break;
				case "Just Lucky":
					//Scry(1);
					hero.CardBlock(2);
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 3);
					break;
				case "Mental Fortress":
					hero.AddBuff(11, 4);
					break;
				case "Omniscience":
					Card omni = new();
					do
						omni = STS.ChooseCard(drawPile);
					while (omni.Description.Contains("Unplayable"));
					omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					if (!omni.Type.Contains("Power"))
						omni.Exhaust(exhaustPile);
					drawPile.Remove(omni);
					break;
				case "Pray":
					hero.AddBuff(10, 3);
					drawPile.Add(new Card(Dict.cardL[335]));
					STS.Shuffle(drawPile, rng);
					break;
				case "Pressure Points":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(12, 8);
					for (int i = 0; i < encounter.Count; i++)
						if (encounter[i].Buffs.Contains(encounter[i].Buffs.Find(x => x.BuffID.Equals(12))))
							hero.NonAttackDamage(encounter[i], encounter[i].Buffs.Find(x => x.BuffID == 12).Intensity.GetValueOrDefault());
					break;
				case "Prostrate":
					hero.AddBuff(10, 2);
					hero.CardBlock(4);
					break;
				case "Protect":
					hero.CardBlock(12);
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
				case "Scrawl":
					STS.DrawCards(drawPile, hand, discardPile, rng, 10 - hand.Count);
					break;
				case "Signature Move":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 30);
					break;
				case "Spirit Shield":
					hero.CardBlock(hand.Count * 3);
					break;
				case "Strike":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 6);
					break;
				case "Tantrum":
					target = hero.DetermineTarget(encounter);
					for (int i = 0; i < 3; i++)
						hero.SingleAttack(encounter[target], 3);
					break;
				case "Third Eye":
					hero.CardBlock(7);
					//Scry(3);
					break;
				case "Tranquility":
					hero.SwitchStance("Calm", discardPile, hand);
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
					CardAction(wish, hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
					break;
				case "Worship":
					hero.AddBuff(10, 5);
					break;
																										// Colorless Cards
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
				case "Deep Breath":
					for (int i = discardPile.Count;i> 0;i--)
                    {
						drawPile.Add(discardPile[i - 1]);
						discardPile.RemoveAt(i-1);
					}
					STS.Shuffle(drawPile,rng);
					STS.DrawCards(drawPile, hand, discardPile, rng, 1);
					break;
				case "Dramatic Entrance":
					hero.AttackAll(encounter, 8);
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
				case "Good Instincts":
					hero.CardBlock(6);
					break;
				case "Hand of Greed":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					if (encounter[target].Hp <= 0)
						hero.Gold += 20;
					break;
				case "Impatience":
					bool noAttack = true;
					foreach(Card c in hand)
						if(c.Type == "Attack")
							noAttack = false;
					if (noAttack)
						STS.DrawCards(drawPile, hand,discardPile, rng, 2);
					break;
				case "Insight":
					STS.DrawCards(drawPile, hand, discardPile, rng, 2);
					break;
				case "J.A.X.":
					hero.SelfDamage(3);
					hero.AddBuff(4, 2);
					break;
				case "Master of Strategy":
					STS.DrawCards(drawPile, hand, discardPile, rng, 3);
					break;
				case "Mind Blast":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], drawPile.Count);
					break;
				case "Miracle":
					hero.GainEnergy(1);
					break;
				case "Panacea":
					hero.AddBuff(8, 1);
					break;
				case "Panic Button":
					hero.CardBlock(30);
					hero.AddBuff(13, 2);
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
				case "Through Violence":
					target = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[target], 20);
					break;
				case "Trip":
					target = hero.DetermineTarget(encounter);
					encounter[target].AddBuff(1, 2);
					break;
				case "Violence":
					List<Card> violenceList = new();
					foreach (Card c in drawPile)
						if (c.Type == "Attack")
							violenceList.Add(c);
					Card violence1,violence2,violence3 = new();
					for (int i = 0; i < 3; i++)
					{
						Card violence = new Card();
						violence = violenceList[rng.Next(0, violenceList.Count)];
						switch (i)
						{
							case 0: 
								violence1 = violence; 
								if (hand.Count < 10)
									hand.Add(violence1);
								else discardPile.Add(violence1);
								break;
							case 1: 
								violence2 = violence;
								if (hand.Count < 10)
									hand.Add(violence2);
								else discardPile.Add(violence2);
								break;
							case 2: 
								violence3 = violence;
								if (hand.Count < 10)
									hand.Add(violence3);
								else discardPile.Add(violence3);
								break;
						}
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
			if (Description.Contains("Exhaust"))
				Exhaust(exhaustPile);
			else if (Name == "Tantrum")
			{
				drawPile.Add(this);
				Shuffle(drawPile, rng);
			}
			else if (Type != "Power")
				discardPile.Add(this);
		}
		//misc card methods
		public void Exhaust(List<Card> exhaustPile)
        {
			exhaustPile.Add(this);
			Console.WriteLine($"The {Name} card has been exhausted.");
		}
    }
}
