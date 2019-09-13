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
            cor: 'red darken-4',
            menus: [
                {
                    nome: "Teste Marca",
                    name: "testeMarca"
                },
                {
                    nome: "Clientes",
                    name: "clientesMarca"
                }, 
                {
                    nome: "Usuários",
                    name: "usuariosMarca"
                }                
            ],

            //Logo
            logo: {
                logotipo: "/dist/Marca_Branco.png",
                altLogo: "Logo Marca d'agua",
                id: "logo",
                name: "homeMarca"
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
