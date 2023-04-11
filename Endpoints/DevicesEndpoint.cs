using Microsoft.AspNetCore.Mvc;
using MILAV.API.Connection;
using MILAV.API.Device;

namespace MILAV.Endpoints
{
    [Route("devices")]
    public class DevicesEndpoint : EndpointController
    {
        public DevicesEndpoint(Controller controller) : base(controller) { }

        [HttpGet(Name = "GetDevices")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AbstractDevice[]))]
        public ActionResult GetDevices()
        {
            return Ok(controller.GetDevices());
        }

        [HttpGet("{id}", Name = "GetDeviceById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AbstractDevice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceById(string id)
        {
            if(controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/driver", Name = "GetDeviceDriverById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceDriverById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device.Driver);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/ip", Name = "GetDeviceIpById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceIpById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device.Ip);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/port", Name = "GetDevicePortById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDevicePortById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device.Port);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/protocol", Name = "GetDeviceProtocolById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Protocol))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceProtocolById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device.Protocol);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/room", Name = "GetDeviceRoomById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceRoomById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device.Room);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/state", Name = "GetDeviceStateById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControlState))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceStateById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                // TODO can be null
                return Ok(device.State);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/states", Name = "GetDeviceStatesById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControlState[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceStatesById(string id)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                return Ok(device.States);
            }

            return BadRequest("Invalid ID");
        }

        [HttpGet("{id}/states/{state}/rooms", Name = "GetDeviceStateRoomsById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult GetDeviceStateRoomsById(string id, string state)
        {
            if (controller.TryGetDevice(id, out AbstractDevice? device))
            {
                // TODO can be null
                return Ok(device.States.FirstOrDefault(s => s.Id == state)?.Rooms);
            }

            return BadRequest("Invalid ID");
        }
    }
}
