using BusInfo.Command;
using BusInfo.Helpers;
using BusInfo.Model;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BusInfo.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {

        private readonly ITransLinkService _dataService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public NextBus NextBusNBCambie57 { get; set; }
        public NextBus NextBusSBCambie57 { get; set; }
        public NextBus NextBusNBOak57 { get; set; }

        public string AppName
        {
            get { return NextBusNBCambie57.RouteName; }
        }

        public string InformationNBCambie57
        {
            get
            {
                return string.Format("{0}\n{1}\n{2}", NextBusNBCambie57.RouteName, NextBusNBCambie57.Schedules[0].ExpectedLeaveTime, NextBusNBCambie57.Schedules[1].ExpectedLeaveTime);

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

        public MainViewModel(): 
            this(
                new TransLinkService(),
                new DialogService(),
                new NavigationService())
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                    ?? (_refreshCommand = new RelayCommand(
                                          async () =>
                                          {
                                              await Refresh();
                                          }));
            }
        }

        private async Task Refresh()
        {
            NextBusNBCambie57=await _dataService.GetNextBus();
            RaisePropertyChanged("InformationNBCambie57");
        }
    }
}