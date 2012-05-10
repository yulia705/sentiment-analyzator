using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace Trainer
{
    class Program
    {
        static void Main(string[] args)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.Load("test.htm", Encoding.UTF8);
            var rootNode = htmlDoc.DocumentNode;
            var marksText = rootNode.SelectNodes("//span[@class='grade-label']");
            foreach(var mark in marksText)
            {
                int grade = 0;
                switch (mark.InnerText)
                {
                    case "отличная модель": 
                        grade = 5; break;
                    case "хорошая модель":
                        grade = 4; break;
                    case "обычная модель":
                        grade = 3; break;
                    case "плохая модель":
                        grade = 2; break;
                    case "ужасная модель":
                        grade = 1; break;
                    default: break;
                }
            }
            Console.ReadLine();
        }
    }
}
