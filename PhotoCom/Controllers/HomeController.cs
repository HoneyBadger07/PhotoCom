using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PhotoCom.Model;
using PhotoCom.Model.Interfaces;
using PhotoCom.Model.SqlManger;
using PhotoCom.Model.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PhotoCom.Controllers
{
    public class HomeController : Controller
    {

        private IWebHostEnvironment _env;
        private IDocumetnsServices _documentsServices;
        private IUserServices  _userServices;
        UserManager<USERS_TB> _userManager;
        SignInManager<USERS_TB> _signInManager;
        public HomeController(IWebHostEnvironment env, UserManager<USERS_TB> userManager, SignInManager<USERS_TB> signInManager, IDocumetnsServices documetnsServices, IUserServices userServices)
        {
            _env = env;
            _documentsServices = documetnsServices;
            _userServices = userServices;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()

        {
            if (!IsLogin())
             return RedirectToAction("Login", "User");

            ViewData["Title"] = "My Photo";
            return View(_documentsServices.GetAllDocument(_userManager.GetUserId(User)));

        }  
        
        public IActionResult PuplicPhotos()

        {
            if (!IsLogin())
             return RedirectToAction("Login", "User");

            ViewData["Title"] = "All Puplic Photos";
            return View(_documentsServices.GetAllPublicPhoto());

        }  
        [HttpGet]
        public IActionResult UploadFile(string DoId)
        {
            if (!IsLogin())
                return RedirectToAction("Login", "User");
            ViewData["Title"] = "Upload Photo";
            ViewBag.UserLst = _userServices.GetAllUsers(_userManager.GetUserId(User));
            ViewBag.shared_document =new List<DOC_SHARED_TB>();
            if (!string.IsNullOrEmpty(DoId))
            {

                
                int id = 0;
                int.TryParse(DoId , out id);
                if (id != 0)
                {

                    var document = _documentsServices.GetDocumentById(id);
                    if (document != null)
                    {
                        string user_id = _userManager.GetUserId(User);
                        if (document.CREATED_BY != user_id)
                        {
                            ViewBag.DoId = DoId;
                            var shared_document = _documentsServices.GetDocumentUserShared(id);
                            var isSharedWithMe = shared_document.Where(c => c.USER_ID == user_id)?.FirstOrDefault();
                            if (isSharedWithMe == null)
                            {
                                return RedirectToAction(nameof(UploadFile));
                            }
                            ViewBag.isSharedWithMe = true;
                            ViewBag.shared_document = shared_document;
                            return View(document);

                        }
                        else
                        {
                            ViewBag.DoId = DoId;
                            var shared_document = _documentsServices.GetDocumentUserShared(id);
                            ViewBag.shared_document = shared_document;
                            return View(document);
                        }

                    }
                  
                   

              
                }

            }

         
            return View();
        }

        [HttpGet]
        
        public JsonResult GetDocumnetDetails(string docId)
        {
            int id = 0;
            int.TryParse(docId, out id);
            if (id != 0)
            {
                var document = _documentsServices.GetDocumentById(id);
                var shared_document = _documentsServices.GetDocumentUserShared(id);
                var json = JsonConvert.SerializeObject(new { document, shared_document });
                return Json(json);

            }
            return Json("");
        }

        [HttpGet]
        public JsonResult GetDocLog (int docId)
        {
           List<DOC_LOG_TB> lst  =  _documentsServices.GetLogByDocId(docId);
          var json =  JsonConvert.SerializeObject(lst);

            return Json(json);
        }
        [HttpPost]
        public IActionResult UploadFile(DOCUMENTS_TB  documents)
        {
            if (!IsLogin())
                return RedirectToAction("Login", "User");
            ViewBag.shared_document = new List<DOC_SHARED_TB>();
            ViewBag.DoId = documents.DOC_ID;

            if (ModelState.IsValid)
            {

                if (documents.FILE == null && documents.DOC_ID == 0)
                {
                    string error = $"You must choose photo";
                    ViewData["Title"] = "Upload Photo";
                    ViewBag.UserLst = _userServices.GetAllUsers(_userManager.GetUserId(User));
                    ModelState.AddModelError("", error);
                    return View(documents);
                }

                if (documents.FILE != null)
                {
                    string type = documents.FILE.ContentType.Split('/')[1];
                    string sf = Constents.AllowPhotoEx.Where(c => c.ToUpper() == type.ToUpper())?.FirstOrDefault();
                    if (sf == null)
                    {
                        string error = $"You can just upload photo with type {string.Join(',', Constents.AllowPhotoEx)}";
                        ViewData["Title"] = "Upload Photo";
                        ViewBag.UserLst = _userServices.GetAllUsers(_userManager.GetUserId(User));
                        ModelState.AddModelError("", error);
                        return View(documents);
                    }

                }


                if (documents.DOC_ID == 0)
                {
                    documents.CREATED_BY = _userManager.GetUserId(User);

                    _documentsServices.SaveDocument(documents);
                }
                else
                {
                    _documentsServices.UpdateDocument(documents, _userManager.GetUserId(User));

                }

                    
                   
               
               
                return RedirectToAction(nameof(Index));


            }



            ViewData["Title"] = "Upload Photo";
            ViewBag.UserLst = _userServices.GetAllUsers(_userManager.GetUserId(User));
            if (documents.DOC_ID != 0)
            {
                var shared_document = _documentsServices.GetDocumentUserShared(documents.DOC_ID);
                ViewBag.shared_document = shared_document;
            }

            
            return View(documents);
        }
    

        public IActionResult DownloadFile(int DOC_ID)
        {
            if (!IsLogin())
                return RedirectToAction("Login", "User");
            var document = _documentsServices.GetDocumentById(DOC_ID);
            if(document != null)
            {
                var pathToFile = Path.Combine(_env.WebRootPath, "Documents", document.FILE_NAME);
                if (System.IO.File.Exists(pathToFile))
                {
                    byte[] contents = System.IO.File.ReadAllBytes(pathToFile);
                    return File(contents, document.GetContentType(),document.ORGINAL_FILE_NAME);
                }
               

            }
            return RedirectToAction("index", "home");


        } 
        public IActionResult DownloadLogFile(int DOC_ID)
        {
            if (!IsLogin())
                return RedirectToAction("Login", "User");
            var document = _documentsServices.GetDocumentLogById(DOC_ID);
            if(document != null)
            {
                var pathToFile = Path.Combine(_env.WebRootPath, "Documents", document.FILE_NAME);
                if (System.IO.File.Exists(pathToFile))
                {
                    byte[] contents = System.IO.File.ReadAllBytes(pathToFile);
                    return File(contents, document.GetContentType(),document.ORGINAL_FILE_NAME);
                }
               

            }
            return RedirectToAction("index", "home");


        } 
        [HttpPost]
        public IActionResult DeleteFile(int DOC_ID)
        {
            if (!IsLogin())
                return RedirectToAction("Login", "User");
            _documentsServices.DeleteDocumentById(DOC_ID); 
            return RedirectToAction("index", "home");


        }


        private bool IsLogin()
        {
            return _signInManager.IsSignedIn(User);

        }
       
    }
}
