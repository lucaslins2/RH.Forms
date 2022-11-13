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