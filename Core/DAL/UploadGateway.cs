using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;
using Core.Models.EntityModels;

namespace Core.DAL
{
    class UploadGateway
    {
        public bool Insert(Upload upload)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.Uploads.Add(upload);
                return db.SaveChanges() > 0;
            }
        }

        public List<Upload> GetAll()
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Uploads.Include(x => x.Category).Include(x => x.SubCategory).ToList();
            }
        }

        public Upload GetById(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                var upload = db.Uploads.Find(uploadId);
                upload.FileInfos.AddRange(db.FileInfos.Where(x => x.UploadId == uploadId).ToList());
                return upload;
            }
        }

        public string SetUploadPath(int categoryId, string title)
        {
            using (BrothersContext db = new BrothersContext())
            {
                string categoryName = db.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefault();
                return categoryName + @"\" + title + @"\";
            }

        }

        public string SetUploadPath(int categoryId, int subCategoryId, string title)
        {
            using (BrothersContext db = new BrothersContext())
            {
                string categoryName = db.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefault();
                string subCategoryName = db.SubCategories.Where(x => x.SubCategoryId == subCategoryId).Select(x => x.SubCategoryName).FirstOrDefault();
                return categoryName + @"\" + subCategoryName + @"\" + title + @"\";
            }
        }

        public bool IsPathExists(string directoryPath)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Uploads.FirstOrDefault(x => x.DirectoryPath.ToLower() == directoryPath.ToLower())!=null;
            }
        }

        public string GetDriveName(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Uploads.Where(x => x.UploadId == uploadId).Select(x => x.Drive).First();
            }
        }

        public string GetCategoryName(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                int categoryId =  db.Uploads.Where(x => x.UploadId == uploadId).Select(x => x.CategoryId).First();
                return db.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).First();
            }
        }

        public string GetSubCategoryName(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                int? subCategoryId = db.Uploads.Where(x => x.UploadId == uploadId).Select(x => x.SubCategoryId).FirstOrDefault();
                if (subCategoryId == null) return String.Empty;
                return db.SubCategories.Where(x => x.SubCategoryId == subCategoryId).Select(x => x.SubCategoryName).First();
            }
        }

        public string GetFilePath(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Uploads.Where(x => x.UploadId == uploadId).Select(x => x.DirectoryPath).First();
            }
        }

        public string GetUploadTitle(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Uploads.Where(x => x.UploadId == uploadId).Select(x => x.Title).First();
            }
        }

        public List<FileInfo> GetFiles(int uploadId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.FileInfos.Where(x => x.UploadId == uploadId).ToList();
            }
        }

        public bool Edit(Upload upload, Upload prevUpload)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.Uploads.Attach(prevUpload);
                //db.Entry(prevUpload).CurrentValues.SetValues(upload);
                //db.Entry(upload).State = EntityState.Modified;
                db.Entry(prevUpload).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
    }
}
