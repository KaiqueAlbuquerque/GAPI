<template>
    <div class="row">
        <menu-qrCode></menu-qrCode>

        <div class="container">
            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastro">
                    <form>
                        <div class="row">
                            <div class="input-field col s12 m7 l7 xl7">
                                <i class="material-icons prefix">contact_mail</i>
                                <input v-on:blur="verificaEmail" id="email" type="email" class="validate" autocomplete="off">
                                <label id="lblEmail" for="email">E-mail</label>
                            </div>
                            <div class="input-field col s12 m5 l5 xl5">
                                <i class="material-icons prefix">lock</i>
                                <input id="senha" type="password" autocomplete="off">
                                <label id="lblSenha" for="senha">Senha</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s12 m7 l7 xl7">
                                <i class="material-icons prefix">account_circle</i>
                                <input id="nome" type="text" autocomplete="off">
                                <label id="lblNome" for="nome">Nome</label>
                            </div>
                            <div class="input-field col s12 m5 l5 xl5">
                                <i class="material-icons prefix">business</i>
                                <select id="clientes">
                                    <option value="" disabled selected>Selecione o Cliente</option>
                                </select>
                                <label>Cliente</label>
                            </div>
                        </div>
                        <div class="row">
                            <div id="btnCadastrarUsuario" class="input-field col right">
                                <a v-on:click="cadastrarUsuario" class="waves-effect waves-light btn grey darken-3 btn-small">Cadastrar</a>
                            </div>
                            <div id="btnAtualizarUsuario" class="input-field right">
                                <a v-on:click="alteraUsuario" class="waves-effect waves-light btn grey darken-3 btn-small">Atualizar</a>
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
        </div>
        <meu-loader id="loaderUsuariosQr"></meu-loader>
    </div>
</template>

<script>
//Componentes
import MenuQrCode from '../menu/MenuQrCode.vue';
import Card from '../../compartilhado/card/Card.vue';
import Tabela from '../../compartilhado/tabela/Tabela.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Serviços
import UsuarioAutenticacaoService from '../../../classes/services/autenticacao/UsuarioAutenticacaoService.js';
import ClienteService from '../../../classes/services/qrCode/ClienteService.js';
import UsuarioService from '../../../classes/services/qrCode/UsuarioService.js';

export default {
    components: {
        'menu-qrCode': MenuQrCode,
        'meu-card': Card,
        'minha-tabela': Tabela,
        'meu-loader': Loader
    },

    methods: {
        mudaPagina(page){
            this.preencheTabela(this.idClienteFiltro, (page - 1));
        },

        aguarde(){
            let loader = document.getElementById("loaderUsuariosQr");
            loader.style.display = 'block';
        },

        naoAguarde(){
            let loader = document.getElementById("loaderUsuariosQr");
            loader.style.display = 'none';
        },

        buscaClientes(){
            let clienteService = new ClienteService(this.$router);
            clienteService.consultaTodosCombo()
                .then(clientes => {
                    for(let i = 0; i < clientes.length; i++){
                        document.getElementById("clientes").options[i+1] = new Option(clientes[i].NomeCliente,clientes[i].ClienteId);
                    }

                    this.reiniciaSelect();
                })
        },

        verificaEmail(){
            let email = document.getElementById("email");

            if(email.classList[1] == "valid"){
                this.aguarde();

                let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);

                usuarioAutenticacaoService.consultaPorEmail(email.value)
                    .then(usuario => {
                        if(usuario != null){
                            this.emailExiste(usuario);
                            this.usuarioExistente = usuario;
                            document.getElementById("btnCancelarUsuario").style.display = "block";
                        }
                        else{
                            this.emailNaoExiste();
                        }
                        this.naoAguarde();
                    })
                    .catch(erro => this.naoAguarde())
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

                if(this.usuarioExistente != ""){
                    let usuarioService = new UsuarioService(this.$router);
                    usuarioService.consultaPorIdLogin(this.usuarioExistente.UsuarioId)
                        .then(retorno => {
                            if(!retorno){
                                let idsAcesso = [];
                    
                                this.usuarioExistente.Acessos.forEach(acesso => {
                                    if(acesso.AcessoId != 2){
                                        idsAcesso.push(acesso.AcessoId);
                                    }
                                });

                                idsAcesso.push(2);

                                let usuario = {
                                    "Usuario": {
                                        "UsuarioId": this.usuarioExistente.UsuarioId,
                                        "Email": this.usuarioExistente.Email,
                                        "Senha": document.getElementById("senha").value,
                                        "Nome": document.getElementById("nome").value,
                                        "Ativo": true
                                    },
                                    "IdsAcesso": idsAcesso
                                }
                                    
                                let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                                usuarioAutenticacaoService.alterar(usuario)
                                    .then(usuario => {
                                        
                                        let usuarioMarca = {
                                            "IdLogin": usuario.UsuarioId,
                                            "Ativo": true,
                                            "ClienteId": document.getElementById("clientes").value
                                        };

                                        usuarioService.cadastrar(usuarioMarca)
                                            .then(res => {
                                                this.preencheTabela(0,0);
                                                this.arrumaInputs();
                                                this.naoAguarde();
                                            })
                                            .catch(erro => this.naoAguarde())
                                    })
                                    .catch(erro => this.naoAguarde())
                            }
                            else{
                                $('.toast').hide();
                                M.toast({html: 'Usuário já está cadastrado.', classes: 'rounded'});
                            }
                            this.naoAguarde();
                        })
                        .catch(erro => this.naoAguarde());
                }
                else{
                    let usuario = {
                        "Usuario": {
                            "Email": document.getElementById("email").value,
                            "Senha": document.getElementById("senha").value,
                            "Nome": document.getElementById("nome").value,
                            "Ativo": true
                        },
                        "IdsAcesso": [2]
                    }

                    let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                    usuarioAutenticacaoService.cadastrar(usuario)
                        .then(usuario => {
                            let usuarioService = new UsuarioService(this.$router);
                            
                            let usuarioMarca = {
                                "IdLogin": usuario.UsuarioId,
                                "Ativo": true,
                                "ClienteId": document.getElementById("clientes").value
                            };

                            usuarioService.cadastrar(usuarioMarca)
                                .then(res => {
                                    this.preencheTabela(0,0);
                                    this.arrumaInputs();
                                    this.naoAguarde();
                                })
                                .catch(erro => this.naoAguarde())
                        })
                        .catch(erro => this.naoAguarde())
                }
            }
        },

        alteraUsuario(){
            if(this.validaCampos()){
                this.aguarde();

                let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                usuarioAutenticacaoService.consultaPorId(this.usuarioAlteracao.IdLogin)
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
                                this.usuarioAlteracao.ClienteId = document.getElementById("clientes").value;

                                let usuarioService = new UsuarioService(this.$router);
                                usuarioService.alterar(this.usuarioAlteracao)
                                    .then(res => {
                                        this.preencheTabela(0,0);
                                        this.arrumaInputs();
                                        this.botoesCadastrar();
                                        this.naoAguarde();
                                        $('.toast').hide();
                                        M.toast({html: 'Usuário alterado com sucesso.', classes: 'rounded'});
                                    })
                                    .catch(erro => this.naoAguarde())
                            })   
                            .catch(erro => this.naoAguarde())              
                    })
                    .catch(erro => this.naoAguarde())
            }
        },

        cancelar(){
            this.botoesCadastrar();
            this.arrumaInputs();
            this.habilitaInputEmail();
        },

        editaUsuario(obj){
            this.aguarde();

            let usuarioService = new UsuarioService(this.$router);
            usuarioService.consultaPorId(obj.id)
                .then(usuario => {
                    let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                    usuarioAutenticacaoService.consultaPorId(usuario.IdLogin)
                        .then(usuarioAutenticacao => {
                            document.getElementById("email").value = usuarioAutenticacao.Email;
                            document.getElementById("lblEmail").classList.add("active");
                            
                            this.emailExiste(usuarioAutenticacao); 
                            
                            this.usuarioAlteracao = usuario;
                            document.getElementById("clientes").value = usuario.ClienteId;
                            this.reiniciaSelect();
                            this.botoesAlterar();
                            $('html, body').animate({ scrollTop: 0 }, 500);
                            this.naoAguarde();
                        })
                        .catch(erro => this.naoAguarde())
                })
                .catch(erro => this.naoAguarde())
        },

        excluirUsuario(obj){
            this.aguarde();

            let usuarioService = new UsuarioService(this.$router);
            usuarioService.consultaPorId(obj.id)
                .then(usuario => {
                    usuario.Ativo = false;

                    usuarioService.alterar(usuario)
                        .then(usuarioAlterado => {
                            let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                            
                            usuarioAutenticacaoService.consultaPorId(usuarioAlterado.IdLogin)
                                .then(usuarioAutenticacao => {
                                    let idsAcesso = [];
                                    
                                    usuarioAutenticacao.Acessos.forEach(acesso => {
                                        if(acesso.AcessoId != 2){
                                            idsAcesso.push(acesso.AcessoId);
                                        }
                                    });

                                    if(idsAcesso.length == 0){
                                        usuarioAutenticacao.Ativo = false;
                                    }

                                    let usuarioEnviar = {
                                        "Usuario": usuarioAutenticacao,
                                        "IdsAcesso": idsAcesso
                                    }

                                    usuarioAutenticacaoService.alterar(usuarioEnviar)
                                        .then(res => {
                                            $('.toast').hide();
                                            M.toast({html: 'Usuário excluido com sucesso.', classes: 'rounded'});
                                            this.preencheTabela(this.idClienteFiltro, 0);
                                            this.botoesCadastrar();
                                            this.arrumaInputs();
                                            this.naoAguarde();
                                        })
                                        .catch(erro => this.naoAguarde())
                                })
                                .catch(erro => this.naoAguarde())
                        })
                        .catch(erro => this.naoAguarde())
                })
                .catch(erro => this.naoAguarde())
        },

        preencheTabela(cliente, pagina){
            this.aguarde();

            let usuarioService = new UsuarioService(this.$router);
            usuarioService.consultaTodos(cliente, pagina)
                .then(usuarios => {
                    this.corpos = [];

                    this.paginas = usuarios.QuantidadePaginas;
                                    
                    usuarios.Usuarios.forEach(usuario => {
                        let usuarioAutenticacaoService = new UsuarioAutenticacaoService(this.$router);
                            usuarioAutenticacaoService.consultaPorId(usuario.IdLogin)
                                .then(usuarioAutenticacao => {
                                    this.corpos.push({"id": usuario.UsuarioId, "valor": [usuarioAutenticacao.Nome, usuario.Cliente.NomeCliente]});

                                    if(this.paginas < 2)
                                        document.getElementById("paginacao").style.display = "none";
                                    else
                                        document.getElementById("paginacao").style.display = "block";

                                    this.naoAguarde();
                                })
                                .catch(erro => this.naoAguarde())
                    })                    
                })
                .catch(erro => this.naoAguarde())
        },

        validaCampos(){
            let email = document.getElementById("email");
            let senha = document.getElementById("senha");
            let nome = document.getElementById("nome");
            let cliente = document.getElementById("clientes");

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
            else if(cliente.value == ""){
                M.toast({html: 'Informe o cliente.', classes: 'rounded'});                
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

            let cliente = document.getElementById("clientes");
            cliente.value = "";

            this.reiniciaSelect();
            this.habilitaInputEmail();
        },

        reiniciaSelect(){
            let elems = document.querySelectorAll('select');
            let options = document.querySelectorAll('option');
            let instances = M.FormSelect.init(elems, options);
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
            idClienteFiltro: 0,

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
            titulos: ['Nome', 'Cliente'],
            corpos: [],
            acoes: true,
        }
    },

    mounted(){
        this.$nextTick(function () {
            var elems = document.querySelectorAll('select');
            var a = M.FormSelect.init(elems);
        });

        this.botoesCadastrar();
        this.buscaClientes();
        this.preencheTabela(this.idClienteFiltro, 0); 
    }   
}
</script>

<style>
.pagination li.active {
    background-color: #424242;
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
