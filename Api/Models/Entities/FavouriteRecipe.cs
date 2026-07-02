
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.Entities;



[PrimaryKey(nameof(UserId),nameof(RecipeId))]
public class FavouriteRecipe
{
    [ForeignKey(nameof(User))]
    public required int UserId {get; set;}
    public User user {get; set;} = null!;
    
    [ForeignKey(nameof(Recipe))]
    public required int RecipeId {get; set;}

    public Recipe recipe {get; set;} = null!;

    public DateTimeOffset DateFavorited {get; set;} 

}