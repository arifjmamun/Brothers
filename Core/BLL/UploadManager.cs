using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.DAL;
using Core.Helper;
using Core.Models.EntityModels;

namespace Core.BLL
{
    public class UploadManager
    {
        private readonly UploadGateway _uploadGateway = new UploadGateway();

        public List<Upload> GetAll()
        {
            return _uploadGateway.GetAll();
        }

        public Upload GetById(int uploadId)
        {
            return _uploadGateway.GetById(uploadId);
        }

        public IEnumerable GetDrives()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.IsReady && x.DriveType.ToString() == "Fixed")
                .Select(x => new { VolumeLabel = !string.IsNullOrEmpty(x.VolumeLabel) ? x.VolumeLabel : x.Name, x.Name}).ToList();
        }



        public Alert IsImageValid(Image image)
        {

            if (Image.AllowedItems.IndexOf(image.Type) == -1)
            {
                return new Alert
                {
                    Flag = false,
                    CssClass = Alert.ErrorClass,
                    Type = Alert.ErrorText,
                    Msg = image.Type + " file not allowed! Only these " + string.Join(", ", Image.AllowedItems) + " files are allowed!"
                };
            }

            // 4194304 Bytes = 4MB; [4 x 1024 x 1024]
            if (image.Length > 4194304)
            {
                return new Alert
                {
                    Flag = false,
                    CssClass = Alert.ErrorClass,
                    Type = Alert.ErrorText,
                    Msg = "File size limit exceeds! Maximum file size: 4MB"
                };
            }

            return new Alert{Flag = true};
        }
    }
}
