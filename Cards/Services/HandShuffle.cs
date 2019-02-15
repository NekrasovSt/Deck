using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Interfaces;
using Cards.Models;

namespace Cards.Services
{
    public class HandShuffle : ICardShuffle
    {
        public IEnumerable<Card> Shuffle(IEnumerable<Card> cards)
        {
            var master = cards.ToList();


            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                int partSize = r.Next(master.Count / 2);

                int index = r.Next(0, master.Count - partSize);

                var subRange = master.Skip(index).Take(partSize).ToList();

                master.RemoveRange(index, partSize);

                master.AddRange(subRange);
            }

            return master;
        }
    }
}