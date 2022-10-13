using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_static.Models;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {

        public IActionResult Index()
        {
            List<Pizza> pizze;
            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizze = db.Pizze.ToList();
                //if(pizze.Count == 0)
                //{
                //    List<Pizza> pizzas = new List<Pizza> {
                //        new Pizza("Margherita", "Pizza con pomodoro, mozzarella e basilico", "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Pizza_Margherita_stu_spivack.jpg/1280px-Pizza_Margherita_stu_spivack.jpg", "5"),
                //        new Pizza("Marinara", "Pizza con pomodoro, mozzarella e basilico", "https://www.buttalapasta.it/wp-content/uploads/2021/03/pizza-marinara.jpg","4"),
                //        new Pizza("Napoli", "Pizza napoletana", "https://wips.plug.it/cips/buonissimo.org/cms/2012/07/pizza-napoli-5.jpg", "6,50"),
                //        new Pizza("Wurstel", "pizza con pomodoro mozzarella e wurtel", "https://www.lospicchiodaglio.it/img/ricette/pizzawurstel.jpg", "6"),
                //        new Pizza("Salamino piccante",  "Pizza con pomodoro, mozzarella e salamino piccante", "https://api.molinocasillo.com/wp-content/uploads/2020/07/pizza.jpg", "6"),
                //        new Pizza("Maialona", "Pizza margherita con wurstel, salsiccia, salamino picccante", "https://media-cdn.tripadvisor.com/media/photo-s/13/a4/63/f8/pizza-maialona.jpg", "7")
                //    };

                //    foreach (Pizza pizza in pizzas)
                //    {
                //        db.Add(pizza);
                //    }
                //    db.SaveChanges();
                //    pizze = db.Pizze.Include("Category").ToList();
                //}
            }
            
            return View(pizze);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza;
            using (PizzeriaContext db = new PizzeriaContext())
            {
                pizza = db.Pizze.Where(pizze => pizze.Id == id).Include("Category").FirstOrDefault();
            }
                

            return View(pizza);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PizzasCategories pizzasCategories = new PizzasCategories();
            pizzasCategories.Pizza = new Pizza();
            pizzasCategories.Categories = new PizzeriaContext().Category.ToList();
            pizzasCategories.Ingredients = new PizzeriaContext().Ingredients.ToList();
            return View(pizzasCategories);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Store(PizzasCategories pizzaData)
        {
            PizzeriaContext db = new PizzeriaContext();
 
            if (!ModelState.IsValid)
            {
                pizzaData.Ingredients = db.Ingredients.ToList();
                pizzaData.Categories = db.Category.ToList();
                return View("Create", pizzaData);
            }

            foreach(int IngId in pizzaData.SelectedIngredients)
            {
                pizzaData.Pizza.Ingredients.Add(db.Ingredients.Find(IngId));
            }

            db.Add(pizzaData.Pizza);

            db.SaveChanges();
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            PizzeriaContext db = new PizzeriaContext();

            Pizza pizzaToUpdate = db.Pizze.Include("Ingredients").Where(pizze => pizze.Id == id).FirstOrDefault();
            if(pizzaToUpdate == null)
            {
                return NotFound();
            } 

            PizzasCategories pizzasCategories = new PizzasCategories();
            pizzasCategories.Pizza = pizzaToUpdate;
            pizzasCategories.Categories = db.Category.ToList();
            pizzasCategories.Ingredients = db.Ingredients.ToList();

            foreach(Ingredient Ing in pizzaToUpdate.Ingredients)
            {
                pizzasCategories.SelectedIngredients.Add(Ing.Id);
            }

            return View("EditPizza", pizzasCategories);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzasCategories pizzaData)
        {
            PizzeriaContext db = new PizzeriaContext();
            if (!ModelState.IsValid)
            {
                pizzaData.Categories = db.Category.ToList();
                pizzaData.Ingredients = db.Ingredients.ToList();
                return View("EditPizza", pizzaData);
            }

            

            Pizza pizzaDaModificare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();

            if(pizzaDaModificare != null)
            {
                pizzaDaModificare.Name = pizzaData.Pizza.Name;
                pizzaDaModificare.Description = pizzaData.Pizza.Description;
                pizzaDaModificare.Price = pizzaData.Pizza.Price;
                pizzaDaModificare.Picture = pizzaData.Pizza.Picture;
                pizzaDaModificare.CategoryId = pizzaData.Pizza.CategoryId;
                db.SaveChanges();

            }
            else
            {
                return NotFound();
            }

            return RedirectToAction("Details", pizzaDaModificare);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            PizzeriaContext db = new PizzeriaContext();

            Pizza pizzaDaEliminare = db.Pizze.Where(pizze => pizze.Id == id).FirstOrDefault();

            db.Remove(pizzaDaEliminare);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
