using System;
using System.Linq;
using Cards.Models;
using Cards.Services;
using NUnit.Framework;

namespace Cards.UnitTests
{
    [TestFixture]
    public class CardBuilderTest
    {
        [Test]
        public void CreateStandardDeck()
        {
            var deck = CardBuilder.CreateStandardDeck();

            Assert.That(deck.Count(), Is.EqualTo(52));
            Assert.That(deck.Count(i => i.Suit == Suit.Clubs), Is.EqualTo(13));
            Assert.That(deck.Count(i => i.Suit == Suit.Hearts), Is.EqualTo(13));
            Assert.That(deck.Count(i => i.Suit == Suit.Spades), Is.EqualTo(13));
            Assert.That(deck.Count(i => i.Suit == Suit.Diamonds), Is.EqualTo(13));
        }
    }
}