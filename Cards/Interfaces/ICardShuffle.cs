using System.Collections.Generic;
using Cards.Models;

namespace Cards.Interfaces
{
    public interface ICardShuffle
    {
        IEnumerable<Card> Shuffle(IEnumerable<Card> cards);
    }
}