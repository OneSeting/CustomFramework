using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LittleHourglass.DataBase.Model
{
    public class ParamBox
    {
        public ParamBox(UniContext uniContext, CurrentUser currentUser)
        {
            UniContext = uniContext;
            CurrentUser = currentUser;
        }

        public object Data { get; set; }

        public UniContext UniContext { get; private set; }

        public CurrentUser CurrentUser { get; private set; }
    }

    public class UniContext
    {

        public IPAddress IpAddress { get; set; }

        public string Referer { get; set; }

        public string AccessToken { get; set; }
    }


    public class CurrentUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Roles { get; set; }
    }
}
