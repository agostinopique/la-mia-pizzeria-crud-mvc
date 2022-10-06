using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        
        //public List<Pizza> GeneratePizzas()
        //{
        //    List<Pizza> pizzas = new List<Pizza> {
        //                new Pizza("Margherita", "Pizza con pomodoro, mozzarella e basilico", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Pizza_Margherita_stu_spivack.jpg/1280px-Pizza_Margherita_stu_spivack.jpg", "5"),
        //                new Pizza("Marinara", "Pizza con pomodoro, mozzarella e basilico", "https://www.buttalapasta.it/wp-content/uploads/2021/03/pizza-marinara.jpg","4"),
        //                new Pizza("Napoli", "Pizza napoletana", "https://wips.plug.it/cips/buonissimo.org/cms/2012/07/pizza-napoli-5.jpg", "6,50"),
        //                new Pizza("Wurstel", "pizza con pomodoro mozzarella e wurtel", "https://www.lospicchiodaglio.it/img/ricette/pizzawurstel.jpg", "6"),
        //                new Pizza("Salamino piccante",  "Pizza con pomodoro, mozzarella e salamino piccante", "https://api.molinocasillo.com/wp-content/uploads/2020/07/pizza.jpg", "6"),
        //                new Pizza("Maialona", "Pizza margherita con wurstel, salsiccia, salamino picccante", "https://media-cdn.tripadvisor.com/media/photo-s/13/a4/63/f8/pizza-maialona.jpg", "7")
        //            };
        //    return pizzas;
        //}

        public IActionResult Index()
        {
            List<Pizza> pizze;
            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizze = db.Pizze.OrderBy(pizza => pizza.Name).ToList();
                if(pizze.Count == 0)
                {
                    List<Pizza> pizzas = new List<Pizza> {
                        new Pizza("Margherita", "Pizza con pomodoro, mozzarella e basilico", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Pizza_Margherita_stu_spivack.jpg/1280px-Pizza_Margherita_stu_spivack.jpg", "5"),
                        new Pizza("Marinara", "Pizza con pomodoro, mozzarella e basilico", "https://www.buttalapasta.it/wp-content/uploads/2021/03/pizza-marinara.jpg","4"),
                        new Pizza("Napoli", "Pizza napoletana", "https://wips.plug.it/cips/buonissimo.org/cms/2012/07/pizza-napoli-5.jpg", "6,50"),
                        new Pizza("Wurstel", "pizza con pomodoro mozzarella e wurtel", "https://www.lospicchiodaglio.it/img/ricette/pizzawurstel.jpg", "6"),
                        new Pizza("Salamino piccante",  "Pizza con pomodoro, mozzarella e salamino piccante", "https://api.molinocasillo.com/wp-content/uploads/2020/07/pizza.jpg", "6"),
                        new Pizza("Maialona", "Pizza margherita con wurstel, salsiccia, salamino picccante", "https://media-cdn.tripadvisor.com/media/photo-s/13/a4/63/f8/pizza-maialona.jpg", "7")
                    };

                    foreach (Pizza pizza in pizzas)
                    {
                        db.Add(pizza);
                    }
                    db.SaveChanges();
                    pizze = db.Pizze.OrderBy(pizza => pizza.Name).ToList();
                }
            }
            
            return View(pizze);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza;
            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizza = db.Pizze.Where(pizze => pizze.Id == id).First();
            }
                

            return View(pizza);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(Pizza pizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Add(new Pizza(pizza.Name, pizza.Description, pizza.Picture, pizza.Price));
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
    }
}
