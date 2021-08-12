using App.Domain.Commands.Handlers;
using App.Domain.Commands.Request.Provider;
using App.Domain.Commands.Response;
using App.Infrastructure.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.WebApi.Controllers
{
    [Authorize(Roles = "User, Admin")]
    public class ProviderController : BaseController
    {
        private readonly ProviderCommandHandler _handler;

        public ProviderController(IUow uow, ProviderCommandHandler handler)
            : base(uow)
        {
            _handler = handler;
        }

        /// <summary>
        /// Retorna todos os fornecedores.
        /// </summary>
        /// <response code="200">A lista dos fornecedores que foram retornadas com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpGet]
        [Route("Get")]
        [ProducesResponseType(typeof(ICollection<ProviderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var data = await _handler.Handle();
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Retorna as informações do fornecedor pelo nome.
        /// </summary>
        /// <response code="200">As informações ddo fornecedor que retornou com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpGet]
        [Route("GetByName/{name}")]
        [ProducesResponseType(typeof(ProviderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByName(string name)
        {
            var request = new SelectProviderByNameRequest
            {
                Name = name
            };

            var data = await _handler.Handle(request);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Retorna o fornecedor pelo cnpj.
        /// </summary>
        /// <response code="200">As informações do fornecedor que foram retornadas com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na consulta.</response>
        [HttpGet]
        [Route("GetByCnpj/{cnpj}")]
        [ProducesResponseType(typeof(ProviderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCnpj(string cnpj)
        {
            var request = new SelectProviderByCnpjRequest
            {
                Cnpj = cnpj
            };

            var data = await _handler.Handle(request);

            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Insere um fornecedor.
        /// </summary>
        /// <response code="200">As informações do fornecedor inserido com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na inclusão.</response>
        [HttpPost]
        [Route("Save")]
        [ProducesResponseType(typeof(ProviderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] RegisterProviderRequest command)
        {
            var data = await _handler.Handle(command);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Atualiza um fornecedor.
        /// </summary>
        /// <response code="200">As informações do fornecedor atualizado com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na inclusão.</response>
        [HttpPut]
        [Route("Put")]
        [ProducesResponseType(typeof(ProviderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateProviderRequest command)
        {
            var data = await _handler.Handle(command);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }

        /// <summary>
        /// Remove um fornecedor.
        /// </summary>
        /// <response code="200">As informações do fornecedor removido com sucesso.</response>
        /// <response code="500">Ocorreu um erro interno na inclusão.</response>
        [HttpDelete]
        [Route("Delete")]
        [ProducesResponseType(typeof(ProviderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] DeleteProviderRequest command)
        {
            var data = await _handler.Handle(command);
            if (data != null)
                return await Response(data, _handler.Notifications);

            return await Response(data, _handler.Notifications);
        }
    }
}
