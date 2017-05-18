using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Context;
using Core.Models.EntityModels;

namespace Core.DAL
{
    class CategoryGateway
    {

        public bool Insert(Category category)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.Categories.Add(category);
                return db.SaveChanges() > 0;
            }
        }

        public bool Edit(Category category)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.Categories.Attach(category);
                db.Entry(category).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }


        public Category GetById(int categoryId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Categories.Find(categoryId);
            }
        }

        public List<Category> GetAll()
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.Categories.ToList();
            }
        }

        public List<Category> Search(Category categorySearchCriteria)
        {
            using (BrothersContext db = new BrothersContext())
            {
                var categories = db.Categories.AsQueryable();
                if (!string.IsNullOrEmpty(categorySearchCriteria.CategoryName))
                {
                    categories = categories.Where(c => c.CategoryName.Contains(categorySearchCriteria.CategoryName));
                }
                return categories.ToList();
            }
        }

        public bool Delete(Category category)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.Categories.Attach(category);
                db.Entry(category).State = EntityState.Deleted;
                return db.SaveChanges() > 0;
            }
        }
    }
}
