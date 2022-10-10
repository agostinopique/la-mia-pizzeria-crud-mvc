namespace la_mia_pizzeria_static.Models
{
    public class PizzasCategories
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Ingredient>? SelectedIngredients { get; set; }
        public PizzasCategories()
        {
            //Pizza = new Pizza();
            SelectedIngredients = new List<Ingredient>();
        }
    }
}
