using STV;
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
        public string EnergyCost { get; set; } // currently string because of X and Unplayable Energy Cost cards, will change in future
        public string Description { get; set; }
        private int AttackDamage { get; set; }
        private int AttackLoops { get; set; }
        private int BlockAmount { get; set; }
        private int BlockLoops { get; set; }
        private int SecondaryEffect { get; set; } // Ironclad self-damage, Silent discards, Defect Orb Channels, and Watcher Scrys, among other misc uses
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
        private bool TurnEnergy { get; set; }
        private bool Upgraded { get; set; }
        private int GoldCost { get; set; }



        //constructors
        public Card()
        {
            this.Name = "Purchased";
            this.GoldCost = 0;
        }

        public Card(string name, string type, string rarity, string energyCost, int FirstEffect = 0, int SecondEffect = 0, bool Targetable = false, bool SingleAttack = false, bool AttackAll = false)
        {
            this.Name = name;
            this.Type = type;
            this.Rarity = rarity;
            this.EnergyCost = energyCost;
            this.Upgraded = false;
            this.AttackDamage = FirstEffect;
            this.AttackLoops = SecondEffect;
            this.Targetable = Targetable;
            this.SingleAttack = SingleAttack;
            this.AttackAll = AttackAll;
        }

        public Card(Card card)
        {
            Random rng = new Random();
            this.Name = card.Name;
            this.Type = card.Type;
            this.Rarity = card.Rarity;
            this.EnergyCost = card.EnergyCost;
            this.GoldCost = Rarity == "Rare" ? rng.Next(135, 166) : Rarity == "Uncommon" ? rng.Next(68, 83) : rng.Next(45, 56);
            this.Upgraded = Upgraded;
            this.AttackDamage = AttackDamage;
            this.AttackLoops = AttackLoops;
            this.Targetable = Targetable;
            this.SingleAttack = SingleAttack;
            this.AttackAll = AttackAll;
        }

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

        // Default comparer for Card type.
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
            if (EnergyCost == "None")
                return $"Name: {Name}{(Upgraded ? "+" : "")}\nType: {Type}\nEffect: {setDescription()}";
            return $"Name: {Name}{(Upgraded ? "+" : "")}\nEnergy Cost: {EnergyCost}\tType: {Type}\nEffect: {setDescription()}";
        }

        public int getGoldCost()
        {
            return GoldCost;
        }

        public static Card FindCard(string cardName, List<Card> list)
        {
            return list.Find(x => x.Name == cardName);
        }

        public void MoveCard(List<Card> from, List<Card> to)
        {
            from.Remove(this);
            to.Add(this);
        }

        //Moving Cards to different List methods
        public void Exhaust(List<Card> exhaustPile, List<Card> leaveThisList)
        {
            MoveCard(leaveThisList, exhaustPile);
            Console.WriteLine($"{Name} has been exhausted.");
        }

        public static void Discard(Hero hero, List<Card> hand, List<Card> discardPile, Card card)
        {
            if (!hand.Any())
                return;
            card.MoveCard(hand, discardPile);
            hero.Actions.Add("Discard");
        }

        public static void Scry(List<Card> drawPile, List<Card> discardPile, List<Card> hand, int amount)
        {
            int scryChoice = -1;
            while (scryChoice != 0 && amount > 0)
            {
                Console.WriteLine($"\nEnter the number of the card you would like to scry into your discard pile or hit 0 to move on.");
                for (int i = 1; i <= amount; i++)
                    Console.WriteLine($"{i}:{drawPile[drawPile.Count - i].Name}");
                while (!Int32.TryParse(Console.ReadLine(), out scryChoice) || scryChoice < 0 || scryChoice > amount)
                    Console.WriteLine("Invalid input, enter again:");
                if (scryChoice > 0)
                {
                    Card scryedCard = drawPile[drawPile.Count - scryChoice];
                    scryedCard.MoveCard(drawPile, discardPile);
                    amount--;
                }
            }
            // Weave Check
            if (Card.FindCard("Weave", discardPile) is Card weave && weave != null && hand.Count < 10)
                weave.MoveCard(discardPile, hand);
        }

        public static List<Card> RandomCards(string type, int count, Random rng)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                switch (type)
                {
                    default:
                        cards.Add(new Card(Dict.cardL[rng.Next(73)]));
                        break;
                    case "Silent":
                        cards.Add(new Card(Dict.cardL[rng.Next(73, 146)]));
                        break;
                    case "Defect":
                        cards.Add(new Card(Dict.cardL[rng.Next(146, 219)]));
                        break;
                    case "Watcher":
                        cards.Add(new Card(Dict.cardL[rng.Next(221, 294)]));
                        break;
                    case "Colorless":
                        cards.Add(new Card(Dict.cardL[rng.Next(297, 332)]));
                        break;
                }
            }
            return cards;
        }

        public static void DrawCards(List<Card> drawPile, List<Card> hand, List<Card> discardPile, Random rng, int cards)
        {
            while (hand.Count < 10)
            {
                if (drawPile.Count == 0)
                    Discard2Draw(drawPile, discardPile, rng);
                if (drawPile.Count == 0)
                    break;
                hand.Add(drawPile[drawPile.Count - 1]);
                drawPile.RemoveAt(drawPile.Count - 1);
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
        public static void Discard2Draw(List<Card> drawPile, List<Card> discardPile, Random rng)
        {
            for (int i = discardPile.Count; i > 0; i--)
                discardPile[i - 1].MoveCard(discardPile, drawPile);
            Shuffle(drawPile, rng);
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

        public string setDescription()
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
                case "Bloodletting": return $"Lose {SelfDamage} HP. Gain {EnergyGained} Energy.";
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
                case "Disarm": return $"Enemy loses {BuffAmount} Strength. Exhaust.";
                case "Double Tap": return $"This turn, your next {(Upgraded ? "2 Attacks are" : "Attack is")} played twice.";
                case "Dropkick": return $"Deal {AttackDamage} damage. If the target is Vulnerable, gain {EnergyGained} Energy and draw {CardsDrawn} card.";
                case "Dual Wield": return $"Create {(Upgraded ? "2 copies" : "a copy")} of an Attack or Power card in your hand.";
                case "Entrench": return $"Double your current Block.";
                case "Evolve": return $"Whenever you draw a Status, draw {BuffAmount} card.";
                case "Exhume": return $"Choose an Exhausted card and put it in your hand. Exhaust.";
                case "Feed": return $"Deal {AttackDamage} damage. If this kills the enemy, gain {SecondaryEffect} permanent Max HP. Exhaust.";
                case "Feel No Pain": return $"Whenever a card is Exhausted, gain {BuffAmount} Block.";
                case "Fiend Fire": return $"Exhaust your hand. Deal {AttackDamage} damage for each Exhausted card. Exhaust.";
                case "Fire Breathing": return $"Whenever you draw a Status or Curse card, deal {BuffAmount} damage to ALL enemies.";
                case "Flame Barrier": return $"Gain {BlockAmount} Block. Whenever you are attacked this turn, deal {BuffAmount} damage to the attacker.";
                case "Flex": return $"Gain {BuffAmount} Strength. At the end of your turn, lose {BuffAmount} Strength.";
                case "Ghostly Armor": return $"Ethereal. Gain {BlockAmount} Block.";
                case "Havoc": return $"Play the top card of your draw pile and Exhaust it.";
                case "Headbutt": return $"Deal {AttackDamage} damage. Place a card from your discard pile on top of your draw pile.";
                case "Heavy Blade": return $"Deal {AttackDamage} damage. Strength affects Heavy Blade {SecondaryEffect} times.";
                case "Hemokinesis": return $"Lose {SecondaryEffect} HP. {AttackDamage} damage.";
                case "Immolate": return $"Deal {AttackDamage} damage to ALL enemies. Shuffle a Burn into your discard pile.";
                case "Impervious": return $"Gain {BlockAmount} Block. Exhaust.";
                case "Infernal Blade": return $"Add a random Attack to your hand. It costs 0 this turn. Exhaust.";
                case "Inflame": return $"Gain {BuffAmount} Strength.";
                case "Intimidate": return $"Apply {BuffAmount} Weak to ALL enemies. Exhaust.";
                case "Iron Wave": return $"Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
                case "Juggernaut": return $"Whenever you gain Block, deal {BuffAmount} damage to a random enemy.";
                case "Limit Break": return $"Double your Strength. Exhaust.";
                case "Metallicize": return $"At the end of your turn, gain {BuffAmount} Block.";
                case "Offering": return $"Lose {SecondaryEffect} HP. Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
                case "Perfected Strike": return $"Deal {AttackDamage} damage. Deals an additional +{SecondaryEffect} damage for ALL of your cards containing Strike.";
                case "Pommel Strike": return $"Deal {AttackDamage} damage. Draw {CardsDrawn} card{(Upgraded ? "s" : "")}.";
                case "Power Through": return $"Add 2 Wounds to your hand. Gain {BlockAmount} Block.";
                case "Pummel": return $"Deal {AttackDamage} damage {AttackLoops} times. Exhaust.";
                case "Rage": return $"Whenever you play an Attack this turn, gain {BuffAmount} Block.";
                case "Rampage": return $"Deal {AttackDamage} damage. Every time this card is played, increase its damage by {SecondaryEffect} for this combat.";
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
                case "True Grit": return $"Gain {BlockAmount} Block. Exhaust a random card from your hand.";
                case "Twin Strike": return $"Deal {AttackDamage} damage twice.";
                case "Uppercut": return $"Deal {AttackDamage} damage. Apply {BuffAmount} Weak. Apply {BuffAmount} Vulnerable.";
                case "Warcry": return $"Draw {CardsDrawn} card{(Upgraded ? "s" : "")}. Place a card from your hand on top of your draw pile. Exhaust.";
                case "Whirlwind": return $"Deal {AttackDamage} damage to ALL enemies X times.";
                case "Wild Strike": return $"Deal {AttackDamage} damage. Shuffle a Wound into your draw pile.";
                case "A Thousand Cuts": return $"Whenever you play a card, deal {BuffAmount} damage to ALL enemies.";
                case "Accuracy": return $"Shivs deal {BuffAmount} additional damage.";
                case "Acrobatics": return $"Draw {CardsDrawn} cards. Discard {SecondaryEffect} card.";
                case "Adrenaline": return $"Gain {EnergyGained} Energy. Draw {CardsDrawn} cards. Exhaust.";
                case "After Image": return $"{(Upgraded ? "Innate. " : "")}Whenever you play a card, gain {BuffAmount} Block.";
                case "Alchemize": return $"Obtain a random potion. Exhaust.";
                case "All-Out Attack": return $"Deal {AttackDamage} damage to ALL enemies. Discard 1 card at random.";
                case "Backflip": return $"Gain {BlockAmount} Block. Draw 2 cards.";
                case "Backstab": return $"Deal {AttackDamage} damage. Innate. Exhaust.";
                case "Bane": return $"Deal {AttackDamage} damage. If the enemy is Poisoned, deal {AttackDamage} damage again.";
                case "Blade Dance": return $"Add 3 Shivs to your hand.";
                case "Blur": return $"Gain {BlockAmount} Block. Block is not removed at the start of your next turn.";
                case "Bouncing Flask": return $"Apply {BuffAmount} Poison to a random enemy {SecondaryEffect} times.";
                case "Bullet Time": return $"You cannot draw any cards this turn. Reduce the cost of cards in your hand to 0 this turn.";
                case "Burst": return $"This turn your next {(Upgraded ? "2 Skills are" : "Skill is")} played twice.";
                case "Calculated Gamble": return $"Discard your hand, then draw that many cards. Exhaust.";
                case "Caltrops": return $"Whenever you are attacked, deal {BuffAmount} damage to the attacker.";
                case "Catalyst": return $"{(Upgraded ? "Triple" : "Double")} an enemy's Poison. Exhaust.";
                case "Choke": return $"Deal {AttackDamage} damage. Whenever you play a card this turn, targeted enemy loses {BuffAmount} HP.";
                case "Cloak And Dagger": return $"Gain {BlockAmount} Block. Add {SecondaryEffect} Shiv to your hand.";
                case "Concentrate": return $"Discard {SecondaryEffect} cards. Gain {EnergyGained} Energy.";
                case "Corpse Explosion": return $"Apply {BuffAmount} Poison. When an enemy dies, deal damage equal to its MAX HP to ALL enemies.";
                case "Crippling Cloud": return $"Apply {BuffAmount} Poison and 2 Weak to ALL enemies. Exhaust.";
                case "Dagger Spray": return $"Deal {AttackDamage} damage to ALL enemies twice.";
                case "Dagger Throw": return $"Deal {AttackDamage} damage. Draw {CardsDrawn} card. Discard {SecondaryEffect} card.";
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
                case "Piercing Wail": return $"ALL enemies lose {BuffAmount} Strength for 1 turn. Exhaust.";
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
                case "Aggregate": return $"Gain 1 Energy for every {SecondaryEffect} cards in your draw pile.";
                case "All For One": return $"Deal {AttackDamage} damage. Put all Cost 0 cards from your discard pile into your hand.";
                case "Amplify": return $"This turn, your next {(Upgraded ? "2 Powers are" : "Power is")} played twice.";
                case "Auto-Shields": return $"If you have 0 Block, gain {BlockAmount} Block.";
                case "Ball Lightning": return $"Deal {AttackDamage} damage. Channel 1 Lightning.";
                case "Barrage": return $"Deal {AttackDamage} damage for each Channeled Orb.";
                case "Beam Cell": return $"Deal {AttackDamage} damage and apply {BuffAmount} Vulnerable.";
                case "Biased Cognition": return $"Gain {BuffAmount} Focus. At the start of each turn, lose 1 Focus.";
                case "Blizzard": return $"Deal damage equal to {SecondaryEffect} times the Frost Channeled this combat to ALL enemies.";
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
                case "FTL": return $"Deal {AttackDamage} damage. If you have played less than {SecondaryEffect} cards this turn, draw 1 card.";
                case "Fusion": return $"Channel 1 Plasma.";
                case "Genetic Algorithm": return $"Gain {BlockAmount} Block. When played, permanently increase this card's Block by {SecondaryEffect}. Exhaust.";
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
                case "Cut Through Fate": return $"Deal {AttackDamage} damage. Scry {SecondaryEffect}. Draw {CardsDrawn} card.";
                case "Deceive Reality": return $"Gain {BlockAmount} Block. Add a Safety to your hand.";
                case "Deus Ex Machina": return $"Unplayable. When you draw this card, add {SecondaryEffect} Miracles to your hand. Exhaust.";
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
                case "Halt": return $"Gain {BlockAmount} Block. If you are in Wrath, gain {SecondaryEffect} additional Block.";
                case "Indignation": return $"If you are in Wrath, apply {BuffAmount} Vulnerable to ALL enemies, otherwise enter Wrath.";
                case "Inner Peace": return $"If you are in Calm, draw{CardsDrawn} cards. Otherwise, enter Calm.";
                case "Judgment": return $"If the enemy has {SecondaryEffect} or less HP, set their HP to 0.";
                case "Just Lucky": return $"Scry {SecondaryEffect}. Gain {BlockAmount} Block. Deal {AttackDamage} damage.";
                case "Lesson Learned": return $"Deal {AttackDamage} damage. If Fatal, Upgrade a random card in your deck. Exhaust.";
                case "Like Water": return $"At the end of your turn, if you are in Calm, gain {BuffAmount} Block.";
                case "Master Reality": return $"Whenever a card is created during combat, Upgrade it.";
                case "Meditate": return $"Put {(Upgraded ? "2 cards" : "a card")} from your discard pile into your hand and Retain. Enter Calm. End your turn.";
                case "Mental Fortress": return $"Whenever you change Stances, gain {BuffAmount} Block.";
                case "Nirvana": return $"Whenever you Scry, gain {BuffAmount} Block.";
                case "Omniscience": return $"Choose a card in your draw pile. Play the chosen card twice and Exhaust it. Exhaust.";
                case "Perseverance": return $"Retain. Gain {BlockAmount} Block. When Retained, increase its Block by {SecondaryEffect} this combat.";
                case "Pray": return $"Gain {SecondaryEffect} Mantra. Shuffle an Insight into your draw pile.";
                case "Pressure Points": return $"Apply {BuffAmount} Mark. ALL enemies lose HP equal to their Mark.";
                case "Prostrate": return $"Gain {SecondaryEffect} Mantra. Gain {BlockAmount} Block.";
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
                case "Third Eye": return $"Gain {BlockAmount} Block. Scry {SecondaryEffect}.";
                case "Tranquility": return $"Retain. Enter Calm. Exhaust.";
                case "Vault": return $"Take an extra turn after this one. End your turn. Exhaust.";
                case "Vigilance": return $"Gain {BlockAmount} Block. Enter Calm.";
                case "Wallop": return $"Deal {AttackDamage} damage. Gain Block equal to unblocked damage dealt.";
                case "Wave of the Hand": return $"Whenever you gain Block this turn, apply {BuffAmount} Weak to ALL enemies.";
                case "Weave": return $"Deal {AttackDamage} damage. Whenever you Scry, return this from the discard pile to your Hand.";
                case "Wheel Kick": return $"Deal {AttackDamage} damage. Draw 2 cards.";
                case "Windmill Strike": return $"Retain. Deal {AttackDamage} damage. When Retained, increase its damage by {SecondaryEffect} this combat.";
                case "Wish": return $"Choose one: Gain {(Upgraded ? "8" : "6")} Plated Armor, {(Upgraded ? "4" : "3")} Strength, or {(Upgraded ? "30" : "25")} Gold. Exhaust.";
                case "Worship": return $"{(Upgraded ? "Retain " : "")}Gain 5 Mantra.";
                case "Wreath of Flame": return $"Your next Attack deals {BuffAmount} additional damage.";
                case "Apotheosis": return $"Upgrade ALL of your cards for the rest of combat. Exhaust.";
                case "Apparition": return $"{(Upgraded ? "" : "Ethereal. ")}Gain 1 Intangible. Exhaust. Ethereal.";
                case "Bandage Up": return $"Heal {SecondaryEffect} HP. Exhaust.";
                case "Beta": return $"Shuffle an Omega into your draw pile. Exhaust.";
                case "Bite": return $"Deal {AttackDamage} damage. Heal {SecondaryEffect} HP.";
                case "Blind": return $"Apply 2 Weak{(Upgraded ? " to ALL enemies." : ".")}";
                case "Chrysalis": return $"Add {SecondaryEffect} random Skills into your Draw Pile. They cost 0 this combat. Exhaust.";
                case "Dark Shackles": return $"Enemy loses {BuffAmount} Strength for the rest of this turn. Exhaust.";
                case "Deep Breath": return $"Shuffle your discard pile into your draw pile. Draw {(Upgraded ? "2 cards" : "a card")}.";
                case "Discovery": return $"Choose 1 of 3 random cards to add to your hand. It costs 0 this turn. {(Upgraded ? "" : "Exhaust.")}";
                case "Dramatic Entrance": return $"Innate. Deal {AttackDamage} damage to ALL enemies. Exhaust.";
                case "Enlightenment": return $"Reduce the cost of cards in your hand to 1 this turn.";
                case "Expunger": return $"Deal {AttackDamage} damage {AttackLoops} times.";
                case "Finesse": return $"Gain {BlockAmount} Block. Draw 1 card.";
                case "Flash of Steel": return $"Deal {AttackDamage} damage. Draw 1 card.";
                case "Forethought": return $"Place {(Upgraded ? "any number of cards" : "a card")} from your hand on the bottom of your draw pile. {(Upgraded ? "They" : "It")} costs 0 until it is played.";
                case "Good Instincts": return $"Gain {BlockAmount} Block.";
                case "Hand of Greed": return $"Deal {AttackDamage} damage. If Fatal, gain {SecondaryEffect} Gold.";
                case "Impatience": return $"If you have no Attack cards in your hand, draw {CardsDrawn} cards.";
                case "Insight": return $"Retain. Draw {CardsDrawn} cards. Exhaust.";
                case "J.A.X.": return $"Lose 3 HP. Gain {BuffAmount} Strength.";
                case "Jack Of All Trades": return $"Add {SecondaryEffect} random Colorless card{(Upgraded ? "s" : "")} to your hand. Exhaust.";
                case "Madness": return $"A random card in your hand costs 0 for the rest of combat. Exhaust.";
                case "Magnetism": return $"At the start of each turn, add a random colorless card to your hand.";
                case "Master of Strategy": return $"Draw {CardsDrawn} cards. Exhaust.";
                case "Mayhem": return $"At the start of your turn, play the top card of your draw pile.";
                case "Metamorphosis": return $"Add {SecondaryEffect} random Attacks into your Draw Pile. They cost 0 this combat. Exhaust.";
                case "Mind Blast": return $"Innate. Deal damage equal to the number of cards in your draw pile.";
                case "Miracle": return $"Retain. Gain {EnergyGained} Energy. Exhaust.";
                case "Omega": return $"At the start of your turn deal {BuffAmount} damage to ALL enemies.";
                case "Panacea": return $"Gain {BuffAmount} Artifact. Exhaust.";
                case "Panache": return $"Every time you play 5 cards in a single turn, deal {BuffAmount} damage to ALL enemies.";
                case "Panic Button": return $"Gain {BlockAmount} Block. You cannot gain Block from cards for the next 2 turns. Exhaust.";
                case "Purity": return $"Choose and exhaust up to {SecondaryEffect} cards in your hand. Exhaust.";
                case "Ritual Dagger": return $"Deal {AttackDamage} Damage. If this kills an enemy, permanently increase this card's damage by {SecondaryEffect}. Exhaust.";
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
                case "Violence": return $"Place {SecondaryEffect} random Attack cards from your draw pile into your hand. Exhaust.";
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
                case "Burn": return $"Unplayable. At the end of your turn, take 2 damage.";
                case "Dazed": return $"Unplayable. Ethereal.";
                case "Wound": return $"Unplayable.";
                case "Slimed": return $"Exhaust.";
                case "Void": return $"Unplayable. Whenever this card is drawn, lose 1 Energy.";
            }
        }


        public void CardAction(Hero hero, List<Enemy> encounter, List<Card> drawPile, List<Card> discardPile, List<Card> hand, List<Card> exhaustPile, Random rng)
        {
            // Check to see if the Card is Playable, if not leave function early
            if (Name == "Eviscerate" || Name == "Force Field" || Name == "Sands of Time")
            {
                switch (Name)
                {
                    default:
                        foreach (string s in hero.Actions)
                            if (s.Contains("Discard") && EnergyCost != "0")
                                EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
                        break;

                    case "Force Field":
                        foreach (string s in hero.Actions)
                            if (s.Contains("Power") && EnergyCost != "0")
                                EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
                        break;
                    case "Sands of Time":
                        foreach (string s in hero.Actions)
                            if (s == "Sands of Time Retained" && EnergyCost != "0")
                                EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
                        break;
                }
            }
            if (Int32.Parse(EnergyCost) > hero.Energy)
            {
                Console.WriteLine($"You failed to play {Name}. You need {EnergyCost} Energy to play {Name}.");
                return;
            }
            if (Name == "Clash" && !hand.All(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Clash as you have something that isn't an Attack in your hand.");
                return;
            }
            if (Name == "Signature Move" && hand.Any(x => x.Type == "Attack"))
            {
                Console.WriteLine("You can't play Signature Move as you have a different Attack in your hand.");
                return;
            }
            if (Name == "Grand Finale" && drawPile.Count != 0)
            {
                Console.WriteLine("You can't play Grand Finale because you have cards in your draw pile.");
                return;
            }
            if (setDescription().Contains("Unplay"))
            {
                Console.WriteLine("You can't play this card, read it's effects to learn more.");
                return;
            }

            // Moves the Card Played from Hand to Designated Location
            if (FindCard(Name, hand) != null)
            {
                if (setDescription().Contains("Exhaust") || Type == "Status" || Type == "Curse")
                    Exhaust(exhaustPile, hand);
                else if (Type == "Power")
                    hand.Remove(this);
                else if (Name == "Tantrum")
                {
                    MoveCard(hand, drawPile);
                    Shuffle(drawPile, rng);
                }
                else
                    MoveCard(hand, discardPile);
            }

            // Card Effects begin here
            Console.WriteLine($"You played {Name}.");
            int target = 0, wallop = encounter[target].Hp, xEnergy = hero.Energy;
            string lastCardPlayed = "";
            if (EnergyCost == "X")
                hero.Energy = 0;
            else hero.Energy -= Int32.Parse(EnergyCost);


            // Setup Phase (if target needs to be selected or something happens prior to normal damage/block

            if (Targetable)
                target = hero.DetermineTarget(encounter);
            if (Name == "Crush Joints" || Name == "Sanctity" || Name == "Follow-Up")
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
                    Card burningPact = ChooseCard(hand, "exhaust");
                    burningPact.Exhaust(exhaustPile, hand);
                    break;
                case "Fiend Fire":
                    int fiendFire = AttackDamage;
                    for (int i = hand.Count; i >= 1; i--)
                    {
                        hand[i - 1].Exhaust(exhaustPile, hand);
                        AttackDamage += fiendFire;
                    }
                    AttackDamage -= fiendFire;
                    break;
                case "Heavy Blade":
                    Buff heavyBlade = Actor.FindBuff("Strength", hero.Buffs);
                    if (heavyBlade != null)
                        AttackDamage += 14 + (heavyBlade.Intensity.GetValueOrDefault() * 3);
                    break;
                case "Perfected Strike":
                    foreach (Card c in hand)
                    {
                        if (c.Name.Contains("Strike"))
                            AttackDamage += 2;
                    }
                    foreach (Card c in drawPile)
                    {
                        if (c.Name.Contains("Strike"))
                            AttackDamage += 2;
                    }
                    foreach (Card c in discardPile)
                    {
                        if (c.Name.Contains("Strike"))
                            AttackDamage += 2;
                    }
                    break;
                case "Rampage":
                    foreach (string s in hero.Actions)
                        if (s.Contains("Rampage"))
                            AttackDamage += 5;
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
                    foreach (Card c in hand)
                    {
                        if (c.Type != "Attack")
                        {
                            c.Exhaust(exhaustPile, hand);
                            AttackLoops++;
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
                    for (int i = 0; i < hand.Count; i++)
                    {
                        if (hand[i].Type == "Skill")
                            AttackLoops++;
                    }
                    break;
                case "Nightmare":
                    Card nightmare = ChooseCard(hand, "copy");
                    break;
                case "Skewer":
                    AttackLoops = xEnergy;
                    break;
                case "Aggregate":
                    EnergyGained = (drawPile.Count / SecondaryEffect);
                    break;
                case "Barrage":
                    AttackLoops = hero.Orbs.Count;
                    break;
                case "Blizzard":
                    foreach (string s in hero.Actions)
                        if (s.Contains("Channel Frost"))
                            AttackDamage += SecondaryEffect;
                    break;
                case "Double Energy":
                    EnergyGained = hero.Energy * 2;
                    break;
                case "Fission":
                    CardsDrawn = 0;
                    foreach (var Orb in hero.Orbs)
                    {
                        hero.Orbs.Remove(Orb);
                        CardsDrawn++;
                    }
                    EnergyGained = CardsDrawn;
                    break;
                case "Melter":
                    encounter[target].Block = 0;
                    break;
                case "Reboot":
                    foreach (Card c in discardPile)
                        c.MoveCard(discardPile, drawPile);
                    foreach (Card c in hand)
                        c.MoveCard(hand, drawPile);
                    Shuffle(drawPile, rng);
                    break;
                case "Recycle":
                    Card recycle = ChooseCard(hand, "exhaust");
                    recycle.Exhaust(exhaustPile, hand);
                    EnergyGained = Int32.Parse(recycle.EnergyCost);
                    break;
                case "Reinforced Body":
                    BlockLoops = xEnergy;
                    break;
                case "Stack":
                    BlockAmount = discardPile.Count;
                    break;
                case "Tempest":
                    BlockLoops = xEnergy;
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
                    BuffAmount = xEnergy;
                    break;
                case "Perseverance":
                    foreach (string s in hero.Actions)
                        if (s == "Perseverance Retained")
                            BlockAmount += SecondaryEffect;
                    break;
                case "Spirit Shield":
                    BlockAmount = hand.Count * 3;
                    break;
                case "Windmill Strike":
                    foreach (string s in hero.Actions)
                        if (s == "Windmill Strike Retained")
                            AttackDamage += SecondaryEffect;
                    break;
            }

            //Scry Check
            if (Scrys)
            {
                Scry(drawPile, discardPile, hand, SecondaryEffect);
            }

            // Self Damage Phase
            if (SelfDamage)
            {
                hero.SelfDamage(SecondaryEffect);
            }

            // Block Phase (with exception for Wallop below)
            if (BlockAmount > 0)
            {
                if ((Name == "Auto-Shields" && hero.Block != 0) || (Name == "Escape Plan" && hand[hand.Count - 1].Type != "Skill"))
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
                switch (Name)
                {
                    default:
                        if (Name == "Go for the Eyes" && !Enemy.AttackIntents().Contains(encounter[target].Intent)) ;
                        else encounter[target].AddBuff(BuffID, BuffAmount);
                        break;
                    case "Intimidate":
                        foreach (var enemy in encounter)
                            enemy.AddBuff(BuffID, BuffAmount);
                        break;
                    case "Shockwave":
                        foreach (var enemy in encounter)
                        {
                            enemy.AddBuff(BuffID, BuffAmount);
                            enemy.AddBuff(1, BuffAmount);
                        }
                        break;
                    case "Thunderclap":
                        foreach (var enemy in encounter)
                            enemy.AddBuff(BuffID, BuffAmount);
                        break;
                    case "Uppercut":
                        encounter[target].AddBuff(BuffID, BuffAmount);
                        encounter[target].AddBuff(2, BuffAmount);
                        break;
                    case "Bouncing Flask":
                        for (int i = 0; i < SecondaryEffect; i++)
                        {
                            target = rng.Next(0, encounter.Count);
                            encounter[target].AddBuff(BuffID, BuffAmount);
                        }
                        break;
                    case "Corpse Explosion":
                        encounter[target].AddBuff(BuffID, BuffAmount);
                        encounter[target].AddBuff(43, 1);
                        break;
                    case "Crippling Cloud":
                        foreach (Enemy enemy in encounter)
                        {
                            enemy.AddBuff(BuffID, BuffAmount);
                            enemy.AddBuff(2, 2);
                        }
                        break;
                    case "Malaise":
                        encounter[target].AddBuff(BuffID, xEnergy + BuffAmount);
                        encounter[target].AddBuff(2, xEnergy + BuffAmount);
                        break;
                    case "Piercing Wail":
                        for (int i = 0; i < encounter.Count; i++)
                        {
                            encounter[i].AddBuff(BuffID, BuffAmount);
                            encounter[i].AddBuff(30, BuffAmount);
                        }
                        break;
                    case "Crush Joints":

                        for (int i = hero.Actions.Count - 1; i >= 0; i--)
                            if (hero.Actions[i].Contains("Played"))
                            {
                                lastCardPlayed = hero.Actions[i];
                                break;
                            }
                        if (lastCardPlayed.Contains("Skill"))
                            encounter[target].AddBuff(BuffID, BuffAmount);
                        break;
                    case "Indignation":
                        if (hero.Stance == "Wrath")
                            foreach (var enemy in encounter)
                                enemy.AddBuff(BuffID, BuffAmount);
                        else hero.SwitchStance("Wrath", discardPile, hand);
                        break;
                    case "Sash Whip":
                        string sashWhip = "";
                        for (int i = hero.Actions.Count - 1; i >= 0; i--)
                            if (hero.Actions[i].Contains("Played"))
                            {
                                sashWhip = hero.Actions[i];
                                break;
                            }
                        if (sashWhip.Contains("Attack"))
                            encounter[target].AddBuff(BuffID, BuffAmount);
                        break;
                    case "Dark Shackles":
                        encounter[target].AddBuff(BuffID, BuffAmount);
                        encounter[target].AddBuff(30, BuffAmount);
                        break;
                    case "Deep Breath":
                        for (int i = discardPile.Count; i > 0; i--)
                        {
                            Card deepBreath = discardPile[i - 1];
                            deepBreath.MoveCard(discardPile, drawPile);
                        }
                        Shuffle(drawPile, rng);
                        break;
                }
            }
            // Draw Effects
            while (CardsDrawn > 0)
            {
                if (Name == "Scrawl")
                    CardsDrawn = 10 - hand.Count;
                if (Name == "Expertise")
                    CardsDrawn -= hand.Count;
                if (Name == "Compile Driver")
                    CardsDrawn = hero.Orbs.Distinct().Count();
                if ((Name == "Impatience" && hand.Any(x => x.Type == "Attack")) || (Name == "FTL" && hero.Actions.Count > 3) || (Name == "Sanctity" && lastCardPlayed != "Skill"))
                    break;
                if (Name == "Inner Peace" && hero.Stance != "Calm")
                {
                    hero.SwitchStance("Calm", discardPile, hand);
                    break;
                }
                if (Name == "Scrape")
                    for (int i = 0; i < CardsDrawn; i++)
                    {
                        DrawCards(drawPile, hand, discardPile, rng, 1);
                        if (hand[hand.Count - 1].EnergyCost != "0")
                            Discard(hero, hand, discardPile, hand[hand.Count - 1]);
                    }
                else DrawCards(drawPile, hand, discardPile, rng, CardsDrawn);
                if (Name == "Escape Plan" && hand.Last().Type == "Skill" && Actor.FindBuff("No Draw", hero.Buffs) != null)
                    hero.CardBlock(BlockAmount);
                break;
            }

            // Gain Turn Energy Phase
            if (TurnEnergy)
            {
                switch (Name)
                {
                    default:
                        hero.GainTurnEnergy(EnergyGained);
                        break;
                    case "Dropkick":
                        if (Actor.FindBuff("Vulnerable", encounter[target].Buffs) != null)
                        {
                            hero.GainTurnEnergy(EnergyGained);
                            DrawCards(drawPile, hand, discardPile, rng, CardsDrawn);
                        }								
                        break;
                    case "Heel Hook":
                        if (Actor.FindBuff("Weak", encounter[target].Buffs) != null)
                        {
                            hero.GainTurnEnergy(EnergyGained);
                            DrawCards(drawPile, hand, discardPile, rng, CardsDrawn);
                        }
                        break;
                    case "Sneaky Strike":
                        if (hero.Actions.Contains("Discard"))
                            hero.GainTurnEnergy(EnergyGained);
                        break;
                    case "Sunder":
                        if (encounter[target].Hp <= 0)
                            hero.GainTurnEnergy(EnergyGained);
                        break;
                }
            }

            // Post-Damage Effects
            switch (Name)
            {
                // IRONCLAD CARDS (0 - 72)																					
                case "Anger":
                    discardPile.Add(new Card(Dict.cardL[0]));
                    break;
                case "Armaments":
                    //Upgrade card
                    break;
                case "Berserk":
                    hero.GainBattleEnergy(1);
                    break;
                case "Dual Wield":
                    Card dualWield = new Card();
                    do
                        dualWield = ChooseCard(hand, "copy");
                    while (dualWield.Type == "Skill");
                    hand.Add(dualWield);
                    break;
                case "Entrench":
                    hero.Block *= 2;
                    break;
                case "Exhume":
                    Card exhume = ChooseCard(exhaustPile, "bring back");
                    exhaustPile.Remove(exhume);
                    hand.Add(exhume);
                    break;
                case "Feed":                                                //minion buff add
                    if (encounter[target].Hp <= 0)
                    {
                        hero.HealHP(3);
                        hero.MaxHP += 3;
                    }
                    break;
                case "Havoc":
                    Card havoc = drawPile.Last();
                    havoc.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                    havoc.Exhaust(exhaustPile, drawPile);
                    break;
                case "Headbutt":
                    Card headbutt = ChooseCard(discardPile, "send to the top of your drawpile");
                    discardPile.Remove(headbutt);
                    drawPile.Add(headbutt);
                    break;
                case "Immolate":
                    discardPile.Add(new Card(Dict.cardL[358]));
                    break;
                case "Infernal Blade":
                    Card infernalBlade = new Card();
                    while (infernalBlade.Type != "Attack")
                        infernalBlade = Dict.cardL[rng.Next(0, 73)];
                    hand.Add(infernalBlade);
                    infernalBlade.EnergyCost = "0";
                    break;
                case "Limit Break":
                    Buff limitBreak = Actor.FindBuff("Strength", hero.Buffs);
                    if (limitBreak != null)
                        limitBreak.Intensity *= 2;
                    break;
                case "Power Through":
                    for (int i = 0; i < 2; i++)
                    {
                        if (hand.Count < 10)
                            hand.Add(new Card(Dict.cardL[357]));
                        else discardPile.Add(new Card(Dict.cardL[357]));
                    }
                    break;
                case "Reaper":
                    int enemyHPAfter = 0;
                    foreach (Enemy e in encounter)
                    {
                        enemyHPAfter += e.Hp;
                    }
                    hero.HealHP(wallop - enemyHPAfter);
                    break;
                case "Reckless Charge":
                    discardPile.Add(new Card(Dict.cardL[356]));
                    break;
                case "Sever Soul":
                    foreach (Card c in hand)
                        if (c.Type != "Attack")
                            c.Exhaust(exhaustPile, hand);
                    break;
                case "Warcry":
                    Card warcry = ChooseCard(hand, "add to the top of your drawpile");
                    warcry.MoveCard(hand, drawPile);
                    break;
                case "Wild Strike":
                    drawPile.Add(new Card(Dict.cardL[357]));
                    Shuffle(drawPile, rng);
                    break;
                case "Alchemize":
                    hero.Potions.Add(Dict.potionL[rng.Next(0, Dict.potionL.Count)]);
                    break;
                case "Blade Dance":
                    for (int i = 0; i < 3; i++)
                    {
                        if (hand.Count < 10)
                            hand.Add(new Card(Dict.cardL[317]));
                        else discardPile.Add(new Card(Dict.cardL[317]));
                    }
                    break;
                case "Bullet Time":
                    foreach (Card c in hand)
                        c.EnergyCost = "0";
                    break;
                case "Catalyst":
                    Buff catalyst = Actor.FindBuff("Poison", encounter[target].Buffs);
                    if (catalyst != null)
                        catalyst.Intensity *= SecondaryEffect;
                    break;
                case "Cloak and Dagger":
                    if (hand.Count < 10)
                        hand.Add(new Card(Dict.cardL[317]));
                    else discardPile.Add(new Card(Dict.cardL[317]));
                    break;
                case "Distraction":
                    Card distraction = new Card();
                    while (distraction.Type != "Skill")
                        distraction = Dict.cardL[rng.Next(73, 146)];
                    distraction.EnergyCost = "0";
                    hand.Add(distraction);
                    break;
                case "Glass Knife":
                    AttackDamage -= 2;
                    break;
                case "Setup":
                    Card setup = ChooseCard(hand, "add to the top of your drawpile");
                    setup.MoveCard(hand, drawPile);
                    setup.EnergyCost = "0";
                    break;
                case "All For One":
                    foreach (Card zeroCost in discardPile)
                    {
                        if (zeroCost.EnergyCost == "0" && hand.Count < 10)
                        {
                            hand.Add(zeroCost);
                            discardPile.Remove(zeroCost);
                        }
                    }
                    break;
                case "Capacitor":
                    hero.OrbSlots += BuffAmount;
                    break;
                case "Claw":
                    foreach (Card claw in hand.FindAll(x => x.Name == "Claw"))
                        AttackDamage += 2;
                    foreach (Card claw in discardPile.FindAll(x => x.Name == "Claw"))
                        AttackDamage += 2;
                    foreach (Card claw in drawPile.FindAll(x => x.Name == "Claw"))
                        AttackDamage += 2;
                    AttackDamage += 2;
                    break;
                case "Consume":
                    hero.OrbSlots -= 1;
                    while (hero.OrbSlots > hero.Orbs.Count)
                        hero.Orbs.RemoveAt(hero.Orbs.Count - 1);
                    break;
                case "Dualcast":
                    hero.Evoke(encounter);
                    hero.Evoke(encounter);
                    hero.Orbs.RemoveAt(0);
                    break;
                case "Genetic Algorithm":                               // This card requires updates to combat Deck vs perma Deck to function properly
                    BlockAmount += 2;
                    break;
                case "Hologram":
                    Card hologram = ChooseCard(discardPile, "add into your hand");
                    hand.Add(hologram);
                    discardPile.Remove(hologram);
                    break;
                case "Multi-Cast":
                    for (int i = 0; i < xEnergy; i++)
                        hero.Evoke(encounter);
                    hero.Orbs.RemoveAt(0);
                    break;
                case "Overclock":
                    discardPile.Add(new Card(Dict.cardL[355]));
                    break;
                case "Recursion":
                    Orb recursion = hero.Orbs[0];
                    hero.Evoke(encounter);
                    hero.Orbs.RemoveAt(0);
                    hero.Orbs.Add(recursion);
                    break;
                case "Seek":
                    hand.Add(ChooseCard(drawPile, "add to your hand"));
                    break;
                case "Steam Barrier":
                    BlockAmount--;
                    break;
                case "Streamline":
                    if (EnergyCost != "0")
                        EnergyCost = $"{Int32.Parse(EnergyCost) - 1}";
                    break;
                case "TURBO":
                    discardPile.Add(Dict.cardL[359]);
                    break;
                case "White Noise":
                    Card whiteNoise = new Card();
                    while (whiteNoise.Type != "Power")
                        whiteNoise = Dict.cardL[rng.Next(146, 219)];
                    whiteNoise.EnergyCost = "0";
                    hand.Add(whiteNoise);
                    break;
                case "Alpha":
                    drawPile.Add(new Card(Dict.cardL[334]));
                    Shuffle(drawPile, rng);
                    break;
                case "Blasphemy":
                    hero.SwitchStance("Divinity", discardPile, hand);
                    break;
                case "Carve Reality":
                    hand.Add(new Card(Dict.cardL[339]));
                    break;
                case "Conclude":
                    //End Turn
                    break;
                case "Conjure Blade":
                    drawPile.Add(new Card(Dict.cardL[360]));
                    drawPile[drawPile.Count - 1].AttackLoops = xEnergy;
                    break;
                case "Crescendo":
                    hero.SwitchStance("Wrath", discardPile, hand);
                    break;
                case "Deceive Reality":
                    hand.Add(new Card(Dict.cardL[338]));
                    break;
                case "Deus Ex Machina":
                    for (int i = 0; i < SecondaryEffect; i++)
                    {
                        if (hand.Count < 10)
                            hand.Add(new Card(Dict.cardL[336]));
                        else discardPile.Add(new Card(Dict.cardL[336]));
                    }
                    Exhaust(exhaustPile, hand);
                    break;
                case "Empty Body":
                    hero.SwitchStance("None", discardPile, hand);
                    break;
                case "Empty Fist":
                    hero.SwitchStance("None", discardPile, hand);
                    break;
                case "Empty Mind":
                    hero.SwitchStance("None", discardPile, hand);
                    break;
                case "Eruption":
                    hero.SwitchStance("Wrath", discardPile, hand);
                    break;
                case "Evaluate":
                    drawPile.Add(new Card(Dict.cardL[335]));
                    Shuffle(drawPile, rng);
                    break;
                case "Fear No Evil":
                    if (Enemy.AttackIntents().Contains(encounter[target].Intent))
                        hero.SwitchStance("Calm", discardPile, hand);
                    break;
                case "Follow-Up":
                    if (lastCardPlayed.Contains("Attack"))
                        hero.Energy += 1;
                    break;
                case "Foreign Influence":
                    Card fore, ign, infl = new Card();
                    List<Card> fi = new(4);
                    for (int i = 0; i < 3; i++)
                    {
                        Card uence = new Card();
                        while (uence.Type != "Attack")
                            uence = Dict.cardL[rng.Next(0, 291)];
                        switch (i)
                        {
                            case 0: fore = uence; fi.Add(fore); break;
                            case 1: ign = uence; fi.Add(ign); break;
                            case 2: infl = uence; fi.Add(infl); break;
                        }
                    }
                    hand.Add(new(ChooseCard(fi, "add to your hand")));
                    break;
                case "Halt":
                    if (hero.Stance == "Wrath")
                        hero.CardBlock(SecondaryEffect);
                    break;
                case "Judgment":
                    if (encounter[target].Hp <= SecondaryEffect)
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
                    Card meditate = ChooseCard(discardPile, "add to your hand");
                    hand.Add(meditate);
                    discardPile.Remove(meditate);
                    //meditate.Description += "Retain.";			Have to fix how descriptions work
                    hero.SwitchStance("Calm", discardPile, hand);
                    break;
                case "Omniscience":
                    Card omni = new();
                    do
                        omni = ChooseCard(drawPile, "play twice");
                    while (omni.setDescription().Contains("Unplayable"));
                    omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                    omni.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                    omni.Exhaust(exhaustPile, drawPile);
                    break;
                case "Pray":
                    drawPile.Add(new Card(Dict.cardL[335]));
                    Shuffle(drawPile, rng);
                    break;
                case "Pressure Points":
                    for (int i = 0; i < encounter.Count; i++)
                        if (Actor.FindBuff("Mark", encounter[i].Buffs) != null)
                            hero.NonAttackDamage(encounter[i], Actor.FindBuff("Mark", encounter[i].Buffs).Intensity.GetValueOrDefault());
                    break;
                case "Reach Heaven":
                    drawPile.Add(new Card(Dict.cardL[340]));
                    Shuffle(drawPile, rng);
                    break;
                case "Tranquility":
                    hero.SwitchStance("Calm", discardPile, hand);
                    break;
                case "Vault":
                    // Have to code how to end turn early
                    break;
                case "Vigilance":
                    hero.SwitchStance("Calm", discardPile, hand);
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
                    wish.CardAction(hero, encounter, drawPile, discardPile, hand, exhaustPile, rng);
                    break;
                // COLORLESS CARDS (294 - 340)
                case "Apotheosis":
                    break;
                case "Bandage Up":
                    hero.HealHP(4);
                    break;
                case "Beta":
                    drawPile.Add(new Card(Dict.cardL[337]));
                    Shuffle(drawPile, rng);
                    break;
                case "Bite":
                    hero.HealHP(2);
                    break;
                case "Chrysalis":
                    List<Card> chrysalis = new List<Card>(RandomCards(hero.Name, 3, rng));
                    foreach (Card c in chrysalis)
                        c.EnergyCost = "0";
                    drawPile.AddRange(chrysalis);
                    Shuffle(drawPile, rng);
                    break;
                case "Discovery":
                    Card discovery = new(ChooseCard(RandomCards(hero.Name, 3, rng), "add to your hand"));
                    discovery.EnergyCost = "0";
                    hand.Add(discovery);
                    break;
                case "Enlightment":
                    foreach (Card c in hand)
                        if (Int32.Parse(c.EnergyCost) > 1)
                            c.EnergyCost = "1";
                    break;
                case "Forethought":
                    Card forethought = ChooseCard(hand, "add to the bottom of your drawpile");
                    forethought.EnergyCost = "0";
                    hand.Remove(forethought);
                    drawPile.Prepend(forethought);
                    break;
                case "Jack of All Trades":
                    Card jackOfAllTrades = new Card(ChooseCard(RandomCards("Colorless", 3, rng), "add to your hand"));
                    hand.Add(jackOfAllTrades);
                    break;
                case "Hand of Greed":
                    if (encounter[target].Hp <= 0)
                        hero.GoldChange(SecondaryEffect);
                    break;
                case "Madness":
                    if (hand.Count > 0)
                        hand[rng.Next(hand.Count)].EnergyCost = "0";
                    break;
                case "Metamorphosis":
                    List<Card> metamorphosis = new List<Card>(RandomCards(hero.Name, 3, rng));
                    foreach (Card c in metamorphosis)
                        c.EnergyCost = "0";
                    drawPile.AddRange(metamorphosis);
                    Shuffle(drawPile, rng);
                    break;
                case "Purity":
                    for (int i = 0; i < 3; i++)
                        ChooseCard(hand, "exhaust").Exhaust(exhaustPile, hand);
                    break;
                case "Ritual Dagger":                                                       // This card requires updates to combat Deck vs perma Deck to function properly
                    if (encounter[target].Hp <= 0)
                        AttackDamage += 3;
                    break;
                case "Secret Technique":
                    List<Card> secretSkill = new();
                    foreach (Card c in drawPile)
                        if (c.Type == "Skill")
                            secretSkill.Add(c);
                    Card secretSkillChoice = ChooseCard(secretSkill, "add to your hand");
                    hand.Add(secretSkillChoice);
                    drawPile.Remove(secretSkillChoice);
                    break;
                case "Secret Weapon":
                    List<Card> secretAttack = new();
                    foreach (Card c in drawPile)
                        if (c.Type == "Attack")
                            secretAttack.Add(c);
                    Card secretAttackChoice = ChooseCard(secretAttack, "add to your hand");
                    hand.Add(secretAttackChoice);
                    drawPile.Remove(secretAttackChoice);
                    break;
                case "Thinking Ahead":
                    Card thinkingAhead = new(ChooseCard(hand, "add to the top of your drawpile"));
                    drawPile.Add(thinkingAhead);
                    hand.Remove(thinkingAhead);
                    break;
                case "Transmutation":
                    for (int i = 0; i < xEnergy; i++)
                    {
                        if (hand.Count < 10)
                        {
                            hand.Add(new Card(RandomCards("Colorless", 1, rng)[0]));
                        }
                        else discardPile.Add(new Card(RandomCards("Colorless", 1, rng)[0]));
                    }
                    break;
                case "Violence":
                    for (int i = 0; i < 3; i++)
                    {
                        Card violence = new Card();
                        while (violence.Type != "Attack" && drawPile.Any(x => x.Type == "Attack"))
                            violence = drawPile[rng.Next(0, drawPile.Count)];
                        if (violence == null)
                            break;
                        if (hand.Count < 10)
                            hand.Add(violence);
                        else discardPile.Add(violence);
                        drawPile.Remove(violence);
                    }
                    break;
                case "Necronomicurse":
                    hand.Add(new Card(Dict.cardL[346]));
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
                        SecondaryEffect = i;                   
                    if (Name == "Chaos")
                        hero.ChannelOrb(encounter, rng.Next(0, 4));
                    else hero.ChannelOrb(encounter, SecondaryEffect);
                }
                if (Name == "Glacier")
                    hero.ChannelOrb(encounter, 1);
            }

            // Discard Effects
            if (Discards)
            {
                if (hand.Count > 1)
                {
                    switch (Name)
                    {
                        default:
                            for (int i = 0; i < SecondaryEffect; i++)
                                if (hand.Count > 1)
                                    Discard(hero, hand, discardPile, ChooseCard(hand, "discard"));
                            break;
                        case "All-Out Attack":
                            Discard(hero, hand, discardPile, hand[rng.Next(0, hand.Count)]);
                            break;
                        case "Unload":
                            for (int i = hand.Count; i > 0; i--)
                            {
                                if (hand[i - 1].Type == "Attack")
                                    Discard(hero, hand, discardPile, hand[i - 1]);
                            }
                            break;
                        case "Storm of Steel":
                            int storm;
                            for (storm = 0; storm < hand.Count; storm++)
                            {
                                Discard(hero, hand, discardPile, hand[hand.Count - 1]);
                            }
                            for (int i = storm; i > 0; i--)
                                hand.Add(new Card(Dict.cardL[317]));
                            break;
                        case "Calculated Gamble":
                            int gamble = 0;
                            for (gamble = 0; gamble < hand.Count; gamble++)
                            {
                                Discard(hero, hand, discardPile, hand[hand.Count - 1]);
                            }
                            DrawCards(drawPile, hand, discardPile, rng, gamble);
                            break;
                    }


                    // End of card action, action being documented in turn list
                    hero.Actions.Add($"{Name}-{Type} Played");
                }
            }
        }
    }
}