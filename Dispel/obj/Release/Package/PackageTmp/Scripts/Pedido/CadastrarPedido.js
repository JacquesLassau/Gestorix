function LoadingPhysicalPath() {
    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        return pathname = "/Dispel";
    else
        return pathname = '';
}

var physicalPath = LoadingPhysicalPath();

const form = document.getElementById('pedido-form');
const botaoAdd = document.getElementById('botao-add');
const tableBody = document.querySelector('#pedido-table tbody');
const totalCustoDiv = document.getElementById('total-custo');
const totalDiv = document.getElementById('total-pedido');
const totalLiquidoDiv = document.getElementById('total-liquido');

let total = 0;
let totalCusto = 0;
let totalLiquido = 0;
let editandoLinha = null;

form.addEventListener('submit', function (event) {
    event.preventDefault();

    const tipo = document.getElementById('Tipo').value;
    const referencia = document.getElementById('ReferenciaProduto').value;
    const descricao = document.getElementById('Descricao').value;
    const custo = parseFloat(document.getElementById('Custo').value);
    const preco = parseFloat(document.getElementById('Preco').value);
    const quantidade = parseInt(document.getElementById('Quantidade').value);
    const precoTotal = preco * quantidade;

    if (custo > preco) {
        Mensagem('Não é possível adicionar um ' + tipo + ' com o valor de CUSTO R$ maior que o  valor de PREÇO R$.', 'Atenção a mensagem informada.')
        if (!editandoLinha)
            LimparCamposItemPedido();
        return;
    }

    verificarReferencia(referencia, tipo);

    const custoTotalItem = custo * quantidade;
    const lucro = (preco - custo) * quantidade;
    const subtotal = preco * quantidade;

    if (editandoLinha) {
        const oldPreco = parseFloat(editandoLinha.cells[4].innerText.replace('R$ ', ''));
        const oldQuantidade = parseInt(editandoLinha.cells[7].innerText);
        const oldCusto = parseFloat(editandoLinha.cells[3].innerText.replace('R$ ', ''));
        const oldSubtotal = oldPreco * oldQuantidade;
        const oldLucro = (oldPreco - oldCusto) * oldQuantidade;
        const oldCustoTotal = oldCusto * oldQuantidade;        

        total -= oldSubtotal;
        totalLiquido -= oldLucro;
        totalCusto -= oldCustoTotal;

        editandoLinha.cells[0].innerText = tipo;
        editandoLinha.cells[1].innerText = referencia;
        editandoLinha.cells[2].innerText = descricao;
        editandoLinha.cells[3].innerText = `R$ ${custo.toFixed(2)}`;
        editandoLinha.cells[4].innerText = `R$ ${preco.toFixed(2)}`;
        editandoLinha.cells[5].innerText = `R$ ${lucro.toFixed(2)}`;
        editandoLinha.cells[6].innerText = `R$ ${precoTotal.toFixed(2)}`;
        editandoLinha.cells[7].innerText = quantidade;

        total += subtotal;
        totalLiquido += lucro;
        totalCusto += custoTotalItem;

        atualizarTotais();
        editandoLinha = null;
        form.reset();
        botaoAdd.innerHTML = `<i class="fa fa-plus-circle"></i> <span>Adicionar</span>`;
        return;
    }

    const row = document.createElement('tr');
    row.innerHTML = `
        <td>${tipo}</td>
        <td>${referencia}</td>
        <td>${descricao}</td>
        <td>R$ ${custo.toFixed(2)}</td>
        <td>R$ ${preco.toFixed(2)}</td>
        <td>R$ ${lucro.toFixed(2)}</td>
        <td>R$ ${precoTotal.toFixed(2)}</td>
        <td>${quantidade}</td>
        <td>          
          <button class="edit-btn" style="width:100%"><i class="fa fa-edit"></i> Editar</button>                              
        </td>
        <td>
          <button class="remove-btn" style="width:100%"><i class="fa fa-trash"></i> Remover</button>
        </td>
      `;

    row.querySelector('.remove-btn').addEventListener('click', () => {
        tableBody.removeChild(row);
        total -= subtotal;
        totalLiquido -= lucro;
        totalCusto -= custoTotalItem;
        atualizarTotais();
        if (editandoLinha === row) {
            editandoLinha = null;
            form.reset();
        }
    });

    row.querySelector('.edit-btn').addEventListener('click', () => {
        document.getElementById('Tipo').value = tipo;
        document.getElementById('ReferenciaProduto').value = referencia;
        document.getElementById('Descricao').value = descricao;
        document.getElementById('Custo').value = custo;
        document.getElementById('Preco').value = preco;
        document.getElementById('Quantidade').value = quantidade;
        editandoLinha = row;
        botaoAdd.innerHTML = `<i class="fa fa-check-square-o"></i> <span>Salvar</span>`;
        botaoAdd.removeAttribute('disabled');
    });

    showLoading();
    setTimeout(function () {
        tableBody.appendChild(row);
        total += subtotal;
        totalLiquido += lucro;
        totalCusto += custoTotalItem;
        atualizarTotais();
        form.reset();
        hideLoading()
    }, 1000);
});

function LimparCamposItemPedido() {
    $('#ReferenciaProduto').val('');
    $('#Descricao').val('');
    $('#Custo').val('');
    $('#Preco').val('');
    $('#Quantidade').val('');
}

function atualizarTotais() {
    totalCustoDiv.textContent = `Custo Total: R$ ${totalCusto.toFixed(2)}`;
    totalDiv.textContent = `Preço Total: R$ ${total.toFixed(2)}`;
    totalLiquidoDiv.textContent = `Lucro Total: R$ ${totalLiquido.toFixed(2)}`;
}

document.getElementById('finalizar-pedido').addEventListener('click', function () {
    const linhas = document.querySelectorAll('#pedido-table tbody tr');
    const itensPedido = [];

    linhas.forEach(linha => {
        const tipo = linha.cells[0].innerText;
        const referencia = linha.cells[1].innerText;
        const descricao = linha.cells[2].innerText;
        const custo = parseFloat(linha.cells[3].innerText.replace('R$ ', '').replace(',', '.'));
        const preco = parseFloat(linha.cells[4].innerText.replace('R$ ', '').replace(',', '.'));
        const lucro = parseFloat(linha.cells[5].innerText.replace('R$ ', '').replace(',', '.'));
        const quantidade = parseInt(linha.cells[7].innerText);

        itensPedido.push({
            Tipo: tipo,
            Referencia: referencia,
            Descricao: descricao,
            Custo: custo,
            Preco: preco,
            Lucro: lucro,
            Quantidade: quantidade
        });
    });
    
    const url = idPedidoEditando
        ? "/Pedido/AtualizarPedido"
        : "/Pedido/CadastrarPedido";

    const payload = {
        ItensPedido: itensPedido,
        TotalCusto: totalCusto,
        TotalPedido: total,
        TotalLucro: totalLiquido
    };

    if (idPedidoEditando) payload.IdPedido = idPedidoEditando;    

    $.post(physicalPath + url, payload, function () {
        window.location.href = location.href;
    });

});

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

aplicarMascaraDecimal(document.getElementById("Custo"));
aplicarMascaraDecimal(document.getElementById("Preco"));