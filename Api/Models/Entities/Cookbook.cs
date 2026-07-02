

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities;



public class Cookbook
{
    [Key]
    public int Id {get; set;}

    public string name {get; set;} ="";

    public DateTimeOffset DateCreated {get; set;}

    public string CoverImagePath {get; set;} ="";

    public bool IsPublic {get; set;}

   
    public List<CookbookMember> members {get; set;} = new();




}