using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace TGbot
{
    class SaveMessages
    {
        private string[] newInfo = new string[3];

        private string txtPath = AppDomain.CurrentDomain.BaseDirectory + "messages.txt";
        private string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "UserInfo.xml";

        private XmlDocument xmlDoc = new XmlDocument();
        
        public void save(string message, string name)
        {
            using (FileStream file = new FileStream(txtPath, FileMode.Append))
                using(StreamWriter writer = new StreamWriter(file))
                writer.Write(message + "  ||  " + name + "\n");
        }

        public void addUser(string id, string word, string shifredW)
        {
            xmlDoc.Load(xmlPath);
            XmlElement element = xmlDoc.DocumentElement;

            XmlElement user = xmlDoc.CreateElement("user");

            XmlElement chatId = xmlDoc.CreateElement ("chatId");
            XmlElement Word = xmlDoc.CreateElement("word");
            XmlElement shifr = xmlDoc.CreateElement("shifr");
            XmlElement tries = xmlDoc.CreateElement("try");
            XmlElement wins = xmlDoc.CreateElement("wins");
            XmlElement loses = xmlDoc.CreateElement("loses");

            chatId.InnerText = id;
            Word.InnerText = word;
            shifr.InnerText = shifredW;
            tries.InnerText = "0";
            wins.InnerText = "0";
            loses.InnerText = "0";

            user.AppendChild(chatId);
            user.AppendChild(Word);
            user.AppendChild(shifr);
            user.AppendChild(tries);
            user.AppendChild(wins); 
            user.AppendChild(loses);

            element.AppendChild(user);
            xmlDoc.Save(xmlPath);
        }

        public void reNewUser(string id, string word, string shifredW)
        {
            xmlDoc.Load(xmlPath);
            XmlElement element = xmlDoc.DocumentElement;

            XmlNode Word = element.SelectSingleNode($"user[chatId = '{id}']/word");
            XmlNode shifr = element.SelectSingleNode($"user[chatId = '{id}']/shifr");
            XmlNode tries = element.SelectSingleNode($"user[chatId = '{id}']/try");

            Word.InnerText = word;
            shifr.InnerText = shifredW;
            tries.InnerText = "0";

            xmlDoc.Save(xmlPath);
        }

        public bool findUser(string id)
        {
            xmlDoc.Load(xmlPath);
            XmlElement element = xmlDoc.DocumentElement;
            foreach(XmlNode node in element) 
                foreach(XmlNode childNode in node.ChildNodes)
                    if (childNode.Name == "chatId" && childNode.InnerText == id)
                        return true;
            return false;
        }

        public string getStat(string id)
        {
            xmlDoc.Load(xmlPath);
            XmlElement element = xmlDoc.DocumentElement;

            XmlNode wins = element.SelectSingleNode($"user[chatId = '{id}']/wins");
            XmlNode loses = element.SelectSingleNode($"user[chatId = '{id}']/loses");

            string outing = $"Перемог: {wins.InnerText}; Програшів: {loses.InnerText}";
            return outing;
        }

        public string getItem(string id, string item)
        {
            xmlDoc.Load(xmlPath);
            XmlElement element = xmlDoc.DocumentElement;

            XmlNode node = element.SelectSingleNode($"user[chatId = '{id}']/{item}");
            return node.InnerText;
        }

        public void reNewItem(string id, string item, string value)
        {
            xmlDoc.Load(xmlPath);
            XmlElement element = xmlDoc.DocumentElement;

            XmlNode node = element.SelectSingleNode($"user[chatId = '{id}']/{item}");

            node.InnerText = value;
            xmlDoc.Save(xmlPath);
        }
    }
}
