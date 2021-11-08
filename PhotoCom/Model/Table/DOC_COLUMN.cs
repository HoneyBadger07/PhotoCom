using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Table
{
    public class DOC_COLUMN
    {
        public string FILE_NAME { get; set; }
        public string ORGINAL_FILE_NAME { get; set; }
        public string FILE_TYPE { get; set; }
        public string FILE_SIZE { get; set; }

        [Required(ErrorMessage = "The Caputured field is required.")]

        public string CAPUTURED { get; set; }
        [Required(ErrorMessage = "The Caputured Date field is required.")]


        public DateTime? CAPUTURED_DATE { get; set; }
        [Required(ErrorMessage = "The Tags field is required.")]
        public string TAGS { get; set; }
        [Required(ErrorMessage = "The Geolocation field is required.")]
        public string GEOLOCATION { get; set; }

        public bool IS_PUBLIC { get; set; }
        public bool IS_HIDDEN { get; set; }
        public DateTime CREATED_ON { get; set; }
        public string CREATED_BY { get; set; }

        [ForeignKey(nameof(CREATED_BY))]
        public USERS_TB CREATED_BY_USER { get; set; }

        public string GetContentType()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("bmp", "image/bmp");
            d.Add("gif", "image/gif");
            d.Add("jpeg", "image/jpeg");
            d.Add("jpg", "image/jpeg");
            d.Add("png", "image/png");
            d.Add("tif", "image/tiff");
            d.Add("tiff", "image/tiff");
            d.Add("ico", "image/ico");



            return d[this.FILE_TYPE];
        }
    }
}
