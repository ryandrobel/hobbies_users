using System.ComponentModel.DataAnnotations;


namespace exam2.Models
{
    public class LoginUser
    {
        [Key]
        public int LoginUserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}