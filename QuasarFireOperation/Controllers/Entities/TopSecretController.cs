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

        /// <summary>
        ///       Descifrar el mensaje y la posicion con la informacion de los 3 satelites.
        /// </summary>
        /// <remarks>
        /// Ejemplo de la request:
        ///
        ///     {
        ///    "satelliteList": [
        ///    {
        ///        "name": "kenobi",
        ///        "distance":   84,
        ///        "message": [
        ///        "este","","","mensaje",""
        ///            ]
        ///    },
        ///    {
        ///        "name": "skywalker",
        ///        "distance":   114,
        ///        "message": [
        ///        "","es","","","secreto"
        ///            ]
        ///    },
        ///    {
        ///        "name": "sato",
        ///        "distance":   120,
        ///        "message": [
        ///        "este","","un","",""
        ///           ]
        ///    },
        ///    ]
        /// }
        /// </remarks>
        /// <param name="addRequest">Contenido de la informacion de los tres satelites.</param>
        /// <returns>La posición y el mensaje si es posible descifrarlo.</returns>

        [HttpPost("topsecret")]
        [ProducesResponseType(typeof(SatelliteMessageDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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


        /// <summary>
        ///     Descifrar el mensaje y la posicion por mensajes divididos.
        /// </summary>
        /// <remarks>
        /// Este recurse permite descifrar un mensaje dividido con la información de un satélite a la vez. Es decir, devolverá un error hasta que haya sido
        ///    llamado al menos una vez todos los satelites por nombre. Es decir, en la tercera llamada contendrá los datos descifrados y devolvera la posicion junto con el mensaje emitido de la nave.
        ///
        /// Ejemplo para la 1ra request:
        ///
        ///     POST /topsecret_split/kenobi
        ///     {
        ///         "distance": 84,
        ///         "message": ["","este","es","un","mensaje"]
        ///     }
        ///
        /// Ejemplo para la 2nd request:
        ///
        ///     POST /topsecret_split/skywalker
        ///     {
        ///         "distance": 114,
        ///         "message": ["este","","un","mensaje"]
        ///     }
        ///
        /// Ejemplo para la 3ra request:
        ///
        ///     POST /topsecret_split/sato
        ///     {
        ///         "distance": 120,
        ///         "message": ["","","es","","mensaje"]
        ///     }
        /// </remarks>
        /// <param name="satelliteName">Nombre del satelite.</param>
        /// <param name="addRequest">Contenido de la informacion de un satelite.</param>
        /// <returns>La posición y el mensaje si es posible descifrarlo.</returns>
        [HttpPost("topsecret_split/{satelliteName}")]
        [ProducesResponseType(typeof(SatelliteMessageDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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
    }
}