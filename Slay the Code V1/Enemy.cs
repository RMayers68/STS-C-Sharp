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

        public abstract void EnemyAction(Hero hero, List<Enemy> encounter);

        public abstract void SetEnemyIntent(int turnNumber, List<Enemy> encounter);

        //enemy attack intents list
        public static List<string> AttackIntents()
        {
            List<string> list = new()
            {
                "Bite",
                "Chomp",
                "Corrosive Spit",
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

            return list switch
            {
                1 => EnemyRNG.Next(4) switch
                {
                    0 => new() { new JawWorm() },
                    1 => new() { new Cultist() },
                    2 => new() { RandomLouse(), RandomLouse() },
                    _ => new() { RandomMedSlime(), RandomSmallSlime() },
                },
                2 => EnemyRNG.Next(10) switch
                {
                    0 => new() { RandomGremlin(),RandomGremlin(), RandomGremlin(), RandomGremlin() },
                    1 => new() { RandomLargeSlime() },
                    2 => new() { new SmallSpikeSlime(), new SmallAcidSlime(), new SmallSpikeSlime(), new SmallSpikeSlime(), new SmallAcidSlime() },
                    3 => new() { new BlueSlaver() },
                    4 => new() { new RedSlaver() },
                    5 => new() { new FungiBeast(), new FungiBeast() },
                    6 => new() { RandomLouse(), RandomLouse(), RandomLouse() },
                    7 => new() { RandomExordium(), RandomThug() },
                    8 => new() { RandomWildlife(), RandomExordium() },
                    _ => new() { new Looter() },
                },
                3 => EnemyRNG.Next(3) switch
                {
                    0 => new() { new Lagavulin() },
                    1 => new() { new GremlinNob() },
                    _ => new() { new Sentry(), new Sentry(), new Sentry() },
                },
                4 => EnemyRNG.Next(3) switch
                {
                    0 => new() { new SlimeBoss() },
                    1 => new() { new Guardian() },
                    _ => new() { new Hexaghost() },
                },
                5 => EnemyRNG.Next(3) switch
                {
                    _ => new() { new SmallAcidSlime(), },
                },
                6 => EnemyRNG.Next(5) switch
                {
                    0 => new() { new SphericGuardian() },
                    1 => new() { new Chosen() },
                    2 => new() { new Byrd(), new Byrd(), new Byrd() },
                    3 => new() { new ShelledParasite() },
                    _ => new() { new Looter(), new Mugger() },
                },
                7 => EnemyRNG.Next(15) switch
                {
                    0 => new() { new Chosen(), new Byrd() },
                    1 => new() { new Chosen(), new Cultist() },
                    2 => new() { new Sentry(), new SphericGuardian() },
                    3 or 4 or 5 => new() { new SnakePlant() },
                    6 or 7 => new() { new Snecko() },
                    8 or 9 => new() { new Cultist(), new Cultist(), new Cultist() },
                    9 or 10 => new() { new ShelledParasite(), new FungiBeast()},
                    _ => new() { new Centurion(), new Mystic() },
                },
                8 => EnemyRNG.Next(3) switch
                {
                    0 => new() { new BookOfStabbing() },
                    1 => new() { new GremlinLeader(), RandomGremlin(), RandomGremlin() },
                    _ => new() { new BlueSlaver(), new Taskmaster(), new RedSlaver() },
                },
                9 => EnemyRNG.Next(3) switch
                {
                    0 => new() { new BronzeAutomaton() },
                    1 => new() { new Champ() },
                    _ => new() { new Collector() },
                },
                10 => EnemyRNG.Next(3) switch
                {
                    _ => new() { new SmallAcidSlime(), },
                },
            };
        }

        private static Enemy RandomLouse()
        {
            return EnemyRNG.Next(2) switch
            {
                0 => new RedLouse(),
                _ => new GreenLouse(),
            };
        }

        private static Enemy RandomLargeSlime()
        {
            return EnemyRNG.Next(2) switch
            {
                0 => new LargeAcidSlime(),
                _ => new LargeSpikeSlime(),
            };
        }

        private static Enemy RandomMedSlime()
        {
            return EnemyRNG.Next(2) switch
            {
                0 => new MediumAcidSlime(),
                _ => new MediumSpikeSlime(),
            };
        }

        private static Enemy RandomSmallSlime()
        {
            return EnemyRNG.Next(2) switch
            {
                0 => new SmallAcidSlime(),
                _ => new SmallSpikeSlime(),
            };
        }

        private static Enemy RandomExordium()
        {
            return EnemyRNG.Next(3) switch
            {
                0 => RandomLouse(),
                1 => new MediumAcidSlime(),
                _ => new MediumSpikeSlime(),
            };
        }

        private static Enemy RandomThug()
        {
            return EnemyRNG.Next(4) switch
            {
                0 => new Looter(),
                1 => new Cultist(),
                2 => new RedSlaver(),
                _ => new BlueSlaver(),
            };
        }

        private static Enemy RandomWildlife()
        {
            return EnemyRNG.Next(2) switch
            {
                0 => new FungiBeast(),
                _ => new JawWorm(),
            };
        }

        public static Enemy RandomGremlin()
        {
            return EnemyRNG.Next(5) switch
            {
                0 => new SneakyGremlin(),
                1 => new MadGremlin(),
                2 => new GremlinWizard(),
                3 => new FatGremlin(),
                _ => new ShieldGremlin(),
            };
        }
    }
}