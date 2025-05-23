using System.Data;
using Dapper;
using Domain.Entity;
using Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UsuarioJogoAdquiridoRepository : EFRepository<UsuarioJogoAdquirido>, IUsuarioJogoAdquiridoRepository
    {
        private readonly IDbConnection _dbConnectionDapper;
        public UsuarioJogoAdquiridoRepository(ApplicationDbContext context) : base(context)
        {
            _dbConnectionDapper = context.Database.GetDbConnection();
        }
        public IEnumerable<UsuarioJogoAdquirido> ObterUsuarioJogosAdquiridosUltimos60DiasDapper()
        {
            var data60Dias = DateTime.Now.Subtract(TimeSpan.FromDays(60));
            var query = "SELECT * FROM UsuarioJogoAdquirido WHERE DataCriacao > @data60Dias";
            return _dbConnectionDapper.Query<UsuarioJogoAdquirido>(query, new { data60Dias }).ToList();
        }
    }
}
