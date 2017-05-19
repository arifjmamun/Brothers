using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.BLL;
using Core.Context;
using Core.Helper;
using Core.Models.EntityModels;

namespace Web.Controllers
{
    public class UploadsController : Controller
    {
        private BrothersContext db = new BrothersContext();

        private readonly UploadManager _uploadManager = new UploadManager();
        private readonly CategoryManager _categoryManager = new CategoryManager();
        private readonly SubCategoryManager _subCategoryManager = new SubCategoryManager();

        #region Private Methods

        private void SetDropDownPostBackData(Upload upload)
        {
            ViewBag.Drives = new SelectList(_uploadManager.GetDrives(), "Name", "VolumeLabel", upload.Drive);
            ViewBag.Category = new SelectList(_categoryManager.GetAll(), "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategory = new SelectList(_subCategoryManager.GetAll(), "SubCategoryId", "SubCategoryName",
                upload.SubCategoryId);
        }

        private void SetDropDownData()
        {
            ViewBag.Drives = new SelectList(_uploadManager.GetDrives(), "Name", "VolumeLabel");
            ViewBag.Category = new SelectList(_categoryManager.GetAll(), "CategoryId", "CategoryName");
            ViewBag.SubCategory = new SelectList(_subCategoryManager.GetAll(), "SubCategoryId", "SubCategoryName");
        }

        #endregion

        // GET: Uploads
        public ActionResult Index()
        {
            var uploads = _uploadManager.GetAll();
            return View(uploads.ToList());
        }

        // GET: Uploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = _uploadManager.GetById((int) id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // GET: Uploads/Create
        public ActionResult Create()
        {
            SetDropDownData();
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "UploadId,Drive,Title,CategoryId,SubCategoryId,Thumbnail")] Upload upload,
            IEnumerable<HttpPostedFileBase> selectedFiles)
        {
            if (selectedFiles == null) ModelState.AddModelError("selectedFiles", "You must select files to upload.");
            HttpPostedFileBase thumbnail = Request.Files["thumbImage"];


            if (ModelState.IsValid)
            {
                upload.UploadPath = _uploadManager.GetUploadPath(upload.CategoryId, upload.SubCategoryId, upload.Title);
                if (thumbnail.ContentLength > 0)
                {
                    var image = new UploadedFile(thumbnail.FileName, thumbnail.ContentLength);
                    var alertThumb = _uploadManager.IsImageValid(image);
                    if (!alertThumb.Flag)
                    {
                        ViewBag.Alert = alertThumb;
                        SetDropDownPostBackData(upload);
                        return View(upload);
                    }
                    using (var reader = new BinaryReader(thumbnail.InputStream))
                    {
                        upload.Thumbnail = reader.ReadBytes(thumbnail.ContentLength);
                    }
                }

                var files = selectedFiles.Select(x => new UploadedFile(x.FileName, x.ContentLength)).ToList();
                var alertFiles = _uploadManager.IsSelectedFilesValid(files);
                if (!alertFiles.Flag)
                {
                    ViewBag.Alert = alertFiles;
                    SetDropDownPostBackData(upload);
                    return View(upload);
                }
                else
                {
                    string path = upload.Drive + upload.UploadPath;
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    foreach (var file in selectedFiles)
                    {
                        try
                        {
                            string fileName = upload.Title + Path.GetExtension(file.FileName);
                            file.SaveAs(path + @"\" + fileName);
                            upload.FileInfos.Add(new Core.Models.EntityModels.FileInfo { FileName = fileName });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        
                    }
                }
                
                var alertInsert = _uploadManager.Insert(upload);
                ViewBag.Alert = alertInsert;
                if (!alertInsert.Flag)
                {
                    SetDropDownPostBackData(upload);
                    return View(upload);
                }
                SetDropDownData();
                return View();
            }

            SetDropDownPostBackData(upload);
            return View(upload);
        }

        // GET: Uploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
            return View(upload);
        }

        // POST: Uploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UploadId,Drive,Title,CategoryId,SubCategoryId,UploadPath,Thumbnail,PublishDate,LastUpdate")] Upload upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(upload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
            return View(upload);
        }

        // GET: Uploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // POST: Uploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Upload upload = db.Uploads.Find(id);
            db.Uploads.Remove(upload);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
