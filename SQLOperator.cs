using System;

namespace csdbmt
{
    class SQLOperator
    {
        SQLParse Parser = new SQLParse();
        SQLStruct curSQLStruct;
        public void Work(string sql)
        {

            curSQLStruct = Parser.Parse(sql);
            Console.WriteLine(curSQLStruct.ToString());
        }
    }
}
