/*
 * Carregamento Inicial Dinâmico
 * Lista de Produtos
 */

var listProdutos = produtos;
var gridProdutos = document.getElementById("CarregarProdutos");

if (listProdutos !== '') {
    for (let i = 0; i < listProdutos.Produto.length; i++) {

        var row = document.createElement("tr");
        var id = document.createElement("td");
        var descricao = document.createElement("td");
        var areaSelecione = document.createElement("td");
        var btnSelecione = document.createElement("button");

        btnSelecione.className = "btn btn-outline-success btn-sm";
        btnSelecione.setAttribute("name", "lnkReferenciaProduto");
        btnSelecione.setAttribute("id", "lnkReferenciaProduto-" + listProdutos.Produto[i].ReferenciaProduto);
        btnSelecione.setAttribute("onclick", "SelecionarProdutoLinkModal('" + listProdutos.Produto[i].ReferenciaProduto + "')");
        btnSelecione.setAttribute("value", listProdutos.Produto[i].ReferenciaProduto);
        btnSelecione.innerText = "Selecione";

        var conteudoReferencia = document.createTextNode(listProdutos.Produto[i].ReferenciaProduto);
        var conteudoDescricao = document.createTextNode(listProdutos.Produto[i].Descricao);

        id.appendChild(conteudoReferencia);
        descricao.appendChild(conteudoDescricao);

        row.appendChild(id);
        row.appendChild(descricao);
        row.appendChild(areaSelecione);
        areaSelecione.appendChild(btnSelecione);
        gridProdutos.appendChild(row);
    }
}