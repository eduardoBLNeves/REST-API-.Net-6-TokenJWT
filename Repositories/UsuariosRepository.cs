using API.Models;
using API.Models.Entities;
using API.Models.Entities.Usuarios;
using API.Services;
using DevOne.Security.Cryptography.BCrypt;
using Newtonsoft.Json;

namespace API.Repositories
{
    public interface IUsuariosRepository
    {
        public ResponseContent Post(PostUsuario usuario);
        public ResponseContent GetOne(GetUsuario usuario);
        public List<Usuarios> GetAll();
    }
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly _DbContext db;
        private readonly Retornos retornos;
        public UsuariosRepository(_DbContext _db, Retornos _retornos)
        {
            db = _db;
            retornos = _retornos;
        }

        public ResponseContent Post(PostUsuario usuario)
        {
            usuario.TrimAll();
            try
            {
                if (!usuario.VerificaCampos())
                    return new ResponseContent { Status = Status.Erro, Content = retornos.DadosInvalidos() };

                if (db.Usuarios.Where(x => x.email == usuario.email).FirstOrDefault() != null)
                    return new ResponseContent { Status = Status.Erro, Content = retornos.ContaExistente() };

                var senhaCriptografada = BCryptHelper.HashPassword(usuario.senha, BCryptHelper.GenerateSalt(7));
                var novoUsuario = new Usuarios()
                {
                    nome = usuario.nome,
                    email = usuario.email,
                    senha = senhaCriptografada,
                    sexo = usuario.sexo,
                    nascimento = DateTime.Parse($"{usuario.ano}-{usuario.mes}-{usuario.dia}")
                };
                var usuario_bd = db.Usuarios.Add(novoUsuario);
                db.SaveChanges();
                var token = new TokenServices().GerarToken(usuario_bd.Entity, false);
                return new ResponseContent { Status = Status.Ok, Content = token };
            }
            catch
            {
                return new ResponseContent { Status = Status.Erro, Content = retornos.ErroInterno() };
            }
        }

        public ResponseContent GetOne(GetUsuario usuario)
        {
            try
            {
                usuario.TrimAll();

                if (!usuario.VerificaCampos())
                    return new ResponseContent { Status = Status.Erro, Content = retornos.DadosInvalidos() };

                var usuario_DB = db.Usuarios.Where(x => x.email == usuario.email).FirstOrDefault();
                if (usuario_DB == null || !BCryptHelper.CheckPassword(usuario.senha, usuario_DB.senha))
                    return new ResponseContent { Status = Status.Erro, Content = retornos.LoginInvalido() };

                var token = new TokenServices().GerarToken(usuario_DB, usuario.manterLogin);

                return new ResponseContent { Status = Status.Ok, Content = token };
            }
            catch
            {
                return new ResponseContent { Status = Status.Erro, Content = retornos.ErroInterno() };
            }
        }

        public List<Usuarios> GetAll()
        {
            try
            {
                var usuarios = db.Usuarios.ToList();
                usuarios = usuarios.Select(x => { x.senha = ""; return x; }).ToList();
                return usuarios;
            }
            catch
            {
                return new List<Usuarios>();
            }
        }


    }
}
