const detalhesPagamento = document.getElementById('detalhes-pagamento');
const opcoesPagamento = document.querySelectorAll('input[name="pagamento"]');

function atualizarCampos() {
    const opcaoSelecionada = document.querySelector('input[name="pagamento"]:checked').value;

    if (opcaoSelecionada === 'credito' || opcaoSelecionada === 'debito') {
        detalhesPagamento.innerHTML = `
            <label for="numero-cartao">Número do cartão</label>
            <input type="text" id="numero-cartao" placeholder="Digite o número do cartão" required>

            <label for="nome-titular">Nome do titular</label>
            <input type="text" id="nome-titular" placeholder="Nome impresso no cartão" required>

            <div style="display: flex; gap: 10px;">
                <div>
                    <label for="validade">Validade</label>
                    <input type="text" id="validade" placeholder="MM/AA" required>
                </div>
                <div>
                    <label for="cvv">CVV</label>
                    <input type="text" id="cvv" placeholder="Código de segurança" required>
                </div>
            </div>

            <label for="cpf-titular">CPF do titular</label>
            <input type="text" id="cpf-titular" placeholder="CPF do titular" required>

            <p><strong>Total</strong>: R$ 119,70</p>
        `;
    } else if (opcaoSelecionada === 'pix') {
        detalhesPagamento.innerHTML = `
            <img src="/view/images/qrCodePix.png" alt="QR Code Pix">
            <p>Código Pix: <strong>DSBFKDNKYGXCC7G6TGC...</strong></p>
            <button style="display: inline; background: none; border: none; color: #8b0000; cursor: pointer;">Copiar</button>
            <p><strong>Total</strong>: R$ 516,00</p>
        `;
    }
}

opcoesPagamento.forEach(opcao => {
    opcao.addEventListener('change', atualizarCampos);
});

atualizarCampos();
