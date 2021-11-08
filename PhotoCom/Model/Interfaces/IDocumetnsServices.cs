using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.Interfaces
{
    
    public interface  IDocumetnsServices
    {

        public DOCUMENTS_TB GetDocumentById(int docId);
        public DOC_LOG_TB GetDocumentLogById(int docLogId);
        public void DeleteDocumentById(int docId);
        public DOCUMENTS_VW GetAllDocument(string user_id);
        public List<DOCUMENTS_TB> GetAllPublicPhoto();

        public List<DOC_SHARED_TB> GetDocumentUserShared(int doc_id); 
        public List<DOC_LOG_TB> GetLogByDocId(int docId);

        public void SaveDocument(DOCUMENTS_TB doc);
        public void UpdateDocument(DOCUMENTS_TB doc,string curr_user_id);


    }
}
