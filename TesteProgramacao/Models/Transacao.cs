using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TesteProgramacao.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public Guid ContaID { get; set; }
        public Guid CategoriaID { get; set; }
        public string Historico { get; set; }
        public DateTime Data { get; set; }
        public decimal Debito { get; set; }
        public decimal Credito { get; set; }
        public int Conciliado { get; set; }
        public string Notas { get; set; }

        [Required(ErrorMessage = "O campo Código é obrigatório.")]
        public string ContaCodigo { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        public decimal Valor { get; set; }
        public string ValorTotal()
        {
            return (this.Credito - this.Debito).ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}