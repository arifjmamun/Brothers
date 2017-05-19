﻿using System;
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

        public Alert Insert(Upload upload)
        {
            upload.PublishDate = DateTime.Now;
            upload.LastUpdate = DateTime.Now;
            bool isSaved = _uploadGateway.Insert(upload);
            if (isSaved) return new Alert
            {
                Flag = true,
                CssClass = Alert.SuccessClass,
                Type = Alert.SuccessText,
                Msg = "Files Uploaded!"
            };
            return new Alert
            {
                Flag = false,
                CssClass = Alert.ErrorClass,
                Type = Alert.ErrorText,
                Msg = "Unexpected Error! Try Again!"
            };
            
        }

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
                .Select(x => new { VolumeLabel = !string.IsNullOrEmpty(x.VolumeLabel) ? x.VolumeLabel : x.Name, x.Name }).ToList();
        }

        public Alert IsImageValid(UploadedFile image)
        {

            if (UploadedFile.AllowedImages.IndexOf(image.Type) == -1)
            {
                return new Alert
                {
                    Flag = false,
                    CssClass = Alert.ErrorClass,
                    Type = Alert.ErrorText,
                    Msg = image.Type + " file not allowed! Only these " + string.Join(", ", UploadedFile.AllowedImages) + " files are allowed!"
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

            return new Alert { Flag = true };
        }

        public Alert IsSelectedFilesValid(List<UploadedFile> files)
        {
            if (files.Count(x => x.Length < 1) != 0)
            {
                var emptyFiles = files.Where(x => x.Length < 1).Select(x => x.Name).ToList();
                return new Alert
                {
                    Flag = false,
                    CssClass = Alert.ErrorClass,
                    Type = Alert.ErrorText,
                    Msg = "The" + string.Join(", ", emptyFiles) + " files are empty!"
                };
            }

            if (files.Count(x => x.Length > 4294967295) != 0)
            {
                //3.99 GB = 4294967295
                var largerFiles = files.Where(x => x.Length > 4294967295).Select(x => x.Name).ToList();
                return new Alert
                {
                    Flag = false,
                    CssClass = Alert.WarningClass,
                    Type = Alert.WarningText,
                    Msg = "The" + string.Join(", ", largerFiles) + " files are larger than 3.99 GB!"
                };
            }

            if (files.Sum(x => x.Length) > 107374182400)
            {
                return new Alert
                {
                    Flag = false,
                    CssClass = Alert.ErrorClass,
                    Type = Alert.ErrorText,
                    Msg = "You can only upload at a time maximum 100GB!"
                };
            }

            return new Alert { Flag = true };
        }


        public string GetUploadPath(int categoryId, int? subCategoryId, string title)
        {
            if (subCategoryId == null) return _uploadGateway.GetUploadPath(categoryId, title);
            return _uploadGateway.GetUploadPath(categoryId, (int)subCategoryId, title);
        }
    }
}
