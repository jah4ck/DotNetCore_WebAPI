using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore_WebAPI.entities
{
    public class PresenceTexte
    {
        //il faut les même nom qu'en bdd pour faire un mapping automatique via dapper 
        //sinon on peut faire un mapping manuel mais pfff flem!!!

        public string ProcName { get; set; }
        public string Extrait { get; set; }
        public string Bdd { get; set; }
        public string Text { get; set; }
    }
}
