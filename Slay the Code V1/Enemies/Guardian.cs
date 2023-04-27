namespace STV
{
    public class Guardian : Enemy
    {
        public Guardian()
        {
            Name = "Guardian";
            MaxHP = 250;
            Hp = MaxHP;
            Block = 0;
            Buffs = new();
            AddBuff(16, 30);
            Actions = new();
            Relics = new();
        }

        public override void EnemyAction(Hero hero, List<Enemy> encounter)
        {
            switch (Intent)
            {
                case "Charging Up":
                    GainBlock(9);
                    break;
                case "Defensive Mode":
                    AddBuff(16, 3);
                    break;
                case "Divider":
                    break;
                case "Fierce Bash":
                    Attack(hero, 32, encounter);
                    break;
                case "Roll Attack":
                    Attack(hero, 9, encounter);
                    break;
                case "Twin Slam":
                    for (int i = 0; i < 2; i++)
                        Attack(hero, 8, encounter);
                    Buffs.Remove(FindBuff("Thorns"));
                    AddBuff(16, 30);
                    Actions.Clear();
                    break;
                case "Vent Steam":
                    hero.AddBuff(1, 3);
                    hero.AddBuff(2, 3);
                    break;
                case "Whirlwind":
                    for (int i = 0; i < 4; i++)
                        Attack(hero, 5, encounter);
                    break;
            }
        }

        public override void SetEnemyIntent(int turnNumber, List<Enemy> encounter)
        {
            if (HasBuff("Mode Shift"))
            {
                Intent = (Actions.Count % 4) switch
                {
                    0 => "Charging Up",
                    1 => "Fierce Bash",
                    2 => "Vent Steam",
                    _ => "Whirlwind",
                };
            }
            else if (Actions != null)
            {
                if (Actions[^1] == "Roll Attack")
                    Intent = "Twin Slam";
                else if (Actions[^1] == "Defensive Mode")
                    Intent = "Roll Attack";
            }
            else Intent = "Defensive Mode";
        }
    }
}