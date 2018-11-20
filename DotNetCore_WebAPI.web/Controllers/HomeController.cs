using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore_WebAPI.web.Models;
using DotNetCore_WebAPI.entities;
using System.Net.Http;
using Microsoft.Extensions.Options;
using DotNetCore_WebAPI.business.Interface;

namespace DotNetCore_WebAPI.web.Controllers
{
    public class HomeController : Controller
    {

        //initialisation du manager
        private readonly IRechercheTextManager _rechercheTextManager;
        private readonly ITokenManager _tokenManager;
        public HomeController(IRechercheTextManager rechercheTextManager, ITokenManager tokenManager)
        {
            _rechercheTextManager = rechercheTextManager;
            _tokenManager = tokenManager;
        }

        public async Task<IActionResult> Index(HomeViewModel vm)
        {
            
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Rechercher(HomeViewModel vm)
        {
            //initialisation de notre objet de stockage
            TaskResult<PresenceTexte> result = new TaskResult<PresenceTexte>();

            //vérification des données d'entrée 
            if (!ModelState.IsValid)
            {
                result.Authorize = false;
                result.IsSuccess = false;
                result.ReturnMessage = "erreur sur le modèle de donnée en POST";
                return View("Index", vm);
            }

            //initialisation de l'objet à transférer
            PresenceTexte presenceTexte = new PresenceTexte
            {
                Bdd = vm.Base,
                Text = vm.Text
            };
            //appel du ws
            result = await _rechercheTextManager.GetText(presenceTexte, vm.SecurityToken);

            vm.resultPresenceTexte = result;

            //retourne les data à la vue
            return View("Index", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<JsonResult> Token(string username, string password)
        {
            TaskResult<UserToken> result = new TaskResult<UserToken>();
            UserToken userToken = new UserToken
            {
                username = username,
                password = password
            };
            result = await _tokenManager.GetToken(userToken);

            return Json(new { result });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
