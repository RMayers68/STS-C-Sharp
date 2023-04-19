namespace STV
{
    public class Relic
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int EffectAmount { get; set; }
        public int PersistentCounter { get; set; }
        public bool IsActive { get; set; }
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
            this.IsActive = false;
        }
        //Cloning from dictionary constructor
        public Relic(Relic relic)
        {
            this.Name = relic.Name;
            this.Type = relic.Type;
            this.EffectAmount = relic.EffectAmount;
            this.PersistentCounter = relic.PersistentCounter;
            this.IsActive = true;
            Random rng = new();
            this.GoldCost = Type == "Rare" ? rng.Next(285, 316) : Type == "Uncommon" ? rng.Next(238, 263) : rng.Next(143, 158);
        }


        public static Relic RandomRelic(string type, Random rng)
        {
            return type switch
            {
                "Silent" => Dict.relicL[rng.Next(73, 146)],
                "Defect" => Dict.relicL[rng.Next(146, 219)],
                "Watcher" => Dict.relicL[rng.Next(221, 294)],
                _ => Dict.relicL[rng.Next(73)],
            };
        }
        public string GetDescription()
        {
            return Name switch
            {
                "Burning Blood" => $"At the end of combat, heal 6 HP.",
                "Ring of the Snake" => $"At the start of each combat, draw 3 additional cards.",
                "Cracked Core" => $"At the start of each combat, Channel 1 Lightning.",
                "Pure Water" => $"At the start of each combat, add a Miracle to your hand.",
                "Akebeko" => $"Your first attack each combat deals 8 additional damage.",
                "Anchor" => $"Start each combat with 10 Block.",
                "Ancient Tea Set" => $"Whenever you enter a Rest Site, start the next combat with 2 extra Energy.",
                "Art of War" => $"If you do not play any Attacks during your turn, gain an extra Energy next turn.",
                "Bag of Marbles" => $"At the start of each combat, apply 1 Vulnerable to ALL enemies.",
                "Bag of Preparation" => $"At the start of each combat, draw 2 additional cards.",
                "Blood Vial" => $"At the start of each combat, heal 2 HP.",
                "Bronze Scales" => $"Whenever you take damage, deal 3 damage back.",
                "Centennial Puzzle" => $"The first time you lose HP each combat, draw 3 cards.",
                "Ceramic Fish" => $"Whenever you add a card to your deck, gain 9 gold.",
                "Dream Catcher" => $"Whenever you rest, you may add a card to your deck.",
                "Happy Flower" => $"Every 3 turns, gain 1 Energy. {(IsActive ? $"({PersistentCounter} turns left)" : "")}",
                "Juzu Bracelet" => $"Regular enemy combats are no longer encountered in ? rooms.",
                "Lantern" => $"Gain 1 Energy on the first turn of each combat.",
                "Maw Bank" => $"Whenever you climb a floor, gain 12 Gold.{(IsActive ? "No longer works when you spend any Gold at the shop." : "Inactive.")} ",
                "Meal Ticket" => $"Whenever you enter a shop room, heal 15 HP.",
                "Nunchaku" => $"Every time you play 10 Attacks, gain 1 Energy. {(IsActive ? $"({PersistentCounter} Attacks needed)" : "")}",
                "Oddly Smooth Stone" => $"At the start of each combat, gain 1 Dexterity.",
                "Omamori" => $"Negate the next {(IsActive ? $"{PersistentCounter}" : "0")} Curses you obtain. ",
                "Orichalchum" => $"If you end your turn without Block, gain 6 Block.",
                "Pen Nib" => $"Every 10th Attack you play deals double damage. {(IsActive ? $"({PersistentCounter} Attacks needed)" : "")}",
                "Potion Slots" => $"Upon pickup, gain 2 Potion slots.",
                "Perserved Insect" => $"Enemies in Elite rooms have 25% less HP.",
                "Regal Pillow" => $"Heal an additional 15 HP when you Rest.",
                "Smiling Mask" => $"The merchant's card removal service now always costs 50 Gold.",
                "Strawberry" => $"Raise your Max HP by 7.",
                "The Boot" => $"Whenever you would deal 4 or less unblocked Attack damage, increase it to 5.",
                "Tiny Chest" => $"Every 4th ? room is a Treasure room.",
                "Toy Ornithopther" => $"Whenever you use a potion, heal 5 HP.",
                "Vajra" => $"At the start of each combat, gain 1 Strength.",
                "War Paint" => $"Upon pick up, Upgrade 2 random Skills.",
                "Whetstone" => $"Upon pick up, Upgrade 2 random Attacks.",
                "Red Skull" => $"While your HP is at or below 50%, you have 3 additional Strength.",
                "Snecko Skull" => $"Whenever you apply Icon Poison.png Poison, apply an additional 1 Poison.",
                "Data Disk" => $"Start each combat with 1 Focus.",
                "Damaru" => $"At the start of your turn, gain 1 Mantra.",
                "Blue Candle" => $"Curse cards can now be played. Playing a Curse will make you lose 1 HP and Exhausts the card.",
                "Bottled Flame" => $"Upon pick up, choose an Attack card. At the start of each combat, this card will be in your hand.",
                "Bottled Lightning" => $"Upon pick up, choose a Skill card. At the start of each combat, this card will be in your hand.",
                "Bottled Tornado" => $"Upon pick up, choose a Power card. At the start of each combat, this card will be in your hand.",
                "Darkstone Periapt" => $"Whenever you obtain a Curse, increase your Max HP by 6.",
                "Eternal Feather" => $"For every 5 cards in your deck, heal 3 HP whenever you enter a Rest Site.",
                "Frozen Egg" => $"Whenever you add a Power card to your deck, it is Upgraded.",
                "Toxic Egg" => $"",
                "Molten Egg" => $"Whenever you add an Attack card to your deck, it is Upgraded.",
                "Gremlin Horn" => $"Whenever an enemy dies, gain 1 Energy and draw 1 card.",
                "Horn Cleat" => $"At the start of your 2nd turn, gain 14 Block.",
                "Ink Bottle" => $"Whenever you play 10 cards, draw 1 card. {(IsActive ? $"({PersistentCounter} Card plays needed)" : "")}",
                "Kunai" => $"Every time you play 3 Attacks in a single turn, gain 1 Dexterity. {(IsActive ? $"({PersistentCounter} Attacks needed)" : "")}",
                "Shuriken" => $"Every time you play 3 Attacks in a single turn, gain 1 Strength. {(IsActive ? $"({PersistentCounter} Attacks needed)" : "")}",
                "Letter Opener" => $"Every time you play 3 Skills in a single turn, deal 5 damage to ALL enemies. {(IsActive ? $"({PersistentCounter} Skills needed)" : "")}",
                "Matryoshka" => $"The next {(IsActive ? $"{PersistentCounter}" : "0")} non-boss chests you open contain 2 Relics.",
                "Meat on the Bone" => $"If your HP is at or below 50% at the end of combat, heal 12 HP.",
                "Mercury Hourglass" => $"At the start of your turn, deal 3 damage to ALL enemies.",
                "Mummified Hand" => $"Whenever you play a Power, a random card in your hand costs 0 for the turn.",
                "Ornamental Fan" => $"Every time you play 3 Attacks in a single turn, gain 4 Block. {(IsActive ? $"({PersistentCounter} Attacks needed)" : "")}",
                "Pantograph" => $"At the start of boss combats, heal 25 HP.",
                "Pear" => $"Raise your Max HP by 10.",
                "Question Card" => $"On future Card Reward screens you have 1 additional card to choose from.",
                "Singing Bowl" => $"When adding cards to your deck, you may gain +2 Max HP instead.",
                "Strike Dummy" => $"Cards containing \"Strike\" deal 3 additional damage.",
                "Sundial" => $"Every 3 times you shuffle your deck, gain 2 Energy.",
                "The Courier" => $"The merchant no longer runs out of cards, relics, or potions and his prices are reduced by 20%.",
                "White Beast Statue" => $"Potions always drop after combat.",
                "Paper Phrog" => $"Enemies with Vulnerable take 75% more damage rather than 50%.",
                "Self-Forming Clay" => $"Whenever you lose HP in combat, gain 3 Block next turn.",
                "Ninja Scroll" => $"Start each combat with 3 Shivs in hand.",
                "Paper Krane" => $"Enemies with Weak deal 40% less damage rather than 25%.",
                "Gold-Plated Cables" => $"Your rightmost Orb triggers its passive an additional time.",
                "Symbiotic Virus" => $"At the start of each combat, Channel 1 Dark Orb.",
                "Duality" => $"Whenever you play an Attack, gain 1 temporary Icon Dexterity.png Dexterity.",
                "Teardrop Locket" => $"Start each combat in Calm.",
                "Bird-Faced Urn" => $"Whenever you play a Power, heal 2 HP.",
                "Calipers" => $"At the start of your turn, lose 15 Block rather than all of your Block.",
                "Captain's Wheel" => $"At the start of your 3rd turn, gain 18 Block.",
                "Dead Branch" => $"Whenever you Exhaust a card, add a random card to your hand.",
                "Du-Vu Doll" => $"For each Curse in your deck, start each combat with 1 additional Strength. (Current Strength: {EffectAmount})",
                "Fossilized Helix" => $"Prevent the first time you would lose HP in combat.",
                "Gambling Chip" => $"At the start of each combat, discard any number of cards then draw that many.",
                "Ginger" => $"You can no longer become Weakened.",
                "Girya" => $"You can now gain Strength at Rest Sites. ({PersistentCounter} times left)",
                "Ice Cream" => $"Energy is now conserved between turns.",
                "Incense Burner" => $"Every 6 turns, gain 1 Intangible. {(IsActive ? $"({PersistentCounter} turns left)" : "")}",
                "Lizard Tail" => $"When you would die, heal to 50% of your Max HP instead. ({(IsActive ? $"Active" : "Inactive")})",
                "Mango" => $"Raise your Max HP by 14.",
                "Old Coin" => $"Gain 300 Gold.",
                "Peace Pipe" => $"You can now remove cards from your deck at Rest Sites.",
                "Pocketwatch" => $"Whenever you play 3 or less cards in a turn, draw 3 additional cards at the start of your next turn.",
                "Prayer Wheel" => $"Normal enemies drop an additional card reward.",
                "Shovel" => $"You can now Dig for loot at Rest Sites.",
                "Stone Calender" => $"At the end of turn 7, deal 52 damage to ALL enemies.",
                "Thread and Needle" => $"At the start of each combat, gain 4 Plated Armor.",
                "Torii" => $"Whenever you would receive 5 or less unblocked Attack damage, reduce it to 1.",
                "Tungsten Rod" => $"Whenever you would lose HP, lose 1 less.",
                "Turnip" => $"You can no longer become Frail.",
                "Unceasing Top" => $"Whenever you have no cards in hand during your turn, draw a card.",
                "Wing Boots" => $"You may ignore paths when choosing the next room to travel to 3 times.",
                "Champion Belt" => $"Whenever you apply Vulnerable, also apply 1 Weak.",
                "Charon's Ashes" => $"Whenever you Exhaust a card, deal 3 damage to ALL enemies.",
                "Magic Flower" => $"Healing is 50% more effective during combat.",
                "The Specimen" => $"Whenever an enemy dies, transfer any Poison it has to a random enemy.",
                "Tingsha" => $"Whenever you discard a card during your turn, deal 3 damage to a random enemy for each card discarded.",
                "Tough Bandages" => $"Whenever you discard a card during your turn, gain 3 Block.",
                "Emotion Chip" => $"At the start of each turn, if you took any damage last turn, trigger the passive ability of all your Orbs.",
                "Cloak Clasp" => $"At the end of your turn, gain 1 Block for each card in your hand.",
                "Golden Eye" => $"Whenever you Scry, Scry 2 additional cards.",
                "Cauldron" => $"When obtained, brews 5 random potions.",
                "Chemical X" => $"The effects of your cost X cards are increased by 2.",
                "Clockwork Souvenir" => $"At the start of each combat, gain 1 Icon Artifact.png Artifact.",
                "Dolly's Mirror" => $"Upon pickup, obtain an additional copy of a card in your deck.",
                "Frozen Eye" => $"When viewing your Draw Pile, the cards are now shown in order.",
                "Hand Drill" => $"Whenever you break an enemy's Icon Block.png Block, apply 2 Icon Vulnerable.png Vulnerable.",
                "Lee's Waffle" => $"Raise your Max HP by 7 and heal all of your HP.",
                "Medical Kit" => $"Status cards can now be played. Playing a Status will Exhaust the card.",
                "Membership Card" => $"50% discount on all products!",
                "Orange Pellets" => $"Whenever you play a Power, Attack, and Skill in the same turn, remove all of your Debuffs.",
                "Orrery" => $"Choose and add 5 cards to your deck.",
                "Prismatic Shard" => $"Combat reward screens now contain colorless cards and cards from other colors.",
                "Sling of Courage" => $"Start each Elite combat with 2 Icon Strength.png Strength.",
                "Strange Spoon" => $"Cards which Exhaust when played will instead discard 50% of the time.",
                "The Abacus" => $"Gain 6 Icon Block.png Block whenever you shuffle your draw pile.",
                "Toolbox" => $"At the start of each combat, choose 1 of 3 random Colorless cards and add the chosen card into your hand.",
                "Brimstone" => $"At the start of your turn, gain 2 Strength and ALL enemies gain 1 Strength.",
                "Twisted Funnel" => $"At the start of each combat, apply 4 Icon Poison.png Poison to ALL enemies.",
                "Runic Capcitor" => $"Start each combat with 3 additional Orb slots.",
                "Melange" => $"Whenever you shuffle your draw pile, Scry 3.",
                "Astrolabe" => $"Upon pickup, choose and Transform 3 cards, then Upgrade them.",
                "Black Star" => $"Elites now drop 2 Relics when defeated.",
                "Busted Crown" => $"Gain 1 Energy at the start of each turn. On Card Reward screens, you have 2 fewer cards to choose from.",
                "Calling Bell" => $"Upon pickup, obtain a unique Curse and 3 relics.",
                "Coffee Dripper" => $"Gain 1 Energy at the start of each turn. You can no longer Rest at Rest Sites.",
                "Cursed Key" => $"Gain 1 Energy at the start of each turn. Whenever you open a non-boss chest, obtain a Curse.",
                "Ectoplasm" => $"Gain 1 Energy at the start of each turn. You can no longer gain Gold.",
                "Empty Cage" => $"Upon pickup, remove 2 cards from your deck.",
                "Fusion Hammer" => $"Gain 1 Energy at the start of each turn. You can no longer Smith at Rest Sites.",
                "Pandora's Box" => $"Transform all Strikes and Defends.",
                "Philosopher's Stone" => $"Gain 1 Energy at the start of each turn. ALL enemies start with 1 Strength.",
                "Runic Dome" => $"Gain 1 Energy at the start of each turn. You can no longer see enemy Intents.",
                "Runic Pyramid" => $"At the end of your turn, you no longer discard your hand.",
                "Sacred Bark" => $"Double the effectiveness of potions.",
                "Slaver's Collar" => $"During Boss and Elite combats, gain Energy at the start of your turn.",
                "Snecko Eye" => $"Draw 2 additional cards each turn. Start each combat Confused.",
                "Sozu" => $"Gain 1 Energy at the start of each turn. You can no longer obtain potions.",
                "Tiny House" => $"Obtain 1 potion. Gain 50 Gold. Raise your Max HP by 5. Obtain 1 card. Upgrade 1 Random card.",
                "Velvet Choker" => $"Gain 1 Energy at the start of each turn. You cannot play more than 6 cards per turn.",
                "Black Blood" => $"Replaces Burning Blood. At the end of combat, heal 12 HP.",
                "Mark of Pain" => $"Gain 1 Energy at the start of each turn. Start combats with 2 Wounds in your draw pile.",
                "Runic Cube" => $"Whenever you lose HP, draw 1 card.",
                "Ring of the Serpent" => $"Replaces Ring of the Snake. At the start of your turn, draw 1 additional card.",
                "Wrist Blade" => $"Attacks that cost 0 deal 4 additional damage.",
                "Hovering Kite" => $"The first time you discard a card each turn, gain 1 Energy.",
                "Frozen Core" => $"Replaces Cracked Core. If you end your turn with empty Orb slots, Channel 1 Frost.",
                "Inserter" => $"Every 2 turns, gain 1 Orb slot. {(IsActive ? $"({PersistentCounter} turns left)" : "")}",
                "Nuclear Battery" => $"At the start of each combat, Channel 1 Plasma.",
                "Holy Water" => $"Replaces Pure Water. At the start of each combat, add 3 Miracles to your hand.",
                "Violet Lotus" => $"Whenever you exit Calm, gain an additional Energy.",
                "Bloody Idol" => $"Whenever you gain Gold, heal 5 HP.",
                "Cultist Headpiece" => $"You feel more talkative.",
                "Enchiridion" => $"At the start of each combat, add a random Power card to your hand. It costs 0 until the end of turn.",
                "Face of Cleric" => $"Raise your Max HP by 1 after each combat.",
                "Golden Idol" => $"Enemies drop 25% more Gold.",
                "Gremlin Visage" => $"Start each combat with 1 Icon Weak.png Weak.",
                "Mark of the Bloom" => $"You can no longer heal.",
                "Mutagenic Strength" => $"Start each combat with 3 Strength that is lost at the end of your turn.",
                "N'loths Gift" => $"Triples the chance of receiving rare cards as monster rewards.",
                "N'loths Hungry Face" => $"The next non-boss chest you open is empty. ({(IsActive ? $"Active" : "Inactive")})",
                "Necronomicon" => $"The first Attack played each turn that costs 2 or more is played twice. When you take this relic, become Cursed.",
                "Neow's Lament" => $"Enemies in your first 3 combats will have 1 HP.",
                "Nilry's Codex" => $"At the end of each turn, you can choose 1 of 3 random cards to shuffle into your draw pile.",
                "Odd Mushroom" => $"When Icon Vulnerable.png Vulnerable, take 25% more damage rather than 50%.",
                "Red Mask" => $"At the start of each combat, apply 1 Weak to ALL enemies.",
                "Spirit Poop" => $"It's unpleasant.",
                "Ssserpent Head" => $"Whenever you enter a ? room, gain 50 Gold.",
                "Warped Tongs" => $"At the start of your turn, Upgrade a random card in your hand for the rest of combat.",
                "Circlet" => $"Collect as many as you can. (Given when you exhaust the relic pool)",
                _ => "",
            };
        }
    }
}