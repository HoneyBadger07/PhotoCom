using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Table
{
    public class DOC_SHARED_TB
    {
        [Key]
        public int SHARED_ID { get; set; }
        public string USER_ID { get; set; }
        [ForeignKey(nameof(USER_ID))]
        public USERS_TB USER { get; set; }

        public int DOC_ID { get; set; }
        [ForeignKey(nameof(DOC_ID))]
        public DOCUMENTS_TB DOCUMENTS { get; set; }
    }
}
