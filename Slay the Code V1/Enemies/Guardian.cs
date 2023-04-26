namespace STV
{
    public class Guardian : Enemy
    {
        public Guardian()
        {
            this.Name = "Guardian";
            this.Intents = new() { "Charging Up", "Fierce Bash", "Vent Steam", "Whirlwind", "Defensive Mode", "Roll Attack", "Twin Slam" };
        }

        public Guardian(Enemy e)
        {
            this.Name = e.Name;
            this.MaxHP = 250;
            this.Hp = this.MaxHP;
            this.Block = 0;
            this.Intents = e.Intents;
            this.Buffs = new();
            AddBuff(16, 30);
            this.Actions = new();
            this.Relics = new();
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
                    int i when i == 0 => "Charging Up",
                    int i when i == 1 => "Fierce Bash",
                    int i when i == 2 => "Vent Steam",
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