using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model
{
    public class PhotoContext  : IdentityDbContext<USERS_TB>
    {
        public PhotoContext(DbContextOptions<PhotoContext> op) : base(op)
        {

        }
        public DbSet<DOC_LOG_TB> DOC_LOG_TB { get; set; }
        public DbSet<DOC_SHARED_TB> DOC_SHARED_TB { get; set; }
        public DbSet<DOCUMENTS_TB> DOCUMENTS_TB { get; set; }

    }
}
