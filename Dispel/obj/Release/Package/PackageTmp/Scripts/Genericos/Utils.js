function LoadingPhysicalPath() {
    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        return pathname = "/Dispel";
    else
        return pathname = '';
}

var physicalPath = LoadingPhysicalPath();

function RecarregaPagina() {
    window.location.href = location.href;
}

function showLoading() {
    document.getElementById('loading').style.display = 'flex';
}

function hideLoading() {
    document.getElementById('loading').style.display = 'none';
}

function Mensagem(titulo, descricao) {
    $.alert({
        title: titulo,
        content: descricao,
        type: 'warning',
        typeAnimated: true,
        containerFluid: true,
        columnClass: 'col-lg-6 col-md-6 col-sm-6 col-xs-8',
        animationBounce: 2,
    });
}

function verificarReferencia(referencia, tipo) {
    try {

        $.get(physicalPath + "/Produto/SelecionarProduto?referenciaProduto=" + referencia, function (data) {
            if (data != null && data.ReferenciaProduto != null) {
                // Do Nothing
            } else {                
                Mensagem(
                    'O ' + tipo + ' de referencia "' + referencia + '" <b>não existe</b> no Sistema.',
                    'Mesmo assim foi adicionado ao Pedido, porém, será visualizado <b>APENAS no Relatório.</b>'
                );                
            }
        });

    } catch (error) {
        console.error("Produto adicionado sem consulta na base de dados.", error);
        return true;
    }
}

function aplicarMascaraDecimal(campo) {
    campo.addEventListener("input", function () {
        let valor = this.value.replace(/\D/g, "");
        if (!valor) {
            this.value = "";
            return;
        }
        let numero = parseInt(valor, 10);
        let formatado = (numero / 100).toFixed(2);
        this.value = formatado.replace(",", ".");
    });
}