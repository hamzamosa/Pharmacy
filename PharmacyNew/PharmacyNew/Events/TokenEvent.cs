using PharmacyNew.Content.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Events
{
    public class TokenEvent :PubSubEvent<string>
    {
    }

    public class SwndNameOfCompanyFromAddDialogEvent : PubSubEvent<string>
    {
    }
    public class UpdateCoompnaiesList : PubSubEvent<CompaniesModel>
    {
    }

    public class MkaeItesmEnable : PubSubEvent<bool>
    {
    }

}
