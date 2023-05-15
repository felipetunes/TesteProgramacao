using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteProgramacao.Models
{
    public class Conta
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
    }
}