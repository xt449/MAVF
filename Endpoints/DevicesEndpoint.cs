using Microsoft.AspNetCore.Mvc;
using MILAV.API.Device;
using MILAV.API.Device.Implementations;

namespace MILAV.Endpoints
{
    [Route("devices")]
    public class DevicesEndpoint : EndpointController
    {
        public DevicesEndpoint(Controller controller) : base(controller) { }

        [HttpGet(Name = "GetDevices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDevice[]))]
        public ActionResult GetAll()
        {
            return Ok(new List<IDevice>());
        }

        private readonly TestDevice testDevice = new TestDevice();

        [HttpGet("{id}", Name = "GetDeviceById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDevice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDevice(string id)
        {
            if(id == "test")
            {
                return Ok(testDevice);
            }

            return BadRequest("Invalid");
        }
    }
}
