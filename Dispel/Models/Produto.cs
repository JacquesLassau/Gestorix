using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace Dispel.Models
{
    public class Produto
    {
        [Required(ErrorMessage = "O preenchimento é obrigatório.", AllowEmptyStrings = false)]
        [DisplayName("Referência:")]
        public string ReferenciaProduto { get; set; }
        [DisplayName("Tipo:")]
        public string TipoProduto { get; set; }
        [DisplayName("Descrição:")]
        [Required(ErrorMessage = "O preenchimento é obrigatório.", AllowEmptyStrings = false)]
        public string Descricao { get; set; }
        [DisplayName("Fabricante:")]
        public string Fabricante { get; set; }
        [DisplayName("Custo R$:")]
        [Required(ErrorMessage = "O preenchimento é obrigatório.", AllowEmptyStrings = false)]
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
                // produtoViewModel.Produto = listaProduto;
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
