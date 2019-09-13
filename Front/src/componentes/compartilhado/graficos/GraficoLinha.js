export default class GraficoLinha{
    constructor(
                    textoLegendaBolinha,
                    valoresEixoX, 
                    valoresPlotar, 
                    corDoPonto, 
                    espessuraLinha, 
                    corDaLinhaPonto, 
                    tituloY, 
                    tituloX, 
                    retaEixoY,
                    linhasFundo
                ){
        
        this._textoLegendaBolinha = textoLegendaBolinha 
        this._valoresEixoX =  valoresEixoX 
        this._valoresPlotar =  valoresPlotar 
        this._corDoPonto =  corDoPonto 
        this._espessuraLinha =  espessuraLinha 
        this._corDaLinhaPonto =  corDaLinhaPonto 
        this._tituloY =  tituloY 
        this._tituloX =  tituloX 
        this._retaEixoY =  retaEixoY 
        this._linhasFundo =  linhasFundo 
    }

    geraGraficoLinha(){
        const grafico = {
            type: 'line',
            data: {
                labels: this._valoresEixoX,
                datasets: [
                { 
                    label: this._textoLegendaBolinha,
                    fill: false,  
                    lineTension: 0.7,
                    pointBackgroundColor: this._corDoPonto,
                    pointBorderWidth: 2,
                    pointHoverBorderWidth: 3,
                    pointRadius: 5,
                    borderWidth: this._espessuraLinha,
                    data: this._valoresPlotar,
                    borderColor: this._corDaLinhaPonto
                },
                ]
            },
            options: {
                responsive: true,
                legend: {
                display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero:true
                        },
                        scaleLabel: {
                            display: true,
                            labelString: this._tituloY
                        },
                        gridLines: {
                            drawBorder: this._retaEixoY,
                        },
                    }],
                    xAxes: [
                        {
                            scaleLabel: {
                                display: true,
                                labelString: this._tituloX
                            },
                            gridLines: {
                                display: this._linhasFundo
                            }
                        }
                    ]
                }
            }
        }

        return grafico;
    }
}