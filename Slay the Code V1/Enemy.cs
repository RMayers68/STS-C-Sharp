using System;

namespace STV
{
    public abstract class Enemy : Actor
    {
        public int BottomHP { get; set; }
        public int TopHP { get; set; }
        public List<string> Intents { get; set; }
        public string Intent { get; set; }
        public static readonly Random EnemyRNG = new();


        public Enemy()
        {
        }

        public Enemy(string name, int bottomHP, int topHP, string intent)
        {
            this.Name = name;
            this.MaxHP = topHP;
            this.Hp = this.MaxHP;
            this.TopHP = topHP + 1;
            this.BottomHP = bottomHP;
            this.Block = 0;
            this.Intent = intent;
            this.Buffs = new();
            this.Actions = new();
        }

        public Enemy(Enemy enemy)
        {
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

        public abstract void EnemyAction(Hero hero, List<Enemy> encounter);

        // Enemy Exclusive methods
        public abstract void SetEnemyIntent(int turnNumber, List<Enemy> encounter);

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
                "Thrash",
                "Lunge",
                "Mug",
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
                        default:    
                            encounter.Add(new JawWorm(Dict.enemyL[0]));
                            break;
                        case 1:     
                            encounter.Add(new Cultist(Dict.enemyL[1]));
                            break;
                        case 2:     // 2 Louses (Red or Green)
                            for (int i = 0; i < 2; i++)
                            {
                                if (EnemyRNG.Next(2) == 0)
                                    encounter.Add(new RedLouse(Dict.enemyL[2]));
                                else encounter.Add(new GreenLouse(Dict.enemyL[3]));
                            }
                            break;
                        case 3:     // Small Slimes
                            if (EnemyRNG.Next(2) == 0)
                            {
                                encounter.Add(new MediumSpikeSlime(Dict.enemyL[8]));
                                encounter.Add(new SmallAcidSlime(Dict.enemyL[4]));
                            }
                            else
                            {
                                encounter.Add(new MediumAcidSlime(Dict.enemyL[5]));
                                encounter.Add(new SmallSpikeSlime(Dict.enemyL[7]));
                            }
                            break;
                    }
                    break;
                /*case 2:
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
                                encounter.Add(new SmallAcidSlime(Dict.enemyL[5]));
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
                    break;*/
            }
            return encounter;
        }
    }
}