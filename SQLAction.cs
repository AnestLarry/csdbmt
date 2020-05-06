using System;

namespace csdbmt
{
    class SQLAction
    {
        private DatabaseRoot dbs = new DatabaseRoot();
        private SQLStruct ss;
        public void Handle(SQLStruct s)
        {
            switch (s.SQLMode)
            {
                case "create database":
                    {
                        if (s.SQLHead.Length > 1)
                        {
                            dbs.Add(s.SQLHead[1]);
                        }
                        else
                        {
                            throw new Exception("SQLAction -> Handle(): create database error: length <= 1");
                        }
                        break;
                    }
                case "create table":
                    {
                        if (s.SQLHead.Length > 1)
                        {
                            Database db = dbs.Get(s.SQLHead[0]);
                            db.Add(s.SQLHead[1]);
                            DatabaseTable dt = db.Get(s.SQLHead[1]);
                            foreach (string[] i in s.SQLBody)
                            {
                                dt.Add(i[0], i[1]);
                            }
                        }
                        else
                        {
                            throw new Exception("SQLAction -> Handle(): create table error: length <= 1");
                        }
                        break;
                    }
                case "drop database":
                    {
                        if (s.SQLHead.Length > 1)
                        {
                            dbs.Delete(s.SQLHead[1]);
                        }
                        else
                        {
                            throw new Exception("SQLAction -> Handle(): drop database error: length <= 1");
                        }
                        break;
                    }
                case "drop table":
                    {
                        if (s.SQLHead.Length > 1)
                        {
                            Database db = dbs.Get(s.SQLHead[0]);
                            db.Delete(s.SQLHead[1]);
                        }
                        else
                        {
                            throw new Exception("SQLAction -> Handle(): drop table error: length <= 1");
                        }
                        break;
                    }
                default:
                    throw new Exception("SQLManager -> Work(): An unexcepted SQL action type.");
            }
        }
    }
}
