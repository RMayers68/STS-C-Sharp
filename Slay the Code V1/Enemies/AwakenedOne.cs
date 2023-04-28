namespace STV
{
    public class AwakenedOne : Enemy
    {
        public bool Revived { get; set; }
        public AwakenedOne()
        {
            Name = "Awakened One";
            MaxHP = 300;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(98, 10);
            AddBuff(117, 1);
            Actions = new();
            Relics = new();
            Revived = false;
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Sludge")
            {
                Attack(hero, 18, encounter);
                hero.DiscardPile.Add(Dict.cardL[359]);
            }
            else if (Intent == "Rebirth")
            {
                HealHP(MaxHP);
                Buffs.RemoveAll(x => !x.BuffDebuff);
            }
            else if (Intent == "Soul Strike")
                for (int i = 0; i < 4; i++)
                    Attack(hero, 6, encounter);
            else if (Intent == "Tackle")
                for (int i = 0; i < 3; i++)
                    Attack(hero, 10, encounter);
            else if (Intent == "Slash")
                Attack(hero, 20, encounter);
            else Attack(hero, 40, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber == 0)
                Intent = "Slash";
            else if (!Revived)
            {
                if (Hp <= 0)
                {
                    Intent = "Rebirth";
                    Buffs.Remove(FindBuff("Curiosity"));
                }                   
                else Intent = EnemyRNG.Next(0, 20) switch
                {
                    int i when i >= 0 && i <= 4 => "Soul Strike",
                    _ => "Slash",
                };
            }               
            else if (Actions.Last() == "Rebirth")
                Intent = "Dark Echo";
            else Intent = EnemyRNG.Next(0, 20) switch
            {
                0 => "Sludge",
                _ => "Tackle",
            };
        }
    }
}
