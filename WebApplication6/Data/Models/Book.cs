using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication6.Data.Models;

namespace WebApplication6.Models
{
    public enum Genre
    {
        Fiction, Fantasy, Buisness, History, Horror, Science
    }
    public enum BookCondition
    {
        Used, New
    }
    public class Book
    {

        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="This field should not be empty")]
        [StringLength(50,MinimumLength =2, ErrorMessage = "The string length must be between 2 and 50 characters")]
        public string Title { get; set; }
        [Column("Year of Publish")]
        [Required(ErrorMessage = "This field should not be empty")]
        [RegularExpression(@"[0-9]{2,4}", ErrorMessage = "Invalid year entered")]
        public int Year { get; set; }
        [Required(ErrorMessage = "This field should not be empty")]
        [RegularExpression(@"[0-9]{2,4}", ErrorMessage = "Invalid number of pages entered")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "This field shoudnt be empty")]
       
        public string Image { get; set; }
        [Required(ErrorMessage = "This field should not be empty")]
        [MaxLength(1000, ErrorMessage = "The max string length is 1000 characters")]
        public string Description { get; set; }
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "This field should not be empty")]
        [RegularExpression(@"[0-9]{1,5}", ErrorMessage = "Invalid price entered")]
        public int Price { get; set; }
        public bool IsSecondHand { get; set; }
        [Required(ErrorMessage = "This field should not be empty")]
        public int AuthorID { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Comment> Comment { get; set; }

    }
}
