// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

$('input[type=radio][name=idcargo]').change(function () {

    AtualizarTabela();
});

$('input[type=radio][name=idcidade]').change(function () {

    AtualizarTabela();
});


function AtualizarTabela() {

    let idcargo2 = $('input[type=radio][name=idcargo]:checked').val();
    let maior182 = $('#maior18').is(':checked'); 
    let nomeCidade = $('input[type=radio][name=idcidade]:checked').val();
    let pesquisar2 = $("#Pesquisar").val();



    if (nomeCidade == undefined)
        nomeCidade = null;

    obj =  {

        idcargo : parseInt(idcargo2),
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