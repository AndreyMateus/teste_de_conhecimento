// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Validação API de CEP
function limitarCEP() {
    const cepInput = document.querySelector("#cep");
    const maxLength = parseInt(cepInput.getAttribute("maxlength"));
    if (cepInput.value.length > maxLength) 
        cepInput.value = cepInput.value.slice(0, maxLength);
}
function limitarCNPJ() {
    const cepInput = document.querySelector("#cnpj");
    const maxLength = parseInt(cepInput.getAttribute("maxlength"));
    if (cepInput.value.length > maxLength) 
        cepInput.value = cepInput.value.slice(0, maxLength);
}

// Consumo API de CEP
function consultarEndereco() {
    const cep = document.querySelector("#cep").value;
    const enderecoInput = document.querySelector("#endereco");
    if (cep.length < 8) {
        enderecoInput.value = "Cep inválido";
        return;
    }

    // Consumindo a API
    let url = `https://viacep.com.br/ws/${cep}/json/`;
    fetch(url).then(function (response) {
        response.json().then(mostrarEndereco);
    });
}

function mostrarEndereco(dados) {
    const enderecoInput = document.querySelector("#endereco");
    if (dados.erro) {
        enderecoInput.innerHTML = "Não foi possível localizar endereço";
    } else {
        enderecoInput.value = dados.localidade;
    }
}

