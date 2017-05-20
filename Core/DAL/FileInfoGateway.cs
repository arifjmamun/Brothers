using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context;

namespace Core.DAL
{
    class FileInfoGateway
    {
        public bool DeleteFile(Models.EntityModels.FileInfo fileInfo)
        {
            using (BrothersContext db = new BrothersContext())
            {
                db.FileInfos.Attach(fileInfo);
                db.Entry(fileInfo).State = EntityState.Deleted;
                return db.SaveChanges() > 0;
            }
        }

        public Models.EntityModels.FileInfo GetById(int fileId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.FileInfos.Find(fileId);
            }
        }

        public string GetFileName(int fileId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.FileInfos.Where(x => x.FileInfoId == fileId).Select(x => x.FileName).First();
            }
        }

        public int GetUploadId(int fileId)
        {
            using (BrothersContext db = new BrothersContext())
            {
                return db.FileInfos.Where(x => x.FileInfoId == fileId).Select(x => x.UploadId).First();
            }
        }
    }
}
