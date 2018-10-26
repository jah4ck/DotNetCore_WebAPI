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

        /*
         * créer les dll business et repo
         * ajouter les dépendances
         * ajouter dapper ou entity (dapper = bcp mieux :p ) sur repo et web
         * ajouter les autres nugets comme bootstrap, jquery, blablabla... 
         * créer la classe BaseRepository qui permet de récup les donnée de connexion et exécuter les requête 
         * le tout de façon async pour avoir des meilleurs perf (ce qu'on ne fait pas ici ... )
         * ajouter la connexion à sql dans le appsetting (spécific à core 2.0)
         * créer la class TaskResult comme entitie qui stock les différent retour d'action
         * 
         * remplire les classes business repo (sans oublier les interfaces)
         * cheminement : vue=>model=>controller=>manager=>repository=>manager=>controller=>model=>vue (le tout en passant par les interfaces
         * 
         * Penser a faire le maping des interface (uniquement pour core 2.0 )  dans le startup.cs sinon marche po
         * 
         * Voilà tu peux fauire un test de charge à 5 000 000 requête seconde, tu pete même pas de timeout (enfin peut être que la bdd crash mais le site lui tiendra :) )
         * 
         * pour l'asp.net framework c'est pareil juste moins performant, et tu risque de faire péter iis à 5 000 000 req / seconde
         * 
         * */


        //initialisation du manager
        private readonly IRechercheTextManager _rechercheTextManager;
        public HomeController(IRechercheTextManager rechercheTextManager)
        {
            _rechercheTextManager = rechercheTextManager;
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
            result = await _rechercheTextManager.GetText(presenceTexte);

            vm.resultPresenceTexte = result;

            //retourne les data à la vue
            return View("Index", vm);
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
