using ApiCubosExamenFGG.Models;
using ApiCubosExamenFGG.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCubosExamenFGG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> GetAllCubos()
        {
            return await this.repo.GetAllCubosAsync();
        }

        [HttpGet]
        [Route("[action]/{marca}")]
        public async Task<ActionResult<List<Cubo>>> GetCubosMarca(string marca)
        {
            return await this.repo.GetCubosMarcaAsync(marca);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCubo(Cubo cubo)
        {
            await this.repo.CreateCuboAsync(cubo);
            return Ok();
        }
    }
}
