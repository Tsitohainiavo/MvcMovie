using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProduitsController> _logger;

        public ProduitsController(ApplicationDbContext context, ILogger<ProduitsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        // GET: Produits

        public IActionResult Index()
        {
            var produits = _context.Produits.ToList(); // Assuming _context is your database context
            return View(produits);
        }


        private static List<Produit> _produits = new List<Produit>
        {
            new Produit { Id = 1, Nom = "Produit 1", Prix = 10.0m },
            new Produit { Id = 2, Nom = "Produit 2", Prix = 20.0m }
        };

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
                _context.Produits.Add(produit);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
        }

        // GET: Produits/Edit/5
        public IActionResult Edit(int id)
        {
            var produit = _context.Produits.Find(id);
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
                try
                {
                    _context.Update(produit);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(produit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produit);
        }

        private bool ProduitExists(int id)
        {
            return _context.Produits.Any(e => e.Id == id);
        }

        // GET: Produits/Delete/5
        public IActionResult Delete(int id)
        {
            var produit = _context.Produits.Find(id);
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
            var produit = _context.Produits.Find(id);
            if (produit != null)
            {
                _context.Produits.Remove(produit);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}