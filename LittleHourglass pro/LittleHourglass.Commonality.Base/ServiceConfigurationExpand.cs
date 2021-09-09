using LittleHourglass.Authorization.JWT;
using LittleHourglass.Authorization.JWT.Policy;
using LittleHourglass.Commonality.Base.Enum;
using LittleHourglass.Commonality.Base.Service;
using LittleHourglass.DataBase.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Commonality.Base
{
    public static class ServiceConfigurationExpand
    {
        /// <summary>
        /// Jwt 服务注册的拓展  服务拓展
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection SupervisorConfiguration(this IServiceCollection services)
        {
            //颁布的密钥  SHA-256 在线加密  Little Hourglass
            string _jwtKey = "71a33d593866962dbcde5c7bd4732036955b42da2526d02c5c6857010f5c1229";
            //过期时间
            string _jwtExpireMinutes = "1440";
            //iss 证书
            string _jwtIssuer = "Little Hourglass";
            //jwt 的接收者
            string _jwtAudience = "Little Hourglass";
            //服务器 redis 地址
            string redisConfig = "47.119.154.135:6379,password=admin123";

            TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToDouble(_jwtExpireMinutes));
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

            services.AddAuthorization(options =>
            {
                //1、Definition authorization policy 定义授权策略
                options.AddPolicy("UniPermission", policy => policy.Requirements.Add(new PolicyRequirement()));
            }).AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(s =>
            {
                //参数
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _jwtIssuer,
                    ValidAudience = _jwtAudience,
                    IssuerSigningKey = key,
                    ClockSkew = expiration,
                    ValidateLifetime = true
                };
                //事件
                s.Events = new JwtBearerEvents
                {
                    //错误处理
                    OnAuthenticationFailed = context =>
                    {
                        var payload = JsonConvert.SerializeObject(new ReturnBox
                        {
                            Code = (int)UniStatusCode.TokenExpired,
                            Message = context.Exception?.Message
                        });
                        context.Response.ContentType = "application/json";
                        context.Response.WriteAsync(payload);
                        return Task.CompletedTask;
                    },
                    //如果授权失败并导致Forbidden响应时调用
                    OnForbidden = context =>
                    {
                        var payload = JsonConvert.SerializeObject(new ReturnBox
                        {
                            Code = (int)HttpStatusCode.Forbidden,
                            Message = context.HttpContext.Items["ValidationError"].ToString()
                        });
                        context.Response.ContentType = "application/json";
                        context.Response.WriteAsync(payload);
                        return Task.CompletedTask;
                    },
                    //首次接收协议消息时调用。
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/notify-hub")))// for me my hub endpoint is ConnectionHub
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            //redis
            services.AddDistributedRedisCache(r =>
            {
                r.Configuration = redisConfig;
                r.InstanceName = "";
            });

            //http 请求的相关服务和实现
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //授权的相关服务和实现 之间转到对应的授权接口数据
            services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
            //jwt 颁发的相关服务   接口服務           接口继承实现
            services.AddTransient<IJwtService, JwtHelpersService>();
            //token 的相关服务
            services.AddTransient<Token>();

            //系统级的用户的服务
            services.AddScoped<UniUserService>();
            services.AddScoped<DapperRepository>();
            return services;
        }

    }
}
