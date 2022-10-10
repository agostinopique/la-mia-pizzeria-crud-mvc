using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // Collegamento con le pizze 
        public List<Pizza> Pizzas { get; set; }
    }
}
