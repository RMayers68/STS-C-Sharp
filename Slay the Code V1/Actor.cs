namespace STV
{
	public abstract class Actor // base class
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

        private static readonly Random ActorRNG = new();

        //constructor
        public Actor() { }


		// Inclusive methods
		public void GainBlock(int block, List<Enemy> encounter = null)
		{
			if (Hp <= 0) return;
			if (FindBuff("Dexterity") is Buff dex && dex != null)
				block += dex.Intensity;
			if (HasBuff("Frail"))
				block = Convert.ToInt32(block * 0.75);
			if (block > 0)
			{
                Block += block;
                Console.WriteLine($"The {Name} gained {block} Block.");
				if (FindBuff("Wave of the Hand") is Buff wave && wave != null)
					foreach (Enemy e in encounter)
						e.AddBuff(2, wave.Intensity, (Hero)this);
				if (FindBuff("Juggernaut") is Buff jugger && jugger != null && encounter != null)
					NonAttackDamage(encounter[ActorRNG.Next(encounter.Count)], jugger.Intensity, "Juggernaut");
            }
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
			if (buff.Name == "Vulnerable" && hero != null && hero.HasRelic("Champion"))
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
					{
                        AddAction($"Mantra Gained: {effect}");
						if (buff.Counter >= 10)
						{
							buff.CounterSet(-10);
							((Hero)this).SwitchStance("Divinity");

                        }
                    }						
					break;
			}
			if (hero != null && hero.FindBuff("Sadistic") is Buff sadistic && !sadistic.BuffDebuff)
				NonAttackDamage(this, sadistic.Intensity,sadistic.Name);
		}

        //HP Altering Methods
        public void Attack(Actor target, int damage, List<Enemy> encounter = null)
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
			if (target.FindBuff("Vulnerable") != null)
			{
                if (HasRelic("Phrog"))
                    damage = Convert.ToInt32(damage * 1.75);
                else if (target.HasRelic("Odd Mushroom")) 
					damage = Convert.ToInt32(damage * 1.25);
				else damage = Convert.ToInt32(damage * 1.50);
            }
			if (target.FindBuff("Slow") is Buff slow && slow != null)
				damage *= Convert.ToInt32(slow.Intensity * 0.1);
            Console.Write($"\n{Name} dealt {damage} from their attack to the {target.Name}");
            damage = ReduceDamageByBlock(target, damage);
			if (damage > 0)
			{
				if (target.FindBuff("Flying") is Buff fly && fly != null)
				{
					damage /= 2;
					if (fly.IntensityAtZero())
						target.Buffs.Remove(fly);
				}
				if (HasRelic("Boot") && damage < 5)
					damage = 5;
				if (damage < 5 && target.HasRelic("Torii"))
					damage = 1;
				damage = target.HPLoss(damage);
				if (target.FindBuff("Plated Armor") is Buff armor && armor != null)
					armor.Intensity--;
				if (damage > 0 && FindBuff("Envenom") is Buff envenom && envenom != null)
					target.AddBuff(39, envenom.Intensity, (Hero)this);
				if (damage > 0 && FindBuff("Static Discharge") is Buff discharge && discharge != null)
					for (int i = 0; i < discharge.Intensity; i++)
						Orb.ChannelOrb((Hero)target, encounter, 0);
				if (target.FindBuff("Angry") is Buff angry && angry != null)
					target.AddBuff(4, angry.Intensity);
				if (HasBuff("Painful Stabs"))
					//((Hero)target).DiscardPile.Add(new(Dict.cardL[357]));
				if (target.HasBuff("Reactive"))
					((Enemy)target).SetEnemyIntent(1);
				if (target.FindBuff("Malleable") is Buff mall && mall != null)
				{
					target.GainBlock(mall.Intensity);
					mall.Intensity++;
				}
            }
			Console.Write("!\n");
            if (target.FindBuff("Curl Up") is Buff curlUp && curlUp != null)      // Louse Curl Up Effect
            {
                Console.WriteLine($"{target.Name} has curled up and gained {curlUp.Intensity} Block!");
                target.Block += curlUp.Intensity;
                target.Buffs.Remove(curlUp);
            }
			if (target.FindBuff("Thorns") is Buff thorns && thorns != null)
				target.NonAttackDamage(this,thorns.Intensity,thorns.Name);
			if (target.FindBuff("Block Return") is Buff block && block != null)
				GainBlock(block.Intensity);
            if (target.FindBuff("Flame Barrier") is Buff flameBarrier && flameBarrier != null)
                target.NonAttackDamage(this, flameBarrier.Intensity, flameBarrier.Name);
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
                damage = target.IntangibleCheck(damage);
                int damageCalc = target.Block - damage;
				damage -= target.Block;
				target.Block = damageCalc;
				if (target.Block <= 0 && HasRelic("Hand Drill"))
					target.AddBuff(1, 2, (Hero)this );
				Console.Write($", reducing Block to {(target.Block > 0 ? $"{target.Block}" : "0")}");
			}
			return damage;
        }

		public void PoisonDamage()
		{
            if (FindBuff("Poison") is Buff poison && poison != null)
			{
                Console.Write($"{Name} suffered from their poison");
                HPLoss(poison.Intensity);
                Console.Write("!\n");
                poison.Intensity--;
            }
		}

		public int HPLoss(int damage)
		{
			if (HasRelic("Tungsten"))
				damage--;
			if (damage > 0)
			{
                if (FindBuff("Buffer") is Buff buffer && buffer != null)
                {
                    buffer.Counter--;
					Console.Write($",but the {Name}'s buffer prevented it");
                    return 0;
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
				damage = IntangibleCheck(damage);
			}
			Hp -= damage;
            Console.Write($", reducing their HP to {(Hp < 0 ? "0" : Hp)}/{MaxHP}");
			if (HasBuff("Shifting"))
			{
				AddBuff(4, -1 * damage);
				AddBuff(30, -1 * damage);
			}
			return damage;
        }

        public void HealHP(int heal)
        {
            if (Relics.Find(x => x.Name == "Mark of the Bloom") != null)
                Console.WriteLine("Your attempt at healing failed due to the Mark of the Bloom.");
            else
            {
                Hp += heal;
                if (Hp > MaxHP)
                {
                    heal = Hp + heal - MaxHP;
                    Hp = MaxHP;
                }
                Console.WriteLine($"{Name} has healed {heal} HP and are now at {Hp}/{MaxHP} HP!");
            }
        }
        public int IntangibleCheck(int damage)
		{
			if (damage > 1 && HasBuff("Intangible"))
				return 1;
			else return damage;
		}

		// Easy searching for Relics and Buffs
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

        public Buff FindBuff(string name)
        {
            return Buffs.Find(x => x.Name.Contains(name));
        }

        public bool HasBuff(string name)
        {
            if (Buffs == null) return false;
            else return Buffs.Find(x => x.Name.Contains(name)) != null;
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