using PhotoCom.Model.Interfaces;
using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.SqlManger
{
    public class UsersServices : IUserServices
    {
        private PhotoContext _con;
        public UsersServices(PhotoContext con)
        {
            _con = con;
        }
        public List<USERS_TB> GetAllUsers(string userId) { return _con.Users.ToList(); }
    }
}
