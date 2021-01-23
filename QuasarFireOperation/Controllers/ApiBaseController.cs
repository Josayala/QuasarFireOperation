using MediatR;
using Web.Core;

namespace QuasarFireOperation.Web.Controllers
{
    public class ApiBaseController : AppControllerBase
    {
        protected IMediator Mediator { get; set; }
    }
}