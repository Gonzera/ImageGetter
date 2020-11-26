using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace Downloader
{
    class Utils
    {
        public static void DownloadShit(string fileUrl, string savePath)
        {
            WebClient web = new WebClient();
            web.DownloadFile(fileUrl, savePath);
        }

        public static void LogUrl(string url)
        {
            string path = @"C:\temp\downloadedUrls.txt";
            File.AppendAllText(path, url + Environment.NewLine);
        }

        public static bool Downloaded(string url)
        {
            var AlreadyDownloaded = File.ReadAllLines(@"C:\temp\downloadedUrls.txt");
            
            if(AlreadyDownloaded.Contains(url))
            {
                return true;
            }
            else
            {
                Utils.LogUrl(url);
                return false;
            }
        }
    }

    class FiveFiveChan
    {
        public static void GetShit (string url)
        {
            string path;
            var document = new HtmlWeb().Load(url);
            var fileUrl = document.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileinfo")).Select(e => e.GetAttributeValue("href", null));
            var boardName = document.DocumentNode.Descendants("h1").FirstOrDefault().InnerText;
            var threadId = document.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("intro")).Select(e => e.GetAttributeValue("id", null)).FirstOrDefault();
            boardName = Regex.Replace(boardName, "[/*?]", "_"); //it will look like shit but this is the best that i can think about

            if(!Directory.Exists("55chan.org"))
            {
                Directory.CreateDirectory("55chan.org");
            }
            if (!Directory.Exists("55chan.org//" + boardName + "//" + threadId))
            {
                Directory.CreateDirectory("55chan.org//" + boardName + "//" + threadId);
            }

            path = "55chan.org//" + boardName + "//" + threadId + "//";


            foreach (var item in fileUrl)
            {
                var actualUrl = "https://55chan.org" + item;
                var ourPath = Directory.GetCurrentDirectory();
                var FileName = Regex.Replace(item, "[/]", "_");
                var save = path + FileName;
                if (!Utils.Downloaded(actualUrl))
                {
                    Utils.DownloadShit(actualUrl, save);
                    Console.WriteLine("GETTING - " + actualUrl);
                }
                else
                    Console.WriteLine(actualUrl + " - ALREADY DOWNLOADED SKIPPING");
            }

        }
    }

    class FourFalhas
    {
        public static void GetShit(string url)
        {
            string path;
            var document = new HtmlWeb().Load(url);
            var boardName = document.DocumentNode.Descendants("div").Where(d => d.HasClass("boardTitle")).SingleOrDefault().InnerText;
            var fileUrls = document.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileText")).Select(e => e.GetAttributeValue("href", null));
            var fileName = document.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileText")).Select(e => e.InnerText);
            var ThreadId = document.DocumentNode.Descendants("div").Where(a => a.ParentNode.HasClass("board")).Select(e => e.GetAttributeValue("id", null)).FirstOrDefault();
            


            boardName = Regex.Replace(boardName, "[/]", "_");
            if (!Directory.Exists("4chan.org"))
            {
                Directory.CreateDirectory("4chan.org");
            }
            if (!Directory.Exists("4chan.org//" + boardName + "//" + ThreadId))                                      //@todo: add a function that does all this shit on the Util class
            {
                Directory.CreateDirectory("4chan.org//" + boardName + "//" + ThreadId);                
            }

            path = "4chan.org//" + boardName + "//" + ThreadId + "//";

            foreach (var item in fileUrls.Zip(fileName, Tuple.Create))
            {
                var actualUrl = "https:" + item.Item1;
                var save = path + item.Item2;

                if (!Utils.Downloaded(actualUrl))
                { 
                    Utils.DownloadShit(actualUrl, save);
                    Console.WriteLine("GETTING - " + actualUrl);
                }
                else
                    Console.WriteLine(actualUrl + " - ALREADY DOWNLOADED SKIPPING");
            }
        }
    }

    

}
