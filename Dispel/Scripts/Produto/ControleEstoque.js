var pathname = location.pathname;
if (pathname.includes("/Dispel"))
    pathname = "/Dispel";
else
    pathname = '';

function AtualizarProdutoNoEstoque(controle) {

    var referenciaProduto = $("#ReferenciaProduto").val();
    var quantidade = $("#AddQuantidade").val() !== '' ? $("#AddQuantidade").val() : $("#RmvQuantidade").val();

    $.post(pathname + "/Produto/AtualizarProdutoNoEstoque?referenciaProduto=" + referenciaProduto + "&quantidadeProduto=" + quantidade + "&controle=" + controle, function (response) {
        if (!response)
            alert("Houve um erro inesperado do sistema. Por favor, tire uma foto da tela e contate o suporte especializado.");
        else
            location.reload();
    });
}