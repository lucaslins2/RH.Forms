const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener("click", () => {
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
    container.classList.remove("sign-up-mode");
});
//validacao com mascara 
$("input[name=cpf]").mask("999.999.999-99");
/*$("input[name=cpf]").mask("99999999999");*/
var senha = document.getElementsByName("senha")[1];
var spanSenha = document.getElementById("spanSenha");
var spanConfimaSenha = document.getElementById("spanConfimaSenha");
var confirmaSenha = document.getElementsByName("confirmaSenha")[0];

const digito = new RegExp(/(?=.*\d)/);              // deve conter ao menos um dígito
const minusculo = new RegExp(/(?=.*[a-zA-Z])/);        // deve conter ao menos uma letra minúscula
/*const maiusculo = new RegExp(/(?=.*[A-Z])/);        // deve conter ao menos uma letra maiúscula*/
/*const especial = new RegExp(/(?=.*[$*&@#])/);  */     // deve conter ao menos um caractere especial
/*const tamanho = new RegExp(/[0-9a-zA-Z$*&@#]{8,}/); // deve conter ao menos 8 dos caracteres mencionados*/
const tamanho = new RegExp(/[0-9a-zA-Z]{8,}/); // deve conter ao menos 8 dos caracteres mencionados

function Login() {

    let $LoginModal = $("#LoginModal").val();

    if ($LoginModal == 1) {

        alertaPadraoErro("Login Invalido!");

    }
    if ($LoginModal == 2) {

        alertaPadraoSucesso("Registrado com Sucesso!");

    }

}
Login();

function validarSenha() {
    arrayValidador = [];

    document.getElementById("legendaSenha").innerHTML = "";
    document.getElementById("tituloSenha").innerHTML = "";

    if (digito.exec(senha.value) == null) {
        arrayValidador.push(1);
    };

    if (minusculo.exec(senha.value) == null) {
        arrayValidador.push(2);
    };

    //if (maiusculo.exec(senha.value) == null) {
    //    arrayValidador.push(3);
    //};

    //if (especial.exec(senha.value) == null) {
    //    arrayValidador.push(4);
    //};

    if (tamanho.exec(senha.value) == null) {
        arrayValidador.push(5);
    };

    if (arrayValidador.length > 0) {

        document.getElementById("tituloSenha").innerHTML = "A senha deve conter ao menos:";
        spanSenha.style.border = "2px solid red";

        if (confirmaSenha.value == senha.value) {
            spanConfimaSenha.style.border = "2px solid green";
        } else {
            spanConfimaSenha.style.border = "2px solid red";
        }

        switch (arrayValidador[0]) {
            case 1:
                document.getElementById("legendaSenha").innerHTML = "A senha deve conter ao menos \n um dígito";
                return false;
                break;

            case 2:
                document.getElementById("legendaSenha").innerHTML = "Uma letra";
                return false;
                break;

            case 3:
                document.getElementById("legendaSenha").innerHTML = "Uma letra maiúscula";
                return false;
                break;

            case 4:
                document.getElementById("legendaSenha").innerHTML = "Um caractere especial";
                return false;
                break;

            case 5:
                document.getElementById("legendaSenha").innerHTML = "8 caracteres";
                return false;
                break;

            default:
                document.getElementById("legendaSenha").innerHTML = "";
        }
    } else {
        spanSenha.style.border = "2px solid green";

        if (confirmaSenha.value.length > 0) {
            if (confirmaSenha.value == senha.value) {
                spanConfimaSenha.style.border = "2px solid green";
            } else {
                spanConfimaSenha.style.border = "2px solid red";
            }
        }

        return true;
    }

}

function confirmarSenha() {

    if (confirmaSenha.value == senha.value && validarSenha()) {
        spanConfimaSenha.style.border = "2px solid green";
        return true;
    } else {
        spanConfimaSenha.style.border = "2px solid red";
        return false;
    }

}


senha.addEventListener('input', (event) => {
    validarSenha();
});

confirmaSenha.addEventListener('input', (event) => {
    confirmarSenha();
});

var formCadastro = document.getElementById("cadastrar");



formCadastro.addEventListener('submit', function (evt) {
    event.preventDefault();
    if (validarSenha() && confirmarSenha()) {
        formCadastro.submit();
    } else {
        alertaPadraoAviso("A senha não atende os requisitos minimos");
    }
});
