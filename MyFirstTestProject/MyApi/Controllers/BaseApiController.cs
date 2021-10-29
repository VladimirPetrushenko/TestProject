using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    public class BaseApiController : Controller
    {
        protected readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
