using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Interfaces;
using Cards.Models;

namespace Cards.Services
{
    public class RandomShuffle : ICardShuffle
    {
        public IEnumerable<Card> Shuffle(IEnumerable<Card> cards)
        {
            Random r = new Random();
            return cards.OrderBy(x => r.Next());
        }
    }
}