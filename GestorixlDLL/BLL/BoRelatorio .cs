using GestorixDLL.Infrastructure;
using System;
using System.Collections.Generic;
using GestorixDLL.DML;
using GestorixDLL.DAL;
using System.Data;

namespace GestorixDLL.BLL
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
