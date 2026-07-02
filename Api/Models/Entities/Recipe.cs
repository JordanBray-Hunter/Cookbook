
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities;


public class Recipe
{
    [Key]
    public int id {get; set;}

    public string title {get; set;} = "";

    public string steps {get; set;} = "";

    public List<string> Ingredients {get; set;} = new();

        [ForeignKey(nameof(Cookbook))]
        public required int CookbookId {get; set;}

        

}