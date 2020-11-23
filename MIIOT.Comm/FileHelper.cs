using MIIOT.Comm.Ex;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Comm
{
    public static class FileHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ByteSizeToString(long size)
        {
            if (size <= 0)
                return "0";
            if (size < 1024)
                return $"{size}Bytes";
            if (size < 1024 * 1024)
                return $"{(((double)size) / 1024d):0.0}KB";
            if (size < 1024 * 1024 * 1024)
                return $"{(((double)size) / 1024d / 1024d):0.0}MB";
            if (size < 1024L * 1024L * 1024L * 1024L)
                return $"{(((double)size) / 1024d / 1024d / 1024d):0.0}GB";

            return $"{size}Bytes";
        }



        public static void SaveJsonFile<T>(string path, T data)
        {
            var json = data.ToJsonString();
            File.WriteAllText(path, json);
        }

        public static T LoadJsonFile<T>(string path)
        {
            var json = File.ReadAllText(path);
            return json.ToJsonObject<T>();
        }
    }
}
