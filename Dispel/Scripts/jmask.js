// # Region Produtos
$('#CustoProduto').mask('#.##0,00', { reverse: true });
$('#PrecoProduto').mask('#.##0,00', { reverse: true });
$('#AddQuantidade').mask('999999');
$('#RmvQuantidade').mask('999999');
// # EndRegion

// # Region Servicos
$('#CustoServico').mask('#.##0,00', { reverse: true });
// # EndRegion

$('#custo, #preco').mask('0000000.00', {
    reverse: true,
    translation: {
        '#': { pattern: /[0-9]/, recursive: true }
    }
});