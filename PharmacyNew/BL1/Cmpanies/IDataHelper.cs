using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL1.Cmpanies
{
    public interface IDataHelper<Table>
    {
        List<Table> GetAll();

        List<Table> Search(string query);

        Table Find(int id);

        void Edit(Table table);
        void Delete(int id);
        int Add(Table table);
    }
}
