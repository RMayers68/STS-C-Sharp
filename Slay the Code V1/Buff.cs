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
                "Strength Next Turn" => $"{(Intensity < 0 ? $"Gain {Intensity * -1}" : $"Lose {Intensity}" )} Strength at the end of the turn.",
                "Juggernaut" => $"Whenever you gain Block, deal {Intensity} damage to a random enemy.",
                "Metallicize" => $"At the end of your turn, gain {Intensity} Block.",
                "Rage" => $"Whenever you play an attack this turn, gain {Intensity} Block.",
                "Rupture" => $"Whenever you lose HP from a card, gain {Intensity} Strength.",
                "Thousand Cuts" => $"Whenever you play a card, deal {Intensity} damage to ALL enemies.",
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
                "Phantasmal" => $"On your next {Duration} turn(s), your Attack damage is doubled.",
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
                "Block Return" => $"Whenever you attack this enemy, gain {Intensity} Block.",
                "Wave of the Hand" => $"Whenever you gain Block this turn, apply {Intensity} Weak to ALL enemies.",
                "Vigor" => $"Your next Attack deals {Intensity} additional damage.",
                "Magnetism" => $"t the start of each turn, add {Intensity} random Colorless card(s) to your hand.",
                "Sadistic Nature" => $"Whenever you apply a Debuff to an enemy, they take {Intensity} damage.",
                "Mayhem" => $"At the start of your turn, play the {Intensity} top card(s) of your draw pile.",
                "Da Bomb" => $"At the end of {Duration} turn(s), deal 40 damage to ALL enemies.",
                "Flame Barrier" => $"When attacked this turn, deals {Intensity} damage back.",
                "Free Attack" => $"The next {Counter} Attacks you play costs 0.",
                "Fire Breathing" => $"Whenever you draw a Status or Curse card, deal {Intensity} damage to ALL enemies.",
                "Panache" => $"If you play {5 - Counter} more cards this turn, deal {Intensity} damage to all enemies.",
                "Omega" => $"At the end of your turn, deal {Intensity} damage to ALL enemies.",
                "Plated Armor" => $"At the end of your turn, gain {Intensity} Block. Receiving unblocked attack damage reduces Plated Armor by 1.",
                "Dexterity Down" => $"Lose {Intensity} Dexterity at the end of the turn.",
                "Regen" => $"At the end of your turn, heal {Intensity} HP and reduce Regen by 1.",
                "Duplication" => $"Your next {Counter} cards are played twice.",
                "Double Damage" => $"Attacks deal double damage for {Duration} turns.",
                "Split" => $"When its HP is at or below 50%, will split into 2 smaller Slimes with its current HP.",
                "Spore Cloud" => $"On death, applies {Intensity} Vulnerable.",
                "Angry" => $"Increases Strength by {Intensity} when attacked.",
                "Flying" => $"Takes 50% less damage on the next {Intensity} attacks. Removed when attacked {Intensity} times in a single turn.",
                "Hex" => $"Whenever you play a non-Attack card, add {Intensity} Dazed to your draw pile.",
                "Malleable" => $"Upon receiving attack damage, gains {Intensity} Block. Block increments each time Malleable is triggered. Resets every turn.",
                "Painful Stabs" => $"Shuffle 1 Wound into your Discard Pile each time you receive unblocked attack damage.",
                "Life Link" => $"On its second turn after its HP reaches 0, revives with half HP if other Darklings are alive.",
                "Explode" => $"Dies and deals 30 damage in {Duration} turns.",
                "Constricted" => $"At the end of your turn, take {Intensity} damage.",
                "Fading" => $"Dies in {Duration} turns.",
                "Shifting" => $"Upon losing HP, loses that much Strength until the end of the turn.",
                "Reactive" => $"Upon receiving attack damage, changes its intent.",
                "Slow" => $"The enemy receives {Intensity} * 10% more damage from attacks this turn. Whenever you play a card, increases by 1.",
                "Draw Reduction" => $"Draw 1 less card next {Duration} turns.",
                "Time Warp" => $"Whenever you play {Counter} more cards this combat, ends your turn and gains 2 Strength.",
                "Curiosity" => $"Whenever you play a Power card, gains {Intensity} Strength.",
                "Minion" => $"Minions abandon combat without their leader.",
                _ => "",
            };
        }


        // Duration-related methods
        public void DurationSet(int turnDuration)
        { Duration += turnDuration; }

        public bool DurationEnded()
        {
            Duration--;
            if (Duration == 0 && Type == 1)
                return true;
            else return false;
        }

        //Counter methods
        public void CounterSet(int counter)
        { Counter += counter; }

        // Decrements counter by 1 and returns true if it is ready to be removed (set to 0)
        public bool CounterAtZero()
        {
            Counter--;
            if (Counter == 0 && Type == 4)
                return true;
            else return false;
        }

        // Intensity related methods
        public void IntensitySet(int intensity)
        { Intensity += intensity; }

        public bool IntensityAtZero()
        {
            Intensity--;
            if (Intensity == 0)
                return true;
            else return false;
        }
    }
}