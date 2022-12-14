function alertaPadraoSucesso(pText){
    Swal.fire({
        icon: 'success',
        title: pText,
        showConfirmButton: false,
        showCloseButton: true,
        timer: 2500
      })
}

function alertaPadraoErro(pText){
    Swal.fire({
        icon: 'error',
        timerProgressBar: true,
        title: pText,
        showConfirmButton: false,
        showCloseButton: true,
        timer: 2500
      })
}

function alertaPadraoAviso(pText) {
    Swal.fire({
        icon: 'warning',
        timerProgressBar: true,
        title: pText,
        showConfirmButton: false,
        showCloseButton: true,
        timer: 2500
    })
}

function AlertaConfrima(pText, btnCanditado) {
    Swal.fire({
        title: pText,
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'Sim',
        denyButtonText: 'N\u00e3o',
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            //Swal.fire('Aprovado!', '', 'success')
            setTimeout(btnCanditado.click()
                , 1000);
       

        } else if (result.isDenied) {
           Swal.fire('Cancelado', '', 'info')
        }
    })


}
