using STV;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace STV
{
    public class Card : IEquatable<Card>, IComparable<Card>
    {

        // attributes
        public string Name { get; set; }
        public string Type { get; set; } // Attack, Skill, Power, Status or Curse
        public string Rarity { get; set; } //Common,Uncommon,Rare 
        public int EnergyCost { get; set; } 
        private int AttackDamage { get; set; }
        private int AttackLoops { get; set; }
        private int BlockAmount { get; set; }
        private int BlockLoops { get; set; }
        private int MagicNumber { get; set; } // Ironclad self-damage, Silent discards, Defect Orb Channels, and Watcher Scrys, among other misc uses
        private int BuffID { get; set; }
        private int BuffAmount { get; set; }
        private int CardsDrawn { get; set; }
        private int EnergyGained { get; set; }
        private bool Targetable { get; set; }
        private bool HeroBuff { get; set; }
        private bool TargetBuff { get; set; }
        private bool SingleAttack { get; set; }
        private bool AttackAll { get; set; }
        private bool SelfDamage { get; set; }
        private bool Discards { get; set; }
        private bool OrbChannels { get; set; }
        private bool Scrys { get; set; }
        private bool Upgraded { get; set; }
        private int GoldCost { get; set; }
        private int TmpEnergyCost { get; set; }



        //constructors
        public Card()
        {
            this.Name = "Purchased";
            this.GoldCost = 0;
        }

        public Card(string name, string type, string rarity, string energyCost, List<int> attributes)
        {
            this.Name = name;
            this.Type = type;
            this.Rarity = rarity;
            if (energyCost == "None")
                this.EnergyCost = -2;
            else if (energyCost == "X")
                this.EnergyCost = -1;
            else 
                this.EnergyCost = Int32.Parse(energyCost);
            this.Upgraded = false;
            this.AttackDamage = attributes[0];
            this.AttackLoops = attributes[1];
            this.BlockAmount = attributes[2];
            this.BlockLoops = attributes[3];
            this.MagicNumber = attributes[4];
            this.BuffID = attributes[5];
            this.BuffAmount = attributes[6];
            this.CardsDrawn = attributes[7];
            this.EnergyGained = attributes[8];
            List<bool> triggers = new List<bool>(9);
            for (int i = 9; i < 18; i++)
            {
                if (attributes[i] == 0)
                    triggers.Add(false);
                else triggers.Add(true);
            }
            this.Targetable = triggers[0];
            this.HeroBuff = triggers[1];
            this.TargetBuff = triggers[2];
            this.SingleAttack = triggers[3];
            this.AttackAll = triggers[4];
            this.SelfDamage = triggers[5];
            this.Discards = triggers[6];
            this.OrbChannels = triggers[7];
            this.Scrys = triggers[8];
        }

        public Card(Card card)
        {
            Random rng = new Random();
            this.Name = card.Name;
            this.Type = card.Type;
            this.Rarity = card.Rarity;
            this.EnergyCost = card.EnergyCost;
            if (this.EnergyCost >= 0)
                TmpEnergyCost = EnergyCost;
            this.GoldCost = Rarity == "Rare" ? rng.Next(135, 166) : Rarity == "Uncommon" ? rng.Next(68, 83) : rng.Next(45, 56);
            this.Upgraded = card.Upgraded;
            this.AttackDamage = card.AttackDamage;
            this.AttackLoops = card.AttackLoops;
            this.BlockAmount = card.BlockAmount;
            this.BlockLoops = card.BlockLoops;
            this.MagicNumber = card.MagicNumber;
            this.BuffID = card.BuffID;
            this.BuffAmount = card.BuffAmount;
            this.CardsDrawn = card.CardsDrawn;
            this.EnergyGained = card.EnergyGained;
            this.Targetable = card.Targetable;
            this.HeroBuff = card.HeroBuff;
            this.TargetBuff = card.TargetBuff;
            this.SingleAttack = card.SingleAttack;
            this.AttackAll = card.AttackAll;
            this.SelfDamage = card.SelfDamage;
            this.Discards = card.Discards;
            this.OrbChannels = card.OrbChannels;
            this.Scrys = card.Scrys;
        }
        
        //accessors and mutators
        public void setAttackDamage(int addedDamage)
        { this.AttackDamage += addedDamage; }

        public void setBlockAmount(int addedDamage)
        { this.BlockAmount += addedDamage; }

        public void setEnergyCost(int energyCost)
        {
            EnergyCost = energyCost;
            TmpEnergyCost = energyCost;
        }
        public void setTmpEnergyCost(int tmpEnergyCost)
        { TmpEnergyCost = tmpEnergyCost; }

        public int getGoldCost()
        { return GoldCost; }

        public int getMagicNumber()
        { return MagicNumber; }

        public string getName()
        { return $"{Name}{(Upgraded ? "+" : "")}"; }

        public bool isUpgraded()
        { return Upgraded; }

        //comparators and equals
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Card objAsPart = obj as Card;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Card other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }

        public int CompareTo(Card other)
        {
            // A null value means that this object is greater.
            if (Name == null && other.Name == null) return 0;
            else if (Name == null) return -1;
            else if (other.Name == null) return 1;
            else return Name.CompareTo(other.Name);
        }

        // methods
        public override string ToString()
        {
            if (EnergyCost == -2)
                return $"Name: {Name}{(Upgraded ? "+" : "")}\nType: {Type}\nEffect: {getDescription()}";
            return $"Name: {Name}{(Upgraded ? "+" : "")}\nEnergy Cost: {(EnergyCost > TmpEnergyCost ? $"{TmpEnergyCost}" : $"{EnergyCost}" )} \tType: {Type}\nEffect: {getDescription()}\n";
        }

        
        public static Card FindCard(string cardName, List<Card> list)
        {
            return list.Find(x => x.Name == cardName);
        }

        public static List<Card> FindCards(string cardName, List<Card> list)
        {
            return list.FindAll(x => x.Name == cardName).ToList();
        }

        public static Card ChooseCard(List<Card> list, string action)
        {
            int cardChoice = 0;
            if (list.Count < 0) { return null; }
            Console.WriteLine($"Which card would you like to {action}?");
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"{i + 1}:{list[i].Name}");
            while (!Int32.TryParse(Console.ReadLine(), out cardChoice) || cardChoice < 1 || cardChoice > list.Count)
                Console.WriteLine("Invalid input, enter again:");
            return list[cardChoice - 1];
        }

        //Moving Cards to different List methods
        public void MoveCard(List<Card> from, List<Card> to)
        {
            from.Remove(this);
            to.Add(this);
        }
       
        public void Exhaust(Hero hero, List<Card> leaveThisList)
        {
            MoveCard(leaveThisList, hero.ExhaustPile);
            if (Name == "Sentinel")
                hero.GainTurnEnergy(EnergyGained);
            Console.WriteLine($"{Name} has been exhausted.");
        }

        public void Discard(Hero hero, Random rng)
        {
            if (hero.Hand.Count < 1)
                return;
            if (Name == "Tactician")
                hero.GainTurnEnergy(EnergyGained);
            else if (Name == "Reflex")
                DrawCards(rng,hero,MagicNumber);
            MoveCard(hero.Hand, hero.DiscardPile);
            hero.Actions.Add("Discard");
        }

        public static void Scry(Hero hero, int amount)
        {
            int scryChoice = -1;
            while (scryChoice != 0 && amount > 0)
            {
                Console.WriteLine($"\nEnter the number of the card you would like to scry into your discard pile or hit 0 to move on.");
                for (int i = 1; i <= amount; i++)
                    Console.WriteLine($"{i}:{hero.DrawPile[hero.DrawPile.Count - i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out scryChoice) || scryChoice < 0 || scryChoice > amount)
                    Console.WriteLine("Invalid input, enter again:");
                if (scryChoice > 0)
                {
                    Card scryedCard = hero.DrawPile[hero.DrawPile.Count - scryChoice];
                    scryedCard.MoveCard(hero.DrawPile, hero.DiscardPile);
                    amount--;
                }
            }
            // Weave Check
            if (Card.FindCard("Weave", hero.DiscardPile) is Card weave && weave != null && hero.Hand.Count < 10)
                weave.MoveCard(hero.DiscardPile, hero.Hand);
        }

        public static void AddShivs(Hero hero, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                if (hero.Hand.Count < 10)
                    hero.Hand.Add(new Card(Dict.cardL[296]));
                else hero.DiscardPile.Add(new Card(Dict.cardL[296]));
            }
        }

        public static List<Card> RandomCards(string type, int count, Random rng, string exclusion = null)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                Card referenceCard; 
                if (exclusion != null)
                {
                    do
                        referenceCard = RandomCard(type, rng);
                    while (referenceCard.Type != exclusion);
                }
                else referenceCard = RandomCard(type, rng);
                cards.Add(new Card(referenceCard));
            }
            return cards;
        }

        public static Card RandomCard(string type, Random rng)
        {
            switch (type)
            {
                default:
                    return Dict.cardL[rng.Next(73)];
                case "Silent":
                    return Dict.cardL[rng.Next(73, 146)];
                case "Defect":
                    return Dict.cardL[rng.Next(146, 219)];
                case "Watcher":
                    return Dict.cardL[rng.Next(221, 294)];
                case "Colorless":
                    return Dict.cardL[rng.Next(297, 332)];
                case "All Heroes":
                    return Dict.cardL[rng.Next(294)];
            }
        }

        public static void DrawCards(Random rng,Hero hero, int cards)
        {
            while (hero.Hand.Count < 10)
            {
                if (hero.DrawPile.Count == 0)
                    Discard2Draw(hero, rng);
                if (hero.DrawPile.Count == 0)
                    break;
                hero.DrawPile.Last().MoveCard(hero.DrawPile, hero.Hand);
                if (hero.Hand.Last().Name == "Deus Ex Machina")
                {
                    for (int i = 0; i < hero.Hand.Last().MagicNumber; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(Dict.cardL[336]));
                        else hero.DiscardPile.Add(new Card(Dict.cardL[336]));
                    }
                    hero.Hand.Last().Exhaust(hero, hero.Hand);
                }                                  
                cards--;
                if (cards == 0)
                    return;
            }
        }

        public static List<Card> Shuffle(List<Card> drawPile, Random rng)
        {
            int n = drawPile.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = drawPile[k];
                drawPile[k] = drawPile[n];
                drawPile[n] = value;
            }
            return drawPile;
        }

        // Takes all cards in discard, moves them to drawpile, and then shuffles the drawpile
        public static void Discard2Draw(Hero hero, Random rng)
        {
            for (int i = hero.DiscardPile.Count; i > 0; i--)
                hero.DiscardPile.Last().MoveCard(hero.DiscardPile, hero.DrawPile);
            Shuffle(hero.DrawPile, rng);
        }

        // Description String
        public string getDescription()
        {
            switch (Name)
            {
                default: return "";
                case "Anger": return $"Deal {AttackDamage} damage. Place a copy of this card in your discard pile.";
                case "Armaments": return $"Gain {BlockAmount} Block. Upgrade {(Upgraded ? "all cards" : "a card")} in your hand for the rest of combat.";
                case "Barricade": return $"Block no longer expires at the start of your turn.";
                case "Bash": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Vulnerable.";
                case "Battle Trance": return $"Draw {CardsDrawn} cards. You cannot draw additional cards this turn.";
                case "Berserk": return $"Gain {BuffAmount} Vulnerable. At the start of your turn, gain 1 Energy.";
                case "Blood for Blood": return $"Costs 1 less Energy for each time you lose HP in combat. Deal {AttackDamage} damage.";
                case "Bloodletting": return $"Lose {MagicNumber} HP. Gain {EnergyGained} Energy.";
                case "Bludgeon": return $"Deal {AttackDamage} damage.";
                case "Body Slam": return $"Deal damage equal to your current Block.";
                case "Brutality": return $"{(Upgraded ? "Innate. " : "")}At the start of your turn, lose {BuffAmount} HP and draw {BuffAmount} card.";
                case "Burning Pact": return $"Exhaust 1 card. Draw {CardsDrawn} cards.";
                case "Carnage": return $"Ethereal. Deal {AttackDamage} damage.";
                case "Clash": return $"Can only be played if every card in your hand is an Attack. Deal {AttackDamage} damage.";
                case "Cleave": return $"Deal {AttackDamage} damage to ALL enemies.";
                case "Clothesline": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak.";
                case "Combust": return $"At the end of your turn, lose 1 HP and deal {BuffAmount} damage to ALL enemies.";
                case "Corruption": return $"Skills cost 0. Whenever you play a Skill, Exhaust it.";
                case "Dark Embrace": return $"Whenever a card is Exhausted, draw {BuffAmount} card.";
                case "Demon Form": return $"At the start of each turn, gain {BuffAmount} Strength.";
                case "Disarm": return $"Enemy loses {-1*BuffAmount} Strength. Exhaust.";
                case "Double Tap": return $"This turn, your next {(Upgraded ? "2 Attacks are" : "Attack is")} played twice.";
                case "Dropkick": return $"Deal {AttackDamage} damage. If the target is Vulnerable, gain {EnergyGained} Energy and draw {CardsDrawn} card.";
                case "Dual Wield": return $"Create {(Upgraded ? "2 copies" : "a copy")} of an Attack or Power card in your hand.";
                case "Entrench": return $"Double your current Block.";
                case "Evolve": return $"Whenever you draw a Status, draw {BuffAmount} card.";
                case "Exhume": return $"Choose an Exhausted card and put it in your hand. Exhaust.";
                case "Feed": return $"Deal {AttackDamage} damage. If this kills the enemy, gain {MagicNumber} permanent Max HP. Exhaust.";
                case "Feel No Pain": return $"Whenever a card is Exhausted, gain {BuffAmount} Block.";
                case "Fiend Fire": return $"Exhaust your hand. Deal {AttackDamage} damage for each Exhausted card. Exhaust.";
                case "Fire Breathing": return $"Whenever you draw a Status or Curse card, deal {BuffAmount} damage to ALL enemies.";
                case "Flame Barrier": return $"Gain {BlockAmount} Block. Whenever you are attacked this turn, deal {BuffAmount} damage to the attacker.";
                case "Flex": return $"Gain {BuffAmount} Strength. At the end of your turn, lose {BuffAmount} Strength.";
                case "Ghostly Armor": return $"Ethereal. Gain {BlockAmount} Block.";
                case "Havoc": return $"Play the top card of your draw pile and Exhaust it.";
                case "Headbutt": return $"Deal {AttackDamage} damage. Place a card from your discard pile on top of your draw pile.";
                case "Heavy Blade": return $"Deal {AttackDamage} damage. Strength affects Heavy Blade {MagicNumber+1} times.";
                case "Hemokinesis": return $"Lose {MagicNumber} HP. {AttackDamage} damage.";
                case "Immolate": return $"Deal {AttackDamage} damage to ALL enemies. Shuffle a Burn into your discard pile.";
                case "Impervious": return $"Gain {BlockAmount} Block. Exhaust.";
                case "Infernal Blade": return $"Add a random Attack to your hand. It costs 0 this turn. Exhaust.";
                case "Inflame": return $"Gain {BuffAmount} Strength.";
                case "Intimidate": return $"Apply {BuffAmount} Weak to ALL enemies. Exhaust.";
                case "Iron Wave": return $"Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
                case "Juggernaut": return $"Whenever you gain Block, deal {BuffAmount} damage to a random enemy.";
                case "Limit Break": return $"Double your Strength. Exhaust.";
                case "Metallicize": return $"At the end of your turn, gain {BuffAmount} Block.";
                case "Offering": return $"Lose {MagicNumber} HP. Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
                case "Perfected Strike": return $"Deal {AttackDamage} damage. Deals an additional +{MagicNumber} damage for ALL of your cards containing Strike.";
                case "Pommel Strike": return $"Deal {AttackDamage} damage. Draw {CardsDrawn} card{(Upgraded ? "s" : "")}.";
                case "Power Through": return $"Add 2 Wounds to your hand. Gain {BlockAmount} Block.";
                case "Pummel": return $"Deal {AttackDamage} damage {AttackLoops} times. Exhaust.";
                case "Rage": return $"Whenever you play an Attack this turn, gain {BuffAmount} Block.";
                case "Rampage": return $"Deal {AttackDamage} damage. Every time this card is played, increase its damage by {MagicNumber} for this combat.";
                case "Reaper": return $"Deal {AttackDamage} damage to ALL enemies. Heal for unblocked damage. Exhaust.";
                case "Reckless Charge": return $"Deal {AttackDamage} damage. Shuffle a Dazed into your draw pile.";
                case "Rupture": return $"Whenever you lose HP from a card, gain {BuffAmount} Strength.";
                case "Searing Blow": return $"Deal {AttackDamage} damage. Can be upgraded any number of times.";
                case "Second Wind": return $"Exhaust all non-Attack cards in your hand and gain {BlockAmount} Block for each.";
                case "Seeing Red": return $"Gain {EnergyGained} Energy. Exhaust.";
                case "Sentinel": return $"Gain {BlockAmount} Block. If this card is Exhausted, gain {EnergyGained} Energy.";
                case "Sever Soul": return $"Exhaust all non-Attack cards in your hand. Deal {AttackDamage} damage.";
                case "Shockwave": return $"Apply {BuffAmount} Weak and Vulnerable to ALL enemies. Exhaust.";
                case "Shrug It Off": return $"Gain {BlockAmount} Block. Draw {CardsDrawn} card.";
                case "Spot Weakness": return $"If an enemy intends to attack, gain {BuffAmount} Strength.";
                case "Sword Boomerang": return $"Deal {AttackDamage} damage to a random enemy {AttackLoops} times.";
                case "Thunderclap": return $"Deal {AttackDamage} damage and apply {BuffAmount} Vulnerable to ALL enemies.";
                case "True Grit": return $"Gain {BlockAmount} Block. Exhaust a{(Upgraded ? "" : " random")} card from your hand.";
                case "Twin Strike": return $"Deal {AttackDamage} damage twice.";
                case "Uppercut": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak. Apply {BuffAmount} Vulnerable.";
                case "Warcry": return $"Draw {CardsDrawn} card{(Upgraded ? "s" : "")}. Place a card from your hand on top of your draw pile. Exhaust.";
                case "Whirlwind": return $"Deal {AttackDamage} damage to ALL enemies X times.";
                case "Wild Strike": return $"Deal {AttackDamage} damage. Shuffle a Wound into your draw pile.";
                case "A Thousand Cuts": return $"Whenever you play a card, deal {BuffAmount} damage to ALL enemies.";
                case "Accuracy": return $"Shivs deal {BuffAmount} additional damage.";
                case "Acrobatics": return $"Draw {CardsDrawn} cards. Discard {MagicNumber} card.";
                case "Adrenaline": return $"Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
                case "After Image": return $"{(Upgraded ? "Innate. " : "")}Whenever you play a card, gain {BuffAmount} Block.";
                case "Alchemize": return $"Obtain a random potion. Exhaust.";
                case "All-Out Attack": return $"Deal {AttackDamage} damage to ALL enemies. Discard 1 card at random.";
                case "Backflip": return $"Gain {BlockAmount} Block. Draw 2 cards.";
                case "Backstab": return $"Deal {AttackDamage} damage. Innate. Exhaust.";
                case "Bane": return $"Deal {AttackDamage} damage. If the enemy is Poisoned, deal {AttackDamage} damage again.";
                case "Blade Dance": return $"Add 3 Shivs to your hand.";
                case "Blur": return $"Gain {BlockAmount} Block. Block is not removed at the start of your next turn.";
                case "Bouncing Flask": return $"Apply {BuffAmount} Poison to a random enemy {MagicNumber} times.";
                case "Bullet Time": return $"You cannot draw any cards this turn. Reduce the cost of cards in your hand to 0 this turn.";
                case "Burst": return $"This turn your next {(Upgraded ? "2 Skills are" : "Skill is")} played twice.";
                case "Calculated Gamble": return $"Discard your hand, then draw that many cards. Exhaust.";
                case "Caltrops": return $"Whenever you are attacked, deal {BuffAmount} damage to the attacker.";
                case "Catalyst": return $"{(Upgraded ? "Triple" : "Double")} an enemy's Poison. Exhaust.";
                case "Choke": return $"Deal {AttackDamage} damage. Whenever you play a card this turn, targeted enemy loses {BuffAmount} HP.";
                case "Cloak And Dagger": return $"Gain {BlockAmount} Block. Add {MagicNumber} Shiv to your hand.";
                case "Concentrate": return $"Discard {MagicNumber} cards. Gain {EnergyGained} Energy.";
                case "Corpse Explosion": return $"Apply {BuffAmount} Poison. When an enemy dies, deal damage equal to its MAX HP to ALL enemies.";
                case "Crippling Cloud": return $"Apply {BuffAmount} Poison and 2 Weak to ALL enemies. Exhaust.";
                case "Dagger Spray": return $"Deal {AttackDamage} damage to ALL enemies twice.";
                case "Dagger Throw": return $"Deal {AttackDamage} damage. Draw {CardsDrawn} card. Discard {MagicNumber} card.";
                case "Dash": return $"Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
                case "Deadly Poison": return $"Apply {BuffAmount} Poison.";
                case "Deflect": return $"Gain {BlockAmount} Block.";
                case "Die Die Die": return $"Deal {AttackDamage} damage to ALL enemies. Exhaust.";
                case "Distraction": return $"Add a random Skill to your hand. It costs 0 this turn. Exhaust.";
                case "Dodge and Roll": return $"Gain {BlockAmount} Block. Next turn gain {BuffAmount} Block.";
                case "Doppelganger": return $"Next turn, draw X{(Upgraded ? "+1" : "")} cards and gain X{(Upgraded ? "+1" : "")} Energy.";
                case "Endless Agony": return $"Whenever you draw this card, add a copy of it to your hand. Deal {AttackDamage} damage. Exhaust.";
                case "Envenom": return $"Whenever an attack deals unblocked damage, apply {BuffAmount} Poison.";
                case "Escape Plan": return $"Draw {CardsDrawn} card. If the card is a Skill, gain {BlockAmount} Block.";
                case "Eviscerate": return $"Costs 1 less Energy for each card discarded this turn. Deal {AttackDamage} damage three times.";
                case "Expertise": return $"Draw cards until you have {CardsDrawn} in hand.";
                case "Finisher": return $"Deal {AttackDamage} damage for each Attack played this turn.";
                case "Flechettes": return $"Deal {AttackDamage} damage for each Skill in your hand.";
                case "Flying Knee": return $"Deal {AttackDamage} damage. Next turn gain {EnergyGained} Energy.";
                case "Footwork": return $"Gain {BuffAmount} Dexterity.";
                case "Glass Knife": return $"Deal {AttackDamage} damage twice. Glass Knife's damage is lowered by 2 this combat.";
                case "Grand Finale": return $"Can only be played if there are no cards in your draw pile. Deal {AttackDamage} damage to ALL enemies.";
                case "Heel Hook": return $"Deal {AttackDamage} damage. If the enemy is Weak, Gain 1 Energy and draw 1 card.";
                case "Infinite Blades": return $"{(Upgraded ? "Innate. " : "")}At the start of your turn, add a Shiv to your hand.";
                case "Leg Sweep": return $"Apply {BuffAmount} Weak. Gain {BlockAmount} Block.";
                case "Malaise": return $"Enemy loses X{(Upgraded ? "+1" : "")} Strength. Apply X{(Upgraded ? "+1" : "")} Weak. Exhaust.";
                case "Masterful Stab": return $"Cost 1 additional for each time you lose HP this combat. Deal {AttackDamage} Damage.";
                case "Neutralize": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak.";
                case "Nightmare": return $"Choose a card. Next turn, add 3 copies of that card into your hand. Exhaust.";
                case "Noxious Fumes": return $"At the start of your turn, apply {BuffAmount} Poison to ALL enemies.";
                case "Outmaneuver": return $"Next turn gain {BuffAmount} Energy.";
                case "Phantasmal Killer": return $"On your next turn, your Attack damage is doubled.";
                case "Piercing Wail": return $"ALL enemies lose {-1 * BuffAmount} Strength for 1 turn. Exhaust.";
                case "Poisoned Stab": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Poison.";
                case "Predator": return $"Deal {AttackDamage} damage. Draw 2 more cards next turn.";
                case "Prepared": return $"Draw {CardsDrawn} card. Discard {CardsDrawn} card.";
                case "Quick Slash": return $"Deal {AttackDamage} damage. Draw 1 card.";
                case "Reflex": return $"Unplayable. If this card is discarded from your hand, draw {CardsDrawn} card.";
                case "Riddle with Holes": return $"Deal {AttackDamage} damage {AttackLoops} times.";
                case "Setup": return $"Place a card in your hand on top of your draw pile. It cost 0 until it is played.";
                case "Skewer": return $"Deal {AttackDamage} damage X times.";
                case "Slice": return $"Deal {AttackDamage} damage.";
                case "Sneaky Strike": return $"Deal {AttackDamage} damage. If you have discarded a card this turn, gain 2 Energy.";
                case "Storm of Steel": return $"Discard your hand. Add 1 Shiv{(Upgraded ? "+" : "")} to your hand for each card discarded.";
                case "Sucker Punch": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak.";
                case "Survivor": return $"Gain {BlockAmount} Block. Discard a card.";
                case "Tactician": return $"Unplayable. If this card is discarded from your hand, gain {EnergyGained} Energy.";
                case "Terror": return $"Apply 99 Vulnerable. Exhaust.";
                case "Tools of the Trade": return $"At the start of your turn, draw 1 card and discard 1 card.";
                case "Unload": return $"Deal {AttackDamage} damage. Discard ALL non-Attack cards.";
                case "Well-Laid Plans": return $"At the end of your turn, Retain up to {BuffAmount} card.";
                case "Wraith Form": return $"Gain {BuffAmount} Intangible. At the end of your turn, lose 1 Dexterity.";
                case "Aggregate": return $"Gain 1 Energy for every {MagicNumber} cards in your draw pile.";
                case "All For One": return $"Deal {AttackDamage} damage. Put all Cost 0 cards from your discard pile into your hand.";
                case "Amplify": return $"This turn, your next {(Upgraded ? "2 Powers are" : "Power is")} played twice.";
                case "Auto-Shields": return $"If you have 0 Block, gain {BlockAmount} Block.";
                case "Ball Lightning": return $"Deal {AttackDamage} damage. Channel 1 Lightning.";
                case "Barrage": return $"Deal {AttackDamage} damage for each Channeled Orb.";
                case "Beam Cell": return $"Deal {AttackDamage} damage and apply {BuffAmount} Vulnerable.";
                case "Biased Cognition": return $"Gain {BuffAmount} Focus. At the start of each turn, lose 1 Focus.";
                case "Blizzard": return $"Deal damage equal to {MagicNumber} times the Frost Channeled this combat to ALL enemies.";
                case "Boot Sequence": return $" Innate. Gain {BlockAmount} Block. Exhaust.";
                case "Buffer": return $"Prevent the next {(Upgraded ? "2 times" : "time")} you would lose HP.";
                case "Bullseye": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Lock-On.";
                case "Capacitor": return $"Gain {BuffAmount} Orb slots.";
                case "Chaos": return $"Channel {BlockLoops} random Orb{(Upgraded ? "s" : "")}.";
                case "Charge Battery": return $"Gain {BlockAmount} Block. Next turn gain 1 Energy.";
                case "Chill": return $"Channel 1 Frost for each enemy in combat. Exhaust.";
                case "Claw": return $"Deal {AttackDamage} damage. All Claw cards deal 2 additional damage this combat.";
                case "Cold Snap": return $"Deal {AttackDamage} damage. Channel 1 Frost.";
                case "Compile Driver": return $"Deal {AttackDamage} damage. Draw 1 card for each unique Orb you have.";
                case "Consume": return $"Gain {BuffAmount} Focus. Lose 1 Orb Slot.";
                case "Coolheaded": return $"Channel 1 Frost. Draw {CardsDrawn} card.";
                case "Core Surge": return $"Deal {AttackDamage} damage. Gain 1 Artifact. Exhaust.";
                case "Creative AI": return $"At the start of each turn, add a random Power card to your hand.";
                case "Darkness": return $"Channel 1 Dark. {(Upgraded ? "Trigger the passive ability of all Dark orbs." : "")}";
                case "Defragment": return $"Gain {BuffAmount} Focus.";
                case "Doom and Gloom": return $"Deal {AttackDamage} damage to ALL enemies. Channel 1 Dark.";
                case "Double Energy": return $"Double your Energy. Exhaust.";
                case "Dualcast": return $"Evoke your next Orb twice.";
                case "Echo Form": return $"{(Upgraded ? "" : "Ethereal. ")}The first card you play each turn is played twice.";
                case "Electrodynamics": return $"Lightning now hits ALL enemies. Channel {BlockLoops} Lightning.";
                case "Equilibrium": return $"Gain {BlockAmount} Block. Retain your hand this turn.";
                case "Fission": return $"{(Upgraded ? "Evoke" : "Remove")} all of your Orbs. Gain Energy for each Orb {(Upgraded ? "evoked" : "removed")}.";
                case "Force Field": return $"Costs 1 less for each Power card played this combat. Gain {BlockAmount} Block.";
                case "FTL": return $"Deal {AttackDamage} damage. If you have played less than {MagicNumber} cards this turn, draw 1 card.";
                case "Fusion": return $"Channel 1 Plasma.";
                case "Genetic Algorithm": return $"Gain {BlockAmount} Block. When played, permanently increase this card's Block by {MagicNumber}. Exhaust.";
                case "Glacier": return $"Gain {BlockAmount} Block. Channel 2 Frost.";
                case "Go for the Eyes": return $"Deal {AttackDamage} damage. If the enemy intends to attack, apply {BuffAmount} Weak.";
                case "Heatsinks": return $"Whenever you play a Power card, draw {BuffAmount} card.";
                case "Hello World": return $"{(Upgraded ? "Innate. " : "")}At the start of your turn, add a random Common card into your hand.";
                case "Hologram": return $"Gain {BlockAmount} Block. Return a card from your discard pile to your hand. {(Upgraded ? "" : "Exhaust.")}";
                case "Hyperbeam": return $"Deal {AttackDamage} damage to ALL enemies. Lose 3 Focus.";
                case "Leap": return $"Gain {BlockAmount} Block.";                
                case "Loop": return $"At the start of your turn, use the passive ability of your first Orb{(Upgraded ? " two times." : ".")}";
                case "Machine Learning": return $"{(Upgraded ? "Innate. " : "")}Draw 1 additional card at the start of each turn.";
                case "Melter": return $"Remove all Block from an enemy. Deal {AttackDamage} damage.";
                case "Meteor Strike": return $"Deal {AttackDamage} damage. Channel 3 Plasma.";
                case "Multi-Cast": return $"Evoke your next Orb X{(Upgraded ? "+1" : "")} times.";
                case "Overclock": return $"Draw {CardsDrawn} cards. Add a Burn into your discard pile.";
                case "Rainbow": return $"Channel 1 Lightning, 1 Frost, and 1 Dark. {(Upgraded ? "" : "Exhaust.")}";
                case "Reboot": return $"Shuffle all of your cards into your draw pile, then draw {CardsDrawn} cards. Exhaust.";
                case "Rebound": return $"Deal {AttackDamage} damage. Place the next card you play this turn on top of your draw pile.";
                case "Recursion": return $"Evoke your next Orb. Channel the Orb that was just Evoked.";
                case "Recycle": return $"Exhaust a card. Gain Energy equal to its cost.";
                case "Reinforced Body": return $"Gain {BlockAmount} Block X times.";
                case "Reprogram": return $"Lose {BuffAmount} Focus. Gain {BuffAmount} Strength. Gain {BuffAmount} Dexterity.";
                case "Rip and Tear": return $"Deal {AttackDamage} damage to a random enemy 2 times.";
                case "Scrape": return $"Deal {AttackDamage} damage. Draw {CardsDrawn} cards. Discard all cards drawn this way that do not cost 0.";
                case "Seek": return $"Choose {(Upgraded ? "2 cards" : "a card")} from your draw pile and place {(Upgraded ? "them" : "it")} into your hand. Exhaust.";
                case "Self Repair": return $"At the end of combat, heal {BuffAmount} HP.";
                case "Skim": return $"Draw {CardsDrawn} cards.";
                case "Stack": return $"Gain Block equal to the number of cards in your discard pile{(Upgraded ? "+3" : "")}.";
                case "Static Discharge": return $"Whenever you take attack damage, Channel {BuffAmount} Lightning.";
                case "Steam Barrier": return $"Gain {BlockAmount} Block. This card's Block is lowered by 1 this combat.";
                case "Storm": return $"{(Upgraded ? "Innate. " : "")}Whenever you play a Power, Channel 1 Lightning.";
                case "Streamline": return $"Deal {AttackDamage} damage. Whenever you play Streamline, reduce its cost by 1 for this combat.";
                case "Sunder": return $"Deal {AttackDamage} damage. If this kills the enemy, gain 3 Energy.";
                case "Sweeping Beam": return $"Deal {AttackDamage} damage to ALL enemies. Draw 1 card.";
                case "Tempest": return $"Channel {(Upgraded ? "+1" : "")} Lightning. Exhaust.";
                case "Thunder Strike": return $"Deal {AttackDamage} damage to a random enemy for each Lightning Channeled this combat.";
                case "TURBO": return $"Gain {EnergyGained} Energy. Add a Void into your discard pile.";
                case "White Noise": return $"Add a random Power to your hand. It costs 0 this turn. Exhaust.";
                case "Zap": return $"Channel 1 Lightning.";
                case "Defend": return $"Gain {BlockAmount} Block.";
                case "Strike": return $"Deal {AttackDamage} damage.";
                case "Alpha": return $"{(Upgraded ? "Innate. " : "")}Shuffle a Beta into your draw pile. Exhaust.";
                case "Battle Hymn": return $"{(Upgraded ? "Innate. " : "")}At the start of each turn, add a Smite into your hand.";
                case "Blasphemy": return $"{(Upgraded ? "Retain. " : "")}Enter Divinity. Die next turn. Exhaust";
                case "Bowling Bash": return $"Deal {AttackDamage} damage for each enemy in combat.";
                case "Brilliance": return $"Deal {AttackDamage} damage. Deals additional damage equal to Mantra gained this combat.";
                case "Carve Reality": return $"Deal {AttackDamage} damage. Add a Smite to your hand.";
                case "Collect": return $"Put a Miracle+ into your hand at the start of your next X{(Upgraded ? "+1" : "")} turns. Exhaust.";
                case "Conclude": return $"Deal {AttackDamage} damage to ALL enemies. End your turn.";
                case "Conjure Blade": return $"Shuffle an Expunger into your draw pile. Exhaust. (Expunger costs 1, deals 9 damage X{(Upgraded ? "+1" : "")} times.)";
                case "Consecrate": return $"Deal {AttackDamage} damage to ALL enemies.";
                case "Crescendo": return $"Retain. Enter Wrath. Exhaust.";
                case "Crush Joints": return $"Deal {AttackDamage} damage. If the last card played this combat was a Skill, apply {BuffAmount} Vulnerable.";
                case "Cut Through Fate": return $"Deal {AttackDamage} damage. Scry {MagicNumber}. Draw {CardsDrawn} card.";
                case "Deceive Reality": return $"Gain {BlockAmount} Block. Add a Safety to your hand.";
                case "Deus Ex Machina": return $"Unplayable. When you draw this card, add {MagicNumber} Miracles to your hand. Exhaust.";
                case "Deva Form": return $"{(Upgraded ? "" : "Ethereal. ")}At the start of your turn, gain Energy and increase this gain by 1.";
                case "Devotion": return $"At the start of your turn, gain {BuffAmount} Mantra.";
                case "Empty Body": return $"Gain {BlockAmount} Block. Exit your Stance.";
                case "Empty Fist": return $"Deal {AttackDamage} damage. Exit your Stance.";
                case "Empty Mind": return $"Draw {CardsDrawn} cards. Exit your Stance.";
                case "Eruption": return $"Deal {AttackDamage} damage. Enter Wrath.";
                case "Establishment": return $"{(Upgraded ? "Innate. " : "")}Whenever a card is Retained, reduce its cost by 1 this combat.";
                case "Evaluate": return $"Gain {BlockAmount} Block. Shuffle an Insight into your draw pile.";
                case "Fasting": return $"Gain {BuffAmount} Strength. Gain {BuffAmount} Dexterity. Gain 1 less Energy at the start of each turn.";
                case "Fear No Evil": return $"Deal {AttackDamage} damage. If the enemy intends to Attack, enter Calm.";
                case "Flurry Of Blows": return $"Deal {AttackDamage} damage. Whenever you change stances, return this from the discard pile to your hand.";
                case "Flying Sleeves": return $"Retain. Deal {AttackDamage} damage twice.";
                case "Follow-Up": return $"Deal {AttackDamage} damage. If the last card played this combat was an Attack, gain 1 Energy.";
                case "Foreign Influence": return $"Choose 1 of 3 Attacks of any color to add into your hand.{(Upgraded ? " It costs 0 this turn." : "")} Exhaust.";
                case "Foresight": return $"At the start of your turn, Scry {BuffAmount}.";
                case "Halt": return $"Gain {BlockAmount} Block. If you are in Wrath, gain {MagicNumber} additional Block.";
                case "Indignation": return $"If you are in Wrath, apply {BuffAmount} Vulnerable to ALL enemies, otherwise enter Wrath.";
                case "Inner Peace": return $"If you are in Calm, draw{CardsDrawn} cards. Otherwise, enter Calm.";
                case "Judgment": return $"If the enemy has {MagicNumber} or less HP, set their HP to 0.";
                case "Just Lucky": return $"Scry {MagicNumber}. Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
                case "Lesson Learned": return $"Deal {AttackDamage} damage. If Fatal, Upgrade a random card in your deck. Exhaust.";
                case "Like Water": return $"At the end of your turn, if you are in Calm, gain {BuffAmount} Block.";
                case "Master Reality": return $"Whenever a card is created during combat, Upgrade it.";
                case "Meditate": return $"Put {(Upgraded ? "2 cards" : "a card")} from your discard pile into your hand and Retain. Enter Calm. End your turn.";
                case "Mental Fortress": return $"Whenever you change Stances, gain {BuffAmount} Block.";
                case "Nirvana": return $"Whenever you Scry, gain {BuffAmount} Block.";
                case "Omniscience": return $"Choose a card in your draw pile. Play the chosen card twice and Exhaust it. Exhaust.";
                case "Perseverance": return $"Retain. Gain {BlockAmount} Block. When Retained, increase its Block by {MagicNumber} this combat.";
                case "Pray": return $"Gain {MagicNumber} Mantra. Shuffle an Insight into your draw pile.";
                case "Pressure Points": return $"Apply {BuffAmount} Mark. ALL enemies lose HP equal to their Mark.";
                case "Prostrate": return $"Gain {MagicNumber} Mantra. Gain {BlockAmount} Block.";
                case "Protect": return $"Retain. Gain {BlockAmount} Block.";
                case "Ragnarok": return $"Deal {AttackDamage} damage to a random enemy {AttackLoops} times.";
                case "Reach Heaven": return $"Deal {AttackDamage} damage. Shuffle a Through Violence into your draw pile.";
                case "Rushdown": return $"Whenever you enter Wrath, draw 2 cards.";
                case "Sanctity": return $"Gain {BlockAmount} Block. If the last card played was a Skill, draw 2 cards.";
                case "Sands of Time": return $"Retain. Deal {AttackDamage} damage. When Retained, lower its cost by 1 this combat.";
                case "Sash Whip": return $"Deal {AttackDamage} damage. If the last card played this combat was an Attack, apply {BuffAmount} Weak.";
                case "Scrawl": return $"Draw cards until your hand is full. Exhaust.";
                case "Signature Move": return $"Can only be played if this is the only Attack in your hand. Deal {AttackDamage} damage.";
                case "Simmering Fury": return $"At the start of your next turn, enter Wrath and draw {BuffAmount} cards.";
                case "Spirit Shield": return $"Gain {BlockAmount} Block for each card in your hand.";
                case "Study": return $"At the end of your turn, shuffle an Insight into your draw pile.";
                case "Swivel": return $"Gain {BlockAmount} Block. The next Attack you play costs 0.";
                case "Talk to the Hand": return $"Deal {AttackDamage} damage. Whenever you attack this enemy, gain {BuffAmount} Block. Exhaust.";
                case "Tantrum": return $"Deal {AttackDamage} damage {AttackLoops} times. Enter Wrath. Shuffle this card into your draw pile.";
                case "Third Eye": return $"Gain {BlockAmount} Block. Scry {MagicNumber}.";
                case "Tranquility": return $"Retain. Enter Calm. Exhaust.";
                case "Vault": return $"Take an extra turn after this one. End your turn. Exhaust.";
                case "Vigilance": return $"Gain {BlockAmount} Block. Enter Calm.";
                case "Wallop": return $"Deal {AttackDamage} damage. Gain Block equal to unblocked damage dealt.";
                case "Wave of the Hand": return $"Whenever you gain Block this turn, apply {BuffAmount} Weak to ALL enemies.";
                case "Weave": return $"Deal {AttackDamage} damage. Whenever you Scry, return this from the discard pile to your Hand.";
                case "Wheel Kick": return $"Deal {AttackDamage} damage. Draw 2 cards.";
                case "Windmill Strike": return $"Retain. Deal {AttackDamage} damage. When Retained, increase its damage by {MagicNumber} this combat.";
                case "Wish": return $"Choose one: Gain {(Upgraded ? "8" : "6")} Plated Armor, {(Upgraded ? "4" : "3")} Strength, or {(Upgraded ? "30" : "25")} Gold. Exhaust.";
                case "Worship": return $"{(Upgraded ? "Retain " : "")}Gain 5 Mantra.";
                case "Wreath of Flame": return $"Your next Attack deals {BuffAmount} additional damage.";
                case "Apotheosis": return $"Upgrade ALL of your cards for the rest of combat. Exhaust.";
                case "Apparition": return $"{(Upgraded ? "" : "Ethereal. ")}Gain 1 Intangible. Exhaust. Ethereal.";
                case "Bandage Up": return $"Heal {MagicNumber} HP. Exhaust.";
                case "Beta": return $"Shuffle an Omega into your draw pile. Exhaust.";
                case "Bite": return $"Deal {AttackDamage} damage. Heal {MagicNumber} HP.";
                case "Blind": return $"Apply 2 Weak{(Upgraded ? " to ALL enemies." : ".")}";
                case "Chrysalis": return $"Add {MagicNumber} random Skills into your Draw Pile. They cost 0 this combat. Exhaust.";
                case "Dark Shackles": return $"Enemy loses {-1*BuffAmount} Strength for the rest of this turn. Exhaust.";
                case "Deep Breath": return $"Shuffle your discard pile into your draw pile. Draw {(Upgraded ? "2 cards" : "a card")}.";
                case "Discovery": return $"Choose 1 of 3 random cards to add to your hand. It costs 0 this turn. {(Upgraded ? "" : "Exhaust.")}";
                case "Dramatic Entrance": return $"Innate. Deal {AttackDamage} damage to ALL enemies. Exhaust.";
                case "Enlightenment": return $"Reduce the cost of cards in your hand to 1 this turn.";
                case "Expunger": return $"Deal {AttackDamage} damage {AttackLoops} times.";
                case "Finesse": return $"Gain {BlockAmount} Block. Draw 1 card.";
                case "Flash of Steel": return $"Deal {AttackDamage} damage. Draw 1 card.";
                case "Forethought": return $"Place {(Upgraded ? "any number of cards" : "a card")} from your hand on the bottom of your draw pile. {(Upgraded ? "They" : "It")} costs 0 until it is played.";
                case "Good Instincts": return $"Gain {BlockAmount} Block.";
                case "Hand of Greed": return $"Deal {AttackDamage} damage. If Fatal, gain {MagicNumber} Gold.";
                case "Impatience": return $"If you have no Attack cards in your hand, draw {CardsDrawn} cards.";
                case "Insight": return $"Retain. Draw {CardsDrawn} cards. Exhaust.";
                case "J.A.X.": return $"Lose 3 HP. Gain {BuffAmount} Strength.";
                case "Jack Of All Trades": return $"Add {MagicNumber} random Colorless card{(Upgraded ? "s" : "")} to your hand. Exhaust.";
                case "Madness": return $"A random card in your hand costs 0 for the rest of combat. Exhaust.";
                case "Magnetism": return $"At the start of each turn, add a random colorless card to your hand.";
                case "Master of Strategy": return $"Draw {CardsDrawn} cards. Exhaust.";
                case "Mayhem": return $"At the start of your turn, play the top card of your draw pile.";
                case "Metamorphosis": return $"Add {MagicNumber} random Attacks into your Draw Pile. They cost 0 this combat. Exhaust.";
                case "Mind Blast": return $"Innate. Deal damage equal to the number of cards in your draw pile.";
                case "Miracle": return $"Retain. Gain {EnergyGained} Energy. Exhaust.";
                case "Omega": return $"At the start of your turn deal {BuffAmount} damage to ALL enemies.";
                case "Panacea": return $"Gain {BuffAmount} Artifact. Exhaust.";
                case "Panache": return $"Every time you play 5 cards in a single turn, deal {BuffAmount} damage to ALL enemies.";
                case "Panic Button": return $"Gain {BlockAmount} Block. You cannot gain Block from cards for the next 2 turns. Exhaust.";
                case "Purity": return $"Choose and exhaust up to {MagicNumber} cards in your hand. Exhaust.";
                case "Ritual Dagger": return $"Deal {AttackDamage} Damage. If this kills an enemy, permanently increase this card's damage by {MagicNumber}. Exhaust.";
                case "Sadistic Nature": return $"Whenever you apply a Debuff to an enemy, they take {BuffAmount} damage.";
                case "Secret Technique": return $"Choose a Skill from your draw pile and place it into your hand. {(Upgraded ? "" : "Exhaust.")}";
                case "Secret Weapon": return $"Choose an Attack from your draw pile and place it into your hand. {(Upgraded ? "" : "Exhaust.")}";
                case "Shiv": return $"Deal {AttackDamage} damage. Exhaust.";
                case "Smite": return $"Retain. Deal {AttackDamage} damage. Exhaust.";
                case "Swift Strike": return $"Deal {AttackDamage} damage.";
                case "The Bomb": return $"At the end of 3 turns, deal {BuffAmount} damage to ALL enemies.";
                case "Thinking Ahead": return $"Draw 2 cards. Place a card from your hand on top of your draw pile. {(Upgraded ? "" : "Exhaust.")}";
                case "Transmutation": return $"Add X {(Upgraded ? "Upgraded" : "")} random colorless cards into your hand that cost 0 this turn. Exhaust.";
                case "Trip": return $"Apply 2 Vulnerable{(Upgraded ? " to ALL enemies." : ".")}.";
                case "Violence": return $"Place {MagicNumber} random Attack cards from your draw pile into your hand. Exhaust.";
                case "Safety": return $"Retain. Gain {BlockAmount} Block. Exhaust.";
                case "Through Violence": return $"Retain. Deal {AttackDamage} damage. Exhaust.";
                case "Become Almighty": return $"Gain {BuffAmount} Strength.";
                case "Fame and Fortune": return $"Gain {BuffAmount} Gold.";
                case "Live Forever": return $"Gain {BuffAmount} Plated Armor.";
                case "Ascender's Bane": return $"Unplayable. Ethereal. Cannot be removed from your deck.";
                case "Clumsy": return $"Unplayable. Ethereal.";
                case "Decay": return $"Unplayable. At the end of your turn, take 2 damage.";
                case "Doubt": return $"Unplayable. At the end of your turn, gain 1 Weak.";
                case "Injury": return $"Unplayable.";
                case "Necronomicurse": return $"Unplayable. There is no escape from this Curse.";
                case "Normality": return $"Unplayable. You cannot play more than 3 cards this turn.";
                case "Pain": return $"Unplayable. While in hand, lose 1 HP when other cards are played.";
                case "Parasite": return $"Unplayable. If transformed or removed from your deck, lose 3 Max HP.";
                case "Regret": return $"Unplayable. At the end of your turn, lose 1 HP for each card in your hand.";
                case "Writhe": return $"Unplayable. Innate.";
                case "Shame": return $"Unplayable. At the end of your turn, gain 1 Frail.";
                case "Pride": return $"At the end of your turn, place a copy of this card on top of your draw pile.";
                case "Curse of the Bell": return $"Unplayable. Cannot be removed from your deck.";
                case "Burn": return $"Unplayable. At the end of your turn, take {AttackDamage} damage.";
                case "Dazed": return $"Unplayable. Ethereal.";
                case "Wound": return $"Unplayable.";
                case "Slimed": return $"Exhaust.";
                case "Void": return $"Unplayable. Whenever this card is drawn, lose 1 Energy.";
            }
        }


        public void CardAction(Hero hero, List<Enemy> encounter, Random rng)
        {
            // Check to see if the Card is Playable, if not leave function early
            if (Name == "Force Field")
            {
                foreach (string s in hero.Actions)
                    if (s.Contains("Power") && EnergyCost != 0)
                        EnergyCost--;
            }
            if (TmpEnergyCost > hero.Energy)
            {
                Console.WriteLine($"You failed to play {Name}. You need {EnergyCost} Energy to play {Name}.");
                return;
            }
            if (Name == "Clash" && !hero.Hand.All(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Clash as you have something that isn't an Attack in your hand.");
                return;
            }
            if (Name == "Signature Move" && hero.Hand.Any(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Signature Move as you have a different Attack in your hand.");
                return;
            }
            if (Name == "Grand Finale" && hero.DrawPile.Count != 0)
            {
                Console.WriteLine("You can't play Grand Finale because you have cards in your draw pile.");
                return;
            }
            if (getDescription().Contains("Unplay"))
            {
                Console.WriteLine("This card is unplayable, read it's effects to learn more.");
                return;
            }

            // Moves the Card Played from Hand to Designated Location
            if (FindCard(Name, hero.Hand) != null)
            {
                if (getDescription().Contains("Exhaust") || Type == "Status" || Type == "Curse")
                    Exhaust(hero , hero.Hand);
                else if (Type == "Power")
                    hero.Hand.Remove(this);
                else if (Name == "Tantrum")
                {
                    MoveCard(hero.Hand, hero.DrawPile);
                    Shuffle(hero.DrawPile, rng);
                }
                else
                    MoveCard(hero.Hand, hero.DiscardPile);
            }

            // Card Effects begin here
            Console.WriteLine($"You played {Name}.");
            int target = 0, wallop = encounter[target].Hp, xEnergy = hero.Energy;
            string lastCardPlayed = "";
            if (EnergyCost == -1)
                hero.Energy = 0;
            else
                hero.Energy -= TmpEnergyCost;


            // Setup Phase (if target needs to be selected or something happens prior to regular action/action numbers need to be determined

            if (Targetable)
                target = hero.DetermineTarget(encounter);
            if (Name == "Crush Joints" || Name == "Sanctity" || Name == "Follow-Up" || Name == "Sash Whip")
            {
                for (int i = hero.Actions.Count - 1; i >= 0; i--)
                    if (hero.Actions[i].Contains("Played"))
                    {
                        lastCardPlayed = hero.Actions[i];
                        break;
                    }
            }
            switch (Name)
            {
                default: break;
                case "Body Slam":
                    AttackDamage = hero.Block;
                    break;
                case "Burning Pact":
                    Card burningPact = ChooseCard(hero.Hand, "exhaust");
                    burningPact.Exhaust(hero, hero.Hand);
                    break;
                case "Fiend Fire":
                    int fiendFire = 0;
                    for (int i = hero.Hand.Count; i >= 1; i--)
                    {
                        hero.Hand[i - 1].Exhaust(hero, hero.Hand);
                        fiendFire++;
                    }
                    AttackLoops = fiendFire;
                    break;
                case "Heavy Blade":
                    Buff heavyBlade = Actor.FindBuff("Strength", hero.Buffs);
                    if (heavyBlade != null)
                        AttackDamage += 14 + (heavyBlade.Intensity.GetValueOrDefault() * MagicNumber);
                    break;
                case "Perfected Strike":
                    foreach (Card c in hero.Hand)
                    {
                        if (c.Name.Contains("Strike"))
                            AttackDamage += 2;
                    }
                    foreach (Card c in hero.DrawPile)
                    {
                        if (c.Name.Contains("Strike"))
                            AttackDamage += 2;
                    }
                    foreach (Card c in hero.DiscardPile)
                    {
                        if (c.Name.Contains("Strike"))
                            AttackDamage += 2;
                    }
                    break;
                case "Reaper":
                    wallop = 0;
                    foreach (Enemy e in encounter)
                    {
                        wallop += e.Hp;
                    }
                    break;
                case "Second Wind":
                    BlockLoops = 0;
                    foreach (Card c in hero.Hand)
                    {
                        if (c.Type != "Attack")
                        {
                            c.Exhaust(hero, hero.Hand);
                            BlockLoops++;
                        }
                    }
                    break;
                case "Whirlwind":
                    AttackLoops = xEnergy;
                    break;
                case "Finisher":
                    AttackLoops = 0;
                    foreach (string s in hero.Actions)
                        if (s.Contains("Attack"))
                            AttackLoops++;
                    break;
                case "Flechettes":
                    AttackLoops = 0;
                    for (int i = 0; i < hero.Hand.Count; i++)
                    {
                        if (hero.Hand[i].Type == "Skill")
                            AttackLoops++;
                    }
                    break;
                case "Nightmare":
                    Card nightmare = ChooseCard(hero.Hand, "copy");
                    break;
                case "Skewer":
                    AttackLoops = xEnergy;
                    break;
                case "Aggregate":
                    EnergyGained = (hero.DrawPile.Count / MagicNumber);
                    break;
                case "Barrage":
                    AttackLoops = hero.Orbs.Count;
                    break;
                case "Blizzard":
                    foreach (string s in hero.Actions)
                        if (s.Contains("Channel Frost"))
                            AttackDamage += MagicNumber;
                    break;
                case "Double Energy":
                    EnergyGained = hero.Energy * MagicNumber;
                    break;
                case "Fission":
                    CardsDrawn = 0;
                    for(int i = hero.Orbs.Count - 1; i >= 0; i--)
                    {
                        if (Upgraded)
                            hero.Evoke(encounter,i);
                        hero.Orbs.RemoveAt(i);
                        CardsDrawn++;
                    }
                    EnergyGained = CardsDrawn;
                    break;
                case "Melter":
                    encounter[target].Block = 0;
                    break;
                case "Reboot":
                    foreach (Card c in hero.DiscardPile)
                        c.MoveCard(hero.DiscardPile, hero.DrawPile);
                    foreach (Card c in hero.Hand)
                        c.MoveCard(hero.Hand, hero.DrawPile);
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Recycle":
                    Card recycle = ChooseCard(hero.Hand, "exhaust");
                    recycle.Exhaust(hero, hero.Hand);
                    EnergyGained += recycle.EnergyCost;
                    break;
                case "Reinforced Body":
                    BlockLoops = xEnergy;
                    break;
                case "Stack":
                    BlockAmount = hero.DiscardPile.Count + MagicNumber;
                    break;
                case "Tempest":
                    BlockLoops += xEnergy;
                    break;
                case "Bowling Bash":
                    AttackLoops = encounter.Count;
                    break;
                case "Brilliance":
                    foreach (string s in hero.Actions)
                        if (s.Contains("Mantra"))
                            AttackDamage += Int32.Parse(s.Last().ToString());
                    break;
                case "Collect":
                    BuffAmount += xEnergy;
                    break;
                case "Spirit Shield":
                    BlockAmount = hero.Hand.Count * MagicNumber;
                    break;
            }

            //Scry Check
            if (Scrys)
            {
                Scry(hero, MagicNumber);
            }

            // Self Damage Phase
            if (SelfDamage)
            {
                hero.SelfDamage(MagicNumber);
            }

            // Block Phase (with exception for Wallop below)
            if (BlockAmount > 0)
            {
                if ((Name == "Auto-Shields" && hero.Block != 0) || (Name == "Escape Plan" && hero.Hand.Last().Type != "Skill"))
                    hero.CardBlock(0);
                else for (int i = 0; i < BlockLoops; i++)
                        hero.CardBlock(BlockAmount);
            }

            // Damage Phase

            if (AttackAll)
            {
                for (int i = 0; i < AttackLoops; i++)
                    hero.AttackAll(encounter, AttackDamage);
            }

            if (SingleAttack)
            {
                switch (Name)
                {
                    default:
                        for (int i = 0; i < AttackLoops; i++)
                            hero.SingleAttack(encounter[target], AttackDamage);
                        if (Name == "Bane" && Actor.FindBuff("Poison", encounter[target].Buffs) != null)
                            hero.SingleAttack(encounter[target], AttackDamage);
                        break;
                    case "Sword Boomerang":
                        for (int i = 0; i < AttackLoops; i++)
                        {
                            target = rng.Next(0, encounter.Count);
                            hero.SingleAttack(encounter[target], AttackDamage);
                        }
                        break;
                    case "Rip And Tear":
                        for (int i = 0; i < AttackLoops; i++)
                        {
                            target = rng.Next(0, encounter.Count);
                            hero.SingleAttack(encounter[target], AttackDamage);
                        }
                        break;
                    case "Thunder Strike":
                        AttackLoops = 0;
                        foreach (string s in hero.Actions)
                            if (s.Contains("Channel Lightning"))
                                AttackLoops++;
                        for (int i = 0; i < AttackLoops; i++)
                        {
                            target = rng.Next(0, encounter.Count);
                            hero.SingleAttack(encounter[target], AttackDamage);
                        }
                        break;
                }
            }

            // Buff/Debuffs Phase
            if (HeroBuff)
            {
                switch (Name)
                {
                    default:
                        if (Name == "Spot Weakness" && !Enemy.AttackIntents().Contains(encounter[target].Intent)) ;
                        else hero.AddBuff(BuffID, BuffAmount);
                        break;
                    case "Flex":
                        hero.AddBuff(BuffID, BuffAmount);
                        hero.AddBuff(30, BuffAmount);
                        break;
                    case "Doppelganger":
                        hero.AddBuff(BuffID, xEnergy + BuffAmount);
                        hero.AddBuff(45, xEnergy + BuffAmount);
                        break;
                    case "Tools of the Trade":
                        hero.AddBuff(BuffID, BuffAmount);
                        hero.AddBuff(50, BuffAmount);
                        break;
                    case "Wraith Form":
                        hero.AddBuff(BuffID, BuffAmount);
                        hero.AddBuff(53, 1);
                        break;
                    case "Biased Cognition":
                        hero.AddBuff(BuffAmount, BuffID);
                        hero.AddBuff(55, 1);
                        break;
                    case "Reprogram":
                        hero.AddBuff(BuffID, BuffAmount * -1);
                        hero.AddBuff(4, BuffAmount);
                        hero.AddBuff(9, BuffAmount);
                        break;
                    case "Fasting":
                        hero.AddBuff(BuffID, BuffAmount);
                        hero.AddBuff(9, BuffAmount);
                        hero.MaxEnergy = -1;
                        break;
                }
            }

            if (TargetBuff)
            {
                if (Targetable)
                {
                    if (Name == "Go for the Eyes" && !Enemy.AttackIntents().Contains(encounter[target].Intent)) ;
                    else if (Name == "Sash Whip" && !lastCardPlayed.Contains("Attack")) ;
                    else if (Name == "Crush Joints" && !lastCardPlayed.Contains("Skill")) ;
                    else if (Name == "Malaise")
                    {
                        encounter[target].AddBuff(BuffID, (xEnergy + BuffAmount) * -1);
                        encounter[target].AddBuff(2, xEnergy + BuffAmount);
                    }
                    else encounter[target].AddBuff(BuffID, BuffAmount);
                    if (Name == "Uppercut")
                            encounter[target].AddBuff(2, BuffAmount);                     
                    else if (Name == "Corpse Explosion" )
                            encounter[target].AddBuff(43, 1);                                       
                    else if (Name == "Dark Shackles")
                            encounter[target].AddBuff(30, BuffAmount);
                }
                else
                {
                    if (Name == "Bouncing Flask")
                        for (int i = 0; i < MagicNumber; i++)
                            encounter[rng.Next(0, encounter.Count)].AddBuff(BuffID, BuffAmount);
                    else if (Name == "Indignation" && hero.Stance != "Wrath")
                        hero.SwitchStance("Wrath");
                    else foreach (Enemy e in encounter)
                        e.AddBuff(BuffID, BuffAmount);
                    if (Name == "Shockwave")
                        foreach (Enemy e in encounter)
                            e.AddBuff(1, BuffAmount);
                    else if (Name == "Crippling Cloud")
                        foreach (Enemy e in encounter)
                            e.AddBuff(2, 2);
                    else if (Name == "Piercing Wail")
                        foreach (Enemy e in encounter)
                            e.AddBuff(30, BuffAmount);
                }
            }

            // Draw Effects
            while (CardsDrawn > 0)
            {
                if (Name == "Scrawl")
                    CardsDrawn = 10 - hero.Hand.Count;
                else if (Name == "Expertise")
                    CardsDrawn -= hero.Hand.Count;
                else if (Name == "Compile Driver")
                    CardsDrawn = hero.Orbs.Distinct().Count();
                else if (Name == "Deep Breath")
                    Discard2Draw(hero, rng);
                if ((Name == "Impatience" && hero.Hand.Any(x => x.Type == "Attack")) || (Name == "FTL" && hero.Actions.Count > MagicNumber) || (Name == "Sanctity" && lastCardPlayed != "Skill"))
                    break;
                else if (Name == "Inner Peace" && hero.Stance != "Calm")
                {
                    hero.SwitchStance("Calm");
                    break;
                }
                else if (Name == "Scrape")
                {
                    for (int i = 0; i < CardsDrawn; i++)
                    {
                        if (hero.Hand.Count == 10) break;
                        DrawCards(rng,hero, 1);
                        if (hero.Hand.Last().EnergyCost != 0)
                            hero.Hand.Last().Discard(hero, rng);
                    }
                    break;
                }                    
                else DrawCards(rng, hero, CardsDrawn);
                if (Name == "Escape Plan" && hero.Hand.Last().Type == "Skill" && Actor.FindBuff("No Draw", hero.Buffs) != null)
                    hero.CardBlock(BlockAmount);
                break;
            }

            // Gain Turn Energy Phase
            if (EnergyGained > 0)
            {
                if (Name == "Dropkick" && Actor.FindBuff("Vulnerable", encounter[target].Buffs) == null) ;
                else if (Name == "Heel Hook" && Actor.FindBuff("Vulnerable", encounter[target].Buffs) == null) ;
                else if (Name == "Sneaky Strike" && !hero.Actions.Contains("Discard")) ;
                else if (Name == "Sunder" && encounter[target].Hp > 0) ;
                else hero.GainTurnEnergy(EnergyGained);
                if ((Name == "Dropkick" && Actor.FindBuff("Vulnerable", encounter[target].Buffs) != null) || (Name == "Heel Hook" && Actor.FindBuff("Vulnerable", encounter[target].Buffs) != null))
                    DrawCards(rng, hero, CardsDrawn);
            }

            // Post-Damage Effects
            switch (Name)
            {
                // IRONCLAD CARDS (0 - 72)																					
                case "Anger":
                    hero.DiscardPile.Add(new Card(this));
                    break;
                case "Armaments":
                    if (hero.Hand.All(x => x.Upgraded))
                        break;
                    Card armaments = hero.Hand[rng.Next(hero.Hand.Count)];
                    while (armaments.Upgraded)
                        armaments = hero.Hand[rng.Next(hero.Hand.Count)];
                    armaments.UpgradeCard();
                    break;
                case "Berserk":
                    hero.MaxEnergy++;
                    break;
                case "Dual Wield":
                    Card dualWield = new Card();
                    for (int i = 0;i < MagicNumber; i++)
                    {
                        do
                            dualWield = ChooseCard(hero.Hand, "copy");
                        while (dualWield.Type == "Skill");
                        hero.Hand.Add(dualWield);
                    }                   
                    break;
                case "Entrench":
                    hero.Block *= MagicNumber;
                    break;
                case "Exhume":
                    Card exhume = ChooseCard(hero.ExhaustPile, "bring back");
                    hero.ExhaustPile.Remove(exhume);
                    hero.Hand.Add(exhume);
                    break;
                case "Feed":                                                //minion buff add
                    if (encounter[target].Hp <= 0)
                        hero.setMaxHP(MagicNumber);
                    break;
                case "Havoc":
                    Card havoc = hero.DrawPile.Last();
                    havoc.CardAction(hero, encounter, rng);
                    havoc.Exhaust(hero, hero.DrawPile);
                    break;
                case "Headbutt":
                    Card headbutt = ChooseCard(hero.DiscardPile, "send to the top of your drawpile");
                    headbutt.MoveCard(hero.DiscardPile, hero.DrawPile);
                    break;
                case "Immolate":
                    hero.DiscardPile.Add(new Card(Dict.cardL[358]));
                    break;
                case "Infernal Blade":
                    Card infernalBlade = new();
                    while (infernalBlade.Type != "Attack")
                        infernalBlade = new Card(Dict.cardL[rng.Next(0, 73)]);
                    hero.Hand.Add(infernalBlade);
                    infernalBlade.TmpEnergyCost = 0;
                    break;
                case "Limit Break":
                    Buff limitBreak = Actor.FindBuff("Strength", hero.Buffs);
                    if (limitBreak != null)
                        limitBreak.Intensity *= MagicNumber;
                    break;
                case "Power Through":
                    for (int i = 0; i < MagicNumber; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(Dict.cardL[357]));
                        else hero.DiscardPile.Add(new Card(Dict.cardL[357]));
                    }
                    break;
                case "Rampage":
                    AttackDamage += MagicNumber;
                    break;
                case "Reaper":
                    int enemyHPAfter = 0;
                    foreach (Enemy e in encounter)
                    {
                        enemyHPAfter += e.Hp;
                    }
                    hero.CombatHeal(wallop - enemyHPAfter);
                    break;
                case "Reckless Charge":
                    hero.DiscardPile.Add(new Card(Dict.cardL[356]));
                    break;
                case "Sever Soul":
                    foreach (Card c in hero.Hand)
                        if (c.Type != "Attack")
                            c.Exhaust(hero, hero.Hand);
                    break;
                case "Warcry":
                    Card warcry = ChooseCard(hero.Hand, "add to the top of your drawpile");
                    warcry.MoveCard(hero.Hand, hero.DrawPile);
                    break;
                case "Wild Strike":
                    hero.DrawPile.Add(new Card(Dict.cardL[357]));
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Alchemize":
                    hero.Potions.Add(Dict.potionL[rng.Next(0, Dict.potionL.Count)]);
                    break;
                case "Blade Dance":
                    AddShivs(hero, MagicNumber);
                    break;
                case "Bullet Time":
                    foreach (Card c in hero.Hand)
                        c.TmpEnergyCost = 0;
                    break;
                case "Catalyst":
                    Buff catalyst = Actor.FindBuff("Poison", encounter[target].Buffs);
                    if (catalyst != null)
                        catalyst.Intensity *= MagicNumber;
                    break;
                case "Cloak and Dagger":
                    AddShivs(hero, MagicNumber);
                    break;
                case "Distraction":
                    Card distraction = new Card();
                    while (distraction.Type != "Skill")
                        distraction = Dict.cardL[rng.Next(73, 146)];
                    distraction.TmpEnergyCost = 0;
                    hero.Hand.Add(distraction);
                    break;
                case "Glass Knife":
                    AttackDamage -= MagicNumber;
                    break;
                case "Setup":
                    Card setup = ChooseCard(hero.Hand, "add to the top of your drawpile");
                    setup.MoveCard(hero.Hand, hero.DrawPile);
                    setup.TmpEnergyCost = 0;
                    break;
                case "All For One":
                    foreach (Card zeroCost in hero.DiscardPile)
                    {
                        if (zeroCost.EnergyCost == 0 && hero.Hand.Count < 10)
                            zeroCost.MoveCard(hero.Hand, hero.DiscardPile);
                    }
                    break;
                case "Capacitor":
                    hero.OrbSlots += MagicNumber;
                    break;
                case "Claw":
                    foreach (Card claw in hero.Hand.FindAll(x => x.Name == "Claw"))
                        AttackDamage += 2;
                    foreach (Card claw in hero.DiscardPile.FindAll(x => x.Name == "Claw"))
                        AttackDamage += 2;
                    foreach (Card claw in hero.DrawPile.FindAll(x => x.Name == "Claw"))
                        AttackDamage += 2;
                    AttackDamage += 2;
                    break;
                case "Consume":
                    hero.OrbSlots -= 1;
                    while (hero.OrbSlots > hero.Orbs.Count)
                        hero.Orbs.RemoveAt(hero.Orbs.Count - 1);
                    break;
                case "Darkness":
                    if (Upgraded)
                    {
                        foreach (var orb in hero.Orbs)
                        {
                            if (orb is null || orb.Name != "Dark") 
                                continue;
                            orb.Effect += 6;
                            Console.WriteLine($"The {orb.Name} Orb stored 6 more Energy!");
                        }
                    }
                    break;
                case "Dualcast":
                    hero.Evoke(encounter);
                    hero.Evoke(encounter);
                    hero.Orbs.RemoveAt(0);
                    break;
                case "Genetic Algorithm":                               // This card requires updates to combat Deck vs perma Deck to function properly
                    BlockAmount += MagicNumber;
                    break;
                case "Hologram":
                    Card hologram = ChooseCard(hero.DiscardPile, "add into your hand");
                    hero.Hand.Add(hologram);
                    hero.DiscardPile.Remove(hologram);
                    break;
                case "Multi-Cast":
                    for (int i = 0; i < xEnergy+MagicNumber; i++)
                        hero.Evoke(encounter);
                    hero.Orbs.RemoveAt(0);
                    break;
                case "Overclock":
                    hero.DiscardPile.Add(new Card(Dict.cardL[355]));
                    break;
                case "Recursion":
                    Orb recursion = hero.Orbs[0];
                    hero.Evoke(encounter);
                    hero.Orbs.RemoveAt(0);
                    hero.Orbs.Add(recursion);
                    break;
                case "Seek":
                    for (int i = 0; i < MagicNumber;i++)
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(ChooseCard(hero.DrawPile, "add to your hand"));
                    break;
                case "Steam Barrier":
                    BlockAmount--;
                    break;
                case "Streamline":
                    if (EnergyCost != 0)
                        EnergyCost--;
                    break;
                case "TURBO":
                    hero.DiscardPile.Add(Dict.cardL[359]);
                    break;
                case "White Noise":
                    Card whiteNoise = new Card();
                    while (whiteNoise.Type != "Power")
                        whiteNoise = Dict.cardL[rng.Next(146, 219)];
                    whiteNoise.TmpEnergyCost = 0;
                    hero.Hand.Add(whiteNoise);
                    break;
                case "Alpha":
                    hero.DrawPile.Add(new Card(Dict.cardL[334]));
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Blasphemy":
                    hero.SwitchStance("Divinity");
                    break;
                case "Carve Reality":
                    hero.Hand.Add(new Card(Dict.cardL[339]));
                    break;
                case "Conclude":
                    //End Turn
                    break;
                case "Conjure Blade":
                    hero.DrawPile.Add(new Card(Dict.cardL[360]));
                    hero.DrawPile.Last().AttackLoops = xEnergy + MagicNumber;
                    Card.Shuffle(hero.DrawPile, rng);
                    break;
                case "Crescendo":
                    hero.SwitchStance("Wrath");
                    break;
                case "Deceive Reality":
                    hero.Hand.Add(new Card(Dict.cardL[338]));
                    break;
                case "Empty Body":
                    hero.SwitchStance("None");
                    break;
                case "Empty Fist":
                    hero.SwitchStance("None");
                    break;
                case "Empty Mind":
                    hero.SwitchStance("None");
                    break;
                case "Eruption":
                    hero.SwitchStance("Wrath");
                    break;
                case "Evaluate":
                    hero.DrawPile.Add(new Card(Dict.cardL[335]));
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Fear No Evil":
                    if (Enemy.AttackIntents().Contains(encounter[target].Intent))
                        hero.SwitchStance("Calm");
                    break;
                case "Follow-Up":
                    if (lastCardPlayed.Contains("Attack"))
                        hero.Energy += 1;
                    break;
                case "Foreign Influence":
                    Card foreignInfluence = new(ChooseCard(RandomCards("All Heroes",MagicNumber,rng,"Attack"), "add to your hand"));
                    if (Upgraded)
                        foreignInfluence.TmpEnergyCost = 0;
                    hero.Hand.Add(foreignInfluence);
                    break;
                case "Halt":
                    if (hero.Stance == "Wrath")
                        hero.CardBlock(MagicNumber);
                    break;
                case "Judgment":
                    if (encounter[target].Hp <= MagicNumber)
                    {
                        encounter[target].Hp = 0;
                        Console.WriteLine($"The {encounter[target].Name} has been judged!");
                    }
                    break;
                case "Lesson Learned":
                    if (encounter[target].Hp <= 0)
                    {
                        // Upgrade card permanently
                    }
                    break;
                case "Meditate":
                    for (int i = 0 ; i < MagicNumber; i++)
                    {
                        if (hero.Hand.Count == 10) break;
                        Card meditate = ChooseCard(hero.DiscardPile, "add to your hand");
                        meditate.MoveCard(hero.DiscardPile, hero.Hand);
                    }                   
                    hero.SwitchStance("Calm");
                    break;
                case "Omniscience":
                    Card omni = new();
                    do
                        omni = ChooseCard(hero.DrawPile, "play twice");
                    while (omni.getDescription().Contains("Unplayable"));
                    omni.CardAction(hero, encounter,rng);
                    omni.CardAction(hero, encounter,rng);
                    omni.Exhaust(hero, hero.DrawPile);
                    break;
                case "Pray":
                    hero.DrawPile.Add(new Card(Dict.cardL[335]));
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Pressure Points":
                    for (int i = 0; i < encounter.Count; i++)
                        if (Actor.FindBuff("Mark", encounter[i].Buffs) != null)
                            hero.NonAttackDamage(encounter[i], Actor.FindBuff("Mark", encounter[i].Buffs).Intensity.GetValueOrDefault());
                    break;
                case "Reach Heaven":
                    hero.DrawPile.Add(new Card(Dict.cardL[340]));
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Tranquility":
                    hero.SwitchStance("Calm");
                    break;
                case "Vault":
                    // Have to code how to end turn early
                    break;
                case "Vigilance":
                    hero.SwitchStance("Calm");
                    break;
                case "Wallop":
                    wallop -= encounter[target].Hp;
                    hero.CardBlock(wallop);
                    break;
                case "Wish":
                    List<Card> lastWish = new List<Card>();
                    for (int i = 0; i < 3; i++)
                        lastWish.Add(new Card(Dict.cardL[i + 361]));
                    Card wish = ChooseCard(lastWish, "use");
                    if (Upgraded)
                        wish.UpgradeCard();
                    if (wish.Name == "Fame and Fortune")
                        hero.GoldChange(wish.MagicNumber);
                    else wish.CardAction(hero, encounter, rng);
                    break;
                // COLORLESS CARDS (294 - 340)
                case "Apotheosis":
                    foreach (Card c in hero.Hand)
                        c.UpgradeCard();
                    foreach (Card c in hero.DiscardPile)
                        c.UpgradeCard();
                    foreach (Card c in hero.DrawPile)
                        c.UpgradeCard();
                    break;
                case "Bandage Up":
                    hero.HealHP(4);
                    break;
                case "Beta":
                    hero.DrawPile.Add(new Card(Dict.cardL[337]));
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Bite":
                    hero.HealHP(2);
                    break;
                case "Chrysalis":
                    List<Card> chrysalis = new List<Card>(RandomCards(hero.Name, 3, rng));
                    foreach (Card c in chrysalis)
                        c.EnergyCost = 0;
                    hero.DrawPile.AddRange(chrysalis);
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Discovery":
                    Card discovery = new(ChooseCard(RandomCards(hero.Name, MagicNumber, rng), "add to your hand"));
                    discovery.TmpEnergyCost = 0;
                    hero.Hand.Add(discovery);
                    break;
                case "Enlightment":
                    foreach (Card c in hero.Hand)
                        if (EnergyCost > MagicNumber)
                        {
                            if (Upgraded)
                                c.EnergyCost = 1;
                            else c.TmpEnergyCost = 1;
                        }                           
                    break;
                case "Forethought":
                    //add upgraded method that runs until you quit it
                    Card forethought = ChooseCard(hero.Hand, "add to the bottom of your drawpile");
                    forethought.TmpEnergyCost = 0;
                    hero.Hand.Remove(forethought);
                    hero.DrawPile.Prepend(forethought);
                    break;
                case "Jack of All Trades":
                    for (int i = 0; i < MagicNumber; i++)
                    {
                        if (hero.Hand.Count == 10)
                            break;
                        Card jackOfAllTrades = new Card(ChooseCard(RandomCards("Colorless", 3, rng), "add to your hand"));
                        hero.Hand.Add(jackOfAllTrades);
                    }                    
                    break;
                case "Hand of Greed":
                    if (encounter[target].Hp <= 0)
                        hero.GoldChange(MagicNumber);
                    break;
                case "Madness":
                    if (hero.Hand.Count > 0)
                        hero.Hand[rng.Next(hero.Hand.Count)].EnergyCost = 0;
                    break;
                case "Metamorphosis":
                    List<Card> metamorphosis = new List<Card>(RandomCards(hero.Name, 3, rng));
                    foreach (Card c in metamorphosis)
                        c.EnergyCost = 0;
                    hero.DrawPile.AddRange(metamorphosis);
                    Shuffle(hero.DrawPile, rng);
                    break;
                case "Purity":
                    for (int i = 0; i < 3; i++)
                        ChooseCard(hero.Hand, "exhaust").Exhaust(hero, hero.Hand);
                    break;
                case "Ritual Dagger":                                                       // This card requires updates to combat Deck vs perma Deck to function properly
                    if (encounter[target].Hp <= 0)
                        AttackDamage += 3;
                    break;
                case "Secret Technique":
                    List<Card> secretSkill = new();
                    foreach (Card c in hero.DrawPile)
                        if (c.Type == "Skill")
                            secretSkill.Add(c);
                    Card secretSkillChoice = ChooseCard(secretSkill, "add to your hand");
                    hero.Hand.Add(secretSkillChoice);
                    hero.DrawPile.Remove(secretSkillChoice);
                    break;
                case "Secret Weapon":
                    List<Card> secretAttack = new();
                    foreach (Card c in hero.DrawPile)
                        if (c.Type == "Attack")
                            secretAttack.Add(c);
                    Card secretAttackChoice = ChooseCard(secretAttack, "add to your hand");
                    hero.Hand.Add(secretAttackChoice);
                    hero.DrawPile.Remove(secretAttackChoice);
                    break;
                case "Thinking Ahead":
                    Card thinkingAhead = new(ChooseCard(hero.Hand, "add to the top of your drawpile"));
                    thinkingAhead.MoveCard(hero.Hand, hero.DrawPile);
                    break;
                case "Transmutation":
                    for (int i = 0; i < xEnergy+MagicNumber; i++)
                    {
                        if (hero.Hand.Count < 10)
                            hero.Hand.Add(new Card(RandomCards("Colorless", 1, rng)[0]));
                        else hero.DiscardPile.Add(new Card(RandomCards("Colorless", 1, rng)[0]));
                    }
                    break;
                case "Violence":
                    for (int i = 0; i < MagicNumber; i++)
                    {
                        Card violence = new();
                        while (violence.Type != "Attack" && hero.DrawPile.Any(x => x.Type == "Attack"))
                            violence = hero.DrawPile[rng.Next(0, hero.DrawPile.Count)];
                        if (violence == null)
                            break;
                        if (hero.Hand.Count < 10)
                            violence.MoveCard(hero.DrawPile, hero.Hand);
                        else violence.MoveCard(hero.DrawPile, hero.DiscardPile);
                    }
                    break;
                case "Necronomicurse":
                    hero.Hand.Add(new Card(Dict.cardL[346]));
                    break;
                default:
                    break;
            }

            // Orb Channeling Effects
            if (OrbChannels)
            {
                if (Name == "Chill")
                    BlockLoops = encounter.Count;
                for (int i = 0; i < BlockLoops; i++)
                {
                    if (Name == "Rainbow")
                        MagicNumber = i;                   
                    if (Name == "Chaos")
                        hero.ChannelOrb(encounter, rng.Next(0, 4));
                    else hero.ChannelOrb(encounter, MagicNumber);
                }
                if (Name == "Glacier")
                    hero.ChannelOrb(encounter, 1);
            }

            // Discard Effects
            if (Discards)
            {
                if (Name == "All-Out Attack")
                    Discard(hero, rng);
                else if (Name == "Unload")
                    for (int i = hero.Hand.Count-1; i >= 0; i--)
                    {
                        if (hero.Hand[i].Type == "Attack")
                            hero.Hand[i].Discard(hero, rng);
                    }
                else if (Name == "Storm of Steel")
                {
                    MagicNumber = 0;
                    for (int i = hero.Hand.Count - 1; i >= 0; i--)
                    {
                        hero.Hand[i].Discard(hero, rng);
                        MagicNumber++;
                    }
                    AddShivs(hero, MagicNumber);
                    if (Upgraded)
                        foreach (Card c in hero.Hand)
                            c.UpgradeCard();
                }
                else if (Name == "Calculated Gamble")
                {
                    MagicNumber = 0;
                    for (int i = hero.Hand.Count - 1; i >= 0; i--)
                    {
                        hero.Hand[i].Discard(hero,rng);
                        MagicNumber++;
                    }
                    DrawCards( rng, hero, MagicNumber);
                }
                else for (int i = 0; i < MagicNumber; i++)
                        ChooseCard(hero.Hand, "discard").Discard(hero, rng);
                if (FindCards("Eviscerate", hero.Hand) is List<Card> evisCheck && evisCheck != null)
                    foreach (Card eviscerate in evisCheck)
                        if (eviscerate.EnergyCost != 0)
                            eviscerate.EnergyCost--;
            }
            // End of card action, card action being documented in turn list
            hero.Actions.Add($"{Name}-{Type} Played");
        }

        public void UpgradeCard()
        {
            if (Upgraded && Name != "Searing Blow")
                return;
            switch (Name)
            {
                default:
                    break;
                case "Anger":
                    AttackDamage += 2;
                    break;
                case "Barricade":
                    EnergyCost--;
                    break;
                case "Bash":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Battle Trance":
                    CardsDrawn++;
                    break;
                case "Berserk":
                    BuffAmount--;
                    break;
                case "Blood for Blood":
                    EnergyCost--;
                    AttackDamage += 4;
                    break;
                case "Bloodletting":
                    EnergyGained++;
                    break;
                case "Bludgeon":
                    AttackDamage += 10;
                    break;
                case "Body Slam":
                    EnergyCost--;
                    break;
                case "Burning Pact":
                    CardsDrawn++;
                    break;
                case "Carnage":
                    AttackDamage += 8;
                    break;
                case "Clash":
                    AttackDamage += 4;
                    break;
                case "Cleave":
                    AttackDamage += 3;
                    break;
                case "Clothesline":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Combust":
                    BuffAmount += 2;
                    break;
                case "Corruption":
                    EnergyCost--;
                    break;
                case "Dark Embrace":
                    EnergyCost--;
                    break;
                case "Demon Form":
                    BuffAmount++;
                    break;
                case "Disarm":
                    BuffAmount--;
                    break;
                case "Double Tap":
                    BuffAmount++;
                    break;
                case "Dropkick":
                    AttackDamage += 3;
                    break;
                case "Dual Wield":
                    MagicNumber++;
                    break;
                case "Entrench":
                    EnergyCost--;
                    break;
                case "Evolve":
                    BuffAmount++;
                    break;
                case "Exhume":
                    EnergyCost--;
                    break;
                case "Feed":
                    AttackDamage += 2;
                    MagicNumber++;
                    break;
                case "Feel No Pain":
                    BuffAmount++;
                    break;
                case "Fiend Fire":
                    AttackDamage += 3;
                    break;
                case "Fire Breathing":
                    BuffAmount += 4;
                    break;
                case "Flame Barrier":
                    BlockAmount += 4;
                    BuffAmount += 2;
                    break;
                case "Flex":
                    BuffAmount += 2;
                    break;
                case "Ghostly Armor":
                    BlockAmount += 3;
                    break;
                case "Havoc":
                    EnergyCost--;
                    break;
                case "Headbutt":
                    AttackDamage += 3;
                    break;
                case "Heavy Blade":
                    MagicNumber += 2;
                    break;
                case "Hemokinesis":
                    AttackDamage += 5;
                    break;
                case "Immolate":
                    AttackDamage += 7;
                    break;
                case "Impervious":
                    BlockAmount += 10;
                    break;
                case "Infernal Blade":
                    EnergyCost--;
                    break;
                case "Inflame":
                    BuffAmount++;
                    break;
                case "Intimidate":
                    BuffAmount++;
                    break;
                case "Iron Wave":
                    BlockAmount += 2;
                    AttackDamage += 2;
                    break;
                case "Juggernaut":
                    BuffAmount += 2;
                    break;
                case "Metallicize":
                    BuffAmount++;
                    break;
                case "Offering":
                    CardsDrawn += 2;
                    break;
                case "Perfected Strike":
                    MagicNumber++;
                    break;
                case "Pommel Strike":
                    AttackDamage++;
                    CardsDrawn++;
                    break;
                case "Power Through":
                    BlockAmount += 5;
                    break;
                case "Pummel":
                    AttackLoops++;
                    break;
                case "Rage":
                    BuffAmount += 2;
                    break;
                case "Rampage":
                    MagicNumber += 3;
                    break;
                case "Reaper":
                    AttackDamage++;
                    break;
                case "Reckless Charge":
                    AttackDamage += 3;
                    break;
                case "Rupture":
                    BuffAmount++;
                    break;
                case "Searing Blow":
                    MagicNumber++;
                    AttackDamage += MagicNumber + 3;
                    break;
                case "Second Wind":
                    BlockAmount += 2;
                    break;
                case "Seeing Red":
                    EnergyCost--;
                    break;
                case "Sentinel":
                    BlockAmount += 3;
                    EnergyGained++;
                    break;
                case "Sever Soul":
                    AttackDamage += 6;
                    break;
                case "Shockwave":
                    BuffAmount += 2;
                    break;
                case "Shrug It Off":
                    BlockAmount += 3;
                    break;
                case "Spot Weakness":
                    BuffAmount++;
                    break;
                case "Sword Boomerang":
                    AttackLoops++;
                    break;
                case "Thunderclap":
                    AttackDamage += 3;
                    break;
                case "True Grit":
                    BlockAmount += 2;
                    break;
                case "Twin Strike":
                    AttackDamage += 2;
                    break;
                case "Uppercut":
                    BuffAmount++;
                    break;
                case "Warcry":
                    CardsDrawn++;
                    break;
                case "Whirlwind":
                    AttackDamage += 3;
                    break;
                case "Wild Strike":
                    AttackDamage += 5;
                    break;
                case "A Thousand Cuts":
                    BuffAmount++;
                    break;
                case "Accuracy":
                    BuffAmount += 2;
                    break;
                case "Acrobatics":
                    CardsDrawn++;
                    break;
                case "Adrenaline":
                    EnergyGained++;
                    break;
                case "Alchemize":
                    EnergyCost--;
                    break;
                case "All-Out Attack":
                    AttackDamage += 4;
                    break;
                case "Backflip":
                    BlockAmount += 3;
                    break;
                case "Backstab":
                    AttackDamage += 4;
                    break;
                case "Bane":
                    AttackDamage += 3;
                    break;
                case "Blade Dance":
                    MagicNumber++;
                    break;
                case "Blur":
                    BlockAmount += 3;
                    break;
                case "Bouncing Flask":
                    MagicNumber++;
                    break;
                case "Bullet Time":
                    EnergyCost--;
                    break;
                case "Burst":
                    BuffAmount++;
                    break;
                case "Caltrops":
                    BuffAmount += 2;
                    break;
                case "Catalyst":
                    MagicNumber++;
                    break;
                case "Choke":
                    BuffAmount += 2;
                    break;
                case "Cloak And Dagger":
                    MagicNumber++;
                    break;
                case "Concentrate":
                    MagicNumber--;
                    break;
                case "Corpse Explosion":
                    BuffAmount += 3;
                    break;
                case "Crippling Cloud":
                    BuffAmount += 3;
                    break;
                case "Dagger Spray":
                    AttackDamage += 2;
                    break;
                case "Dagger Throw":
                    AttackDamage += 3;
                    break;
                case "Dash":
                    AttackDamage += 3;
                    BlockAmount += 3;
                    break;
                case "Deadly Poison":
                    BuffAmount += 2;
                    break;
                case "Deflect":
                    BlockAmount += 3;
                    break;
                case "Die Die Die":
                    AttackDamage += 4;
                    break;
                case "Distraction":
                    EnergyCost--;
                    break;
                case "Dodge and Roll":
                    BlockAmount += 2;
                    BuffAmount += 2;
                    break;
                case "Doppelganger":
                    BuffAmount++;
                    break;
                case "Endless Agony":
                    AttackDamage += 2;
                    break;
                case "Envenom":
                    EnergyCost--;
                    break;
                case "Escape Plan":
                    BlockAmount += 2;
                    break;
                case "Eviscerate":
                    AttackDamage += 2;
                    break;
                case "Expertise":
                    CardsDrawn++;
                    break;
                case "Finisher":
                    AttackDamage += 2;
                    break;
                case "Flechettes":
                    AttackDamage += 2;
                    break;
                case "Flying Knee":
                    AttackDamage += 3;
                    break;
                case "Footwork":
                    BuffAmount++;
                    break;
                case "Glass Knife":
                    AttackDamage += 4;
                    break;
                case "Grand Finale":
                    AttackDamage += 10;
                    break;
                case "Heel Hook":
                    AttackDamage += 3;
                    break;
                case "Leg Sweep":
                    BuffAmount++;
                    BlockAmount += 3;
                    break;
                case "Malaise":
                    BuffAmount++;
                    break;
                case "Masterful Stab":
                    AttackDamage += 4;
                    break;
                case "Neutralize":
                    AttackDamage++;
                    BuffAmount++;
                    break;
                case "Nightmare":
                    EnergyCost--;
                    break;
                case "Noxious Fumes":
                    BuffAmount++;
                    break;
                case "Outmaneuver":
                    EnergyGained++;
                    break;
                case "Phantasmal Killer":
                    EnergyCost--;
                    break;
                case "Piercing Wail":
                    BuffAmount -= 2;
                    break;
                case "Poisoned Stab":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Predator":
                    AttackDamage += 5;
                    break;
                case "Prepared":
                    CardsDrawn++;
                    MagicNumber++;
                    break;
                case "Quick Slash":
                    AttackDamage += 4;
                    break;
                case "Reflex":
                    CardsDrawn++;
                    break;
                case "Riddle with Holes":
                    AttackDamage++;
                    break;
                case "Setup":
                    EnergyCost--;
                    break;
                case "Skewer":
                    AttackDamage += 3;
                    break;
                case "Slice":
                    AttackDamage += 3;
                    break;
                case "Sneaky Strike":
                    AttackDamage += 4;
                    break;
                case "Sucker Punch":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Survivor":
                    BlockAmount += 3;
                    break;
                case "Tactician":EnergyGained++;
                    break;
                case "Terror":
                    EnergyCost--;
                    break;
                case "Tools of the Trade":
                    EnergyCost--;
                    break;
                case "Unload":
                    AttackDamage += 4;
                    break;
                case "Well-Laid Plans":
                    BuffAmount++;
                    break;
                case "Wraith Form":
                    BuffAmount++;
                    break;
                case "Aggregate":
                    MagicNumber--;
                    break;
                case "All For One":
                    AttackDamage += 4;
                    break;
                case "Amplify":
                    BuffAmount++;
                    break;
                case "Auto-Shields":
                    BlockAmount += 4;
                    break;
                case "Ball Lightning":
                    AttackDamage += 3;
                    break;
                case "Barrage":
                    AttackDamage += 2;
                    break;
                case "Beam Cell":
                    AttackDamage++;
                    BuffAmount++;
                    break;
                case "Biased Cognition":
                    BuffAmount++;
                    break;
                case "Blizzard":
                    MagicNumber++;
                    break;
                case "Boot Sequence":
                    BlockAmount += 3;
                    break;
                case "Buffer":
                    BuffAmount++;
                    break;
                case "Bullseye":
                    AttackDamage += 3;
                    BuffAmount++;
                    break;
                case "Capacitor":
                    MagicNumber++;
                    break;
                case "Chaos":
                    BlockLoops++;
                    break;
                case "Charge Battery":
                    BlockAmount += 3;
                    break;
                case "Claw":
                    AttackDamage += 2;
                    break;
                case "Cold Snap":
                    AttackDamage += 3;
                    break;
                case "Compile Driver":
                    AttackDamage += 3;
                    break;              
                case "Consume":
                    BuffAmount++;
                    break;
                case "Coolheaded":
                    CardsDrawn++;
                    break;
                case "Core Surge":
                    AttackDamage += 4;
                    break;
                case "Creative AI":
                    EnergyCost--;
                    break;
                case "Defragment":
                    BuffAmount++;
                    break;
                case "Doom and Gloom":
                    AttackDamage += 4;
                    break;
                case "Double Energy":
                    EnergyCost--;
                    break;
                case "Dualcast":
                    EnergyCost--;
                    break;
                case "Electrodynamics":
                    BlockLoops++;
                    break;
                case "Equilibrium":
                    BlockAmount += 3;
                    break;
                case "Force Field":
                    BlockAmount += 4;
                    break;
                case "FTL":
                    AttackDamage++;
                    MagicNumber++;
                    break;
                case "Fusion":
                    EnergyCost--;
                    break;
                case "Genetic Algorithm":
                    MagicNumber++;
                    break;
                case "Glacier":
                    BlockAmount += 3;
                    break;
                case "Go for the Eyes":
                    AttackDamage++;
                    BuffAmount++;
                    break;
                case "Heatsinks":
                    BuffAmount++;
                    break;
                case "Hologram":
                    BlockAmount += 2;
                    break;
                case "Hyperbeam":
                    AttackDamage += 8;
                    break;
                case "Leap":
                    BlockAmount += 3;
                    break;
                case "Loop":
                    BuffAmount++;
                    break;
                case "Melter":
                    AttackDamage += 4;
                    break;
                case "Meteor Strike":
                    AttackDamage += 6;
                    break;
                case "Multi-Cast":
                    MagicNumber++;
                    break;
                case "Overclock":
                    CardsDrawn++;
                    break;
                case "Reboot":
                    CardsDrawn += 2;
                    break;
                case "Rebound":
                    AttackDamage += 3;
                    break;
                case "Recursion":
                    EnergyCost--;
                    break;
                case "Recycle":
                    EnergyCost--;
                    break;
                case "Reinforced Body":
                    BlockAmount += 2;
                    break;
                case "Reprogram":
                    BuffAmount++;
                    break;
                case "Rip and Tear":
                    AttackDamage += 2;
                    break;
                case "Scrape":
                    AttackDamage += 3;
                    CardsDrawn++;
                    break;
                case "Seek":
                    MagicNumber++;
                    break;
                case "Self Repair":
                    BuffAmount += 3;
                    break;
                case "Skim":
                    CardsDrawn++;
                    break;
                case "Stack":
                    MagicNumber += 3;
                    break;
                case "Static Discharge":
                    BuffAmount++;
                    break;
                case "Steam Barrier":
                    BlockAmount += 2;
                    break;
                case "Streamline":
                    AttackDamage += 5;
                    break;
                case "Sunder":
                    AttackDamage += 8;
                    break;
                case "Sweeping Beam":
                    AttackDamage += 3;
                    break;
                case "Tempest":
                    BlockLoops++;
                    break;
                case "Thunder Strike":
                    AttackDamage += 2;
                    break;
                case "TURBO":
                    EnergyGained++;
                    break;
                case "White Noise":
                    EnergyCost--;
                    break;
                case "Zap":
                    EnergyCost--;
                    break;
                case "Defend":
                    BlockAmount += 3;
                    break;
                case "Strike":
                    AttackDamage += 3;
                    break;
                case "Bowling Bash":
                    AttackDamage += 3;
                    break;
                case "Brilliance":
                    AttackDamage += 4;
                    break;
                case "Carve Reality":
                    AttackDamage += 4;
                    break;
                case "Collect":
                    BuffAmount++;
                    break;
                case "Conclude":
                    AttackDamage += 4;
                    break;
                case "Conjure Blade":
                    MagicNumber++;
                    break;
                case "Consecrate":
                    AttackDamage += 3;
                    break;
                case "Crescendo":
                    EnergyCost--;
                    break;
                case "Crush Joints":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Cut Through Fate":
                    AttackDamage += 2;
                    MagicNumber++;
                    break;
                case "Deceive Reality":
                    BlockAmount += 3;
                    break;
                case "Deus Ex Machina":
                    MagicNumber++;
                    break;
                case "Devotion":
                    BuffAmount++;
                    break;
                case "Empty Body":
                    BlockAmount += 3;
                    break;
                case "Empty Fist":
                    AttackDamage += 5;
                    break;
                case "Empty Mind":
                    CardsDrawn++;
                    break;
                case "Eruption":
                    EnergyCost--;
                    break;
                case "Evaluate":
                    BlockAmount += 4;
                    break;
                case "Fasting":
                    BuffAmount++;
                    break;
                case "Fear No Evil":
                    AttackDamage += 3;
                    break;
                case "Flurry Of Blows":
                    AttackDamage += 2;
                    break;
                case "Flying Sleeves":
                    AttackDamage += 2;
                    break;
                case "Follow-Up":
                    AttackDamage += 4;
                    break;
                case "Foresight":
                    BuffAmount++;
                    break;
                case "Halt":
                    BlockAmount++;
                    MagicNumber += 5;
                    break;
                case "Indignation":
                    BuffAmount += 2;
                    break;
                case "Inner Peace":
                    CardsDrawn++;
                    break;
                case "Judgment":
                    MagicNumber += 10;
                    break;
                case "Just Lucky":
                    MagicNumber++;
                    AttackDamage++;
                    BlockAmount++;
                    break;
                case "Lesson Learned":
                    AttackDamage += 3;
                    break;
                case "Like Water":
                    BuffAmount += 2;
                    break;
                case "Master Reality":
                    EnergyCost--;
                    break;
                case "Meditate":
                    MagicNumber++;
                    break;
                case "Mental Fortress":
                    BuffAmount += 2;
                    break;
                case "Nirvana":
                    BuffAmount++;
                    break;
                case "Omniscience":
                    EnergyCost--;
                    break;
                case "Perseverance":
                    BlockAmount += 2;
                    MagicNumber++;
                    break;
                case "Pray":
                    BuffAmount++;
                    break;
                case "Pressure Points":
                    BuffAmount += 3;
                    break;
                case "Prostrate":
                    BuffAmount++;
                    break;
                case "Protect":
                    BlockAmount += 4;
                    break;
                case "Ragnarok":
                    AttackDamage++;
                    AttackLoops++;
                    break;
                case "Reach Heaven":
                    AttackDamage += 5;
                    break;
                case "Rushdown":
                    EnergyCost--;
                    break;
                case "Sanctity":
                    BlockAmount += 3;
                    break;
                case "Sands of Time":
                    AttackDamage += 6;
                    break;
                case "Sash Whip":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Scrawl":
                    EnergyCost--;
                    break;
                case "Signature Move":
                    AttackDamage += 10;
                    break;
                case "Simmering Fury":
                    BuffAmount++;
                    break;
                case "Spirit Shield":
                    MagicNumber++;
                    break;
                case "Study":
                    EnergyCost--;
                    break;
                case "Swivel":
                    BlockAmount += 3;
                    break;
                case "Talk to the Hand":
                    AttackDamage += 2;
                    BuffAmount++;
                    break;
                case "Tantrum":
                    AttackLoops++;
                    break;
                case "Third Eye":
                    BlockAmount += 2;
                    MagicNumber += 2;
                    break;
                case "Tranquility":
                    EnergyCost--;
                    break;
                case "Vault":
                    EnergyCost--;
                    break;
                case "Vigilance":
                    BlockAmount += 4;
                    break;
                case "Wallop":
                    AttackDamage += 3;
                    break;
                case "Wave of the Hand":
                    BuffAmount++;
                    break;
                case "Weave":
                    AttackDamage += 2;
                    break;
                case "Wheel Kick":
                    AttackDamage += 5;
                    break;
                case "Windmill Strike":
                    AttackDamage += 3;
                    MagicNumber++;
                    break;
                case "Wreath of Flame":
                    BuffAmount += 3;
                    break;
                case "Bite":
                    AttackDamage++;
                    MagicNumber++;
                    break;
                case "J.A.X.":
                    BuffAmount++;
                    break;
                case "Shiv":
                    AttackDamage += 2;
                    break;
                case "Apotheosis":
                    EnergyCost--;
                    break;
                case "Bandage Up":
                    MagicNumber += 2;
                    break;
                case "Blind":
                    Targetable = false;
                    break;
                case "Dark Shackles":
                    BuffAmount -= 6;
                    break;
                case "Deep Breath":
                    CardsDrawn++;
                    break;
                case "Dramatic Entrance":
                    AttackDamage += 4;
                    break;
                case "Finesse":
                    BlockAmount += 2;
                    break;
                case "Flash of Steel":
                    AttackDamage += 3;
                    break;
                case "Good Instincts":
                    BlockAmount += 3;
                    break;
                case "Jack Of All Trades":
                    MagicNumber++;
                    break;
                case "Madness":
                    EnergyCost--;
                    break;
                case "Magnetism":
                    EnergyCost--;
                    break;
                case "Master of Strategy":
                    CardsDrawn++;
                    break;
                case "Panacea":
                    BuffAmount++;
                    break;
                case "Panache":
                    BuffAmount += 4;
                    break;
                case "Purity":
                    MagicNumber += 2;
                    break;
                case "Sadistic Nature":
                    BuffAmount += 2;
                    break;
                case "Swift Strike":
                    AttackDamage += 3;
                    break;
                case "Transmutation":
                    MagicNumber++;
                    break;
                case "Trip":
                    Targetable = false;
                    break;
                case "Impatience":
                    CardsDrawn++;
                    break;
                case "Panic Button":
                    BlockAmount += 10;
                    break;
                case "Chrysalis":
                    MagicNumber += 2;
                    break;
                case "Hand of Greed":
                    AttackDamage += 5;
                    MagicNumber += 5;
                    break;
                case "Mayhem":
                    EnergyCost--;
                    break;
                case "Metamorphosis":
                    MagicNumber += 2;
                    break;
                case "The Bomb":
                    BuffAmount += 10;
                    break;
                case "Violence":
                    MagicNumber++;
                    break;
                case "Ritual Dagger":
                    MagicNumber += 2;
                    break;
                case "Beta":
                    EnergyCost--;
                    break;
                case "Insight":
                    CardsDrawn++;
                    break;
                case "Miracle":
                    EnergyGained++;
                    break;
                case "Omega":
                    BuffAmount += 10;
                    break;
                case "Safety":
                    BlockAmount += 4;
                    break;
                case "Smite":
                    AttackDamage += 4;
                    break;
                case "Through Violence":
                    AttackDamage += 10;
                    break;
                case "Burn":
                    AttackDamage += 2;
                    break;
                case "Expunger":
                    AttackDamage += 6;
                    break;
                case "Become Almighty":
                    BuffAmount++;
                    break;
                case "Fame and Fortune":
                    MagicNumber += 5;
                    break;
                case "Live Forever":
                    BuffAmount += 2;
                    break;
            }
            Upgraded = true;
        }
    }
}