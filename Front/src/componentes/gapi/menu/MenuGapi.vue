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
            cor: 'purple darken-4',
            menus: [
                {
                    nome: "API's",
                    name: "apisGapi"
                },
                {
                    nome: "Usuários",
                    name: "usuariosGapi"
                }
            ],

            //Logo
            logo: {
                logotipo: "/dist/Gapi_Branco.png",
                altLogo: "Logo Gapi",
                id: "logo",
                name: "homeGapi"
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
