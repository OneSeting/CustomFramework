using LittleHourglass.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Authorization.JWT
{
    public interface IJwtService
    {
        /// <summary>
        /// 创建授权Token（Jwt Token）
        /// </summary>
        /// <returns></returns>
        JwtAuthorizationModel CreateToken(UniUser user);

        /// <summary>
        /// 停用Token
        /// </summary>
        /// <returns></returns>
        Task DeactivateTokenAsync(string token);

        /// <summary>
        /// 检查 Token 是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> IsTokenActiveAsync(string token);

        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCurrentTokenActiveAsync();

    }
}
