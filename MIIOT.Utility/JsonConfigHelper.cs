using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    public class JsonConfigHelper
    {
        private JObject jObject = null;
        public string this[string key]
        {
            get
            {
                string str = "";
                if (jObject != null)
                {
                    try { str = GetValue(key); }
                    catch (Exception ex)
                    {
                        Log.Error("JsonConfigHelper.GetValue", ex);
                    }
                }
                return str;
            }
        }

        string _path = "";
        public JsonConfigHelper(string path)
        {
            _path = path;
            jObject = new JObject();
            try
            {
                using (System.IO.StreamReader file = System.IO.File.OpenText(path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        jObject = JObject.Load(reader);
                    }
                };
            }
            catch (Exception ex)
            {
                Log.Error("JsonConfigHelper", ex);
            }
        }

        public void Update(string key, string value)
        {
            try
            {

                if (File.Exists(_path))
                {
                    string jsonString = File.ReadAllText(_path, Encoding.UTF8);//读取文件
                    JObject jobject = JObject.Parse(jsonString);//解析成json
                    jobject[key] = value;//替换需要的文件
                    string convertString = Convert.ToString(jobject);//将json装换为string
                    File.WriteAllText(_path, convertString);//将内容写进jon文件中

                    using (System.IO.StreamReader file = System.IO.File.OpenText(_path))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            jObject = JObject.Load(reader);
                        }
                    };

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Log.Error("Update", ex);
            }
        }

        public void SetValue(string key, string value)
        {
            jObject[key] = value;
            WriteTestFlagFile(_path);
        }
        public void WriteTestFlagFile(string LogPath)
        {
            try
            {
                using (FileStream aFile = new FileStream(LogPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(aFile))
                    {
                        sw.Write(JsonConvert.SerializeObject(jObject));
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public T GetValue<T>(string key) where T : class
        {
            return JsonConvert.DeserializeObject<T>(jObject.SelectToken(key).ToString());
        }
        public string GetValue(string key)
        {
            string vstr = jObject[key] == null ? "" : jObject[key].ToString();
            return vstr;//Regex.Replace(vstr, @"\s", "");
        }
    }

    public class JsonConfigUtil
    {

        // private JObject jObject = null;
        private static object obj = new object();
        private static JsonConfigUtil _ins;

        public static JsonConfigUtil Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new JsonConfigUtil();
                }
                return _ins;
            }
        }
        public JsonConfigUtil()
        {

        }
        string _path = Environment.CurrentDirectory + "\\Config\\config.json";

        public void Update(string key, string value)
        {
            if (File.Exists(_path))
            {
                string jsonString = File.ReadAllText(_path, Encoding.UTF8);//读取文件
                JObject jobject = JObject.Parse(jsonString);//解析成json
                jobject[key] = value;//替换需要的文件
                string convertString = Convert.ToString(jobject);//将json装换为string
                File.WriteAllText(_path, convertString);//将内容写进jon文件中
            }
            else
            {

            }
        }


        public T GetValue<T>(string key) where T : class
        {
            JObject jObject = new JObject();
            try
            {
                using (System.IO.StreamReader file = System.IO.File.OpenText(_path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        jObject = JObject.Load(reader);
                    }
                };
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return JsonConvert.DeserializeObject<T>(jObject.SelectToken(key).ToString());
        }
        public string GetValue(string key)
        {
            JObject jObject = new JObject();
            try
            {
                using (System.IO.StreamReader file = System.IO.File.OpenText(_path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        jObject = JObject.Load(reader);
                    }
                };
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            string vstr = jObject[key] == null ? "" : jObject[key].ToString();
            return vstr;//Regex.Replace(vstr, @"\s", "");
        }
    }


}
