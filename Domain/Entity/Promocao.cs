﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Promocao : EntityBase
    {
        public string NomePromocao { get; set; } = null!;
        [Range(0, 100, ErrorMessage = "A porcentagem deve estar entre 0 e 100.")]
        public int Porcentagem { get; set; }
        public bool PromocaoAtiva { get; set; }
        public int JogoId { get; set; }
        public virtual Jogo JogoPromocao { get; set; } = null!;
    }
}
