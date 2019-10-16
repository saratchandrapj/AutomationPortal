using AutomationPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomationPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reports()
        {
            //ConfigurationManager
            List<string> FName = new List<string>();
            var path = ConfigurationManager.AppSettings["sprintPath"]; //Server.MapPath("/Sprints");
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            foreach (string item2 in dirs)
            {
                FileInfo f = new FileInfo(item2);
                FName.Add(f.Name);
            }
            foreach (string item in files)
            {
                FileInfo f = new FileInfo(item);

                FName.Add(f.Name);
            }
            ViewBag.FolderList = FName.ToList();
            return View();
        }

        public ActionResult OpenSprintFolder(string sprintfolderName)
        {
            List<FilesToOpen> filestoopen = new List<FilesToOpen>();
            var path = Server.MapPath("/Sprints/" + sprintfolderName);
            string[] files = Directory.GetFiles(path);
            //string[] dirs = Directory.GetDirectories(path);
            foreach (string item2 in files)
            {
                FileInfo f = new FileInfo(item2);
                FilesToOpen file = new FilesToOpen();
                file.fileName = f.Name;
                file.path = f.FullName;
                filestoopen.Add(file);
            }
            ViewBag.FileNames = filestoopen.ToList();
            return View(filestoopen);
        }

        public void OpenFile(string fileName)
        {
            //string url = Server.MapPath(fileName);
            //string fileName = ListBox1.SelectedValue;
            ReadFile(fileName);
            //string serverName = System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"] + ":" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            //string virtualPath = "http://" + serverName + "/" + "Data" + "/" + fileName;
            //return Redirect(virtualPath);
            //return Redirect("");
            //Server.Transfer(url);
        }

        protected void ReadFile(string path)
        {
            // Get the physical Path of the file
            string filepath = path;

            // Create New instance of FileInfo class to get the properties of the file being downloaded
            FileInfo file = new FileInfo(filepath);

            // Checking if file exists
            if (file.Exists)
            {
                // Clear the content of the response
                Response.ClearContent();

                // LINE1: Add the file name and attachment, which will force the open/cance/save dialog to show, to the header
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Disposition", "inline; filename=" + file.Name);


                // Add the file size into the response header
                Response.AddHeader("Content-Length", file.Length.ToString());

                // Set the ContentType
                Response.ContentType = ReturnExtension(file.Extension.ToLower());

                // Write the file into the response (TransmitFile is for ASP.NET 2.0. In ASP.NET 1.1 you have to use WriteFile instead)
                Response.TransmitFile(file.FullName);

                // End the response
                Response.End();
            }

        }

        private string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".doc":
                    return "application/msword";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}