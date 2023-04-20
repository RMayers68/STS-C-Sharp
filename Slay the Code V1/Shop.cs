using static Global.Functions;
namespace STV
{
    public class Shop
    {
        private static readonly Random ShopRNG = new();
        public Shop() { }

        public static void VisitShop(Hero hero)
        {
            if (hero.HasRelic("Ticket"))
                hero.HealHP(15);
            if (hero.HasRelic("Smiling"))
                hero.RemoveCost = 50;
            int shopChoice = -1;
            List<Card> shopCards = new();
            List<Potion> shopPotions = new();
            for (int i = 0; i < 7; i++)
            {
                Card shopCard = new();
                while (shopCard.Type != "Attack" && i < 2)
                    shopCard = Card.RandomCards(hero.Name, 1)[0];
                while (shopCard.Type != "Skill" && i >= 2 && i < 4)
                    shopCard = Card.RandomCards(hero.Name, 1)[0];
                while (shopCard.Type != "Power" && i == 4)
                    shopCard = Card.RandomCards(hero.Name, 1)[0];
                while (shopCard.Rarity != "Uncommon" && i == 5)
                    shopCard = Card.RandomCards("Colorless", 1)[0];
                while (shopCard.Rarity != "Rare" && i == 6)
                    shopCard = Card.RandomCards("Colorless", 1)[0];
                shopCards.Add(shopCard);
            }
            for (int i = 0; i < 3; i++)
            {
                Potion shopPotion = new(Dict.potionL[ShopRNG.Next(Dict.potionL.Count)]);
                shopPotions.Add(shopPotion);
            }
            string removeCard = "Remove Card";
            while (shopChoice != 0)
            {
                ScreenWipe();
                Console.WriteLine($"Hello {hero.Name}! You have {hero.Gold} Gold. What would you like to purchase? Enter your option or press 0 to leave.\n");
                Console.WriteLine("\nCards:\n*************************************");
                for (int i = 1; i <= 7; i++)
                    Console.WriteLine($"{i}: {shopCards[i - 1].Name} {(shopCards[i - 1].Name == "Purchased" ? "" : "- " + shopCards[i - 1].GetGoldCost())}");
                Console.WriteLine("\nPotions:\n*************************************");
                for (int i = 8; i <= 10; i++)
                    Console.WriteLine($"{i}: {shopPotions[i - 8].Name} {(shopPotions[i - 8].Name == "Purchased" ? "" : "- " + shopPotions[i - 8].GoldCost)}");
                Console.WriteLine($"*************************************\n11: {removeCard} {(removeCard == "Remove Card" ? $"- {hero.RemoveCost}" : "")}");
                while (!Int32.TryParse(Console.ReadLine(), out shopChoice) || shopChoice < 0 || shopChoice > 11)
                    Console.WriteLine("Invalid input, enter again:");
                int newHeroGold;
                if (shopChoice != 0 && hero.FindRelic("Maw") is Relic mawBank && mawBank.IsActive)
                    mawBank.IsActive = false;
                if (shopChoice > 0 && shopChoice < 8)
                {
                    Card shopCard = shopCards[shopChoice - 1];
                    if (shopCard.Name != "Purchased")
                    {
                        newHeroGold = hero.Gold - shopCard.GetGoldCost();
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.AddToDeck(shopCard);
                            Console.WriteLine($"You have purchased {shopCard.Name}");
                            shopCards[shopChoice - 1] = new Card();
                        }
                        else Console.WriteLine("You don't have enough Gold to buy this card.");
                    }
                    else Console.WriteLine("Card has already been purchased.");
                }
                else if (shopChoice > 7 && shopChoice < 11)
                {
                    Potion shopPotion = shopPotions[shopChoice - 8];
                    if (shopPotion.Name != "Purchased")
                    {
                        if (hero.Potions.Count <= hero.PotionSlots)
                        {
                            newHeroGold = hero.Gold - shopPotion.GoldCost;
                            if (newHeroGold >= 0)
                            {
                                hero.Gold = newHeroGold;
                                hero.Potions.Add(shopPotion);
                                Console.WriteLine($"You have purchased {shopPotion.Name}");
                                shopPotions[shopChoice - 8] = new Potion();
                            }
                            else Console.WriteLine("You don't have enough Gold to buy this potion.");
                        }
                        else Console.WriteLine("You don't have any empty potions slots.");
                    }
                    else Console.WriteLine("Potion has already been purchased.");
                }
                else if (shopChoice == 11)
                {
                    if (removeCard == "Remove Card")
                    {
                        newHeroGold = hero.Gold - hero.RemoveCost;
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.Deck.Remove(Card.ChooseCard(hero.Deck, "remove from your Deck"));
                            removeCard = "Removed";
                            if (!hero.HasRelic("Smiling"))
                                hero.RemoveCost += 25;
                        }
                        else Console.WriteLine("You don't have enough Gold to remove a card.");
                    }
                    else Console.WriteLine("You have already removed a card.");
                }
                else break;
                Pause();
            }
        }
    }
}
