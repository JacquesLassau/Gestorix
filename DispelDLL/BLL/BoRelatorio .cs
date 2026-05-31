using DispelDLL.Infrastructure;
using System;
using System.Collections.Generic;
using DispelDLL.DML;
using DispelDLL.DAL;
using System.Data;

namespace DispelDLL.BLL
{
    public class BoRelatorio
    {
        DALRelatorio _daoRelatorio;
        public BoRelatorio()
        {
            _daoRelatorio = new DALRelatorio();
        }

        /// <summary>
        /// Consulta de relatório analitico dos pedidos por intervalo de datas
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Pedido></returns>
        public DataSet RelatorioAnaliticoPedidos(DateTime dataInicial, DateTime dataFinal)
        {
            DateTime dataHoraFinal = dataFinal.AddHours(23);
            dataHoraFinal = dataHoraFinal.AddMinutes(59);
            dataHoraFinal = dataHoraFinal.AddSeconds(59);

            return _daoRelatorio.RelatorioAnaliticoPedidos(dataInicial, dataHoraFinal);
        }
    }
}
