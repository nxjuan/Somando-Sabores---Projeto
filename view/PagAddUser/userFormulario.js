const url = "http://localhost:8080/user"

function postUser(){
    var name = document.getElementById('name').value;
    var email = document.getElementById('email').value;
    var cpf = document.getElementById('cpf').value;

    cpf = cpf.replace(".", "")
    cpf = cpf.replace(".", "")
    cpf = cpf.replace("-", "")


    console.log(name + " | " + email + " | " + cpf);

    var userData = {
        name: name,
        email: email,
        cpf: cpf
    }

    getAPI(userData)

}

console.log(localUser)

async function getAPI(userData){
    console.log(userData)
    const response = await fetch(
        url, 
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


