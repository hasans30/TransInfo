using BusInfo.Design;
using BusInfo.Helpers;
using BusInfo.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusInfo.ViewModel
{
    class ViewModelLocator
    {
        public MainViewModel Main
        {
            get;
            private set;
        }
        public ViewModelLocator()
        {
            IDialogService dialogService = new DialogService();
            INavigationService navigationService = new NavigationService();
            ITranslinkService translinkService;
            if (ViewModelBase.IsInDesignModeStatic)
            {
                translinkService = new DesignTranslinkInfoService();
            }
            else
            {
                translinkService = new TranslinkService();
            }
            //Cache Service code - TODO
            translinkService.CacheUserPreference("0");

            Main = new MainViewModel(translinkService, dialogService, navigationService);
            //Initializing User Preference storage
        }
    }
}
