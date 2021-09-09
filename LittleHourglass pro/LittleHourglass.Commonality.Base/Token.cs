using LittleHourglass.Authorization.JWT;
using LittleHourglass.Commonality.Base.Service;
using LittleHourglass.Commonality.BaseExpand;
using LittleHourglass.DataBase.Model;
using LittleHourglass.DataBase.Model.ModelBox;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace LittleHourglass.Commonality.Base
{
    public class Token:BaseSvrAction, ISvrAction
    {
        #region Fields
        public readonly UniUserService _uniUserService = ServiceLocator.Current.GetInstance<UniUserService>();
        public readonly IJwtService _jwtHelpersService;
        #endregion

        #region 
        public Token(IJwtService jwtHelpersService) 
        {
            _jwtHelpersService = jwtHelpersService;
        }

        public async Task<ReturnBox> Process(ParamBox paramBox)
        {
            try
            {
                if (paramBox.Data == null)
                    throw new AggregateException("Param is null");
                UniUserBox uniUserBox = JsonConvert.DeserializeObject<UniUserBox>(paramBox.Data.ToString());
                if (string.IsNullOrEmpty(uniUserBox.Password))
                    throw new AggregateException("Password is null");
                if (string.IsNullOrEmpty(uniUserBox.UserName))
                    throw new AggregateException("UserName is null");

                var dataUtcNow = DateTime.UtcNow;
                var user = await _uniUserService.GetUserNamePwd(uniUserBox.UserName,uniUserBox.Password);
                var token= _jwtHelpersService.CreateToken(user);
                return UniData(token);
            }
            catch (Exception ex)
            {
                throw new AggregateException(ex.Message);
               
            }
        }
        #endregion

        #region 
        #endregion

        #region 
        #endregion
    }
}
