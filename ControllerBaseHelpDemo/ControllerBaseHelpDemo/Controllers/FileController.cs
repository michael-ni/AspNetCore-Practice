﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ControllerBaseHelpDemo.Controllers
{
    [Route("File/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Provide IWebHostEnvironment Instance.
        /// </summary>
        /// <param name="environment"></param>
        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Return byte[] data.
        /// 回傳 byte[] 資料。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile1()
        {
            var fileContents = GetfileContents();
            return File(fileContents, "application/pdf");
        }

        /// <summary>
        /// Return byte[] data and enable range processing.
        /// 回傳 byte[] 資料與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile2()
        {
            var fileContents = GetfileContents();
            return File(fileContents, "application/pdf", true);
        }

        /// <summary>
        /// Return byte[] data and set download name.
        /// 回傳 byte[] 資料，並設定下載檔案名稱。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile3()
        {
            var fileContents = GetfileContents();
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf");
        }

        /// <summary>
        /// Return byte[] data and set download name and enable range processing.
        /// 回傳 byte[] 資料，並設定下載檔案名稱，與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile4()
        {
            var fileContents = GetfileContents();
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf", true);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information.
        /// 回傳 byte[] 資料，並設定 "Last-Modified" 和 "ETag" 標頭資訊。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile5()
        {
            var fileContents = GetFileContentsWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", lastModified, entityTag);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information and enable range processing.
        /// 回傳 byte[] 資料，並設定 "Last-Modified" 和 "ETag" 標頭資訊，與啟用部份請求的處理(partial requests)。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile6()
        {
            var fileContents = GetFileContentsWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", lastModified, entityTag, true);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information and set download name.
        /// 回傳 byte[] 資料，並設定 "Last-Modified" 和 "ETag" 標頭資訊，並設定下載檔案名稱。
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile7()
        {
            var fileContents = GetFileContentsWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf", lastModified, entityTag);
        }

        /// <summary>
        /// Return byte[] data and set "Last-Modified" and "ETag" header information and set download name and enable range processing.
        /// 回傳 byte[] 資料，並設定 "Last-Modified" 和 "ETag" 標頭資訊，並設定下載檔案名稱，與啟用部份請求的處理(partial requests)。s
        /// </summary>
        /// <returns>FileContentResult</returns>
        public IActionResult DemoFile8()
        {
            var fileContents = GetFileContentsWithEtag(out var wwwroot, out var lastModified, out var entityTag);
            return File(fileContents, "application/pdf", "ASPNetCoreNo1.pdf", lastModified, entityTag, true);
        }

        private byte[] GetfileContents()
        {
            var wwwroot = _environment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/files/Sample.pdf");
            return fileContents;
        }

        private byte[] GetFileContentsWithEtag(out string wwwroot, out DateTimeOffset lastModified, out EntityTagHeaderValue entityTag)
        {
            wwwroot = _environment.WebRootPath;
            var fileContents = System.IO.File.ReadAllBytes($"{wwwroot}/files/Sample.pdf");
            lastModified = DateTimeOffset.Parse("2020/02/07 14:21:13 PM");
            entityTag = new EntityTagHeaderValue("\"Etag\"");
            return fileContents;
        }

        /// <summary>
        /// VirtualFileResult it will look for the "wwwroot" directory.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile98()
        {
            // Don't need use IWebHostEnvironment Instance.
            return File("files\\TaiwanNo1.txt", "application/octet-stream");
        }

        /// <summary>
        /// VirtualFileResult it will look for the "wwwroot" directory and set download name.
        /// VirtualFileResult 會到 wwwroot 目錄尋找檔案，並設定下載檔案名稱。
        /// </summary>
        /// <returns>VirtualFileResult</returns>
        public IActionResult DemoFile99()
        {
            return File("files\\TaiwanNo1.txt", "application/octet-stream", "SkilltreeNo1.txt");
        }
    }
}