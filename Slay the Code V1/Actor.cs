namespace STV
{
	public class Actor // base class
	{
		//public string Type { get; set; }
		public string Name { get; set; }
		public int MaxHP { get; set; }
		public int Hp { get; set; }
		public int Block { get; set; }
        public int Gold { get; set; }
        public string? Stance { get; set; }
        public List<Buff> Buffs { get; set; }
        public List<Relic> Relics { get; set; }
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

		public void AddBuff(int ID, int effect, Hero hero = null)
		{
			if ((ID == 2 && HasRelic("Ginger")) || (ID == 6 && HasRelic("Turnip")))
				return;
			if (effect == 0) return;
            // Artifact stopping debuffs
            if ((!Dict.buffL[ID].BuffDebuff) && HasRelic("Artifact"))
			{
                FindBuff("Artifact").Counter--;
				return;
			}

			// If buff is not there, add it to target's list
			if (FindBuff(Dict.buffL[ID].Name) == null)
				Buffs.Add(new Buff(Dict.buffL[ID]));
			Buff buff = FindBuff(Dict.buffL[ID].Name);

			// Misc checks
			if (buff.Name == "Poison" && hero.HasRelic("Snecko Skull"))
				effect++;
			if (buff.Name == "Vulnerable" && hero.HasRelic("Champion"))
				AddBuff(2, 1);
			// Add attributes based on type of Buff
			byte b = Dict.buffL[ID].Type switch
			{
				byte i when i == 1 =>  1,
				byte i when i >= 2 && i <= 3 => 2,
				_ => 3,
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
						AddAction($"Mantra Gained: {effect}");
					break;
			}
			if (hero != null && hero.FindBuff("Sadistic") is Buff sadistic && !sadistic.BuffDebuff)
				NonAttackDamage(this, sadistic.Intensity,sadistic.Name);
		}

        public Buff FindBuff(string name)
        {
            return Buffs.Find(x => x.Name.Contains(name));
        }

        //HP Altering Methods
        public void Attack(Actor target, int damage)
		{
			if (Hp <= 0) return;
			if (FindBuff("Strength") is Buff strength && strength != null)
				damage += strength.Intensity;
            if (Stance == "Wrath" || target.Stance == "Wrath")
                damage *= 2;
			if (FindBuff("Weak") != null)
			{
				if (!target.HasRelic("Krane"))
					damage = Convert.ToInt32(damage * 0.75);
				else damage = Convert.ToInt32(damage * 0.60);
            }				
			if (FindBuff("Vulnerable") != null)
			{
                if (HasRelic("Phrog"))
                    damage = Convert.ToInt32(damage * 1.75);
                else if (target.HasRelic("Odd Mushroom")) 
					damage = Convert.ToInt32(damage * 1.25);
				else damage = Convert.ToInt32(damage * 1.50);
            }
            Console.Write($"\n{Name} dealt {damage} from their attack to the {target.Name}");
            damage = ReduceDamageByBlock(target, damage);
			if (damage > 0)
			{
				if (HasRelic("Boot") && damage < 5)
					damage = 5;
				if (damage < 5 && target.HasRelic("Torii"))
					damage = 1;
				target.HPLoss(damage);
            }
			Console.Write("!\n");
            if (target.FindBuff("Curl Up") is Buff curlUp && curlUp != null)      // Louse Curl Up Effect
            {
                Console.WriteLine($"The Louse has curled up and gained {curlUp.Intensity} Block!");
                target.Block += curlUp.Intensity;
                target.Buffs.Remove(curlUp);
            }
			if (target.FindBuff("Thorns") is Buff thorns && thorns != null)
				NonAttackDamage(this,thorns.Intensity,thorns.Name);
            if (target.FindBuff("Flame Barrier") is Buff flameBarrier && flameBarrier != null)
                NonAttackDamage(this, flameBarrier.Intensity, flameBarrier.Name);
        }

		public void NonAttackDamage(Actor target, int damage, string effect)
		{
			if (Hp <= 0) return;
            Console.Write($"\n{Name} dealt {damage} from their {effect} to the {target.Name}");
			damage = ReduceDamageByBlock(target,damage);
            if (damage > 0)
                target.HPLoss(damage);
            Console.Write("!\n");
        }
		
		public int ReduceDamageByBlock(Actor target, int damage)
		{
			if (target.Block > 0)
			{
				int damageCalc = target.Block - damage;
				damage -= target.Block;
				target.Block = damageCalc;
				if (target.Block <= 0 && HasRelic("Hand Drill"))
					target.AddBuff(1, 2, (Hero)this );
				Console.Write($", reducing Block to {(target.Block > 0 ? $"{target.Block}" : "0")}");
			}
			return damage;
        }

		public void PoisonDamage(int damage)
		{
			Console.Write($"{Name} suffered from their poison");
			HPLoss(damage);
			FindBuff("Poison").Intensity--;
		}

		public void HPLoss(int damage)
		{
			if (HasRelic("Tungsten"))
				damage--;
			if (damage > 0)
			{
                if (FindBuff("Buffer") is Buff buffer && buffer != null)
                {
                    buffer.Counter--;
					Console.Write($",but the {Name}'s buffer prevented it");
                    return;
                }
                if (HasRelic("Runic Cube"))
					((Hero)this).DrawCards(1);
                if (FindRelic("Centennial") is Relic puzzle && puzzle.IsActive)
				{
                    ((Hero)this).DrawCards(3);
					puzzle.IsActive = false;
                }
                if (HasRelic("Self-Forming"))
					AddBuff(44, 3);
				if (FindRelic("Emotion") is Relic emotionChip && emotionChip != null)
					emotionChip.IsActive = true;
			}
			Hp -= damage;
            Console.Write($", reducing their HP to {Hp}/{MaxHP}");
        }

		// Easy searching for Relic
        public Relic FindRelic(string name)
        {
			if (Relics == null) return null;
            else return Relics.Find(x => x.Name.Contains(name));
        }

		public bool HasRelic(string name)
		{
            if (Relics == null) return false;
            else return Relics.Find(x => x.Name.Contains(name)) != null;
        }

		public void AddAction(string action,int turnNumber = 0)
		{
			Actions.Add($"{(turnNumber > 0 ? $"{turnNumber}: " : "")}{action}");
		}

		public List<string> FindTurnActions(int turnNumber,string type = "")
		{
			if (type != "")
				return Actions.FindAll(x => x.Contains($"{turnNumber}"));
			else return Actions.FindAll(x => x.Contains($"{turnNumber}")).FindAll(x => x.Contains(type));
        }
    }
}