namespace STV
{
	public class Actor
	{
		public string Type { get; set; }
		public string Name { get; set; }
		public int MaxHP { get; set; }
		public int Hp { get; set; }
		public int BottomHP { get; set; }
		public int TopHP { get; set; }
		public int Block { get; set; }
		public int MaxEnergy { get; set; }
		public int Energy { get; set; }
		public int Gold { get; set; }
		public int OrbSlots { get; set; }
		public string? Stance { get; set; }
		public int EnemyID { get; set; } // ID correlates to method ran (Name without spaces)
		public string? Intent { get; set; }
		public List<Buff> Buffs { get; set; }
		public List<Relic> Relics { get; set; }
		public List<Orb> Orbs { get; set; } 
		public List<Potion> Potions { get; set; }
		

		//constructor
		public Actor(int enemyID, string name, int bottomHP,int topHP, string intent)
		{
			this.Type = "Enemy";
			this.EnemyID = enemyID;
			this.Name = name;
			this.MaxHP = topHP;
			this.Hp = this.MaxHP;
			this.TopHP = topHP;
			this.BottomHP = bottomHP;
			this.Block = 0;
			this.Intent = intent;
			this.Buffs = new();				
		}
		public Actor(string name, int maxHP)
		{
			this.Type = "Hero";
			this.Name = name;
			this.MaxHP = maxHP;
			this.Hp = maxHP;
			this.MaxEnergy = 3;
			this.Energy = 3;
			this.Block = 0;
			this.Buffs = new();
			this.Relics = new();
			this.Potions = new();
			this.Orbs = new();
			this.OrbSlots = 1;
			this.Gold = 99;
		}
		
		public Actor(Actor actor)
        {
			if (actor.Type == "Enemy")
            {
				this.Type = "Enemy";
				this.EnemyID = actor.EnemyID;
				this.Name = actor.Name;
				this.MaxHP = actor.TopHP;
				this.Hp = this.MaxHP;
				this.TopHP = actor.TopHP;
				this.BottomHP = actor.BottomHP;
				this.Block = 0;
				this.Intent = actor.Intent;
				this.Buffs = new();
			}
			else
            {
				this.Type = "Hero";
				this.Name = actor.Name;
				this.MaxHP = actor.MaxHP;
				this.Hp = actor.MaxHP;
				this.MaxEnergy = 3;
				this.Energy = 3;
				this.Block = 0;
				this.Buffs = new();
				this.Relics = new();
				this.Potions = new();
				this.Orbs = new();
				this.OrbSlots = 1;
				this.Gold = 99;
			}

		}

		// Inclusive methods
		public void GainBlock(int block)
		{
			if (Buffs.Contains(Buffs.Find(x => x.Name.Equals("Frail"))))
				block = Convert.ToInt32(block * 0.75);
			Block += block;
			Console.WriteLine($"The {Name} gained {block} Block.");	
		}
		public void AddBuff(int ID)
        {
			if (!Buffs.Contains(Buffs.Find(x => x.BuffID.Equals(ID))))
				Buffs.Add(new Buff(Dict.buffL[ID]));
		}
		public void IntensityBuff(int ID, int intensity)
		{
			AddBuff(ID);
			Buffs.Find(y => y.BuffID.Equals(ID)).IntensitySet(intensity);
			Console.WriteLine($"{Name} gained {intensity} {Dict.buffL[ID].Name}!");
		}
		public void DurationDebuff(Actor target,int ID, int duration)
        {
			target.AddBuff(ID);
			target.Buffs.Find(y => y.BuffID.Equals(ID)).setDuration(duration);
			Console.WriteLine($"{Name} applied {duration} {Dict.buffL[ID].Name} to the {target.Name}!");
		}
		public void SingleAttack(Actor target,int damage)
		{
			if (Buffs.Contains(Buffs.Find(x => x.Name.Equals("Strength"))))
				damage += Buffs.Find(x => x.Name.Equals("Strength")).Intensity.GetValueOrDefault(0);
			if(Stance == "Wrath" || target.Stance == "Wrath")
				damage = damage * 2;
			if (Buffs.Contains(Buffs.Find(x => x.Name.Equals("Weak"))))
				damage = Convert.ToInt32(damage * 0.75);
			if (target.Buffs.Contains(target.Buffs.Find(x => x.Name.Equals("Vulnerable"))))
				damage = Convert.ToInt32(damage * 1.5);
			if (target.Block > 0)
			{
				target.Block -= damage;
				if (target.Block < 0)
				{
					target.Hp -= Math.Abs(target.Block);
					target.Block = 0;				
				}
			}
			else
				target.Hp -= damage;
		    
			Console.WriteLine($"{Name} attacked for {damage} damage!");
			if (target.EnemyID == 2 && target.Buffs.Contains(target.Buffs.Find(x => x.Name.Equals("Curl Up"))))      // Louse Curl Up Effect
			{
				Console.WriteLine($"The Louse has curled up and gained {target.Buffs[0].Intensity} Block!");
				target.Block += target.Buffs[0].Intensity.GetValueOrDefault();
				target.Buffs.RemoveAt(0);
			}
		}
		public void NonAttackDamage(Actor target, int damage)
        {
			if (target.Block > 0)
			{
				target.Block -= damage;
				if (target.Block < 0)
				{
					target.Hp -= Math.Abs(target.Block);
					target.Block = 0;
				}
			}
			else
				target.Hp -= damage;
		}
		public void StatusCardAdd(List<Card> drawPile, List<Card> discardPile, int cardNumber, bool inDraw)
		{
			switch (inDraw)
			{
				case true:
					drawPile.Add(new Card(Dict.cardL[cardNumber]));
					Console.WriteLine($"A {Dict.cardL[cardNumber].Name} has been added to your draw pile!");
					break;
				case false:
					discardPile.Add(new Card(Dict.cardL[cardNumber]));
					Console.WriteLine($"A {Dict.cardL[cardNumber].Name} has been added to your discard pile!");
					break;
			}
		}
		// Enemy Exclusive methods
		public void EnemyIntent(Actor enemy, int turnNumber)
		{
			Random enemyrng = new();
			switch (EnemyID)
			{
				case 0:	// Jaw Worm
					if (turnNumber == 1)
						break;
					enemy.Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 4 => "Chomp",
						int i when i >= 5 && i <= 10 => "Thrash",
						int i when i >= 11 && i <= 19 => "Bellow",
					};
					break;
				case 1:	// Cultist
					if (turnNumber == 1)
						break;
					else enemy.Intent = "Dark Strike";
					break;
				case 2:	// Louse
					enemy.Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 4 => "Grow",
						int i when i >= 5 && i <= 19 => "Bite",
					};
					break;
				case 3: // Med Acid Slime
					enemy.Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 7 => "Corrosive Spit",
						int i when i >= 8 && i <= 15 => "Tackle",
						int i when i >= 16 && i <= 19 => "Lick",
					};
					break;
				case 4: // Med Spike Slime
					enemy.Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 5 => "Flame Tackle",
						int i when i >= 6 && i <= 19 => "Lick",
					};
					break;
				case 5: // Small Acid Slime

					enemy.Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 9 => "Tackle",
						int i when i >= 10 && i <= 19 => "Lick",
					};
					break;
				case 6: // Small Spike Slime
					break;

			}
		}

		public void EnemyAction(Actor hero, Actor enemy, List<Card> drawPile, List<Card> discardPile)
		{
			switch (enemy.Intent)
			{
				case "Bellow":
					IntensityBuff(4,3);
					GainBlock(6);
					break;
				case "Bite":
					SingleAttack(hero,enemy.MaxHP / 2);
					break;
				case "Chomp":
					SingleAttack(hero, 11);
					break;
				case "Corrosive Spit":
					SingleAttack(hero, 7);
					StatusCardAdd(drawPile, discardPile, 358, false);
					break;
				case "Dark Strike":
					SingleAttack(hero, 6);
					break;
				case "Flame Tackle":
					SingleAttack(hero, 8);
					StatusCardAdd(drawPile, discardPile, 358, false);
					break;
				case "Grow":
					IntensityBuff(4, 3);
					break;
				case "Incantation":
					IntensityBuff(3, 3);
					break;
				case "Lick":
					int lick = 0;
					if (enemy.EnemyID == 3 || enemy.EnemyID == 5)
						lick = 2;
					else lick = 6;
					DurationDebuff(hero,lick, 1);
					break;
				case "Tackle":
					int damage = 0;
					switch (enemy.EnemyID)
                    {
						case 3:
							damage = 10;
							break;
						case 5:
							damage = 3;
							break;
						case 6:
							damage = 5;
							break;
                    }
					SingleAttack(hero,damage);
					break;
				case "Thrash":
					SingleAttack(hero,7);
					GainBlock(5);
					break;
			}
		}

		public int EnemyHealthSet(int bottom, int top)                                          //init enemy health upon combat start
		{
			Random r = new Random();
			int maxHP = r.Next(bottom, top + 1);
			return maxHP;
		}

		// Hero exclusive methods
		public void SwitchStance(string newStance)
		{
			string oldStance = this.Stance;
			this.Stance = newStance;
			if (oldStance != this.Stance && oldStance == "Calm")
				this.Energy += 2;
			else if (oldStance != this.Stance && this.Stance == "Divinity")
				this.Energy += 3;
		}
		public void Evoke(List<Actor> encounter)
        {
			Random random = new();
			if (this.Orbs[0] == null) return;
			else if (this.Orbs[0].Name == "Lightning")
			{
				int target = random.Next(0, encounter.Count);
				NonAttackDamage(encounter[target], 8);
				Console.WriteLine($"The {encounter[target].Name} took 8 damage from the Evoked Lightning Orb!");
			}
			else if (this.Orbs[0].Name == "Frost")
			{
				GainBlock(5);
				Console.WriteLine($"The {this.Name} gained 2 Block from the Evoked Frost Orb!");
			}
			else if (this.Orbs[0].Name == "Dark")
			{
				Actor lowestHP = encounter[0];
				foreach (var enemy in encounter)
					if (enemy.Hp < lowestHP.Hp) lowestHP = enemy;
				NonAttackDamage(lowestHP, this.Orbs[0].Effect);
				Console.WriteLine($"The Evoked Dark Orb exploded on the {lowestHP.Name} for {this.Orbs[0].Effect} damage!");
			}
			else
			{
				this.GainEnergy(2);
			}
        }
		public int DetermineTarget(List<Actor> encounter)
		{
			int x = 0;
			if (encounter.Count == 1)
				return x;
			Console.WriteLine("What enemy would you like to target?\n");
			for (int i = 0; i < encounter.Count; i++)
				Console.Write($"{i + 1}:{encounter[i].Name}\t");
			while (!Int32.TryParse(Console.ReadLine(), out x) || x < 1 || x > encounter.Count || encounter[x - 1].Hp == 0)
				Console.WriteLine("Invalid input, enter again:");
			return x - 1;
		}
		public void GainEnergy(int energy)
        {
			this.Energy += energy;
			Console.WriteLine($"The {this.Name} gained {energy} Energy!");
        }
	}
}