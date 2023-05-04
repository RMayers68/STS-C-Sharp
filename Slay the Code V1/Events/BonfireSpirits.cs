namespace STV
{
    public class BonfireSpirits : Event
    {
        public BonfireSpirits()
        {
            Name = "Bonfire Spirits";
            StartOfEncounter = $"You happen upon a group of what looks like purple fire spirits dancing around a large bonfire.\r\nThe spirits toss small bones and fragments into the fire, which brilliantly erupts each time. As you approach, the spirits all turn to you, expectantly...";
        }

        public override void EventAction(Hero hero)
        {
            Console.WriteLine(StartOfEncounter);
            if (hero.Deck.Count > 0)
            {
                Console.WriteLine($"\nYou toss an offering into the bonfire...");
                Card offering = Card.PickCard(hero.Deck, "remove");
                hero.Deck.Remove(offering);
                if (offering.Type == "Curse")
                {
                    Console.WriteLine(Result(0));
                    hero.AddToRelics(Dict.relicL[175]);
                }
                else if (offering.Rarity == "Basic")
                    Console.WriteLine(Result(1));
                else if (offering.Rarity == "Common" || offering.Rarity == "Special")
                {
                    Console.WriteLine(Result(2));
                    hero.HealHP(5);
                }
                else if (offering.Rarity == "Uncommon")
                {
                    Console.WriteLine(Result(3));
                    hero.HealHP(hero.MaxHP);
                }
                else
                {
                    Console.WriteLine(Result(4));
                    hero.SetMaxHP(10);
                    hero.HealHP(hero.MaxHP);
                }
            }
            else Console.WriteLine(Result(1));

        }

        public override string Result(int result)
        {
            return result switch
            {
                0 => "However, the spirits aren't happy that you offered a Curse... The card fizzles a meek black smoke.",
                1 => "Nothing happens...\r\nThe spirits seem to be ignoring you now. Disappointing...",
                2 => "The flames grow slightly brighter.\r\nThe spirits continue dancing. You feel slightly warmer from their presence...",
                3 => "The flames erupt, growing significantly stronger!\r\nThe spirits dance around you excitedly, filling you with a sense of warmth.",
                _ => "The flames burst, nearly knocking you off your feet, as the fire doubles in strength.\r\nThe spirits dance around you excitedly before merging into your form, filling you with warmth and strength.",
            };
        }
    }
}