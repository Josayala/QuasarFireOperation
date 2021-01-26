using System;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core
{
    public class AppControllerBase : ControllerBase
    {

        protected IActionResult BuildNotFoundResponse(Type resourceType)
        {
            return NotFound($"Resource Not Found. Resource: {resourceType.Name}");
        }

    }
}