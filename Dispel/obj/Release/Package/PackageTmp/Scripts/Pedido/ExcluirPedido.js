function LoadingPhysicalPath() {
    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        return pathname = "/Dispel";
    else
        return pathname = '';
}

var physicalPath = LoadingPhysicalPath();

function RemoverPedido(pIdPedido) {
    const payload = { IdPedido: pIdPedido };    
    $.post(physicalPath + "/Pedido/ExcluirPedido", payload, function () {
        window.location.href = location.href;
    });
}