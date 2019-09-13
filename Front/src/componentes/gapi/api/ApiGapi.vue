<template>
    <div class="row">
       <menu-gapi></menu-gapi>
            
        <div class="row">
            <meu-cardColorido v-on:navegar="navegaMarca" :classe="classeCardColorido1" :idElemento="idElemento" :textos="textosCardColorido1" id="cardMarca"></meu-cardColorido>
            <meu-cardColorido v-on:navegar="navegaQr" :classe="classeCardColorido2" :idElemento="idElemento" :textos="textosCardColorido2" class="cards" id="cardQr"></meu-cardColorido>
            <meu-cardColorido v-on:navegar="navegaHyper" :classe="classeCardColorido3" :idElemento="idElemento" :textos="textosCardColorido3" class="cards" id="cardHyperspace"></meu-cardColorido>
        </div>
        
    </div>
</template>

<script>
//Componentes
import MenuGapi from '../menu/MenuGapi.vue';
import CardColorido from '../../compartilhado/card/CardColorido.vue';

//Serviços
import DashBoardServiceHyperspace from '../../../classes/services/hyperspace/DashBoardServiceHyperspace.js';
import DashBoardServiceMarcaDagua from '../../../classes/services/marcaDagua/DashBoardServiceMarcaDagua.js';
import DashBoardServiceQrCode from '../../../classes/services/qrCode/DashBoardServiceQrCode.js';

export default {
    components: {
        'menu-gapi': MenuGapi,
        'meu-cardColorido': CardColorido
    },

    methods: {
        navegaMarca(){
            this.$router.push({ name: 'homeMarca' });
        },

        navegaQr(){
            this.$router.push({ name: 'homeQrCode' });
        },

        navegaHyper(){
            this.$router.push({ name: 'homeHyperspace' });
        }
    },

    created(){
        let hoje = new Date();
        let ano = hoje.getFullYear();
        let mes = hoje.getMonth() + 1;
        let dia = hoje.getDate();
        let data = `${ano}-${mes}-${dia}`;

        let dashBoardServiceMarcaDagua = new DashBoardServiceMarcaDagua(this.$router);
        dashBoardServiceMarcaDagua.dashboard(data, dia)
            .then(dadosMarca => 
            {     
                this.textosCardColorido1.destaque = "Online";
            })
            .catch(erro => {
                this.textosCardColorido1.destaque = "Offline";
                let marca = document.getElementById("cardMarca");
                marca.classList.add("fa-blink");
                marca.classList.remove("infoGeralOnline");
                marca.classList.add("infoGeralOffline");
            })
        
        let dashBoardServiceQrCode = new DashBoardServiceQrCode(this.$router);
        dashBoardServiceQrCode.dashboard(data, dia)
            .then(dadosQrCode => 
            {    
                this.textosCardColorido2.destaque = "Online";
            })
            .catch(erro => {
                this.textosCardColorido2.destaque = "Offline";
                let qr = document.getElementById("cardQr");
                qr.classList.add("fa-blink");
                qr.classList.remove("infoGeralOnline");
                qr.classList.add("infoGeralOffline");
            })

        let dashBoardServiceHyperspace = new DashBoardServiceHyperspace(this.$router);
        dashBoardServiceHyperspace.dashboard(data, dia)
          .then(dadosHyperspace => 
          {
                this.textosCardColorido3.destaque = "Online";
          })
          .catch(erro => {
                this.textosCardColorido3.destaque = "Offline";
                let hyper = document.getElementById("cardHyperspace");
                hyper.classList.add("fa-blink");
                hyper.classList.remove("infoGeralOnline");
                hyper.classList.add("infoGeralOffline");
          })
    },

    data(){
        return{
            //Card colorido
            classeCardColorido1: "col s10 m7 l2 xl2 offset-m2 infoGeralOnline",
            classeCardColorido2: "col s10 m7 l2 xl2 offset-m2 infoGeralOnline",
            classeCardColorido3: "col s10 m7 l2 xl2 offset-m2 infoGeralOnline",

            textosCardColorido1: {
                titulo: "Marca",
                destaque: ""
            },
            textosCardColorido2: {
                titulo: "QrCode",
                destaque: ""
            },    
            textosCardColorido3: {
                titulo: "Hyperspace",
                destaque: ""
            },  
            idElemento: "numeroDestaqueMonitoramento",
        }
    }
}
</script>

<style scoped>   
    .cards{
        margin-top: 20px;
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
        #cardMarca{
            cursor: pointer;
        }
        #cardQr{
            margin-left: 10px;
        }
        #cardHyperspace{
            margin-left: 10px;
        }
    }
</style>
