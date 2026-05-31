var pathname = location.pathname;
if (pathname.includes("/Dispel"))
    pathname = "/Dispel";
else
    pathname = '';

function RemoverPedido(pIdPedido) {
    const payload = { IdPedido: pIdPedido };
    $.post(pathname + "/Pedido/ExcluirPedido", payload, function () {
        window.location.href = location.href;
    });
}