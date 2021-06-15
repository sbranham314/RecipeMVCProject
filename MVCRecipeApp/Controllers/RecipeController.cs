using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MVCRecipeApp.Models;
using System.IO;

namespace MVCRecipeApp.Controllers
{
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/

        public ActionResult Index()
        {
 
            using (RecipeDBEntities recipeDB = new RecipeDBEntities())
            {
                return View(recipeDB.Recipes.ToList());
            }
            
        }

        //
        // GET: /Recipe/Details/5

        public ActionResult Details(int id)
        {

            using (RecipeDBEntities recipeDB = new RecipeDBEntities())
            {
                return View(recipeDB.Recipes.Where(x => x.recipeID == id).FirstOrDefault());
            }
            
        }

        //
        // GET: /Recipe/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Recipe/Create

        [HttpPost]
        public ActionResult Create(Recipe recipe)
        {
            try
            {
                // TODO: Add insert logic here
                string filename = Path.GetFileNameWithoutExtension(recipe.ImageFile.FileName);
                string extension = Path.GetExtension(recipe.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                recipe.recipeImagePath = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                recipe.ImageFile.SaveAs(filename);
                using (RecipeDBEntities recipeDB = new RecipeDBEntities())
                {
                    recipeDB.Recipes.Add(recipe);
                    recipeDB.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Recipe/Edit/5

        public ActionResult Edit(int id)
        {
            using (RecipeDBEntities recipeDB = new RecipeDBEntities())
            {
                return View(recipeDB.Recipes.Where(x => x.recipeID == id).FirstOrDefault());
            }
        }

        //
        // POST: /Recipe/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Recipe recipe)
        {
            try
            {
                // TODO: Add update logic here
                using (RecipeDBEntities recipeDB = new RecipeDBEntities())
                {
                    recipeDB.Entry(recipe).State = EntityState.Modified;
                    recipeDB.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Recipe/Delete/5

        public ActionResult Delete(int id)
        {
            using (RecipeDBEntities recipeDB = new RecipeDBEntities())
            {
                return View(recipeDB.Recipes.Where(x => x.recipeID == id).FirstOrDefault());
            }
        }

        //
        // POST: /Recipe/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (RecipeDBEntities recipeDB = new RecipeDBEntities())
                {
                    Recipe recipe = recipeDB.Recipes.Where(x => x.recipeID == id).FirstOrDefault();
                    recipeDB.Recipes.Remove(recipe);
                    recipeDB.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
