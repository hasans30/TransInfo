using BusInfo.Helpers;
using BusInfo.Model;
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
            ITransLinkService translinkService = new TransLinkService();
            IDialogService dialogService = new DialogService();
            INavigationService navigationService = new NavigationService();
            Main = new MainViewModel(translinkService, dialogService, navigationService);
        }
    }
}
