using DotNetCore_WebAPI.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.web.Models
{
    public class HomeViewModel
    {
        /// <summary>
        /// selection de la bdd
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// saisie du text à rechercher
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// permet de passer au front les erreur ou autre si besoin 
        /// peut êttre hérité mais flem !! ya qu'un viewmodel
        /// </summary>
        public TaskResult<PresenceTexte> resultPresenceTexte { get; set; }

        /// <summary>
        /// contient la liste des résultats
        /// </summary>
        public IList<PresenceTexte> lstTextResult { get; set; }

        public string SecurityToken { get; set; }
    }
}
