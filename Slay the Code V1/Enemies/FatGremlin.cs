﻿namespace STV
{
    public class FatGremlin : Enemy
    {
        public FatGremlin(bool minion = false)
        {
            Name = "Fat Gremlin";
            MaxHP = EnemyRNG.Next(13, 18);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            if (minion)
                AddBuff(118, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            Attack(hero, 4, encounter);
            hero.AddBuff(2, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = "Smash";
        }
    }
}
