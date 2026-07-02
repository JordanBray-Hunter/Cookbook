

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.Entities;


public enum CookbookRole
{
    VIEWER,
    EDITOR,
    OWNER


}

[PrimaryKey(nameof(CookbookId),nameof(UserId))]
public class CookbookMember
{

    [ForeignKey(nameof(Cookbook))]
    public required int CookbookId {get; set;}

    public Cookbook cookbook {get; set;} = null!; 

    [ForeignKey(nameof(User))]
    public required int UserId {get; set;}

    public User user {get; set;} = null!;

    public required CookbookRole Role {get; set;}

    public DateTimeOffset DateAdded {get; set;}


}