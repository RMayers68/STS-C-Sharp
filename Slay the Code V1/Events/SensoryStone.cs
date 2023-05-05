namespace STV
{
    public class SensoryStone : Event
    {
        public SensoryStone()
        {
            Name = "Sensory Stone";
            StartOfEncounter = $"Navigating through the Beyond, you discover a glowing tesseract spinning and shifting gently in the air.\nYou touch it.\nA sharp pain flows through you, followed by vivid flashes of a distant memory.\n...whose memories are these?";
            Options = new() { "Recall - Add [1] Colorless Card", "Recall - Add [2] Colorless Cards, Take 5 Damage", "Recall - Add [3] Colorless Cards, Take 10 Damage" };
        }

        public override void EventAction(Hero hero)
        {
            List<string> choices = new() { "1", "2", "3" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            int amount = Int32.Parse(playerChoice);
            for (int i = 0; i < amount; i++)
                hero.AddToDeck(Card.ChooseCard(3, "add", "Colorless"));
            if (amount > 1)
                hero.SelfDamage((amount - 1) * 5);
            Console.WriteLine(Result(EventRNG.Next(4)));
        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "FEAR.\nA demonic creature towers above you, wings spread wide as it howls with laughter.\nDead bodies of a tribe surround you while the village is engulfed in terrible dark flames.\nThe demon calls out, taunting you.\n\"YOU REALLY ARE THE STRONGEST NOW! Haha.. HEHE... HAHAHAAAAH!!\"\nThis laughter echoes forever...",
                1 => "TRIUMPH.\nThe remains of a ghostly creature sink slowly into the mud before you, barely visible in the moonlight.\nYou have proven yourself amongst your sisters.\nStanding victoriously, you wait in silence as the others ceremoniously place the creature's skull atop your head. The ritual has concluded.\nYou head towards the Spire...",
                2 => "CONFUSION.\n[OBJECTIVE] BALANCE must be ENFORCED\n[DEFINE] BALANCE\n[ERROR] BALANCE NOT FOUND\n[DEFINE] BALANCE\n[ERROR] BALANCE NOT FOUND\n[WARNING] Large object approaching\n\"I... ..am ....Neow..\"",
                _ => "SERENITY.\nTwo primitive creatures fight over a carcass on the side of the road. You observe, devoid of emotion.\nWatch. Remember. Live. This is the Watcher's mission.\nRecently, one of your peers had stopped reporting on their assignment: a Spire of unknown origin.\nAs the fight ends, you continue onward, unfazed by the bloody scene that took place.",
            };
        }
    }
}
