function BuscarItem() {    
    var referencia = $('#ReferenciaProduto').val();

    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        pathname = "/Dispel";
    else
        pathname = '';

    $.ajax({
        url: pathname + "/Pedido/BuscarItemParaPedido",
        type: "GET",
        data: { referencia: $('#ReferenciaProduto').val() },
        dataType: "json",
        success: function (item) {
            if (item && item.Descricao) {
                $("#ReferenciaProduto").val(referencia);
                $("#Tipo").val(item.Tipo);
                $("#Descricao").val(item.Descricao);
                $("#Custo").val(item.Custo);
                $("#Preco").val(item.Preco);

                $("#botao-add").removeAttr("disabled");                
            } else {
                Mensagem('Não existe nenhum Produto ou Serviço com a referência "' + referencia + '" informada.',
                    'Mesmo assim é possível adicionar ao Pedido.');
            }
        }
    });
}