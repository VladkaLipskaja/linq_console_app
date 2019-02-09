using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp1
{
    public static class Program
    {
        public class A
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public static IOrderedQueryable<TSource> OrderWithSeveralRulesBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> mainRule, params Expression<Func<TSource, object>>[] rules)
        {
            var query = source.OrderBy(mainRule);

            foreach (var rule in rules)
            {
                query = query.ThenBy(rule);
            }

            return query;
        }

        static void Main(string[] args)
        {
            List<A> a = new List<A>
            {
                new A { Id = 1, Name = "Name1" },
                new A { Id = 2, Name = "Name2"}
            };

            a.AsQueryable().OrderWithSeveralRulesBy(i => i.Id, i => i.Id, i => i.Name);
            Console.ReadKey();
        }
    }
}
