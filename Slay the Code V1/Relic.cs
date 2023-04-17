namespace STV
{
    public class Relic
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int EffectAmount { get; set; }
        public int PersistentCounter { get; set; }
        public bool isActive { get; set; }
        public int GoldCost { get; set; }

        // constructor
        public Relic()
        {
            this.Name = "Purchased";
            this.GoldCost = 0;
        }

        public Relic(string name, string type,int EffectAmount, int persistentCounter = 0)
        {
            this.Name = name;
            this.Type = type;
            this.EffectAmount = EffectAmount;
            this.PersistentCounter = persistentCounter;
            this.isActive = false;
        }
        //Cloning from dictionary constructor
        public Relic(Relic relic)
        {
            this.Name = relic.Name;
            this.Type = relic.Type;
            this.EffectAmount = relic.EffectAmount;
            this.PersistentCounter = relic.PersistentCounter;
            this.isActive = true;
            Random rng = new Random();
            this.GoldCost = Type == "Rare" ? rng.Next(285, 316) : Type == "Uncommon" ? rng.Next(238, 263) : rng.Next(143, 158);
        }

        public string getDescription()
        {
            switch (Name)
            {
                default: return "";
                case "Burning Blood": return $"At the end of combat, heal 6 HP.";
                case "Ring of the Snake": return $"At the start of each combat, draw 3 additional cards.";
                case "Cracked Core": return $"At the start of each combat, Channel 1 Lightning.";
                case "Pure Water": return $"At the start of each combat, add a Miracle to your hand.";
                case "Akebeko": return $"Your first attack each combat deals 8 additional damage.";
                case "Anchor": return $"Start each combat with 10 Block.";
                case "Ancient Tea Set": return $"Whenever you enter a Rest Site, start the next combat with 2 extra Energy.";
                case "Art of War": return $"If you do not play any Attacks during your turn, gain an extra Energy next turn.";
                case "Bag of Marbles": return $"At the start of each combat, apply 1 Vulnerable to ALL enemies.";
                case "Bag of Preparation": return $"At the start of each combat, draw 2 additional cards.";
                case "Blood Vial": return $"At the start of each combat, heal 2 HP.";
                case "Bronze Scales": return $"Whenever you take damage, deal 3 damage back.";
                case "Centennial Puzzle": return $"The first time you lose HP each combat, draw 3 cards.";
                case "Ceramic Fish": return $"Whenever you add a card to your deck, gain 9 gold.";
                case "Dream Catcher": return $"Whenever you rest, you may add a card to your deck.";
                case "Happy Flower": return $"Every 3 turns, gain 1 Energy. {(isActive ? $"({PersistentCounter} turns left)" : "")}";
                case "Juzu Bracelet": return $"Regular enemy combats are no longer encountered in ? rooms.";
                case "Lantern": return $"Gain 1 Energy on the first turn of each combat.";
                case "Maw Bank": return $"Whenever you climb a floor, gain 12 Gold.{(isActive ? "No longer works when you spend any Gold at the shop." : "Inactive.")} ";
                case "Meal Ticket": return $"Whenever you enter a shop room, heal 15 HP.";
                case "Nunchaku": return $"Every time you play 10 Attacks, gain 1 Energy. {(isActive ? $"({PersistentCounter} Attacks needed)" : "")}";
                case "Oddly Smooth Stone": return $"At the start of each combat, gain 1 Dexterity.";
                case "Omamori": return $"Negate the next {(isActive ? $"{PersistentCounter}" : "0")} Curses you obtain. ";
                case "Orichalchum": return $"If you end your turn without Block, gain 6 Block.";
                case "Pen Nib": return $"Every 10th Attack you play deals double damage. {(isActive ? $"({PersistentCounter} Attacks needed)" : "")}";
                case "Potion Slots": return $"Upon pickup, gain 2 Potion slots.";
                case "Perserved Insect": return $"Enemies in Elite rooms have 25% less HP.";
                case "Regal Pillow": return $"Heal an additional 15 HP when you Rest.";
                case "Smiling Mask": return $"The merchant's card removal service now always costs 50 Gold.";
                case "Strawberry": return $"Raise your Max HP by 7.";
                case "The Boot": return $"Whenever you would deal 4 or less unblocked Attack damage, increase it to 5.";
                case "Tiny Chest": return $"Every 4th ? room is a Treasure room.";
                case "Toy Ornithopther": return $"Whenever you use a potion, heal 5 HP.";
                case "Vajra": return $"At the start of each combat, gain 1 Strength.";
                case "War Paint": return $"Upon pick up, Upgrade 2 random Skills.";
                case "Whetstone": return $"Upon pick up, Upgrade 2 random Attacks.";
                case "Red Skull": return $"While your HP is at or below 50%, you have 3 additional Strength.";
                case "Snecko Skull": return $"Whenever you apply Icon Poison.png Poison, apply an additional 1 Poison.";
                case "Data Disk": return $"Start each combat with 1 Focus.";
                case "Damaru": return $"At the start of your turn, gain 1 Mantra.";
                case "Blue Candle": return $"Curse cards can now be played. Playing a Curse will make you lose 1 HP and Exhausts the card.";
                case "Bottled Flame": return $"Upon pick up, choose an Attack card. At the start of each combat, this card will be in your hand.";
                case "Bottled Lightning": return $"Upon pick up, choose a Skill card. At the start of each combat, this card will be in your hand.";
                case "Bottled Tornado": return $"Upon pick up, choose a Power card. At the start of each combat, this card will be in your hand.";
                case "Darkstone Periapt": return $"Whenever you obtain a Curse, increase your Max HP by 6.";
                case "Eternal Feather": return $"For every 5 cards in your deck, heal 3 HP whenever you enter a Rest Site.";
                case "Frozen Egg": return $"Whenever you add a Power card to your deck, it is Upgraded.";
                case "Toxic Egg": return $"";
                case "Molten Egg": return $"Whenever you add an Attack card to your deck, it is Upgraded.";
                case "Gremlin Horn": return $"Whenever an enemy dies, gain 1 Energy and draw 1 card.";
                case "Horn Cleat": return $"At the start of your 2nd turn, gain 14 Block.";
                case "Ink Bottle": return $"Whenever you play 10 cards, draw 1 card. {(isActive ? $"({PersistentCounter} Card plays needed)" : "")}";
                case "Kunai": return $"Every time you play 3 Attacks in a single turn, gain 1 Dexterity. {(isActive ? $"({PersistentCounter} Attacks needed)" : "")}";
                case "Shuriken": return $"Every time you play 3 Attacks in a single turn, gain 1 Strength. {(isActive ? $"({PersistentCounter} Attacks needed)" : "")}";
                case "Letter Opener": return $"Every time you play 3 Skills in a single turn, deal 5 damage to ALL enemies. {(isActive ? $"({PersistentCounter} Skills needed)" : "")}";
                case "Matryoshka": return $"The next {(isActive ? $"{PersistentCounter}" : "0")} non-boss chests you open contain 2 Relics.";
                case "Meat on the Bone": return $"If your HP is at or below 50% at the end of combat, heal 12 HP.";
                case "Mercury Hourglass": return $"At the start of your turn, deal 3 damage to ALL enemies.";
                case "Mummified Hand": return $"Whenever you play a Power, a random card in your hand costs 0 for the turn.";
                case "Ornamental Fan": return $"Every time you play 3 Attacks in a single turn, gain 4 Block. {(isActive ? $"({PersistentCounter} Attacks needed)" : "")}";
                case "Pantograph": return $"At the start of boss combats, heal 25 HP.";
                case "Pear": return $"Raise your Max HP by 10.";
                case "Question Card": return $"On future Card Reward screens you have 1 additional card to choose from.";
                case "Singing Bowl": return $"When adding cards to your deck, you may gain +2 Max HP instead.";
                case "Strike Dummy": return $"Cards containing \"Strike\" deal 3 additional damage.";
                case "Sundial": return $"Every 3 times you shuffle your deck, gain 2 Energy.";
                case "The Courier": return $"The merchant no longer runs out of cards, relics, or potions and his prices are reduced by 20%.";
                case "White Beast Statue": return $"Potions always drop after combat.";
                case "Paper Phrog": return $"Enemies with Vulnerable take 75% more damage rather than 50%.";
                case "Self-Forming Clay": return $"Whenever you lose HP in combat, gain 3 Block next turn.";
                case "Ninja Scroll": return $"Start each combat with 3 Shivs in hand.";
                case "Paper Krane": return $"Enemies with Weak deal 40% less damage rather than 25%.";
                case "Gold-Plated Cables": return $"Your rightmost Orb triggers its passive an additional time.";
                case "Symbiotic Virus": return $"At the start of each combat, Channel 1 Dark Orb.";
                case "Duality": return $"Whenever you play an Attack, gain 1 temporary Icon Dexterity.png Dexterity.";
                case "Teardrop Locket": return $"Start each combat in Calm.";
                case "Bird-Faced Urn": return $"Whenever you play a Power, heal 2 HP.";
                case "Calipers": return $"At the start of your turn, lose 15 Block rather than all of your Block.";
                case "Captain's Wheel": return $"At the start of your 3rd turn, gain 18 Block.";
                case "Dead Branch": return $"Whenever you Exhaust a card, add a random card to your hand.";
                case "Du-Vu Doll": return $"For each Curse in your deck, start each combat with 1 additional Strength. (Current Strength: {PersistentCounter})";
                case "Fossilized Helix": return $"Prevent the first time you would lose HP in combat.";
                case "Gambling Chip": return $"At the start of each combat, discard any number of cards then draw that many.";
                case "Ginger": return $"You can no longer become Weakened.";
                case "Girya": return $"You can now gain Strength at Rest Sites. ({PersistentCounter} times left)";
                case "Ice Cream": return $"Energy is now conserved between turns.";
                case "Incense Burner": return $"Every 6 turns, gain 1 Intangible. {(isActive ? $"({PersistentCounter} turns left)" : "")}";
                case "Lizard Tail": return $"When you would die, heal to 50% of your Max HP instead. ({(isActive ? $"Active" : "Inactive")})";
                case "Mango": return $"Raise your Max HP by 14.";
                case "Old Coin": return $"Gain 300 Gold.";
                case "Peace Pipe": return $"You can now remove cards from your deck at Rest Sites.";
                case "Pocketwatch": return $"Whenever you play 3 or less cards in a turn, draw 3 additional cards at the start of your next turn.";
                case "Prayer Wheel": return $"Normal enemies drop an additional card reward.";
                case "Shovel": return $"You can now Dig for loot at Rest Sites.";
                case "Stone Calender": return $"At the end of turn 7, deal 52 damage to ALL enemies.";
                case "Thread and Needle": return $"At the start of each combat, gain 4 Plated Armor.";
                case "Torii": return $"Whenever you would receive 5 or less unblocked Attack damage, reduce it to 1.";
                case "Tungsten Rod": return $"Whenever you would lose HP, lose 1 less.";
                case "Turnip": return $"You can no longer become Frail.";
                case "Unceasing Top": return $"Whenever you have no cards in hand during your turn, draw a card.";
                case "Wing Boots": return $"You may ignore paths when choosing the next room to travel to 3 times.";
                case "Champion Belt": return $"Whenever you apply Vulnerable, also apply 1 Weak.";
                case "Charon's Ashes": return $"Whenever you Exhaust a card, deal 3 damage to ALL enemies.";
                case "Magic Flower": return $"Healing is 50% more effective during combat.";
                case "The Specimen": return $"Whenever an enemy dies, transfer any Poison it has to a random enemy.";
                case "Tingsha": return $"Whenever you discard a card during your turn, deal 3 damage to a random enemy for each card discarded.";
                case "Tough Bandages": return $"Whenever you discard a card during your turn, gain 3 Block.";
                case "Emotion Chip": return $"At the start of each turn, if you took any damage last turn, trigger the passive ability of all your Orbs.";
                case "Cloak Clasp": return $"At the end of your turn, gain 1 Block for each card in your hand.";
                case "Golden Eye": return $"Whenever you Scry, Scry 2 additional cards.";
                case "Cauldron": return $"When obtained, brews 5 random potions.";
                case "Chemical X": return $"The effects of your cost X cards are increased by 2.";
                case "Clockwork Souvenir": return $"At the start of each combat, gain 1 Icon Artifact.png Artifact.";
                case "Dolly's Mirror": return $"Upon pickup, obtain an additional copy of a card in your deck.";
                case "Frozen Eye": return $"When viewing your Draw Pile, the cards are now shown in order.";
                case "Hand Drill": return $"Whenever you break an enemy's Icon Block.png Block, apply 2 Icon Vulnerable.png Vulnerable.";
                case "Lee's Waffle": return $"Raise your Max HP by 7 and heal all of your HP.";
                case "Medical Kit": return $"Status cards can now be played. Playing a Status will Exhaust the card.";
                case "Membership Card": return $"50% discount on all products!";
                case "Orange Pellets": return $"Whenever you play a Power, Attack, and Skill in the same turn, remove all of your Debuffs.";
                case "Orrery": return $"Choose and add 5 cards to your deck.";
                case "Prismatic Shard": return $"Combat reward screens now contain colorless cards and cards from other colors.";
                case "Sling of Courage": return $"Start each Elite combat with 2 Icon Strength.png Strength.";
                case "Strange Spoon": return $"Cards which Exhaust when played will instead discard 50% of the time.";
                case "The Abacus": return $"Gain 6 Icon Block.png Block whenever you shuffle your draw pile.";
                case "Toolbox": return $"At the start of each combat, choose 1 of 3 random Colorless cards and add the chosen card into your hand.";
                case "Brimstone": return $"At the start of your turn, gain 2 Strength and ALL enemies gain 1 Strength.";
                case "Twisted Funnel": return $"At the start of each combat, apply 4 Icon Poison.png Poison to ALL enemies.";
                case "Runic Capcitor": return $"Start each combat with 3 additional Orb slots.";
                case "Melange": return $"Whenever you shuffle your draw pile, Scry 3.";
                case "Astrolabe": return $"Upon pickup, choose and Transform 3 cards, then Upgrade them.";
                case "Black Star": return $"Elites now drop 2 Relics when defeated.";
                case "Busted Crown": return $"Gain 1 Energy at the start of each turn. On Card Reward screens, you have 2 fewer cards to choose from.";
                case "Calling Bell": return $"Upon pickup, obtain a unique Curse and 3 relics.";
                case "Coffee Dripper": return $"Gain 1 Energy at the start of each turn. You can no longer Rest at Rest Sites.";
                case "Cursed Key": return $"Gain 1 Energy at the start of each turn. Whenever you open a non-boss chest, obtain a Curse.";
                case "Ectoplasm": return $"Gain 1 Energy at the start of each turn. You can no longer gain Gold.";
                case "Empty Cage": return $"Upon pickup, remove 2 cards from your deck.";
                case "Fusion Hammer": return $"Gain 1 Energy at the start of each turn. You can no longer Smith at Rest Sites.";
                case "Pandora's Box": return $"Transform all Strikes and Defends.";
                case "Philosopher's Stone": return $"Gain 1 Energy at the start of each turn. ALL enemies start with 1 Strength.";
                case "Runic Dome": return $"Gain 1 Energy at the start of each turn. You can no longer see enemy Intents.";
                case "Runic Pyramid": return $"At the end of your turn, you no longer discard your hand.";
                case "Sacred Bark": return $"Double the effectiveness of potions.";
                case "Slaver's Collar": return $"During Boss and Elite combats, gain Energy at the start of your turn.";
                case "Snecko Eye": return $"Draw 2 additional cards each turn. Start each combat Confused.";
                case "Sozu": return $"Gain 1 Energy at the start of each turn. You can no longer obtain potions.";
                case "Tiny House": return $"Obtain 1 potion. Gain 50 Gold. Raise your Max HP by 5. Obtain 1 card. Upgrade 1 Random card.";
                case "Velvet Choker": return $"Gain 1 Energy at the start of each turn. You cannot play more than 6 cards per turn.";
                case "Black Blood": return $"Replaces Burning Blood. At the end of combat, heal 12 HP.";
                case "Mark of Pain": return $"Gain 1 Energy at the start of each turn. Start combats with 2 Wounds in your draw pile.";
                case "Runic Cube": return $"Whenever you lose HP, draw 1 card.";
                case "Ring of the Serpent": return $"Replaces Ring of the Snake. At the start of your turn, draw 1 additional card.";
                case "Wrist Blade": return $"Attacks that cost 0 deal 4 additional damage.";
                case "Hovering Kite": return $"The first time you discard a card each turn, gain 1 Energy.";
                case "Frozen Core": return $"Replaces Cracked Core. If you end your turn with empty Orb slots, Channel 1 Frost.";
                case "Inserter": return $"Every 2 turns, gain 1 Orb slot. {(isActive ? $"({PersistentCounter} turns left)" : "")}";
                case "Nuclear Battery": return $"At the start of each combat, Channel 1 Plasma.";
                case "Holy Water": return $"Replaces Pure Water. At the start of each combat, add 3 Miracles to your hand.";
                case "Violet Lotus": return $"Whenever you exit Calm, gain an additional Energy.";
                case "Bloody Idol": return $"Whenever you gain Gold, heal 5 HP.";
                case "Cultist Headpiece": return $"You feel more talkative.";
                case "Enchiridion": return $"At the start of each combat, add a random Power card to your hand. It costs 0 until the end of turn.";
                case "Face of Cleric": return $"Raise your Max HP by 1 after each combat.";
                case "Golden Idol": return $"Enemies drop 25% more Gold.";
                case "Gremlin Visage": return $"Start each combat with 1 Icon Weak.png Weak.";
                case "Mark of the Bloom": return $"You can no longer heal.";
                case "Mutagenic Strength": return $"Start each combat with 3 Strength that is lost at the end of your turn.";
                case "N'loths Gift": return $"Triples the chance of receiving rare cards as monster rewards.";
                case "N'loths Hungry Face": return $"The next non-boss chest you open is empty. ({(isActive ? $"Active" : "Inactive")})";
                case "Necronomicon": return $"The first Attack played each turn that costs 2 or more is played twice. When you take this relic, become Cursed.";
                case "Neow's Lament": return $"Enemies in your first 3 combats will have 1 HP.";
                case "Nilry's Codex": return $"At the end of each turn, you can choose 1 of 3 random cards to shuffle into your draw pile.";
                case "Odd Mushroom": return $"When Icon Vulnerable.png Vulnerable, take 25% more damage rather than 50%.";
                case "Red Mask": return $"At the start of each combat, apply 1 Weak to ALL enemies.";
                case "Spirit Poop": return $"It's unpleasant.";
                case "Ssserpent Head": return $"Whenever you enter a ? room, gain 50 Gold.";
                case "Warped Tongs": return $"At the start of your turn, Upgrade a random card in your hand for the rest of combat.";
                case "Circlet": return $"Collect as many as you can. (Given when you exhaust the relic pool)";
            }
        }
    }
}