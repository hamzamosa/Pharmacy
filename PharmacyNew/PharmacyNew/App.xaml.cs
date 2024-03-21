using DryIoc;
using Example;
using PharmacyNew.Content;
using PharmacyNew.Content.Categories.Dialog;
using PharmacyNew.Content.ViewModels;
using PharmacyNew.Content.Views;
using PharmacyNew.Ribbon;
using PharmacyNew.Ribbon.Dialog;
using PharmacyNew.Views;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PharmacyNew
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<SellWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
         

            containerRegistry.RegisterDialog<LoginDialogView, LoginDialogViewModel>();
            containerRegistry.RegisterDialog<AddCompanyViewDilog, AddCompanyViewDilogModel>();
            containerRegistry.RegisterDialog<ConformationDialogTpremveCompanyView, ConformationDialogTpremveCompanyViewModel>();
            containerRegistry.RegisterDialog<ConformationDialogToremveSupplierView, ConformationDialogToremveSupplierViewModel>();
            containerRegistry.RegisterDialog<UpdateCompanyNameView, UpdateCompanyNameViewModel>();
            containerRegistry.RegisterDialog<AddSupllierView, AddSupllierViewModel>();
            containerRegistry.RegisterDialog<UpdateSupplierView, UpdateSupplierViewModel>();
            containerRegistry.RegisterDialog<AddCategoryViewDialog, AddCategoryViewDialogModel>();
            containerRegistry.RegisterDialog<UpdateDialogView, UpdateDialogViewModel>();
            containerRegistry.RegisterDialog<ConformationDialogView, ConformationDialogViewModel>();
            containerRegistry.RegisterDialog<AddMedicanView, AddMedicanViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<RibbonModule>();
            moduleCatalog.AddModule<ContentModule>();

        }

    }
}
