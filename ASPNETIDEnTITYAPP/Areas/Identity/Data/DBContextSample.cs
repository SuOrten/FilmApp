using ASPNETIDEnTITYAPP.Areas.Identity.Data;
using ASPNETIDEnTITYAPP.Models; // Import your models
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETIDEnTITYAPP.Areas.Identity.Data;

public class DBContextSample : IdentityDbContext<SampleUser, IdentityRole<int>, int>
{
    public DBContextSample(DbContextOptions<DBContextSample> options)
        : base(options)
    {
    }

    // Add DbSet for Genres and UserGenres
    public DbSet<Genre> Genres { get; set; }
    public DbSet<UserGenre> UserGenres { get; set; }

    public DbSet<MovieList> MovieLists { get; set; }
    public DbSet<MovieInList> MovieInLists { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model if needed
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        // Seed data for Genres (optional)
        builder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Comedy" },
            new Genre { Id = 3, Name = "Drama" },
            new Genre { Id = 4, Name = "Fantasy" },
            new Genre { Id = 5, Name = "Horror" },
            new Genre { Id = 6, Name = "Science Fiction" },
            new Genre { Id = 7, Name = "Thriller" },
            new Genre { Id = 8, Name = "Romance" }
        );
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SampleUser>
{
    public void Configure(EntityTypeBuilder<SampleUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
    }
}
