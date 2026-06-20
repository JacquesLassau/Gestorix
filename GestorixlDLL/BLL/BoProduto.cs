using GestorixDLL.DML;
using GestorixDLL.Infrastructure;
using GestorixDLL.DAL;
using System.Collections.Generic;

namespace GestorixDLL.BLL
{
    public class BoProduto
    {
        DALProduto _dalProduto;

        public BoProduto()
        {
            _dalProduto = new DALProduto();
        }

        /// <summary>
        /// Cadastra um novo produto.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>Cadeia de caracteres.</returns>
        public string IncluirProduto(Produto produto)
        {
            produto.Situacao = Constantes.ATIVO;    
            return _dalProduto.IncluirProdutoDb(produto);
        }

        /// <summary>
        /// Editar um produto.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>Cadeia de caracteres.</returns>
        public string EditarProduto(Produto produto)
        {
            return _dalProduto.EditarProdutoDb(produto);
        }

        /// <summary>
        /// Editar um produto.
        /// </summary>
        /// <param name="produto"></param>
        /// <returns>Cadeia de caracteres.</returns>
        public string ExcluirProduto(Produto produto)
        {
            produto.Situacao = Constantes.INATIVO;
            return _dalProduto.ExcluirProdutoDb(produto);
        }

        /// <summary>
        /// Lista de produtos da base de dados.
        /// </summary>
        /// <param></param>
        /// <returns>Propriedade do tipo List<Produto></returns>
        public List<Produto> ListaProdutos()
        {
            return _dalProduto.ListarProdutosDb();
        }

        /// <summary>
        /// Seleciona um produto da base de dados.
        /// </summary>
        /// <param name="idProduto"></param>
        /// <returns>Propriedade do tipo Produto</returns>
        public Produto SelecionaProduto(string referencia)
        {
            return _dalProduto.SelecionarProdutoDb(referencia);
        }

        public string AtualizarProdutoEstoque(string referencia, string quantidade, string controle)
        {
            return _dalProduto.AtualizarProdutoEstoqueDb(referencia, quantidade, controle);
        }
    }
}