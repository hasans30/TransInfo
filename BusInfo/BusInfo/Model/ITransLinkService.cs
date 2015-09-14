﻿using System.Threading.Tasks;

namespace BusInfo.Model
{
    public interface ITransLinkService
    {
        Task <NextBus> GetNextBus();
        Task <NextBus> GetNextBus(int stopId);
    }
}
