function LoadingPhysicalPath() {
    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        return pathname = "/Dispel";
    else
        return pathname = '';
}
var physicalPath = LoadingPhysicalPath();
function AtualizarProdutoNoEstoque(controle) {
    
    var referenciaProduto = $("#ReferenciaProduto").val();
    var quantidade = $("#AddQuantidade").val() !== '' ? $("#AddQuantidade").val() : $("#RmvQuantidade").val();    

    $.post(physicalPath + "/Produto/AtualizarProdutoNoEstoque?referenciaProduto=" + referenciaProduto + "&quantidadeProduto=" + quantidade + "&controle=" + controle, function (response) {
            if (!response)
                alert("Houve um erro inesperado do sistema. Por favor, tire uma foto da tela e contate o suporte especializado.");
            else
                location.reload();
        });
}