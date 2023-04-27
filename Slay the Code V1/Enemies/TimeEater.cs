namespace STV
{
    public class TimeEater : Enemy
    {
        public TimeEater()
        {
            Name = "Time Eater";
            MaxHP = 456;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(116, 12);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Haste")
            {
                HealHP(MaxHP / 2 - Hp);
                Buffs.RemoveAll(x => !x.BuffDebuff);
            }
            else if (Intent == "Ripple")
            {
                GainBlock(20);
                hero.AddBuff(1, 2);
                hero.AddBuff(2, 2);
            }
            else if (Intent == "Head Slam")
            {
                Attack(hero, 26, encounter);
                hero.AddBuff(115, 2);
            }
            else for (int i = 0; i < 3; i++) 
                    Attack(hero, 7, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (Hp <= MaxHP / 2 && !Actions.Contains("Haste"))
                Intent = "Haste";
            Intent = EnemyRNG.Next(0, 20) switch
            {
                int i when i >= 0 && i <= 3 => "Ripple",
                int i when i >= 4 && i <= 12 => "Reverberate",
                _ => "Head Slam",
            };
        }
    }
}
