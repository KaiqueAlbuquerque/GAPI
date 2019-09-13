<template>
    <div class="row">    
        <menu-hyper></menu-hyper>

        <div class="row">
            <meu-cardColorido v-on:navegar="navegaClientes" :classe="classeCardColorido1" :idElemento="idElemento" :textos="textosCardColorido1" id="cardClientes"></meu-cardColorido>
            <meu-cardColorido v-on:navegar="navegaTokens" :classe="classeCardColorido2" :idElemento="idElemento" :textos="textosCardColorido2" id="cardTokens" class="cards"></meu-cardColorido>
            <meu-cardColorido :classe="classeCardColorido3" :idElemento="idElemento" :textos="textosCardColorido3" class="cards" id="cardRequisicoes"></meu-cardColorido>
        </div>   
        <meu-card :classe="tamanhoGraficoLinhas" :header="headerCardLinhas" class="cardGrafico">
            <div class="divGrafico">
                <canvas id="graficoRequisicoesLinhaHyperspace"></canvas>
            </div> 
        </meu-card> 
        <meu-card :classe="tamanhoGraficoPizza" :header="headerCardPizza" class="cardGrafico">
            <div class="divGrafico">
                <canvas id="graficoRequisicoesPizzaHyperspace"></canvas>
            </div> 
        </meu-card> 
        <meu-loader id="loaderHomeHyperspace"></meu-loader>     
    </div>
</template>

<script>
//Componentes
import MenuHyper from '../menu/MenuHyper.vue';
import Card from '../../compartilhado/card/Card.vue';
import CardColorido from '../../compartilhado/card/CardColorido.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Graficos
import GraficoLinha from '../../compartilhado/graficos/GraficoLinha.js';
import GraficoPizza from '../../compartilhado/graficos/GraficoPizza.js';

//Serviços
import DashBoardServiceHyperspace from '../../../classes/services/hyperspace/DashBoardServiceHyperspace.js';

//Helpers
import MontaComboDatas from '../../../classes/helpers/MontaComboDatas.js';
import MontaCores from '../../../classes/helpers/MontaCores.js';

export default {
    components: {
        'menu-hyper': MenuHyper,
        'meu-card': Card,
        'meu-cardColorido': CardColorido,
        'meu-loader': Loader
    },

    methods: {
        createChart(chartId, chartData) {
            const ctx = document.getElementById(chartId);
            const myChart = new Chart(ctx, {
                type: chartData.type,
                data: chartData.data,
                options: chartData.options
            });

            if(chartId == 'graficoRequisicoesLinhaHyperspace'){
                this.ctxGraficoLinha = myChart;
            }
            else{
                this.ctxGraficoPizza = myChart;
            }
        },

        aguarde(){
            let loader = document.getElementById("loaderHomeHyperspace");
            loader.style.display = 'block';
        },

        naoAguarde(){
            let loader = document.getElementById("loaderHomeHyperspace");
            loader.style.display = 'none';
        },

        navegaClientes(){
            this.$router.push({ name: 'clientesHyperspace' });
        },

        navegaTokens(){
            this.$router.push({ name: 'tokensHyperspace' });
        },

        montaTela(data, dia, tipo){
            if(document.getElementById("loaderHomeHyperspace") != undefined){
                this.aguarde();	
            }

            let dashBoardService = new DashBoardServiceHyperspace(this.$router);
            dashBoardService.dashboard(data, dia)
                .then(dados => 
                {                  
                    this.textosCardColorido1.destaque = dados.QuantidadeClientes;
                    this.textosCardColorido2.destaque = dados.QuantidadeTokens;
                    this.textosCardColorido3.destaque = dados.QuantidadeFeitas;

                    let apenasDiasMensal = dados.QuantidadeRequisicoesMensal.map(obj => {
                        return obj.Dia
                    });
                    let apenasRequisicoesMensal = dados.QuantidadeRequisicoesMensal.map(obj => {
                        return obj.Requisicoes
                    });
                    let apenasClientesDiarias = dados.QuantidadeRequisicoesDiarias.map(obj => {
                        return obj.NomeCliente
                    });
                    let apenasRequisicoesDiarias = dados.QuantidadeRequisicoesDiarias.map(obj => {
                        return obj.Requisicoes
                    });
                    
                    let graficoLinha = new GraficoLinha(
                                    "Requisições",
                                    apenasDiasMensal,
                                    apenasRequisicoesMensal,
                                    '#fff',
                                    5,
                                    '#42a5f5',
                                    "Requisições",
                                    "Dia do mês",
                                    false,
                                    false
                                );

                    let cores = [];

                    for(let i = 0; i < apis.length; i++){
                        cores.push(MontaCores.geraCor());
                    }

                    let graficoPizza = new GraficoPizza(
                                        apenasClientesDiarias,
                                        cores,
                                        apenasRequisicoesDiarias
                                    );

                    //Tipo 1 é criação da tela e tipo 2 é atualização
                    if(tipo == 1){
                        this.createChart('graficoRequisicoesLinhaHyperspace', graficoLinha.geraGraficoLinha());
                        this.createChart('graficoRequisicoesPizzaHyperspace', graficoPizza.geraGraficoPizza());
                    }
                    else{
                        let linha = graficoLinha.geraGraficoLinha();
                        let pizza = graficoPizza.geraGraficoPizza();
                        
                        this.ctxGraficoLinha.data.datasets[0].data = linha.data.datasets[0].data;
                        this.ctxGraficoLinha.data.labels = linha.data.labels;
                        this.ctxGraficoLinha.update();
                        
                        this.ctxGraficoPizza.data.datasets[0].data = pizza.data.datasets[0].data;
                        this.ctxGraficoPizza.data.labels = pizza.data.labels;
                        this.ctxGraficoPizza.update();
                    }
                    this.naoAguarde();
                })
                .catch(erro => this.naoAguarde());
        },
    },

    data(){
        return{
            //Card branco
            tamanhoGraficoLinhas: "col s12 m12 l6 xl6",
            tamanhoGraficoPizza: "col s12 m12 l6 xl6",

            headerCardLinhas: {
                titulo: "Requisições Mensal",
                select: [
                    {
                        valores: MontaComboDatas.montaAno()
                    },
                    {
                        valores: MontaComboDatas.montaMeses(new Date().getFullYear())
                    }
                ]
            },

            headerCardPizza: {
                titulo: "Requisições Diárias Clientes",
                select: [
                    {
                        valores: MontaComboDatas.montaDias(
                                                            new Date().getFullYear(), 
                                                            MontaComboDatas.getNomeMes(new Date().getMonth())
                                                          )
                    }
                ]
            },
            
            //Card colorido
            classeCardColorido1: "col s10 m7 l3 xl3 offset-m2 infoGeralHyperspace",
            classeCardColorido2: "col s10 m7 l3 xl3 offset-xl1 offset-l1 offset-m2 infoGeralHyperspace",
            classeCardColorido3: "col s10 m7 l3 xl3 offset-xl1 offset-l1 offset-m2 infoGeralHyperspace",

            textosCardColorido1: {
                titulo: "Clientes",
                destaque: "",
                complementoDestaque: "Clientes"
            },
            textosCardColorido2: {
                titulo: "Tokens",
                destaque: "",
                complementoDestaque: "Tokens"
            },
            textosCardColorido3: {
                titulo: "Requisições",
                destaque: "",
                complementoDestaque: "Requisições"
            },
            idElemento: "numeroDestaqueMarca",
        }
    },

    created() {
        //Variaveis para ter o contexto de onde os graficos são gerados para poder atualiza-los
        var ctxGraficoLinha;
        var ctxGraficoPizza;

        let hoje = new Date();
        let ano = hoje.getFullYear();
        let mes = hoje.getMonth() + 1;
        let dia = hoje.getDate();

        let data = `${ano}-${mes}-${dia}`;
        
        this.montaTela(data, dia, 1); 
    },

    mounted() {
        let selects = document.getElementsByTagName("select");
        let selectMes = selects[selects.length - 2];
        let selectAno = selects[selects.length - 3];
        let selectDia = selects[selects.length - 1];

        selectMes.addEventListener("change", () => {
            let ano = selectAno.value;
            let mes = selectMes.value;

            let numeroMes = MontaComboDatas.getNumeroMes(mes);
            let hoje = new Date();
            let data = '';
            
            if(hoje.getFullYear() == ano && hoje.getMonth() == (numeroMes - 1)){
                data = `${ano}-${numeroMes}-${hoje.getDate()}`;
            }
            else{
                data = `${ano}-${numeroMes}-${MontaComboDatas.getUltimoDiaMes(ano, numeroMes)}`;
            }
            
            this.headerCardPizza.select[0].valores = MontaComboDatas.montaDias(ano, mes);
            this.montaTela(data, '0', 2); 
        });

        selectAno.addEventListener("change", () => {
            let ano = selectAno.value;

            let data = `${ano}-01-${MontaComboDatas.getUltimoDiaMes(ano, '01')}`;

            this.headerCardPizza.select[0].valores = MontaComboDatas.montaDias(ano, '01');
            this.headerCardLinhas.select[1].valores = MontaComboDatas.montaMeses(ano, true);

            this.montaTela(data, '0', 2); 
        });

        selectDia.addEventListener("change", () => {
            let ano = selectAno.value;
            let mes = selectMes.value;
            let dia = selectDia.value;

            if(dia == "Todos"){
                dia = 0;
            }

            let numeroMes = MontaComboDatas.getNumeroMes(mes);
            let hoje = new Date();
            let data = '';

            if(hoje.getFullYear() == ano && hoje.getMonth() == (numeroMes - 1)){
                data = `${ano}-${numeroMes}-${hoje.getDate()}`;
            }
            else{
                data = `${ano}-${numeroMes}-${MontaComboDatas.getUltimoDiaMes(ano, numeroMes)}`;
            }
            
            this.montaTela(data, dia, 2); 
        });
    }
}
</script>

<style scoped>
.cards{
    margin-top: 20px;
}
#cardRequisicoes{    
    margin-bottom: 20px;
}
@media only screen and (min-width: 992px){
    .cards{
        margin-top: 0px;
    }
    .divGrafico{
        padding-top: 10px; 
    }
    .cardGrafico{
        padding-top: 25px;
    }
    #cardClientes{
        margin-left: 25px;
        cursor: pointer;
    }   
    #cardTokens{
        cursor: pointer;
    }
}
</style>
