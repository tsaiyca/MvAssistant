using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component
{
    [GuidAttribute("AAAE12DC-A001-4200-9EC1-DE63247603BB")]
    public interface IHalSECSMODULE : IMacHalComponent
    {
        bool ConnectTAP(string IPAddress, int port); 
        bool SendToTAP(string StreamFunction, object data);
        void StartListenTAP();
    }
}
