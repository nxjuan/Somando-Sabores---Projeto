const url = "http://localhost:8080/product";

async function getAPI(url) {
    const response = await fetch(url);
    const data = await response.json();
    const productsContainer = document.getElementById("products");

    data.forEach(product => {
        const productHTML = 
        `
            <input type="checkbox" id="option1" name="preference" value="option1">
            <label for="${product.name}">${product.name}: ${product.price}</label><br>
        `;
        productsContainer.insertAdjacentHTML("beforeend", productHTML);
    });
}


getAPI(url);