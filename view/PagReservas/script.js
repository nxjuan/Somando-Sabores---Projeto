let guestCount = 1; // Inicialmente temos apenas o responsável
let guestData = {}; // Objeto para armazenar dados dos convidados

function addGuest() {
    guestCount++;
    updateGuestFields();
}

function removeGuest() {
    if (guestCount > 1) {
        delete guestData[`guest-${guestCount}`]; // Remove os dados do último convidado
        guestCount--;
        updateGuestFields();
    }
}

function updateGuestFields() {
    const guestFields = document.getElementById("guest-fields");
    guestFields.innerHTML = ""; // Limpa os campos

    for (let i = 2; i <= guestCount; i++) {
        const guestId = `guest-${i}`;
        const guestDiv = document.createElement("div");
        guestDiv.className = "guest-field"; // Inicializa como invisível
        guestDiv.innerHTML = `
            <p>👤 Convidado ${i - 1}</p>
            <div>
            <label for="${guestId}">Nome completo</label>
            <input type="text" id="${guestId}" name="${guestId}" value="${guestData[guestId] || ''}" required>
            <hr>
            </div>
        `;
        guestFields.appendChild(guestDiv);

        // Adiciona a classe `visible` com um pequeno delay para a transição
        setTimeout(() => {
            guestDiv.classList.add("visible");
            guestDiv.querySelector("input").focus();
        }, 50);

        // Salva os dados ao alterar o valor do campo
        guestDiv.querySelector("input").addEventListener("input", (e) => {
            guestData[guestId] = e.target.value;
        });
    }

    // Atualiza o valor do contador de convidados no input
    document.getElementById("guest-count").value = guestCount;
}

function acessarResumo() {
    const inputs = document.querySelectorAll("#reservation-form input:required");
    let allFieldsValid = true;

    // Remove mensagens de erro existentes
    document.querySelectorAll(".error-message").forEach((error) => error.remove());

    for (let input of inputs) {
        if (!input.value.trim()) {
            allFieldsValid = false;

            // Adiciona uma mensagem de erro ao lado do campo vazio
            const errorMessage = document.createElement("span");
            errorMessage.className = "error-message";
            errorMessage.style.color = "red";
            errorMessage.style.fontSize = "12px";
            errorMessage.textContent = `Por favor, preencha este campo.`;
            input.parentNode.appendChild(errorMessage);

            // Destaca o campo com borda vermelha
            input.style.borderColor = "red";
        } else {
            // Remove destaque caso o campo seja preenchido
            input.style.borderColor = "";
        }
    }

    if (!allFieldsValid) {
        return; // Interrompe a função se algum campo estiver vazio
    }

    // Exibe o modal
    const modal = document.getElementById("modal-resumo");
    modal.style.display = "flex";

    // Atualizar os detalhes do resumo dinamicamente
    const reservas = document.getElementById("guest-count").value;
    const subtotal = reservas * 39.9;
    const total = subtotal;
    document.getElementById("resumo-detalhes").innerText = `${reservas} reservas`;
    document.getElementById("subtotal").innerText = `R$ ${subtotal.toFixed(2)}`;
    document.getElementById("total").innerText = `R$ ${total.toFixed(2)}`;
}

function fecharResumo() {
    const modal = document.getElementById("modal-resumo");
    modal.style.display = "none";
}



// Fechar modal ao clicar fora dele
window.addEventListener("click", (event) => {
    const modal = document.getElementById("modal-resumo");
    if (event.target === modal) {
        fecharResumo();
    }
});
