using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace DVUnityUtilities
{
    public static class JsonUtils
    {
        public static string ToFile<T>(T obj, string path) where T : class
        {
            string dir = new FileInfo(path).Directory.FullName;

            bool folderExists = Directory.Exists(dir);
            if (!folderExists) Directory.CreateDirectory(dir);

            string write = ToJson(obj);
            File.WriteAllText(path, write);

            return write;
        }

        public static string TextToFile(string str, string path)  
        {
            string dir = new FileInfo(path).Directory.FullName;

            bool folderExists = Directory.Exists(dir);
            if (!folderExists) Directory.CreateDirectory(dir);
             
            File.WriteAllText(path, str);

            return str;
        }

        public static T FromJsonOrNew<T>(string json) where T : class, new()
        {
            var res = new T();

            if (!string.IsNullOrEmpty(json))
            {
                JsonUtility.FromJsonOverwrite(json, res);
            }

            return res;
        }

        public static string ToJson<T>(T obj)
        {
            return JsonUtility.ToJson(obj, true);
        }

        public static async Task<T> FromFileOrNew<T>(string filePath) where T : class, new()
        {
            string json = await ReadFile(filePath);
            return FromJsonOrNew<T>(json);
        }

        public static async Task<string> ReadFile(string filePath)
        {
            var json = string.Empty;

            try
            {
                json = await File.ReadAllTextAsync(filePath);
                return json;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
                return json;
            }
        }
    }
}