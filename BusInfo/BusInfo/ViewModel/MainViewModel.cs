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
        {
            get;
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

        public ObservableCollection<Schedule> SelectedBusSchedule
        {
            get;
            set;
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
            SelectedBusSchedule = new ObservableCollection<Schedule>();
            UserWatchList = new List<int>();
            //int[,] stopIDs = new int[,] { { 50412, 0 },  { 50747, 17 }, { 61073, 17 }, { 50996, 15 }, { 50987, 15 } };
            string userPreference = _dataService.GetCachedUserPreference().Result;
            int[] stopIDs = new int[] { 50747, 61073, 50996, 50987 };
            for (int i = 0; i < stopIDs.Length; i++)
            {
                UserWatchList.Add(stopIDs[i]);
            }

#if DEBUG
            if (IsInDesignMode)
            {
                Refresh();
            }
#endif

        }
        //TODO: Implement Cache service for the app. fix during that time
        RelayCommand _clearCache;
        public RelayCommand ClearCache
        {
            get
            {
                return _clearCache ?? (
                    _clearCache = new RelayCommand(
                          () => { _dataService.ClearUserPreferenceCache(); }
                        )
                    );
            }
        }

        RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (
                    _addCommand = new RelayCommand(
                           async
                           () =>
                           {
                               UserWatchList.Add(EnteredTransInfo);
                               await Refresh();
                               //_dataService.CacheUserPreference(EnteredTransInfo.ToString());
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
                    (_refreshCommand = new RelayCommand(async () =>
                    {
                        await Refresh();
                    })); //Simplifying RefreshCommand assignment)
            }
        }


        RelayCommand<NextBus> _showDetailsCommand;
        public RelayCommand<NextBus> ShowDetailsCommand
        {
            get
            {
                return _showDetailsCommand ??
                   (_showDetailsCommand = new RelayCommand<NextBus>(
                        (_selectedbus) =>
                        {
                            SelectedBusSchedule.Clear();
                            if (_selectedbus != null)
                                foreach (var val in _selectedbus.Schedules)
                                {
                                    SelectedBusSchedule.Add(val);
                                }

                        })
                   );
            }
        }

        private async Task Refresh()
        {
            NextBusList.Clear();
            //UserWatchList.AddRange( ParseToList(_dataService.GetCachedUserPreference().Result));

            for (int i = 0; i < UserWatchList.Count; i++)
            {
                var result = await _dataService.GetNextBus(
                    UserWatchList[i]);

                //adding the responses returned by the API
                foreach (var val in result.Buses)
                    NextBusList.Add(val);

            }
        }
        //Fix it during cache implmentation
        private List<int> ParseToList(string result)
        {
            return new List<int>(50996);
        }
    }
}