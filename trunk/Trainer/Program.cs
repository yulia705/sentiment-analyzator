using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HtmlAgilityPack;
using System.Xml;

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
            List<int> grades = new List<int>();
            // Get marks list
            foreach(var mark in marksText)
            {
                switch (mark.InnerText)
                {
                    case "отличная модель": 
                        grades.Add(5); break;
                    case "хорошая модель":
                        grades.Add(4); break;
                    case "обычная модель":
                        grades.Add(3); break;
                    case "плохая модель":
                        grades.Add(2); break;
                    case "ужасная модель":
                        grades.Add(1); break;
                    default: break;
                }
            }
            //Get texts for marks
            List<string> advantages = new List<string>();
            List<string> disadvantages = new List<string>();
            List<string> comments = new List<string>();
            var texts = rootNode.SelectNodes("//div[@class='data']");
            foreach(var text in texts)
            {
                
                if (text.ChildNodes[2].Name == "div")
                {
                    //Достоинства
                    advantages.Add(text.ChildNodes[3].InnerText);
                    //Недостатки
                    disadvantages.Add(text.ChildNodes[4].InnerText);
                    //Комментарий
                    comments.Add(text.ChildNodes[5].InnerText);
                }
                else
                {
                    //Достоинства
                    advantages.Add(text.ChildNodes[2].InnerText);
                    //Недостатки
                    disadvantages.Add(text.ChildNodes[3].InnerText);
                    //Комментарий
                    comments.Add(text.ChildNodes[4].InnerText);
                }
            }
            //Generating XML
            XmlDocument xml = new XmlDocument();
            var xmlNode = xml.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xml.AppendChild(xmlNode);
            var xmlElem = xml.CreateElement("", "review", "");
            //var xmlText = xml.CreateTextNode("test!");
            //xmlElem.AppendChild(xmlText);
            xml.AppendChild(xmlElem);
            for (int i = 0; i < advantages.Count; i++)
            {
                var xmlAdvantages = xml.CreateElement("", "advantages", "");
                var xmlAdvatagesText = xml.CreateTextNode(advantages[i]);
                xmlAdvantages.AppendChild(xmlAdvatagesText);
                xml.LastChild.AppendChild(xmlAdvantages);

                var xmlDisadvantages = xml.CreateElement("", "disadvantages", "");
                var xmlDisadvantagesText = xml.CreateTextNode(disadvantages[i]);
                xmlDisadvantages.AppendChild(xmlDisadvantagesText);
                xml.LastChild.AppendChild(xmlDisadvantages);

                var xmlComments = xml.CreateElement("", "advantages", "");
                var xmlCommentsText = xml.CreateTextNode(comments[i]);
                xmlComments.AppendChild(xmlCommentsText);
                xml.LastChild.AppendChild(xmlComments);

                var xmlGrade = xml.CreateElement("","grade","");
                var xmlGradeText = xml.CreateTextNode(grades[i].ToString());
                xmlGrade.AppendChild(xmlGradeText);
                xml.LastChild.AppendChild(xmlGrade);
                
            }
            xml.Save("c:\\dsds.xml");

                Console.ReadLine();
        }
    }
}
