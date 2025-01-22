using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ProduitsController : Controller
    {
        private static List<Produit> _produits = new List<Produit>
        {
            new Produit { Id = 1, Nom = "Produit 1", Prix = 10.0m },
            new Produit { Id = 2, Nom = "Produit 2", Prix = 20.0m }
        };

        // GET: Produits
        public IActionResult Index()
        {
            return View(_produits);
        }

        // GET: Produits/Details/5
        public IActionResult Details(int id)
        {
            var produit = _produits.FirstOrDefault(p => p.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // GET: Produits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nom,Prix")] Produit produit)
        {
            if (ModelState.IsValid)
            {
                produit.Id = _produits.Max(p => p.Id) + 1;
                _produits.Add(produit);
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // GET: Produits/Edit/5
        public IActionResult Edit(int id)
        {
            var produit = _produits.FirstOrDefault(p => p.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // POST: Produits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nom,Prix")] Produit produit)
        {
            if (id != produit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingProduit = _produits.FirstOrDefault(p => p.Id == id);
                if (existingProduit != null)
                {
                    existingProduit.Nom = produit.Nom;
                    existingProduit.Prix = produit.Prix;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        // GET: Produits/Delete/5
        public IActionResult Delete(int id)
        {
            var produit = _produits.FirstOrDefault(p => p.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var produit = _produits.FirstOrDefault(p => p.Id == id);
            if (produit != null)
            {
                _produits.Remove(produit);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}