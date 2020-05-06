using System;
namespace csdbmt
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql;
            SQLManager o = new SQLManager();
            while (true)
            {
                //try
                //{
                    Console.Write("csdbmt >");
                    sql = Console.ReadLine();
                    if (sql == "exit")
                    {
                        Console.WriteLine("Bye");
                        Environment.Exit(0);
                    }
                    o.Work(sql);
                //}
                //catch
                //{
                //    Console.WriteLine("Something Error.");
                //}
            }
        }
    }
}
