let idPedidoEditando = null;

function EditarPedido(idPedido) {
    idPedidoEditando = idPedido;    

    var pathname = location.pathname;
    if (pathname.includes("/Dispel"))
        pathname = "/Dispel";
    else
        pathname = '';

    $.get(pathname + `/Pedido/ObterPedido`, { idPedido: idPedido }, function (response) {
        const itens = response.itensPedido;

        // Limpa a tabela atual e variáveis de totais
        tableBody.innerHTML = '';
        total = 0;
        totalCusto = 0;
        totalLiquido = 0;

        itens.forEach(item => {
            const custoTotalItem = item.Custo * item.Quantidade;
            const lucro = (item.Preco - item.Custo) * item.Quantidade;
            const subtotal = item.Preco * item.Quantidade;            

            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${item.Tipo}</td>
                <td>${item.Referencia}</td>
                <td>${item.Descricao}</td>
                <td>R$ ${item.Custo.toFixed(2)}</td>
                <td>R$ ${item.Preco.toFixed(2)}</td>
                <td>R$ ${lucro.toFixed(2)}</td>
                <td>R$ ${subtotal}</td>
                <td>${item.Quantidade}</td>
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
            });

            row.querySelector('.edit-btn').addEventListener('click', () => {
                document.getElementById('Tipo').value = item.Tipo;
                document.getElementById('ReferenciaProduto').value = item.Referencia;
                document.getElementById('Descricao').value = item.Descricao;
                document.getElementById('Custo').value = item.Custo;
                document.getElementById('Preco').value = item.Preco;
                document.getElementById('Quantidade').value = item.Quantidade;
                editandoLinha = row;
                botaoAdd.innerHTML = `<i class="fa fa-check-square-o"></i> <span>Salvar</span>`;                 
            });

            tableBody.appendChild(row);
            document.getElementById('alerta-edicao').style.display = 'block';

            $('#nrOrcamento').text(item.IdPedido);

            total += subtotal;
            totalLiquido += lucro;
            totalCusto += custoTotalItem;
        });

        atualizarTotais();
    });
}