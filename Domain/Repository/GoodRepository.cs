using System;
using Domain.Entities;
using System.Data.Entity;
using System.Linq;

namespace Domain.Repository
{
    public class GoodRepository 
    {
        private AbzContext db = new AbzContext();
        public IQueryable<Good> Get()
        {
            return db.Goods.OrderBy(b => b.CategId).ThenBy(c => c.txt);
        }

        public IQueryable<Good> GetSkipTake(int skip, int take, int categId)
        {
            return db.Goods.Where(w => w.CategId == categId)
                    .OrderBy(b => b.txt)
                    .Skip(skip)
                    .Take(take);
        } 

        public int GetCountItems(int categoryId = 0)
        {
            if (categoryId == 0)
            {
                return db.Goods.Count();
            }
            else 
                return db.Goods.Where(a => a.CategId == categoryId).Count();
        }

    }
}