namespace STV
{
    public class Buff
    {
        // attributes
        public int BuffID { get; set; }
        public string Name { get; set; }
        public bool BuffDebuff { get; set; }
        public byte Type { get; set; }
        public int? Intensity { get; set; }
        public int? Duration { get; set; }
        public int? Counter { get; set; }


        //constructor

        public Buff(int buffID, string name, bool buffDebuff, byte type)
        {
            this.BuffID = buffID;
            this.Name = name;
            this.BuffDebuff = buffDebuff;
            this.Type = type;
        }

        // pulling from dictionary
        public Buff(Buff buff)
        {
            this.BuffID = buff.BuffID;
            this.Name = buff.Name;
            this.BuffDebuff = buff.BuffDebuff;
            this.Type = buff.Type;
        }


        //string method
        public string Description()
        {
            switch (BuffID)
            {
                default:    return "";
                case 1:     return $"Takes 50% more damage from attacks for {Duration} turns.";
                case 2:     return $"Deal 25% less damage with attacks for {Duration} turns.";
                case 3:     return $"At the end of its turn, gains {Intensity} Strength.";
                case 4:     return $"Increases attack damage by {Intensity}.";
                case 5:     return $"Gains {Intensity} Block upon first receiving attack damage.";
                case 6:     return $"Gain 25% less Block from cards for {Duration} turns.";
                case 7:     return $"Increases the effectiveness of Orbs by {Intensity}.";
                case 8:     return $"Prevents the next {Counter} debuffs from being applied.";
                case 9:     return $"Increases Block gained from Cards by {Intensity}.";
                case 10:    return $"When you obtain 10 Mantra, enter Divinity. (current Mantra:{Counter})";
                case 11:    return $"Whenever you switch stances, gain {Intensity}";
                case 12:    return $"Whenever you play Pressure Points, all enemy with Mark loses {Intensity} HP.";
                case 13:    return $"You may not gain Block from cards for the next {Duration} turns.";
                case 14:    return $"You may not play any Attacks for {Duration} turns.";
                case 15:    return $"Will not awaken for {Duration} turns";
                case 16:    return $"After receiving {Intensity} damage, changes to a Defensive Mode.";
                case 17:    return $"When attacked, deals {Intensity} damage back.";
                case 18:    return $"Steals {Intensity} Gold whenever it attacks.";
                case 19:    return $"Whenever you play a Skill, gains {Intensity} Strength";
                case 20:    return $"At the end of your turn, gain {Intensity} Block. Receiving unblocked attack damage reduces Plated Armor by 1.";
                case 21:    return $"At the end of your turn, lose {Intensity} Strength.";
                case 22:    return $"Reduce ALL damage taken and HP loss to 1 this turn. (lasts {Duration} turns)";
                case 23:    return $"At the end of its turn, gains {Intensity} Block.";
                case 24:    return $"At the beginning of its turn, the target loses {Duration} HP and 1 stack of Poison.";
                case 25:    return $"At the end of its turn, heals {Duration} HP and loses 1 stack of Regen.";
                case 26:    return $"At the end of your turn, lose {Intensity} Dexterity.";
                case 27:    return $"When its HP is at or below 50%, will split into 2 smaller Slimes with its current HP.";
                case 28:    return $"On death, applies {Intensity} Vulnerable.";                   
            }
        }


        // Duration-related methods
        public void DurationSet(int turnDuration)
        {
            if (this.Duration == null) this.Duration = turnDuration;
            else this.Duration += turnDuration;
        }

        public void DurationDecrease()
        {
            if (this.Type == 1)
                this.Duration -= 1;
        }

        public bool DurationEnded()
        {
            if (this.Duration == 0)
            {
                return true;
            }
            else return false;
        }

        //Counter methods
        public void CounterSet(int counter)
        {
            if (this.Counter == null) this.Counter = counter;
            else this.Counter += counter;
        }
        
        // Intensity related methods
        public void IntensitySet(int intensity)
        {
            if (this.Intensity == null) this.Intensity = intensity;
            else this.Intensity += intensity;
        }
    }
}
