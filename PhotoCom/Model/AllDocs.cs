using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model
{
    public  class AllDocs
    {
        public static List<DOCUMENTS_TB> lstDocs = new List<DOCUMENTS_TB>
        {
            //new DOCUMENTS_TB {CREATED_BY="1", DOC_ID=1,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="hus.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=false, CREATED_ON= DateTime.Now, CAPUTURED="wdwdw1" ,CAPUTURED_DATE= DateTime.Now, TAGS="fsfsf1" ,GEOLOCATION="wsdsa"},
            //new DOCUMENTS_TB {CREATED_BY=1, DOC_ID=2,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="hus2.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=true, CREATED_ON= DateTime.Now, CAPUTURED="wdwdw2" ,CAPUTURED_DATE= DateTime.Now, TAGS="fsfsf2" ,GEOLOCATION="wsdsa"},
            //new DOCUMENTS_TB {CREATED_BY=1, DOC_ID=3,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="testFile.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=true, CREATED_ON= DateTime.Now, CAPUTURED="wdwdw3" ,CAPUTURED_DATE= DateTime.Now, TAGS="fsfsf3" ,GEOLOCATION="wsdsa"},
            //new DOCUMENTS_TB {CREATED_BY=1, DOC_ID=4,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="testFile1.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=true, CREATED_ON= DateTime.Now, CAPUTURED="wdwdw4" ,CAPUTURED_DATE= DateTime.Now, TAGS="fsfsf4" ,GEOLOCATION="wsdsa"},
            //new DOCUMENTS_TB {CREATED_BY=1, DOC_ID=5,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="testFile2.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=true, CREATED_ON= DateTime.Now, CAPUTURED="wdwdw5" ,CAPUTURED_DATE= DateTime.Now, TAGS="fsfsf5" ,GEOLOCATION="wsdsa"},
        };

        public static List<DOC_LOG_TB> lstLog = new List<DOC_LOG_TB>
        {
              //new DOC_LOG_TB {CREATED_BY=1,DOC_ID=1, DOC_LOG_ID=1,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="hus.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=false, CREATED_ON= DateTime.Now},
              //new DOC_LOG_TB {CREATED_BY=1,DOC_ID=1, DOC_LOG_ID=4,FILE_NAME="hus1.jpg" , ORGINAL_FILE_NAME="testFile1.png", FILE_SIZE="9087", IS_HIDDEN=false, FILE_TYPE="jpg",IS_PUBLIC=true, CREATED_ON= DateTime.Now},

        };
        public static List<USERS_TB> lstUser = new List<USERS_TB>
        {
            //new USERS_TB {Id =""1", FIRST_NAME="husam"},
            //new USERS_TB {USER_ID =2, FIRST_NAME="rezq"},
            //new USERS_TB {USER_ID =3, FIRST_NAME="Mohammed"},
            //new USERS_TB {USER_ID =4, FIRST_NAME="bakr"},
        };
        public static List<DOC_SHARED_TB> lstShared = new List<DOC_SHARED_TB>
        {
            //new DOC_SHARED_TB {USER_ID =1, SHARED_ID =1, DOC_ID=1},
            //new DOC_SHARED_TB {USER_ID =2, SHARED_ID =2, DOC_ID=1},
            //new DOC_SHARED_TB {USER_ID =4, SHARED_ID =3, DOC_ID=1},
     
        };
    }
}
