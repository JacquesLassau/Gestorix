using GestorixDLL.DAL;
using GestorixDLL.DML;
using GestorixDLL.Infrastructure;

namespace GestorixDLL.BLL
{
    public class BoEmpresa
    {
        DALEmpresa _dalEmpresa;

        public BoEmpresa()
        {
            _dalEmpresa = new DALEmpresa(); 
        }

        /// <summary>
        /// Seleciona uma empresa no banco de dados.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns>Propriedade do tipo Empresa/returns>
        public Empresa SelecionaEmpresa(string cnpj)
        {
            return _dalEmpresa.ConsultarEmpresaDb(cnpj);
        }

        /// <summary>
        /// Editar uma Empresa.
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns>Cadeia de caracteres contendo uma mensagem de sucesso ou de erro</returns>
        public string EditarEmpresa(Empresa empresa)
        {
            return _dalEmpresa.EditarEmpresaDb(empresa);
        }
    }
}
