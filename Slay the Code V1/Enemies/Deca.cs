namespace STV
{
    public class Deca : Enemy
    {
        public Deca()
        {
            Name = "Deca";
            MaxHP = 250;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(8, 2);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Beam")
                for (int i = 0; i < 2; i++)
                {
                    Attack(hero, 10, encounter);
                    hero.DiscardPile.Add(new(Dict.cardL[356]));
                }
            else foreach (Enemy e in encounter)
                    e.GainBlock(16);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (turnNumber % 2 == 0)
                Intent = "Square of Protection";
            else Intent = "Beam";
        }
    }
}