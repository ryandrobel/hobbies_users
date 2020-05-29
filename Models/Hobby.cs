using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exam2.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }
        
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        
        [Required]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        

        public List<Enthusiast> Enthusiasts { get; set; }
        public int UserId { get; set; }
        public User Creator { get; set; }

    }
}