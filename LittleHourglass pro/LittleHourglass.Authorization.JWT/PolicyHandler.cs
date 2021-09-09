using LittleHourglass.Authorization.JWT.Policy;
using LittleHourglass.Commonality.BaseExpand;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Authorization.JWT
{
    /// <summary>
    /// 自定义授权策略
    /// </summary>
    public class PolicyHandler: AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly RoleService _roleService = ServiceLocator.Current.GetInstance<RoleService>();
      
        public PolicyHandler(IAuthenticationSchemeProvider schemes, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            Schemes = schemes;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary> 
        /// 所有程序的入口事件 指定用户进入程序后  能干什么  做什么处理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            bool isEnableVerifyRoute = true;
            var httpContext = _httpContextAccessor.HttpContext;
            try
            {
                //获取授权方式
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate!=null)
                {
                    //验证签发的用户信息
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    if (!result.Succeeded)
                        throw new SecurityTokenException(result.Failure.Message);
                    //判断是否为已停用 Token
                    if (!await _jwtService.IsCurrentTokenActiveAsync())
                        throw new SecurityTokenException("Token is invalid");

                    httpContext.User = result.Principal;
                    if (!isEnableVerifyRoute)
                    {
                        context.Succeed(requirement);
                        return;
                    }

                    //判断角色与 Url 是否对应
                    //以下是走业务走逻辑

                    //var path = httpContext.Request.RouteValues["path"].ToString().ToLower();
                    //var roles = httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault().Value;

                    //if (roles.IsBlank())
                    //    throw new SecurityTokenException("Permession denied");

                    //var roleIds = JsonConvert.DeserializeObject<List<int>>(roles);
                    //var permessions = await _roleService.GetRolesPermessions(roleIds);

                    //if (permessions?.Where(i => i.RoutePath.ToLower().Equals(path.ToLower())).FirstOrDefault() == null)
                    //    throw new SecurityTokenException("Permession denied");
                }
                context.Succeed(requirement);
                return;
            }
            catch (Exception e)
            {
                httpContext.Items.Add("ValidationError", e.Message);
                context.Fail();
            }

        }
    }
}
