﻿namespace STV
{
    public class SmallAcidSlime : Enemy
    {
        public SmallAcidSlime()
        {
            Name = "Acid Slime (S)";
            MaxHP = EnemyRNG.Next(8, 13);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Tackle")
                Attack(hero, 3, encounter);
            else hero.AddBuff(2, 1);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 9 => "Tackle",
                _ => "Lick",
            };
        }
    }
}