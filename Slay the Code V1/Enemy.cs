
namespace STV
{
    public class Enemy : Actor
    {
        public int BottomHP { get; set; }
        public int TopHP { get; set; }
        public int EnemyID { get; set; } // ID correlates to method ran (Name without spaces)
        public string Intent { get; set; }
        private static readonly Random EnemyRNG = new();



        public Enemy(int enemyID, string name, int bottomHP, int topHP, string intent)
        {
            this.EnemyID = enemyID;
            this.Name = name;
            this.MaxHP = topHP;
            this.Hp = this.MaxHP;
            this.TopHP = topHP;
            this.BottomHP = bottomHP;
            this.Block = 0;
            this.Intent = intent;
            this.Buffs = new();
            this.Actions = new();
        }

        public Enemy(Enemy enemy)
        {
            this.EnemyID = enemy.EnemyID;
            this.Name = enemy.Name;
            this.MaxHP = enemy.TopHP;
            this.Hp = this.MaxHP;
            this.TopHP = enemy.TopHP;
            this.BottomHP = enemy.BottomHP;
            this.Block = 0;
            this.Intent = enemy.Intent;
            this.Buffs = new();
            this.Actions = new();
            this.Relics = new();
        }

        public void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            int damage = 0;
            int target = 0;
            Random rng = new();
            switch (Intent)
            {
                case "Attack":
                    Attack(hero, 18, encounter);
                    break;
                case "Beam":
                    Attack(hero, 9, encounter);
                    break;
                case "Bellow":
                    if (EnemyID == 17)
                        AddBuff(19, 2);
                    else
                    {
                        AddBuff(4, 3);
                        GainBlock(6);
                    }
                    break;
                case "Bite":
                    if (EnemyID == 10)
                        Attack(hero, MaxHP / 2, encounter);
                    else Attack(hero, 6, encounter);
                    break;
                case "Bolt":
                    for (int i = 0; i < 2; i++)
                        hero.DiscardPile.Add(new Card(Dict.cardL[356]));
                    Console.WriteLine($"{Name} has added 2 Dazed cards to your deck!");
                    break;
                case "Charging":
                    Console.WriteLine($"{Name} is charging up!");
                    break;
                case "Charging Up":
                    GainBlock(9);
                    break;
                case "Chomp":
                    Attack(hero, 11, encounter);
                    break;
                case "Corrosive Spit":
                    Attack(hero, 7, encounter);
                    hero.DiscardPile.Add(Dict.cardL[358]);
                    if (EnemyID == 21)
                        hero.DiscardPile.Add(Dict.cardL[358]);
                    break;
                case "Dark Strike":
                    Attack(hero, 6, encounter);
                    break;
                case "Defensive Mode":
                    AddBuff(16, 3);
                    break;
                case "Divider":
                    damage = hero.Hp / 12 + 1;
                    for (int i = 0; i < 6; i++)
                        Attack(hero, damage, encounter);
                    break;
                case "Entangle":
                    hero.AddBuff(14, 2);
                    break;
                case "Escape":
                    encounter.Remove(this);
                    Console.WriteLine($"The {Name} has escaped!");
                    break;
                case "Fierce Bash":
                    Attack(hero, 32, encounter);
                    break;
                case "Flame Tackle":
                    if (EnemyID == 22)
                        damage = 16;
                    else damage = 8;
                    Attack(hero, damage, encounter);
                    hero.DiscardPile.Add(Dict.cardL[358]);
                    if (EnemyID == 22)
                        hero.DiscardPile.Add(Dict.cardL[358]);
                    break;
                case "Goop Spray":
                    for (int i = 0; i < 3; i++)
                        hero.DiscardPile.Add(new Card(Dict.cardL[358]));
                    Console.WriteLine($"{Name} has added 3 Slimed cards into your Deck! Ewww!");
                    break;
                case "Grow":
                    AddBuff(4, 3);
                    break;
                case "Incantation":
                    AddBuff(3, 3);
                    break;
                case "Inferno":
                    for (int i = 0; i < 6; i++)
                    {
                        Attack(hero, 2, encounter);
                        if (i % 2 == 0)
                            hero.DiscardPile.Add(new Card(Dict.cardL[355]));
                    }
                    Console.WriteLine($"{Name} has added 3 Burns to your Deck!");
                    break;
                case "Inflame":
                    AddBuff(4, 2);
                    GainBlock(12);
                    break;
                case "Lick":
                    int li = 0;
                    int ck = 2;
                    if (EnemyID == 3 || EnemyID == 5 || EnemyID == 21)
                        li = 2;
                    else li = 6;
                    if (EnemyID == 21 || EnemyID == 22)
                        ck++;
                    hero.AddBuff(li, ck);
                    break;
                case "Lunge":
                    Attack(hero, 12, encounter);
                    hero.GoldChange(-1*FindBuff("Thievery").Intensity);
                    Gold += FindBuff("Thievery").Intensity;
                    Console.WriteLine($"The {Name} stole 15 Gold!");
                    break;
                case "Mug":
                    Attack(hero, 10, encounter);
                    hero.GoldChange(-1 * FindBuff("Thievery").Intensity);
                    Gold += FindBuff("Thievery").Intensity;
                    Console.WriteLine($"The {Name} stole 15 Gold!");
                    break;
                case "Protect":
                    do 
                        target = rng.Next(0, encounter.Count);
                    while (target != encounter.FindIndex(x => x == this));                        
                    encounter[target].GainBlock(7);
                    break;
                case "Puncture":
                    Attack(hero, 9, encounter);
                    break;
                case "Rake":
                    Attack(hero, 7, encounter);
                    hero.AddBuff(2, 2);
                    break;
                case "Roll Attack":
                    Attack(hero, 9, encounter);
                    break;
                case "Rush":
                    Attack(hero, 14, encounter);
                    break;
                case "Scrape":
                    Attack(hero, 8, encounter);
                    hero.AddBuff(1, 2);
                    break;
                case "Scratch":
                    Attack(hero, 4, encounter);
                    break;
                case "Shield Bash":
                    Attack(hero, 6, encounter);
                    break;
                case "Sear":
                    Attack(hero, 6, encounter);
                    hero.DiscardPile.Add(new Card(Dict.cardL[355]));
                    Console.WriteLine($"{Name} has added a Burn to your Deck!");
                    break;
                case "Siphon Soul":
                    hero.AddBuff(9, -1);
                    hero.AddBuff(4, -1);
                    break;
                case "Skull Bash":
                    Attack(hero, 6, encounter);
                    hero.AddBuff(1, 3);
                    break;
                case "Slam":
                    Attack(hero, 35, encounter);
                    break;
                case "Sleeping":
                    Console.WriteLine($"{Name} is sleeping, be cautious on waking it...");
                    break;
                case "Smash":
                    Attack(hero, 4, encounter);
                    hero.AddBuff(2, 2);
                    break;
                case "Smoke Bomb":
                    GainBlock(6);
                    break;
                case "Stab":
                    switch (EnemyID)
                    {
                        case 8:
                            damage = 10;
                            break;
                        case 9:
                            damage = 13;
                            break;
                    }
                    Attack(hero, damage, encounter);
                    break;
                case "Tackle":
                    switch (EnemyID)
                    {
                        case 3:
                            damage = 10;
                            break;
                        case 5:
                            damage = 3;
                            break;
                        case 6:
                            damage = 5;
                            break;
                        case 21:
                            damage = 16;
                            break;
                        case 24:
                            damage = 5;
                            Attack(hero, damage, encounter);
                            break;
                    }
                    Attack(hero, damage, encounter);
                    break;
                case "Thrash":
                    Attack(hero, 7, encounter);
                    GainBlock(5);
                    break;
                case "Twin Slam":
                    for (int i = 0; i < 2; i++)
                        Attack(hero, 8, encounter);
                    Buffs.Remove(FindBuff("Thorns"));
                    AddBuff(16, 30);
                    Actions.Clear();
                    break;
                case "Ultimate Blast":
                    Attack(hero, 25, encounter);
                    break;
                case "Vent Steam":
                    hero.AddBuff(1, 3);
                    hero.AddBuff(2, 3);
                    break;
                case "Web Spit":
                    hero.AddBuff(2, 3);
                    break;
                case "Whirlwind":
                    for (int i = 0; i < 4; i++)
                        Attack(hero, 5, encounter);
                    break;

            }
        }

        // Enemy Exclusive methods
        public void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Random enemyrng = new();
            switch (EnemyID)
            {
                default:                                                            // Any enemy who only uses one Intent
                    break;
                case 0:                                                             // Jaw Worm
                    if (turnNumber == 1)
                        break;
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 4 => "Chomp",
                        int i when i >= 5 && i <= 10 => "Thrash", 
                        _ => "Bellow",
                    };
                    Repeat3Prevent("Chomp", "Bellow", "Thrash");
                    break;
                case 1:                                                             // Cultist
                    if (turnNumber == 1)
                        break;
                    else Intent = "Dark Strike";
                    break;
                case 2:                                                             // Red Louse
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 4 => "Grow",
                        _ => "Bite",
                    };
                    Repeat3Prevent("Bite", "Grow");
                    break;
                case 3:                                                             // Med Acid Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Corrosive Spit",
                        int i when i >= 8 && i <= 15 => "Tackle",
                        _ => "Lick",
                    };
                    Repeat3Prevent("Corrosive Spit", "Tackle", "Lick");
                    break;
                case 4:                                                             // Med Spike Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 5 => "Flame Tackle",
                        _ => "Lick",
                    };
                    Repeat3Prevent("Flame Tackle", "Lick");
                    break;
                case 5:                                                             // Small Acid Slime

                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 9 => "Tackle",
                        _ => "Lick",
                    };
                    Repeat3Prevent("Tackle", "Lick");
                    break;
                case 7:                                                             // Green Louse
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 4 => "Web Spit",
                        _ => "Bite",
                    };
                    Repeat3Prevent("Bite", "Web Spit");
                    break;
                case 8:                                                             //Blue Slaver
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Rake",
                        _ => "Stab",
                    };
                    Repeat3Prevent("Stab", "Rake");
                    break;
                case 9:                                                             //Red Slaver
                    if (turnNumber == 1)
                        break;
                    if (!Actions.Contains("Entangle"))
                        Intent = enemyrng.Next(0, 20) switch
                        {
                            int i when i >= 0 && i <= 4 => "Entangle",
                            _ => "Determine",
                        };
                    else Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 10 => "Scrape",
                        _ => "Stab",
                    };
                    if (Intent == "Determine")
                    {
                        if (Actions != null && Actions.Count % 3 == 0)
                            Intent = "Stab";
                        else Intent = "Scrape";
                    }
                    if (Actions != null && Actions.Count >= 2)
                        Repeat3Prevent("Stab", "Scrape");
                    break;
                case 10:                                                            //Fungi Beast
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Grow",
                        _ => "Bite",
                    };
                    Repeat3Prevent("Bite", "Grow");
                    break;
                case 11:                                                            //Looters
                    if (turnNumber == 1 || turnNumber == 2)
                        break;
                    else if (turnNumber == 3)
                        Intent = enemyrng.Next(0, 20) switch
                        {
                            int i when i >= 0 && i <= 7 => "Lunge",
                            _ => "Smoke Bomb",
                        };
                    if (Actions != null && Actions.Count >= 3)
                        if (Actions[^1] == "Lunge")
                            Intent = "Smoke Bomb";
                        else Intent = "Escape";
                    break;
                case 14:                                                            //Gremlin Wizard
                    if (Actions != null && Actions.Count % 3 == 0)
                        Intent = "Ultimate Blast";
                    else Intent = "Charging";
                    break;
                case 16:                                                            //Shield Gremlin
                    bool targetExists = false;
                    for (int i = 0; i < encounter.Count; i++)
                        if (encounter[i].Hp != 0 && encounter[i] != this)
                            targetExists = true;
                    if (targetExists)
                        Intent = "Protect";
                    else Intent = "Shield Bash";
                    break;
                case 17:                                                            //Gremlin Nob
                    if (turnNumber == 1)
                        break;
                    Intent = enemyrng.Next(0, 21) switch
                    {
                        int i when i >= 0 && i <= 6 => "Skull Bash",
                        _ => "Rush",
                    };
                    Repeat3Prevent("Skull Bash", "Rush");
                    break;
                case 18:                                                            // Lagavulin
                    if (FindBuff("Asleep") != null)
                        break;
                    else if (Actions.Count >= 3 && Actions[^1] == "Attack" && Actions[^2] == "Attack")
                        Intent = "Siphon Soul";
                    else Intent = "Attack";
                    break;
                case 19:                                                            // Sentry
                    if (turnNumber == 1 && encounter.Count == 3)
                    {
                        encounter[1].Intent = "Beam";
                        break;
                    }
                    if (Intent == "Bolt")
                        Intent = "Beam";
                    else Intent = "Bolt";
                    break;
                case 20:                                                            // Slime Boss
                    Intent = (Actions.Count % 3) switch
                    {
                        int i when i == 0 => "Goop Spray",
                        int i when i == 1 => "Charging",
                        _ => "Slam",
                    };
                    break;
                case 21:                                                            // Large Acid Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Corrosive Spit",
                        int i when i >= 8 && i <= 15 => "Tackle",
                        _ => "Lick",
                    };
                    Repeat3Prevent("Corrosive Spit", "Tackle", "Lick");
                    break;
                case 22:                                                            // Large Spike Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 5 => "Flame Tackle",
                        _ => "Lick",
                    };
                    Repeat3Prevent("Flame Tackle", "Lick");
                    break;
                case 23:                                                            // The Guardian
                    if (FindBuff("Mode Shift") != null)
                    {
                        Intent = (Actions.Count % 4) switch
                        {
                            int i when i == 0 => "Charging Up",
                            int i when i == 1 => "Fierce Bash",
                            int i when i == 2 => "Vent Steam",
                            _ => "Whirlwind",
                        };
                    }
                    else if (Actions != null)
                    {
                        if (Actions[^1] == "Roll Attack")
                            Intent = "Twin Slam";
                        else if (Actions[^1] == "Defensive Mode")
                            Intent = "Roll Attack";
                    }
                    else Intent = "Defensive Mode";
                    break;
                case 24:                                                            // Hexaghost
                    if (turnNumber == 1)
                        break;
                    if (turnNumber == 2)
                    {
                        Intent = "Divider";
                        break;
                    }
                    Intent = ((Actions.Count - 2) % 7) switch
                    {
                        int i when i == 0 || i == 2 || i == 5 => "Sear",
                        int i when i == 1 || i == 4 => "Slice",
                        int i when i == 3 => "Inflame",
                        _ => "Inferno",
                    };
                    break;
            }
        }


        public void Repeat3Prevent(string one, string two)
        {
            if (Actions != null && Actions.Count >= 2)
                while (Actions[^1] == Actions[^2] && Intent == Actions[^1])
                {
                    if (Intent == one)
                        Intent = two;
                    else Intent = one;
                }
        }
        public void Repeat3Prevent(string one, string two, string three)
        {
            if (Actions != null && Actions.Count >= 2)
                while (Actions[^1] == Actions[^2] && Intent == Actions[^1])
                {
                    if (Intent == one)
                        Intent = two;
                    else if (Intent == two)
                        Intent = three;
                    else Intent = one;
                }
        }

        // Randomizes Enemy Health based on a range at start of encounter
        public int EnemyHealthSet()
        {
            Random r = new();
            int maxHP = r.Next(BottomHP, TopHP);
            return maxHP;
        }

        //enemy attack intents list
        public static List<string> AttackIntents()
        {
            List<string> list = new()
            {
                "Bite",
                "Chomp",
                "Corrosive Spit",
                "Bite",
                "Dark Strike",
                "Flame Tackle",
                "Tackle",
                "Thrash"
            };
            return list;
        }

        public static List<Enemy> CreateEncounter(int list)
        {
            List<Enemy> encounter = new();
            int encounterChoice;

            /* Looks at specific list (Encounter Type plus Act Modifier )
             * Encounter Types:
             * 1. Debut (first 3 normal combats each floor)
             * 2. Normal
             * 3. Elite
             * 4. Boss
             * 5. Event Combats
             * 
             * Level Modifier:
             * Act 1: 0
             * Act 2: 5
             * Act 3: 10
             * 
             * Example(Act 3 Elites): 10+3 = Case 13
             */

            switch (list)
            {
                default:
                    encounterChoice = EnemyRNG.Next(4);
                    switch (encounterChoice)
                    {
                        default:     //Cultist
                            encounter.Add(new Enemy(Dict.enemyL[0]));
                            break;
                        case 1:     //Jaw Worm
                            encounter.Add(new Enemy(Dict.enemyL[1]));
                            break;
                        case 2:     // 2 Louses (Red or Green)
                            for (int i = 0; i < 2; i++)
                                encounter.Add(new Enemy(Dict.enemyL[2 + EnemyRNG.Next(2)]));
                            break;
                        case 3:     // Small Slimes
                            if (EnemyRNG.Next(2) == 0)
                            {
                                encounter.Add(new Enemy(Dict.enemyL[7]));
                                encounter.Add(new Enemy(Dict.enemyL[4]));
                            }
                            else
                            {
                                encounter.Add(new Enemy(Dict.enemyL[6]));
                                encounter.Add(new Enemy(Dict.enemyL[5]));
                            }
                            break;
                    }
                    break;
                case 2:
                    encounterChoice = EnemyRNG.Next(0, 10);
                    switch (encounterChoice)
                    {
                        default:        // Gremlin Gang
                            for (int i = 0; i < 4; i++)
                            {
                                encounter.Add(new Enemy(Dict.enemyL[12 + EnemyRNG.Next(5)]));
                            }
                            break;
                        case 1:         // Large Slime
                            if (EnemyRNG.Next(2) == 0)
                                encounter.Add(new Enemy(Dict.enemyL[21]));
                            else
                                encounter.Add(new Enemy(Dict.enemyL[22]));
                            break;
                        case 2:         // Lots of Slimes
                            for (int i = 0; i < 3; i++)
                                encounter.Add(new Enemy(Dict.enemyL[6]));
                            for (int i = 0; i < 2; i++)
                                encounter.Add(new Enemy(Dict.enemyL[5]));
                            break;
                        case 3:         // Blue Slaver
                            encounter.Add(new Enemy(Dict.enemyL[8]));
                            break;
                        case 4:         // Red Slaver
                            encounter.Add(new Enemy(Dict.enemyL[9]));
                            break;
                        case 5:         // 3 Louses
                            for (int i = 0; i < 3; i++)
                            {
                                encounter.Add(new Enemy(Dict.enemyL[2 + EnemyRNG.Next(2)]));
                            }
                            break;
                        case 6:         // Fungi Beasts
                            for (int i = 0; i < 2; i++)
                                encounter.Add(new Enemy(Dict.enemyL[10]));
                            break;
                        case 7:         // Exordium Thugs
                            encounter.Add(new Enemy(Dict.enemyL[2 + EnemyRNG.Next(4)]));
                            switch (EnemyRNG.Next(4))
                            {
                                default:
                                    encounter.Add(new Enemy(Dict.enemyL[11]));
                                    break;
                                case 1:
                                    encounter.Add(new Enemy(Dict.enemyL[1]));
                                    break;
                                case 2:
                                    encounter.Add(new Enemy(Dict.enemyL[8]));
                                    break;
                                case 3:
                                    encounter.Add(new Enemy(Dict.enemyL[9]));
                                    break;
                            }
                            break;
                        case 8:         // Exordium Wildlife
                            if (EnemyRNG.Next(2) == 0)
                                encounter.Add(new Enemy(Dict.enemyL[0]));
                            else encounter.Add(new Enemy(Dict.enemyL[10]));
                            encounter.Add(new Enemy(Dict.enemyL[2 + EnemyRNG.Next(4)]));
                            break;
                        case 9:         //Looter
                            encounter.Add(new Enemy(Dict.enemyL[11]));
                            break;
                    }
                    break;
                case 3:
                    encounterChoice = EnemyRNG.Next(3);
                    switch (encounterChoice)
                    {
                        default:     // Lagavulin
                            encounter.Add(new Enemy(Dict.enemyL[18]));
                            break;
                        case 1:     // Gremlin Nob
                            encounter.Add(new Enemy(Dict.enemyL[17]));
                            break;
                        case 2:     // 3 Sentries
                            for (int i = 0; i < 3; i++)
                                encounter.Add(new Enemy(Dict.enemyL[19]));
                            break;
                    }
                    break;
                case 4:
                    encounterChoice = EnemyRNG.Next(3);
                    switch (encounterChoice)
                    {
                        default:     // Slime Boss
                            encounter.Add(new Enemy(Dict.enemyL[20]));
                            break;
                        case 1:     // The Guardian
                            encounter.Add(new Enemy(Dict.enemyL[23]));
                            break;
                        case 2:     // Hexaghost
                            encounter.Add(new Enemy(Dict.enemyL[24]));
                            break;
                    }
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;

            }

            for (int i = 0; i < encounter.Count; i++)
            {
                encounter[i].MaxHP = encounter[i].EnemyHealthSet();
                encounter[i].Hp = encounter[i].MaxHP;
                switch (encounter[i].EnemyID)
                {
                    default:
                        break;
                    case 2 or 7:
                        encounter[i].AddBuff(5, EnemyRNG.Next(3, 8));
                        break;
                    case 11:
                        encounter[i].AddBuff(18, 15);
                        break;

                }
            }
            return encounter;
        }
    }
}