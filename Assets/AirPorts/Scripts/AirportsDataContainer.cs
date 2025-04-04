
using Utility;
using System.Collections.Generic;

namespace AirPorts
{
    public sealed class AirportsDataContainer : Singleton<AirportsDataContainer>
    {
        public List<AirPortData> AirPortDatas = new();
    }
}