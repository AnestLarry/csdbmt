using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace csdbmt
{
    class SQLOperator
    {
        SQLParse Parser = new SQLParse();
        public void Work(string sql)
        {

            SQLStruct s = Parser.Parse(sql);
            Console.WriteLine(s.ToString());
        }
    }
    class SQLStruct
    {
        public string SQLMode;
        public string[] SQLHead;
        public string[][] SQLBody;
        public string[][] SQLFoot;
        public string[][] SQLTail;
        private SQLStruct(Builder x)
        {
            this.SQLMode = x.getSQLMode;
            this.SQLHead = x.getSQLHead;
            this.SQLBody = x.getSQLBody;
            this.SQLFoot = x.getSQLFoot;
            this.SQLTail = x.getSQLTail;
        }

        public override string ToString()
        {
            return string.Format("SQLStruct [ \n SQLMode [{0}]\n SQLHead [{1}]\n SQLBody [{2}]\n SQLFoot [{3}]\n SQLTail [{4}]\n]",
                SQLMode,
                string.Join(",", SQLHead).Trim(),
                string.Join(", ", from s in SQLBody select "[" + string.Join(",", s) + "]"),
                string.Join(", ", from s in SQLFoot select "[" + string.Join(",", s) + "]"),
                string.Join(", ", from s in SQLTail select "[" + string.Join(",", s) + "]")
                //"","","",""
                );
        }
        public class Builder
        {
            private string SQLMode;
            private string[] SQLHead;
            private string[][] SQLBody;
            private string[][] SQLFoot;
            private string[][] SQLTail;

            public string getSQLMode { get => SQLMode; }
            public string[] getSQLHead { get => SQLHead; }
            public string[][] getSQLBody { get => SQLBody; }
            public string[][] getSQLFoot { get => SQLFoot; }
            public string[][] getSQLTail { get => SQLTail; }

            public Builder(string mode)
            {
                SQLMode = mode;
            }
            public Builder setSQLMode(string x)
            {
                SQLMode = x;
                return this;
            }
            public Builder setSQLHead(string[] x)
            {
                SQLHead = x;
                return this;
            }
            public Builder setSQLBody(string[][] x)
            {
                SQLBody = x;
                return this;
            }
            public Builder setSQLFoot(string[][] x)
            {
                SQLFoot = x;
                return this;
            }
            public Builder setSQLTail(string[][] x)
            {
                SQLTail = x;
                return this;
            }
            public SQLStruct Build()
            {
                if (SQLHead == null)
                {
                    SQLHead = new string[] { };
                }
                if (SQLBody == null)
                {
                    SQLBody = new string[][] { };
                }
                if (SQLFoot == null)
                {
                    SQLFoot = new string[][] { };
                }
                if (SQLTail == null)
                {
                    SQLTail = new string[][] { };
                }
                return new SQLStruct(this);
            }
        }

    }
    class SQLParse
    {
        public SQLStruct newStruct = new SQLStruct.Builder("None").Build();
        public SQLStruct Parse(string x)
        {
            String Mode = x.Split(" ")[0];
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
                case "select":
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
                databaseName = m.Groups[2].ToString().Trim();
                tableName = m.Groups[m.Groups.Count - 1].ToString().Trim();
                List<string[]> body = new List<string[]>();
                foreach (string s in m.Groups[3].ToString().Trim().Split(","))
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
            string[] sqlSplit = x.Split(" ");
            //SQLStruct.Builder b = new SQLStruct.Builder();
            switch (sqlSplit[1])
            {
                case "table":
                    string otherString = x.Substring(x.IndexOf("table") + 5);
                    int from_index_of_oS = otherString.IndexOf("from");

                    newStruct = new SQLStruct.Builder("drop table")
                        .setSQLHead(new string[] { x.Substring(x.IndexOf("table") + 5, from_index_of_oS - x.IndexOf("table") + 5), })
                        .setSQLBody(new string[][] { new string[] { x.Substring(from_index_of_oS + 4) } })
                        .Build();
                    break;
                case "database":
                    newStruct = new SQLStruct.Builder("drop database")
                        .setSQLHead(new string[] { x.Substring(x.IndexOf("database") + 8), })
                        .Build();
                    break;
            }
        }
        private void InsertInto(string x)
        {
            Match m = Regex.Match(x, "insert[ ]+into[ ]+([A-z0-9]+)[ ]*(\\(([^\\)]+)\\)|[^ ]*)[ ]*values[ ]*(\\(.+\\))[ ]*from[ ]*([A-z0-9]*)");
            string tableName;
            string databaseName;
            List<string[]> foot = new List<string[]>();
            string[][] Names = new string[1][];

            if (m.Success)
            {
                tableName = m.Groups[1].ToString();
                databaseName = m.Groups[m.Groups.Count - 1].ToString();
                if (m.Groups[3].ToString() != "")
                {
                    Names[0] = m.Groups[3].ToString().Split(",");
                }
                if (m.Groups[4].ToString() != "")
                {
                    string temp = m.Groups[4].ToString();
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
                foreach (string s in whereSub.Split("or"))
                {
                    string[] temp = s.Split("and");
                    body.Add(temp);
                }
                newStruct = new SQLStruct.Builder(whereSub == "" ? "delete" : "delete where")
                    .setSQLHead(new string[] { databaseName, tableName })
                    .setSQLBody(body.ToArray())
                    .Build();

            }

        }
    }
}
