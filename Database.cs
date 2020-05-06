using System;
using System.Collections.Generic;

namespace csdbmt
{
    class DatabaseRoot
    {
        private Dictionary<string, Database> databases = new Dictionary<string, Database>();
        public Database Get(string Name)
        {
            foreach (string i in databases.Keys)
            {
                if (i == Name)
                {
                    return databases[i];
                }
            }
            throw new Exception("DatabaseStuct -> Get(): Not in tables.");
        }
        public void Delete(string Name)
        {
            foreach (string i in databases.Keys)
            {
                if (i == Name)
                {
                    databases.Remove(Name);
                    return;
                }
            }
            throw new Exception("DatabaseStuct -> Delete(): Not in tables.");
        }
        public void Add(string Name)
        {
            foreach (string i in databases.Keys)
            {
                if (i == Name)
                {
                    throw new Exception("DatabaseStuct -> Put(): Exists in tables.");
                }
            }
            databases.Add(Name, new Database());
        }
    }
    class Database
    {
        private Dictionary<string, DatabaseTable> tables = new Dictionary<string, DatabaseTable>();
        public DatabaseTable Get(string tableName)
        {
            foreach (string i in tables.Keys)
            {
                if (i == tableName)
                {
                    return tables[i];
                }
            }
            throw new Exception("DatabaseStuct -> Get(): Not in tables.");
        }
        public void Delete(string tableName)
        {
            foreach (string i in tables.Keys)
            {
                if (i == tableName)
                {
                    tables.Remove(tableName);
                    return;
                }
            }
            throw new Exception("DatabaseStuct -> Delete(): Not in tables.");
        }
        public void Add(string tableName)
        {
            foreach (string i in tables.Keys)
            {
                if (i == tableName)
                {
                    throw new Exception("DatabaseStuct -> Put(): Exists in tables.");
                }
            }
            tables.Add(tableName, new DatabaseTable());
        }
    }
    class DatabaseTable
    {
        public Dictionary<string, DataBaseTableField> databaseTableFields = new Dictionary<string, DataBaseTableField>();
        public DataBaseTableField Get(string x)
        {
            foreach (string i in databaseTableFields.Keys)
            {
                if (i == x)
                {
                    return databaseTableFields[i];
                }
            }
            throw new Exception("DatabaseTable -> Get(): Not in tables.");
        }
        public void Delete(string Name)
        {
            foreach (string i in databaseTableFields.Keys)
            {
                if (i == Name)
                {
                    databaseTableFields.Remove(Name);
                    return;
                }
            }
            throw new Exception("DatabaseStuct -> Delete(): Not in tables.");
        }
        public void Add(string Name, string DataType)
        {
            foreach (string i in databaseTableFields.Keys)
            {
                if (i == Name)
                {
                    throw new Exception("DatabaseStuct -> Put(): Exists in tables.");
                }
            }
            databaseTableFields.Add(Name, new DataBaseTableField(DataType));
        }
    }
    public class DataBaseTableField
    {
        private string DataType = "";
        public string dataType { get => DataType; }
        private List<dynamic> Data = new List<dynamic>();
        public DataBaseTableField(string dataType)
        {
            DataType = dataType;
        }
        public IEnumerable<dynamic> travel()
        {
            foreach (var i in Data)
            {
                yield return i;
            }
        }

    }
}
