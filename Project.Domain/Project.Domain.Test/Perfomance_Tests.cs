using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Project.Domain.Test.BD;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Project.Domain.Test
{
    [TestClass]
    public class Perfomance_Tests
    {
        private MyDbContext _context;
        [TestInitialize]
        public void SetUp()
        {
            _context = new MyDbContext();
        }


        [TestMethod]
        public void TestMethod1()
        {
            var a =   _context.Ceps.GetWhereNoTracking(x => x.Id == 1).ToList();

            var c = _context.Ceps
                            .GetWhereByCompleteDate(x => x.Id == 1, x => new Retorno {Id =x.Id, Nome= x.Logradouro })
                            .ToList();

        }
    }
    public class Retorno
    {
        public string Nome { get; set; }
        public int Id { get; set; }
    }
    public static class Helper
    {

        public static IEnumerable<T> GetWhereNoTracking<T>(this IQueryable<T> source, Expression<Func<T,bool>> predicate) where T : class
        {
            return source.AsNoTracking().Where(predicate);
        }
        public static IEnumerable<T> GetAllNoTracking<T>(this IQueryable<T> source) where T : class
        {
            return source.AsNoTracking();
        }

        public static IEnumerable<Retorno> GetWhereByCompleteDate<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, Expression<Func<T, Retorno>> proje) where T : class
        {
            return  source.Where(predicate)
                          .Select(proje);
        }
    }
}
