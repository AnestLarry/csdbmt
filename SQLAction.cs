using System;
using System.Collections.Generic;
using System.Linq;

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
                case "insert into":
                    {
                        if (!(s.SQLHead.Length>1))
                        {
                            throw new Exception("SQLAction -> Handle(): insert into: SQLHead Length Error.\nDo you miss table/database name?");
                        }
                        if (!(s.SQLBody.Length>0))
                        {
                            throw new Exception("SQLAction -> Handle(): insert into: SQLBody Length Error.\nDo you miss field(s)?");
                        }
                        if (!(s.SQLFoot.Length > 0))
                        {
                            throw new Exception("SQLAction -> Handle(): insert into: SQLFoot Length Error.\nDo you miss value(s)?");
                        }
                        Database db = dbs.Get(s.SQLHead[0]);
                        db.Add(s.SQLHead[1]);
                        DatabaseTable dt = db.Get(s.SQLHead[1]);
                        List<DatabaseTableField> dtfs = new List<DatabaseTableField>();
                        foreach (string i in s.SQLBody[0])
                        {
                            dtfs.Add(dt.Get(i));
                        }
                        foreach (string[] i in s.SQLFoot)
                        {
                            for (int j = 0; j < i.Length; j++)
                            {
                                dtfs[j].Add(i[j],dtfs[j].dataType);
                            }
                        }
                        break;
                    }
                case "delete":
                    {
                        if (s.SQLHead.Length > 1)
                        {
                            Database db = dbs.Get(s.SQLHead[0]);
                            DatabaseTable dt = db.Get(s.SQLHead[1]);
                            dt.ToEmpty();
                        }
                        else
                        {
                            throw new Exception("SQLAction -> Handle(): Delete error: length <= 1");
                        }
                        break;
                    }

                default:
                    throw new Exception("SQLManager -> Work(): An unexcepted SQL action type.");
            }
        }
        public string ToString()
        {
            return dbs.ToString();
        }
    }
}
