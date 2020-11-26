using System;
using System.IO;
using Downloader;
using HtmlAgilityPack;

namespace ImageGetter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(@"C:\temp\downloadedUrls.txt"))    
                File.Create(@"C:\temp\downloadedUrls.txt");           //since i want to avoid writing one extra line in each downloader i will create the file here


            Console.Title = "IMAGE GETTER ALPHA 0.1";
            Console.WriteLine("owo choose the ib: ");
            Console.WriteLine("1 - 55chan");
            Console.WriteLine("2 - 4chan");
            var choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("owo give me the thread url: ");
            string url = Console.ReadLine();

            switch (choice)
            {
                case 1:
                    FiveFiveChan.GetShit(url);
                    break;
                case 2:
                    FourFalhas.GetShit(url);
                    break;
                default:
                    break;
            }

        }
    }
}
