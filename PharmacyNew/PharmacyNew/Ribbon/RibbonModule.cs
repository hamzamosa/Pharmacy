using PharmacyNew.Ribbon.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Ribbon
{
    public class RibbonModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public RibbonModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("Ribbon", typeof(RibbonView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
