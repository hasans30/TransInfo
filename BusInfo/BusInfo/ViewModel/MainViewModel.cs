using BusInfo.Command;
using BusInfo.Helpers;
using BusInfo.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace BusInfo.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        private readonly ITransLinkService _dataService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        private NextBus NextBus { get; set; }

        public string AppName
        {
            get { return NextBus.RouteName; }
        }

        private string _information;
        public string Information
        {
            get
            {
                return _information;
            }
            set
            {
                Set(()=>Information,ref _information, value);
            }
        }
        public MainViewModel(
            ITransLinkService dataService,
            IDialogService dialogService,
            INavigationService navigationService
            )
        {
            _dataService = dataService;
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public RelayCommand RefreshCommand
        {
            get;
            private set;
        }

        public MainViewModel(): 
            this(
                new TransLinkService(),
                new DialogService(),
                new NavigationService())
        {
            RefreshCommand = new RelayCommand( async ()=> { await Refresh();}); //Simplifying RefreshCommand assignment
        }

       

        private async Task Refresh()
        {
            StringBuilder sb = new StringBuilder();
            int[,] stopIDs = new int[,] { { 50747, 17 }, { 61073, 17 }, { 50996, 15 }, { 50987, 15 } };
            
            NextBus [] nb = new NextBus [4];
            for (int i= 0;i< 4;i++)
            {
                nb[i] = await _dataService.GetNextBus(stopIDs[i,0],stopIDs[i,1]);
            }

            
            for(int j=0;j<4;j++)
            {

                sb.AppendLine(string.Format("@StopID{0}-{1}-{2} to {3} \n{4}\n{5}\n", 
                    stopIDs[j,0], nb[j].RouteName, nb[j].Direction,
                    nb[j].Schedules[0].Destination,
                    nb[j].Schedules[0].ExpectedLeaveTime, 
                    nb[j].Schedules[1].ExpectedLeaveTime));
            }
            Information = sb.ToString(); // no need to call raisepropertychange event as setter of this uses set() method that will call implicitly 
        }
    }
}