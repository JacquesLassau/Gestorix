var pathname = location.pathname;
if (pathname.includes("/Gestorix"))
    pathname = "/Gestorix";
else
    pathname = '';

document.getElementById('buscarRelatorio').addEventListener('click', () => {
    const dataInicio = document.getElementById('dataInicio').value;
    const dataFim = document.getElementById('dataFim').value;

    if (!dataInicio || !dataFim) {
        Mensagem('Por favor, informe corretamente a <b>Data Início</b> e a <b>Data Fim</b>.', 'Verifique se as datas foram informadas corretamente.');
        return;
    }
    if (dataInicio > dataFim) {
        Mensagem('A "Data Início" não pode ser maior que a "Data Fim".', 'Verifique se as datas foram informadas corretamente.');
        return;
    }

    fetch(pathname + `/Relatorio/BuscarRelatorioAnalitico?dataInicio=${dataInicio}&dataFim=${dataFim}`)
        .then(res => res.json())
        .then(response => {
            if (response.Tabela1.length > 0) {
                popularTabelaSintetica(response.Tabela1);
                popularTabelaTotalSintetica(response.Tabela2);
                popularTabelaAnalitica(response.Tabela3);
                popularTabelaTotalAnalitica(response.Tabela4);
                popularTabelaMaisVendidos(response.Tabela5);
                popularTabelaMaiorLucro(response.Tabela6);
                popularTabelaMenorLucro(response.Tabela7);
                criarBotaoImprimirGeral();
            } else {
                return Mensagem('Não foram encontrados Pedidos no período informado.', 'Por favor, tente novamente.');
            }
        })
        .catch(err => {
            console.error(err);
            alert('Erro ao buscar o relatório.');
        });

    $('#relatorio-content').css('display', 'block');
});

function criarBotaoImprimirGeral() {
    if (document.getElementById('btn-imprimir-geral')) return; // evita duplicação
    const relatorioContent = document.getElementById('relatorio-content');
    const btnGeral = document.createElement('button');
    btnGeral.id = 'btn-imprimir-geral';
    btnGeral.textContent = 'Imprimir Todas as Tabelas';
    btnGeral.className = 'btn btn-success my-3';
    btnGeral.onclick = () => imprimirElementos([
        'tabela-sintetica',
        'tabela-analitica',
        'tabela-mais-vendidos',
        'tabela-maior-lucro',
        'tabela-menor-lucro'
    ]);
    relatorioContent.appendChild(btnGeral);
}

function popularTabelaSintetica(dados) {
    const tabela = document.getElementById('tabela-sintetica');
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="5">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Pedido Nº</th>
      <th>Finalizado</th>
      <th>Custo Total R$</th>
      <th>Preço Total R$</th>
      <th>Lucro Total R$</th>
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const dataFinalizado = parseJsonDate(item.DATaHORaFINALIZADO);
        const row = document.createElement('tr');
        row.innerHTML = `
      <td style="padding: 0.2rem 0.2rem;">${item.IdPEDIDO}</td>
      <td style="padding: 0.2rem 0.2rem;">${dataFinalizado}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.TOTAlCUSTO.toFixed(2))}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.TOTAlPEDIDO.toFixed(2))}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.TOTAlLUCRO.toFixed(2))}</td>
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
}

function popularTabelaAnalitica(dados) {
    const tabela = document.getElementById('tabela-analitica');
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="9">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Pedido Nº</th>
      <th>Finalizado</th>
      <th>Tipo</th>
      <th>Referência</th>
      <th>Descrição</th>
      <th>Custo R$</th>
      <th>Preço R$</th>
      <th>Lucro R$</th>
      <th>Qtd</th>
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const dataFinalizado = parseJsonDate(item.DATaHORaFINALIZADO);
        const row = document.createElement('tr');
        row.innerHTML = `
      <td style="padding: 0.2rem 0.2rem;">${item.IdPEDIDOiTEM}</td>
      <td style="padding: 0.2rem 0.2rem;">${dataFinalizado}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.TIPoITEM}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.IdERFiTEM}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.DESCRICAOiTEM}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.CUSTOiTEM.toFixed(2))}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.PRECOiTEM.toFixed(2))}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.LUCROiTEM.toFixed(2))}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.QTDiTEM}</td>
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
}

function popularTabelaMaisVendidos(dados) {
    const tabela = document.getElementById('tabela-mais-vendidos');
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="4">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Tipo</th>
      <th>Referência</th>
      <th>Descrição</th>      
      <th>Quantidade</th>      
      <th>Total R$</th>
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
      <td style="padding: 0.2rem 0.2rem;">${item.TpITEMtOP3}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.IdREfITEMtOP3}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.DESCRICAOtOP3}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.QUANTIDADeP3}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.LUCROP3)}</td>      
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
    adicionarBotaoImprimir(tabela, 'Produtos e Serviços Mais Vendidos');
}

function popularTabelaMaiorLucro(dados) {
    const tabela = document.getElementById('tabela-maior-lucro');
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="4">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Tipo</th>
      <th>Referência</th>
      <th>Descrição</th>
      <th>Quantidade</th>
      <th>Total R$</th>
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
      <td style="padding: 0.2rem 0.2rem;">${item.TIPoITEMTOPlUCRO}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.IdREFTOPlUCRO}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.DESCRICAoTOPlUCRO}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.QUANTIDADeP4}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.TotalLucro.toFixed(2))}</td>
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
    adicionarBotaoImprimir(tabela, 'Produtos e Serviços com Maior Lucro');
}

function popularTabelaMenorLucro(dados) {
    const tabela = document.getElementById('tabela-menor-lucro');
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="6">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Tipo</th>
      <th>Referência</th>
      <th>Descrição</th>
      <th>Quantidade</th>      
      <th>Total R$</th>
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
      <td style="padding: 0.2rem 0.2rem;">${item.TIPoITEMpREJUIZO}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.IdREFpREJUIZO}</td>
      <td style="padding: 0.2rem 0.2rem;">${item.DESCRICAOpREJUIZO}</td>      
      <td style="padding: 0.2rem 0.2rem;">${item.QUANTIDADeP5}</td>
      <td style="padding: 0.2rem 0.2rem;">${convertStringParaRealBRL(item.TotalPrejuizo.toFixed(2))}</td>
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
    adicionarBotaoImprimir(tabela, 'Produtos e Serviços com Menor Lucro');
}

function popularTabelaTotalSintetica(dados) {
    const tabela = document.getElementById("tabela-total-sintetica");
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="6">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Total de Custo R$</th>
      <th>Total de Preço R$</th>
      <th>Total de Lucro R$</th>      
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
      <td id="TotalSinteticoDeCusto">${convertStringParaRealBRL(item.TotalSinteticoDeCusto.toFixed(2))}</td>
      <td id="TotalSinteticoDePreco">${convertStringParaRealBRL(item.TotalSinteticoDePreco.toFixed(2))}</td>
      <td id="TotalSinteticoDeLucro">${convertStringParaRealBRL(item.TotalSinteticoDeLucro.toFixed(2))}</td>      
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
}

function popularTabelaTotalAnalitica(dados) {
    const tabela = document.getElementById("tabela-total-analitica");
    tabela.innerHTML = '';
    if (!dados || dados.length === 0) {
        tabela.innerHTML = '<tr><td colspan="6">Nenhum dado encontrado.</td></tr>';
        return;
    }
    // Cabeçalho
    const thead = `<thead>
    <tr>
      <th>Total de Custo R$</th>
      <th>Total de Preço R$</th>
      <th>Total de Lucro R$</th>      
    </tr>
  </thead>`;
    tabela.innerHTML += thead;

    // Corpo
    const tbody = document.createElement('tbody');
    dados.forEach(item => {
        const row = document.createElement('tr');
        row.innerHTML = `
      <td>${convertStringParaRealBRL(item.TotalAnaliticoDeCusto.toFixed(2))}</td>
      <td>${convertStringParaRealBRL(item.TotalAnaliticoDePreco.toFixed(2))}</td>
      <td>${convertStringParaRealBRL(item.TotalAnaliticoDeLucro.toFixed(2))}</td>      
    `;
        tbody.appendChild(row);
    });
    tabela.appendChild(tbody);
}

function adicionarBotaoImprimir(tabela, nomeTabela) {
    const btnExistente = tabela.nextElementSibling;
    if (btnExistente && btnExistente.classList.contains('btn-imprimir')) {
        btnExistente.remove();
    }
    const btn = document.createElement('button');
    btn.className = 'btn btn-secondary btn-imprimir my-2';
    btn.innerHTML = `<i class="fa fa-print"></i> ${nomeTabela}`;
    btn.onclick = () => imprimirElemento(tabela);
    tabela.parentNode.insertBefore(btn, tabela.nextSibling);
}

function imprimirElemento(elemento) {
    const conteudo = elemento.outerHTML;
    const janela = window.open('', '', 'width=800,height=600');
    janela.document.write(`
    <html>
      <head>
        <title>Impressão do relatório</title>
        <style>
          table { width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; }
          th, td { border: 1px solid #000; padding: 8px; text-align: left; }
          th { background-color: #f0f0f0; }
        </style>
      </head>
      <body>
        ${conteudo}
      </body>
    </html>
  `);
    janela.document.close();
    janela.focus();
    janela.print();
    janela.close();
}

function imprimirElementos(idsTabelas) {
    const conteudo = idsTabelas.map(id => {
        const el = document.getElementById(id);
        return el ? el.outerHTML : '';
    }).join('<hr style="margin:20px 0;">');

    const janela = window.open('', '', 'width=900,height=700');
    janela.document.write(`
    <html>
      <head>
        <title>Impressão dos relatórios</title>
        <style>
          table { width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; margin-bottom: 30px; }
          th, td { border: 1px solid #000; padding: 8px; text-align: left; }
          th { background-color: #f0f0f0; }
          hr { border: none; border-top: 2px solid #ccc; }
        </style>
      </head>
      <body>
        ${conteudo}
      </body>
    </html>
  `);
    janela.document.close();
    janela.focus();
    janela.print();
    janela.close();
}

function imprimirTabelaSintetica() {
    const conteudo = `
        <html>
            <head>
                <title>Impressão - Relatório Sintético</title>
                <style>
                    table { width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; margin-bottom: 30px; }
                    th, td { border: 1px solid #000; padding: 8px; text-align: left; }
                    th { background-color: #f0f0f0; }
                    hr { border: none; border-top: 2px solid #ccc; }
                </style>
            </head>
            <body>
                <h3>Relatório Sintético de Pedidos</h3>
                ${document.getElementById("tabela-sintetica").outerHTML}
                ${document.getElementById("tabela-total-sintetica").outerHTML}
            </body>
        </html>
    `;
    const janela = window.open('', '_blank');
    janela.document.write(conteudo);
    janela.document.close();
    janela.print();
}

function imprimirTabelaAnalitica() {
    const conteudo = `
        <html>
            <head>
                <title>Impressão - Relatório Analítico</title>
                <style>
                    table { width: 100%; border-collapse: collapse; font-family: Arial, sans-serif; margin-bottom: 30px; }
                    th, td { border: 1px solid #000; padding: 8px; text-align: left; }
                    th { background-color: #f0f0f0; }
                    hr { border: none; border-top: 2px solid #ccc; }
                </style>
            </head>
            <body class="p-4">
                <h3>Relatório Analítico de Itens dos Pedidos</h3>
                ${document.getElementById("tabela-analitica").outerHTML}
                ${document.getElementById("tabela-total-analitica").outerHTML}
            </body>
        </html>
    `;
    const janela = window.open('', '_blank');
    janela.document.write(conteudo);
    janela.document.close();
    janela.print();
}

function convertStringParaRealBRL(str) {
    const valor = parseFloat(str);
    const valorConvertido = valor.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    });

    return valorConvertido;
}

function parseJsonDate(jsonDate) {
    // Ex: "/Date(1748798777513)/"
    const timestamp = parseInt(jsonDate.replace(/\/Date\((\d+)\)\//, '$1'));
    const dt = new Date(timestamp);
    return dt.toLocaleDateString() + ' ' + dt.toLocaleTimeString();
}