using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Table
{
    public class USERS_TB :IdentityUser

    {

        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
       
      
    }
}
