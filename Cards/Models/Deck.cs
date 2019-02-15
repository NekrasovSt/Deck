using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Cards.Models
{
    public class Deck
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore] public HashSet<DeckCard> DeckCard { get; set; } = new HashSet<DeckCard>();
    }
}