using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz_August6_KyleElyk.Models
{
    public class Topping
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }

        public Topping()
        {
            Pizzas = new HashSet<Pizza>();
        }
    }
}