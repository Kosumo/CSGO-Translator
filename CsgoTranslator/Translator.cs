﻿// Copyright (c) 2015 Ravi Bhavnani
// License: Code Project Open License
// http://www.codeproject.com/info/cpol10.aspx

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CsgoTranslator
{
    /// <summary>
    /// Translates text using Google's online language tools.
    /// </summary>
    public static class Translator
    {
        public static string Translate(string sourceText)
        {
            //Downloading translation

            string url = string.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={0}&dt=t&q={1}",Properties.Settings.Default.Lang,HttpUtility.UrlEncode (sourceText));
            string outputFile = Path.GetTempFileName();

            //sometimes will throw an exeption when too many requests are made
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");
                    wc.DownloadFile(url, outputFile);
                }
                string text = File.ReadAllText(outputFile).Substring(4).Split('"')[0];
                return text;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
