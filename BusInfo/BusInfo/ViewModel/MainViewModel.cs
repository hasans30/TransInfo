using BusInfo.Helpers;
using BusInfo.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusInfo.ViewModel
{

    public class MainViewModel : ViewModelBase
    {

        private readonly ITranslinkService _dataService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        public ObservableCollection<NextBus> NextBusList
        {   get;
            private set;
        }
        public string AppName
        {
            get { return "Some Name -- Fix It -- TODO"; }
        }
        public int EnteredTransInfo
        {
            get; set;
        }

        private List<int> UserWatchList //TODO: will be good idea to have seperate model for such user preference
        {
            get; set;
        }

        public MainViewModel(
            ITranslinkService dataService,
            IDialogService dialogService,
            INavigationService navigationService
            )
        {
            _dataService = dataService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            NextBusList = new ObservableCollection<NextBus>();
            UserWatchList = new List<int>();
            int[,] stopIDs = new int[,] { { 50747, 17 }, { 61073, 17 }, { 50996, 15 }, { 50987, 15 } };
            for(int i=0;i<4;i++)
            {
                UserWatchList.Add(stopIDs[i, 0]);
                UserWatchList.Add(stopIDs[i, 1]);
            }
           
#if DEBUG
            if (IsInDesignMode)
            {
                Refresh();
            }
#endif

        }


        RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (
                    _addCommand = new RelayCommand(
                           async
                           () => { UserWatchList.Add(EnteredTransInfo);
                                UserWatchList.Add(17);
                                await Refresh();
                            }
                        )
                    );
            }
        }

        RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand ??
                    (_refreshCommand = new RelayCommand(async () => { await Refresh(); })); //Simplifying RefreshCommand assignment)
            }
        }

        private async Task Refresh()
        {
            NextBusList.Clear();
            for(int i=0; i<UserWatchList.Count;i+=2)
            {
                var result = await _dataService.GetNextBus(
                    UserWatchList[i]
                    ,UserWatchList[i+1]);
                NextBusList.Add(result.Buses[0]); //Need to modify this code to handle for list of buses for a given stop
                
            }            
        }

    }
}