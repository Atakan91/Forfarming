using System.ComponentModel.DataAnnotations;

namespace Forfarming.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set;}
        public string Password { get; set;}
        public string Name { get; set;}
        public string SurName { get; set;}
        public DateTime BirthDate { get; set;}
        public string Email { get; set;}

    }
    
}
