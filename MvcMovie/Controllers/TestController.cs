using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TestController> _logger;

        public TestController(ApplicationDbContext context, ILogger<TestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult TestConnection()
        {
            try
            {
                // Test si la base est accessible
                bool canConnect = _context.Database.CanConnect();

                // Essaie de créer la table si elle n'existe pas
                _context.Database.EnsureCreated();

                // Essaie d'ajouter une entrée de test
                var testEntry = new Test { Name = "Test Entry " + DateTime.Now };
                _context.Tests.Add(testEntry);
                _context.SaveChanges();

                // Récupère toutes les entrées
                var allTests = _context.Tests.ToList();

                ViewBag.ConnectionStatus = "Connexion réussie! Nombre d'entrées: " + allTests.Count;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors du test de connexion");
                ViewBag.ConnectionStatus = "Erreur de connexion: " + ex.Message;
                return View();
            }
        }

        public IActionResult Index()
        {
            try
            {
                // Récupère les informations de la base de données
                var databaseInfo = _context.Database.GetDbConnection().Database;
                ViewBag.DatabaseName = databaseInfo;

                // Récupère toutes les entrées de la table TEST
                var allTests = _context.Tests.ToList();
                return View(allTests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des données");
                ViewBag.ErrorMessage = "Erreur: " + ex.Message;
                return View(new List<Test>());
            }
        }

    }
}