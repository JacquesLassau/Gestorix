using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace DispelDLL.DML
{
    public class Produto
    {
        [Required(ErrorMessage = "O preenchimento é obrigatório", AllowEmptyStrings = false)]
        [DisplayName("Referência:")]
        public string ReferenciaProduto { get; set; }
        [DisplayName("Tipo:")]
        public string TipoProduto { get; set; }
        [DisplayName("Descrição:")]
        public string Descricao { get; set; }
        [DisplayName("Fabricante:")]
        public string Fabricante { get; set; }
        [DisplayName("Custo R$:")]
        public string CustoProduto { get; set; }
        [DisplayName("Preço R$:")]
        public string PrecoProduto { get; set; }
        public string Situacao { get; set; }
        [DisplayName("Quantidade:")]
        public string Quantidade { get; set; }

        public Produto()
        {
            // do nothing
        }        

        public Produto(string referenciaProduto, string tipoProduto, string descricao, string fabricante, string custoProduto, string precoProduto)
        {
            ReferenciaProduto = referenciaProduto;
            TipoProduto = tipoProduto;
            Descricao = descricao;
            Fabricante = fabricante;
            CustoProduto = custoProduto;
            PrecoProduto = precoProduto;            
        }
    }

    public class ProdutoViewModelDML
    {
        public List<Produto> Produto { get; set; }

        public ProdutoViewModelDML()
        {
            this.Produto = new List<Produto>();
        }

        public ProdutoViewModelDML ConvertToListProduto(List<Produto> listaProduto)
        {
            ProdutoViewModelDML produtoViewModel = new ProdutoViewModelDML();
            if (listaProduto != null)
            {
                // atendenteViewModel.Atendente = listaAtendente;
                // foreach está sendo usado para CASO deseje incluir validação no carregamento dos registros via conversão
                foreach (var produto in listaProduto)
                {
                    produtoViewModel.Produto.Add(produto);
                }
            }

            return produtoViewModel;
        }
    }
}
