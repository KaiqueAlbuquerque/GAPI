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
            cor: 'blue lighten-2',
            menus: [
                {
                    nome: "Tokens",
                    name: "tokensHyperspace"
                },
                {
                    nome: "Clientes",
                    name: "clientesHyperspace"
                }
            ],

            //Logo
            logo: {
                logotipo: "/dist/hyper_branco.png",
                altLogo: "Logo Hyperspace",
                id: "logoHyper",
                name: "homeHyperspace"
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
