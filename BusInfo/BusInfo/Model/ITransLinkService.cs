using System.Threading.Tasks;

namespace BusInfo.Model
{
    public interface ITranslinkService
    {
        Task <NextBuses> GetNextBus();
        Task <NextBuses> GetNextBus(int stopId);
        Task <NextBuses> GetNextBus(int stopId, int busId);
        void CacheUserPreference(string strResponse);
        Task<string> GetCachedUserPreference();
        void ClearUserPreferenceCache();
    }
}
