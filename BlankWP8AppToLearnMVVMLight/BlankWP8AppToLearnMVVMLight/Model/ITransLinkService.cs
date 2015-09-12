using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankWP8AppToLearnMVVMLight.Model
{
    public interface ITransLinkService
    {
        Task <NextBus> GetNextBus();
    }
}
