using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotoCom.Model.Interfaces;
using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCom.Model.SqlManger
{
    public class DocumentsServices : IDocumetnsServices
    {
        private IWebHostEnvironment _env;
        private PhotoContext _con;
        UserManager<USERS_TB> _userManager;
        SignInManager<USERS_TB> _signInManager;
        public DocumentsServices(IWebHostEnvironment env, PhotoContext con, UserManager<USERS_TB> userManager, SignInManager<USERS_TB> signInManager)
        {
            _env = env;
            _con = con;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void DeleteDocumentById(int docId)
        {
            using (var trans = _con.Database.BeginTransaction())
            {
                List<string> photoLst = new List<string>(); 
                try
                {
                    var doceument = _con.DOCUMENTS_TB.Where(c => c.DOC_ID == docId)?.FirstOrDefault();

                    foreach (var item in _con.DOC_SHARED_TB.Where(c => c.DOC_ID == doceument.DOC_ID).ToList())
                    {
                        _con.DOC_SHARED_TB.Remove(item);
                    }  
                    foreach (var item in _con.DOC_LOG_TB.Where(c => c.DOC_ID == doceument.DOC_ID).ToList())
                    {
                        photoLst.Add(item.FILE_NAME);

                        _con.DOC_LOG_TB.Remove(item);
                    }

                    photoLst.Add(doceument.FILE_NAME);
                    _con.DOCUMENTS_TB.Remove(doceument);
                    _con.SaveChanges();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    foreach (var item in photoLst)
                    {
                        if(item != null)
                        {
                            string ImagePath = Path.Combine(_env.WebRootPath, "Documents", item);

                            if (System.IO.File.Exists(ImagePath))
                            {
                                System.IO.File.Delete(ImagePath);
                            }
                        }
                        
                    }
                   
                    

                }
            }
              
        }

        public DOCUMENTS_VW GetAllDocument(string user_id)
        {
            DOCUMENTS_VW docs = new DOCUMENTS_VW()
            {
                LstDocument = _con.DOCUMENTS_TB.Where(c=>c.CREATED_BY == user_id).Include(c=>c.CREATED_BY_USER).ToList(),
                LstShared = _con.DOC_SHARED_TB.Where(c => c.USER_ID == user_id).Include(c=>c.DOCUMENTS).Include(c=>c.DOCUMENTS.CREATED_BY_USER).ToList()
            };
            return docs;
        }

        public List< DOCUMENTS_TB> GetAllPublicPhoto()
        {
            return _con.DOCUMENTS_TB.Where(c => c.IS_PUBLIC ).Include(c=>c.CREATED_BY_USER).ToList();
        }

        public DOCUMENTS_TB GetDocumentById(int docId)
        {
            return _con.DOCUMENTS_TB.Where(c => c.DOC_ID == docId)?.FirstOrDefault();
        }

        public DOC_LOG_TB GetDocumentLogById(int docLogId)
        {
            return _con.DOC_LOG_TB.Where(c => c.DOC_LOG_ID == docLogId)?.FirstOrDefault();
        }

        public List<DOC_SHARED_TB> GetDocumentUserShared(int doc_id)
        {
           var  LstShared = _con.DOC_SHARED_TB.Where(c => c.DOC_ID == doc_id).ToList();
            return LstShared;
        }

        public List<DOC_LOG_TB> GetLogByDocId(int docId)
        {
            return _con.DOC_LOG_TB.Where(c => c.DOC_ID == docId).Include(c=>c.CREATED_BY_USER).ToList();
        }

        public void SaveDocument(DOCUMENTS_TB documents)
        {

            using (var trans  =_con.Database.BeginTransaction())
            {
                string type = documents.FILE.ContentType.Split('/')[1];

                string name = documents.FILE.FileName;
                string ImageName = Guid.NewGuid().ToString() + name;
                documents.FILE_NAME = ImageName;

                string ImagePath = Path.Combine(_env.WebRootPath, "Documents", ImageName);
                try
                {
                    
                    using (FileStream FS = new FileStream(ImagePath, FileMode.Create))
                    {
                        documents.FILE.CopyTo(FS);
                    }


                    documents.FILE_SIZE = Constents.SizeSuffix(documents.FILE.Length);
                    documents.FILE_TYPE = type;
                    documents.ORGINAL_FILE_NAME = name;
                    documents.CREATED_ON = DateTime.Now;
                   
                    _con.DOCUMENTS_TB.Add(documents);

                    _con.SaveChanges();




                    int id = documents.DOC_ID;
                    if (documents.SharedUser != null)
                    {
                        foreach (var item in documents.SharedUser)
                        {
                            DOC_SHARED_TB doc_shared = new DOC_SHARED_TB
                            {
                                DOC_ID = id,
                                USER_ID = item
                            };
                            _con.DOC_SHARED_TB.Add(doc_shared);
                            _con.SaveChanges();


                        }
                    }
                  
                    DOC_LOG_TB log = new DOC_LOG_TB() 
                    { 
                        DOC_ID =id,
                       CAPUTURED= documents.CAPUTURED,
                       CAPUTURED_DATE = documents.CAPUTURED_DATE,
                       CREATED_BY = documents.CREATED_BY,
                       CREATED_ON= documents.CREATED_ON,
                       FILE_NAME= documents.FILE_NAME,
                       FILE_SIZE = documents.FILE_SIZE,
                       FILE_TYPE = documents.FILE_TYPE,
                       GEOLOCATION =documents.GEOLOCATION,
                       IS_PUBLIC = documents.IS_PUBLIC,
                       IS_HIDDEN = documents.IS_HIDDEN,
                       TAGS= documents.TAGS,
                       ORGINAL_FILE_NAME = documents.ORGINAL_FILE_NAME,
                       
                    };
                    _con.DOC_LOG_TB.Add(log);
                    _con.SaveChanges();
                    
                    trans.Commit();
                }
                catch
                {

                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                    trans.Rollback();
                }

            }
            
        } 
        
        public void UpdateDocument(DOCUMENTS_TB documents,string curr_user_id)
        {
            var old_doc = _con.DOCUMENTS_TB.Where(c => c.DOC_ID == documents.DOC_ID)?.FirstOrDefault();

          


            using (var trans = _con.Database.BeginTransaction())
            {
                string ImagePath = "";
                if (documents.FILE != null)
                {

                    string type = documents.FILE.ContentType.Split('/')[1];

                    string name = documents.FILE.FileName;
                    string ImageName = Guid.NewGuid().ToString() + name;
                    old_doc.FILE_NAME = ImageName;

                    ImagePath = Path.Combine(_env.WebRootPath, "Documents", ImageName);
                    using (FileStream FS = new FileStream(ImagePath, FileMode.Create))
                    {
                        documents.FILE.CopyTo(FS);
                    }
                    old_doc.FILE_SIZE = Constents.SizeSuffix(documents.FILE.Length);
                    old_doc.FILE_TYPE = type;
                    old_doc.ORGINAL_FILE_NAME = name;
                }
                try
                {
                    old_doc.CAPUTURED_DATE = documents.CAPUTURED_DATE;
                    old_doc.CAPUTURED = documents.CAPUTURED;
                    old_doc.GEOLOCATION = documents.GEOLOCATION;
                    old_doc.TAGS = documents.TAGS;
                    old_doc.IS_PUBLIC = documents.IS_PUBLIC;
                    old_doc.CREATED_ON = DateTime.Now;

                    _con.DOCUMENTS_TB.Update(old_doc);
                    _con.SaveChanges();

                    if (old_doc.CREATED_BY == curr_user_id)
                    {
                        foreach (var item in _con.DOC_SHARED_TB.Where(c => c.DOC_ID == documents.DOC_ID).ToList())
                        {
                            _con.DOC_SHARED_TB.Remove(item);
                        }

                        if (documents.SharedUser != null)
                        {
                            foreach (var item in documents.SharedUser)
                            {
                                DOC_SHARED_TB doc_shared = new DOC_SHARED_TB
                                {
                                    DOC_ID = documents.DOC_ID,
                                    USER_ID = item
                                };
                                _con.DOC_SHARED_TB.Add(doc_shared);
                                _con.SaveChanges();


                            }
                        }
                    }
                   

                    DOC_LOG_TB log = new DOC_LOG_TB()
                    {
                        DOC_ID = old_doc.DOC_ID,
                        CAPUTURED = old_doc.CAPUTURED,
                        CAPUTURED_DATE = old_doc.CAPUTURED_DATE,
                        CREATED_BY = curr_user_id,
                        CREATED_ON = old_doc.CREATED_ON,
                        FILE_NAME = old_doc.FILE_NAME,
                        FILE_SIZE = old_doc.FILE_SIZE,
                        FILE_TYPE = old_doc.FILE_TYPE,
                        GEOLOCATION = old_doc.GEOLOCATION,
                        IS_PUBLIC = old_doc.IS_PUBLIC,
                        IS_HIDDEN = old_doc.IS_HIDDEN,
                        TAGS = old_doc.TAGS,
                        ORGINAL_FILE_NAME = old_doc.ORGINAL_FILE_NAME,

                    };
                    _con.DOC_LOG_TB.Add(log);
                    _con.SaveChanges();
                    trans.Commit();
                }
                catch(Exception ex)
                {

                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                    trans.Rollback();
                }

            }

          
           
          
        }
    }
}
