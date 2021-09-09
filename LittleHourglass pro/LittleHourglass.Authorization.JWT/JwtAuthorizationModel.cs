using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Authorization.JWT
{
    public class JwtAuthorizationModel
    {
        /// <summary>
        /// 授权时间
        /// </summary>
        public long AuthTime { get; set; }

        /// <summary>
        /// 授权过期时间
        /// </summary>
        public long ExpireTime { get; set; }

        /// <summary>
        /// 是否授权成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
