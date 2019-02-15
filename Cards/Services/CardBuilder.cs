using System;
using System.Collections.Generic;
using Cards.Models;

namespace Cards.Services
{
    public class CardBuilder
    {
        public static IEnumerable<Card> CreateStandardDeck()
        {
            var numbers = new[] {2, 3, 4, 5, 6, 7, 8, 9, 10};
            var suits = new[] {Suit.Clubs, Suit.Hearts, Suit.Spades, Suit.Diamonds};
            var types = new[] {CardType.Jack, CardType.Queen, CardType.King, CardType.Ace};
            foreach (var suit in suits)
            {
                foreach (int number in numbers)
                {
                    yield return new Card(suit, number, CardType.None);
                }
                foreach (CardType type in types)
                {
                    yield return new Card(suit, 0, type);
                }
                
            }
        }
    }
}