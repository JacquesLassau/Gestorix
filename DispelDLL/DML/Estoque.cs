using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DispelDLL.DML
{
    public class Estoque
    {
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Referência:")]
        public string ReferenciaProduto { get; set; }
        public string TipoProduto { get; set; }
        public string Descricao { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public string EmailUsuarioManutencao { get; set; }
    }
}
