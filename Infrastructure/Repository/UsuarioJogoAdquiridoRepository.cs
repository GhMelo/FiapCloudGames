using System.Data;
using System.Data.Common;
using Core.DTOs;
using Core.Entity;
using Core.Repository;
using Dapper;
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
        public IEnumerable<UsuarioJogoAdquiridoDto> ObterUsuarioJogosAdquiridosUltimos60DiasDapper()
        {
            var data60Dias = DateTime.Now.Subtract(TimeSpan.FromDays(60));
            var query = "SELECT * FROM UsuarioJogoAdquirido WHERE DataCriacao > @data60Dias";
            return _dbConnectionDapper.Query<UsuarioJogoAdquiridoDto>(query, new { data60Dias }).ToList();
        }
    }
}
