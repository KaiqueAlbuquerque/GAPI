export default class GraficoPizza{
    constructor(textos, cores, dados){
        this._textos = textos;
        this._cores = cores;
        this._dados = dados;
    }

    geraGraficoPizza(){
        const grafico = {
            type: 'pie',
            data: {
                labels: this._textos,
                datasets: [
                    { 
                        backgroundColor: this._cores,
                        data: this._dados
                    },
                ]
            },
            options: {
                responsive: true,
                legend: {
                    display: true
                }
            }
        }

        return grafico;
    }
}