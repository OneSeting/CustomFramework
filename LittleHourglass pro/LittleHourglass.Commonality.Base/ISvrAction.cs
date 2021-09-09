using LittleHourglass.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LittleHourglass.Commonality.Base
{
    public interface ISvrAction
    {
        /// <summary>
        /// 全局进入 全局参数接口
        /// </summary>
        /// <returns></returns>
        Task<ReturnBox> Process(ParamBox paramBox);
    }
}
