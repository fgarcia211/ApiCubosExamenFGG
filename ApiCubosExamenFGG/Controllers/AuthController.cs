using ApiCubosExamenFGG.Helpers;
using ApiCubosExamenFGG.Models;
using ApiCubosExamenFGG.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCubosExamenFGG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryCubos repo;
        private HelperOAuthToken helper;

        public AuthController(RepositoryCubos repo, HelperOAuthToken helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            UsuarioCubo user = await this.repo.ExisteUsuarioAsync(model.Email,model.Pass);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials =new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);

                string jsonUser = JsonConvert.SerializeObject(user);
                Claim[] informacion = new[]
                {
                    new Claim("UserData", jsonUser)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: informacion,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    notBefore: DateTime.UtcNow
                );

                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }
    }
}
