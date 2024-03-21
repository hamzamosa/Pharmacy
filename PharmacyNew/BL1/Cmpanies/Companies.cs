using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL1.Cmpanies
{
    public class Companies : IDataHelper<TbCompany>
    {
        private PharmacyContext _context;
        public Companies(PharmacyContext context)
        {

            _context = context;

        }
        public int Add(TbCompany table)
        {

            _context.TbCompanies.Add(table);
            _context.SaveChanges();

            return table.Id;

        }

        public void Delete(int id)
        {

            var iteam = Find(id);
            _context.Remove(iteam);
            _context.SaveChanges();
        }

        public void Edit(TbCompany table)
        {
            _context.TbCompanies.Update(table);
            _context.SaveChanges();
        }

        public List<TbCompany> GetAll()
        {
            return _context.TbCompanies.ToList();
        }

        public TbCompany Find(int id)
        {
            return _context.TbCompanies.Find(id);
        }

        public List<TbCompany> Search(string query)
        {


            return _context.TbCompanies.Where(x => x.Id.ToString() == query || x.ComapanyName.Contains(query)).ToList();
        }
    }
}
