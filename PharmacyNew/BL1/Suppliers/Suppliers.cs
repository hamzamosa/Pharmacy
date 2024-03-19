using BL1.Cmpanies;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL1.Suppliers
{
    public class Suppliers : IDataHelper<tb_suplliers>
    {

        private PharmacyContext _context;
        public Suppliers(PharmacyContext context)
        {

            _context = context;

        }
        public void Add(tb_suplliers table)
        {
            _context.tbsuplliers.Add(table);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var iteam = Find(id);
            _context.Remove(iteam);
            _context.SaveChanges();
        }

        public void Edit(tb_suplliers table)
        {
            _context.tbsuplliers.Update(table);
            _context.SaveChanges();
        }

        public tb_suplliers Find(int id)
        {
            return _context.tbsuplliers.Find(id);
        }

        public List<tb_suplliers> GetAll()
        {
            return _context.tbsuplliers.ToList();
        }

        public List<tb_suplliers> Search(string query)
        {
            return _context.tbsuplliers.Where(x => x.Id.ToString() == query || x.SupplierName.Contains(query)).ToList();
        }
    }
}
