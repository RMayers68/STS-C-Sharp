namespace STV
{
	public class Actor // base class
	{
		//public string Type { get; set; }
		public string Name { get; set; }
		public int MaxHP { get; set; }
		public int Hp { get; set; }
		public int BottomHP { get; set; }
		public int TopHP { get; set; }
		public int Block { get; set; }
        public int Gold { get; set; }
        public string? Stance { get; set; }
        public List<Buff> Buffs { get; set; }



		//constructor
		public Actor() { }


		// Inclusive methods
		public void CardBlock(int block)
		{
			if (Buffs.Find(x => x.Name == "No Block") != null)
				return;
			GainBlock(block);
		}
		public void GainBlock(int block)
		{
			if (Hp <= 0) return;
			if (Buffs.Find(x => x.Name == "Dexterity") != null)
				block += Buffs.Find(x => x.Name == "Dexterity").Intensity.GetValueOrDefault();
			if (Buffs.Find(x => x.Name == "Frail") != null)
				block = Convert.ToInt32(block * 0.75);
			Block += block;
			Console.WriteLine($"The {Name} gained {block} Block.");
		}
		public void AddBuff(int ID, int effect)
		{
			if ((!Dict.buffL[ID].BuffDebuff || effect < 0) && Buffs.Find(x => x.Name == "Artifact") != null)
			{
				Buffs.Find(x => x.Name == "Artifact").Counter--;
				return;
			}
			if (Buffs.Find(x => x.BuffID.Equals(ID)) == null)
				Buffs.Add(new Buff(Dict.buffL[ID]));
			byte b = Dict.buffL[ID].Type switch
			{
				byte i when i >= 0 && i <= 1 => 1,
				byte i when i >= 2 && i <= 4 => 2,
				byte i when i >= 5 && i <= 5 => 3,
			};
			switch (b)
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

		//HP Altering Methods
		public void SingleAttack(Actor target, int damage)
		{
			if (Hp <= 0) return;
			if (Buffs.Find(x => x.Name.Equals("Strength")) != null)
				damage += Buffs.Find(x => x.Name.Equals("Strength")).Intensity.GetValueOrDefault(0);
            if (Stance == "Wrath" || target.Stance == "Wrath")
                damage *= 2;
			if (Buffs.Find(x => x.Name.Equals("Weak")) != null)
				damage = Convert.ToInt32(damage * 0.75);
			if (target.Buffs.Find(x => x.Name.Equals("Vulnerable")) != null)
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
			if (target.Buffs.Find(x => x.Name.Equals("Curl Up")) != null)      // Louse Curl Up Effect
			{
				Console.WriteLine($"The Louse has curled up and gained {target.Buffs[0].Intensity} Block!");
				target.Block += target.Buffs[0].Intensity.GetValueOrDefault();
				target.Buffs.RemoveAt(0);
			}
		}

		public void NonAttackDamage(Actor target, int damage)
		{
			if (Hp <= 0) return;
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
		
		public void HealHP(int heal)
		{
			Hp += heal;
			Console.WriteLine($"You have healed {heal} HP and are now at {Hp}/{MaxHP}!");
		}	
	}

	
}