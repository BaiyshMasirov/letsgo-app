using Application.MediatR.Accounts.Commands.RefreshPassword;
using Application.MediatR.Admins.Accounts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с авторизацией.
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : BaseController
    {
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] CreateAdminCommand command)
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
        /// <param name="command">ForgotPasswordCommand</param>
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