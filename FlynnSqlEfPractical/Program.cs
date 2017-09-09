using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace FlynnSqlEfPractical
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new FlynnPracticalTestEntities();

            var qInnerJoin = from t1 in db.Table_1
                             join t2 in db.Table_2 on t1.Id equals t2.Id
                             select new { ID = t1.Id, Name = t2.Name, Code = t1.Code };

            Console.WriteLine("Inner Join:");
            foreach (var item in qInnerJoin)
                Console.WriteLine(item.ID + " " + item.Code + " " + item.Name);

            var qLeftJoin = from t1 in db.Table_1
                            join t2 in db.Table_2 on t1.Id equals t2.Id into newGroup
                            from ng in newGroup.DefaultIfEmpty()
                            select new { ID = t1.Id, Name = ng.Name ?? String.Empty, Code = t1.Code };

            Console.WriteLine("\nLeft Join:");
            foreach (var item in qLeftJoin)
                Console.WriteLine(item.ID + " " + item.Code + " " + item.Name);

            var qRightJoin = from t2 in db.Table_2
                            join t1 in db.Table_1 on t2.Id equals t1.Id into newGroup
                            from ng in newGroup.DefaultIfEmpty()
                            select new { ID = t2.Id, Name = t2.Name, Code = ng.Code ?? "  " };

            Console.WriteLine("\nRight Join:");
            foreach (var item in qRightJoin)
                Console.WriteLine(item.ID + " " + item.Code + " " + item.Name);

            var qOuterJoin = qLeftJoin.Union(qRightJoin);

            Console.WriteLine("\nOuter Join:");
            foreach (var item in qOuterJoin)
                Console.WriteLine(item.ID + " " + item.Code + " " + item.Name);

            Console.WriteLine("\nPress Any Key To Exit");
            Console.ReadKey();
        }
    }
}
