using BusInfo.Command;
using BusInfo.Helpers;
using BusInfo.Model;
using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace BusInfo.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
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
            StringBuilder sb = new StringBuilder();
            int[] stopIDs = new int[] { 50747, 61073 };//, 50996 , 50987 };
            NextBus [] nb = new NextBus [stopIDs.Length];
            int i = 0;
            foreach (int stopID in stopIDs)
            {
                nb[i++] = await _dataService.GetNextBus(stopID);
            }

            i = 0;
            foreach(int stopID in stopIDs)
            {
                sb.AppendLine(string.Format("@StopID{0}-{1}-{2} to {3} \n{4}\n{5}\n", 
                    stopID, nb[i].RouteName, nb[i].Direction,
                    nb[i].Schedules[0].Destination,
                    nb[i].Schedules[0].ExpectedLeaveTime, 
                    nb[i].Schedules[1].ExpectedLeaveTime));
                i++;
            }
            _information = sb.ToString();
            RaisePropertyChanged("Information");
        }
    }
}