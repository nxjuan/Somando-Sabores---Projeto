const producturl = "http://localhost:8080/product";
const saleurl = "http://localhost:8080/sale";

const jsonDoProduto = document.getElementById("Show")


function getSelectedProductIds() {
    event.preventDefault();
    const div = document.getElementById(identificador);

    const elementos = div.children;

    // Itera sobre os elementos e obtém o id de cada um
    for (let i = 0; i < elementos.length; i++) {
        const id = elementos[i].id;
        `
            <p>${elementos}</p>
        `
    }
}
// --------------------------------------------------------------
function viewSelecteds(){

    event.preventDefault();

    event.preventDefault();

    const form = document.getElementById("products");
    const checkboxes = form.querySelectorAll('input[type="checkbox"]:checked');
    const selectedProductIds = [];
    checkboxes.forEach(checkbox => {
        selectedProductIds.push(checkbox.value);
    });

    console.log("Produtos selecionados:", selectedProductIds);

    // console.log("345346")

    var saleData = {
        user: {
            id: 1
        },
        products: selectedProductIds.map(id => ({ id }))
    }

    console.log(saleData)
    postToAPI(saleData)
    
}
// --------------------------------------------------------------

async function getProductsForDatabase(url) {
    const response = await fetch(url);
    const data = await response.json();
    const productsContainer = document.getElementById("products");

    data.forEach(product => {
        const productHTML =        
        `
            <div class="checkbox-wrapper-10">
                <input class="tgl tgl-flip" id="cb5" type="checkbox" checked />
                <label class="tgl-btn" data-tg-off="Remover: ${product.name}" data-tg-on="${product.name}" for="cb5"></label>
            </div>

        `;
        productsContainer.insertAdjacentHTML("beforeend", productHTML);
    });
}
// --------------------------------------------------------------
async function postToAPI(saleData){
    console.log(saleData)
    const response = await fetch(
        saleurl, 
        { 
            method: "POST",
            headers:{
                'Accept': 'application/json',
                'Content-Type':'application/json'
            },
            body: JSON.stringify(saleData)
        },        
    ).then(response => {
        if (!response.ok) {
            throw new Error('Failed to add sale');
        }
        return response.json();
    })
    .catch(error => {
        console.error('Error adding sale:', error);
    });

    var data = await response.json();
    console.log(data);

}

getProductsForDatabase(producturl);

