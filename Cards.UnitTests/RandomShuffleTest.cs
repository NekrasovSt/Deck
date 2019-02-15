using System.Linq;
using Cards.Interfaces;
using Cards.Services;
using NUnit.Framework;

namespace Cards.UnitTests
{
    [TestFixture]
    public class RandomShuffleTest
    {
        private ICardShuffle _cardShuffle;

        [SetUp]
        public void Init()
        {
            _cardShuffle = new RandomShuffle();
        }

        [Test]
        public void Shuffle()
        {
            var expected = CardBuilder.CreateStandardDeck().ToArray();

            var actual = _cardShuffle.Shuffle(expected).ToArray();

            Assert.That(actual, Is.Not.EqualTo(expected));
        }
    }
}