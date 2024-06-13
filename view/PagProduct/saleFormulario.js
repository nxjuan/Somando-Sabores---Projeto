const producturl = "http://localhost:8080/product";
const saleurl = "http://localhost:8080/sale";

// Obter produtos do banco de dados e criar o HTML dinâmico com botões de incremento e decremento
async function getProductsForDatabase(url) {
    const response = await fetch(url);
    const data = await response.json();
    const productsContainer = document.getElementById("products");

    data.forEach(product => {
        let descriptionsHTML = '';

        if (Array.isArray(product.descriptions)) {
            product.descriptions.forEach(descricao => {
                descriptionsHTML += `<li>${descricao}</li>`;
            });
        }

        const productHTML = `
            <div id="product_${product.id}" class="product-item">
                <div class="product-info">
                    <div class="quantity-controls">
                        <button type="button" onclick="decreaseQuantity(${product.id})">-</button>
                        <input type="text" id="quantity_${product.id}" name="quantity_${product.id}" value="0" readonly>
                        <button type="button" onclick="increaseQuantity(${product.id})">+</button>
                    </div>
                    <label for="product_${product.id}">${product.name}: R$ ${product.price}</label>
                </div>
                <ul>${descriptionsHTML}</ul>
            </div>
        `;
        productsContainer.insertAdjacentHTML("beforeend", productHTML);
    });
}

// Função para incrementar a quantidade de um produto
function increaseQuantity(productId) {
    const quantityInput = document.getElementById(`quantity_${productId}`);
    let quantity = parseInt(quantityInput.value);
    quantityInput.value = quantity + 1;
}

// Função para decrementar a quantidade de um produto
function decreaseQuantity(productId) {
    const quantityInput = document.getElementById(`quantity_${productId}`);
    let quantity = parseInt(quantityInput.value);
    if (quantity > 0) {
        quantityInput.value = quantity - 1;
    }
}

// Enviar o pedido com as quantidades selecionadas
function submitOrder() {
    const productsContainer = document.getElementById("products");
    const productItems = productsContainer.getElementsByClassName("product-item");
    const selectedProducts = [];

    for (let item of productItems) {
        const productId = item.id.split('_')[1];
        const quantity = parseInt(document.getElementById(`quantity_${productId}`).value);
        if (quantity > 0) {
            selectedProducts.push({ id: productId, quantity: quantity });
        }
    }

    const saleData = {
        user: {
            id: 1 // Substitua pelo ID real do usuário
        },
        products: selectedProducts
    };

    console.log("Dados da venda:", saleData);
    postToAPI(saleData);
}

// Enviar os dados para a API
async function postToAPI(saleData) {
    const response = await fetch(saleurl, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(saleData)
    });

    if (!response.ok) {
        throw new Error('Failed to add sale');
    }

    const data = await response.json();
    console.log(data);
}

getProductsForDatabase(producturl);
