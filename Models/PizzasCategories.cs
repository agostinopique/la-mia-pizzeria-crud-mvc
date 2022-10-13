namespace la_mia_pizzeria_static.Models
{
    public class PizzasCategories
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<int> SelectedIngredients { get; set; }
        public PizzasCategories()
        {
            //Pizza = new Pizza();
            Ingredients = new List<Ingredient>();
            SelectedIngredients = new List<int>();
        }
    }
}
