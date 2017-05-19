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
    }
}
