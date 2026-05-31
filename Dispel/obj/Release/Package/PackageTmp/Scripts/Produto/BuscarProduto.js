function LoadingPhysicalPath() {
    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        return pathname = "/Dispel";
    else
        return pathname = '';
}

var physicalPath = LoadingPhysicalPath();

function CriaEscutaPostMessageParaIFrameProduto() {

    var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
    var eventer = window[eventMethod];
    var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";

    eventer(messageEvent, function (e) {
        $("#Produtos").modal("hide");
        showLoading();
        setTimeout(function () {
            $("#ReferenciaProduto").val(e.data.ReferenciaProduto);
            $("#Tipo").val(e.data.TipoProduto); 
            $("#Descricao").val(e.data.Descricao);
            var custo = (e.data.CustoProduto).replace(",", ".");
            var preco = (e.data.PrecoProduto).replace(",", ".");
            $("#Custo").val(custo);
            $("#Preco").val(preco);
            hideLoading();
        }, 1000);        
        FnTxtPesquisarProduto();
    }, false);

}

function FnTxtPesquisarProduto() {
        
    var item = $("#ReferenciaProduto").val();    

    $.get(physicalPath + "/Produto/SelecionarProduto?referenciaProduto=" + item, function (data) {
        if (data.ReferenciaProduto != null) {
            $(function () {
                $("#Produtos").modal("hide");
                $("#Descricao").val(data.Descricao);
            });
        }
        else { 
            $("#Descricao").val("");
        }
    });

    physicalPath = '';

}

function SelecionarProdutoLinkModal(referenciaProduto) {

    $.get(physicalPath + "/Produto/SelecionarProduto?referenciaProduto=" + referenciaProduto, function (response) {
        parent.postMessage(response);
    });    

    physicalPath = '';
}