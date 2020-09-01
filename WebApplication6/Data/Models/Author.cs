using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Models;

namespace WebApplication6.Models
{
    public class Author
    {
        public int ID { get; set; }
        public Genre? Genre { get; set; }
        [Column("Firt Name")]
        [Required(ErrorMessage = "This field shoudnt be empty")]
        [StringLength(50,MinimumLength =2,ErrorMessage = "The string length must be between 2 and 50 characters")]
        public string FirstName { get; set; }
        [Column("Last Name")]
        [Required(ErrorMessage = "This field shoudnt be empty")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 2 and 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field shoudnt be empty")]
      
        public string Image { get; set; }
        [Required(ErrorMessage = "This field shoudnt be empty")]
        [MinLength(200,ErrorMessage = "The minimum string length is 200 characters")]
        public string Biography { get; set; }
        [MaxLength(140,ErrorMessage = "The max string length is 140 characters")]
        [Required(ErrorMessage = "This field shoudnt be empty")]
        public string ShortBiography { get; set; }
        public ICollection<Book> Book { get; set; }
    }
}
