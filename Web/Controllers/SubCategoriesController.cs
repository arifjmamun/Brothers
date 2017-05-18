using System.Net;
using System.Web.Mvc;
using Core.BLL;
using Core.Models.EntityModels;

namespace Web.Controllers
{
    public class SubCategoriesController : Controller
    {

        private readonly SubCategoryManager _categoryManager = new SubCategoryManager();

        // GET: SubCategories
        public ActionResult Index()
        {
            return View(_categoryManager.GetAll());
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = _categoryManager.GetById((int) id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoryId,SubCategoryName")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                _categoryManager.Insert(subCategory);
                return RedirectToAction("Index");
            }

            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = _categoryManager.GetById((int) id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoryId,SubCategoryName")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                _categoryManager.Edit(subCategory);
                return RedirectToAction("Index");
            }
            return View(subCategory);
        }

        // GET: SubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = _categoryManager.GetById((int) id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryManager.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
