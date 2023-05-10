namespace STV
{
    public class BigFish : Event
    {
        public BigFish()
        {
            Name = "Big Fish";
            StartOfEncounter = $"As you make your way down a long corridor you see a banana, a donut, and a chest floating about. No... upon closer inspection they are tied to strings coming from holes in the ceiling. There is a quiet cackling from above as you approach the objects.\r\nWhat do you do?";
            Options = new() { "[B]anana - Heal 1/3 of HP", "[D]onut - Max HP + 5", "[C]hest - Relic & Regret" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "B", "D", "C" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (!choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "B")
            {
                Console.WriteLine(Result(0));
                hero.HealHP(hero.MaxHP / 3);
            }
            else if (playerChoice == "D")
            {
                Console.WriteLine(Result(1));
                hero.SetMaxHP(5);
            }
            else
            {
                Console.WriteLine(Result(2));
                hero.AddToRelics(Relic.RandomRelic(hero));
                hero.AddToDeck(new Regret());
            }
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "You eat the banana. It is nutritious and slightly magical, healing you.",
                1 => "You eat the donut. It really hits the spot! Your Max HP increases.",
                _ => "You grab the box. Inside you find a relic!\r\nHowever, you really craved the donut...\r\nYou are filled with sadness, but mostly regret.",
            };
        }
    }
}
