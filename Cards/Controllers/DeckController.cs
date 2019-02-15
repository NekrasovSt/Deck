using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Cards.Interfaces;
using Cards.Models;
using Cards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly DeckContext _context;
        private readonly ICardShuffle _cardShuffle;

        public DeckController(DeckContext context, ICardShuffle cardShuffle)
        {
            _context = context;
            _cardShuffle = cardShuffle;
        }

        /// <summary>
        /// Список колод
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Deck>), 200)]
        public IActionResult Get()
        {
            return Ok(_context.Decks);
        }

        /// <summary>
        /// Получить колоду карт по ид 
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound("The deck is not found");
            }

            return Ok(deck);
        }

        /// <summary>
        /// Получить колоду карт по названию
        /// </summary>
        /// <param name="name">Название колоды карт</param>
        [HttpGet("getByName/{name}")]
        public IActionResult GetByName(string name)
        {
            var deck = _context.Decks.Select(i => new {i.Id, i.Name}).FirstOrDefault(i => i.Name == name);
            if (deck == null)
            {
                return NotFound("The deck is not found");
            }

            return Ok(deck);
        }

        /// <summary>
        /// Получить карты из колоды
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpGet("{id}/cards")]
        [ProducesResponseType(typeof(IEnumerable<Card>), 200)]
        public IActionResult GetCards(int id)
        {
            if (!_context.Decks.Any(i => i.Id == id))
            {
                return NotFound("The deck is not found");
            }

            var cards = _context.DeckCards
                .Where(i => i.DeckId == id)
                .OrderBy(i => i.Order)
                .Select(i => i.Card);
            return Ok(cards);
        }

        /// <summary>
        /// Получить карты из колоды в человекочитаемом виде
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpGet("{id}/HumanizeCards")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public IActionResult GetHumanizeCards(int id)
        {
            if (!_context.Decks.Any(i => i.Id == id))
            {
                return NotFound("The deck is not found");
            }

            var cards = _context.DeckCards
                .Where(i => i.DeckId == id)
                .OrderBy(i => i.Order)
                .Select(i => i.Card)
                .ToList()
                .Select(i => i.ToString());
            return Ok(cards);
        }

        /// <summary>
        /// Добавить новую колоду
        /// </summary>
        /// <param name="deck">Колода</param>
        [HttpPost]
        [ProducesResponseType(typeof(Deck), 201)]
        [ProducesResponseType(400)]
        public IActionResult Post(Deck deck)
        {
            if (!TryValidateModel(deck))
            {
                return BadRequest(ModelState);
            }
            
            
            _context.Decks.Add(deck);

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

            _context.SaveChanges();
            return Created($"api/deck/{deck.Id}", new {deck.Id, deck.Name});
        }

        /// <summary>
        /// Изменение колоды
        /// </summary>
        /// <param name="id">ИД</param>
        /// <param name="changedDeck">Измененная колода</param>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(typeof(Deck), 200)]
        public async Task<IActionResult> Put(int id, Deck changedDeck)
        {
            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound("The deck is not found");
            }
            if (!TryValidateModel(deck))
            {
                return BadRequest(ModelState);
            }
            deck.Name = changedDeck.Name;
            _context.SaveChanges();
            return Ok(deck);
        }

        /// <summary>
        /// Перетосовать колоду
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpPut("{id}/shuffle")]
        public async Task<IActionResult> Shuffle(int id)
        {
            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound("The deck is not found");
            }

            var cards = _context.DeckCards.Include(i => i.Card).Where(i => i.DeckId == id).ToList();
            _context.DeckCards.RemoveRange(cards);
            var newDeck = _cardShuffle.Shuffle(cards.Select(i => i.Card)).ToList();

            _context.DeckCards.AddRange(newDeck.Select((card, i) => new DeckCard()
            {
                Card = card,
                Deck = deck,
                Order = i,
            }));

            _context.SaveChanges();
            return Ok(new {deck.Id, deck.Name});
        }

        /// <summary>
        /// Удалить колоду
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete(int id)
        {
            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound("The deck is not found");
            }

            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}