using ApiCubosExamenFGG.Models;
using ApiCubosExamenFGG.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ApiCubosExamenFGG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasCuboController : ControllerBase
    {
        private RepositoryCubos repo;

        public ComprasCuboController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<List<CompraCubo>>> ComprasUsuario()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            UsuarioCubo user = JsonConvert.DeserializeObject<UsuarioCubo>(claim.Value);

            return await this.repo.GetComprasUsuarioAsync(user.IdUsuario);
        }

        [HttpPost("{idcubo}")]
        [Authorize]
        public async Task<ActionResult> CreatePedido(int idcubo)
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");
            UsuarioCubo user = JsonConvert.DeserializeObject<UsuarioCubo>(claim.Value);

            await this.repo.CreatePedidoAsync(user.IdUsuario, idcubo);
            return Ok();
        }
    }
}
