using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Context;
using Core.Models.EntityModels;

namespace Core.DAL
{
    class SubCategoryGateway
    {
        public bool Insert(SubCategory subCategory)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.SubCategories.Add(subCategory);
                return db.SaveChanges() > 0;
            }
        }

        public bool Edit(SubCategory subCategory)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.SubCategories.Attach(subCategory);
                db.Entry(subCategory).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        public SubCategory GetById(int subCategoryId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.SubCategories.Find(subCategoryId);
            }
        }

        public List<SubCategory> GetAll()
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.SubCategories.ToList();
            }
        }

        public List<SubCategory> Search(SubCategory subCategorySearchCriteria)
        {
            using (BrothersContext db = new BrothersContext())
            {
                var subCategories = db.SubCategories.AsQueryable();
                if (!string.IsNullOrEmpty(subCategorySearchCriteria.SubCategoryName))
                {
                    subCategories = subCategories.Where(c => c.SubCategoryName.Contains(subCategorySearchCriteria.SubCategoryName));
                }
                return subCategories.ToList();
            }
        }

        public bool Delete(SubCategory subCategory)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.SubCategories.Attach(subCategory);
                db.Entry(subCategory).State = EntityState.Deleted;
                return db.SaveChanges() > 0;
            }
        }
    }
}
