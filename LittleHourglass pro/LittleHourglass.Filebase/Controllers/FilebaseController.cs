using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LittleHourglass.Filebase.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LittleHourglass.Filebase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "UniPermission")]
    public class FilebaseController : ControllerBase
    {
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload()
        {
            ReturnBox returnBox = new ReturnBox();
            try
            {
                var files = Request.Form.Files;

                List<FilebaseResponse> filebaseResponse = new List<FilebaseResponse>();
                foreach (var item in files)
                {
                    
                    filebaseResponse.Add();
                }

                return Ok(new ReturnBox
                {
                    Data = filebaseResponse
                });

            }
            catch (Exception ex)
            {
                return Ok(new ReturnBox
                {
                    Code = 40000,
                    Message = ex.ToString()
                });
            }
        }

        [HttpPost]
        [Route("download")]
        public async Task<IActionResult> Packing([FromBody] List<DownFileInfo> downFileInfos)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"This is Packing([FromBody]List<DownFileInfo> downFileInfos) Exception  Message:{ex.Message}");
                return Ok(new ReturnBox
                {
                    Code = 40000,
                    Message = ex.ToString()
                }); ;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/")]
        public IActionResult Test()
        {
            return Ok("Success");
        }

        public class ReturnBox
        {
            public int Code { get; set; } = 20000;
            public object Data { get; set; }
            public string Message { get; set; } = "Success";
        }
    }

}
