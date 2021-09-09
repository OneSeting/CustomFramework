using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LittleHourglass.Commonality.Base;
using LittleHourglass.DataBase.Model;
using Microsoft.AspNetCore.Authorization;

namespace LittleHourglass.WebRouter.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = "UniPermission")]
    public class HomeController : BaseServices
    {
        #region Files
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Token _token;
        #endregion

        #region Constructors   
        public HomeController(Token token, IHttpContextAccessor httpContextAccessor)
        {
            _token = token;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        //主入口
        [HttpGet]
        public IActionResult MainPepiline([FromBody] JsonElement parameter)
        {
            object result = "我是主入口，参数如下" + parameter;
            return Ok(result);
        }

        [HttpGet]
        //[Route("Token")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] JsonElement parameter)
        {
            ParamBox param = ParamBoxForMat(parameter);
            var result = _token.Process(param).Result;
            return Ok(result);
        }

        //测试入口
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Test()
        {
            object result = "我是测试";
            return Ok(result);
        }

    }

    //[ApiController]
    //public class IndexController : BaseServices
    //{
    //    #region Files
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly Token _token;
    //    #endregion

    //    #region Constructors
    //    public IndexController(Token token, IHttpContextAccessor httpContextAccessor)
    //    {
    //        token = _token;
    //        httpContextAccessor = _httpContextAccessor;
    //    }
    //    #endregion

    //    [HttpGet]
    //    [Route("Home")]
    //    [Route("Home/About")]
    //    public IActionResult HomePepiline([FromBody] JsonElement parameter)
    //    {
    //        object result = "我是测试入口01" + parameter;
    //        return Ok(result);
    //    }
    //}
}
