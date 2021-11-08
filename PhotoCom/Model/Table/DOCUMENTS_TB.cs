using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Table
{
    public class DOCUMENTS_TB : DOC_COLUMN
    {
        [Key]
        public int DOC_ID { get; set; }
      
        [NotMapped]
        [DisplayName("Photo")]
        //[Required(ErrorMessage ="You must choose photo")]
        public IFormFile  FILE { get; set; }

        [NotMapped]
        
        public string [] SharedUser { get; set; }

    }
   
}
