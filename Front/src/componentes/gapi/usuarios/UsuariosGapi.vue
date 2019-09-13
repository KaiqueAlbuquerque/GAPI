<template>
    <div class="row">
        <menu-gapi></menu-gapi>

        <div class="container">
            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastro">
                    <form>
                        <div class="row">
                            <div class="input-field col s12 m6 l6 xl6">
                                <i class="material-icons prefix">contact_mail</i>
                                <input v-on:blur="verificaEmail" id="email" type="email" class="validate" autocomplete="off">
                                <label id="lblEmail" for="email">E-mail</label>
                            </div>
                            <div class="input-field col s12 m6 l6 xl6">
                                <i class="material-icons prefix">lock</i>
                                <input id="senha" type="password" autocomplete="off">
                                <label id="lblSenha" for="senha">Senha</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s12 m12 l9 xl9">
                                <i class="material-icons prefix">account_circle</i>
                                <input id="nome" type="text" autocomplete="off">
                                <label id="lblNome" for="nome">Nome</label>
                            </div>
                            <div id="btnCadastrarUsuario" class="input-field right">
                                <a v-on:click="cadastrarUsuario" class="waves-effect waves-light btn purple darken-4 btn-small">Cadastrar</a>
                            </div>
                            <div id="btnAtualizarUsuario" class="input-field right">
                                <a v-on:click="alteraUsuario" class="waves-effect waves-light btn purple darken-4 btn-small">Atualizar</a>
                            </div>
                            <div id="btnCancelarUsuario" class="input-field right">
                                <a v-on:click="cancelar" class="waves-effect waves-light btn grey btn-small">Cancelar</a>
                            </div>
                        </div>
                    </form>     
                </meu-card>                            
            </div>

            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastrados">
                    <minha-tabela v-on:alterar="editaUsuario" v-on:excluir="excluirUsuario" :titulos="titulos" :corpos="corpos" :acoes="acoes"></minha-tabela>
                    <div class="row" id="paginacao">
                        <div class="right">
                            <paginate
                                :page-count="paginas"
                                :page-range="2"
                                :margin-pages="2"
                                :click-handler="mudaPagina"
                                :prev-text="'<'"
                                :next-text="'>'"
                                :container-class="'pagination'"
                                :page-class="'waves-effect'">
                            </paginate>
                        </div>
                    </div>
                </meu-card>
            </div>
            <meu-loader id="loaderUsuariosGapi"></meu-loader>     
        </div>
    </div>
</template>

<script>
//Componentes
import MenuGapi from '../menu/MenuGapi.vue';
import Card from '../../compartilhado/card/Card.vue';
import Tabela from '../../compartilhado/tabela/Tabela.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Serviços
import UsuarioAutenticacaoService from '../../../classes/services/autenticacao/UsuarioAutenticacaoService.js';

export default {
    components: {
        'menu-gapi': MenuGapi,
        'meu-card': Card,
        'minha-tabela': Tabela,
        'meu-loader': Loader
    },

    methods: {
        mudaPagina(page){
            this.preencheTabela((page - 1));
        },

        aguarde(){
            let loader = document.getElementById("loaderUsuariosGapi");
            loader.style.display = 'block';
        },

        naoAguarde(){
            let loader = document.getElementById("loaderUsuariosGapi");
            loader.style.display = 'none';
        },

        verificaEmail(){
            let email = document.getElementById("email");
            let existe = false;

            if(email.classList[1] == "valid"){
                this.aguarde();

                let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);

                usuarioAutenticacaoService.consultaPorEmail(email.value)
                    .then(usuario => {
                        if(usuario != null){
                            this.emailExiste(usuario);
                            this.usuarioExistente = usuario;
                            
                            for(let i = 0; i < this.corpos.length; i++){
                                if(this.corpos[i].valor[1] == email.value){
                                    existe = true;
                                }
                            }

                            if(existe){
                                this.botoesAlterar();
                                this.usuarioAlteracao = this.usuarioExistente;
                            }
                            else{
                                document.getElementById("btnCancelarUsuario").style.display = "block";
                            }
                        }
                        else{
                            this.emailNaoExiste();
                        }
                        this.naoAguarde();
                    })
                    .catch(erro => this.naoAguarde());
            }
            else if(email.classList[1] == "invalid"){
                this.emailNaoExiste();
                $('.toast').hide();
                M.toast({html: 'Informe um e-mail válido.', classes: 'rounded'});
            }
        },

        cadastrarUsuario(){
            if(this.validaCampos()){
                this.aguarde();

                if(this.usuarioExistente == ""){
                    let usuario = {
                        "Usuario": {
                            "Email": document.getElementById("email").value,
                            "Senha": document.getElementById("senha").value,
                            "Nome": document.getElementById("nome").value,
                            "Ativo": true
                        },
                        "IdsAcesso": [1,2,3]
                    }

                    let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                    usuarioAutenticacaoService.cadastrar(usuario)
                        .then(usuario => {                            
                            this.preencheTabela(0);
                            this.arrumaInputs();
                            this.naoAguarde();
                            $('.toast').hide();
                            M.toast({html: 'Usuário cadastrado com sucesso.', classes: 'rounded'});
                        })
                        .catch(erro => this.naoAguarde());
                }
                else{
                    this.usuarioExistente.Email = document.getElementById("email").value;
                    this.usuarioExistente.Nome = document.getElementById("nome").value;
                    this.usuarioExistente.Senha = document.getElementById("senha").value;

                    let usuario = {
                        "Usuario": this.usuarioExistente,
                        "IdsAcesso": [1,2,3]
                    }

                    let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                    usuarioAutenticacaoService.alterar(usuario)
                        .then(usuario => {                            
                            this.preencheTabela(0);
                            this.arrumaInputs();
                            this.naoAguarde();
                            $('.toast').hide();
                            M.toast({html: 'Usuário alterado com sucesso.', classes: 'rounded'});
                        })
                        .catch(erro => this.naoAguarde());
                }
            }
        },

        alteraUsuario(){
            if(this.validaCampos()){
                this.aguarde();	

                let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                usuarioAutenticacaoService.consultaPorId(this.usuarioAlteracao.UsuarioId)
                    .then(usuarioAutenticacao => {
                        usuarioAutenticacao.Senha = document.getElementById("senha").value;
                        usuarioAutenticacao.Nome = document.getElementById("nome").value;

                        let idsAcesso = [];
                    
                        usuarioAutenticacao.Acessos.forEach(acesso => {                            
                            idsAcesso.push(acesso.AcessoId);                            
                        });

                        let usuarioAlteracao = {
                            "Usuario": usuarioAutenticacao,
                            "IdsAcesso": idsAcesso
                        }

                        usuarioAutenticacaoService.alterar(usuarioAlteracao)
                            .then(usuario => {                                
                                this.preencheTabela(0);
                                this.arrumaInputs();
                                this.botoesCadastrar();
                                this.naoAguarde();
                                $('.toast').hide();
                                M.toast({html: 'Usuário alterado com sucesso.', classes: 'rounded'});                                                                 
                            })
                            .catch(erro => this.naoAguarde());               
                    })
                    .catch(erro => this.naoAguarde());
            }
        },

        cancelar(){
            this.botoesCadastrar();
            this.arrumaInputs();
            this.habilitaInputEmail();
        },

        editaUsuario(obj){
            this.aguarde();

            let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
            usuarioAutenticacaoService.consultaPorId(obj.id)
                .then(usuarioAutenticacao => {
                    document.getElementById("email").value = usuarioAutenticacao.Email;
                    document.getElementById("lblEmail").classList.add("active");
                    
                    this.emailExiste(usuarioAutenticacao); 
                    
                    this.usuarioAlteracao = usuarioAutenticacao;
                    this.botoesAlterar();
                    $('html, body').animate({ scrollTop: 0 }, 500);
                    this.naoAguarde();
                })
                .catch(erro => this.naoAguarde());                
        },

        excluirUsuario(obj){
            this.aguarde();
            
            let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
            usuarioAutenticacaoService.consultaPorId(obj.id)
                .then(usuarioAutenticacao => {
                    usuarioAutenticacao.Ativo = false;
                    
                    let idsAcesso = [];
                    
                    let usuarioEnviar = {
                        "Usuario": usuarioAutenticacao,
                        "IdsAcesso": idsAcesso
                    }

                    usuarioAutenticacaoService.alterar(usuarioEnviar)
                        .then(res => {
                            $('.toast').hide();
                            M.toast({html: 'Usuário excluido com sucesso.', classes: 'rounded'});
                            this.preencheTabela(0);
                            this.botoesCadastrar();
                            this.arrumaInputs();
                            this.naoAguarde()
                        })
                        .catch(erro => this.naoAguarde());   
                })
                .catch(erro => this.naoAguarde());                
        },

        preencheTabela(pagina){
            this.aguarde();

            let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
            usuarioAutenticacaoService.consultaTodos(pagina)
                .then(usuarios => {
                    this.corpos = [];

                    this.paginas = usuarios.QuantidadePaginas;
                                    
                    usuarios.Usuarios.forEach(usuario => {
                        this.corpos.push({"id": usuario.UsuarioId, "valor": [usuario.Nome, usuario.Email]});                        
                    }) 

                    if(this.paginas < 2)
                        document.getElementById("paginacao").style.display = "none";
                    else
                        document.getElementById("paginacao").style.display = "block";
                    
                    this.naoAguarde();
                })
                .catch(erro => this.naoAguarde())
        },

        validaCampos(){
            let email = document.getElementById("email");
            let senha = document.getElementById("senha");
            let nome = document.getElementById("nome");

            $('.toast').hide();

            if(email.value == ""){
                M.toast({html: 'Informe o e-mail.', classes: 'rounded'});
                return false;
            }
            else if(email.classList[1] == "invalid"){
                M.toast({html: 'Informe um e-mail válido.', classes: 'rounded'});
                return false;
            }
            else if(senha.value == ""){
                M.toast({html: 'Informe a senha.', classes: 'rounded'});
                return false;
            }
            else if(nome.value == ""){
                M.toast({html: 'Informe o nome.', classes: 'rounded'});                
                return false;
            }
            else{
                return true;
            }
        },

        arrumaInputs(){
            let email = document.getElementById("email");
            email.value = "";
            email.classList.remove("valid");
            document.getElementById("lblEmail").classList.remove("active");

            let senha = document.getElementById("senha");
            senha.value = "";
            document.getElementById("lblSenha").classList.remove("active");

            let nome = document.getElementById("nome");
            nome.value = "";
            document.getElementById("lblNome").classList.remove("active");

            this.habilitaInputEmail();
        },

        botoesCadastrar(){
            this.usuarioAlteracao = "";

            document.getElementById("btnCadastrarUsuario").style.display = "block";
            document.getElementById("btnCancelarUsuario").style.display = "none";
            document.getElementById("btnAtualizarUsuario").style.display = "none";
        },

        botoesAlterar(){
            document.getElementById("btnCadastrarUsuario").style.display = "none";
            document.getElementById("btnCancelarUsuario").style.display = "block";
            document.getElementById("btnAtualizarUsuario").style.display = "block";
        },

        emailExiste(usuario){
            this.desabilitaInputEmail();

            document.getElementById("senha").value = usuario.Senha;
            document.getElementById("lblSenha").classList.add("active");
            document.getElementById("nome").value = usuario.Nome;
            document.getElementById("lblNome").classList.add("active");
        },

        emailNaoExiste(){
            this.usuarioExistente = "";

            document.getElementById("senha").value = "";
            document.getElementById("senha").classList.remove("valid");
            document.getElementById("lblSenha").classList.remove("active");
            document.getElementById("nome").value = "";
            document.getElementById("nome").classList.remove("valid");
            document.getElementById("lblNome").classList.remove("active");
        },

        desabilitaInputEmail(){
            document.getElementById("email").setAttribute("disabled", true);
        },

        habilitaInputEmail(){
            document.getElementById("email").removeAttribute("disabled", true);
        }
    },

    data(){
        return{
            //Controlador
            usuarioExistente: "",
            usuarioAlteracao: "",

            //Paginação
            paginas: 0,

            //Card branco
            tamanho: "col s12",
            headerCardCadastro: {
                titulo: "Cadastro de usuários"
            },
            headerCardCadastrados: {
                titulo: "Usuários cadastrados"
            },

            //Tabela
            titulos: ['Nome', 'Email'],
            corpos: [],
            acoes: true,
        }
    },

    mounted(){
        this.botoesCadastrar();
        this.preencheTabela(0); 
    }   
}
</script>

<style>
.pagination li.active {
    background-color: #4a148c;
}

@media(max-width: 600px){
    #divisor{
        display: none;
    }
}
</style>

<style scoped>
#paginacao{
    padding-top: 15px;
    display: none;
}
.row {
    margin-bottom: 0px;
}
#btnCancelarUsuario{
    margin-right: 5px;
}
</style>