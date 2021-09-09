using LittleHourglass.Commonality.Base.Enum;
using LittleHourglass.DataBase.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LittleHourglass.Commonality.Base
{
    public class BaseSvrAction
    {
        /// <summary>
        /// Return obj
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected ReturnBox UniData(object data, string msg = "", UniStatusCode statusCode = UniStatusCode.Success)
        {
            if (data == null)
                throw new ArgumentException("Data is null");

            var result = new ReturnBox()
            {
                Message = msg,
                Data = data,
                Code = (int)statusCode
            };
            return result;
        }

        /// <summary>
        /// Return message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="meta"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected ReturnBox UniMessage(string msg, string meta = "", UniStatusCode statusCode = UniStatusCode.Success)
        {
            var result = new ReturnBox
            {
                Message = msg,
                Meta = meta,
                Code = (int)statusCode
            };
            return result;
        }

        /// <summary>
        /// Return page list
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected ReturnBox UniPagedBox(object data, int pageIndex = 0, int pageSize = 0, long totalCount = 0, string msg = "", UniStatusCode statusCode = UniStatusCode.Success)
        {
            if (data == null)
                throw new ArgumentException("Parameter is null");

            var result = new ReturnBox
            {
                Data = data,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                Message = msg,
                Code = (int)statusCode
            };
            return result;
        }

        /// <summary>
        /// Return status，Success or Failed
        /// </summary>
        /// <param name="isSuccess">True or False</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected ReturnBox UniStatus(bool isSuccess, string msg = "")
        {
            //msg = isSuccess? msg.IsBlankThen("Success"): msg.IsBlankThen("Failed");
            msg = isSuccess ? string.IsNullOrEmpty(msg) ? "Success" : msg : string.IsNullOrEmpty(msg) ? "Failed" : msg;
            if (isSuccess)
                return UniMessage("Success");

            return UniError();
        }

        /// <summary>
        /// Return success
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected ReturnBox UniSuccess(string msg = "", UniStatusCode statusCode = UniStatusCode.Success)
        {
            return UniMessage(string.IsNullOrEmpty(msg) ? "Success" : msg, "", statusCode);
        }

        /// <summary>
        /// Return error message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        protected ReturnBox UniError(string msg = "", UniStatusCode statusCode = UniStatusCode.Error)
        {
            return UniMessage(string.IsNullOrEmpty(msg) ? "Failed" : msg, "", statusCode);
        }

        /// <summary>
        /// Convert Object to Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected T ConvertObjToModel<T>(object obj)
        {
            return JsonConvert.DeserializeObject<T>(obj.ToString());
        }

    }
}
