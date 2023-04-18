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
        public List<string> Actions { get; set; }


        //constructor
        public Actor() { }


		// Inclusive methods
		public void CardBlock(int block)
		{
			if (FindBuff("No Block") != null || block == 0)
				return;
			GainBlock(block);
		}
		public void GainBlock(int block)
		{
			if (Hp <= 0) return;
			if (FindBuff("Dexterity") != null)
				block += FindBuff("Dexterity").Intensity;
			if (Buffs.Find(x => x.Name == "Frail") != null)
				block = Convert.ToInt32(block * 0.75);
			Block += block;
			Console.WriteLine($"The {Name} gained {block} Block.");
		}

		public void AddBuff(int ID, int effect)
		{
			if (effect == 0) return;
            // Artifact stopping debuffs
            if ((!Dict.buffL[ID].BuffDebuff) && FindBuff("Artifact") != null)
			{
                FindBuff("Artifact").Counter--;
				return;
			}

			// If buff is not there, add it to target's list
			if (FindBuff(Dict.buffL[ID].Name) == null)
				Buffs.Add(new Buff(Dict.buffL[ID]));
			Buff buff = FindBuff(Dict.buffL[ID].Name); 
			// Add attributes based on type of Buff
			byte b = Dict.buffL[ID].Type switch
			{
				byte i when i == 1 =>  1,
				byte i when i >= 2 && i <= 3 => 2,
				byte i when i == 4 => 3,
			};
			switch (b)
			{
                default:
                    return;
                case 1: //Duration
					buff.DurationSet(effect);
					Console.WriteLine($"{Name} is now {buff.Name} for {buff.Duration} turns!");
					break;
				case 2: //Intensity
					buff.IntensitySet(effect);
					Console.WriteLine($"{Name} gained {effect} {buff.Name}!");
					break;
				case 3: //Counter
					buff.CounterSet(effect);
					Console.WriteLine($"{Name}'s {buff.Name} is now at {buff.Counter}!");
					if (buff.Name == "Mantra")
						Actions.Add($"Mantra Gained: {effect}");
					break;
			}
		}

        public Buff FindBuff(string name)
        {
            return Buffs.Find(x => x.Name == name);
        }

        //HP Altering Methods
        public void SingleAttack(Actor target, int damage)
		{
			if (Hp <= 0) return;
			if (FindBuff("Strength") != null)
				damage += FindBuff("Strength").Intensity;
            if (Stance == "Wrath" || target.Stance == "Wrath")
                damage *= 2;
			if (FindBuff("Weak") != null)
				damage = Convert.ToInt32(damage * 0.75);
			if (FindBuff("Vulnerable") != null)
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
            if (target.FindBuff("Curl Up") is Buff curlUp && curlUp != null)      // Louse Curl Up Effect
            {
                Console.WriteLine($"The Louse has curled up and gained {curlUp.Intensity} Block!");
                target.Block += curlUp.Intensity;
                target.Buffs.Remove(curlUp);
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
		
		public void PoisonDamage(int damage)
		{
			Hp -= damage;
			FindBuff("Poison").Duration -= 1;
		}
		
	}
}