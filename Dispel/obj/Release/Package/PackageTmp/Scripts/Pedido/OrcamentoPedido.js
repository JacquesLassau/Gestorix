document.addEventListener("DOMContentLoaded", function () {
    // Preenche os produtos da nota com base na tabela principal
    let rows = window.opener.document.querySelectorAll("#pedido-table tbody tr");
    let tbody = document.querySelector("#tabela-nota tbody");
    let total = 0;

    rows.forEach(row => {
        let cols = row.querySelectorAll("td");
        let referencia = cols[1].innerText;
        let descricao = cols[2].innerText;
        let quantidade = parseFloat(cols[6].innerText);
        let preco = parseFloat(cols[4].innerText);
        let valorTotal = quantidade * preco;

        total += valorTotal;

        let tr = document.createElement("tr");
        tr.innerHTML = `
            <td><input type="text" value="${referencia}" /></td>
            <td><input type="text" value="${descricao}" /></td>
            <td><input type="text" /></td> <!-- NCM -->
            <td><input type="text" /></td> <!-- CST -->
            <td><input type="text" /></td> <!-- CFOP -->
            <td><input type="text" value="UN" /></td>
            <td><input type="number" value="${quantidade}" /></td>
            <td><input type="number" value="${preco.toFixed(2)}" /></td>
            <td><input type="number" value="${valorTotal.toFixed(2)}" /></td>
        `;
        tbody.appendChild(tr);
    });

    document.getElementById("valor-total").innerText = "R$ " + total.toFixed(2);
});
