using Microsoft.EntityFrameworkCore;

namespace Cards.Models
{
    public class DeckContext : DbContext
    {
        public DeckContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<DeckCard> DeckCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeckCard>()
                .HasKey(t => new {t.DeckId, t.CardId});
            
            base.OnModelCreating(modelBuilder);
        }
    }
}