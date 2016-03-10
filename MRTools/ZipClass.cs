using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace MRTools
{
    public class ZipClass
    {
        private List<string> files;
        private List<string> paths;

        public ZipClass()
        {
            files = new List<string>();
            paths = new List<string>();
        }

        public void ZipFile(string fileToZip, string zipedFile, int compressionLevel, int blockSize)
        {
            if (!System.IO.File.Exists(fileToZip))
            {
                throw new FileNotFoundException("The specified file " + fileToZip + " could not be found. Zipping aborderd");
            }

            FileStream streamToZip = new FileStream(fileToZip, FileMode.Open, FileAccess.Read);
            FileStream zipFile = File.Create(zipedFile);
            ZipOutputStream zipStream = new ZipOutputStream(zipFile);
            ZipEntry zipEntry = new ZipEntry(fileToZip);
            zipStream.PutNextEntry(zipEntry);
            zipStream.SetLevel(compressionLevel);
            byte[] buffer = new byte[blockSize];
            int size = streamToZip.Read(buffer, 0, buffer.Length);
            zipStream.Write(buffer, 0, size);

            try
            {
                while (size < streamToZip.Length)
                {
                    int sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
            }
            catch (Exception ex)
            {
                GC.Collect();
                throw ex;
            }

            zipStream.Finish();
            zipStream.Close();
            streamToZip.Close();
            GC.Collect();
        }

        public void ZipFileFromDirectory(string rootPath, string destinationPath, int compressLevel)
        {
            GetAllDirectories(rootPath);


            string rootMark = rootPath + "\\";
            Crc32 crc = new Crc32();
            ZipOutputStream outPutStream = new ZipOutputStream(File.Create(destinationPath));
            outPutStream.SetLevel(compressLevel); // 0 - store only to 9 - means best compression
            foreach (string file in files)
            {
                FileStream fileStream = File.OpenRead(file);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(file.Replace(rootMark, string.Empty));
                entry.DateTime = DateTime.Now;
                entry.Size = fileStream.Length;
                fileStream.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                outPutStream.PutNextEntry(entry);
                outPutStream.Write(buffer, 0, buffer.Length);
            }

            this.files.Clear();

            foreach (string emptyPath in paths)
            {
                ZipEntry entry = new ZipEntry(emptyPath.Replace(rootMark, string.Empty) + "/");
                outPutStream.PutNextEntry(entry);
            }

            this.paths.Clear();
            outPutStream.Finish();
            outPutStream.Close();
            GC.Collect();
        }

        private void GetAllDirectories(string rootPath)
        {
            string[] subPaths = Directory.GetDirectories(rootPath);
            foreach (string path in subPaths)
            {
                GetAllDirectories(path);
            }
            string[] files = Directory.GetFiles(rootPath);
            foreach (string file in files)
            {
                this.files.Add(file);
            }
            if (subPaths.Length == files.Length && files.Length == 0)
            {
                this.paths.Add(rootPath);
            }
        }

        public List<string> UnZip(string zipfilepath, string unzippath)
        {
            List<string> unzipFiles = new List<string>();

            if (unzippath.EndsWith("\\") == false || unzippath.EndsWith(":\\") == false)
            {
                unzippath += "\\";
            }

            ZipInputStream s = new ZipInputStream(File.OpenRead(zipfilepath));
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = Path.GetDirectoryName(unzippath);
                string fileName = Path.GetFileName(theEntry.Name);

                if (!string.IsNullOrEmpty(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }

                if (fileName != String.Empty)
                {
                    //if (theEntry.CompressedSize == 0)
                    //    break;
                    directoryName = Path.GetDirectoryName(unzippath + theEntry.Name);
                    Directory.CreateDirectory(directoryName);

                    unzipFiles.Add(unzippath + theEntry.Name);

                    FileStream streamWriter = File.Create(unzippath + theEntry.Name);

                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
            GC.Collect();
            return unzipFiles;
        }
    }

}
