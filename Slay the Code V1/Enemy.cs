namespace STV
{
    public class Enemy : Actor
    {
        public int EnemyID { get; set; } // ID correlates to method ran (Name without spaces)
        public string? Intent { get; set; }
        


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
        }
        public void EnemyAction(Hero hero, List<Card> drawPile, List<Card> discardPile, List<Enemy> encounter)
        {
            int damage = 0;
            int target = 0;
            Random rng = new Random();
            switch (Intent)
            {
                case "Attack":
                    SingleAttack(hero, 18);
                    break;
                case "Beam":
                    SingleAttack(hero, 9);
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
                        SingleAttack(hero, MaxHP / 2);
                    else SingleAttack(hero, 6);
                    break;
                case "Bolt":
                    for (int i = 0; i < 2; i++)
                        discardPile.Add(new Card(Dict.cardL[356]));
                    Console.WriteLine($"{Name} has added 2 Dazed cards to your deck!");
                    break;
                case "Charging":
                    Console.WriteLine($"{Name} is charging up!");
                    break;
                case "Charging Up":
                    GainBlock(9);
                    break;
                case "Chomp":
                    SingleAttack(hero, 11);
                    break;
                case "Corrosive Spit":
                    SingleAttack(hero, 7);
                    discardPile.Add(Dict.cardL[358]);
                    if (EnemyID == 21)
                        discardPile.Add(Dict.cardL[358]);
                    break;
                case "Dark Strike":
                    SingleAttack(hero, 6);
                    break;
                case "Defensive Mode":
                    AddBuff(16, 3);
                    break;
                case "Divider":
                    damage = hero.Hp / 12 + 1;
                    for (int i = 0; i < 6; i++)
                        SingleAttack(hero, damage);
                    break;
                case "Entangle":
                    hero.AddBuff(14, 2);
                    break;
                case "Escape":
                    encounter.Remove(this);
                    Console.WriteLine($"The {Name} has escaped!");
                    STS.Pause();
                    break;
                case "Fierce Bash":
                    SingleAttack(hero, 32);
                    break;
                case "Flame Tackle":
                    if (EnemyID == 22)
                        damage = 16;
                    else damage = 8;
                    SingleAttack(hero, damage);
                    discardPile.Add(Dict.cardL[358]);
                    if (EnemyID == 22)
                        discardPile.Add(Dict.cardL[358]);
                    break;
                case "Goop Spray":
                    for (int i = 0; i < 3; i++)
                        discardPile.Add(new Card(Dict.cardL[358]));
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
                        SingleAttack(hero, 2);
                        if (i % 2 == 0)
                            discardPile.Add(new Card(Dict.cardL[355]));
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
                    SingleAttack(hero, 12);
                    hero.Gold -= Buffs.Find(x => x.Name == "Thievery").Intensity.GetValueOrDefault(15);
                    Gold += Buffs.Find(x => x.Name == "Thievery").Intensity.GetValueOrDefault(15);
                    Console.WriteLine($"The {Name} stole 15 Gold!");
                    break;
                case "Mug":
                    SingleAttack(hero, 10);
                    hero.Gold -= Buffs.Find(x => x.Name == "Thievery").Intensity.GetValueOrDefault(15);
                    Gold += Buffs.Find(x => x.Name == "Thievery").Intensity.GetValueOrDefault(15);
                    Console.WriteLine($"The {Name} stole 15 Gold!");
                    break;
                case "Protect":
                    while (target != encounter.FindIndex(x => x == this))
                        target = rng.Next(0, encounter.Count);
                    encounter[target].GainBlock(7);
                    break;
                case "Puncture":
                    SingleAttack(hero, 9);
                    break;
                case "Rake":
                    SingleAttack(hero, 7);
                    hero.AddBuff(2, 2);
                    break;
                case "Roll Attack":
                    SingleAttack(hero, 9);
                    break;
                case "Rush":
                    SingleAttack(hero, 14);
                    break;
                case "Scrape":
                    SingleAttack(hero, 8);
                    hero.AddBuff(1, 2);
                    break;
                case "Scratch":
                    SingleAttack(hero, 4);
                    break;
                case "Shield Bash":
                    SingleAttack(hero, 6);
                    break;
                case "Sear":
                    SingleAttack(hero, 6);
                    discardPile.Add(new Card(Dict.cardL[355]));
                    Console.WriteLine($"{Name} has added a Burn to your Deck!");
                    break;
                case "Siphon Soul":
                    hero.AddBuff(9, -1);
                    hero.AddBuff(4, -1);
                    break;
                case "Skull Bash":
                    SingleAttack(hero, 6);
                    hero.AddBuff(1, 3);
                    break;
                case "Slam":
                    SingleAttack(hero, 35);
                    break;
                case "Sleeping":
                    Console.WriteLine($"{Name} is sleeping, be cautious on waking it...");
                    break;
                case "Smash":
                    SingleAttack(hero, 4);
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
                    SingleAttack(hero, damage);
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
                            SingleAttack(hero, damage);
                            break;
                    }
                    SingleAttack(hero, damage);
                    break;
                case "Thrash":
                    SingleAttack(hero, 7);
                    GainBlock(5);
                    break;
                case "Twin Slam":
                    for (int i = 0; i < 2; i++)
                        SingleAttack(hero, 8);
                    Buffs.Remove(Buffs.Find(x => x.Name == "Sharp Hide"));
                    AddBuff(16, 30);
                    Actions.Clear();
                    break;
                case "Ultimate Blast":
                    SingleAttack(hero, 25);
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
                        SingleAttack(hero, 5);
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
                        int i when i >= 11 && i <= 19 => "Bellow",
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
                        int i when i >= 5 && i <= 19 => "Bite",
                    };
                    Repeat3Prevent("Bite", "Grow");
                    break;
                case 3:                                                             // Med Acid Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Corrosive Spit",
                        int i when i >= 8 && i <= 15 => "Tackle",
                        int i when i >= 16 && i <= 19 => "Lick",
                    };
                    Repeat3Prevent("Corrosive Spit", "Tackle", "Lick");
                    break;
                case 4:                                                             // Med Spike Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 5 => "Flame Tackle",
                        int i when i >= 6 && i <= 19 => "Lick",
                    };
                    Repeat3Prevent("Flame Tackle", "Lick");
                    break;
                case 5:                                                             // Small Acid Slime

                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 9 => "Tackle",
                        int i when i >= 10 && i <= 19 => "Lick",
                    };
                    Repeat3Prevent("Tackle", "Lick");
                    break;
                case 7:                                                             // Green Louse
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 4 => "Web Spit",
                        int i when i >= 5 && i <= 19 => "Bite",
                    };
                    Repeat3Prevent("Bite", "Web Spit");
                    break;
                case 8:                                                             //Blue Slaver
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Rake",
                        int i when i >= 8 && i <= 19 => "Stab",
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
                            int i when i >= 5 && i <= 19 => "Determine",
                        };
                    else Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 10 => "Scrape",
                        int i when i >= 11 && i <= 19 => "Stab",
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
                        int i when i >= 8 && i <= 19 => "Bite",
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
                            int i when i >= 8 && i <= 19 => "Smoke Bomb",
                        };
                    if (Actions != null && Actions.Count >= 3)
                        if (Actions[Actions.Count - 1] == "Lunge")
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
                        int i when i >= 7 && i <= 20 => "Rush",
                    };
                    Repeat3Prevent("Skull Bash", "Rush");
                    break;
                case 18:                                                            // Lagavulin
                    if (Buffs.Find(x => x.Name == "Asleep") != null)
                        break;
                    else if (Actions.Count >= 3 && Actions[Actions.Count - 1] == "Attack" && Actions[Actions.Count - 2] == "Attack")
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
                        int i when i == 3 => "Slam",
                    };
                    break;
                case 21:                                                            // Large Acid Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 7 => "Corrosive Spit",
                        int i when i >= 8 && i <= 15 => "Tackle",
                        int i when i >= 16 && i <= 19 => "Lick",
                    };
                    Repeat3Prevent("Corrosive Spit", "Tackle", "Lick");
                    break;
                case 22:                                                            // Large Spike Slime
                    Intent = enemyrng.Next(0, 20) switch
                    {
                        int i when i >= 0 && i <= 5 => "Flame Tackle",
                        int i when i >= 6 && i <= 19 => "Lick",
                    };
                    Repeat3Prevent("Flame Tackle", "Lick");
                    break;
                case 23:                                                            // The Guardian
                    if (Buffs.Find(x => x.Name == "Mode Shift") != null)
                    {
                        Intent = (Actions.Count % 4) switch
                        {
                            int i when i == 0 => "Charging Up",
                            int i when i == 1 => "Fierce Bash",
                            int i when i == 2 => "Vent Steam",
                            int i when i == 3 => "Whirlwind",
                        };
                    }
                    else if (Actions != null)
                    {
                        if (Actions[Actions.Count - 1] == "Roll Attack")
                            Intent = "Twin Slam";
                        else if (Actions[Actions.Count - 1] == "Defensive Mode")
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
                        int i when i == 6 => "Inferno",
                    };
                    break;
            }
        }


        public void Repeat3Prevent(string one, string two)
        {
            if (Actions != null && Actions.Count >= 2)
                while (Actions[Actions.Count - 1] == Actions[Actions.Count - 2] && Intent == Actions[Actions.Count - 1])
                {
                    if (Intent == one)
                        Intent = two;
                    else Intent = one;
                }
        }
        public void Repeat3Prevent(string one, string two, string three)
        {
            if (Actions != null && Actions.Count >= 2)
                while (Actions[Actions.Count - 1] == Actions[Actions.Count - 2] && Intent == Actions[Actions.Count - 1])
                {
                    if (Intent == one)
                        Intent = two;
                    else if (Intent == two)
                        Intent = three;
                    else Intent = one;
                }
        }

        // Randomizes Enemy Health based on a range at start of encounter
        public int EnemyHealthSet(int bottom, int top)
        {
            Random r = new Random();
            int maxHP = r.Next(bottom, top + 1);
            return maxHP;
        }

        //enemy attack intents list
        public static List<string> AttackIntents()
        {
            List<string> list = new List<string>();
            list.Add("Bite");
            list.Add("Chomp");
            list.Add("Corrosive Spit");
            list.Add("Bite");
            list.Add("Dark Strike");
            list.Add("Flame Tackle");
            list.Add("Tackle");
            list.Add("Thrash");
            return list;
        }
    }
}
