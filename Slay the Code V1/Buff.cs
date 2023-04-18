namespace STV
{
    public class Buff
    {
        // attributes
        public string Name { get; set; }
        public bool BuffDebuff { get; set; }
        public byte Type { get; set; }
        public int Intensity { get; set; }
        public int Duration { get; set; }
        public int Counter { get; set; }


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
            return Name switch
            {
                "Vulnerable" => $"Takes 50% more damage from attacks for {Duration} turns.",
                "Weak" => $"Deal 25% less damage with attacks for {Duration} turns.",
                "Ritual" => $"Gains {Intensity} Strength at the end of its turn.",
                "Strength" => $"Increases attack damage by {Intensity}.",
                "Curl Up" => $"Gains {Intensity} Block upon receiving first attack.",
                "Frail" => $"Gain 25% less Block from cards for {Duration} turns.",
                "Focus" => $"Increases effectiveness of Orbs by {Intensity}.",
                "Artifact" => $"Prevents the next {Counter} debuffs from being applied.",
                "Dexterity" => $"Increases Block gained from Cards by {Intensity}.",
                "Mantra" => $"When you obtain 10 Mantra, enter Divinity. (current Mantra:{Counter})",
                "Mental Fortress" => $"Whenever you switch stances, gain {Intensity} Block.",
                "Mark" => $"Whenever you play Pressure Points, this enemy loses {Intensity} HP.",
                "No Block" => $"You may not gain Block from cards for the next {Duration} turns.",
                "Entangled" => $"You may not play any Attacks this turn.",
                "Sleeping" => $"This enemy is sleeping and damaging it will awaken it.",
                "Mode Shift" => $"After receiving {Intensity} damage, changes to defensive mode.",
                "Nightmare" => $"At the start of your next turn, Copy the selected card {Intensity} time(s).",
                "Thievery" => $"Steals {Intensity} Gold whenever it attacks.",
                "Enrage" => $"Whenever you play a skill, gains {Intensity} Strength.",
                "Barricade" => $"Block is not removed at the start of turn.",
                "No Draw" => $"You may not draw any more cards this turn.",
                "Energized" => $"Gain {Intensity} additional Energy next turn.",
                "Brutality" => $"At the start of your turn, lose {Intensity} HP and draw {Intensity} cards.",
                "Combust" => $"At the end of your turn, lose {Intensity} HP and deal {Intensity * 5} damage to ALL enemies.",
                "Corruption" => $"Skills cost 0. Whenever you play a Skill, Exhaust it.",
                "Dark Embrace" => $"Whenever a card is Exhausted, draw {Intensity} card.",
                "Double Tap" => $"This turn, your next {Counter} Attack is played twice.",
                "Evolve" => $"Whenever you draw a Status, draw {Intensity} card.",
                "Feel No Pain" => $"Whenever a card is Exhausted, gain {Intensity} Block.",
                "Strength Down" => $"Lose {Intensity} Strength at the end of the turn.",
                "Juggernaut" => $"Whenever you gain Block, deal {Intensity} damage to a random enemy.",
                "Metallicize" => $"At the end of your turn, gain {Intensity} Block.",
                "Rage" => $"Whenever you play an attack this turn, gain {Intensity} Block.",
                "Rupture" => $"Whenever you lose HP from a card, gain {Intensity} Strength.",
                "A Thousand Cuts" => $"Whenever you play a card, deal {Intensity} damage to ALL enemies.",
                "Accuracy" => $"Shivs deal {Intensity} additional damage.",
                "After Image" => $"Whenever you play a card, gain {Intensity} Block.",
                "Blur" => $"Block is not removed at the start of your next {Duration} turns.",
                "Poison" => $"At the beginning of its turn, the target loses {Intensity} HP and 1 stack of Poison.",
                "Burst" => $"This turn, your next {Counter} Skill is played twice.",
                "Thorns" => $"Whenever you are attacked, deal {Intensity} damage to the attacker.",
                "Choked" => $"Whenever you play a card, this enemy loses {Intensity} HP.",
                "Corpse Explosion" => $"When this enemy dies, deal damage equal to its MAX HP * {Intensity} to ALL enemies.",
                "Dodge and Roll" => $"Next turn, gain {Intensity} Block.",
                "Draw Card" => $"Next turn, draw {Intensity} additional card(s).",
                "Envenom" => $"Whenever an attack deals unblocked damage, apply {Intensity} Poison.",
                "Infinite Blades" => $"At the start of your turn, add {Intensity} Shiv to your hand.",
                "Noxious Fumes" => $"At the start of your turn, apply {Intensity} Poison to ALL enemies.",
                "Phantasmal Killer" => $"On your next {Counter} turn(s), your Attack damage is doubled.",
                "Tools of the Trade" => $"Next turn, discard {Intensity} card(s).",
                "Well-Laid Plans" => $"At the end of your turn, Retain up to {Intensity} card(s).",
                "Intangible" => $"Reduce any damage dealt to 1.",
                "Wraith Form" => $"Lose {Intensity} Dexterity at the end of your turn.",
                "Amplify" => $"This turn, your next {Counter} Power is played twice.",
                "Bias" => $"Lose {Intensity} Focus at the end of your turn.",
                "Buffer" => $"Prevent the next {Counter} time(s) you would lose HP.",
                "Lock-On" => $"Orbs deal 50% more damage to the this enemy for {Duration} turn(s).",
                "Creative AI" => $"At the start of each turn, add {Intensity} random Power card(s) to your hand.",
                "Echo Form" => $"The first {Counter} cards you play each turn are played twice.",
                "Heatsinks" => $"Whenever you play a Power card, draw {Intensity} card(s).",
                "Hello" => $"At the start of your turn, add {Intensity} random Common card(s) into your hand.",
                "Loop" => $"At the start of your turn, use the passive ability of your first Orb {Intensity} time(s).",
                "Machine Learning" => $"Draw {Intensity} additional card(s) at the start of each turn.",
                "Rebound" => $"Place the next {Intensity} card(s) you play this turn on top of your draw pile.",
                "Self Repair" => $"At the end of combat, heal {Intensity} HP.",
                "Static Discharge" => $"Whenever you take attack damage, Channel {Intensity} Lightning.",
                "Storm" => $"Whenever you play a Power, Channel {Intensity} Lightning.",
                "Electrodynamics" => $"Lightning now hits ALL enemies.",
                "Retain Hand" => $"Retain your hand this turn.",
                "Battle Hymn" => $"At the start of each turn, add {Intensity} Smite(s) into your hand.",
                "Blasphemy" => $"Die next turn.",
                "Collect" => $"Put Miracle+ into your hand at the start of your next {Duration} turns.",
                "Deva Form" => $"At the start of your turn, gain {Intensity} Energy and increase this gain by 1.",
                "Devotion" => $"At the start of your turn, gain {Intensity} Mantra.",
                "Establishment" => $"Whenever a card is Retained, reduce its cost by {Intensity} this combat.",
                "Foresight" => $"At the start of your turn, Scry {Intensity}.",
                "Like Water" => $"At the end of your turn, if you are in Calm, gain {Intensity} Block.",
                "Master Reality" => $"Whenever a card is created during combat, Upgrade it.",
                "Nirvana" => $"Whenever you Scry, gain {Intensity} Block.",
                "Rushdown" => $"Whenever you enter Wrath, draw {Intensity} cards.",
                "Simmering Fury" => $"At the start of your next turn, enter Wrath and draw {Intensity} cards.",
                "Study" => $"At the end of your turn, shuffle {Intensity} Insight(s) into your draw pile.",
                "Talk to the Hand" => $"Whenever you attack this enemy, gain {Intensity} Block.",
                "Wave of the Hand" => $"Whenever you gain Block this turn, apply {Intensity} Weak to ALL enemies.",
                "Wreath of Flame" => $"Your next Attack deals {Intensity} additional damage.",
                "Magnetism" => $"t the start of each turn, add {Intensity} random Colorless card(s) to your hand.",
                "Sadistic Nature" => $"Whenever you apply a Debuff to an enemy, they take {Intensity} damage.",
                "Mayhem" => $"At the start of your turn, play the {Intensity} top card(s) of your draw pile.",
                "Da Bomb" => $"At the end of {Duration} turn(s), deal 40 damage to ALL enemies.",
                "Flame Barrier" => $"When attacked this turn, deals {Intensity} damage back.",
                "Free Attack" => $"The next {Counter} Attacks you play costs 0.",
                "Fire Breathing" => $"Whenever you draw a Status or Curse card, deal {Intensity} damage to ALL enemies.",
                "Panache" => $"If you play {Counter} more cards this turn, deal {Intensity} damage to all enemies.",
                "Omega" => $"At the end of your turn, deal {Intensity} damage to ALL enemies.",
                "Plated Armor" => $"At the end of your turn, gain {Intensity} Block. Receiving unblocked attack damage reduces Plated Armor by 1.",
                "Dexterity Down" => $"Lose {Intensity} Dexterity at the end of the turn.",
                "Regeneration" => $"At the end of your turn, heal {Intensity} HP and reduce Regen by 1.",
                "Duplication" => $"Your next {Counter} cards are played twice.",
                _ => "",
            };
        }


        // Duration-related methods
        public void DurationSet(int turnDuration)
        { Duration += turnDuration; }

        public void DurationDecrease()
        {
            if (Type == 1)
                Duration -= 1;
        }

        public bool DurationEnded()
        {
            if (Duration == 0 && Type == 1)
                return true;
            else return false;
        }

        //Counter methods
        public void CounterSet(int counter)
        { Counter += counter; }

        // Intensity related methods
        public void IntensitySet(int intensity)
        { Intensity += intensity; }
    }
}