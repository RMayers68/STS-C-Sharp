namespace STV
{
    public class Neow : Event
    {
        public Neow()
        {
            Name = "Neow";
            StartOfEncounter = $"You see a big, whale-like creature with light blue skin and six bright yellow eyes. She bellows: \"Hello... Again.. I am... Neow.\nShe then offers a choice of Blessings for your ascent up the Spire";
            Options = new();
        }

        public override void EventAction(Hero hero)
        {
            List<string> advDisadv = new();
            List<string> firstTwoBlessings = new() 
            { 
                $"Max HP +{hero.MaxHP/10}", 
                $"Enemies in the next three combat will have one health.",
                "Remove a card",
                "Transform a card",
                "Upgrade a card",
                "Choose a card to obtain",
                "Choose an uncommon colorless card to obtain",
                "Obtain a random rare card",
                "Obtain a random common relic",
                "Receive 100 gold",
                "Obtain 3 random potions",
            };
            List<string> advantages = new()
            {
                $"Max HP +{hero.MaxHP/10}",
                "Remove 2 cards",
                "Transform 2 cards",
                "Gain 250 Gold",
                "Choose a rare card to obtain",
                "Choose an rare colorless card to obtain",
                "Obtain a random rare relic",
            };
            List<string> disadvantages = new()
            {
                $"Max HP -{hero.MaxHP/10}",
                $"Take 20 damage",
                "Obtain a Curse",
                "Lose all Gold",
            };
            for (int i = 1; i < 3; i++)
            {
                int blessingSelected = EventRNG.Next(firstTwoBlessings.Count);
                Options.Add($"[{i}] {firstTwoBlessings[blessingSelected]}");
                firstTwoBlessings.RemoveAt(blessingSelected);
            }
            int advantageSelected = EventRNG.Next(advantages.Count);
            if (advantageSelected == 0)
                disadvantages.RemoveAt(0);
            else if (advantageSelected == 3)
                disadvantages.RemoveAt(3);
            int disadvantageSelected = EventRNG.Next(disadvantages.Count);
            Options.Add($"[3] {advantages[advantageSelected]}, {disadvantages[disadvantageSelected]}");
            advDisadv.Add(advantages[advantageSelected]);
            advDisadv.Add(disadvantages[disadvantageSelected]);
            Options.Add($"[4] Replace your starting Relic with a random Boss Relic");
            List<string> choices = new() { "1", "2", "3", "4" };
            Console.WriteLine($"{StartOfEncounter}\n{Options[0]}\n{Options[1]}\n{Options[2]}\n{Options[3]}");
            string playerChoice = "";
            while (choices.Any(x => x == playerChoice))
            {
                playerChoice = Console.ReadLine().ToUpper();
            }
            if (playerChoice == "1" || playerChoice == "2")
            {
                switch (Options[Convert.ToInt32(playerChoice)])
                {
                    default:
                        hero.SetMaxHP(hero.MaxHP / 10);
                        break;
                    case "Enemies in the next three combat will have one health.":
                        hero.AddToRelics(Dict.relicL[171]);
                        break;
                    case "Remove a card":
                        hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
                        break;
                    case "Transform a card":
                        Card.PickCard(hero.Deck, "transform").TransformCard(hero);
                        break;
                    case "Upgrade a card":
                        Card.PickCard(hero.Deck, "transform").UpgradeCard();
                        break;
                    case "Choose a card to obtain":
                        hero.AddToDeck(Card.PickCard(Card.RandomCards(hero.Name,3), "obtain"));
                        break;
                    case "Choose an uncommon colorless card to obtain":
                        hero.AddToDeck(Card.PickCard(Card.RandomCards("Colorless", 3,"Uncommon"), "obtain"));
                        break;
                    case "Obtain a random rare card":
                        hero.AddToDeck(Card.RandomCards(hero.Name, 1, "Rare")[0]);
                        break;
                    case "Obtain a random common relic":
                        hero.AddToRelics(Relic.RandomRelic(hero.Name));
                        break;
                    case "Receive 100 gold":
                        hero.GoldChange(100);
                        break;
                    case "Obtain 3 random potions":
                        for (int i = 0; i < 3; i++)
                            hero.AddToPotions(Potion.RandomPotion(hero));
                        break;
                }
            }
            else if (playerChoice == "3")
            {
                switch (advDisadv[0])
                {
                    default:
                        hero.SetMaxHP(hero.MaxHP / 10 * 2);
                        break;
                    case "Remove 2 cards":
                        for (int i = 0; i < 2; i++)
                            hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
                        break;
                    case "Transform 2 cards":
                        for (int i = 0; i < 2; i++)
                            Card.PickCard(hero.Deck, "transform").TransformCard(hero);
                        break;
                    case "Gain 250 Gold":
                        hero.GoldChange(250);
                        break;
                    case "Choose a rare card to obtain":
                        hero.AddToDeck(Card.PickCard(Card.RandomCards(hero.Name, 3,"Rare"), "obtain"));
                        break;
                    case "Choose an rare colorless card to obtain":
                        hero.AddToDeck(Card.PickCard(Card.RandomCards("Colorless", 3, "Rare"), "obtain"));
                        break;
                    case "Obtain a random rare relic":
                        hero.AddToRelics(Relic.RandomRelic(hero.Name));
                        break;
                }
                switch (advDisadv[1])
                {
                    default:
                        hero.SetMaxHP(hero.MaxHP / 10 * -1);
                        break;
                    case "Obtain a Curse":
                        hero.AddToDeck(new Clumsy());
                        break;
                    case "Lose all Gold":
                        hero.GoldChange(hero.Gold * -1);
                        break;
                    case "Take 20 damage":
                        hero.NonAttackDamage(hero, 20, "Neow's Curse");
                        break;
                }
            }
            else
            {
                hero.Relics.Clear();
                hero.AddToRelics(Relic.BossRelic());
            }
        }
    }
}
