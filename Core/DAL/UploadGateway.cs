using System;
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
                return db.Uploads.Find(uploadId);
            }
        }

        public string GetUploadPath(int categoryId, string title)
        {
            using (BrothersContext db = new BrothersContext())
            {
                string categoryName = db.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefault();
                return categoryName + @"\" + title + @"\";
            }

        }

        public string GetUploadPath(int categoryId, int subCategoryId, string title)
        {
            using (BrothersContext db = new BrothersContext())
            {
                string categoryName = db.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.CategoryName).FirstOrDefault();
                string subCategoryName = db.SubCategories.Where(x => x.SubCategoryId == subCategoryId).Select(x => x.SubCategoryName).FirstOrDefault();
                return categoryName + @"\" + subCategoryName + @"\" + title + @"\";
            }
        }
    }
}
