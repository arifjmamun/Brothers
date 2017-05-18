using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Context;
using Core.Models.EntityModels;

namespace Web.Controllers
{
    public class UploadsController : Controller
    {
        private BrothersContext db = new BrothersContext();

        // GET: Uploads
        public ActionResult Index()
        {
            var uploads = db.Uploads.Include(u => u.Category).Include(u => u.SubCategory);
            return View(uploads.ToList());
        }

        // GET: Uploads/Details/5
        public ActionResult Details(int? id)
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

        // GET: Uploads/Create
        public ActionResult Create()
        {
            var drives = DriveInfo.GetDrives().Where(x => x.IsReady && x.DriveType.ToString() == "Fixed").Select(x => new
            {
                VolumeLabel = !string.IsNullOrEmpty(x.VolumeLabel) ? x.VolumeLabel : x.Name, x.Name
            }).ToList();
            ViewBag.Drives = new SelectList(drives, "Name", "VolumeLabel");
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.SubCategory = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName");
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UploadId,Drive,Title,CategoryId,SubCategoryId,UploadPath,Thumbnail,PublishDate,LastUpdate")] Upload upload)
        {
            if (ModelState.IsValid)
            {
                var files = Request.Files["SelectedFiles"];
                var uploadPath = db.Categories.Where(x => x.CategoryId == upload.CategoryId).Select(x => x.CategoryName).FirstOrDefault();
                db.Uploads.Add(upload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var drives = DriveInfo.GetDrives().Where(x => x.IsReady && x.DriveType.ToString() == "Fixed")
                        .Select(x => new { VolumeLabel = !string.IsNullOrEmpty(x.VolumeLabel) ? x.VolumeLabel : x.Name, x.Name }).ToList();
            ViewBag.Drives = new SelectList(drives, "Name", "VolumeLabel", upload.Drive);
            ViewBag.Category = new SelectList(db.Categories, "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategory = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
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
