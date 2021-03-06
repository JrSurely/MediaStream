﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        static string urlPath = string.Empty;
        string name = string.Empty;

        public HomeController()
        {
            var applicationPath = VirtualPathUtility.ToAbsolute("~") == "/" ? "" : VirtualPathUtility.ToAbsolute("~");
            urlPath = applicationPath + "/Upload";
        }
        #region 文件上传
        [HttpPost]
        public ActionResult Upload()
        {
            string fileName = Request["name"];
            string fileRelName = fileName.Substring(0, fileName.LastIndexOf('.'));//设置临时存放文件夹名称
            int index = Convert.ToInt32(Request["chunk"]);//当前分块序号
            var guid = Request["guid"];//前端传来的GUID号
            var dir = Server.MapPath("~/Upload");//文件上传目录
            dir = Path.Combine(dir, fileRelName);//临时保存分块的目录
            if (!System.IO.Directory.Exists(dir))
                System.IO.Directory.CreateDirectory(dir);
            string filePath = Path.Combine(dir, index.ToString());//分块文件名为索引名，更严谨一些可以加上是否存在的判断，防止多线程时并发冲突
            var data = Request.Files["file"];//表单中取得分块文件
            string extension = data.FileName.Substring(data.FileName.LastIndexOf(".") + 1,
                (data.FileName.Length - data.FileName.LastIndexOf(".") - 1));//获取文件扩展名
            //if (data != null)//为null可能是暂停的那一瞬间
            //{
            data.SaveAs(filePath + fileName);
            //}
            return Json(new { erron = 0 });//Demo，随便返回了个值，请勿参考
        }
        public ActionResult Merge()
        {
            var guid = Request["guid"];//GUID
            var uploadDir = Server.MapPath("~/Upload");//Upload 文件夹
            var fileName = Request["fileName"];//文件名
            string fileRelName = fileName.Substring(0, fileName.LastIndexOf('.'));
            var dir = Path.Combine(uploadDir, fileRelName);//临时文件夹          
            var files = System.IO.Directory.GetFiles(dir);//获得下面的所有文件
            var finalPath = Path.Combine(uploadDir, fileName);//最终的文件名（demo中保存的是它上传时候的文件名，实际操作肯定不能这样）
            var fs = new FileStream(finalPath, FileMode.Create);
            foreach (var part in files.OrderBy(x => x.Length).ThenBy(x => x))//排一下序，保证从0-N Write
            {
                var bytes = System.IO.File.ReadAllBytes(part);
                fs.Write(bytes, 0, bytes.Length);
                bytes = null;
                System.IO.File.Delete(part);//删除分块
            }
            fs.Flush();
            fs.Close();
            System.IO.Directory.Delete(dir);//删除文件夹
                                            //  return RedirectToAction("MediaIndex", fileName);
            return Json(1);//随便返回个值，实际中根据需要返回
        }
        #endregion

        public ActionResult UpLoadProcess(string id, string name, string type, string lastModifiedDate, int size, HttpPostedFileBase file)
        {
            string filePathName = string.Empty;

            string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, "Upload");
            if (Request.Files.Count == 0)
            {
                return Json(new { jsonrpc = 2.0, error = new { code = 102, message = "保存失败" }, id = "id" });
            }

            string ex = Path.GetExtension(file.FileName);
            filePathName = Guid.NewGuid().ToString("N") + ex;
            if (!System.IO.Directory.Exists(localPath))
            {
                System.IO.Directory.CreateDirectory(localPath);
            }
            file.SaveAs(Path.Combine(localPath, filePathName));

            return Json(new
            {
                jsonrpc = "2.0",
                id = id,
                filePath = urlPath + "/" + filePathName
            });

        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult PhotoIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            string path = @"C:\Users\zhoufugui\Desktop\test.txt";
            List<User> list = GetUserList(path);
            ViewBag.Users = list;
            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MediaIndex(string fileName = "")
        {
            //GetFileList();
            //ViewBag.fileName = name;
            return View();
        }

        public ActionResult GetFileList()
        {
            List<FileModel> fileList = new List<FileModel>();
            string path = AppDomain.CurrentDomain.BaseDirectory + @"/Upload";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                return Json(fileList, JsonRequestBehavior.AllowGet);
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            //path为某个目录，如： “D:\Program Files”
            FileInfo[] inf = dir.GetFiles();

            foreach (FileInfo finf in inf)
            {
                if (finf.Extension.Equals(".mp4") || finf.Extension.Equals(".flv"))
                {
                    //如果扩展名为“.mp4”
                    //name = finf.Name;
                    fileList.Add(new FileModel() { FileName = finf.Name, Size = GetFileSize(finf.FullName) });
                    //读取文件的完整目录和文件名
                }

            }
            return Json(fileList, JsonRequestBehavior.AllowGet);

        }

        private string GetFileSize(string sFileFullName)
        {
            FileInfo fiInput = new FileInfo(sFileFullName);
            double len = fiInput.Length;

            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            string filesize = String.Format("{0:0.##} {1}", len, sizes[order]);
            return filesize;
        }

        public static bool FileIsLargerThan1KB(string sFileFullName)
        {
            FileInfo fiInput = new FileInfo(sFileFullName);
            double len = fiInput.Length;

            len = len / 1024 / 1024;
            return len > 1;
        }

        private List<User> GetUserList(string path)
        {
            List<User> list = new List<Controllers.User>();
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                //循环读取所有行              
                while (!reader.EndOfStream)
                {
                    //将每行数据，用-分割成3段        
                    string[] data = reader.ReadLine().Replace("---", "/").Split('/');
                    //新建一行，并将读出的数据分段，分别存入3个对应的列中        
                    User user = new User();
                    user.Ip = data[0];
                    user.UserName = data[1];
                    user.Type = data[2];
                    //将这行数据加入到datatable中    
                    list.Add(user);
                }
            }
            return list;
        }

        public ActionResult SaveTxt(string strInfo)
        {
            string txtFile = AppDomain.CurrentDomain.BaseDirectory + "/test.txt";    
            List<User> list = JsonConvert.DeserializeObject<List<User>>(strInfo);
            //创建一个文件流，用以写入或者创建一个StreamWriter 
            FileStream fs = new FileStream(txtFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Flush();
            // 使用StreamWriter来往文件中写入内容 
            sw.BaseStream.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < list.Count; i++)
            {
                sw.WriteLine(list[i].Ip+"/"+list[i].UserName+"/"+list[i].Type+"\r\n");
            }           
            //关闭此文件t 
            sw.Flush();
            sw.Close();
            fs.Close();
            return Json(true);
        }

    }

    public class User
    {
        public string Ip { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
    }
}