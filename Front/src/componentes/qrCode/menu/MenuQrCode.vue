<template>
    <meu-menu :cor="cor" :menus="menus" :logotipo="logo"></meu-menu>
</template>

<script>
import Menu from '../../compartilhado/menu/Menu.vue';
export default {
    components: {
        'meu-menu': Menu
    },

    data(){
        return{
            //Menu
            cor: 'grey darken-3',
            menus: [
                {
                    nome: "Teste QrCode",
                    name: "testeQrCode"
                },
                {
                    nome: "Clientes",
                    name: "clientesQrCode"
                }, 
                {
                    nome: "Usuários",
                    name: "usuariosQrCode"
                }                
            ],
            
            //Logo
            logo: {
                logotipo: "/dist/QrCode_Branco.png",
                altLogo: "Logo QrCode",
                id: "logo",
                name: "homeQrCode"
            }
        }
    }
}
</script>

<style>
    #toast-container {
        min-width: 10%;
        top: auto !important;
        bottom: 10%;
        right: 50%;
        transform: translateX(50%) translateY(50%);
    }
</style>
