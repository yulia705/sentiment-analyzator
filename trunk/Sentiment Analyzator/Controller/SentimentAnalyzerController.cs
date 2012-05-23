using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trainer;
using System.IO;

namespace Sentiment_Analyzator.Controller
{
    internal sealed class SentimentAnalyzerController
    {
        public int GetTextForMark(string text)
        {
            Stemmer stemmer = new Stemmer();
            List<string> stemmedWords = new List<string>();
            var words = text.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i< words.Count();i++)
            {
                words[i] = stemmer.Stem(words[i]);
            }
            stemmedWords = words.ToList<string>();
            StreamReader reader = new StreamReader("correlations.txt");
            Dictionary<string,double> correlationTable = new Dictionary<string,double>();
            string line;
            while((line = reader.ReadLine()) !=null)
            {
                double correlation;
                string[] splittedLine = line.Split(' ');
                if (splittedLine.Count() == 2)
                {
                    if (Double.TryParse(splittedLine[1], out correlation) && splittedLine[0] != "")
                    {
                        correlationTable.Add(splittedLine[0], correlation);
                    }
                }
                
            }
            double logOfP = 0;
            foreach (var word in stemmedWords)
            {
                if (correlationTable.ContainsKey(word))
                {
                    logOfP += correlationTable[word];
                }
            }
            double pFraction = Math.Exp(logOfP) ;
            double result = pFraction/(pFraction + 1);
            if (result > 0.7)
            {
                return 1;
            }
            return 0;
        }
    }
}
