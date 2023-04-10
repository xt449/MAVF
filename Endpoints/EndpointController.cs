using Microsoft.AspNetCore.Mvc;

namespace MILAV.Endpoints
{
    [ApiController]
    [Produces("application/json")]
    public abstract class EndpointController : ControllerBase
    {
        protected readonly Controller controller;

        public EndpointController(Controller controller)
        {
            this.controller = controller;
        }
    }
}
