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

        public Buff(int buffID, string name, bool buffDebuff, byte type, string description)
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
                default: return "";
                case 1: return $"Takes 50% more damage from attacks for {Duration} turns.";
                case 2: return $"Deal 25% less damage with attacks for {Duration} turns.";
                case 3: return $"At the end of its turn, gains {Intensity} Strength.";
                case 4: return $"Increases attack damage by {Intensity}.";
                case 5: return $"Gains {Intensity} Block upon first receiving attack damage.";
                case 6: return $"Gain 25% less Block from cards for {Duration} turns.";
                case 7: return $"Increases the effectiveness of Orbs by {Intensity}.";
                case 8: return $"Prevents the next {Counter} debuffs from being applied.";
                case 9: return $"Increases Block gained from Cards by {Intensity}.";
                case 10: return $"When you obtain 10 Mantra, enter Divinity. (current Mantra:{Counter})";
                case 11: return $"Whenever you switch stances, gain {Intensity}.";
                case 12: return $"Whenever you play Pressure Points, all enemy with Mark loses {Intensity} HP.";
                case 13: return $"You may not gain Block from cards for the next {Duration} turns.";
                case 14: return $"You may not play any Attacks this turn.";
                case 15: return $"This enemy is sleeping and damaging its health will awaken it.";
                case 16: return $"After receiving {Intensity} damage, changes to a defensive mode.";
                case 17: return $"Whenever you play an attack, take {Intensity} damage.";
                case 18: return $"Steals {Intensity} Gold whenever it attacks.";
                case 19: return $"Whenever you play a skill, gains {Intensity} Strength.";
                case 20: return $"Block is not removed at the start of turn.";
                case 21: return $"You may not draw any more cards this turn.";
                case 22: return $"Gain {Intensity} additional Energy next turn.";
                case 23: return $"At the start of your turn, lose {Intensity} HP and draw {Intensity} cards.";
                case 24: return $"At the end of your turn, lose {Intensity} HP and deal {Intensity * 5} damage to ALL enemies.";
                case 25: return $"Skills cost 0. Whenever you play a Skill, Exhaust it.";
                case 26: return $"Whenever a card is Exhausted, draw {Intensity} card.";
                case 27: return $"This turn, your next {Counter} Attack is played twice.";
                case 28: return $"Whenever you draw a Status, draw {Intensity} card.";
                case 29: return $"Whenever a card is Exhausted, gain {Intensity} Block.";
                case 30: return $"Lose {Intensity} Strength at the end of the turn.";
                case 31: return $"Whenever you gain Block, deal {Intensity} damage to a random enemy.";
                case 32: return $"At the end of your turn, gain {Intensity} Block.";
                case 33: return $"Whenever you play an Attack this turn, gain {Intensity} Block.";
                case 34: return $"Whenever you lose HP from a card, gain {Intensity} Strength.";
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

