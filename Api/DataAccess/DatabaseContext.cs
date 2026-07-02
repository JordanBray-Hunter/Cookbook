
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess;


public class DatabaseContext(DbContextOptions options) : DbContext(options)
{

    DbSet<User> Users { get; set; } = null!;
    DbSet<CookbookMember> CookbookMembers { get; set; } = null!;

    DbSet<Cookbook> Cookbooks {get; set;} = null!;
    DbSet<Recipe> Recipes { get; set; } = null!;

    DbSet<FavouriteRecipe> FavouriteRecipes { get; set; } = null!;
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Recipe>().Property(r => r.Ingredients)
        .HasConversion(v => string.Join("\n",v),
        v => v.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList());
        base.OnModelCreating(modelBuilder);
    }
}