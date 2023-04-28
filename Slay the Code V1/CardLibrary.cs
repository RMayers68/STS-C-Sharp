namespace STV
{
    public class CardLibrary
    {
        public string CardName { get; set; }
        public string CardType { get; set; }
        public string EnergyCost { get; set; }
        public string Description { get; set; }

        public CardLibrary(Card card) 
        {
            CardName = card.Name;
            CardType = card.Type;
            if (card.EnergyCost >= 0)
                EnergyCost += card.EnergyCost;
            else if (card.EnergyCost == -1)
                EnergyCost = "X";
            Description = card.GetDescription();
        }

        public static List<CardLibrary> ViewLibrary(bool Upgraded = false)
        {
            List<CardLibrary> list = new();
            foreach (Card c in Dict.cardL.Values)
            {
                Card card = new Anger();
                if (Upgraded)
                    card.UpgradeCard();
                list.Add(new(card));               
            }                
            return list;
        } 
    }
}