export default class MontaComboDatas{
    
    static montaAno(){
        let arrAnos = [];
        let anoInicial = 2018;

        let hoje = new Date();
        let anoAtual = hoje.getFullYear();

        for(var i = anoInicial; i <= anoAtual; i++){
            if(i == anoAtual)
                arrAnos.push({"valor": i, "selected": "selected"});
            else
                arrAnos.push({"valor": i, "selected": ""});
        }

        return arrAnos;
    }

    static montaMeses(ano, alteracao=false){
        let arrMesesRetorno = [];

        let hoje = new Date();
        let mesAtual = hoje.getMonth();
        let anoAtual = hoje.getFullYear();
        
        var retorno = [];
        var arrayMes = retorno.concat(this.getMesesPortugues());
        
        if(ano < anoAtual){
            for(let i = 0; i < arrayMes.length; i++){
                arrMesesRetorno.push({"valor": arrayMes[i], "selected": ""});
            }
        } 
        else if(ano == anoAtual){
            for(let i = 0; i <= mesAtual; i++){
                if(i == mesAtual && !alteracao)
                    arrMesesRetorno.push({"valor": arrayMes[i], "selected": "selected"});    
                else                    
                    arrMesesRetorno.push({"valor": arrayMes[i], "selected": ""});               
            }
        }
        
        return arrMesesRetorno;
    }

    static montaDias(ano, mes){
        let arrDias = [];
        arrDias.push({"valor": "Todos", "selected": ""});

        let hoje = new Date();

        let anoHoje = hoje.getFullYear();
        let mesHoje = hoje.getMonth();

        let retorno = [];
        let arrayMes = retorno.concat(this.getMesesPortugues());
        let nomeMesHoje = arrayMes[mesHoje];

        if(anoHoje == ano && nomeMesHoje == mes)
        {
            for(let i = 1; i <= hoje.getDate(); i++){
                if(i == hoje.getDate())
                    arrDias.push({"valor": i, "selected": "selected"});
                else
                    arrDias.push({"valor": i, "selected": ""});
            }
        }
        else
        {
            let numeroMes = this.getNumeroMes(mes);
            let ultimoDia = this.getUltimoDiaMes(ano, numeroMes);

            for(let i = 1; i <= ultimoDia; i++){
                arrDias.push({"valor": i, "selected": ""});
            }
        }

        return arrDias;
    }

    static getMesesPortugues(){
        let arrayMes = new Array(12);
        arrayMes[0] = "Janeiro";
        arrayMes[1] = "Fevereiro";
        arrayMes[2] = "Março";
        arrayMes[3] = "Abril";
        arrayMes[4] = "Maio";
        arrayMes[5] = "Junho";
        arrayMes[6] = "Julho";
        arrayMes[7] = "Agosto";
        arrayMes[8] = "Setembro";
        arrayMes[9] = "Outubro";
        arrayMes[10] = "Novembro";
        arrayMes[11] = "Dezembro";
        
        return arrayMes;
    }

    static getUltimoDiaMes(ano, mes){
        let ultimoDia = new Date(ano, mes, 0).getDate();

        return ultimoDia;
    }

    static getNomeMes(numeroMes){
        let retorno = [];
        let arrayMes = retorno.concat(this.getMesesPortugues());
        
        return arrayMes[numeroMes];
    }

    static getNumeroMes(nomeMes){
        let retorno = [];
        let arrayMes = retorno.concat(this.getMesesPortugues());
        
        return arrayMes.indexOf(nomeMes) + 1;
    }
}