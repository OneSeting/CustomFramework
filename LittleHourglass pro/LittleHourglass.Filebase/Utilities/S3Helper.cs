using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using LittleHourglass.Filebase.Policy;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace LittleHourglass.Filebase.Utilities
{
    public class S3Helper
    {
        #region Fields
        private static IAmazonS3 _amazonS3Client;
        private readonly string _objectKeyFormat = Startup.Conf["Awss3:ObjectKeyFormat"] ?? "filebase_dev/{0}";
        private readonly int _thumbSize = int.Parse(Startup.Conf["Awss3:ThumbSize"] ?? "350");
        private readonly int _defaultSize = int.Parse(Startup.Conf["Awss3:DefaultSize"] ?? "750");
        private readonly string _bucketName = Startup.Conf["Awss3:BucketName"] ?? "media.tijn.co";
        private readonly string _publicUrlFormat = Startup.Conf["Awss3:PublicUrlFormat"] ?? "https://{0}.media-nodes.tijn.co/{1}";
        private readonly string _accessKey = Startup.Conf["Awss3:AccessKey"] ?? "AKIAJZS25FPJMNLILCUQ";
        private readonly string _secretKey = Startup.Conf["Awss3:Secretkey"] ?? "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";
        #endregion

        #region Constructors
        private S3Helper()
        {
            RegionEndpoint region = RegionEndpoint.USWest2;
            _amazonS3Client = new AmazonS3Client(_accessKey, _secretKey, region);
        }
        #endregion

        #region Methods
        public static S3Helper _()
        {
            return new S3Helper();
        }

        public async Task<FilebaseResponse> UploadFileAsync(IFormFile file)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string key = string.Format(_objectKeyFormat, guid);
                string ext = Path.GetExtension(file.FileName);
                string oringinalKey = $"{key}/oringinal{ext}";
                string hash = "";
                int width = 0, height = 0;
                if (file.ContentType.Equals("image/jpeg") || file.ContentType.Equals("image/png"))
                {
                    byte[] oringinalBuffer, thumbBuffer, defaultBuffer;
                    string thumbKey = $"{key}/thumb{ext}";
                    string defaultKey = $"{key}/default{ext}";

                    using MemoryStream stream = new MemoryStream();
                    file.CopyTo(stream);

                    using (Bitmap bitmap = new Bitmap(stream))
                    {
                        width = bitmap.Width;
                        height = bitmap.Height;
                    }

                    oringinalBuffer = stream.ToArray();
                    thumbBuffer = GetThumbnail(oringinalBuffer, _thumbSize);
                    defaultBuffer = GetThumbnail(oringinalBuffer, _defaultSize);

                    using var fileTransferUtility = new TransferUtility(_amazonS3Client);
                    using (MemoryStream oringinalStream = new MemoryStream(oringinalBuffer))
                    {
                        hash = ComputeMd5Hash(oringinalStream);
                        await fileTransferUtility.UploadAsync(oringinalStream, _bucketName, oringinalKey);
                    }
                    using (MemoryStream thumbStream = new MemoryStream(thumbBuffer))
                    {
                        await fileTransferUtility.UploadAsync(thumbStream, _bucketName, thumbKey);
                    }
                    using (MemoryStream defaultStream = new MemoryStream(defaultBuffer))
                    {
                        await fileTransferUtility.UploadAsync(defaultStream, _bucketName, defaultKey);
                    }
                }
                else
                {
                    using MemoryStream stream = new MemoryStream();
                    file.CopyTo(stream);
                    string dsd = Convert.ToString(file);
                    hash = ComputeMd5Hash(stream);
                    using (var fileTransferUtility = new TransferUtility(_amazonS3Client))
                    {
                        await fileTransferUtility.UploadAsync(stream, _bucketName, oringinalKey);
                    }
                }
                return new FilebaseResponse
                {
                    FileName = file.FileName,
                    FilebaseGuid = guid,
                    Extension = ext,
                    FilebaseUrl = string.Format(_publicUrlFormat, WeekOfYear, oringinalKey),
                    MimeType = file.ContentType,
                    Size = file.Length,
                    Message = "Upload Success",
                    Width = width,
                    Height = height,
                    Hash = hash
                };

            }
            catch (Exception ex)
            {
                Logger(ex.ToString());
                return new FilebaseResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                throw;
            }
        }

        /// <summary>
        /// AWS 文件下载并压缩
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public async Task<byte[]> BatchDownload(IEnumerable<DownFileInfo> fileNames)
        {
            try
            {
                return await Task.Run(() =>
                {
                    byte[] res;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                        {
                            foreach (var item in fileNames)
                            {
                                RestClient restClient = new RestClient();
                                var request = new RestRequest(item.Url);
                                var pdfBytes = restClient.DownloadData(request);

                                ZipArchiveEntry entry = zip.CreateEntry(item.Name);////fileName.Type+@"\"+压缩文件内创建一个文件名,流是什么文件格式就用什么格式
                                if (pdfBytes != null)
                                {
                                    using (Stream sw = entry.Open())
                                    {
                                        sw.Write(pdfBytes, 0, pdfBytes.Length);
                                    }
                                }

                            }

                            InvokeWriteFile(zip);//重新计算压缩文件的大小，
                            int nowPos = (int)ms.Position;
                            res = new byte[ms.Length];
                            ms.Position = 0;
                            ms.Read(res, 0, res.Length);
                            ms.Position = nowPos;
                        }
                        return res;
                    }
                });
            }
            catch (Exception ex)
            {
                LoggerHelper.Error($"This is BatchDownload(IEnumerable<DownFileInfo> fileNames) Exception  Message:{ex.Message}");
                throw ex;
            }

        }


        public void InvokeWriteFile(ZipArchive zipArchive)
        {
            foreach (MethodInfo method in zipArchive.GetType().GetRuntimeMethods())
            {
                if (method.Name == "WriteFile")
                {
                    method.Invoke(zipArchive, new object[0]);
                }
            }
        }

        /// <summary>
        /// 获取当前周数
        /// </summary>
        private string WeekOfYear
        {
            get
            {
                GregorianCalendar gc = new GregorianCalendar();
                string weekOfYear = $"w{gc.GetWeekOfYear(DateTime.UtcNow, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString()}";
                return weekOfYear;
            }
        }

        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="info"></param>
        private void Logger(string info)
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }

            File.AppendAllLines($"Logs/{DateTime.Now.Date.ToString("yyyyMMdd")}", new string[] { info });
        }


        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="targetSize"></param>
        /// <param name="Orientation"></param>
        /// <returns></returns>
        private byte[] GetThumbnail(byte[] buffer, int targetSize, int Orientation = 0)
        {
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                if (targetSize == 0)
                {
                    return buffer;
                }

                using (Image bigImage = new Bitmap(stream))
                {
                    if (bigImage.Width < targetSize)
                    {
                        return buffer;
                    }
                    int width = bigImage.Width, height = bigImage.Height;
                    int ow = width;
                    switch (Orientation)
                    {
                        case 2:
                            bigImage.RotateFlip(RotateFlipType.RotateNoneFlipX);//horizontal flip
                            break;
                        case 3:
                            bigImage.RotateFlip(RotateFlipType.Rotate180FlipNone);//right-top
                            break;
                        case 4:
                            bigImage.RotateFlip(RotateFlipType.RotateNoneFlipY);//vertical flip
                            break;
                        case 5:
                            bigImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                            break;
                        case 6:
                            bigImage.RotateFlip(RotateFlipType.Rotate90FlipNone);//right-top
                            width = height;
                            height = ow;
                            break;
                        case 7:
                            bigImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                            break;
                        case 8:
                            bigImage.RotateFlip(RotateFlipType.Rotate270FlipNone);//left-bottom
                            width = height;
                            height = ow;
                            break;
                        default:
                            break;
                    }

                    var newSize = CalculateDimensions(bigImage.Size, targetSize);

                    using (Image smallImage = bigImage.GetThumbnailImage(newSize.Width, newSize.Height, new Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            smallImage.Save(m, ImageFormat.Jpeg);
                            return m.GetBuffer();
                        }
                    }
                }
            }

        }

        private bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>
        /// 等比例计算尺寸
        /// </summary>
        /// <param name="oldSize"></param>
        /// <param name="targetSize"></param>
        /// <param name="autoResize"></param>
        /// <returns></returns>
        private Size CalculateDimensions(Size oldSize, int targetSize, bool autoResize = false)
        {
            Size newSize = new Size();
            if (autoResize && oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private string ComputeMd5Hash(MemoryStream stream)
        {
            string hash = "";
            using (MD5 md5 = MD5.Create())
            {
                stream.Position = 0;
                foreach (var item in md5.ComputeHash(stream))
                {
                    hash += item.ToString("x2");
                }
            }
            return hash;
        }
        #endregion

        #region Utilities

        #endregion
    }

    public class FilebaseResponse
    {
        public string FileName { get; set; }

        public string Extension { get; set; }

        public string MimeType { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public string FilebaseUrl { get; set; }

        public string FilebaseGuid { get; set; }

        public long Size { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; } = true;

        public string Hash { get; set; }
    }



    public class DownFileInfo
    {
        /// <summary>
        /// 素材URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 素材名称
        /// </summary>
        public string Name { get; set; }

        ///// <summary>
        ///// 图片分类
        ///// </summary>
        //public string Type { get; set; }
    }


}
