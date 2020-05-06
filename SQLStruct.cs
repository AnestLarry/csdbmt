using System.Linq;

namespace csdbmt
{
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
}
