<template>
  <div class="row">
    <menu-gapi></menu-gapi>
    
    <div class="row">
      <meu-cardColorido :classe="classeCardColorido1" :idElemento="idElemento" :textos="textosCardColorido1" id="cardAcessos"></meu-cardColorido>
      <meu-cardColorido v-on:navegar="navegaApis" :classe="classeCardColorido2" :idElemento="idElemento" :textos="textosCardColorido2" class="cards" id="cardApi"></meu-cardColorido>
      <meu-cardColorido v-on:navegar="navegaApis" :classe="classeCardColorido3" :idElemento="idElemento" :textos="textosCardColorido3" class="cards" id="cardApiInativas"></meu-cardColorido>
    </div>   
    <meu-card :classe="tamanhoGraficoLinhas" :header="headerCardLinhas" class="cardGrafico">
        <div class="divGrafico">
          <canvas id="graficoRequisicoesLinhaGapi"></canvas>
        </div> 
    </meu-card> 
    <meu-card :classe="tamanhoGraficoPizza" :header="headerCardPizza" class="cardGrafico">
        <div class="divGrafico">
          <canvas id="graficoRequisicoesPizzaGapi"></canvas>
        </div> 
    </meu-card>
    <meu-loader id="loaderHomeGapi"></meu-loader>     
  </div>
</template>

<script>
//Componentes
import MenuGapi from '../menu/MenuGapi.vue';
import Card from '../../compartilhado/card/Card.vue';
import CardColorido from '../../compartilhado/card/CardColorido.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Graficos
import GraficoLinha from '../../compartilhado/graficos/GraficoLinha.js';
import GraficoPizza from '../../compartilhado/graficos/GraficoPizza.js';

//Serviços
import DashBoardServiceHyperspace from '../../../classes/services/hyperspace/DashBoardServiceHyperspace.js';
import DashBoardServiceMarcaDagua from '../../../classes/services/marcaDagua/DashBoardServiceMarcaDagua.js';
import DashBoardServiceQrCode from '../../../classes/services/qrCode/DashBoardServiceQrCode.js';

//Helpers
import MontaComboDatas from '../../../classes/helpers/MontaComboDatas.js';
import MontaCores from '../../../classes/helpers/MontaCores.js';

export default {
  components: {
    'menu-gapi': MenuGapi,
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

      if(chartId == 'graficoRequisicoesLinhaGapi'){
          this.ctxGraficoLinha = myChart;
      }
      else{
          this.ctxGraficoPizza = myChart;
      }
    },

    navegaApis(){
      this.$router.push({ name: 'apisGapi' });
    },

    aguarde(){
      let loader = document.getElementById("loaderHomeGapi");
      loader.style.display = 'block';
    },

    naoAguarde(){
      let loader = document.getElementById("loaderHomeGapi");
      loader.style.display = 'none';
    },

    montaTela(data, dia, tipo){
      let apisInativas = 0;
      let apisAtivas = 0;

      if(document.getElementById("loaderHomeGapi") != undefined){
        this.aguarde();
      }
      
      let apenasDiasMensal = [];
      let apenasRequisicoesMensal = [];
      let apis = [];
      let apenasRequisicoesDiarias = [];

      this.textosCardColorido1.destaque = 0;

      let dashBoardServiceHyperspace = new DashBoardServiceHyperspace(this.$router);
      dashBoardServiceHyperspace.dashboard(data, dia)
          .then(dadosHyperspace => 
          {
            apisAtivas = apisAtivas + 1;

            this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosHyperspace.QuantidadeFeitas;
            this.textosCardColorido2.destaque = apisAtivas;
            this.textosCardColorido3.destaque = apisInativas;
            
            apenasDiasMensal = dadosHyperspace.QuantidadeRequisicoesMensal.map(obj => {
                return obj.Dia
            });
            apenasRequisicoesMensal = dadosHyperspace.QuantidadeRequisicoesMensal.map(obj => {
                return obj.Requisicoes
            });
            
            apis.push("Hyperspace");

            let quantidade = dadosHyperspace.QuantidadeRequisicoesDiarias.map(obj => {
                return obj.Requisicoes
            });

            let soma = quantidade.reduce((somaAux, numero) => {
              return somaAux + numero;
            }, 0);

            apenasRequisicoesDiarias.push(soma);
          
            let dashBoardServiceMarcaDagua = new DashBoardServiceMarcaDagua(this.$router);
            dashBoardServiceMarcaDagua.dashboard(data, dia)
                .then(dadosMarca => 
                {     
                    apisAtivas = apisAtivas + 1;

                    this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosMarca.QuantidadeFeitas;
                    this.textosCardColorido2.destaque = apisAtivas;
                    this.textosCardColorido3.destaque = apisInativas;

                    for(let i = 0; i < apenasRequisicoesMensal.length; i++){
                      apenasRequisicoesMensal[i] = apenasRequisicoesMensal[i] + dadosMarca.QuantidadeRequisicoesMensal[i].Requisicoes;
                    } 

                    apis.push("Marca D'agua");

                    let quantidade = dadosMarca.QuantidadeRequisicoesDiarias.map(obj => {
                        return obj.Requisicoes
                    });

                    let soma = quantidade.reduce((somaAux, numero) => {
                      return somaAux + numero;
                    }, 0);

                    apenasRequisicoesDiarias.push(soma);

                    let dashBoardServiceQrCode = new DashBoardServiceQrCode(this.$router);
                    dashBoardServiceQrCode.dashboard(data, dia)
                        .then(dadosQrCode => 
                        {     
                            apisAtivas = apisAtivas + 1;

                            this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosQrCode.QuantidadeFeitas;
                            this.textosCardColorido2.destaque = apisAtivas;
                            this.textosCardColorido3.destaque = apisInativas;

                            for(let i = 0; i < apenasRequisicoesMensal.length; i++){
                              apenasRequisicoesMensal[i] = apenasRequisicoesMensal[i] + dadosQrCode.QuantidadeRequisicoesMensal[i].Requisicoes;
                            }

                            apis.push("QrCode");

                            let quantidade = dadosQrCode.QuantidadeRequisicoesDiarias.map(obj => {
                                return obj.Requisicoes
                            });

                            let soma = quantidade.reduce((somaAux, numero) => {
                              return somaAux + numero;
                            }, 0);

                            apenasRequisicoesDiarias.push(soma);

                            this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo);
                            this.naoAguarde();
                        })
                        .catch(erro => {
                          apisInativas = apisInativas + 1;
                          let cardColorido = document.getElementById("cardApiInativas");
                          cardColorido.classList.add("fa-blink");
                          cardColorido.classList.remove("infoGeralGapi");
                          cardColorido.classList.add("infoGeralMarca");

                          this.textosCardColorido3.destaque = apisInativas;

                          this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo)
                          this.naoAguarde();
                        });
                })
                .catch(erro => {
                  apisInativas = apisInativas + 1;
                  let cardColorido = document.getElementById("cardApiInativas");
                  cardColorido.classList.add("fa-blink");
                  cardColorido.classList.remove("infoGeralGapi");
                  cardColorido.classList.add("infoGeralMarca");

                  this.textosCardColorido3.destaque = apisInativas;

                  let dashBoardServiceQrCode = new DashBoardServiceQrCode(this.$router);
                  dashBoardServiceQrCode.dashboard(data, dia)
                    .then(dadosQrCode => 
                    {     
                        apisAtivas = apisAtivas + 1;

                        this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosQrCode.QuantidadeFeitas;
                        this.textosCardColorido2.destaque = apisAtivas;
                        this.textosCardColorido3.destaque = apisInativas;

                        for(let i = 0; i < apenasRequisicoesMensal.length; i++){
                          apenasRequisicoesMensal[i] = apenasRequisicoesMensal[i] + dadosQrCode.QuantidadeRequisicoesMensal[i].Requisicoes;
                        }

                        apis.push("QrCode");

                        let quantidade = dadosQrCode.QuantidadeRequisicoesDiarias.map(obj => {
                            return obj.Requisicoes
                        });

                        let soma = quantidade.reduce((somaAux, numero) => {
                          return somaAux + numero;
                        }, 0);

                        apenasRequisicoesDiarias.push(soma);

                        this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo);
                        this.naoAguarde();
                    })
                    .catch(erro => {
                      apisInativas = apisInativas + 1;
                      let cardColorido = document.getElementById("cardApiInativas");
                      cardColorido.classList.add("fa-blink");                      
                      cardColorido.classList.remove("infoGeralGapi");
                      cardColorido.classList.add("infoGeralMarca");

                      this.textosCardColorido3.destaque = apisInativas;

                      this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo)
                      this.naoAguarde();
                    });
                });
          })
          .catch(erro => {
            apisInativas = apisInativas + 1;
            let cardColorido = document.getElementById("cardApiInativas");
            cardColorido.classList.add("fa-blink");
            cardColorido.classList.remove("infoGeralGapi");
            cardColorido.classList.add("infoGeralMarca");

            this.textosCardColorido3.destaque = apisInativas;
            
            let dashBoardServiceMarcaDagua = new DashBoardServiceMarcaDagua(this.$router);
            dashBoardServiceMarcaDagua.dashboard(data, dia)
                .then(dadosMarca => 
                {     
                    apisAtivas = apisAtivas + 1;

                    this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosMarca.QuantidadeFeitas;
                    this.textosCardColorido2.destaque = apisAtivas;
                    this.textosCardColorido3.destaque = apisInativas;

                    apenasDiasMensal = dadosMarca.QuantidadeRequisicoesMensal.map(obj => {
                        return obj.Dia
                    });

                    apenasRequisicoesMensal = dadosMarca.QuantidadeRequisicoesMensal.map(obj => {
                        return obj.Requisicoes
                    });

                    apis.push("Marca D'agua");

                    let quantidade = dadosMarca.QuantidadeRequisicoesDiarias.map(obj => {
                        return obj.Requisicoes
                    });

                    let soma = quantidade.reduce((somaAux, numero) => {
                      return somaAux + numero;
                    }, 0);

                    apenasRequisicoesDiarias.push(soma);

                    let dashBoardServiceQrCode = new DashBoardServiceQrCode(this.$router);
                    dashBoardServiceQrCode.dashboard(data, dia)
                        .then(dadosQrCode => 
                        {     
                            apisAtivas = apisAtivas + 1;

                            this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosQrCode.QuantidadeFeitas;
                            this.textosCardColorido2.destaque = apisAtivas;
                            this.textosCardColorido3.destaque = apisInativas;

                            for(let i = 0; i < apenasRequisicoesMensal.length; i++){
                              apenasRequisicoesMensal[i] = apenasRequisicoesMensal[i] + dadosQrCode.QuantidadeRequisicoesMensal[i].Requisicoes;
                            }

                            apis.push("QrCode");

                            let quantidade = dadosQrCode.QuantidadeRequisicoesDiarias.map(obj => {
                                return obj.Requisicoes
                            });

                            let soma = quantidade.reduce((somaAux, numero) => {
                              return somaAux + numero;
                            }, 0);

                            apenasRequisicoesDiarias.push(soma);

                            this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo);
                            this.naoAguarde();
                        })
                        .catch(erro => {
                          apisInativas = apisInativas + 1;
                          let cardColorido = document.getElementById("cardApiInativas");
                          cardColorido.classList.add("fa-blink");
                          cardColorido.classList.remove("infoGeralGapi");
                          cardColorido.classList.add("infoGeralMarca");

                          this.textosCardColorido3.destaque = apisInativas;

                          this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo);
                          this.naoAguarde();
                        });
                })
                .catch(erro => {
                  apisInativas = apisInativas + 1;
                  let cardColorido = document.getElementById("cardApiInativas");
                  cardColorido.classList.add("fa-blink");
                  cardColorido.classList.remove("infoGeralGapi");
                  cardColorido.classList.add("infoGeralMarca");

                  this.textosCardColorido3.destaque = apisInativas;

                  let dashBoardServiceQrCode = new DashBoardServiceQrCode(this.$router);
                  dashBoardServiceQrCode.dashboard(data, dia)
                    .then(dadosQrCode => 
                    {     
                        apisAtivas = apisAtivas + 1;

                        this.textosCardColorido1.destaque = this.textosCardColorido1.destaque + dadosQrCode.QuantidadeFeitas;
                        this.textosCardColorido2.destaque = apisAtivas;
                        this.textosCardColorido3.destaque = apisInativas;

                        apenasDiasMensal = dadosQrCode.QuantidadeRequisicoesMensal.map(obj => {
                            return obj.Dia
                        });

                        apenasRequisicoesMensal = dadosQrCode.QuantidadeRequisicoesMensal.map(obj => {
                            return obj.Requisicoes
                        });

                        apis.push("QrCode");

                        let quantidade = dadosQrCode.QuantidadeRequisicoesDiarias.map(obj => {
                            return obj.Requisicoes
                        });

                        let soma = quantidade.reduce((somaAux, numero) => {
                          return somaAux + numero;
                        }, 0);

                        apenasRequisicoesDiarias.push(soma);

                        this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo);
                        this.naoAguarde();
                    })
                    .catch(erro => {
                      apisInativas = apisInativas + 1;
                      let cardColorido = document.getElementById("cardApiInativas");
                      cardColorido.classList.add("fa-blink");
                      cardColorido.classList.remove("infoGeralGapi");
                      cardColorido.classList.add("infoGeralMarca");

                      this.textosCardColorido3.destaque = apisInativas;

                      this.criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo);
                      this.naoAguarde();
                    });
                });
          });
    },

    criaGrafico(apenasDiasMensal, apenasRequisicoesMensal, apis, apenasRequisicoesDiarias, tipo){
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
                          apis,
                          cores,
                          apenasRequisicoesDiarias
                      );

      //Tipo 1 é criação da tela e tipo 2 é atualização
      if(tipo == 1){
          this.createChart('graficoRequisicoesLinhaGapi', graficoLinha.geraGraficoLinha());
          this.createChart('graficoRequisicoesPizzaGapi', graficoPizza.geraGraficoPizza());
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
    }
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
        titulo: "Requisições Diárias APIs",
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
      classeCardColorido1: "col s10 m7 l3 xl3 offset-m2 infoGeralGapi",
      classeCardColorido2: "col s10 m7 l3 xl3 offset-xl1 offset-l1 offset-m2 infoGeralGapi",
      classeCardColorido3: "col s10 m7 l3 xl3 offset-xl1 offset-l1 offset-m2 infoGeralGapi",

      textosCardColorido1: {
        titulo: "Acessos",
        destaque: 0,
        complementoDestaque: "Requisições"
      },
      textosCardColorido2: {
        titulo: "Ativas",
        destaque: 0,
        complementoDestaque: "APIs ativas"
      },    
      textosCardColorido3: {
        titulo: "Inativas",
        destaque: 0,
        complementoDestaque: "APIs inativas"
      },  
      idElemento: "numeroDestaqueGapi",
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
    document.getElementById("rodape").style.display = "block";

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
#cardApiInativas{    
    margin-bottom: 20px;
}
@keyframes fa-blink {
  0% { opacity: 1; }
  50% { opacity: 0.5; }
  100% { opacity: 1; }
}
.fa-blink {
  -webkit-animation: fa-blink 1.5s linear infinite;
  -moz-animation: fa-blink 1.5s linear infinite;
  -ms-animation: fa-blink 1.5s linear infinite;
  -o-animation: fa-blink 1.5s linear infinite;
  animation: fa-blink 1.5s linear infinite;
}
@media only screen and (min-width: 992px){
  .cards{
    margin-top: 0px;
    cursor: pointer;
  }
  .divGrafico{
    padding-top: 10px; 
  }
  .cardGrafico{
    padding-top: 25px;
  }
  #cardAcessos{
    margin-left: 25px;
  }   
}
</style>

