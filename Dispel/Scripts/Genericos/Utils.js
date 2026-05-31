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

        var pathname = location.pathname;
        if (pathname.includes("/Dispel"))
            pathname = "/Dispel";
        else
            pathname = '';

        $.get(pathname + "/Produto/SelecionarProduto?referenciaProduto=" + referencia, function (data) {
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

$('#Cep').on('blur', function () {
    let cep = $(this).val().replace(/\D/g, '');

    if (cep.length !== 8) {
        Mensagem('CEP inválido', 'Por favor, preencha os dados corretamente.');
        return;
    }

    $.ajax({
        url: `https://viacep.com.br/ws/${cep}/json/`,
        dataType: 'jsonp',
        success: function (data) {

            if (data.erro) {
                $('#atencaoCep').css('display', 'block');
                $('#atencaoCep').css('visibility', 'visible');
                $('#atencaoCep').css('font-size', 'small');
                return;
            }

            $('#Logradouro').val(data.logradouro);
            $('#Bairro').val(data.bairro);
            $('#Cidade').val(data.localidade);
            $('#Estado').val(data.uf);
        }
    });
});