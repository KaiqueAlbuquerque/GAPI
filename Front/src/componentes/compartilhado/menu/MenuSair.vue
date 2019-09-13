<template>
    <div>
        <ul class="collection dropdown-content" id="outrasOpcoes">
            <li class="collection-item">
                <div class="row">                    
                    <div class="col s4">
                        <i id="circuloIniciais"><span id="iniciais">{{iniciais}}</span></i>
                    </div>
                    <div class="col s6">
                        <div>
                            <span><b>{{nome}}</b></span> 
                        </div>
                        <div>
                            <span>{{email}}</span>  
                        </div>
                        <div style="margin-top: 20px;" class="center">
                            <router-link :to="{name: 'login'}" id="btnSair">Logout</router-link>
                        </div>
                    </div>      
                </div>                        
            </li>
        </ul>
    </div>
</template>

<script>
import LoginService from '../../../classes/services/LoginService.js';
import MenuSair from '../../../classes/model/MenuSair.js';

export default {
    props: ['corMenuSair'],

    data(){
        return{
            nome: '',
            email: '',
            iniciais: ''
        }
    },

    beforeCreate(){
        let rotas = this.$router;

        let loginService = new LoginService(rotas);
        loginService.get()
            .then(dados => {
                let menuSair = new MenuSair(dados.Nome, dados.Email);
                menuSair.geraIniciais();
                
                let arrNome = menuSair.Nome.split(" "); 

                if(arrNome.length >= 2)
                    this.nome = arrNome[0] + " " + arrNome[1];
                else
                    this.nome = menuSair.Nome;

                this.email = menuSair.Email;
                this.iniciais = menuSair.Iniciais;
            });
    },

    mounted(){
        document.getElementById("btnSair").className = `waves-effect waves-light btn btn-small ${this.corMenuSair}`; 
        document.getElementById("circuloIniciais").className = `material-icons circle center ${this.corMenuSair}`;      
    }
}
</script>

<style>
#outrasOpcoes{
    width: 400px;
}
#circuloIniciais{
    color:white;
    height: 100px;
    font-size: 2rem;
}
#iniciais{
    display: block; 
    padding-top: 12px;
}
</style>

