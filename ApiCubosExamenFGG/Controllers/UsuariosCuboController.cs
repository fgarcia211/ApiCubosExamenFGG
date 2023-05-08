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
    public class UsuariosCuboController : ControllerBase
    {
        private RepositoryCubos repo;

        public UsuariosCuboController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<UsuarioCubo>> PerfilUsuario()
        {
            Claim claim = HttpContext.User.Claims.SingleOrDefault(x => x.Type == "UserData");

            return JsonConvert.DeserializeObject<UsuarioCubo>(claim.Value);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUsuario(UsuarioCubo user)
        {
            await this.repo.CreateUsuarioAsync(user);
            return Ok();
        }
    }
}
