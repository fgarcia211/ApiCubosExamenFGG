using ApiCubosExamenFGG.Data;
using ApiCubosExamenFGG.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCubosExamenFGG.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        #region METODOSCUBOS

        public async Task<List<Cubo>> GetAllCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<List<Cubo>> GetCubosMarcaAsync(string marca)
        {
            return await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
        }

        public async Task CreateCuboAsync(Cubo cubo)
        {
            if (this.context.Cubos.Count() > 0)
            {
                cubo.IdCubo = this.context.Cubos.Max(x => x.IdCubo) + 1;
            }
            else
            {
                cubo.IdCubo = 1;
            }
            
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        #endregion

        #region METODOSUSUARIOS

        public async Task CreateUsuarioAsync(UsuarioCubo user)
        {
            if (this.context.Usuarios.Count() > 0)
            {
                user.IdUsuario = this.context.Usuarios.Max(x => x.IdUsuario) + 1;
            }
            else
            {
                user.IdUsuario = 1;
            }

            this.context.Usuarios.Add(user);
            await this.context.SaveChangesAsync();
        }

        //CAMBIAR LUEGO CON TOKEN
        public async Task<UsuarioCubo> GetPerfilUsuarioAsync(int id)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.IdUsuario == id);
        }

        #endregion

        #region METODOSPEDIDOS

        //CAMBIAR LUEGO CON TOKEN
        public async Task CreatePedidoAsync(int idusuario, int idcubo)
        {
            int idpedido = 0;

            if (this.context.Compras.Count() > 0)
            {
                idpedido = this.context.Compras.Max(x => x.IdPedido) + 1;
            }
            else
            {
                idpedido = 1;
            }

            CompraCubo compra = new CompraCubo
            {
                IdPedido = idpedido,
                IdCubo = idcubo,
                IdUsuario = idusuario,
                FechaPedido = DateTime.Now
            };

            this.context.Compras.Add(compra);
            await this.context.SaveChangesAsync();
        }

        //CAMBIAR LUEGO CON TOKEN
        public async Task<List<CompraCubo>> GetComprasUsuarioAsync(int idusuario)
        {
            return await this.context.Compras.Where(x => x.IdUsuario == idusuario).ToListAsync();
        }

        #endregion

        #region METODOSLOGIN

        public async Task<UsuarioCubo> ExisteUsuarioAsync(string email, string pass)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.Pass == pass);
        }

        #endregion
    }
}
