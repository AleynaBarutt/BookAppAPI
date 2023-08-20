using BookApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookApp.Repositories.Config
{
    public class BookConfig :IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id=1,Title="Serenad",Price=75}, 
                new Book { Id=2,Title="Körlük", Price=120}, 
                new Book { Id=3,Title="MOMO",Price=64} 
            
            );
        }
    }
}
