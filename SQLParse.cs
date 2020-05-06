using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace csdbmt
{
    class SQLParse
    {
        public SQLStruct newStruct = new SQLStruct.Builder("None").Build();
        public SQLStruct Parse(string x)
        {
            string Mode = x.Split(" ")[0];
            switch (Mode)
            {
                case "create":
                    CreateParse(x);
                    break;
                case "drop":
                    DropParse(x);
                    break;
                case "insert":
                    InsertInto(x);
                    break;
                case "delete":
                    DeleteParse(x);
                    break;
                case "update":
                    UpdateParse(x);
                    break;
                case "select":
                    SelectParse(x);
                    break;
                default:
                    Console.WriteLine(String.Format("Parse(): Value Error: unexpected '{0}'", Mode));
                    break;
            }
            return newStruct;
        }
        private void CreateParse(string x)
        {
            Match m = Regex.Match(x, "create[ ]+(table|database)[ ]+([A-z0-9]+)([ ]*\\((.+)\\)|)([ ]*from[ ]+([A-z0-9]+)|)");
            string tableName;
            string databaseName;
            if (m.Success)
            {
                tableName = m.Groups[2].ToString().Trim();
                databaseName = m.Groups[m.Groups.Count - 1].ToString().Trim();
                List<string[]> body = new List<string[]>();
                foreach (string s in m.Groups[3].ToString().Replace("(", "").Replace(")", "").Trim().Split(","))
                {
                    string[] temp = s.Split(" ");
                    body.Add(temp);
                }
                newStruct = new SQLStruct.Builder("create " + m.Groups[1].ToString())
                    .setSQLHead(new string[] { databaseName, tableName })
                    .setSQLBody(body.ToArray())
                    .Build();

            }
        }
        private void DropParse(string x)
        {
            Match m = Regex.Match(x, "drop[ ]+(table|database)[ ]+([A-z0-9]+)( +from[ ]+([A-z0-9]+)|)");
            string dropName;
            string databaseName;
            if (m.Success)
            {
                dropName = m.Groups[2].ToString().Trim();
                databaseName = m.Groups[4].ToString().Trim();
                newStruct = new SQLStruct.Builder("drop " + m.Groups[1].ToString())
                    .setSQLHead(new string[] { databaseName, dropName })
                    .Build();
            }
        }
        private void InsertInto(string x)
        {
            Match m = Regex.Match(x, "insert[ ]+into[ ]+([A-z0-9]*)[ ]*\\.[ ]*([A-z0-9]+)[ ]*(\\(([^\\)]+)\\)|[^ ]*)[ ]*values[ ]*(\\(.+\\))");
            string tableName;
            string databaseName;
            List<string[]> foot = new List<string[]>();
            string[][] Names = new string[1][] { new string[] { "" } };

            if (m.Success)
            {
                tableName = m.Groups[2].ToString();
                databaseName = m.Groups[1].ToString();
                if (m.Groups[4].ToString() != "")
                {
                    Names[0] = m.Groups[3].ToString().Replace("(", "").Replace(")", "").Trim().Split(",");
                }
                if (m.Groups[5].ToString() != "")
                {
                    string temp = m.Groups[5].ToString();
                    foreach (string s in Regex.Split(temp.Substring(temp.IndexOf("(") + 1, temp.LastIndexOf(")") - temp.IndexOf("(") - 1),
                        "[ ]*\\)[ ]*,[ ]*\\([ ]*"))
                    {
                        foot.Add(s.Replace("(", "").Replace(")", "").Split(","));
                    }
                }
                newStruct = new SQLStruct.Builder("insert into")
                    .setSQLHead(new string[] { databaseName, tableName })
                    .setSQLBody(Names)
                    .setSQLFoot(foot.ToArray())
                    .Build();
            }
        }
        private void DeleteParse(string x)
        {
            Match m = Regex.Match(x, "delete[ ]+from[ ]+(([A-z0-9]+)\\.([A-z0-9]+))([ ]+where(.+)|)");
            string tableName;
            string databaseName;
            if (m.Success)
            {
                databaseName = m.Groups[2].ToString().Trim();
                tableName = m.Groups[3].ToString().Trim();
                List<string[]> body = new List<string[]>();
                string whereSub = m.Groups[5].ToString().Trim();
                foreach (string s in whereSub.Split(" or "))
                {
                    string[] temp = s.Split(" and ");
                    body.Add(temp);
                }
                newStruct = new SQLStruct.Builder(whereSub == "" ? "delete" : "delete where")
                    .setSQLHead(new string[] { databaseName, tableName })
                    .setSQLBody(body.ToArray())
                    .Build();
            }
        }
        private void UpdateParse(string x)
        {
            Match m = Regex.Match(x, "update[ ]+([A-z0-9]*)[ ]*\\.[ ]*([A-z0-9]+)[ ]+set *([ ]+where(.+)|)");
            string tableName;
            string databaseName;
            if (m.Success)
            {
                databaseName = m.Groups[1].ToString().Trim();
                tableName = m.Groups[2].ToString().Trim();
                List<string[]> body = new List<string[]>();
                List<string[]> foot = new List<string[]>();

                if (x.IndexOf(" where ") > -1)
                {
                    m = Regex.Match(x, "set.*where(.+)");
                    string whereSub = m.Groups[1].ToString().Trim();

                    foreach (string s in whereSub.Split(" or "))
                    {
                        string[] temp = s.Trim().Split(" and ");
                        foot.Add(temp);
                    }
                }
                if (x.IndexOf(" where ") > -1)
                {
                    m = Regex.Match(x, "set *(.*) *where");
                }
                else
                {
                    m = Regex.Match(x, "set *(.*)");

                }
                string setSub = m.Groups[1].ToString();
                Console.WriteLine(setSub);
                foreach (string s in setSub.Trim().Split(","))
                {
                    string[] temp = s.Trim().Split("=");
                    body.Add(temp);
                }
                newStruct = new SQLStruct.Builder("update")
                    .setSQLHead(new string[] { databaseName, tableName })
                    .setSQLBody(body.ToArray())
                    .setSQLFoot(foot.ToArray())
                    .Build();

            }

        }
        private void SelectParse(string x)
        {
            Match m;
            bool hasWhere = x.IndexOf("where") > -1;
            if (hasWhere)
            {
                m = Regex.Match(x, "select *(.*) *where *(.*) *");
            }
            else
            {
                m = Regex.Match(x, "select *(.*)( *)");
            }
            if (m.Success)
            {
                List<string> head;
                List<string[]> body = new List<string[]>();
                head = (from s in m.Groups[1].ToString().Split(",") select s).ToList();
                if (hasWhere)
                {
                    string whereSub = m.Groups[2].ToString().Trim();

                    foreach (string s in whereSub.Split(" or "))
                    {
                        string[] temp = s.Trim().Split(" and ");
                        body.Add(temp);
                    }
                }
                newStruct = new SQLStruct.Builder("select")
                    .setSQLHead(head.ToArray())
                    .setSQLBody(body.ToArray())
                    .Build();
            }
        }
    }
}
