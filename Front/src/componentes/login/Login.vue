<template>
  <div class="row">
    <transition name="slide-fade">
      <div v-if="show" class="col s10 m6 l4 offset-s1 offset-m3 offset-l4 form"> 
        <div class="row center">
          <img src="../../assets/gapi_branco.png" width="200px" height="100px">
        </div>     
        <div class="row">
          <div class="input-field col s8 offset-s1 campoEmail">
            <input placeholder="E-mail" class="campoText" id="email" type="text" autocomplete="off">
          </div>
          <div class="input-field col s1 iconeUser">
            <i class="small material-icons icone">account_circle</i>
          </div>
        </div>  
        <div class="row">
          <div class="input-field col s8 offset-s1" id="campoSenha">
            <input placeholder="Senha" class="campoText" id="senha" type="password" autocomplete="off">
          </div>
          <div class="input-field col s1" id="iconeSenha">
            <i class="small material-icons icone">lock</i>
          </div>
        </div> 
        <div class="row">
          <div class="col s12" id="campoBtnEnvia">
            <button v-on:click="loga" type="button" class="waves-effect waves-light btn col s10 offset-s1 purple darken-4 btnLogin">Login</button>
          </div>
        </div> 
        <div class="row">
          <div class="col s9 m9 l5 offset-s3 offset-m3 offset-l4" id="esqueciSenha">
            <span v-on:click="show = !show" class="campoText" id="btnEsqueciSenha"><u>Esqueci minha senha</u></span>
          </div>
        </div>
      </div>
    </transition>
    <transition name="slide-fade">
      <div v-if="!show" class="col s10 m6 l4 offset-s1 offset-m3 offset-l4 form"> 
        <div class="row center">
          <img src="../../assets/gapi_branco.png" width="200px" height="100px">
        </div>
        <div class="row">
          <div class="center">
            <span class="campoText">Informe o seu e-mail e enviaremos a senha para você.</span>
          </div>
        </div>     
        <div class="row">
          <div class="input-field col s8 offset-s1">
            <input placeholder="E-mail" class="campoText" id="emailEsqueciSenha" type="text" autocomplete="off">
          </div>
          <div class="input-field col s1 iconeUser2">
            <i class="small material-icons icone">account_circle</i>
          </div>
        </div>
        <div class="row">
          <div class="col s12">
            <button v-on:click="enviaSenha" type="button" class="waves-effect waves-light btn col s6 offset-s3 purple darken-4 btnLogin">Enviar a Senha</button>
          </div>
        </div>  
        <div class="row">
          <div class="col s12">
            <button v-on:click="voltar" type="button" class="waves-effect waves-light btn col s6 offset-s3 grey btnLogin">Voltar</button>
          </div>
        </div>   
      </div>
    </transition>
  </div>
</template>

<script>
import Login from '../../classes/model/Login.js';
import LoginService from '../../classes/services/LoginService.js'

export default {
  methods: {
    enviaSenha(){
      let email = document.getElementById("emailEsqueciSenha").value;
      if(email != '')
      {
        let loginService = new LoginService(this.$router);
        loginService.esqueciSenha(email)
          .then(res => {
            $('.toast').hide();
            M.toast({html: 'Caso realmente você esteja cadastrado, você receberá um e-mail com a sua senha.', classes: 'rounded'});
            this.show = !this.show;
          })
          .catch(err => {
            $('.toast').hide();
            M.toast({html: 'Ocorreu algum erro. Tente novamente mais tarde', classes: 'rounded'});
            this.show = !this.show;
          })
      }
      else
      {
        $('.toast').hide();
        M.toast({html: 'Informe o e-mail.', classes: 'rounded'});
      }
    },

    voltar(){
      this.show = !this.show;
    },

    loga(){
      this.realizaLogin();
    },

    realizaLogin(){
      let email = document.getElementById("email").value;
      let senha = document.getElementById("senha").value;

      if(email != '' && senha != '')
      {
        let dadosLogin = new Login(email, senha);
        let loginService = new LoginService(this.$router);
        loginService.post(dadosLogin);
      }
      else
      {
        $('.toast').hide();
        M.toast({html: 'Informe o e-mail e a senha.', classes: 'rounded'});
      }
    }
  },

  data(){
    return{
      show: true
    }
  },

  beforeCreate(){
    localStorage.setItem('token', '');
  }, 

  mounted(){
    //Aguarda para ter certeza de que a tela foi toda carregada
    this.$nextTick(function () {
      
      //Serve para não deixar a tela escura no mobile quando desloga
      var menuMobile = document.getElementsByClassName("sidenav-overlay");
      if(menuMobile.length > 0)
        menuMobile[menuMobile.length - 1].style = "display: none; opacity: 0;";

      //Esconde a barra de rolagem
      document.getElementsByClassName("body")[0].style = "overflow: hidden";
      //Esconde o rodape
      document.getElementById("rodape").style.display = "none";
    });

    //Serve para colocar a cor no fundo para a tela de login
    document.body.classList.add("body");

    //Ação para logar apertando o enter
    document.body.onkeypress = tecla.bind(this);

    //Realiza o login
    function tecla(){
      if(event.keyCode == 13){
        this.realizaLogin();
      }
    }  
  },

  beforeDestroy(){
    //Para colocar a barra de rolagem quando for para a próxima tela    
    document.getElementsByClassName("body")[0].style = "overflow: auto";
  },

  destroyed()
  {
    //Serve para remover a cor no fundo da tela de login
    document.body.classList.remove("body");
    //Coloca o rodape
    document.getElementById("rodape").style.display = "block";
  }
}
</script>

<style>
  .body{
    background: rgba(65,113,209,1);
    background: -moz-linear-gradient(left, rgba(65,113,209,1) 0%, rgba(87,17,128,1) 100%);
    background: -webkit-gradient(left top, right top, color-stop(0%, rgba(65,113,209,1)), color-stop(100%, rgba(87,17,128,1)));
    background: -webkit-linear-gradient(left, rgba(65,113,209,1) 0%, rgba(87,17,128,1) 100%);
    background: -o-linear-gradient(left, rgba(65,113,209,1) 0%, rgba(87,17,128,1) 100%);
    background: -ms-linear-gradient(left, rgba(65,113,209,1) 0%, rgba(87,17,128,1) 100%);
    background: linear-gradient(to right, rgba(65,113,209,1) 0%, rgba(87,17,128,1) 100%);
    filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#4171d1', endColorstr='#571180', GradientType=1 );
  }

  #toast-container {
    min-width: 10%;
    top: auto !important;
    bottom: 10%;
    right: 50%;
    transform: translateX(50%) translateY(50%);
  }
</style>

<style scoped>  
  .slide-fade-enter-active {
    transition: all .5s ease;
  }
  .slide-fade-leave-active {
    transition: all .5s ease cubic-bezier(1.0, 0.5, 0.8, 1.0);
  }
  .slide-fade-enter, .slide-fade-leave-to {
    transform: translateY(-10px);
    opacity: 0;
  }
  .form{
    height: 500px;
    margin-top: 80px; 
    border-radius:20px; 
  }
  input:not([type]), input[type=text]:not(.browser-default), input[type=password]:not(.browser-default), input[type=email]:not(.browser-default), input[type=url]:not(.browser-default), input[type=time]:not(.browser-default), input[type=date]:not(.browser-default), input[type=datetime]:not(.browser-default), input[type=datetime-local]:not(.browser-default), input[type=tel]:not(.browser-default), input[type=number]:not(.browser-default), input[type=search]:not(.browser-default), textarea.materialize-textarea
  {
    border-radius: 30px;
    border: 1px solid #ffffff;
    padding: 15px; 
    height: 15px;   
    padding-right: 55px;
  }  
  .row .input-field input:focus {
    border-bottom: 1px solid #ffffff !important;
  }
  .campoEmail{
    margin-top: 50px;
  }
  #campoSenha{
    margin-top: -20px;
  }
  #esqueciSenha{
    margin-top: 20px;
  }
  #campoBtnEnvia{
    margin-left: 10px;
  }
  #btnEsqueciSenha{
    cursor: pointer;
    padding-left: 10px;
  }
  .icone{
    color: #e6e6e6;
  }
  .iconeUser2{
    margin-top: 23px;
    margin-left: 5px;
  }
  .iconeUser{
    margin-top: 57px;
    margin-left: 5px;
  }
  #iconeSenha{
    margin-top: -12px;
    margin-left: 0px;
  }
  .btnLogin{
    border-radius: 30px;
    padding: 7px; 
    height: 50px;  
  } 
  .campoText{
    color: #ffffff;
  }

  @media only screen and (min-width: 992px){
    #campoBtnEnvia{
      margin-left: 0px;
    }
    #btnEsqueciSenha{
      padding-left: 0px;
    }
  }
</style>
