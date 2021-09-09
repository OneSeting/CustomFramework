using LittleHourglass.Authorization.JWT.Policy;
using LittleHourglass.DataBase.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Authorization.JWT
{
    public static class ServiceConfiguration
    {
        ///// <summary>
        ///// Jwt 服务注册的拓展  服务拓展
        ///// </summary>
        ///// <returns></returns>
        //public static IServiceCollection SupervisorConfiguration(this IServiceCollection services)
        //{
        //    //颁布的密钥  SHA-256 在线加密  Little Hourglass
        //    string _jwtKey = "71a33d593866962dbcde5c7bd4732036955b42da2526d02c5c6857010f5c1229";
        //    //过期时间
        //    string _jwtExpireMinutes = "1440";
        //    //iss 证书
        //    string _jwtIssuer = "Little Hourglass";
        //    //jwt 的接收者
        //    string _jwtAudience = "Little Hourglass";

        //    TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToDouble(_jwtExpireMinutes));
        //    SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

        //    services.AddAuthorization(options =>
        //    {
        //        //1、Definition authorization policy
        //        options.AddPolicy("UniPermission", policy => policy.Requirements.Add(new PolicyRequirement()));
        //    }).AddAuthentication(s =>
        //    {
        //        s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //        s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer(s=> {
        //        //参数
        //        s.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidIssuer= _jwtIssuer,
        //            ValidAudience = _jwtAudience,
        //            IssuerSigningKey= key,
        //            ClockSkew= expiration,
        //            ValidateLifetime=true
        //        };
        //        //事件
        //        s.Events = new JwtBearerEvents
        //        {
                     

        //        };
        //    });


        //}

    }
}
