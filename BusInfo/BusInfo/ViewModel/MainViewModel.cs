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
#if DEBUG
            if (IsInDesignMode)
            {
                Refresh();
            }
#endif

        }
        public MainViewModel() :
        this(
            new TranslinkService(),
            new DialogService(),
            new NavigationService())
        {
            NextBusList = new ObservableCollection<NextBus>();
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
            int[,] stopIDs = new int[,] { { 50747, 17 }, { 61073, 17 }, { 50996, 15 }, { 50987, 15 } };
            NextBusList.Clear();
            for (int i= 0;i< 4;i++)
            {
                var result = await _dataService.GetNextBus(stopIDs[i,0],stopIDs[i,1]);
                NextBusList.Add(result);
            }            
        }
       
    }
}