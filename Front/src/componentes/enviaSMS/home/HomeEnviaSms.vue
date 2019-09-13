<template>
    <div class="row">
        <meu-menu :cor="cor" :menus="menus" :logotipo="logo"></meu-menu>

        <div class="row">
            <meu-card :classe="tamanho" :header="headerCardRelatorio">
                
                <iframe id="relatorio-sms" src="https://app.powerbi.com/reportEmbed?reportId=22a09cde-ed7a-487a-b3aa-614c878b736b&groupId=a611a154-51a9-42c3-b9d1-2a1ecbefd89e&autoAuth=true&ctid=80378186-2a0a-4f59-8dc4-fbb0cd796b34" frameborder="0" allowFullScreen="true"></iframe>
                   
            </meu-card>
        </div>
    </div>
</template>

<script>
import Menu from '../../compartilhado/menu/Menu.vue';
import Card from '../../compartilhado/card/Card.vue';

export default {

    data()
    {
        return{
            //Menu
            cor: 'amber',
            menus: [
                
            ],

            //Card branco
            tamanho: "col s12",
            headerCardRelatorio: {
                titulo: "Relatório de SMS requisições"
            },

            //Logo
            logo: {
                logotipo: "/dist/EnviaSMS_Branco.png",
                altLogo: "Logo SMS",
                id: "logo",
                name: "homeSms"
            }
        }        
    },
    
    components: {
        'meu-menu': Menu,
        'meu-card': Card
    },
}
</script>

<style scoped>
#relatorio-sms{
    width: -webkit-fill-available;
    height: -webkit-fill-available;
}
</style>
