using DotNetCore_WebAPI.entities;
using DotNetCore_WebAPI.repository.Interface;
using DotNetCore_WebAPI.web.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.repository
{
    public class TokenRepository : UrlRepository, ITokenRepository
    {
        private readonly IOptions<MySettingsUrlModel> appSettings;
        public TokenRepository(IOptions<MySettingsUrlModel> app) : base(new Uri(app.Value.WebApiBaseUrl))
        {

        }

        public async Task<TaskResult<UserToken>> GetToken(UserToken userToken)
        {
            TaskResult<UserToken> taskResult = new TaskResult<UserToken>();
            try
            {
                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "Token/Create"));
                return await PostAsync<UserToken>(requestUrl, userToken, null);
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
