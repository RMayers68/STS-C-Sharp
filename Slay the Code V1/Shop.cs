using static Global.Functions;
namespace STV
{
    public class Shop
    {

        public static void VisitShop(Hero hero)
        {
            if (hero.HasRelic("Ticket"))
                hero.HealHP(15);
            if (hero.HasRelic("Smiling"))
                hero.RemoveCost = 50;
            int shopChoice = -1;
            List<Card> shopCards = new();
            List<Potion> shopPotions = new();
            List<Relic> shopRelics = new();
            for (int i = 0; i < 7; i++)
            {
                Card shopCard = new Purchased();
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
                Potion shopPotion = new(Potion.RandomPotion(hero));
                shopPotions.Add(shopPotion);
            }
            for (int i = 0; i < 2; i++)
            {
                Relic shopRelic = Relic.RandomRelic(hero.Name);
                shopRelics.Add(shopRelic);
            }
            shopRelics.Add(Relic.ShopRelic(hero.Name));
            string removeCard = "Remove Card";
            while (shopChoice != 0)
            {
                int discount = 1;
                if (hero.HasRelic("Member"))
                    discount = 5;
                else if (hero.HasRelic("Courier"))
                    discount = 2;
                ScreenWipe();
                Console.WriteLine($"Hello {hero.Name}! You have {hero.Gold} Gold. What would you like to purchase? Enter your option or press 0 to leave.\n");
                Console.WriteLine("\nCards:\n*************************************");
                for (int i = 1; i <= 7; i++)
                    Console.WriteLine($"{i}: {shopCards[i - 1].Name} {(shopCards[i - 1].Name == "Purchased" ? "" : "- " + $"{( discount > 1 ? $"{shopCards[i - 1].GoldCost / discount}" : $"{shopCards[i - 1].GoldCost}")}")}");
                Console.WriteLine("\nPotions:\n*************************************");
                for (int i = 8; i <= 10; i++)
                    Console.WriteLine($"{i}: {shopPotions[i - 8].Name} {(shopPotions[i - 8].Name == "Purchased" ? "" : "- " + $"{(discount > 1 ? $"{shopPotions[i - 8].GoldCost / discount}" : $"{shopPotions[i - 8].GoldCost}")}")}");
                Console.WriteLine("\nRelics:\n*************************************");
                for (int i = 11; i <= 13; i++)
                    Console.WriteLine($"{i}: {shopRelics[i - 11].Name} {(shopRelics[i - 11].Name == "Purchased" ? "" : "- " + $"{(discount > 1 ? $"{shopRelics[i - 11].GoldCost / discount}" : $"{shopRelics[i - 11].GoldCost}")}")}");
                Console.WriteLine($"*************************************\n14: {removeCard} {(removeCard == "Remove Card" ? $"- {hero.RemoveCost}" : "")}");
                while (!Int32.TryParse(Console.ReadLine(), out shopChoice) || shopChoice < 0 || shopChoice > 14)
                    Console.WriteLine("Invalid input, enter again:");
                int newHeroGold;
                if (shopChoice != 0 && hero.FindRelic("Maw") is Relic mawBank && mawBank.IsActive)
                    mawBank.IsActive = false;
                if (shopChoice > 0 && shopChoice < 8)
                {
                    Card shopCard = shopCards[shopChoice - 1];
                    if (shopCard.Name != "Purchased")
                    {
                        newHeroGold = hero.Gold - shopCard.GoldCost / discount;
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.AddToDeck(shopCard);
                            Console.WriteLine($"You have purchased {shopCard.Name}");
                            shopCards[shopChoice - 1] = hero.HasRelic("Courier") ? Card.RandomCards(hero.Name, 1)[0] : new Purchased();
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
                            newHeroGold = hero.Gold - shopPotion.GoldCost / discount;
                            if (newHeroGold >= 0)
                            {
                                hero.Gold = newHeroGold;
                                hero.Potions.Add(shopPotion);
                                Console.WriteLine($"You have purchased {shopPotion.Name}");
                                shopPotions[shopChoice - 8] = new Potion(hero.HasRelic("Courier") ? Potion.RandomPotion(hero) : new());
                            }
                            else Console.WriteLine("You don't have enough Gold to buy this potion.");
                        }
                        else Console.WriteLine("You don't have any empty potions slots.");
                    }
                    else Console.WriteLine("Potion has already been purchased.");
                }
                else if (shopChoice > 10 && shopChoice < 14)
                {
                    Relic shopRelic = shopRelics[shopChoice - 11];
                    if (shopRelic.Name != "Purchased")
                    {
                        newHeroGold = hero.Gold - shopRelic.GoldCost / discount;
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.AddToRelics(shopRelic);
                            Console.WriteLine($"You have purchased {shopRelic.Name}");
                            shopRelics[shopChoice - 11] = new Relic(hero.HasRelic("Courier") ? shopChoice < 13 ? Relic.RandomRelic(hero.Name) : Relic.ShopRelic(hero.Name) : new());
                        }
                        else Console.WriteLine("You don't have enough Gold to buy this relic.");
                    }
                    else Console.WriteLine("Relic has already been purchased.");
                }
                else if (shopChoice == 14)
                {
                    if (removeCard == "Remove Card")
                    {
                        newHeroGold = hero.Gold - hero.RemoveCost;
                        if (newHeroGold >= 0)
                        {
                            hero.Gold = newHeroGold;
                            hero.RemoveFromDeck(Card.PickCard(hero.Deck, "remove"));
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