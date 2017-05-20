using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;
using Core.Models.EntityModels;

namespace Core.BLL
{
    public class FileInfoManager
    {
        private readonly FileInfoGateway _fileInfoGateway = new FileInfoGateway();
        private readonly UploadGateway _uploadGateway = new UploadGateway();

        public FileInfo GetById(int fileId)
        {
            return _fileInfoGateway.GetById(fileId);
        }

        public bool DeleteFile(int fileId)
        {
            var fileInfo = GetById(fileId);
            return _fileInfoGateway.DeleteFile(fileInfo);
        }

        public string GetFileName(int fileId)
        {
            return _fileInfoGateway.GetFileName(fileId);
        }

        public string GetDriveName(int fileId)
        {
            int uploadId = GetUploadId(fileId);
            return _uploadGateway.GetDriveName(uploadId);
        }

        public int GetUploadId(int fileId)
        {
            return _fileInfoGateway.GetUploadId(fileId);
        }

        public string GetUploadPath(int fileId)
        {
            int uploadId = GetUploadId(fileId);
            return _uploadGateway.GetFilePath(uploadId);
        }
    }
}
