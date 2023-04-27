﻿namespace STV
{
    public class LargeAcidSlime : Enemy
    {
        public LargeAcidSlime()
        {
            Name = "Acid Slime (L)";
            TopHP = 70;
            BottomHP = 65;
            Intents = new() { "Corrosive Spit", "Lick", "Tackle" };
            MaxHP = EnemyRNG.Next(BottomHP, TopHP);
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(101, 1);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Corrosive Spit")
            {
                Attack(hero, 11, encounter);
                for (int i = 0; i < 2; i++)
                    hero.DiscardPile.Add(Dict.cardL[358]);
            }
            else if (Intent == "Tackle")
                Attack(hero, 16, encounter);
            else if (Intent == "Lick") 
                hero.AddBuff(2, 2);
            else
            {
                // Split Function
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 7 => "Corrosive Spit",
                int i when i >= 8 && i <= 15 => "Tackle",
                _ => "Lick",
            };
        }
    }
}
