using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TickTick.Entity;
using TickTick.Helper;
using TickTick.Manager;

namespace TickTick.Common
{
    public class FileLimiter
    {
        private static String[] SUPPORT_SYNC_FILE_TYPES = {
            ".jpg", ".png", ".bmp", ".gif", ".jpeg", ".3gpp", ".txt", ".amr", ".mp3", ".mp4",
            ".avi", ".3gp", ".pdf", ".tiff", ".pdf", ".aac", ".ram", ".flv", ".rmvb", ".wmv",
            ".doc", ".docx", ".pages", ".odt", ".xls", ".xlsx", ".numbers", ".ods", ".ppt", ".pps",
            ".keynote", ".odp", ".m4a"
    };

        /** the max File size allow to download without wifi 0 */
        //private static long FILE_DOWNLOAD_MX_SIZE_NOT_WIFI = 200 * 1024;

        /** the max File size allow to upload without wifi 0 */
        //private static long FILE_UPLOAD_MX_SIZE_NOT_WIFI = 100 * 1024;

        private static FileLimiter _staticFileLimiter;

        public static FileLimiter StaticFileLimiter
        {
            get { return FileLimiter._staticFileLimiter; }
            set { FileLimiter._staticFileLimiter = value; }
        }
        //private ConnectivityManager _connManager;

        //public ConnectivityManager ConnManager
        //{
        //    get { return _connManager; }
        //    set { _connManager = value; }
        //}
        private TickTickAccountManager _accountManager;

        public TickTickAccountManager AccountManager
        {
            get { return _accountManager; }
            set { _accountManager = value; }
        }
        public FileLimiter()
        {
            if (StaticFileLimiter != null)
            {
                lock ("create")
                {
                    if (StaticFileLimiter != null)
                    {
                        StaticFileLimiter = new FileLimiter();
                    }
                }
            }
        }
        public static async Task<bool> IsOverSyncFileSize(long? fileSize)
        {
            return fileSize >= await new FileLimiter().GetSyncFileSizeByUser();
        }
        private async Task<long> GetSyncFileSizeByUser()
        {
            long size = (await LimitHelper.StaticLimitHelper.GetLimits(AccountManager.CurrentUser.IsPro())).FileSizeLimit;
            return size > 0 ? size : Limits.DEFAULT_FILE_SIZE_LIMIT_PRO;
        }
    }
}
