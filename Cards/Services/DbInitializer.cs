using System.Linq;
using Cards.Models;

namespace Cards.Services
{
    public class DbInitializer
    {
        public static void Initialize(DeckContext context)
        {
            if (context.Database.EnsureCreated())
            {
                var deck = new Deck();
                deck.Name = "Demo deck";

                context.Decks.Add(deck);

                var cards = CardBuilder.CreateStandardDeck().ToArray();

                for (int i = 0; i < cards.Length; i++)
                {
                    deck.DeckCard.Add(new DeckCard()
                    {
                        Card = cards[i],
                        Deck = deck,
                        Order = i
                    });
                }

                context.SaveChanges();
            }
        }
    }
}