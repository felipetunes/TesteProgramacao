using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteProgramacao.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "O campo ContaID é obrigatório.")]
        //public string ContaID { get; set; }
        //[Required(ErrorMessage = "O campo CategoriaID é obrigatório.")]
        //public string CategoriaID { get; set; }
        //[Required(ErrorMessage = "O campo Historico é obrigatório.")]
        //public string Historico { get; set; }
        public DateTime Data { get; set; }
        //public string Debito { get; set; }
        //public string Credito { get; set; }
        public int Conciliado { get; set; }
        public string Notas { get; set; }
    }
}