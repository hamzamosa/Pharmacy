using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL1.Authuntiacation
{
    public class Users
    {
        private PharmacyContext _context;
        public Users(PharmacyContext context)
        {

            _context = context;

        }

        public bool IsUseValid(string Name, string password)
        {
            var user = _context.TbUsers.FirstOrDefault(u => u.Name == Name && u.Password == password);

            if (user is not null)
            {

                return true;
            }
            else
            {

                return false;
            }

        }
    }
}
