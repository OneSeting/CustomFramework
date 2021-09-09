using LittleHourglass.DataBase.Mysql.ORM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LittleHourglass.DataBase.Model.ModelBox
{
    public class UniUserBox:BaseEntity
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// 加密方式 10：Md5  20：Shet250 
        /// </summary>
        public int PasswordFormatId { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Deleted { get; set; } = false;

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Toggle { get; set; } = true;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOnUtc { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginOnUtc { get; set; }

        /// <summary>
        /// 分组 0是浏览客户 1是系统管理
        /// </summary>
        public int Groups { get; set; }

        /// <summary>
        /// 当前用户的角色
        /// </summary>
        public string Roles { get; set; }
    }
}
