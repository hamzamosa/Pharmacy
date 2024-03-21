using PharmacyNew.Content.Models;
using Prism.Commands;
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

    public class SwndNameOfCategoryFromAddDialogEvent : PubSubEvent<string>
    {
    }
    public class UpdateCoompnaiesList : PubSubEvent<CompaniesModel>
    {
    }

    public class MkaeItesmEnable : PubSubEvent<bool>
    {
    }

    public class ConfirmEvent : PubSubEvent<bool>
    {
    }
    public class ConfirmEventFromRemoveSupplier : PubSubEvent<bool>
    {
    }
    public class ConfirmEventFromRemoveCategorie : PubSubEvent<bool>
    {
    }

    public class SendItemId : PubSubEvent<int>
    {
    }


    public class SendCompanyNameEVENT : PubSubEvent<String>
    {
    }

    public class SendCategoryNameEVENT : PubSubEvent<String>
    {
    }

    public class SendCategoryNameEVENTFormUpdate : PubSubEvent<String>
    {
    }



    public class SendCompanyNameeventFromUpdate : PubSubEvent<String>
    {
    }
    public class SendDataOfSuppliers : PubSubEvent<(string supplierName, string phone)>
    {
    }

    public class SendDataOfSuppliersToUpdateDialog : PubSubEvent<(string supplierName, string phone)>
    {
    }

    public class SendDataOfSuppliersFromUpdateDialog : PubSubEvent<(string supplierName, string phone)>
    {
        
    }


}
