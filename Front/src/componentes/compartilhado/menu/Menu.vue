<template>
    <div class="navbar">
        <nav>
            <div class="nav-wrapper" :class="cor">
                <meu-logo :logo="logo"></meu-logo>
                <a href="#" data-target="menu-mobile" class="sidenav-trigger">
                    <i class="material-icons">menu</i>
                </a>
                <ul class="right hide-on-med-and-down">
                    <li v-for="menu of menus" :key="menu.nome">
                        <router-link :to="{ name: menu.name }">{{ menu.nome }}</router-link>
                    </li>
                    <li><a class="dropdown-trigger" href="#" data-target="apis"><i class="material-icons">apps</i></a></li>
                    <li><a class="dropdown-trigger" href="#" data-target="outrasOpcoes"><i class="material-icons">more_horiz</i></a></li>
                </ul>

                <ul class="sidenav" id="menu-mobile">
                    <li :class="cor">
                        <div class="row">
                            <div class="col s10">
                                <a href="#"><i class="material-icons left">home</i>Menu</a>
                            </div>
                            <div class="col s2 valign-wrapper" >
                                <a class="sidenav-close right">
                                    <i style="height: 48px; line-height: 48px; margin-left: -0.75rem; cursor: pointer;" class="material-icons right white-text">close</i>
                                </a>
                            </div>
                        </div>
                    </li>
                    <li class="no-padding">
                        <ul class="collapsible collapsible-accordion">
                            <li v-for="menu of menus" :key="menu.nome">
                                <router-link :to="{ name: menu.name }">{{ menu.nome }}</router-link>
                            </li>
                            <li>
                                <a class='collapsible-header'>Trocar sistema
                                    <i class="material-icons right">arrow_drop_down</i>
                                </a>
                                <div class="collapsible-body">
                                    <ul>
                                        <li>
                                            <router-link :to="{ name:'homeGapi' }">Gapi</router-link>
                                        </li>
                                        <li>
                                            <router-link :to="{ name:'homeHyperspace' }">Hyperspace</router-link>             
                                        </li>  
                                        <li>
                                            <router-link :to="{ name:'homeMarca' }">Marca d'agua</router-link>            
                                        </li>    
                                        <li>
                                            <router-link :to="{ name:'homeQrCode' }">QrCode</router-link>            
                                        </li>        
                                        <li>
                                            <router-link :to="{ name:'homeSms' }">Envia SMS</router-link>             
                                        </li>   
                                    </ul>
                                </div>
                            </li>
                            <li><router-link :to="{ name: 'login' }">Sair</router-link></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </nav>
                   
        <meu-menuRedirecionamento></meu-menuRedirecionamento>
        <meu-menuSair :corMenuSair="corMenuSair"></meu-menuSair>             
    </div>
</template>

<script>
import MenuRedirecionamento from './MenuRedirecionamento.vue';
import Logo from '../logo/Logo.vue';
import MenuSair from './MenuSair.vue';


export default {
    mounted(){
        this.$nextTick(function () {
            var elems = document.querySelectorAll('.sidenav');
            var instances = M.Sidenav.init(elems);
            
            var elemento = document.querySelectorAll('.collapsible');
            var instances = M.Collapsible.init(elemento);
        });
    },

    components: {
        'meu-menuRedirecionamento': MenuRedirecionamento,
        'meu-logo': Logo,
        'meu-menuSair': MenuSair
    },
    
    props: ['cor', 'menus', 'logotipo'],

    data(){
        return{
            logo: this.logotipo,
            corMenuSair: this.cor
        }
    },
}
    
</script>

<style>
nav ul li:hover {
    background-color: rgba(0, 0, 0, 0) !important;
}
nav ul li>a:hover {
    background-color: rgba(0, 0, 0, 0) !important;
}
.sidenav li>a {
    padding: 0 16px;
}
</style>