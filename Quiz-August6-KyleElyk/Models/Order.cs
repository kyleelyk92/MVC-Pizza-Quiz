using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz_August6_KyleElyk.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public bool Paid { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
            Pizzas = new HashSet<Pizza>();
        }
    }
}