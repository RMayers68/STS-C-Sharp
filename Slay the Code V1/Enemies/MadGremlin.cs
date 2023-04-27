﻿namespace STV
{
    public class MadGremlin : Enemy
    {
        public MadGremlin()
        {
            Name = "Mad Gremlin";
            TopHP = 25;
            BottomHP = 20;
            Intents = new() { "Scratch" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(103, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 4, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = Intents.First();
        }
    }
}