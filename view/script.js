const url = "http://localhost:8080/sale/2";

var localUser = null;

function hideLoader(){
    document.getElementById("loading").style.display = "none";
}

function show(sales){

    let tab = 
        `
            <thead>
                <td>Id</td>
                <td>date</td>
                <td>price</td>
                <td>user_id</td>
            </thead>

            <tbody>
                <td>${sales.id}</td>
                <td>${sales.date}</td>
                <td>${sales.value}</td>
                <td>${sales.user.id}</td>
            </tbody>
        `

    document.getElementById("sale").innerHTML = tab;

}

async function getAPI(url){
    const response = await fetch(
        url,
        { method: "GET" },
    );

    var data = await response.json();
    console.log(data);
    if(response) hideLoader();
    show(data);
}

getAPI(url);