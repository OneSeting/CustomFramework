using LittleHourglass.Commonality.Base;
using LittleHourglass.Commonality.Base.Service;
using LittleHourglass.Commonality.BaseExpand;
using LittleHourglass.DataBase.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LittleHourglass.WebRouter.Controllers
{
    public class BaseServices : ControllerBase
    {
        #region Files
        private readonly UniUserService _uniUserService = ServiceLocator.Current.GetInstance<UniUserService>();
        #endregion

        /// <summary>
        /// 格式化处理传入的数据
        /// </summary>
        /// <param name="jsonElement"></param>
        /// <returns></returns>
        public ParamBox ParamBoxForMat(JsonElement jsonElement)
        {
            object data = JsonConvert.DeserializeObject<object>(jsonElement.ToString());
            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            var referer = HttpContext.Request.Headers["Referer"].FirstOrDefault();
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split().Last();

            var currentUser = new CurrentUser();
            var userId = HttpContext.User.Claims.Where(c => c.Type.Equals("userId")).FirstOrDefault()?.Value;

            //有该用户信息
            if (userId != null)
            {
                Task<UniUser> taskUser = _uniUserService.GetUserById(int.Parse(userId));
                var user = taskUser.Result;
                user.LastIpAddress = ipAddress?.ToString();
                user.LastLoginOnUtc = DateTime.UtcNow;
                //获取当前用户
                currentUser.Id = user.Id;
                currentUser.Name = user.Name;
                currentUser.NickName = user.NickName;
                currentUser.UserName = user.UserName;
                currentUser.Email = user.Email;
                currentUser.Roles = user.Roles;
            }

            var uniContext = new UniContext
            {
                AccessToken = token,
                IpAddress = ipAddress,
                Referer = referer
            };

            ParamBox paramBox = new ParamBox(uniContext, currentUser);
            paramBox.Data = data;

            return paramBox;
        }
    }
}
