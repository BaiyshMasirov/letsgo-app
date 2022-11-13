using Application.MediatR.Users.Accounts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с авторизацией.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : BaseController
    {
        /// <summary>
        /// Метод для регистрации пользователя
        /// </summary>
        /// <param name="command">RegisterUserCommand</param>
        /// <returns>Возвращает сообщение об успешной авторизации</returns>
        /// <returns>Возвращает 400 ошибку, при неуспешном выполнении запроса</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (!result.Succeed)
                    return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState.Values);
        }

        /// <summary>
        /// Метод для авторизации пользователя
        /// </summary>
        /// <param name="command">LoginCommand</param>
        /// <returns>Возвращает сообщение  об успешном выполнении, jwt token и рефреш токен</returns>
        /// <returns>Возвращает 400 ошибку, при неуспешном выполнении запроса</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (!result.Succeed)
                    return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState.Values);
        }

        /// <summary>
        /// Метод для сброса пароля
        /// </summary>
        /// <param name="command">RefreshPasswordCommand</param>
        /// <returns>Возвращает сообщение об успешном обновлении пароля</returns>
        /// <returns>Возвращает 400 ошибку, при неуспешном выполнении запроса</returns>
        [HttpPost("refresh-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] RefreshPasswordCommand command)
        {
            if (ModelState.IsValid)
            {
                var result = await Mediator.Send(command);
                if (!result.Succeed)
                    return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState.Values);
        }
    }
}