// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

//$('input[type=radio][name=idcargo]').change(function () {

//AtualizarTabela();
//});

//$('input[type=radio][name=idcidade]').change(function () {

//   // AtualizarTabela();
//});
$('#maior18').click(function () {
    AtualizarTabela();
});

function AtualizarTabela() {

    let idcargo2 = $('input[type=radio][name=idcargo]:checked').val();
    let maior182 = $('#maior18').is(':checked');
    let nomeCidade = $('input[type=radio][name=idcidade]:checked').val();
    let pesquisar2 = $("#Pesquisar").val();



    if (nomeCidade == undefined)
        nomeCidade = null;
    if (pesquisar2 == undefined)
        pesquisar2 = "";
    if (idcargo2 == undefined)
        idcargo2 = "0";


    obj = {

        idcargo: parseInt(idcargo2),
        maior18: maior182,
        cidade: nomeCidade,
        pesquisar: pesquisar2,
    }
    let data = JSON.stringify(obj);

    $.ajax({
        type: "POST",
        url: "Painel/Pesquisar",
        contentType: "application/json; charset=utf-8",
        //contentType: false,
        //processData: false,
        beforeSend: function () {
            abrirCarregando();
        },
        data: data,
        success: function (data) {

            $('#conteudo-pesquisa').html(data);
            //var $tabela = "#TabelaRetorno";
            //if ($($tabela).length > 0)
            //    $("#btn-RetornoResultado").show();
            //if ($tabela != "") {
            //    carregaFuncoesPaginacao($tabela);
            //carregaFuncoesLista(tabela);


        },
        error: function (e) {
            fecharCarregando();
            //  msgErroFalhaConexao(e.error);
        },
        complete: function () {

            fecharCarregando();
        }

    });


}


//
function abrirCarregando() {
    $("#fundo-carregando").height($(window).height()).removeClass("d-none");
    console.log('abrirCarregando');
}


function fecharCarregando() {
    setTimeout(function () {
        $("#fundo-carregando").addClass("d-none");
    }, 100); //200
    //exibeMsg();
    console.log('fecharCarregando');
}


$("input:radio").on("click", function (e) {
    AtualizarTabela();
    var inp = $(this); //cache the selector
    if (inp.is(".theone")) { //see if it has the selected class
        inp.prop("checked", false).removeClass("theone");
        return;
    }
    $("input:radio[name='" + inp.prop("name") + "'].theone").removeClass("theone");
    inp.addClass("theone");
});


//function onkeyEnterSubmit(event) {


//    if (event.keyCode === 13) {

//    }

//}


function SelecionarCanditado(idUsuario) {


    let idcargo2 = $('input[type=radio][name=idcargo]:checked').val();
    if (idcargo2 != undefined) {

        window.location.replace("Painel/Visualizar?id=" + idUsuario + "&idCargo=" + idcargo2);
    }

    else {

        //alert("");
        let cargos = null;
        $.ajax({
            type: "GET",
            url: "Painel/GetCargos/?id=" + idUsuario,
            // contentType: "application/json; charset=utf-8",
            //contentType: false,
            //processData: false,
            beforeSend: function () {
                abrirCarregando();
            },
            // data: data,
            success: function (data) {
                fecharCarregando();
                // $('#conteudo-pesquisa').html(data);
                console.log(data);
                // cargos = JSON.parse(data);
                cargos = data;
                const map = new Map()
              
                if (cargos.length == 1) {
                    window.location.replace("Painel/Visualizar/?id=" + idUsuario + "&idCargo=" + cargos[0].idCargo);

                } else {
                    cargos.forEach(element => map.set(element.idCargo, element.cargo));
                    Swal.fire({
                        title: 'Vagas aplicada do canditado',
                        input: 'select',
                        //Isso aqui você pode passar um array pro java script se preferir
                        inputOptions: map,
                        //        inputOptions: {
                        //                '1': 'Vaga 1',
                        //                '2': 'Vaga 2',
                        //                '3': 'Vaga 3'
                        //},
                        inputPlaceholder: 'Selecione uma vaga',
                        showCancelButton: true,
                        confirmButtonText: 'Selecionar',
                        cancelButtonText: 'Voltar',
                        confirmButtonColor: '#3390F1',
                        cancelButtonColor: '#6D6D6F',
                        inputValidator: function (value) {
                            return new Promise(function (resolve, reject) {
                                if (value !== '') {
                                    resolve();
                                } else {
                                    resolve('Nenhuma vaga selecionada');
                                }
                            });
                        }

                    }).then(function (result) {
                        if (result.isConfirmed) {
                            window.location.replace("Painel/Visualizar/?id=" + idUsuario + "&idCargo=" + result.value);
                        }
                    });
                }
            },
            error: function (e) {
                fecharCarregando();
                //  msgErroFalhaConexao(e.error);
            },
            complete: function () {

                fecharCarregando();
            }

        });
        //let lista = cargos.forEach(element => console.log(element));


    }

}