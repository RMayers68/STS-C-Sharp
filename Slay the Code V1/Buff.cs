namespace STV
{
    public class Buff
    {
        // attributes
        public string Name { get; set; }
        public bool BuffDebuff { get; set; }
        public byte Type { get; set; }
        public int? Intensity { get; set; }
        public int? Duration { get; set; }
        public int? Counter { get; set; }


        //constructor

        public Buff(string name, bool buffDebuff, byte type)
        {
            this.Name = name;
            this.BuffDebuff = buffDebuff;
            this.Type = type;
        }

        // pulling from dictionary
        public Buff(Buff buff)
        {
            this.Name = buff.Name;
            this.BuffDebuff = buff.BuffDebuff;
            this.Type = buff.Type;
        }

        //string method
        public string Description()
        {
            switch (Name)
            {
                default: return "";
                case "Vulnerable": return $"Takes 50% more damage from attacks for {Duration} turns.";
                case "Weak": return $"Deal 25% less damage with attacks for {Duration} turns.";
                case "Ritual": return $"Gains {Intensity} Strength at the end of its turn.";
                case "Strength": return $"Increases attack damage by {Intensity}.";
                case "Curl Up": return $"Gains {Intensity} Block upon receiving first attack.";
                case "Frail": return $"Gain 25% less Block from cards for {Duration} turns.";
                case "Focus": return $"Increases effectiveness of Orbs by {Intensity}.";
                case "Artifact": return $"Prevents the next {Counter} debuffs from being applied.";
                case "Dexterity": return $"Increases Block gained from Cards by {Intensity}.";
                case "Mantra": return $"When you obtain 10 Mantra, enter Divinity. (current Mantra:{Counter})";
                case "Mental Fortress": return $"Whenever you switch stances, gain {Intensity} Block.";
                case "Mark": return $"Whenever you play Pressure Points, this enemy loses {Intensity} HP.";
                case "No Block": return $"You may not gain Block from cards for the next {Duration} turns.";
                case "Entangled": return $"You may not play any Attacks this turn.";
                case "Sleeping": return $"This enemy is sleeping and damaging it will awaken it.";
                case "Mode Shift": return $"After receiving {Intensity} damage, changes to defensive mode.";
                case "Nightmare": return $"At the start of your next turn, Copy the selected card {Intensity} time(s).";
                case "Thievery": return $"Steals {Intensity} Gold whenever it attacks.";
                case "Enrage": return $"Whenever you play a skill, gains {Intensity} Strength.";
                case "Barricade": return $"Block is not removed at the start of turn.";
                case "No Draw": return $"You may not draw any more cards this turn.";
                case "Energized": return $"Gain {Intensity} additional Energy next turn.";
                case "Brutality": return $"At the start of your turn, lose {Intensity} HP and draw {Intensity} cards.";
                case "Combust": return $"At the end of your turn, lose {Intensity} HP and deal {Intensity * 5} damage to ALL enemies.";
                case "Corruption": return $"Skills cost 0. Whenever you play a Skill, Exhaust it.";
                case "Dark Embrace": return $"Whenever a card is Exhausted, draw {Intensity} card.";
                case "Double Tap": return $"This turn, your next {Counter} Attack is played twice.";
                case "Evolve": return $"Whenever you draw a Status, draw {Intensity} card.";
                case "Feel No Pain": return $"Whenever a card is Exhausted, gain {Intensity} Block.";
                case "Strength Down": return $"Lose {Intensity} Strength at the end of the turn.";
                case "Juggernaut": return $"Whenever you gain Block, deal {Intensity} damage to a random enemy.";
                case "Metallicize": return $"At the end of your turn, gain {Intensity} Block.";
                case "Rage": return $"Whenever you play an attack this turn, gain {Intensity} Block.";
                case "Rupture": return $"Whenever you lose HP from a card, gain {Intensity} Strength.";
                case "A Thousand Cuts": return $"Whenever you play a card, deal {Intensity} damage to ALL enemies.";
                case "Accuracy": return $"Shivs deal {Intensity} additional damage.";
                case "After Image": return $"Whenever you play a card, gain {Intensity} Block.";
                case "Blur": return $"Block is not removed at the start of your next {Duration} turns.";
                case "Poison": return $"At the beginning of its turn, the target loses {Intensity} HP and 1 stack of Poison.";
                case "Burst": return $"This turn, your next {Counter} Skill is played twice.";
                case "Thorns": return $"Whenever you are attacked, deal {Intensity} damage to the attacker.";
                case "Choked": return $"Whenever you play a card, this enemy loses {Intensity} HP.";
                case "Corpse Explosion": return $"When this enemy dies, deal damage equal to its MAX HP * {Intensity} to ALL enemies.";
                case "Dodge and Roll": return $"Next turn, gain {Intensity} Block.";
                case "Draw Card": return $"Next turn, draw {Intensity} additional card(s).";
                case "Envenom": return $"Whenever an attack deals unblocked damage, apply {Intensity} Poison.";
                case "Infinite Blades": return $"At the start of your turn, add {Intensity} Shiv to your hand.";
                case "Noxious Fumes": return $"At the start of your turn, apply {Intensity} Poison to ALL enemies.";
                case "Phantasmal Killer": return $"On your next {Counter} turn(s), your Attack damage is doubled.";
                case "Tools of the Trade": return $"Next turn, discard {Intensity} card(s).";
                case "Well-Laid Plans": return $"At the end of your turn, Retain up to {Intensity} card(s).";
                case "Intangible": return $"Reduce any damage dealt to 1.";
                case "Wraith Form": return $"Lose {Intensity} Dexterity at the end of your turn.";
                case "Amplify": return $"This turn, your next {Counter} Power is played twice.";
                case "Bias": return $"Lose {Intensity} Focus at the end of your turn.";
                case "Buffer": return $"Prevent the next {Counter} time(s) you would lose HP.";
                case "Lock-On": return $"Orbs deal 50% more damage to the this enemy for {Duration} turn(s).";
                case "Creative AI": return $"At the start of each turn, add {Intensity} random Power card(s) to your hand.";
                case "Echo Form": return $"The first {Counter} cards you play each turn are played twice.";
                case "Heatsinks": return $"Whenever you play a Power card, draw {Intensity} card(s).";
                case "Hello": return $"At the start of your turn, add {Intensity} random Common card(s) into your hand.";
                case "Loop": return $"At the start of your turn, use the passive ability of your first Orb {Intensity} time(s).";
                case "Machine Learning": return $"Draw {Intensity} additional card(s) at the start of each turn.";
                case "Rebound": return $"Place the next {Intensity} card(s) you play this turn on top of your draw pile.";
                case "Self Repair": return $"At the end of combat, heal {Intensity} HP.";
                case "Static Discharge": return $"Whenever you take attack damage, Channel {Intensity} Lightning.";
                case "Storm": return $"Whenever you play a Power, Channel {Intensity} Lightning.";
                case "Electrodynamics": return $"Lightning now hits ALL enemies.";
                case "Retain Hand": return $"Retain your hand this turn.";
                case "Battle Hymn": return $"At the start of each turn, add {Intensity} Smite(s) into your hand.";
                case "Blasphemy": return $"Die next turn.";
                case "Collect": return $"Put Miracle+ into your hand at the start of your next {Duration} turns.";
                case "Deva Form": return $"At the start of your turn, gain {Intensity} Energy and increase this gain by 1.";
                case "Devotion": return $"At the start of your turn, gain {Intensity} Mantra.";
                case "Establishment": return $"Whenever a card is Retained, reduce its cost by {Intensity} this combat.";
                case "Foresight": return $"At the start of your turn, Scry {Intensity}.";
                case "Like Water": return $"At the end of your turn, if you are in Calm, gain {Intensity} Block.";
                case "Master Reality": return $"Whenever a card is created during combat, Upgrade it.";
                case "Nirvana": return $"Whenever you Scry, gain {Intensity} Block.";
                case "Rushdown": return $"Whenever you enter Wrath, draw {Intensity} cards.";
                case "Simmering Fury": return $"At the start of your next turn, enter Wrath and draw {Intensity} cards.";
                case "Study": return $"At the end of your turn, shuffle {Intensity} Insight(s) into your draw pile.";
                case "Talk to the Hand": return $"Whenever you attack this enemy, gain {Intensity} Block.";
                case "Wave of the Hand": return $"Whenever you gain Block this turn, apply {Intensity} Weak to ALL enemies.";
                case "Wreath of Flame": return $"Your next Attack deals {Intensity} additional damage.";
                case "Magnetism": return $"t the start of each turn, add {Intensity} random Colorless card(s) to your hand.";
                case "Sadistic Nature": return $"Whenever you apply a Debuff to an enemy, they take {Intensity} damage.";
                case "Mayhem": return $"At the start of your turn, play the {Intensity} top card(s) of your draw pile.";
                case "Da Bomb": return $"At the end of {Duration} turn(s), deal 40 damage to ALL enemies.";
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