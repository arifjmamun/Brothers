using System;
using System.Collections.Generic;
using System.Data;
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

        private void SetDropDownData(Upload upload)
        {
            ViewBag.Drives = new SelectList(_uploadManager.GetDrives(), "Name", "VolumeLabel", upload.Drive);
            ViewBag.Category = new SelectList(_categoryManager.GetAll(), "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategory = new SelectList(_subCategoryManager.GetAll(), "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
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
            Upload upload = _uploadManager.GetById((int)id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // GET: Uploads/Create
        public ActionResult Create()
        {
            ViewBag.Drives = new SelectList(_uploadManager.GetDrives(), "Name", "VolumeLabel");
            ViewBag.Category = new SelectList(_categoryManager.GetAll(), "CategoryId", "CategoryName");
            ViewBag.SubCategory = new SelectList(_subCategoryManager.GetAll(), "SubCategoryId", "SubCategoryName");
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UploadId,Drive,Title,CategoryId,SubCategoryId,UploadPath,Thumbnail,PublishDate,LastUpdate")] Upload upload, IEnumerable<HttpPostedFileBase> selectedFiles)
        {

            HttpPostedFileBase thumbnail = Request.Files["thumbImage"];


            if (ModelState.IsValid)
            {
                if (thumbnail != null)
                {
                    var image = new Image(thumbnail.FileName, thumbnail.ContentLength);
                    var alert = _uploadManager.IsImageValid(image);
                    if (!alert.Flag)
                    {
                        ViewBag.Alert = alert;
                        SetDropDownData(upload);
                        return View(upload);
                    }
                }
                var files = Request.Files["SelectedFiles"];
                var uploadPath = db.Categories.Where(x => x.CategoryId == upload.CategoryId).Select(x => x.CategoryName).FirstOrDefault();
                db.Uploads.Add(upload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SetDropDownData(upload);
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
