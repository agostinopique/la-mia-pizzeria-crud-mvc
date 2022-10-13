using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{

    [Table("Pizze")]
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public string Price { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new();

        public Pizza()
        {

        }
       public Pizza(string name, string description, string picture, string price)
        {
            int id = 0;
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Picture = picture;
            this.Price = price;
            id++;
        }
    }
}