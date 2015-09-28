using System.Threading.Tasks;

namespace BusInfo.Model
{
    public interface ITranslinkService
    {
        Task <NextBuses> GetNextBus();
        Task <NextBuses> GetNextBus(int stopId);
        Task <NextBuses> GetNextBus(int stopId, int busId);
    }
}
