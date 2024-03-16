using PharmacyNew.Content.Views;
using PharmacyNew.Ribbon.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Content
{
    public class ContentModule: IModule
    {
        private readonly IRegionManager _regionManager;
    public ContentModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;

    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion("CompaniesView", typeof(CompaniesView));

    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {

    }
   

    }
}
