using App.Domain.Commands.Handlers;
using App.Domain.Commands.Request.User;
using App.Domain.Commands.Response;
using App.Domain.Services;
using App.Infrastructure.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.WebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthenticateController : BaseController
    {
        private readonly UserCommandHandler _handler;
        private readonly IJWTService _service;

        public AuthenticateController(IUow uow, UserCommandHandler handler, IJWTService service)
            : base(uow)
        {
            _handler = handler;
            _service = service;

        }

        /// <summary>
        /// Retorna as informações do(a) usuário(a) pelo email e senha.
        /// </summary>
        /// <response code="200">As informações do(a) usuário(a) que retornou com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AuthenticateUserRequest command)
        {
            var data = await _handler.Handle(command.Email, command.Password);

            if (data != null)
            {
                data.Token = _service.GenerateBearerToken(data);

                var response = new
                {
                    id = data.Id,
                    name = data.Name,
                    email = data.Email,
                    gender = data.Gender.ToString(),
                    role = data.Role,
                    token = data.Token
                };

                return await Response(response, _handler.Notifications);
            }

            return await Response(data, _handler.Notifications);

        }

        /// <summary>
        /// Cria um novo usuário (apenas para Admin).
        /// </summary>
        /// <response code="200">As informações do(a) usuário(a) que foi criado com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequest command)
        {
            var data = await _handler.Handle(command);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Atualiza informações de um novo usuário (apenas para Admin).
        /// </summary>
        /// <response code="200">As informações do(a) usuário(a) que foi atualizado com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpPut]
        [Route("Put")]
        [ProducesResponseType(typeof(ICollection<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequest command)
        {
            var data = await _handler.Handle(command);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Remove um usuário (apenas para Admin).
        /// </summary>
        /// <response code="200">As informações do(a) usuário(a) que foi removido(a) com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(ICollection<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest command)
        {
            var data = await _handler.Handle(command);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Retorna todos os usuários (apenas para Admin).
        /// </summary>
        /// <response code="200">A lista dos usuários foram retornados com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(ICollection<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await _handler.Handle();
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }
    }
}
