using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Utilisateur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utilisateur/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EMAIL,PASSWORD")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login)); // Redirige vers une page de confirmation ou autre
            }
            return View(utilisateur);
        }
        // Afficher le formulaire de connexion
        public IActionResult Login()
        {
            return View();
        }

        // Traiter la soumission du formulaire de connexion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "L'email et le mot de passe sont requis.");
                return View();
            }

            // Rechercher l'utilisateur dans la base de données
            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.EMAIL == email && u.PASSWORD == password);

            if (utilisateur == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
                return View();
            }

            // Connexion réussie : créer une session utilisateur
            HttpContext.Session.SetInt32("UserId", utilisateur.ID);
            HttpContext.Session.SetString("UserEmail", utilisateur.EMAIL);

            return RedirectToAction("Index", "Produits"); // Rediriger vers la page d'accueil
        }

        // Déconnecter l'utilisateur
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Supprimer toutes les données de session
            return RedirectToAction("Login");
        }
        public IActionResult ProtectedPage()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login"); // Rediriger vers la page de connexion si l'utilisateur n'est pas connecté
            }

            return View();
        }

    }
}