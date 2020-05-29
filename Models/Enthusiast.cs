using exam2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Enthusiast
{
    [Key]
    public int EnthusiastId { get; set; }
    
    public int UserId { get; set; }
    public int HobbyId { get; set; }
    public User User { get; set; }
    public Hobby Hobbies { get; set; }
}