using dotnet_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_web_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Column> Columns => Set<Column>();
    public DbSet<Board> Boards => Set<Board>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cascade delete columns when deleting board
        modelBuilder.Entity<Column>()
            .HasOne(c => c.Board)
            .WithMany(b => b.Columns)
            .HasForeignKey(c => c.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Cards needs to be able to cascade delete such that deleting an entire board
        // also deletes the cards.
        // The delete function in service layer makes sure cards won't be cascade deleted
        // when trying to delete a column.
        modelBuilder.Entity<Card>()
            .HasOne(c => c.Column)
            .WithMany(c => c.Cards)
            .HasForeignKey(c => c.ColumnId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
