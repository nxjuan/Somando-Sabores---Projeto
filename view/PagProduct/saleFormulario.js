const producturl = "http://localhost:8080/product";
const saleurl = "http://localhost:8080/sale";

function selectedProducts() {
    const selectedProducts = [];
    const checkboxes = document.querySelectorAll('input[name="preference"]:checked');

    checkboxes.forEach(checkbox => {
        const productId = parseInt(checkbox.value);
        const productName = checkbox.getAttribute('id').replace('product_', ''); // Remover o prefixo 'product_' do ID
        selectedProducts.push({ id: productId, name: productName });
    });

    return selectedProducts;
}

function postProducts(){

    var saledata = {
        user: {
            id: 1
        },
        products: [

        ]
    }
    for (let i = 0; i < selectedProducts.length; i++) {
        saleData.products.push({ id: selectedProducts[i].id });
    }

    var teste = {
        user: {
            id: 1
        },
        products: [
            {
                id: 1
            },
            {
                id: 3
            }
        ]
    }

    console.log(teste)
    show(teste)

    getAPI(teste)   
}



async function getProductsForDatabase(url) {
    const response = await fetch(url);
    const data = await response.json();
    const productsContainer = document.getElementById("products");

    data.forEach(product => {
        const productHTML = 
        `
            <input type="checkbox" id="product_${product.id}" name="preference" value="${product.id}">
            <label for="product_${product.id}">${product.name}: ${product.price}</label><br>
        `;
        productsContainer.insertAdjacentHTML("beforeend", productHTML);
    });
}


async function getAPI(userData){
    console.log(userData)

    const response = await fetch(
        saleurl, 
        { 
            method: "POST",
            headers:{
                'Accept': 'application/json',
                'Content-Type':'application/json'
            },
            body: JSON.stringify(userData)
        },
        
    ).then(response => {
        if (!response.ok) {
            throw new Error('Failed to add user');
        }
        return response.json();
    })
    .then(data => {
        // If the user was successfully added, add it to the table
        addUserToTable(data);
    })
    .catch(error => {
        console.error('Error adding user:', error);
    });

    var data = await response.json();
    console.log(data);
    if(response) hideLoader();
    show(data);
}


getProductsForDatabase(producturl);