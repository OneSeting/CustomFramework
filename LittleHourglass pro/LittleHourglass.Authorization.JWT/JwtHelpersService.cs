using LittleHourglass.DataBase.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using IDistributedCache = Microsoft.Extensions.Caching.Distributed.IDistributedCache;
using IHttpContextAccessor = Microsoft.AspNetCore.Http.IHttpContextAccessor;
using StringValues = Microsoft.Extensions.Primitives.StringValues;

namespace LittleHourglass.Authorization.JWT
{
    public class JwtHelpersService: IJwtService
    {

        #region Fields
        /// <summary>
        /// 缓存存储token
        /// </summary>
        private readonly IDistributedCache _cache;

        /// <summary>
        /// 获取http 请求的上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructors
        public JwtHelpersService( IHttpContextAccessor httpContextAccessor, IDistributedCache distributedCache)
        {
            _cache = distributedCache;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Utilities
        /// <summary>
        ///  获取http 请求的Token 的值
        /// </summary>
        /// <returns></returns>
        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];
            return authorizationHeader == StringValues.Empty ? string.Empty : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token) => $"deactivated token:{token}";
        #endregion

        /// <summary>
        /// 创建用户的Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JwtAuthorizationModel CreateToken(UniUser user)
        {
            //颁布的密钥  SHA-256 在线加密  Little Hourglass
            string _jwtKey = "71a33d593866962dbcde5c7bd4732036955b42da2526d02c5c6857010f5c1229";
            //过期时间
            string _jwtExpireMinutes = "1440";
            //iss 证书
            string _jwtIssuer = "Little Hourglass";
            //jwt 的接收者
            string _jwtAudience = "Little Hourglass";

            DateTime expireTime = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtExpireMinutes));
            var claims = new List<Claim>();
            //claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userName));
            //自行扩充roles 登录者该有的角色 需要
            claims.Add(new Claim("UserId", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            //claims.Add(new Claim(ClaimTypes.Role, user.r));
            claims.Add(new Claim(ClaimTypes.GroupSid, user.Groups.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Expiration, expireTime.ToString()));
            //var userClaimsIdentity = new ClaimsIdentity(claims);
            //不记名认证使用的缺省值。
            var userClaimsIdentity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            // 建立一組對稱式加密的金鑰，主要用於 JWT 簽章之用
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            // HmacSha256 有要求必須要大於 128 bits，所以 key 不能太短，至少要 16 字元以上
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //创建声明信息
                Subject = new ClaimsIdentity(claims),
                //Jwt token 的签发者
                Issuer = _jwtIssuer,
                //过期时间
                Expires = expireTime,
                //创建 token
                SigningCredentials = signingCredentials,
                //Jwt token 的接收者
                Audience = _jwtAudience
            };

            // 產出所需要的 JWT securityToken 物件，並取得序列化後的 Token 結果(字串格式)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);
            var jwtInfo = new JwtAuthorizationModel
            {
                AuthTime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
                ExpireTime = new DateTimeOffset(expireTime).ToUnixTimeSeconds(),
                Success = true,
                Token = serializeToken,
                UserId = user.Id.ToString()
            };

            return jwtInfo;
        }

        /// <summary>
        /// 判断token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> IsTokenActiveAsync(string token)
        {
            return await _cache.GetStringAsync(GetKey(token)) == null;
        }

        /// <summary>
        /// 判断token是否有效
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<bool> IsCurrentTokenActiveAsync()
        {
            var token = GetCurrentAsync();
            var isyes = GetKey(token);
            var cacheData = await _cache.GetStringAsync(isyes);
            return cacheData == null;
        }

        /// <summary>
        /// 停用token
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task DeactivateTokenAsync(string token)
        {
            string _jwtExpireMinutes = "1440";
            await _cache.SetStringAsync(GetKey(token), " ", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(_jwtExpireMinutes))
            });
        }


    }
}
