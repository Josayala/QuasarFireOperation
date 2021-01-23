using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSatelites;
using QuasarFireOperation.Domain.CommandModel.Requests.AddSingleSatellite;
using QuasarFireOperation.Domain.QueryModel.Dtos;
using QuasarFireOperation.Domain.QueryModel.Queries.Satellite;
using Web.Core;

namespace QuasarFireOperation.Web.Controllers.Entities
{
    [ApiController]
    public class TopSecretController : ApiBaseController
    {
        private readonly IMemoryCache _cache;

        public TopSecretController(IMediator mediator, IMemoryCache cache)
        {
            Mediator = mediator;
            _cache = cache;
        }


        [HttpPost("topsecret")]
        [ProducesResponseType(typeof(SatelliteMessageDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResourceNotFoundResult), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post([FromBody] AddSatelliteRequest addRequest)
        {
            try
            {
                var result = await Mediator.Send(addRequest);
                if (!result.Status.IsSuccessful)
                {
                    result.Status.AddToModelState(ModelState);
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(result.Entity.Message) || result.Entity.Position == null)
                    return BuildNotFoundResponse(typeof(SatelliteMessageDto));

                return Ok(result.Entity);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPost("topsecret_split/{satelliteName}")]
        [ProducesResponseType(typeof(SatelliteMessageDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResourceNotFoundResult), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post([FromBody] AddSingleSatelliteRequest addRequest,
            [FromRoute] string satelliteName)
        {
            try
            {
                addRequest.Cache = _cache;
                addRequest.SatelliteName = satelliteName;
                var result = await Mediator.Send(addRequest);
                if (!result.Status.IsSuccessful)
                {
                    result.Status.AddToModelState(ModelState);
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(result.Entity.Message) || result.Entity.Position == null)
                    return BuildNotFoundResponse(typeof(SatelliteMessageDto));
                return Ok(result.Entity);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("topsecret_split")]
        [ProducesResponseType(typeof(SatelliteMessageDto), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetMessageAndLocation()
        {
            try
            {
                var dto = await Mediator.Send(new SatelliteListQuery {Cache = _cache});
                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}