using static Global.Functions;
namespace STV
{
    public class RestSite
    {
        public static void Rest(Hero hero)
        {
            if (hero.HasRelic("Eternal"))
                hero.HealHP(3 * (hero.Deck.Count / 5));
            List<string> choices = new() { "R", "S" };
            List<string> Options = new() { "[R]est - Heal 30% HP", "[S]mith - Upgrade a Card" };
            if (hero.HasRelic("Coffee"))
            {
                choices.Remove("R");
                Options[0] = "Locked - Can't Rest";
            }
            if (hero.HasRelic("Fusion Hammer") || hero.Deck.FindAll(x => x.Type != "Curse").FindAll(x => !x.Upgraded).Count < 1) ;
            {
                choices.Remove("S");
                Options[1] = "Locked - Can't Upgrade";
            }
            if (hero.FindRelic("Girya") is Relic lift && lift.PersistentCounter > 0)
            {
                choices.Add("L");
                Options.Add("[L]ift - Gain 1 Strength");
            }
            if (hero.HasRelic("Peace") && hero.Deck.Count > 0)
            {
                choices.Add("T");
                Options.Add("[T]oke - Remove a Card");
            }
            if (hero.HasRelic("Shovel"))
            {
                choices.Add("D");
                Options.Add("[D]ig - Obtain a Relic");
            }
            if (choices.Count == 0)
            {
                Options.Add("[M]ove On");
                choices.Add("M");
            }
            ScreenWipe();
            Console.WriteLine($"Hello {hero.Name}! You have arrived at a campfire. What would you like to do? Enter your option.\n");
            string restChoice = "";
            while (choices.Any(x => x == restChoice))
            {
                restChoice = Console.ReadLine().ToUpper();
            }
            switch (restChoice)
            {
                default:
                    Console.WriteLine("Okay, you're really just leaving? See ya bud.");
                    break;
                case "R": // Rest
                    int regalPillow = 0;
                    if (hero.HasRelic("Regal"))
                        regalPillow = 15;
                    hero.HealHP(Convert.ToInt32(hero.MaxHP * 0.3) + regalPillow);
                    if (hero.HasRelic("Dream"))
                        hero.AddToDeck(Card.PickCard(Card.RandomCards(hero.Name, 3), "add to your Deck"));
                    break;
                case "S": // Upgrade
                    if (hero.Deck.FindAll(x => x.Type != "Curse").Any(x => !x.Upgraded))
                        Card.PickCard(hero.Deck.FindAll(x => !x.Upgraded), "upgrade").UpgradeCard();
                    break;
                case "L": // Lift
                    if (hero.FindRelic("Girya") is Relic girya && girya != null)
                    {
                        girya.EffectAmount++;
                        girya.PersistentCounter--;
                    }
                    break;
                case "T": // Toke
                    hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
                    break;
                case "D": //Dig
                    hero.AddToRelics(Relic.RandomRelic(hero));
                    break;
            }
            Pause();
            if (hero.HasRelic("Ancient"))
                hero.GainTurnEnergy(2);
        }
    }
}
