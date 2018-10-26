using DotNetCore_WebAPI.entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCore_WebAPI.repository.Interface
{
    public interface ITokenRepository
    {
        Task<TaskResult<UserToken>> GetToken(UserToken userToken);
    }
}
