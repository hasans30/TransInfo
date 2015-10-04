
using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using BusInfo.ViewModel;
using BusInfo.Helpers;
using BusInfo.Model;
using System.Threading.Tasks;

namespace BusInfoTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void MyTest1()
        {
            Assert.AreSame("Hasan", "Hasan");
        }
        [TestMethod]
        public void VerifyBusInfoGet()
        {
            ITranslinkService _testTransService = new TranslinkService();
            IDialogService _dummyDialogService = new TestDialogService();
            INavigationService _dummyNavigationService = new TestNavigationService();
            MainViewModel vm = new MainViewModel(_testTransService, _dummyDialogService, _dummyNavigationService);
            vm.RefreshCommand.Execute(null);
            Assert.IsNotNull(vm.NextBusList);
        }

        private class TestDialogService : IDialogService
        {
            public void ShowMessage(string message)
            {
                throw new NotImplementedException();
            }
        }

        private class TestNavigationService : INavigationService
        {
            public void GoBack()
            {
                throw new NotImplementedException();
            }

            public void NavigateTo(Uri uri)
            {
                throw new NotImplementedException();
            }
        }

        private class TestTranslinkService : ITranslinkService
        {
            void ITranslinkService.CacheUserPreference(string strResponse)
            {
                throw new NotImplementedException();
            }

            void ITranslinkService.ClearUserPreferenceCache()
            {
                throw new NotImplementedException();
            }

            Task<string> ITranslinkService.GetCachedUserPreference()
            {
                throw new NotImplementedException();
            }

            Task<NextBuses> ITranslinkService.GetNextBus()
            {
                throw new NotImplementedException();
            }

            Task<NextBuses> ITranslinkService.GetNextBus(int stopId)
            {
                throw new NotImplementedException();
            }

            Task<NextBuses> ITranslinkService.GetNextBus(int stopId, int busId)
            {
                throw new NotImplementedException();
            }
        }
    }
}