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
		public List<string> Actions { get; set; }
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
			this.Gold = 0;
			this.Actions = new();
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
			this.Actions = new();
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
				this.Gold = 0;
				this.Actions = new();
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
				this.Actions = new();
			}

		}

		// Inclusive methods
		public void CardBlock(int block)
		{
			if (Buffs.Contains(Buffs.Find(x => x.Name == "No Block")))
				return;
			GainBlock(block);
		}
		public void GainBlock(int block)
        {
			if (Hp <= 0) return;
			if (Buffs.Contains(Buffs.Find(x =>x.Name == "Dexterity")))
				block += Buffs.Find(x =>x.Name == "Dexterity").Intensity.Value;
			if (Buffs.Contains(Buffs.Find(x => x.Name == "Frail")))
				block = Convert.ToInt32(block * 0.75);
			Block += block;
			Console.WriteLine($"The {Name} gained {block} Block.");
		}
		public void AddBuff(int ID,int effect)
        {
			if ((!Dict.buffL[ID].BuffDebuff || effect < 0) && Buffs.Contains(Buffs.Find(x => x.Name == "Artifact")))
            {
				Buffs.Find(x => x.Name == "Artifact").Counter--;
				return;
			}				
			if (!Buffs.Contains(Buffs.Find(x => x.BuffID.Equals(ID))))
				Buffs.Add(new Buff(Dict.buffL[ID]));
			switch (Dict.buffL[ID].Type)
            {
				case 1:
					Buffs.Find(y => y.BuffID.Equals(ID)).DurationSet(effect);
					Console.WriteLine($"{Name} is now {Dict.buffL[ID].Name} for {effect} turns!");
					break;
				case 2:
					Buffs.Find(y => y.BuffID.Equals(ID)).IntensitySet(effect);
					Console.WriteLine($"{Name} gained {effect} {Dict.buffL[ID].Name}!");
					break;
				case 3:
					Buffs.Find(y => y.BuffID.Equals(ID)).CounterSet(effect);
					Console.WriteLine($"{Name}'s {Dict.buffL[ID].Name} is now at {effect}!");
					break;
				default:
					return;
			}
		}
		public void DamageEffectChecks(Actor target, int damage)
        {
			if (target.Buffs.Exists(x => x.Name == "Mode Shift"))
            {
				target.Buffs.Find(x => x.Name == "Mode Shift").IntensitySet(-damage);
				if (target.Buffs.Find(x => x.Name == "Mode Shift").Intensity == 0)
                {
					target.Buffs.RemoveAll(x => x.Name == "Mode Shift");
					Console.WriteLine("The Guardian has shifted into Defensive Mode!");
					target.GainBlock(20);
					target.Intent = "Defensive Mode";
				}					
			}							
		}
		//HP Altering Methods
		public void SingleAttack(Actor target,int damage)
		{
			if(Hp <= 0) return;			
			if (Buffs.Exists(x => x.Name == "Strength"))
				damage += Buffs.Find(x => x.Name == "Strength").Intensity.Value;
			if(Stance == "Wrath" || target.Stance == "Wrath")
				damage = damage * 2;
			if (Stance == "Divinity")
				damage = damage * 3;
			if (Buffs.Exists(x => x.Name == "Weak"))
				damage = Convert.ToInt32(damage * 0.75);
			if (target.Buffs.Exists(x => x.Name == "Vulnerable"))
				damage = Convert.ToInt32(damage * 1.5);
			if (target.Buffs.Exists(x => x.Name == "Intangible"))
				damage = 1;
			if (target.Block > damage)
			{
				Console.WriteLine($"{target.Name} has blocked {damage}, leaving itself with {target.Block} Block!");
				if (target.Buffs.Exists(x => x.Name == "Thorns"))
					NonAttackDamage(this, target.Buffs.Find(x => x.Name == "Thorns").Intensity.Value);
				return;
			}
			else
			{
				damage = Math.Abs(target.Block);
				target.Block = 0;
			}
			target.Hp -= damage;		    
			Console.WriteLine($"{Name} attacked the {target.Name} for {damage} damage!");
			if (target.Buffs.Exists(x => x.Name == "Curl Up"))							      // Louse Curl Up Effect
			{
				Console.WriteLine($"The Louse has curled up and gained {target.Buffs[0].Intensity} Block!");
				target.Block += target.Buffs[0].Intensity.Value;
				target.Buffs.RemoveAt(0);
			}
			if (target.Buffs.Exists(x => x.Name == "Plated Armor"))
				target.Buffs.Find(x => x.Name == "Plated Armor").IntensitySet(-1);
			if (target.Buffs.Exists(x => x.Name == "Thorns"))
				NonAttackDamage(this, target.Buffs.Find(x => x.Name == "Thorns").Intensity.Value);
			DamageEffectChecks(target, damage);
		}
		public void AttackAll( List<Actor> encounter, int damage)
        {
			{
				if (Hp <= 0) return;
				foreach (var target in encounter)
                {
					SingleAttack(target, damage);
				}				
			}
		}
		public void NonAttackDamage(Actor target, int damage)
        {
			if (Hp <= 0) return;
			if (target.Buffs.Exists(x => x.Name == "Intangible"))
				damage = 1;
			if (target.Block > 0)
			{
				target.Block -= damage;
				if (target.Block > damage)
                {
					Console.WriteLine($"{target.Name} has blocked {damage}, leaving itself with {target.Block} Block!");
					return;
				}					
				else
				{
					damage = Math.Abs(target.Block);
					target.Block = 0;
				}
			}
			target.Hp -= damage;
			Console.WriteLine($"{Name} inflicted the {target.Name} with {damage} damage!");
			DamageEffectChecks(target,damage);
		}
		public void PoisonDamage()
		{
			int damage = Buffs.Find(x => x.Name == "Poison").Duration.Value;
			if (Hp <= 0) return;
			if (Buffs.Exists(x => x.Name == "Intangible"))
				damage = 1;
			Hp -= damage;
			Console.WriteLine($"{Name} suffered {damage} poison damage!");
			DamageEffectChecks(this, damage);
		}
		public void SelfDamage(int damage)
        {
			if (Hp <= 0) return;
			if (Buffs.Exists(x => x.Name == "Intangible"))
				damage = 1;
			this.Hp -= damage;
			Console.WriteLine($"{Name} hurt themselves for {damage} damage!");
        }
		public void HealHP(int heal)
        {
			Hp += heal;
			if (Hp > MaxHP)
				Hp = MaxHP;
			Console.WriteLine($"You have healed {heal} HP and are now at {Hp}/{MaxHP}!");
		}

		// Enemy Exclusive methods
		public void EnemyIntent(int turnNumber, List<Actor> encounter)
		{
			Random enemyrng = new();
			switch (EnemyID)
			{
				default:															// Any enemy who only uses one Intent
					break;
				case 0:																// Jaw Worm
					if (turnNumber == 1)
						break;
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 4 => "Chomp",
						int i when i >= 5 && i <= 10 => "Thrash",
						int i when i >= 11 && i <= 19 => "Bellow",
					};
					Repeat3Prevent("Chomp", "Bellow", "Thrash");
					break;
				case 1:																// Cultist
					if (turnNumber == 1)
						break;
					else Intent = "Dark Strike";
					break;
				case 2:																// Red Louse
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 4 => "Grow",
						int i when i >= 5 && i <= 19 => "Bite",
					};
					Repeat3Prevent("Bite", "Grow");
					break;
				case 3:																// Med Acid Slime
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 7 => "Corrosive Spit",
						int i when i >= 8 && i <= 15 => "Tackle",
						int i when i >= 16 && i <= 19 => "Lick",
					};
					Repeat3Prevent("Corrosive Spit", "Tackle", "Lick");
					break;
				case 4:																// Med Spike Slime
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 5 => "Flame Tackle",
						int i when i >= 6 && i <= 19 => "Lick",
					};
					Repeat3Prevent("Flame Tackle", "Lick");
					break;
				case 5:																// Small Acid Slime

					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 9 => "Tackle",
						int i when i >= 10 && i <= 19 => "Lick",
					};
					Repeat3Prevent("Tackle", "Lick");
					break;
				case 7:																// Green Louse
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 4 => "Web Spit",
						int i when i >= 5 && i <= 19 => "Bite",
					};
					Repeat3Prevent("Bite", "Web Spit");
					break;
				case 8:																//Blue Slaver
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 7 => "Rake",
						int i when i >= 8 && i <= 19 => "Stab",
					};
					Repeat3Prevent("Stab", "Rake");
					break;
				case 9:																//Red Slaver
					if (turnNumber == 1)
						break;
					if (!Actions.Contains("Entangle"))
						Intent = enemyrng.Next(0, 20) switch
						{
							int i when i >= 0 && i <= 4 => "Entangle",
							int i when i >= 5 && i <= 19 => "Determine",
						};
					else Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 10 => "Scrape",
						int i when i >= 11 && i <= 19 => "Stab",
					};
					if (Intent == "Determine")
                    {
						if (Actions != null && Actions.Count % 3 == 0)
							Intent = "Stab";
						else Intent = "Scrape";
                    }
					if (Actions != null && Actions.Count >= 2)
						Repeat3Prevent("Stab", "Scrape");
					break;
				case 10:															//Fungi Beast
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 7 => "Grow",
						int i when i >= 8 && i <= 19 => "Bite",
					};
					Repeat3Prevent("Bite", "Grow");
					break;
				case 11:															//Looters
					if (turnNumber == 1 || turnNumber == 2)
						break;
					else if (turnNumber == 3)
						Intent = enemyrng.Next(0, 20) switch
						{
							int i when i >= 0 && i <= 7 => "Lunge",
							int i when i >= 8 && i <= 19 => "Smoke Bomb",
						};
					if (Actions != null && Actions.Count >= 3)
						if (Actions[Actions.Count - 1] == "Lunge")
							Intent = "Smoke Bomb";
						else Intent = "Escape";
					break;
				case 14:															//Gremlin Wizard
					if (Actions != null && Actions.Count % 3 == 0)
						Intent = "Ultimate Blast";
					else Intent = "Charging";
					break;
				case 16:															//Shield Gremlin
					bool targetExists = false;
					for (int i = 0; i < encounter.Count; i++)
						if (encounter[i].Hp != 0 && encounter[i] != this)
							targetExists = true;
					if (targetExists)
						Intent = "Protect";
					else Intent = "Shield Bash";
					break;
				case 17:															//Gremlin Nob
					if (turnNumber == 1)
						break;
					Intent = enemyrng.Next(0, 21) switch
					{
						int i when i >= 0 && i <= 6 => "Skull Bash",
						int i when i >= 7 && i <= 20 => "Rush",
					};
					Repeat3Prevent("Skull Bash", "Rush");
					break;
				case 18:                                                            // Lagavulin
					if (Buffs.Exists(x => x.Name == "Asleep"))
						if (Hp < MaxHP)
						{
							Console.WriteLine("Lagavulin has been startled by your attack and awakens!");
							Buffs.Remove(Buffs.Find((x => x.Name == "Asleep")));
							Buffs.Remove(Buffs.Find((x => x.Name == "Metallicize")));
							break;
						}
					else if (Buffs.Exists(x => x.Name == "Asleep") && Buffs.Find((x => x.Name == "Asleep")).Duration == 1)
                        {
							Console.WriteLine("Lagavulin is stirring and will wake up soon...");
							Buffs.Remove(Buffs.Find((x => x.Name == "Metallicize")));
							break;
                        }
					else if (Actions.Count >= 3 && Actions[Actions.Count - 1] == "Attack" && Actions[Actions.Count - 2] == "Attack")
						Intent = "Siphon Soul";
					else Intent = "Attack";
					break;
				case 19:															// Sentry
					if (turnNumber == 1 && encounter.Count == 3)
                    {
						encounter[1].Intent = "Beam";
						break;
					}
					if (Intent == "Bolt")
						Intent = "Beam";
					else Intent = "Bolt";
					break;

				case 20:															// Large Acid Slime
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 7 => "Corrosive Spit",
						int i when i >= 8 && i <= 15 => "Tackle",
						int i when i >= 16 && i <= 19 => "Lick",
					};
					Repeat3Prevent("Corrosive Spit", "Tackle", "Lick");
					break;
				case 21:															// Large Spike Slime
					Intent = enemyrng.Next(0, 20) switch
					{
						int i when i >= 0 && i <= 5 => "Flame Tackle",
						int i when i >= 6 && i <= 19 => "Lick",
					};
					Repeat3Prevent("Flame Tackle", "Lick");
					break;
				case 22:                                                            // Slime Boss
					Intent = (Actions.Count % 3) switch
					{
						int i when i == 0 => "Goop Spray",
						int i when i == 1 => "Charging",
						int i when i == 3 => "Slam",
					};
					break;
				case 23:															// The Guardian
					if (Buffs.Contains(Buffs.Find(x => x.Name == "Mode Shift")))
                    {
						Intent = (Actions.Count % 4) switch
						{
							int i when i == 0  => "Charging Up",
							int i when i == 1 => "Fierce Bash",
							int i when i == 2 => "Vent Steam",
							int i when i == 3 => "Whirlwind",
						};
					}					
					else if (Actions.Count != 0)
					{
						if (Actions[Actions.Count - 1] == "Roll Attack")
							Intent = "Twin Slam";
						else if (Actions[Actions.Count - 1] == "Defensive Mode")
							Intent = "Roll Attack";
					}
					else Intent = "Defensive Mode";
					break;
				case 24:															// Hexaghost
					if (turnNumber == 1)
						break;
					if (turnNumber == 2)
                    {
						Intent = "Divider";
						break;
					}
					Intent = ((Actions.Count - 2) % 7) switch
					{
						int i when i == 0 || i == 2 || i == 5 => "Sear",
						int i when i == 1 || i == 4 => "Slice",
						int i when i == 3 => "Inflame",
						int i when i ==6 => "Inferno",
					};
					break;
			}
		}


		public void Repeat3Prevent(string one, string two)
        {
			if (Actions != null && Actions.Count >= 2)
				while (Actions[Actions.Count - 1] == Actions[Actions.Count - 2] && Intent == Actions[Actions.Count - 1])
				{
					if (Intent == one)
						Intent = two;
					else Intent = one;
				}
		}
		public void Repeat3Prevent(string one, string two,string three)
		{
			if (Actions != null && Actions.Count >= 2)
				while (Actions[Actions.Count - 1] == Actions[Actions.Count - 2] && Intent == Actions[Actions.Count - 1])
				{
					if (Intent == one)
						Intent = two;
					else if (Intent == two)
						Intent = three;
					else Intent = one;
				}
		}
		public void EnemyAction(Actor hero,List<Card> drawPile, List<Card> discardPile, List<Actor> encounter)
		{
			int damage = 0;
			int target = 0;
			Random rng = new Random();
			switch (Intent)
			{
				case "Attack":
					SingleAttack(hero, 18);
					break;
				case "Beam":
					SingleAttack(hero, 9);
					break;
				case "Bellow":
					if (EnemyID == 17)
						AddBuff(19, 2);
					else
                    {
						AddBuff(4, 3);
						GainBlock(6);
					}				
					break;
				case "Bite":
					if (EnemyID == 10)
						SingleAttack(hero, MaxHP / 2); 
					else SingleAttack(hero, 6);
					break;
				case "Bolt":
					for (int i = 0; i < 2; i++)
						discardPile.Add(new Card(Dict.cardL[356]));
					Console.WriteLine($"{Name} has added 2 Dazed cards to your deck!");
					break;
				case "Charging":
					Console.WriteLine($"{Name} is charging up!");
					break;
				case "Charging Up":
					GainBlock(9);
					break;
				case "Chomp":
					SingleAttack(hero, 11);
					break;
				case "Corrosive Spit":
					SingleAttack(hero, 7);
					discardPile.Add(Dict.cardL[358]);
					if (EnemyID == 20)
						discardPile.Add(Dict.cardL[358]);
					break;
				case "Dark Strike":
					SingleAttack(hero, 6);
					break;
				case "Defensive Mode":
					AddBuff(17, 3);
					break;
				case "Divider":
					damage = hero.Hp / 12 + 1;
					for (int i = 0; i < 6; i++)
					SingleAttack(hero, damage);
					break;
				case "Entangle":
					hero.AddBuff(14, 2);
					break;
				case "Escape":
					encounter.Remove(this);
					Console.WriteLine($"The {Name} has escaped!");
					STS.Pause();
					break;
				case "Fierce Bash":
					SingleAttack(hero, 32);
					break;
				case "Flame Tackle":
					if (EnemyID == 21)
						damage = 16;
					else damage = 8;
					SingleAttack(hero, damage);
					discardPile.Add(Dict.cardL[358]);
					if (EnemyID == 21)
						discardPile.Add(Dict.cardL[358]);
					break;
				case "Goop Spray":
					for (int i = 0; i < 3; i++)
						discardPile.Add(new Card(Dict.cardL[358]));
					Console.WriteLine($"{Name} has added 3 Slimed cards into your Deck! Ewww!");
					break;
				case "Grow":
					AddBuff(4, 3);
					break;
				case "Incantation":
					AddBuff(3, 3);
					break;
				case "Inferno":
					for (int i = 0; i < 6; i++)
                    {
						SingleAttack(hero, 2);
						if (i % 2 == 0)
							discardPile.Add(new Card(Dict.cardL[355]));
					}
					Console.WriteLine($"{Name} has added 3 Burns to your Deck!");
					break;
				case "Inflame":
					AddBuff(4, 2);
					GainBlock(12);
					break;
				case "Lick":
					int li = 0;
					int ck = 2;
					if (EnemyID == 3 || EnemyID == 5 || EnemyID == 20)
						li = 2;
					else li = 6;
					if (EnemyID == 20 || EnemyID == 21)
						ck++;
					hero.AddBuff(li,ck);
					break;
				case "Lunge":
					SingleAttack(hero, 12);
					hero.Gold -= Buffs.Find(x => x.Name == "Thievery").Intensity.Value;
					Gold += Buffs.Find(x => x.Name == "Thievery").Intensity.Value;
					Console.WriteLine($"The {Name} stole 15 Gold!");
					break;
				case "Mug":
					SingleAttack(hero, 10);
					hero.Gold -= Buffs.Find(x => x.Name == "Thievery").Intensity.Value;
					Gold += Buffs.Find(x => x.Name == "Thievery").Intensity.Value;
					Console.WriteLine($"The {Name} stole 15 Gold!");
					break;
				case "Protect":
					while (target != encounter.FindIndex(x => x == this))
						target = rng.Next(0, encounter.Count);
					if (encounter[target].Hp == 0)
						target = encounter.FindIndex(x => x == this);
					encounter[target].GainBlock(7);
					break;
				case "Puncture":
					SingleAttack(hero, 9);
					break;
				case "Rake":
					SingleAttack(hero, 7);
					hero.AddBuff(2, 2);
					break;
				case "Roll Attack":
					SingleAttack(hero, 9);
					break;
				case "Rush":
					SingleAttack(hero, 14);
					break;
				case "Scrape":
					SingleAttack(hero, 8);
					hero.AddBuff(1, 2);
					break;
				case "Scratch":
					SingleAttack(hero, 4);
					break;
				case "Shield Bash":
					SingleAttack(hero, 6);
					break;
				case "Sear":
					SingleAttack(hero, 6);
					discardPile.Add(new Card(Dict.cardL[355]));
					Console.WriteLine($"{Name} has added a Burn to your Deck!");
					break;
				case "Siphon Soul":
					hero.AddBuff(9, -1);
					hero.AddBuff(4, -1);
					break;
				case "Skull Bash":
					SingleAttack(hero, 6);
					hero.AddBuff(1, 3);
					break;
				case "Slam":
					SingleAttack(hero, 35);
					break;
				case "Sleeping":
					Console.WriteLine($"{Name} is sleeping, be cautious on waking it...");
					break;
				case "Smash":
					SingleAttack(hero, 4);
					hero.AddBuff(2, 2);
					break;
				case "Smoke Bomb":
					GainBlock(6);
					break;
				case "Stab":
					switch(EnemyID)
                    {
						case 8:
							damage = 10;
							break;
						case 9:
							damage = 13;
							break;
                    }
					SingleAttack(hero, damage);
					break;
				case "Tackle":
					switch (EnemyID)
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
						case 21:
							damage = 16;
							break;
						case 24:
							damage = 5;
							SingleAttack(hero,damage);
							break;
                    }
					SingleAttack(hero, damage);
					break;
				case "Thrash":
					SingleAttack(hero,7);
					GainBlock(5);
					break;
				case "Twin Slam":
					for (int i = 0;i < 2; i++)
						SingleAttack(hero, 8);
					Buffs.RemoveAll(x => x.Name == "Thorns");
					AddBuff(16, 30);
					Actions.Clear();
					break;
				case "Ultimate Blast":
					SingleAttack(hero, 25);
					break;
				case "Vent Steam":
					hero.AddBuff(1, 3);
					hero.AddBuff(2, 3);
					break;
				case "Web Spit":
					hero.AddBuff(2, 3);
					break;
				case "Whirlwind":
					for (int i = 0; i < 4; i++)
						SingleAttack(hero, 5);
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
		public void SwitchStance(string newStance, List<Card> discardPile, List<Card> hand)
		{
			string oldStance = Stance;
			Stance = newStance;
			if(oldStance != Stance)
                switch (Stance)
                {
					default:
						Console.WriteLine($"{Name} has switched to {Stance} Stance.");
						break;
					case "None":
						Console.WriteLine($"{Name} has left {oldStance} Stance.");
						break;
				}
			if (Buffs.Contains(Buffs.Find(x => x.BuffID.Equals(11))))							// Mental Fortress Check
				GainBlock(Buffs.Find(x => x.BuffID.Equals(11)).Intensity.Value);
			if (oldStance != Stance && oldStance == "Calm")
				Energy += 2;
			else if (oldStance != Stance && Stance == "Divinity")
				Energy += 3;
			for (int i = discardPile.Count; i > 0; i--)											// Flurry of Blows Check
            {
				if (discardPile[i - 1].FuncID == 246 && hand.Count < 10)
                {
					hand.Add(discardPile[i - 1]);
					discardPile.Remove(discardPile[i - 1]);
				}					
            }
		}

		public void ChannelOrb(List<Actor> encounter, int orbID)
		{
			if (Hp <= 0) return;
			if (Orbs.Count == OrbSlots)
			{
				Evoke(encounter);
				Orbs.RemoveAt(0);
			}
			Orbs.Add(new Orb(Dict.orbL[orbID]));
		}
		public void Evoke(List<Actor> encounter)
        {
			if (Hp <= 0) return;
			Random random = new();
			int focus = 0;
			if (Buffs.Exists(x => x.Name == "Focus")) ;
				focus = Buffs.Find(x => x.Name == "Focus").Intensity.Value;
			if (Orbs[0] == null) return;
			else if (Orbs[0].Name == "Lightning")
			{
				int target = random.Next(0, encounter.Count);
				NonAttackDamage(encounter[target], focus+8);
				Console.WriteLine($"The {encounter[target].Name} took {focus+8} damage from the Evoked Lightning Orb!");
			}
			else if (Orbs[0].Name == "Frost")
			{
				GainBlock(focus+5);
				Console.WriteLine($"The {Name} gained {focus} Block from the Evoked Frost Orb!");
			}
			else if (Orbs[0].Name == "Dark")
			{
				Actor lowestHP = encounter[0];
				foreach (var enemy in encounter)
					if (enemy.Hp < lowestHP.Hp) lowestHP = enemy;
				NonAttackDamage(lowestHP, Orbs[0].Effect);
				Console.WriteLine($"The Evoked Dark Orb exploded on the {lowestHP.Name} for {Orbs[0].Effect} damage!");
			}
			else
			{
				GainEnergy(2);
			}
        }
		public int DetermineTarget(List<Actor> encounter)
		{
			int x = 0;
			if (encounter.Count == 1 || Hp == 0)
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
			Console.WriteLine($"The {Name} gained {energy} Energy!");
		}

		public void GoldChange(int amount) //For when rewards are set
        {

        }
		//enemy attack intents list
		public static List<string> AttackIntents()
        {
			List<string> list = new List<string>();
			list.Add("Attack");
			list.Add("Beam");
			list.Add("Bite");
			list.Add("Chomp");
			list.Add("Corrosive Spit");
			list.Add("Dark Strike");
			list.Add("Divider");
			list.Add("Fierce Bash");
			list.Add("Flame Tackle");
			list.Add("Inferno");
			list.Add("Lunge");
			list.Add("Mug");
			list.Add("Puncture");
			list.Add("Rake");
			list.Add("Roll Attack");
			list.Add("Rush");
			list.Add("Scrape");
			list.Add("Scratch");
			list.Add("Shield Bash");
			list.Add("Sear");
			list.Add("Skull Bash");
			list.Add("Slam");
			list.Add("Smash");
			list.Add("Stab");
			list.Add("Tackle");
			list.Add("Thrash");
			list.Add("Twin Slam");
			list.Add("Ultimate Blast");
			list.Add("Whirlwind");
			return list;
		}

	}
}
