using System.Collections.Generic;
using Core.DAL;
using Core.Models.EntityModels;

namespace Core.BLL
{
    public class SubCategoryManager
    {
        private readonly SubCategoryGateway _subCategoryGateway = new SubCategoryGateway();

        public bool Insert(SubCategory subCategory)
        {
            return _subCategoryGateway.Insert(subCategory);
        }

        public bool Edit(SubCategory subCategory)
        {
            return _subCategoryGateway.Edit(subCategory);
        }

        public bool Delete(int subCategoryId)
        {
            SubCategory subCategory = GetById(subCategoryId);
            return _subCategoryGateway.Delete(subCategory);
        }

        public SubCategory GetById(int subCategoryId)
        {
            return _subCategoryGateway.GetById(subCategoryId);
        }

        public List<SubCategory> GetAll()
        {
            return _subCategoryGateway.GetAll();
        }

        public List<SubCategory> Search(SubCategory subCategorySearchCriteria)
        {
            return _subCategoryGateway.Search(subCategorySearchCriteria);
        }
    }
}
