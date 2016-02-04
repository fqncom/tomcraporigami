using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MyBookShop.ashx
{
    /// <summary>
    /// UpLoadFile 的摘要说明
    /// </summary>
    public class UpLoadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string transCode = context.Request["TransCode"];
            if (transCode == null)
            {
                return;
            }
            switch (transCode)
            {
                case "UpLoadImage"://上传图片
                    HttpPostedFile file = context.Request.Files["Filedata"];
                    if (file != null)
                    {
                        UpLoadImage(file);
                    }
                    break;
                case "GetSmallPic":
                    string filePath = context.Request["pic"] ?? "";
                    int height = Convert.ToInt32(context.Request["height"] ?? "-1");
                    int width = Convert.ToInt32(context.Request["width"] ?? "-1");
                    int x = Convert.ToInt32(context.Request["x"] ?? "-1");
                    int y = Convert.ToInt32(context.Request["y"] ?? "-1");
                    if (filePath != "")
                    {
                        GetSmallPic(filePath, height, width, x, y);
                    }
                    break;
                default:
                    break;
            }
        }

        //获取小图
        private void GetSmallPic(string filePath, int height, int width, int x, int y)
        {
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics gri = Graphics.FromImage(bitmap))
                {
                    using (Image image = Image.FromFile(HttpContext.Current.Request.MapPath(filePath)))
                    {
                        gri.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height),
                            GraphicsUnit.Pixel);
                        string[] filePaths = filePath.Split(new char[]{'.'}, StringSplitOptions.RemoveEmptyEntries);
                        filePath = string.Join("_small.", filePaths);
                        bitmap.Save(HttpContext.Current.Request.MapPath(filePath), ImageFormat.Jpeg);
                        HttpContext.Current.Response.Write(filePath);
                    }
                }
            }
        }

        //上传图片方法
        private void UpLoadImage(HttpPostedFile file)
        {
            string fileExt = Path.GetExtension(file.FileName);
            if (fileExt != ".jpg" && fileExt != ".png")
            {
                return;
            }
            string newFileName = Common.CommonTools.GetStreamMD5(file.InputStream) + fileExt;//Guid.NewGuid().ToString() + fileExt;//创建新的文件名

            DateTime nowDate = DateTime.Now;
            string dir = string.Format("/{0}/{1}/", nowDate.Year, nowDate.Month);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(HttpContext.Current.Request.MapPath(dir)));
            }
            file.SaveAs(HttpContext.Current.Request.MapPath(dir + newFileName));
            using (Image image = Image.FromStream(file.InputStream))
            {

                HttpContext.Current.Response.Write("success," + dir + newFileName + "," + image.Width + "," + image.Height);
                //var obj = new
                //{
                //    ReturnCode = "success",
                //    ImagePath = dir + newFileName,
                //    Width = image.Width,
                //    Height = image.Height
                //};
                //JavaScriptSerializer js = new JavaScriptSerializer();
                //HttpContext.Current.Response.Write(js.Serialize(obj));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}