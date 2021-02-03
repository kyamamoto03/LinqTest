using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Entity> SearchData = new List<Entity>();
            SearchData.Add(new Entity { ID = 1,DateTime = new DateTime(2021,1,2) });

            var search = new Search();

            var result = search.Run(SearchData);

            result.ForEach(x => Console.WriteLine(x.ToString()));
        }
    }

    class Search
    {
        public List<Entity> Run(List<Entity> SearchData)
        {
            List<Entity> ret = new List<Entity>();

            var query = DataBase.Datas.AsQueryable();
            SearchData.ForEach(s =>
            {
                query = query.Where(x => x.ID == s.ID && x.DateTime >= s.DateTime);
            });

            query.ToList().ForEach(x => ret.Add(x));
            return ret;
        }
    }

    public class DataBase
    {
        public static List<Entity> Datas { get; } = new List<Entity>();
        [ModuleInitializer]
        public static void Init()
        {
            Enumerable.Range(0, 10).ToList().ForEach(y =>
            {
                Enumerable.Range(0, 10).ToList().ForEach(x =>
                 {
                     var d = new DateTime(2021, 1, 1).AddDays(x);
                     Datas.Add(new Entity { ID = y, DateTime = d });
                 });
            });
        }

    }
    public class Entity
    {
        public override string ToString()
        {
            return $"ID:{ID} DateTime:{DateTime.ToString()}";
        }
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
