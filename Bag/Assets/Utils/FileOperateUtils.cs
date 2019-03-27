
using System.IO;
using System.Text;
using UnityEngine;

namespace WYF.Utils
{
    /// <summary>
    /// 文件操作工具类
    /// </summary>
    public class FileOperateUtils: SingletonUtils<FileOperateUtils>
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="localPath">本地路径，即游戏安装路径</param>
        /// <param name="fullPath">完整路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="isOver">是否覆盖原数据</param>
        public void WriteFile(string localPath, string fullPath,string content, bool isOver = true)
        {
            string path = string.IsNullOrEmpty(fullPath) ? Application.dataPath + "/" + localPath : fullPath;
            FileInfo fi = new FileInfo(path);
            var di = fi.Directory;
            if (!di.Exists)
            {
                di.Create(); // 创建文件夹
            }
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            FileStream fs;
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            if (!isOver)
            {
                fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(path, FileMode.OpenOrCreate);
            }
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }

        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="localPath">本地路径，即游戏安装路径</param>
        /// <param name="fullPath">完整路径</param>
        /// <returns>文件内容</returns>
        public string ReadFile(string localPath, string fullPath)
        {
            string path = string.IsNullOrEmpty(fullPath) ? Application.dataPath + "/" + localPath : fullPath;
            if (!File.Exists(path))
            {
                return string.Empty;
            }

            FileStream fs = new FileStream(path, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            string str = Encoding.UTF8.GetString(bytes);
            fs.Flush();
            fs.Close();
            return str;
        }

    }
}
