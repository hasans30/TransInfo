using System;

namespace BusInfo.Helpers
{
    public interface INavigationService
    {
        void GoBack();
        void NavigateTo(Uri uri);
    }
}
