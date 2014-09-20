using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.File
{
    public interface IFileService
    {
        bool FileExists(string fileFullPath);
        long LastModifiedDateUnix(string fileFullPath);
        string RetriveContentsAsBase64String(string fileFullPath);
        byte[] ReadContentsOfFile(string fileFullPath);
        string GetFileName(string fileFullPath);
        bool SaveFileContents(string fileFullPath, byte[] contents);
        bool SaveFileContents(string fileFullPath, string base64Contents);
        string GetFileExtension(string fileName);
        bool DeleteFile(string fileFullPath);
    }
}
