$("#cep").mask("99999-999");
$("#telefoneFixo").mask("9999-9999");
$("#celular").mask("99999-9999");

$("#RG").mask("99.999.999-9");

$('#telefoneEmpresa').mask('0000-0000', {
    onKeyPress: function (cpfcnpj, e, field, options) {
        const masks = ['0000-00000', '00000-0000'];
        const mask = (cpfcnpj.length > 9) ? masks[1] : masks[0];
        $('#telefoneEmpresa').mask(mask, options);
    }
});



var contadorEmpregosAntigos = document.getElementById('contadorEmpregos').value;
var divEmpregosAnteriores = document.getElementById('empregosAnteriores');

function addEmpregoAntigo() {

    if (contadorEmpregosAntigos < 3) {

        if (contadorEmpregosAntigos > 0) {
            divEmpregosAnteriores.innerHTML += `<hr id="empregosAnterioresSessaoHR${contadorEmpregosAntigos}" />`
        }

        divEmpregosAnteriores.innerHTML += `
                <div id="empregosAnterioresSessao${contadorEmpregosAntigos}">
                    <div class="d-flex flex-row justify-content-between w-100 div-responsivo">
                        <div class="w-100 p-3">
                            <label for="empresa" class="form-label">Empresa:</label>
                            <input type="text" class="form-control" id="empresa" name="empresa[${contadorEmpregosAntigos}]" placeholder="Insira a empresa">
                        </div>
                        <div class="w-100 p-3">
                            <label for="telefoneEmpresa" class="form-label">Telefone:</label>
                            <input type="text" class="form-control" id="telefoneEmpresa" name="telefoneEmpresa[${contadorEmpregosAntigos}]" placeholder="Insira o número de telefone">
                        </div>
                    </div>

                    <div class="d-flex flex-row justify-content-between w-100 div-responsivo">
                        <div class="w-100 p-3">
                            <label for="contato" class="form-label">Contato:</label>
                            <input type="text" class="form-control" id="contato" name="contato[${contadorEmpregosAntigos}]" placeholder="Insira o contato">
                        </div>
                        <div class="w-100 p-3">
                            <label for="setor" class="form-label">Setor:</label>
                            <input type="text" class="form-control" id="setor" name="setor[${contadorEmpregosAntigos}]" placeholder="Insira o setor">
                        </div>
                    </div>

                    <div class="d-flex flex-row justify-content-between w-100 div-responsivo">
                        <div class="w-100 p-3">
                            <label for="cargoExercido" class="form-label">Cargo que Exerceu:</label>
                            <input type="text" class="form-control" id="cargoExercido" name="cargoExercido[${contadorEmpregosAntigos}]" placeholder="Insira o cargo que exerceu">
                        </div>
                        <div class="w-100 p-3">
                            <label for="enderecoEmpresa" class="form-label">Endereço da Empresa:</label>
                            <input type="text" class="form-control" id="enderecoEmpresa" name="enderecoEmpresa[${contadorEmpregosAntigos}]" placeholder="Insira o endereço da empresa">
                        </div>
                    </div>

                    <div class="d-flex flex-row justify-content-between w-100 div-responsivo">
                        <div class="w-100 p-3">
                            <label for="dataAdmissao" class="form-label">Data de Admissão:</label>
                            <input type="date" class="form-control" id="dataAdmissao" name="dataAdmissao[${contadorEmpregosAntigos}]">
                        </div>
                        <div class="w-100 p-3">
                            <label for="dataDemissao" class="form-label">Data de Demissão:</label>
                            <input type="date" class="form-control" id="dataDemissao" name="dataDemissao[${contadorEmpregosAntigos}]">
                        </div>
                    </div>

                    <div class="d-flex flex-row justify-content-between w-100 div-responsivo">
                        <div class="w-50 p-3 input-responsivo">
                            <label for="motivoSaida" class="form-label">Motivo da Saída:</label>
                            <input type="text" class="form-control" id="motivoSaida" name="motivoSaida[${contadorEmpregosAntigos}] placeholder="Insira o motivo da saída">
                            <input type="hidden" name="QtdExp[${contadorEmpregosAntigos}]">
                        </div>
                    </div>
                `

        if (contadorEmpregosAntigos == 0) {
            var divBtn = document.getElementById("divBtn");
            divBtn.innerHTML += `
                <div type="button" class="d-flex align-items-center flex-row icon" id="btnRemover" onclick="removeEmpregoAntigo()">
                    <img src="../img/menos.png" alt="" width="48px" height="48px">
                    <p>Remover uma nova experiencia</p>
                </div>
            `

        }
        contadorEmpregosAntigos++;


        let heightPage = document.getElementsByClassName("scroll")[0];
        heightPage.scrollTo(0, heightPage.scrollHeight);
    } else {
        alertaPadraoAviso("Você so pode adicionar 3 experiencias");
    }
}

function removeEmpregoAntigo() {
    if (contadorEmpregosAntigos > 0) {

        var node = document.getElementById(`empregosAnterioresSessao${contadorEmpregosAntigos - 1}`);
        var node2 = document.getElementById(`empregosAnterioresSessaoHR${contadorEmpregosAntigos - 1}`);

        var btnRemover = document.getElementById("btnRemover");

        if (node.parentNode) {
            node.parentNode.removeChild(node);
        }

        if (contadorEmpregosAntigos > 1) {
            if (node2.parentNode) {
                node2.parentNode.removeChild(node2);
            }
        }

        if (contadorEmpregosAntigos - 1 == 0) {
            if (btnRemover.parentNode) {
                btnRemover.parentNode.removeChild(btnRemover);
            }
        }


        contadorEmpregosAntigos--;
    } else {
        alertaPadraoAviso("Não existem experiencias para serem removidas");
    }
}


function uploadImg() {


    //alert("Imagem Clickadado");

    let $InputImg = document.getElementById("ftPerfilInput")

    $InputImg.click();
    // let valor = $InputImg.next().val();

    // $('#FotoPerfil').attr("src", "data:image/png;base64," + imginput);
}
/*
document.getElementById('ftPerfilInput').addEventListener('change', function () {
    var reader = new FileReader();
    reader.onload = function () {
        var arrayBuffer = this.result,
            array = new Uint8Array(arrayBuffer),
            binaryString = String.fromCharCode.apply(null, array);
        $('#FotoPerfil').attr("src", "data:image/png;base64," + binaryString);
       // console.log(binaryString);
    }
    reader.readAsArrayBuffer(this.files[0]);
}, false);*/


$(document).ready(function () {
    (function (document) {
        var input = document.getElementById("ftPerfilInput"),
            //output = document.getElementById("result"),
            fileData; // We need fileData to be visible to getBuffer.

        // Eventhandler for file input. 
        function openfile(evt) {
            var files = input.files;
            if (files[0].size > 1024000) {
                alert("Tamanho excedido!");
                return;
            };
            // Pass the file to the blob, not the input[0].
            fileData = new Blob([files[0]]);
            // Pass getBuffer to promise.
            var promise = new Promise(getBuffer);
            // Wait for promise to be resolved, or log error.
            promise.then(function (data) {
                // Here you can pass the bytes to another function.
                if (data != undefined || data != null) {
                    let imginput = _arrayBufferToBase64(data);
                    if (imginput != "dW5kZWZpbmVk") {
                        $('#FotoPerfil').attr("src", "data:image/png;base64," + imginput);
                        $('#fotoperfilback').val(imginput);
                    }
                }
            }).catch(function (err) {
                console.log('Error: ', err);
            });
        }

        /* 
          Create a function which will be passed to the promise
          and resolve it when FileReader has finished loading the file.
        */
        function getBuffer(resolve) {
            var reader = new FileReader();
            reader.readAsArrayBuffer(fileData);
            reader.onload = function () {
                var arrayBuffer = reader.result
                var bytes = new Uint8Array(arrayBuffer);
                resolve(bytes);
            }
        }

        // Eventlistener for file input.
        input.addEventListener('change', openfile, false);
    }(document));
});

function _arrayBufferToBase64(buffer) {
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}
function CarregaSelect() {

    $("#categoriaString").val($("#categoriasCNH"));
    VirtualSelect.init({
        ele: '.multipleSelect',
        disableSelectAll: true
    })

    $(".multipleSelect").change(function () {
       // console.log(this.value);
        $("#categoriaString").val(this.value);

    });

    

}
setTimeout(CarregaSelect, 500);
function SetarImage() {
    let imginput = $("#fotoperfilback").val();
    if (imginput != "") {
        $('#FotoPerfil').attr("src", "data:image/jpeg;base64," + imginput);
        
    }
 
}
SetarImage();             

//but, non


//$('input[name*="native-select"]').change(function () {

//    console.log(this.value);

//}