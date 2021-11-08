using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Table
{
    public class DOC_LOG_TB :DOC_COLUMN
    {
        [Key]
        public int DOC_LOG_ID { get; set; }
        public int DOC_ID { get; set; }
    }
}
