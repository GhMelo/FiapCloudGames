using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PromocaoDto
    {
        public int Id { get; set; }
        public int JogoId { get; set; }
        public string NomePromocao { get; set; }
        public int Porcentagem { get; set; }
        public bool PromocaoAtiva { get; set; }
        public DateTime DataCriacao { get; set; }
        public JogoDto JogoPromocao { get; set; }
    }
}
