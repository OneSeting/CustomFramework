using LittleHourglass.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using LittleHourglass.Commonality.Base.Enum;

namespace LittleHourglass.Commonality.Base.Service
{
    public class UniUserService
    {
        #region Fields
        private readonly DapperRepository _dapperRepository;
        #endregion

        #region Constructors
        public UniUserService(DapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        #endregion

        /// <summary>
        /// 通过Id查找用户
        /// </summary>
        /// <returns></returns>
        public async Task<UniUser> GetUserById(int id)
        {
            if (id <= 0)
                throw new AggregateException("Id this is null");
            return await _dapperRepository.GetAsync<UniUser>(id);
        }

        /// <summary>
        /// 通过用户名和密码去验证
        /// </summary>
        /// <returns></returns>
        public async Task<UniUser> GetUserNamePwd(string userName, string pwd)
        {
            if (string.IsNullOrEmpty(userName))
                throw new AggregateException("userName this is null");
            if (string.IsNullOrEmpty(pwd))
                throw new AggregateException("pwd this is null");

            var data = await _dapperRepository.QueryAsync<UniUser>($"Select * from UniUser where UserName='{userName}'");//Id
            UniUser uniUser = data.FirstOrDefault();

            if (uniUser == null || uniUser.Id <= 0 || uniUser.Deleted)
                throw new AggregateException("User does not exist");

            switch ((PasswordFormat)uniUser.PasswordFormatId)
            {
                case PasswordFormat.Clear:
                    pwd = uniUser.Password;
                    break;
                case PasswordFormat.Hashed:
                    pwd = EncryptionHelper.CreatePasswordSHA256(uniUser.Password, uniUser.PasswordSalt);
                    break;
                case PasswordFormat.Md5ed:
                    pwd = EncryptionHelper.CreatePasswordMD5(uniUser.Password, uniUser.PasswordSalt);
                    break;
                default:
                    break;
            }

            int id = await _dapperRepository.ExecuteScalarAsync<int>($"Select Id from UniUser where UserName='{userName}' and Password='{pwd}' and Deleted = 0 ");
            if (id <= 0)
                throw new AggregateException("UserName or pwd error");

            return uniUser;
        }

    }
}
