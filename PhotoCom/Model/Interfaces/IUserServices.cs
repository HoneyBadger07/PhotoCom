using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Interfaces
{
    public interface IUserServices
    {

        public List<USERS_TB> GetAllUsers(string userId);
    }
}
