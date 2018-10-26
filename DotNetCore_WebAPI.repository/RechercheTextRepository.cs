using Dapper;
using DotNetCore_WebAPI.entities;
using DotNetCore_WebAPI.repository.Interface;
using DotNetCore_WebAPI.web.Models;
using DotNetCore_WebAPI.web.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.repository
{
    public class RechercheTextRepository : UrlRepository, IRechercheTextRepository
    {
        //permet de spécifier la connexion de cette classe 
        // on va donc allé chercher les info dans le fichier appsetting.json

        private readonly IOptions<MySettingsUrlModel> appSettings;
        public RechercheTextRepository(IOptions<MySettingsUrlModel> app) : base(new Uri(app.Value.WebApiBaseUrl))
        {
         
        }


        public async Task<TaskResult<PresenceTexte>> GetText(PresenceTexte model)
        {
            TaskResult<PresenceTexte> taskResult = new TaskResult<PresenceTexte>();
            try
            {
                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "values/Rechercher"));
                return await PostAsync<PresenceTexte>(requestUrl, model);
            }
            catch (Exception ex)
            {
                taskResult.Exception = ex;
                taskResult.ReturnMessage = "Erreur lors de la récupération des données";
                taskResult.IsSuccess = false;
                return taskResult;
            }
            
        }
    }
}
