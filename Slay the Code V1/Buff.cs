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
                case 11: return $"Whenever you switch stances, gain {Intensity} Block.";
                case 12: return $"Whenever you play Pressure Points, this enemy loses {Intensity} HP.";
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
                case 35: return $"Whenever you play a card, deal {Intensity} damage to ALL enemies.";
                case 36: return $"Shivs deal {Intensity} additional damage.";
                case 37: return $"Whenever you play a card, gain {Intensity} Block.";
                case 38: return $"Block is not removed at the start of your next {Duration} turns.";
                case 39: return $"At the beginning of its turn, the target loses {Intensity} HP and 1 stack of Poison.";
                case 40: return $"This turn, your next {Counter} Skill is played twice.";
                case 41: return $"Whenever you are attacked, deal {Intensity} damage to the attacker.";
                case 42: return $"Whenever you play a card this turn, targeted enemy loses {Intensity} HP.";
                case 43: return $"When this enemy dies, deal damage equal to its MAX HP * {Intensity} to ALL enemies.";
                case 44: return $"Next turn, gain {Intensity} Block.";
                case 45: return $"Next turn, draw {Intensity} additional card(s).";
                case 46: return $"Whenever an attack deals unblocked damage, apply {Intensity} Poison.";
                case 47: return $"At the start of your turn, add {Intensity} Shiv to your hand.";
                case 48: return $"At the start of your turn, apply {Intensity} Poison to ALL enemies.";
                case 49: return $"On your next {Counter} turn(s), your Attack damage is doubled.";
                case 50: return $"Next turn, discard {Intensity} card(s).";
                case 51: return $"At the end of your turn, Retain up to {Intensity} card(s).";
                case 52: return $"Reduce any damage dealt to 1.";
                case 53: return $"Lose {Intensity} Dexterity at the end of your turn.";
                case 54: return $"This turn, your next {Counter} Power is played twice.";
                case 55: return $"Lose {Intensity} Focus at the end of your turn.";
                case 56: return $"Prevent the next {Counter} time(s) you would lose HP.";
                case 57: return $"Orbs deal 50% more damage to the this enemy for {Duration} turn(s).";
                case 58: return $"At the start of each turn, add {Intensity} random Power card(s) to your hand.";
                case 59: return $"The first {Counter} cards you play each turn are played twice.";
                case 60: return $"Whenever you play a Power card, draw {Intensity} card(s).";
                case 61: return $"At the start of your turn, add {Intensity} random Common card(s) into your hand.";
                case 62: return $"At the start of your turn, use the passive ability of your first Orb {Intensity} time(s).";
                case 63: return $"Draw {Intensity} additional card(s) at the start of each turn.";
                case 64: return $"Place the next {Intensity} card(s) you play this turn on top of your draw pile.";
                case 65: return $"At the end of combat, heal {Intensity} HP.";
                case 66: return $"Whenever you take attack damage, Channel {Intensity} Lightning.";
                case 67: return $"Whenever you play a Power, Channel {Intensity} Lightning.";
                case 68: return $"Lightning now hits ALL enemies.";
                case 69: return $"Retain your hand this turn.";
                case 70: return $"At the start of each turn, add {Intensity} Smite(s) into your hand.";
                case 71: return $"Die next turn.";
                case 72: return $"Put Miracle+ into your hand at the start of your next {Duration} turns.";
                case 73: return $"At the start of your turn, gain {Intensity} Energy and increase this gain by 1.";
                case 74: return $"At the start of your turn, gain {Intensity} Mantra.";
                case 75: return $"Whenever a card is Retained, reduce its cost by {Intensity} this combat.";
                case 76: return $"At the start of your turn, Scry {Intensity}.";
                case 77: return $"At the end of your turn, if you are in Calm, gain {Intensity} Block.";
                case 78: return $"Whenever a card is created during combat, Upgrade it.";
                case 79: return $"Whenever you Scry, gain {Intensity} Block.";
                case 80: return $"Whenever you enter Wrath, draw {Intensity} cards.";
                case 81: return $"At the start of your next turn, enter Wrath and draw {Intensity} cards.";
                case 82: return $"At the end of your turn, shuffle {Intensity} Insight(s) into your draw pile.";
                case 83: return $"Whenever you attack this enemy, gain {Intensity} Block.";
                case 84: return $"Whenever you gain Block this turn, apply {Intensity} Weak to ALL enemies.";
                case 85: return $"Your next Attack deals {Intensity} additional damage.";
                case 86: return $"t the start of each turn, add {Intensity} random Colorless card(s) to your hand.";
                case 87: return $"Whenever you apply a Debuff to an enemy, they take {Intensity} damage.";
                case 88: return $"At the start of your turn, play the {Intensity} top card(s) of your draw pile.";
                case 89: return $"At the end of {Duration} turn(s), deal 40 damage to ALL enemies.";
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