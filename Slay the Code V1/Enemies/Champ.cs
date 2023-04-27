namespace STV
{
    public class Champ : Enemy
    {
        public Champ()
        {
            Name = "The Champ";
            MaxHP = 420;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            if (Intent == "Face Slap")
            {
                Attack(hero,12,encounter);
                hero.AddBuff(6, 2);
                hero.AddBuff(1, 2);
            }
            else if (Intent == "Defensive Stance")
            {               
                GainBlock(15);
                AddBuff(32, 5);
            }
            else if (Intent == "Taunt")
            {
                hero.AddBuff(2, 2);
                hero.AddBuff(1, 2);
            }
            else if (Intent == "Anger")
            {
                AddBuff(4, 2);
                Buffs.RemoveAll(x => !x.BuffDebuff);
            }
            else if (Intent == "Execute")
                for (int i = 0; i < 2; i++)
                    Attack(hero, 10, encounter);
            else if (Intent == "Gloat")
                AddBuff(4, 2);
            else Attack(hero, 16, encounter);
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (!Actions.Contains("Anger"))
            {
                if (Hp < MaxHP / 2)
                    Intent = "Anger";
                else if (!(Actions.FindAll( x => x == "Defensive Stance").Count < 2))
                    Intent = EnemyRNG.Next(20) switch
                    {
                        0 or 1 or 2 => "Defensive Stance",
                        3 or 4 or 5 => "Gloat",
                        int i when i >= 6 && i <= 10 => "Face Slap",
                        _ => "Heavy Slash",
                    };
                else Intent = EnemyRNG.Next(20) switch
                {
                    int i when i >= 0 && i <= 5 => "Gloat",
                    int i when i >= 6 && i <= 10 => "Face Slap",
                    _ => "Heavy Slash",
                };
            }
            else
            {
                if (Actions.Last() == "Anger" || Actions[^3] == "Execute")
                    Intent = "Execute";
                else if (!(Actions.FindAll(x => x == "Defensive Stance").Count < 2))
                    Intent = EnemyRNG.Next(20) switch
                    {
                        0 or 1 or 2 => "Defensive Stance",
                        3 or 4 or 5 => "Gloat",
                        int i when i >= 6 && i <= 10 => "Face Slap",
                        _ => "Heavy Slash",
                    };
                else Intent = EnemyRNG.Next(20) switch
                {
                    int i when i >= 0 && i <= 5 => "Gloat",
                    int i when i >= 6 && i <= 10 => "Face Slap",
                    _ => "Heavy Slash",
                };
            }
        }
    }
}