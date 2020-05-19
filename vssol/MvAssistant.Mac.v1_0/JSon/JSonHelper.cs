﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using System.Reflection;

namespace MvAssistant.Mac.v1_0.JSon
{
   public  class JSonHelper
    {
        /// <summary>將物件存成 JSON 檔</summary>
        /// <param name="inst">要存成 Json 的物件</param>
        /// <param name="path">路徑名稱</param>
        /// <param name="fileName">Json 檔案名稱(含路徑)</param>
        public static void SaveInstanceToJsonFile(object inst,string path,string fileName)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(inst);
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default);
            sw.Write(json);
            sw.Flush();
            sw.Close();
        } 

        /// <summary>讀出Json 檔資料轉作成 物件</summary>
        /// <typeparam name="TInstance">轉出物件型態</typeparam>
        /// <param name="fileName">JSon 檔案名稱 </param>
        /// <returns></returns>
        public static TInstance GetInstanceFromJsonFile<TInstance>(string fileName)
        {
            StreamReader sr = new StreamReader(fileName, Encoding.Default);
            var json = sr.ReadToEnd();
            sr.Close();
            TInstance rtnV = Newtonsoft.Json.JsonConvert.DeserializeObject<TInstance>(json);
            return rtnV;
        }
    }

    public class PositionInfo
    {

        public string PositionID { get; set; }
        public HalRobotMotion Position { get; set; }
        public static string GetNewInstID()
        {
            DateTime thisTime = DateTime.Now;
            var rtnV = thisTime.ToString("yyyyMMddHHmmssfff");
            return rtnV;
        }
        public override string ToString()
        {
            string text = PositionID + " | ";
            PropertyInfo[] properties = typeof(HalRobotMotion).GetProperties();
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    text = text + property.Name + ": " + property.GetValue(this.Position).ToString() + ", ";
                }
            }
            return text;
        }
    }
}
