using DotNetCore_WebAPI.business.Interface;
using DotNetCore_WebAPI.entities;
using DotNetCore_WebAPI.repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.business
{
    public class TokenManager : ITokenManager
    {

        readonly ITokenRepository _tokenRepository;
        public TokenManager(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<TaskResult<UserToken>> GetToken (UserToken userToken)
        {
            TaskResult<UserToken> result = new TaskResult<UserToken>();
            result = await _tokenRepository.GetToken(userToken);

            return result;
        }
    }
}
