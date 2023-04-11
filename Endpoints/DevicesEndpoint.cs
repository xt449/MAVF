using Microsoft.AspNetCore.Mvc;
using MILAV.API.Device;

namespace MILAV.Endpoints
{
    [Route("devices")]
    public class DevicesEndpoint : EndpointController
    {
        public DevicesEndpoint(Controller controller) : base(controller) { }

        [HttpGet(Name = "GetDevices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AbstractDevice[]))]
        public ActionResult GetAll()
        {
            return Ok(new List<AbstractDevice>());
        }

        [HttpGet("{id}", Name = "GetDeviceById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AbstractDevice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDevice(string id)
        {
            if (id == "test")
            {
                return Ok(controller.GetText());
            }

            return BadRequest("Invalid");
        }
    }
}
