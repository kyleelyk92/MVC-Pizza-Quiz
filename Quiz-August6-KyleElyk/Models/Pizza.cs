using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz_August6_KyleElyk.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public string Crust { get; set; }
        public int NumberOfToppings { get; set; }
        public virtual ICollection<Topping> Toppings { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public Pizza()
        {
            Toppings = new HashSet<Topping>();
        }
    }
}