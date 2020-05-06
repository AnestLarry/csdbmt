using System;

namespace csdbmt
{
    class SQLManager
    {
        SQLParse Parser = new SQLParse();
        SQLStruct curSQLStruct;
        SQLAction sa = new SQLAction();
        public void Work(string sql)
        {
            curSQLStruct = Parser.Parse(sql);
            Console.WriteLine(curSQLStruct.ToString());
            try
            {
                sa.Handle(curSQLStruct);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Console.WriteLine(sa.ToString());
        }
    }
}
