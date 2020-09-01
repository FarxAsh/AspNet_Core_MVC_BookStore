using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;

namespace WebApplication6.Models
{
    public enum PaymentType
    {
        cardOnline, cashCourier, cardCourier
    }

    public class Order
    {
        [Key]
        [BindNever]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        [StringLength(50,ErrorMessage = "The max string length is 50 characters")]
        [Required(ErrorMessage = "This field should not be empty")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [StringLength(50,ErrorMessage = "The max string length is 50 characters")]
        [Required(ErrorMessage = "This field should not be empty")]
        public string LastName { get; set; }

        public int TotalPrice { get; set; }
        public string Byer { get; set; }
        [Required(ErrorMessage = "This field should not be empty")] 
        public string Adress { get; set; }
        [Required(ErrorMessage = "This field should not be empty")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Invalid phone number")]
        public string ContactPhone { get; set;}
        [Display(Name = "Payment Type")]
        public PaymentType PaymentType { get; set; }
        public DateTime Date { get; set; }
        public DateTime orderDateTime { get; set; }
        public ICollection<OrderDetailes> OrderDetailes { get; set; }
    }
}
