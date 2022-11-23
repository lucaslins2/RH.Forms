var cep_is_full = false;
    
const showData = (result) =>{
    for(const campo in result){
        if(document.querySelector("#"+campo)){
            document.querySelector("#"+campo).value = result[campo];
        }
    }
    var cep = document.getElementById('cep').value;
    var count = (cep.match(/-/g) || []).length;
    if(count < 1){
        alertaPadraoAviso ("CEP INVALIDO!!!");
    }
}

cepCheck = function (cep) {
    cep = cep.value.replace("-","");
    if(cep_is_full == true && cep.length < 8){
        document.getElementById('cep').value = cep;
        cep_is_full = false;
    }
    if(cep_is_full == false && cep.length == 8){
        cep_is_full = true;

        const options ={
            method:'GET',
            mode: 'cors',
            cache: 'default',
        }

        fetch(`https://viacep.com.br/ws/${cep}/json/`,options)
        .then(response=>{response.json()
            .then( data=> showData(data))
        })
        .catch(e => console.log('Deu Erro:' +e,message))

    }
}