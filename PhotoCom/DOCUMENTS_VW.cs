using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom
{
    public class DOCUMENTS_VW
    {
       public List<DOCUMENTS_TB> LstDocument { get; set; }
       public List<DOC_SHARED_TB> LstShared { get; set; }
    }
}
