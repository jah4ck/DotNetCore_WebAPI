using DotNetCore_WebAPI.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<List<PresenceTexte>> GetUsers()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "User/GetAllUsers"));
            return await GetAsync<List<PresenceTexte>>(requestUrl);
        }

        public async Task<TaskResult<PresenceTexte>> SaveUser(PresenceTexte model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "values/Rechercher"));
            return await PostAsync<PresenceTexte>(requestUrl, model);
        }
    }
}
