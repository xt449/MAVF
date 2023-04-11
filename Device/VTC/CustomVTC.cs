using MILAV.API.Device;
using MILAV.API.Device.TVTuner;

namespace MILAV.Device.VTC
{
    [Device("customvtc")]
    public class CustomVTC : AbstractDevice, IChannelControl, IPowerControl
    {
        public string GetChannel()
        {
            throw new NotImplementedException();
        }

        public void SetChannel(string channel)
        {
            throw new NotImplementedException();
        }

        public bool GetPower()
        {
            return false;
        }

        public void SetPower(bool state)
        {
            // nothing
        }
    }
}
