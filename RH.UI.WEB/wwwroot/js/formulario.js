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