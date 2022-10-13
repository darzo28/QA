using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace lab2
{
    class Program
    {
        static int validCount = 0, invalidCount = 0;
        static string rootLink = /*"http://links.qatl.ru/"*/"https://www.travelline.ru/", 
            domainStr = /*"links.qatl.ru"*/"www.travelline.ru", pattern = "<a.*href=\"([^#]*?)\".*>", 
            countLinks = "\nКоличество строк: ", checkDate = "Дата проверки: " + (DateTime.Now).ToString();
        static Dictionary<string, bool> visitedLinks = new Dictionary<string, bool>();

        static void GetLinks(List<string> links, List<string> currentLinks, string content)
        {
            foreach (var link in Regex.Matches(content, pattern))
            {
                string url = Regex.Match(link.ToString(), pattern).Groups[1].Value;
                if (url.Substring(0, 7) == "http://" || url.Substring(0, 8) == "https://")
                {
                    Uri uri = new Uri(url);
                    string domain = uri.Authority;
                    if (domain != domainStr)
                    {
                        continue;
                    }
                }
                url = rootLink + url;
                if (!visitedLinks.ContainsKey(url) && !links.Contains(url) && !currentLinks.Contains(url))
                {
                    links.Add(url);
                }
            }
        }

        static List<string> SearchLinks(List<string> links, StreamWriter validFile, StreamWriter invalidFile)
        {
            List<string> newLinks = new List<string>();
            foreach (string url in links)
            {
                visitedLinks.Add(url, true);
                try
                {
                    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse resp = (HttpWebResponse)webReq.GetResponse();
                    string content = "";
                    int status = (int)resp.StatusCode;
                    validFile.WriteLine(url, " ", status, " ", resp.StatusCode);
                    using (Stream stream = resp.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            content += reader.ReadToEnd();
                        }
                    }
                    resp.Close();
                    GetLinks(newLinks, links, content);
                    validCount++;
                }
                catch (WebException error)
                {
                    HttpWebResponse response = (System.Net.HttpWebResponse)error.Response;
                    var statusCode = response.StatusCode;
                    invalidFile.WriteLine(url, " ", (int)statusCode, " ", response.StatusCode);
                    invalidCount++;
                }
            }
            return newLinks;
        }

        static void Main(string[] args)
        {
            string startUrl = rootLink;
            List<string> links = new List<string>();
            links.Add(startUrl);
            using (StreamWriter validFile = new StreamWriter("Valid.txt", false, System.Text.Encoding.Default))
            {
                using (StreamWriter invalidFile = new StreamWriter("Invalid.txt", false, System.Text.Encoding.Default))
                {
                    while (links.Count != 0)
                    {
                        links = SearchLinks(links, validFile, invalidFile);
                    }
                    validFile.WriteLine(countLinks + (validCount).ToString());
                    validFile.WriteLine(checkDate);
                    invalidFile.WriteLine(countLinks + (invalidCount).ToString());
                    invalidFile.WriteLine(checkDate);
                }
            }
        }
    }
}