using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using Core.Models.EntityModels;

namespace Core.BLL
{
    public class CategoryManager
    {
        private readonly CategoryGateway _categoryGateway = new CategoryGateway();

        public bool Insert(Category category)
        {
            return _categoryGateway.Insert(category);
        }

        public bool Edit(Category category)
        {
            return _categoryGateway.Edit(category);
        }

        public bool Delete(int categoryId)
        {
            Category category = GetById(categoryId);
            return _categoryGateway.Delete(category);
        }

        public Category GetById(int categoryId)
        {
            return _categoryGateway.GetById(categoryId);
        }

        public List<Category> GetAll()
        {
            return _categoryGateway.GetAll();
        }

        public List<Category> Search(Category categorySearchCriteria)
        {
            return _categoryGateway.Search(categorySearchCriteria);
        }
    }
}
