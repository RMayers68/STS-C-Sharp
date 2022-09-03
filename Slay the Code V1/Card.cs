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
			return $"Name: {Name}\nEnergy Cost: {EnergyCost}\tType: {Type}\n + {Description}";																	// need if conditions for X cost, type, and rarity
		}
		
        public void CardAction(Card card, Actor hero, List<Actor> encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile)
		{
			switch (card.Name)
			{
				case "Bash":
					int bash = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[bash], 8);
					hero.DurationDebuff(encounter[bash],1, 2);
					break;
				case "Neutralize":
					int neutralize = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[neutralize], 3);
					hero.DurationDebuff(encounter[neutralize],2, 1);
					break;
				case "Survivor":
					hero.GainBlock(8);
					if (hand.Count > 1)
                    {
						STS.Discard(hand,discardPile);
					}
					break;
				case "Dualcast":
					hero.Evoke(encounter);
					hero.Evoke(encounter);
					hero.Orbs.RemoveAt(0);
					break;
				case "Zap":
					if (hero.Orbs.Count == hero.OrbSlots)
                    {
						hero.Evoke(encounter);
						hero.Orbs.RemoveAt(0);
					}						
					hero.Orbs.Add(new Orb(Dict.orbL[0]));
					break;
				case "Defend":
					hero.GainBlock(5);
					break;
				case "Strike":
					int strike = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[strike], 6);
					break;
				case "Eruption":
					int eruption = hero.DetermineTarget(encounter);
					hero.SingleAttack(encounter[eruption], 9);
					hero.SwitchStance("Wrath");
					break;
				case "Vigilance":
					hero.GainBlock(8);
					hero.SwitchStance("Calm");
					break;
				case "Miracle":
					hero.Energy += 1;
					ExhaustCard(card, exhaustPile, discardPile);
					break;
				case "Slimed":
					ExhaustCard(card, exhaustPile, discardPile);
					break;
			}
		}


		public void ExhaustCard(Card card, List<Card> exhaustPile, List<Card> discardPile)
        {
			exhaustPile.Add(card);
			discardPile.Remove(card);
			Console.WriteLine($"The {card.Name} card has been exhausted.");
		}
    }
}