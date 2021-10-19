using Ekmob.TechSession.Core.Utilities.Response;
using Microsoft.AspNetCore.Mvc;

namespace Ekmob.TechSession.Shared.BaseController
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
