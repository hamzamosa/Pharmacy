using BL1.Cmpanies;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL1.Categories
{
    public class Categories : IDataHelper<TbCategories>
    {
        private PharmacyContext _context;
        public Categories(PharmacyContext context)
        {

            _context = context;

        }
        public void Add(TbCategories table)
        {
            _context.TbCategory.Add(table);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var iteam = Find(id);
            _context.Remove(iteam);
            _context.SaveChanges();
        }

        public void Edit(TbCategories table)
        {
            _context.TbCategory.Update(table);
            _context.SaveChanges();
        }

        public TbCategories Find(int id)
        {
            return _context.TbCategory.Find(id);
        }

        public List<TbCategories> GetAll()
        {
            return _context.TbCategory.ToList();
        }

        public List<TbCategories> Search(string query)
        {
            return _context.TbCategory.Where(x => x.Id.ToString() == query || x.CategoryName.Contains(query)).ToList();
        }
    }
}
