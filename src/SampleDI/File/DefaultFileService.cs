using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleDI.Helper;

namespace SampleDI.File
{
    public class DefaultFileService : IFileService
    {
        public bool FileExists(string fileFullPath)
        {
            var fileInfo = new FileInfo(fileFullPath);
            return fileInfo.Exists;
        }

        public long LastModifiedDateUnix(string fileFullPath)
        {
            var fileInfo = new FileInfo(fileFullPath);
            if (!fileInfo.Exists)
            {
                return -1;
            }

            return fileInfo.LastWriteTimeUtc.ToUnix();
        }

        public string RetriveContentsAsBase64String(string fileFullPath)
        {
            var contents = this.ReadContentsOfFile(fileFullPath);
            if (contents == null)
            {
                return string.Empty;
            }

            return Convert.ToBase64String(contents);
        }

        public byte[] ReadContentsOfFile(string fileFullPath)
        {
            if (!this.FileExists(fileFullPath))
            {
                return null;
            }

            return System.IO.File.ReadAllBytes(fileFullPath);
        }

        public string GetFileName(string fileFullPath)
        {
            var fileInfo = new FileInfo(fileFullPath);
            return fileInfo.Name;
        }

        public bool SaveFileContents(string fileFullPath, byte[] contents)
        {
            try
            {
                System.IO.File.WriteAllBytes(fileFullPath, contents);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveFileContents(string fileFullPath, string base64Contents)
        {
            return this.SaveFileContents(fileFullPath, Convert.FromBase64String(base64Contents));
        }

        public string GetFileExtension(string fileName)
        {
            var fileInfo = new FileInfo(fileName);
            return fileInfo.Extension;
        }

        public bool DeleteFile(string fileFullPath)
        {
            if (this.FileExists(fileFullPath))
            {
                System.IO.File.Delete(fileFullPath);
            }

            return true;
        }
    }
}
