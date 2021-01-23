using System;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web.Core
{
    public class AppControllerBase : ControllerBase
    {
        public const string IDS_NOT_EQUAL = "The ID in the uri must match the ID in the json";

        protected IActionResult BuildPutResponse(CompletionStatus status)
        {
            if (!status.IsSuccessful)
            {
                status.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return Ok();
        }

        protected IActionResult BuildDeleteResponse(CompletionStatus status)
        {
            if (!status.IsSuccessful)
            {
                status.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        protected IActionResult BuildNotFoundResponse(Type resourceType)
        {
            return NotFound($"Resource Not Found. Resource: {resourceType.Name}");
        }

        protected IActionResult BuildIdsNotEqualResponse()
        {
            return BadRequest(IDS_NOT_EQUAL);
        }
    }
}