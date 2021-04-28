﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using KsisLab8.Models;

namespace KsisLab8.Controllers
{
    public class FileController : Controller
    {
        // GET: FileController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Download(string path)
        {
            if (System.IO.File.Exists(path))
            {
                byte[] data = System.IO.File.ReadAllBytes(path);
                var info = new FileInfo(path);
                string fileName = info.Name;
                string fileType = MimeTypes.GetMimeType(fileName);
                return File(data, fileType, fileName);
            }

            Response.StatusCode = 404;
            return Content("Not Found");
        }

        [HttpGet]
        public JsonResult FileList(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                var fileList = Directory.GetFiles(path).Select(Path.GetFileName).ToArray();
                return new JsonResult(fileList);
            }

            return new JsonResult(null);
        }

        [HttpHead]
        //[AcceptVerbs(new[] { "GET", "HEAD" })]
        //  curl -X HEAD https://localhost:44382/file/fileinfo/files/example.txt
        public JsonResult FileInfo(string path)
        {
            //Response.Clear();
            if (System.IO.File.Exists(path))
            {
                var info = new FileInfo(path);
                var customInfo = new FileModel();
                Response.ContentLength = info.Length;
                Response.Headers.Add("File-name", info.Name);
                Response.Headers.Add("Full-name", info.FullName);
                Response.Headers.Add("Full-type", Path.GetExtension(path));
                return new JsonResult(customInfo);
            }

            return new JsonResult(null);
        }

        [HttpDelete]
        public void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
