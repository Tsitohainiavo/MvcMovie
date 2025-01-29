using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // Affiche le formulaire de connexion
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // Traite la soumission du formulaire de connexion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Tentative de connexion
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, // Nom d'utilisateur ou email
                    model.Password, // Mot de passe
                    model.RememberMe, // Se souvenir de moi
                    lockoutOnFailure: false); // Verrouiller en cas d'échec

                if (result.Succeeded)
                {
                    // Redirection vers l'URL de retour ou la page d'accueil
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                // En cas d'échec de connexion
                ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
            }

            // Si le modèle n'est pas valide, réafficher le formulaire avec les erreurs
            return View(model);
        }

        // Déconnexion de l'utilisateur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}