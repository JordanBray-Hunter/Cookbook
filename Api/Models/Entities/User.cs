


using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace Api.Models.Entities;




public class User
{
    [Key]
    public int Id {get; set;}

    public required string name {get; set;}

    public required string email {get; set;}
    
    public required string HashedPassword {get; set;}





}