using Application.MediatR.Users.Events.Queries.GetEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с авторизацией.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class EventController : BaseController
    {
        /// <summary>
        /// Метод для получения событий
        /// </summary>
        /// <param name="query">GetEventsQuery</param>
        /// <returns>Список событий</returns>
        [HttpPost("get-events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] GetEventsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}