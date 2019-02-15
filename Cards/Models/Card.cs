namespace Cards.Models
{
    public class Card
    {
        public Card()
        {
        }

        public Card(Suit suit, int number, CardType cardType)
        {
            CardType = cardType;
            Suit = suit;
            Number = number;
        }

        public int Id { get; set; }
        public CardType CardType { get; set; }
        public Suit Suit { get; set; }
        public int Number { get; set; }

        private string Symbol
        {
            get
            {
                switch (Suit)
                {
                    case Suit.Clubs:
                        return "♣";
                    case Suit.Hearts:
                        return "♥";
                    case Suit.Spades:
                        return "♠";
                    case Suit.Diamonds:
                        return "♦";
                    default:
                        return "";
                }
            }
        }

        public override string ToString()
        {
            if (CardType == CardType.None)
                return $"{Number} {Symbol}";

            return $"{CardType} {Symbol}";
        }
    }
}